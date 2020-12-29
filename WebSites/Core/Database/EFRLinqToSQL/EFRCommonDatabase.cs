using GA.BDC.Core.Configuration;
using GA.BDC.Core.Database.EFRLinqToSQL;
using System.Configuration;
namespace efundraising.EFRLinqToSQL
{
    public class EFRCommonDatabase : EFRCommonDataContext
    {
        EFRCommonDatabase() { }

        public static EFRCommonDataContext GetEFRCommonDataContext()
        {
            EFRCommonDataContext dbo;
            dbo = new EFRCommonDataContext(EFRCommonDatabase.ConnectionString);
            return dbo;
        }

        public static string ConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["EFRCommon"];
                return connectionString.ConnectionString;
            }
        }

        public static string DataProvider
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["EFRCommon"];
                return connectionString.ProviderName;
            }
        }

        public static bool IsProduction
        {
            get { return (ApplicationSettings.GetConfig()["EFRCommon.Production", "isProduction"].ToLower() == "true"); }
        }
    }
}
