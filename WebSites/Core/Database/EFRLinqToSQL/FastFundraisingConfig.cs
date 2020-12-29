using GA.BDC.Core.Configuration;
using System.Configuration;
namespace GA.BDC.Core.Database.EFRLinqToSQL
{
    class FastFundraising
    {
        public FastFundraising()
        {

        }

        public static string ConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["FastFundraising"];
                return connectionString.ConnectionString;
            }
        }

        public static string DataProvider
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["FastFundraising"];
                return connectionString.ProviderName;
            }
        }

        public static bool IsTrapLog
        {
            get { return (ApplicationSettings.GetConfig()[" FastFundraising.TrapLog", "value"].ToLower() == "true"); }
        }

        public static bool IsProduction
        {
            get { return (ApplicationSettings.GetConfig()[" FastFundraising.Production", "isProduction"].ToLower() == "true"); }
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
