using System;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.Collections;
using System.Configuration;
namespace GA.BDC.Core.EFundraisingCRM.DataAccess
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
