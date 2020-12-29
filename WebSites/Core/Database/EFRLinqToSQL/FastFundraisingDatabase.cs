using GA.BDC.Core.Configuration;
using GA.BDC.Core.Database.EFRLinqToSQL;
using System.Configuration;
namespace GA.BDC.Core.Database.EFRLinqToSQL
{
    public class FastFundraisingDatabase : FastFundraisingDataContext
    {
        
       
        public static FastFundraisingDataContext GetFastFundraisinDataContext()
        {
            var dbo = new FastFundraisingDataContext(FastFundraisingDatabase.ConnectionString);
             return dbo;
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

        

        public static bool IsProduction
        {
            get { return (ApplicationSettings.GetConfig()["FastFundraising.Production", "isProduction"].ToLower() == "true"); }
        }
    }
}
