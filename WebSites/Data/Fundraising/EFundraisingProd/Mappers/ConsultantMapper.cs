using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class ConsultantMapper
   {
      public static Consultant Hydrate(consultant consultant)
      {
         return new Consultant
         {
            Id = consultant.consultant_id,
            Name = consultant.name,
            IsActive = consultant.is_active,
            Email = consultant.email_address
         };
      }

      public static consultant Dehydrate(Consultant consultant)
      {
         return new consultant
         {
            consultant_id = consultant.Id,
            name = consultant.Name,
            is_active = consultant.IsActive,
            email_address = consultant.Email
         };
      }
   }
}
