using System;
using System.Collections.Generic;

using GA.BDC.Core.EmailVerification.Model;
using GA.BDC.Core.EmailVerification.Properties;
using GA.BDC.Core.EmailVerification.StrikeIron.Configuration;

using log4net;

namespace GA.BDC.Core.EmailVerification.StrikeIron
{
    public class ValidationProcessor : ValidationProcessorBase
    {
        private readonly ServiceCredentialsWrapper _serviceCredentials;

        private IValidationRequest _request;

        private static readonly int[] __validStatusCodes;
        private static readonly int[] __catchAllStatusCodes;
        private static readonly int[] __potentiallyDangerousCatchAllStatusCodes;
        private static readonly int[] __potentiallyDangerousStatusCodes;
        private static readonly int[] __retryStatusCodes;
        private static readonly int[] __retryPotentiallyDangerousStatusCodes;
        private static readonly int[] __validReasonCodes;

        private bool allowPotentiallyDangerousEmails = false;

        public ILog Log { get; set; }

        static ValidationProcessor()
        {
            __validStatusCodes = new[] { (int)StatusCode.EMAIL_VALID };
            __catchAllStatusCodes = new[] { (int)StatusCode.DOMAIN_CONFIRMED };
            __potentiallyDangerousCatchAllStatusCodes = new[] { (int)StatusCode.DOMAIN_CONFIRMED_POTENTIALLY_DANGEROUS };
            __potentiallyDangerousStatusCodes = new[] { (int)StatusCode.EMAIL_VALID_POTENTIALLY_DANGEROUS };
            __retryStatusCodes = new[] { (int)StatusCode.ANALYTICS_IN_PROGRESS };
            __retryPotentiallyDangerousStatusCodes = new[] { (int)StatusCode.ANALYTICS_IN_PROGRESS_POTENTIALLY_DANGEROUS };
            __validReasonCodes = new[] { (int)ReasonCode.SERVER_WILL_ACCEPT, (int)ReasonCode.UNABLE_TO_VERIFY_AT_THIS_TIME };
        }

        public ValidationProcessor(string serviceName)
            : base(serviceName)
        {
            Log = LogManager.GetLogger(typeof(ValidationProcessor));

            _serviceCredentials = new ServiceCredentialsWrapper(Settings);
        }

        protected override void Init(IValidationRequest request)
        {
            _request = request;
        }

        private void ValidateResponseCode(int statusCodeNbr, int reasonCodeNbr, out ServiceResponseCode responseCode)
        {
            if (Enum.IsDefined(typeof(StatusCode), statusCodeNbr))
            {
                if (Array.IndexOf<int>(__validStatusCodes, statusCodeNbr) != -1)
                {
                    responseCode = ServiceResponseCode.Success;
                }
                else if (Array.IndexOf<int>(__catchAllStatusCodes, statusCodeNbr) != -1 &&
                         Array.IndexOf<int>(__validReasonCodes, reasonCodeNbr) != -1)
                {
                    responseCode = ServiceResponseCode.Success;
                }
                else if (Array.IndexOf<int>(__retryStatusCodes, statusCodeNbr) != -1)
                {
                    responseCode = ServiceResponseCode.Retry;
                }
                else if (allowPotentiallyDangerousEmails &&
                         Array.IndexOf<int>(__potentiallyDangerousStatusCodes, statusCodeNbr) != -1)
                {
                    responseCode = ServiceResponseCode.Success;
                }
                else if (allowPotentiallyDangerousEmails &&
                         Array.IndexOf<int>(__potentiallyDangerousCatchAllStatusCodes, statusCodeNbr) != -1 &&
                         Array.IndexOf<int>(__validReasonCodes, reasonCodeNbr) != -1)
                {
                    responseCode = ServiceResponseCode.Success;
                }
                else if (allowPotentiallyDangerousEmails &&
                         Array.IndexOf<int>(__retryPotentiallyDangerousStatusCodes, statusCodeNbr) != -1)
                {
                    responseCode = ServiceResponseCode.Retry;
                }
                else
                {
                    responseCode = ServiceResponseCode.Fail;
                }
            }
            else
            {
                responseCode = ServiceResponseCode.UnknownError;
            }
        }

        protected override IValidationResponse VerifyEmail()
        {
            if (!Settings.Enabled)
                return null;

            int remainingHits = GetRemainingHits();
            if (Settings.MonitorVerification)
            {
                if (Settings.AnnualQuota <= 0)
                {
                    throw new Exception(StrikeIronMessages.MissingAnnualQuota);
                }

                double rateRemaining = GetRemainingRate(Settings.AnnualQuota, remainingHits);
                if (rateRemaining <= 0)
                {
                    Log.ErrorFormat("[URGENT] StrikeIron Has Reached it's Limit: {0} remaining out of {1}",
                                    remainingHits, Settings.AnnualQuota);
                    return null;
                }
                else if (rateRemaining < StrikeIronSettings.Default.RemainingQuotaPercentDelta)
                {
                    Log.ErrorFormat("[URGENT] StrikeIron Has Reached The {0}% Threshold: {1} remaining out of {2}",
                                    StrikeIronSettings.Default.RemainingQuotaPercentDelta, remainingHits, Settings.AnnualQuota);
                }
            }

            if (_request == null)
            {                
                throw new Exception(StrikeIronMessages.ValidationRequestNullError);
            }
            if (string.IsNullOrWhiteSpace(_request.EmailAddress))
            {
                throw new Exception(StrikeIronMessages.ValidationRequestMissingEmailError);
            }

            ServiceResponseCode responseCode = ServiceResponseCode.UnknownError;

            EmailVerification siService = new EmailVerification();

            /* stores authentication credentials for a web service request */
            LicenseInfo authHeader = new LicenseInfo();

            /* stores authentication credentials for Registered StrikeIron users */
            RegisteredUser regUser = new RegisteredUser();

            regUser.UserID = _serviceCredentials.User;
            regUser.Password = _serviceCredentials.Password;

            authHeader.RegisteredUser = regUser;
            siService.LicenseInfoValue = authHeader;

            int timeout = Settings.Timeout;
            string OptionalSourceID = Settings.OptionalSourceID;

            SIWsOutputOfVerifyEmailRecord wsOutput = null;

            try
            {
                wsOutput = siService.VerifyEmail(_request.EmailAddress, timeout, OptionalSourceID);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format(StrikeIronMessages.ValidationError, exception.Message));
            }

            string description = string.Empty;
            if (wsOutput.ServiceResult.Reason != null)
            {
                /* reason codes further describe the reason a particular status code was returned */
                description = string.Format(StrikeIronMessages.ValidationResponseFullDescription,
                                            wsOutput.ServiceStatus.StatusNbr,
                                            wsOutput.ServiceStatus.StatusDescription,
                                            wsOutput.ServiceResult.Reason.Code,
                                            wsOutput.ServiceResult.Reason.Description);
            }
            else
            {
                description = string.Format(StrikeIronMessages.ValidationResponseDescription, wsOutput.ServiceStatus.StatusDescription);
            }

            ValidateResponseCode(wsOutput.ServiceStatus.StatusNbr, wsOutput.ServiceResult.Reason.Code, out responseCode);

            IValidationResponse response =
                new ValidationResponse(_request.EmailAddress, (int)responseCode,
                                       description, wsOutput.ServiceStatus.StatusNbr.ToString(), 
                                       responseCode == ServiceResponseCode.Success,
                                       responseCode == ServiceResponseCode.Retry);

            if (Settings.Log)
            {
                Log.WarnFormat("Executed StrikeIron EmailVerification for Email={0}, Remaining Hit Count={1}, ResponseCode={2}, ResponseMessage={3}, Source ID={4}",
                               _request.EmailAddress, remainingHits, response.ResponseCode, response.ResponseMessage, OptionalSourceID);
            }

            return response;
        }

        public int GetRemainingHits()
        {
            EmailVerification siService = new EmailVerification();

            /* stores authentication credentials for a web service request */
            LicenseInfo authHeader = new LicenseInfo();

            /* stores authentication credentials for Registered StrikeIron users */
            RegisteredUser regUser = new RegisteredUser();

            regUser.UserID = _serviceCredentials.User;
            regUser.Password = _serviceCredentials.Password;

            authHeader.RegisteredUser = regUser;
            siService.LicenseInfoValue = authHeader;

            try
            {
                siService.GetRemainingHits();
                return siService.SubscriptionInfoValue.RemainingHits;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format(StrikeIronMessages.ValidationError, exception.Message));
            }
        }

        public SIWsOutputOfSIWsResultArrayOfMethodStatusRecord GetStatusCodes()
        {
            EmailVerification siService = new EmailVerification();

            /* stores authentication credentials for a web service request */
            LicenseInfo authHeader = new LicenseInfo();

            /* stores authentication credentials for Registered StrikeIron users */
            RegisteredUser regUser = new RegisteredUser();

            regUser.UserID = _serviceCredentials.User;
            regUser.Password = _serviceCredentials.Password;

            authHeader.RegisteredUser = regUser;
            siService.LicenseInfoValue = authHeader;

            try
            {
                return siService.GetStatusCodes();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format(StrikeIronMessages.ValidationError, exception.Message));
            }
        }

        public SIWsOutputOfSIWsResultArrayOfReason GetReasonCodes()
        {
            EmailVerification siService = new EmailVerification();

            /* stores authentication credentials for a web service request */
            LicenseInfo authHeader = new LicenseInfo();

            /* stores authentication credentials for Registered StrikeIron users */
            RegisteredUser regUser = new RegisteredUser();

            regUser.UserID = _serviceCredentials.User;
            regUser.Password = _serviceCredentials.Password;

            authHeader.RegisteredUser = regUser;
            siService.LicenseInfoValue = authHeader;

            try
            {
                return siService.GetReasonCodes();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format(StrikeIronMessages.ValidationError, exception.Message));
            }
        }

        private double GetRemainingRate(int annualQuota, int remainingHits)
        {
            return 100.0 - Math.Round(100.0 * (double)(annualQuota - remainingHits) / annualQuota);
        }

        protected override void ValidateServiceEnvironment()
        {
            Settings.Validate();
        }
    }
}
