using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using System.Configuration;
namespace GA.BDC.Core.Database.EFRLinqToSQL
{
    class EFRECommerceConfig
    {
        public EFRECommerceConfig()
        {

        }

        public static string ConnectionString
        {
             get {
                var connectionString = ConfigurationManager.ConnectionStrings["EFRECommerce"];
                return connectionString.ConnectionString;
            }
        }

        public static string DataProvider
        {
             get {
                var connectionString = ConfigurationManager.ConnectionStrings["EFRECommerce"];
                return connectionString.ProviderName;
            }
        }

        public static bool IsTrapLog
        {
            get { return (ApplicationSettings.GetConfig()["EFRECommerce.TrapLog", "value"].ToLower() == "true"); }
        }

        public static bool IsProduction
        {
            get { return (ApplicationSettings.GetConfig()["EFRECommerce.Production", "isProduction"].ToLower() == "true"); }
        }

        public static bool IsMachineProduction
        {
            get { return (ApplicationSettings.GetConfig()["MachineProduction", "isProduction"].ToLower() == "true"); }
        }

        public static string SmtpServer
        {
            get { return ConfigurationManager.AppSettings["Common.Email.SmtpServer"]; }
        }

       }
}
