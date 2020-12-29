using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class OrganizationTypeMapper
   {
      public static OrganizationType Hydrate(organization_type row)
      {
         return new OrganizationType
         {
            Id = row.organization_type_id,
            Name = row.organization_type_desc
         };
      }
   }
}
