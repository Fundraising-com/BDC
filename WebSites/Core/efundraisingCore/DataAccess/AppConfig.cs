using System;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using System.Configuration;
namespace GA.BDC.Core.efundraisingCore.DataAccess 
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
            get { return ConfigurationManager.AppSettings["Common.Email.SmtpServer"]; }
		}
		#endregion

        #region EFundraisingProd configs
        public static string EFundraisingProdConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["EFundraisingProd"];
                return connectionString.ConnectionString;
            }
        }

        public static string EFundraisingProdDataProvider
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["EFundraisingProd"];
                return connectionString.ProviderName;
            }
        }
        #endregion


        #region EFundWeb configs
        public static string EFundWebConnectionString 
		{
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["EFundWeb"];
                return connectionString.ConnectionString;
            }
		}

		public static string EFundWebDataProvider
		{
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["EFundWeb"];
                return connectionString.ProviderName;
            }
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