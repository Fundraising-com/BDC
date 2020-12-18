using System;
using efundraising.Collections;
using efundraising.Configuration;

namespace efundraising.efundraisingCore.DataAccess 
{
	/// <summary>
	/// Summary description for Config.
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

		#region EFundWeb configs
		public static string EFundWebConnectionStringRelease 
		{
			get { return (ApplicationSettings.GetConfig()["EFundWeb.SqlConnection.Release", "connectionString"]); }
		}

		public static string EFundWebDataProviderRelease 
		{
			get { return (ApplicationSettings.GetConfig()["EFundWeb.SqlConnection.Release", "provider"]); }
		}

		public static string EFundWebConnectionStringDebug 
		{
			get { return (ApplicationSettings.GetConfig()["EFundWeb.SqlConnection.Debug", "connectionString"]); }
		}

		public static string EFundWebDataProviderDebug 
		{
			get { return (ApplicationSettings.GetConfig()["EFundWeb.SqlConnection.Debug", "provider"]); }
		}
		
		public static bool IsEFundWebTrapLog 
		{
			get { return (ApplicationSettings.GetConfig()["EFundWeb.TrapLog", "value"].ToLower() == "true"); }
		}

		public static bool IsEFundWebProduction 
		{
			get { return (ApplicationSettings.GetConfig()["EFundWeb.Production", "isProduction"].ToLower() == "true"); }
		}
		#endregion
		
		#region Denali configs

		public static bool IsDenaliProduction 
		{
			get { return (ApplicationSettings.GetConfig()["Denali.Production", "isProduction"].ToLower() == "true"); }
		}


		public static string DenaliConnectionStringRelease 
		{
			get { return (ApplicationSettings.GetConfig()["Denali.SqlConnection.Release", "connectionString"]); }
		}
		
		public static string DenaliConnectionStringDebug 
		{
			get { return (ApplicationSettings.GetConfig()["Denali.SqlConnection.Debug", "connectionString"]); }
		}

		public static string DenaliDataProviderDebug 
		{
			get { return (ApplicationSettings.GetConfig()["Denali.SqlConnection.Debug", "provider"]); }
		}

		public static string DenaliDataProviderRelease 
		{
			get { return (ApplicationSettings.GetConfig()["Denali.SqlConnection.Debug", "provider"]); }
		}
		#endregion

	}
}