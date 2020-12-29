using System.Data.Entity;

namespace GA.BDC.Data.Fundraising.ESubs.Tables
{
   public class DataProvider : DbContext
   {
      public DataProvider()
          : base("name=ESubs")
      {
      }
   }
}
