using System;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class ShoppingCartsController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      /// <summary>
      /// Returns the Shopping Cart found
      /// </summary>
      /// <param name="id">Shopping Cart id</param>
      /// <returns>Shopping Cart</returns>
      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var shoppingCartRepository = efundStoreUnitOfWork.CreateRepository<IShoppingCartRepository>();
            var shoppingCart = shoppingCartRepository.GetById(id);
            foreach (var item in shoppingCart.Items)
            {
               item.Product = productRepository.GetById(item.ProductId);
               item.Product.Category = categoriesRepository.GetById(item.Product.CategoryId);
            }
            if (shoppingCart.PromotionCodeId > 0)
            {
               using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
               {
                  var promotionCodesRepository = efundraisingProdUnitOfWork.CreateRepository<IPromotionCodeRepository>();
                  shoppingCart.PromotionCode = promotionCodesRepository.GetById(shoppingCart.PromotionCodeId);
               }
            }
            return Ok(shoppingCart);
         }
      }

      /// <summary>
      /// Returns the first Shopping Cart found for the user that has a status of created
      /// </summary>
      /// <param name="userId">User id</param>
      /// <returns>Shopping Cart</returns>
      [HttpGet]
      public IHttpActionResult GetByUserId(string userId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var shoppingCartRepository = efundStoreUnitOfWork.CreateRepository<IShoppingCartRepository>();
            var shoppingCarts = shoppingCartRepository.GetByUserId(userId);

            if (shoppingCarts.All(p => p.Status != ShoppingCartStatus.Created)) return null;

            var shoppingCart =
               shoppingCarts.OrderByDescending(p => p.Created).First(p => p.Status == ShoppingCartStatus.Created);
            foreach (var item in shoppingCart.Items)
            {
               item.Product = productRepository.GetById(item.ProductId);
               item.Product.Category = categoriesRepository.GetById(item.Product.CategoryId);
               item.Product.Category.Parent = categoriesRepository.GetById(item.Product.Category.ParentId);
            }
            if (shoppingCart.PromotionCodeId > 0)
            {
               using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
               {
                  var promotionCodesRepository = efundraisingProdUnitOfWork.CreateRepository<IPromotionCodeRepository>();
                  shoppingCart.PromotionCode = promotionCodesRepository.GetById(shoppingCart.PromotionCodeId);
               }
            }
            return Ok(shoppingCart);
         }
      }

      /// <summary>
      /// Returns the first Shopping Cart found for the anonymous user that has a status of created
      /// </summary>
      /// <param name="anonymousId">Anonymous id</param>
      /// <returns>Shopping Cart</returns>
      [HttpGet]
      public IHttpActionResult GetByAnonymousId(string anonymousId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var shoppingCartRepository = efundStoreUnitOfWork.CreateRepository<IShoppingCartRepository>();
            var shoppingCarts = shoppingCartRepository.GetByAnonymousId(anonymousId);

            if (shoppingCarts.All(p => p.Status != ShoppingCartStatus.Created)) return Ok();

            var shoppingCart =
               shoppingCarts.OrderByDescending(p => p.Created).First(p => p.Status == ShoppingCartStatus.Created);
            foreach (var item in shoppingCart.Items)
            {
               item.Product = productRepository.GetById(item.ProductId);
               item.Product.Category = categoriesRepository.GetById(item.Product.CategoryId);
               item.Product.Category.Parent = categoriesRepository.GetById(item.Product.Category.ParentId);
            }
            if (shoppingCart.PromotionCodeId > 0)
            {
               using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
               {
                  var promotionCodesRepository = efundraisingProdUnitOfWork.CreateRepository<IPromotionCodeRepository>();
                  shoppingCart.PromotionCode = promotionCodesRepository.GetById(shoppingCart.PromotionCodeId);
               }
            }
            return Ok(shoppingCart);
         }
      }


      [HttpGet]
      public IHttpActionResult GetByClientId(int clientId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var shoppingCartRepository = efundStoreUnitOfWork.CreateRepository<IShoppingCartRepository>();
            var shoppingCart = shoppingCartRepository.GetByClientId(clientId);
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
               var clientRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
               var client = clientRepository.GetById(clientId);
               shoppingCart.Client = client;
            }
            foreach (var item in shoppingCart.Items)
            {
               item.Product = productRepository.GetById(item.ProductId);
               item.Product.Category = categoriesRepository.GetById(item.Product.CategoryId);
               item.Product.Category.Parent = categoriesRepository.GetById(item.Product.Category.ParentId);
            }
            if (shoppingCart.PromotionCodeId > 0)
            {
               using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
               {
                  var promotionCodesRepository = efundraisingProdUnitOfWork.CreateRepository<IPromotionCodeRepository>();
                  shoppingCart.PromotionCode = promotionCodesRepository.GetById(shoppingCart.PromotionCodeId);
               }
            }
            return Ok(shoppingCart);
         }
      }

      /// <summary>
      /// Creates a Shopping Cart and returns the Id
      /// </summary>
      /// <param name="model">Shopping Cart</param>
      /// <returns>Id of the created Shopping Cart</returns>
      [HttpPost]
      public IHttpActionResult Create(ShoppingCart model)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var shoppingCartRepository = efundStoreUnitOfWork.CreateRepository<IShoppingCartRepository>();
            var id = shoppingCartRepository.Save(model);            
            efundStoreUnitOfWork.Commit();
            return Get(id);
         }
      }

      [HttpDelete]
      public IHttpActionResult Delete(int id)
      {
         throw new NotImplementedException();
      }

      [HttpPut]
      public IHttpActionResult Update(ShoppingCart model)
      {
         mergeDuplicatedShoppingCartItems(model);
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var shoppingCartRepository = efundStoreUnitOfWork.CreateRepository<IShoppingCartRepository>();
            shoppingCartRepository.Update(model);
            efundStoreUnitOfWork.Commit();
            return Get(model.Id);
         }
      }
      /// <summary>
      /// Looks for duplicated items and merge them
      /// </summary>
      /// <param name="shoppingCart"></param>
      private void mergeDuplicatedShoppingCartItems(ShoppingCart shoppingCart)
      {
         var duplicatedProductIds = shoppingCart.Items.GroupBy(p => p.ProductId).Where(p => p.Count() > 1).Select(p => p.Key);
         foreach (var productId in duplicatedProductIds)
         {
            if (shoppingCart.Items.Any(p => p.ProductId == productId && p.Id == 0) && shoppingCart.Items.Any(p => p.ProductId == productId && p.Id != 0))
            {
               var itemToBeRemoved = shoppingCart.Items.First(p => p.ProductId == productId && p.Id == 0);
               var itemToBeUpdated = shoppingCart.Items.First(p => p.ProductId == productId && p.Id != 0);
               itemToBeUpdated.Quantity += itemToBeRemoved.Quantity;
               shoppingCart.Items.Remove(itemToBeRemoved);
            }
         }
      }
   }
}
