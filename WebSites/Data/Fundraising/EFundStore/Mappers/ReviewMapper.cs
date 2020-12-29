using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
   public static class ReviewMapper
   {
      public static Review Hydrate(review row, string productName = "")
      {
         return new Review
         {
            Id = row.Id,
            Comments = row.Comments,
            IsApproved = row.IsApproved,
            Name = row.Name,
            ProductId = row.ProductId,
            Rating = row.Rating,
            SaleId = row.SaleId ?? 0,
            Created = row.Created,
            Email = row.Email,
            ProductName = productName
         };
      }

      public static review DeHydrate(Review model)
      {
         return new review
         {
            Id = model.Id,
            Name = model.Name,
            Comments = model.Comments,
            IsApproved = model.IsApproved,
            ProductId = model.ProductId,
            Rating = model.Rating,
            SaleId = model.SaleId == 0 ? null : (int?)model.SaleId,
            Created = model.Created,
            Email = model.Email
         };
      }
   }
}
