/* Title:	AppConfig
 * Author:	Jean-Francois Buist
 * Summary:	Data access layer object to retreive web.config/app.config values.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Configuration;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;

namespace GA.BDC.Core.ESubsGlobal.DataAccess {
	/// <summary>
	/// Summary description for Config.
	/// </summary>
	public class AppConfig {
		public AppConfig() {

		}

		public static string ConnectionString {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ESubsGlobal"];
                return connectionString.ConnectionString;
            }
		}

		public static string DataProvider {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ESubsGlobal"];
                return connectionString.ProviderName;
            }
		}

		public static bool IsTrapLog {
			get { return (ApplicationSettings.GetConfig()["ESubsGlobal.TrapLog", "value"].ToLower() == "true"); }
		}

        public static bool IsProduction
        {
            get { return (ApplicationSettings.GetConfig()["ESubsGlobal.Production", "isProduction"].ToLower() == "true"); }
        }

		public static bool IsMachineProduction {
			get { return (ApplicationSettings.GetConfig()["MachineProduction", "isProduction"].ToLower() == "true"); }
		}

		public static bool IsRedirectToStoreLog {
			get { return (ConfigurationManager.AppSettings["RedirectToStore.Log"].ToLower() == "true"); }
		}

        public static string RedirectToStoreLogType
        {
            get { return ConfigurationManager.AppSettings["RedirectToStore.Log.Type"]; }
        }

		public static string RedirectToStoreLogEmail {
			get { return ConfigurationManager.AppSettings["RedirectToStore.Email"]; }
		}

		public static string SmtpServer
		{
            get { return ConfigurationManager.AppSettings["Common.Email.SmtpServer"]; }
		}

		public static bool IsSmartNavigation {
			get { return (EnterpriseComponents.Helper.GetWebConfigValue("SmartNavigationEnable").ToLower() == "true"); }
		}

		public static string[] ResponsibleLeadsEmails
		{
			get 
			{ 
                var emails = ConfigurationManager.AppSettings["Leads.ResponsibleEmail"].Split(',');
                return emails;
			}
		}

	}
}
