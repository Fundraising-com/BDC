using System;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;

namespace GA.BDC.WEB.ScratchcardWeb.Components.Server
{
	/// <summary>
	/// Summary description for AppConfig.
	/// </summary>
	public class AppConfig {

		private AppConfig() {

		}

		public static string ConnectionStringRelease {
			get { return (ApplicationSettings.GetConfig()["Scratchcard.SqlConnection.Release", "connectionString"]); }
		}

		public static string DataProviderRelease {
			get { return (ApplicationSettings.GetConfig()["Scratchcard.SqlConnection.Release", "provider"]); }
		}

		public static string ConnectionStringDebug {
			get { return (ApplicationSettings.GetConfig()["Scratchcard.SqlConnection.Debug", "connectionString"]); }
		}

		public static string DataProviderDebug {
			get { return (ApplicationSettings.GetConfig()["Scratchcard.SqlConnection.Debug", "provider"]); }
		}

		public static bool IsTrapLog {
			get { return (ApplicationSettings.GetConfig()["Scratchcard.TrapLog", "value"].ToLower() == "true"); }
		}

		public static bool IsProduction {
			get { return (ApplicationSettings.GetConfig()["Scratchcard.Production", "isProduction"].ToLower() == "true"); }
		}

		public static int Version {
			get { return (int.Parse(ApplicationSettings.GetConfig()["Scratchcard.Version", "version"])); }
		}

		public static int SubVersion {
			get { return (int.Parse(ApplicationSettings.GetConfig()["Scratchcard.Version", "subversion"])); }
		}
	}
}
