using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IReviewRepository : IRepository<Review>
   {
      /// <summary>
      /// Returns a list of Reviews for the given Product Id
      /// </summary>
      /// <param name="productId">Product Id</param>
      /// <returns></returns>
      IList<Review> GetAllByProductId(int productId);
      /// <summary>
      /// Returns a list of Reviews for the given Sales Id
      /// </summary>
      /// <param name="saleId">Sale Id</param>
      /// <returns></returns>
      IList<Review> GetAllBySaleId(int saleId);
   }
}
