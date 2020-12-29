using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class CountryMapper
   {
      public static Country Hydrate(country row)
      {
         return new Country
         {
            Name = row.country_name,
            Code = row.country_code
         };
      }
   }
}
