using System;
using System.Configuration;

using GA.BDC.Core.EmailVerification.Model.Configuration;

namespace GA.BDC.Core.EmailVerification.StrikeIron.Configuration
{
    internal sealed class ServiceCredentialsWrapper
    {
        private const string __CONFIG_PROPERTY_NAME_user = "user";
        private const string __CONFIG_PROPERTY_NAME_password = "password";
        private const string __CONFIG_PROPERTY_NAME_licenseKey = "licenseKey";
        private const string __CONFIG_PROPERTY_NAME_masterKey = "masterKey";

        private readonly string _user;
        private readonly string _password;
        private readonly string _licenseKey;
        private readonly string _masterKey;

        public ServiceCredentialsWrapper(ServiceSettings settings)
        {
            ServiceCredentials credentials = settings.Credentials;

            _user = (string)credentials.GetDynamicProperty(__CONFIG_PROPERTY_NAME_user);
            _password = (string)credentials.GetDynamicProperty(__CONFIG_PROPERTY_NAME_password);
            _licenseKey = (string)credentials.GetDynamicProperty(__CONFIG_PROPERTY_NAME_licenseKey);
            _masterKey = (string)credentials.GetDynamicProperty(__CONFIG_PROPERTY_NAME_masterKey);

            if (!string.IsNullOrWhiteSpace(_user) && string.IsNullOrWhiteSpace(_password) ||
                string.IsNullOrWhiteSpace(_user) && !string.IsNullOrWhiteSpace(_password))
            {
                throw new ConfigurationErrorsException(string.Format("Invalid '{0}' configuration for '{1}' email verification provider. Expected attributes: '{2}', '{3}'",
                                                                     ServiceCredentials.ConfigurationElementName,
                                                                     settings.Provider,
                                                                     __CONFIG_PROPERTY_NAME_user,
                                                                     __CONFIG_PROPERTY_NAME_password));
            }
            if (string.IsNullOrWhiteSpace(_user) && string.IsNullOrWhiteSpace(_password) &&
                string.IsNullOrWhiteSpace(_licenseKey) && string.IsNullOrWhiteSpace(_masterKey))
            {
                throw new ConfigurationErrorsException(string.Format("Invalid '{0}' configuration for '{1}' email verification provider. Expected attributes: '{2}', '{3}'",
                                                                     ServiceCredentials.ConfigurationElementName,
                                                                     settings.Provider,
                                                                     __CONFIG_PROPERTY_NAME_licenseKey,
                                                                     __CONFIG_PROPERTY_NAME_masterKey));
            }
        }

        public string User
        {
            get
            {
                return _user;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }

        public string LicenseKey
        {
            get
            {
                return _licenseKey;
            }
        }

        public string MasterKey
        {
            get
            {
                return _masterKey;
            }
        }
    }
}
