/* Title:	AppConfig
 * Author:	Jean-Francois Buist
 * Summary:	Data access layer object to retreive web.config/app.config values.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using System.Configuration;
namespace GA.BDC.Core.StoreJunction.DataAccess
{
	/// <summary>
	/// Summary description for AppConfig.
	/// </summary>
	public class AppConfig
	{
		private AppConfig() { }

		public static bool IsProduction {
			get { return (ApplicationSettings.GetConfig()["ESubsGlobal.Production", "isProduction"].ToLower() == "true"); }
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
	}
}


