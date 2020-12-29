
namespace GA.BDC.Core.Database.EFRLinqToSQL
{
    public class EFRECommerceDatabase:EFRECommerceDataContext
    {
       
        

        public static EFRECommerceDataContext GetEFRECommerceDataContext()
        {
            var dbo = new EFRECommerceDataContext(EFRECommerceConfig.ConnectionString);
             return dbo;
        }
    }
}
