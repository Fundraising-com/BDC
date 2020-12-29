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

namespace GA.BDC.Core.Database.Scratchcard.DataAccess
{
	/// <summary>
	/// Summary description for Config.
	/// </summary>
	public class AppConfig {
		public AppConfig() {

		}

		public static bool IsProduction {
			get { return (ApplicationSettings.GetConfig()["Scratchcard.Production", "isProduction"].ToLower() == "true"); }
		}
		
		public static int Version {
			get { return (int.Parse(ApplicationSettings.GetConfig()["Scratchcard.Version", "version"])); }
		}
		
		public static int SubVersion 
		{
			get { return (int.Parse(ApplicationSettings.GetConfig()["Scratchcard.Version", "subversion"])); }
		}

		public static string ConnectionStringRelease 
		{
			get { return (ApplicationSettings.GetConfig()["Scratchcard.SqlConnection.Release", "connectionString"]); }
		}

		public static string DataProviderRelease 
		{
			get { return (ApplicationSettings.GetConfig()["Scratchcard.SqlConnection.Release", "provider"]); }
		}

		public static string ConnectionStringDebug 
		{
			get { return (ApplicationSettings.GetConfig()["Scratchcard.SqlConnection.Debug", "connectionString"]); }
		}

		public static string DataProviderDebug 
		{
			get { return (ApplicationSettings.GetConfig()["Scratchcard.SqlConnection.Debug", "provider"]); }
		}

	}
}
