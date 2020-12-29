using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IFundraiserProductRepository : IRepository<FundraiserProduct>
   {
      IList<FundraiserProduct> GetAllByCategory(int categoryId);

      IList<FundraiserProduct> GetAllProducts();
    }
}
