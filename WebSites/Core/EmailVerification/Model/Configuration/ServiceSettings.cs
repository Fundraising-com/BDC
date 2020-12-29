using System;
using System.Configuration;
using GA.BDC.Core.EmailVerification.Properties;

namespace GA.BDC.Core.EmailVerification.Model.Configuration
{
    public sealed class ServiceSettings : ConfigurationElement
    {
        public const string ConfigurationElementName = "service";
        public const string ConfigurationElementKeyName = "name";
        public const string ConfigurationElementProviderAttributeName = "provider";
        public const string ConfigurationElementTimeoutAttributeName = "timeout";
        public const string ConfigurationElementOptionalSourceIDAttributeName = "optionalSourceID";
        public const string ConfigurationElementEnvironmentAttributeName = "environment";
        public const string ConfigurationElementEnabledAttributeName = "enabled";
        public const string ConfigurationElementLogAttributeName = "log";
        public const string ConfigurationElementMonitorVerificationAttributeName = "monitorVerification";
        public const string ConfigurationElementAnnualQuotaAttributeName = "annualQuota";

        public ServiceSettings()
        {
            Enabled = true;

            Log = true;

            MonitorVerification = false;
        }

        public ServiceSettings(string name, ServiceProvider provider, int timeout, string optionalSourceID, ServiceEnvironment environment)
        {
            Name = name;

            Provider = provider;

            Timeout = timeout;

            OptionalSourceID = optionalSourceID;

            Environment = environment;

            Enabled = true;

            Log = true;

            MonitorVerification = false;
        }

        [ConfigurationProperty(ConfigurationElementKeyName, DefaultValue = "BDC.Service", IsKey = true, IsRequired = true)]
        [StringValidator(MinLength = 3, MaxLength = 255)]
        public string Name
        {
            get
            {
                return (string)this[ConfigurationElementKeyName];
            }
            set
            {
                this[ConfigurationElementKeyName] = value;
            }
        }

        [ConfigurationProperty(ConfigurationElementProviderAttributeName, DefaultValue=ServiceProvider.Other, IsKey=false, IsRequired=true)]
        public ServiceProvider Provider
        {
            get 
            {
                return (ServiceProvider)this[ConfigurationElementProviderAttributeName]; 
            }
            set 
            { 
                this[ConfigurationElementProviderAttributeName] = value; 
            }
        }

        [ConfigurationProperty(ConfigurationElementTimeoutAttributeName, DefaultValue=30, IsKey = false, IsRequired = true)]
        [IntegerValidator(MinValue = 30, MaxValue = 90)]
        public int Timeout
        {
            get
            {
                return (int)this[ConfigurationElementTimeoutAttributeName];
            }
            set
            {
                this[ConfigurationElementTimeoutAttributeName] = value;
            }
        }

        [ConfigurationProperty(ConfigurationElementOptionalSourceIDAttributeName, IsKey = false, IsRequired = false)]
        public string OptionalSourceID
        {
            get
            {
                return (string)this[ConfigurationElementOptionalSourceIDAttributeName];
            }
            set
            {
                this[ConfigurationElementOptionalSourceIDAttributeName] = value;
            }
        }

        [ConfigurationProperty(ConfigurationElementEnvironmentAttributeName, DefaultValue=ServiceEnvironment.TEST, IsKey=false, IsRequired=true)]
        public ServiceEnvironment Environment
        {
            get 
            {
                return (ServiceEnvironment)this[ConfigurationElementEnvironmentAttributeName];
            }
            set 
            { 
                this[ConfigurationElementEnvironmentAttributeName] = value; 
            }
        }
        
        [ConfigurationProperty(ServiceCredentials.ConfigurationElementName, DefaultValue=default(ServiceCredentials), IsKey=false, IsRequired=true)]
        public ServiceCredentials Credentials
        {
            get 
            { 
                return (ServiceCredentials)this[ServiceCredentials.ConfigurationElementName]; 
            }
            set 
            { 
                this[ServiceCredentials.ConfigurationElementName] = value; 
            }
        }

        [ConfigurationProperty(ConfigurationElementEnabledAttributeName, IsKey = false, IsRequired = false)]
        public bool Enabled
        {
            get
            {
                return (bool)this[ConfigurationElementEnabledAttributeName];
            }
            set
            {
                this[ConfigurationElementEnabledAttributeName] = value;
            }
        }

        [ConfigurationProperty(ConfigurationElementLogAttributeName, IsKey = false, IsRequired = false)]
        public bool Log
        {
            get
            {
                return (bool)this[ConfigurationElementLogAttributeName];
            }
            set
            {
                this[ConfigurationElementLogAttributeName] = value;
            }
        }

        [ConfigurationProperty(ConfigurationElementMonitorVerificationAttributeName, IsKey = false, IsRequired = false)]
        public bool MonitorVerification
        {
            get
            {
                return (bool)this[ConfigurationElementMonitorVerificationAttributeName];
            }
            set
            {
                this[ConfigurationElementMonitorVerificationAttributeName] = value;
            }
        }

        [ConfigurationProperty(ConfigurationElementAnnualQuotaAttributeName, IsKey = false, IsRequired = false)]
        public int AnnualQuota
        {
            get
            {
                return (int)this[ConfigurationElementAnnualQuotaAttributeName];
            }
            set
            {
                this[ConfigurationElementAnnualQuotaAttributeName] = value;
            }
        }
    }
}
