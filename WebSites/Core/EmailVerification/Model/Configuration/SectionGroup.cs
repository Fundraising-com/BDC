using System;
using System.Configuration; 

namespace GA.BDC.Core.EmailVerification.Model.Configuration
{
    public sealed class SectionGroup : ConfigurationSectionGroup
    {
        public const string ConfigurationSectionGroupName = "BDC.emailVerification";

        [ConfigurationProperty(ServiceSettingsSection.ConfigurationSectionName)]
        public ServiceSettingsSection Services
        {
            get 
            {
                return (ServiceSettingsSection)base.Sections[ServiceSettingsSection.ConfigurationSectionName]; 
            }
        }
    }
}
