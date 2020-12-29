using GA.BDC.Data.Fundraising.EFundraisingProd.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Mappers
{
   public static class ProductClassMapper
   {
      public static ProductClass Hydrate(product_class row)
      {
         return new ProductClass
         {
            Id = row.product_class_id,
            Name = row.description
         };
      }
   }
}
