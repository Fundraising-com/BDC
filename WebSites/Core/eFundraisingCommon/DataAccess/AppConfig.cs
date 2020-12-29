using System;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using System.Configuration;
namespace GA.BDC.Core.eFundraisingCommon.DataAccess
{
    /// <summary>
    /// Summary description for AppConfig.
    /// </summary>
    public class AppConfig
    {

        #region Common
        public static string SmtpServer
        {
            get { return ConfigurationManager.AppSettings["Common.Email.SmtpServer"]; }
        }
        #endregion

        #region EFRCommon configs
        public static string EFRCommonConnectionString
        {
            get {
                var connectionString = ConfigurationManager.ConnectionStrings["EFRCommon"];
                return connectionString.ConnectionString;
            }
        }

        public static string EFRCommonDataProvider
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["EFRCommon"];
                return connectionString.ProviderName;
            }
        }

        public static bool IsEFRCommonProduction
        {
            get { return (ApplicationSettings.GetConfig()["EFRCommon.Production", "isProduction"].ToLower() == "true"); }
        }
        #endregion








    }

   








}
