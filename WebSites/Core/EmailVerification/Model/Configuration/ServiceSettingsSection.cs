using System;
using System.Configuration;

namespace GA.BDC.Core.EmailVerification.Model.Configuration
{
    public sealed class ServiceSettingsSection : ConfigurationSection
    {
        public const string ConfigurationSectionName = "services";

        public static readonly string ConfigurationSectionNameQualified = SectionGroup.ConfigurationSectionGroupName + '/' + ConfigurationSectionName;

        [ConfigurationProperty("", IsRequired = false, IsDefaultCollection = true)]
        public ServiceSettingsCollection Services
        {
            get
            {
                return (ServiceSettingsCollection)this[""];
            }
        }
    }
}
