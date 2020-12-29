using System;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.Collections;

namespace GA.BDC.Core.eFundraisingStore.DataAccess {
	/// <summary>
	/// Summary description for AppConfig.
	/// </summary>
	internal class AppConfig {
		public AppConfig() {
			
		}
		
		#region Common
		public static string SmtpServer {
			get { return ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"]; }
		}
		#endregion

		#region EFundraisingStore configs
		public static string EFundraisingStoreConnectionStringRelease {
			get { return (ApplicationSettings.GetConfig()["EFundraisingStore.SqlConnection.Release", "connectionString"]); }
		}

		public static string EFundraisingStoreDataProviderRelease {
			get { return (ApplicationSettings.GetConfig()["EFundraisingStore.SqlConnection.Release", "provider"]); }
		}

		public static string EFundraisingStoreConnectionStringDebug {
			get { return (ApplicationSettings.GetConfig()["EFundraisingStore.SqlConnection.Debug", "connectionString"]); }
		}

		public static string EFundraisingStoreDataProviderDebug {
			get { return (ApplicationSettings.GetConfig()["EFundraisingStore.SqlConnection.Debug", "provider"]); }
		}
		
		public static bool IsEFundraisingStoreProduction {
			get { return (ApplicationSettings.GetConfig()["EFundraisingStore.Production", "isProduction"].ToLower() == "true"); }
		}
		#endregion
	}
}
