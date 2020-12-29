using System;
using System.Configuration;

using GA.BDC.Core.EmailVerification.Model;
using GA.BDC.Core.EmailVerification.Model.Configuration;

namespace GA.BDC.Core.EmailVerification.StrikeIron.Configuration
{
    internal static class ServiceSettingsEx
    {
        public static void Validate(this ServiceSettings settings)
        {
            if (settings != null)
            {
                if (settings.Provider != ServiceProvider.StrikeIron)
                {
                    string message =
                        string.Format("Configuration error in <{0}> section: <{1} {2}=\"{3}\" {4}=\"{5}\"...>; expected: <{1} {2}=\"{3}\" {4}=\"{6}\"...>;",
                                      ServiceSettingsSection.ConfigurationSectionNameQualified, ServiceSettings.ConfigurationElementName,
                                      ServiceSettings.ConfigurationElementKeyName, settings.Name, ServiceSettings.ConfigurationElementProviderAttributeName,
                                      settings.Provider, ServiceProvider.StrikeIron);

                    throw new ConfigurationErrorsException(message);
                }
            }
        }
    }
}
