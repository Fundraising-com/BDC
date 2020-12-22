using System;
using efundraising.Configuration;
using efundraising.Collections;

namespace efundraising.EFundraisingCRM.DataAccess
{
	/// <summary>
	/// Summary description for AppConfig.
	/// </summary>
	public class AppConfig
	{
		public AppConfig()
		{
			
		}
		
		#region Common
		public static string SmtpServer
		{
			get { return ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"]; }
		}
		#endregion

		#region EFundraisingProd configs
		public static string EFundraisingProdConnectionStringRelease 
		{
			get { return (ApplicationSettings.GetConfig()["EFundraisingProd.SqlConnection.Release", "connectionString"]); }
		}

		public static string EFundraisingProdDataProviderRelease 
		{
			get { return (ApplicationSettings.GetConfig()["EFundraisingProd.SqlConnection.Release", "provider"]); }
		}

		public static string EFundraisingProdConnectionStringDebug 
		{
			get { return (ApplicationSettings.GetConfig()["EFundraisingProd.SqlConnection.Debug", "connectionString"]); }
		}

		public static string EFundraisingProdDataProviderDebug 
		{
			get { return (ApplicationSettings.GetConfig()["EFundraisingProd.SqlConnection.Debug", "provider"]); }
		}
		
		public static bool IsEFundraisingProdTrapLog 
		{
			get { return (ApplicationSettings.GetConfig()["EFundraisingProd.TrapLog", "value"].ToLower() == "true"); }
		}

		public static bool IsEFundraisingProdProduction 
		{
			get { return (ApplicationSettings.GetConfig()["EFundraisingProd.Production", "isProduction"].ToLower() == "true"); }
		}
		#endregion
	}
}
