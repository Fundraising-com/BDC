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
namespace GA.BDC.Core.WebTracking.DataAccess
{
	/// <summary>
	/// Summary description for AppConfig.
	/// </summary>
	public class AppConfig
	{
		public AppConfig()
		{

		}

		public static string ConnectionString {
			get {
                var connectionString = ConfigurationManager.ConnectionStrings["WebTracking"];
                return connectionString.ConnectionString;
            }
		}

		public static string DataProvider {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["WebTracking"];
                return connectionString.ProviderName;
            }
		}

		public static int HostID {
			get { return (int.Parse(ApplicationSettings.GetConfig()["WebTracking.Setting", 0, "hostID"])); }
		}

		public static int WebSiteID {
			get { return (int.Parse(ApplicationSettings.GetConfig()["WebTracking.Setting", 0, "WebSiteID"])); }
		}

	}
}

