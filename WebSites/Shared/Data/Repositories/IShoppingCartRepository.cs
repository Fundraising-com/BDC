using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IShoppingCartRepository : IRepository<ShoppingCart>
   {
      ShoppingCart GetByClientId(int clientId);
      ShoppingCart GetByOrderId(int orderId);
      IList<ShoppingCart> GetByUserId(string userId);
      IList<ShoppingCart> GetByAnonymousId(string anonymousId);
   }
}
