using System;
using System.Configuration;

using GA.BDC.Core.EmailVerification.Model.Configuration;

namespace GA.BDC.Core.EmailVerification.Model
{
    public abstract class ValidationProcessorBase : IValidationProcessor
    {
        private static ServiceSettingsSection __configSection;

        private readonly ServiceSettings _settings;

        protected ValidationProcessorBase()
        {
        }

        protected ValidationProcessorBase(string serviceName)
        {
            if (__configSection == null)
            {
                __configSection = (ConfigurationManager.GetSection(ServiceSettingsSection.ConfigurationSectionNameQualified) as ServiceSettingsSection);
            }
            if (__configSection == null)
            {
                string exceptionMessage = string.Format("Missing configuration section: '{0}'.", ServiceSettingsSection.ConfigurationSectionNameQualified);

                throw new ConfigurationErrorsException(exceptionMessage);
            }

            _settings = __configSection.Services[serviceName];
            if (_settings == null)
            {
                string exceptionMessage =
                   string.Format("The '{0}' configuration section does not contain a '{1}' configuration element with a '{2}' attribute value of '{3}'.",
                                 ServiceSettingsSection.ConfigurationSectionNameQualified, ServiceSettings.ConfigurationElementName,
                                 ServiceSettings.ConfigurationElementKeyName, serviceName);

                throw new ConfigurationErrorsException(exceptionMessage);
            }            
        }

        protected ServiceSettings Settings
        {
            get
            {
                return _settings;
            }
        }

        protected abstract void Init(IValidationRequest request);

        protected abstract IValidationResponse VerifyEmail();

        protected abstract void ValidateServiceEnvironment();

        #region IValidationProcessor Members

        public IValidationResponse ProcessRequest(IValidationRequest request)
        {
            // Validate Environment...
            ValidateServiceEnvironment();

            // Initialize...
            Init(request);

            IValidationResponse response = VerifyEmail();

            return response;
        }

        #endregion
    }
}
