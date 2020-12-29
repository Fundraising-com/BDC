using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class RegionMapper
   {
      public static Region Hydrate(State row)
      {
         return new Region
         {
            Name = row.State_Name,
            Code = row.State_Code,
            CountryCode = row.Country_Code
         };
      }
   }
}
