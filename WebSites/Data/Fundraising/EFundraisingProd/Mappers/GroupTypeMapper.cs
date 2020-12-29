using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class GroupTypeMapper
   {
      public static GroupType Hydrate(group_type row)
      {
         return new GroupType
         {
            Id = row.group_type_id,
            Name = row.description
         };
      }
   }
}
