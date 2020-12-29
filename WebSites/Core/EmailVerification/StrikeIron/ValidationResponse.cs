using System;

using GA.BDC.Core.EmailVerification.Model;

namespace GA.BDC.Core.EmailVerification.StrikeIron
{
    public sealed class ValidationResponse : IValidationResponse
    {
        private ValidationResponse()
            : base()
        {
        }

        internal ValidationResponse(string emailAddress, int responseCode, string responseMessage, string responseRaw,
                                    bool isValid, bool retryRequest)
            : this()
        {
            EmailAddress = emailAddress;

            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            ResponseRaw = responseRaw;

            IsValid = isValid;
            RetryRequest = retryRequest;
        }

        public string EmailAddress { get; private set; }

        public int ResponseCode { get; private set; }
        public string ResponseMessage { get; private set; }
        public string ResponseRaw { get; private set; }

        public bool IsValid { get; private set; }
        public bool RetryRequest { get; private set; }
    }
}
