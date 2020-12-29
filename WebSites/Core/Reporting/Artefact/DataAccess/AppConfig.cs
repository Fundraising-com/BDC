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

namespace GA.BDC.Core.Reporting.Artefact.DataAccess
{
	/// <summary>
	/// Summary description for Config.
	/// </summary>
	public class AppConfig {
		public AppConfig() {

		}

		public static string ConnectionStringRelease {
			get { return (ApplicationSettings.GetConfig()["ReportingCenter.SqlConnection.Release", "connectionString"]); }
		}

		public static string DataProviderRelease {
			get { return (ApplicationSettings.GetConfig()["ReportingCenter.SqlConnection.Release", "provider"]); }
		}

		public static string ConnectionStringDebug {
			get { return (ApplicationSettings.GetConfig()["ReportingCenter.SqlConnection.Debug", "connectionString"]); }
		}

		public static string DataProviderDebug {
			get { return (ApplicationSettings.GetConfig()["ReportingCenter.SqlConnection.Debug", "provider"]); }
		}

		public static bool IsTrapLog {
			get { return (ApplicationSettings.GetConfig()["ReportingCenter.TrapLog", "value"].ToLower() == "true"); }
		}

		public static bool IsProduction {
			get { return (ApplicationSettings.GetConfig()["ReportingCenter.Production", "isProduction"].ToLower() == "true"); }
		}

		public static bool IsMachineProduction {
			get { return (ApplicationSettings.GetConfig()["MachineProduction", "isProduction"].ToLower() == "true"); }
		}

	}
}
