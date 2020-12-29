using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
   public class ShoppingCartRepository : IShoppingCartRepository
   {
      private readonly DataProvider _dataProvider;
      public ShoppingCartRepository(DataProvider dataProvider)
      {
         _dataProvider = dataProvider;
      }
      public ShoppingCart GetById(int id)
      {
         const string sql = "SELECT TOP 1 SC.id, SC.user_id, SC.anonymous_id, SC.status, SC.comments, SC.created, SC.client_id, SC.promotion_code_id FROM shopping_cart SC (NOLOCK) WHERE SC.id = @id; SELECT SCI.id, SCI.shopping_cart_id, SCI.product_id, SCI.quantity, SCI.comments, SCI.created FROM shopping_cart_item SCI (NOLOCK) WHERE SCI.shopping_cart_id = @id;";
         var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new {id},
            _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         var shoppingCart = multi.Read<shopping_cart>().First();
         var result = ShoppingCartMapper.Hydrate(shoppingCart);

         var shoppingCartItems = multi.Read<shopping_cart_item>().ToList();
         foreach (var item in shoppingCartItems.Select(ShoppingCartMapper.HydrateItem))
         {
            result.Items.Add(item);
         }
         return result;
      }

      public IList<ShoppingCart> GetAll()
      {
         throw new NotImplementedException();
      }

      public void Update(ShoppingCart shoppingCart)
      {

         const string sql = "UPDATE shopping_cart SET comments = @comments, status = @status, client_id = @clientId, promotion_code_id = @promotionCodeId WHERE id = @id;";
         const string sqlInsertItem = "INSERT shopping_cart_item(shopping_cart_id, product_id, quantity, comments, created) VALUES (@shoppingCartId, @productId, @quantity, @comments, GETDATE());";
         const string sqlDeleteItem = "DELETE shopping_cart_item WHERE id = @id;";
         _dataProvider.Database.Connection.Execute(sql, new { comments = shoppingCart.Comments, clientId = shoppingCart.ClientId, id = shoppingCart.Id, status = (int)shoppingCart.Status, promotionCodeId = shoppingCart.PromotionCodeId}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         
         var originalShoppingCart = GetById(shoppingCart.Id);
         foreach (var item in originalShoppingCart.Items)
         {
            _dataProvider.Database.Connection.Execute(sqlDeleteItem, new {id = item.Id}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         }
         foreach (var shoppingCartItem in shoppingCart.Items)
         {
            _dataProvider.Database.Connection.Execute(sqlInsertItem, new { shoppingCartId = shoppingCart.Id, comments = shoppingCartItem.Comments, quantity = shoppingCartItem.Quantity, productId = shoppingCartItem.ProductId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         }
      }

      public void Delete(ShoppingCart shoppingCart)
      {
         var toBeDeleted = _dataProvider.shopping_cart.Find(shoppingCart.Id);
         _dataProvider.shopping_cart.Remove(toBeDeleted);
         _dataProvider.SaveChanges();
      }

      public ShoppingCart GetByClientId(int clientId)
      {
         var id = _dataProvider.Database.Connection.Query<int>("SELECT TOP 1 SC.id FROM shopping_cart SC (NOLOCK) WHERE SC.client_id = @clientId", new { clientId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
         return GetById(id);
      }

      public int Save(ShoppingCart shoppingCart)
      {
         try
         {
            if (shoppingCart.Status == ShoppingCartStatus.InvalidStatus)
            {
               shoppingCart.Status = ShoppingCartStatus.Created;
            }
         

         var shoppingCartToBePersisted = ShoppingCartMapper.Dehydrate(shoppingCart);

         const string sql = "INSERT INTO shopping_cart (user_id, anonymous_id, status, comments, created, client_id, promotion_code_id) VALUES (@user_id, @anonymous_id, @status, @comments, GETDATE(), @client_id, @promotionCodeId); SELECT @@identity;";
         var id = _dataProvider.Database.Connection.ExecuteScalar<int>(sql, new { shoppingCartToBePersisted.user_id, shoppingCartToBePersisted.anonymous_id, shoppingCartToBePersisted.status, shoppingCartToBePersisted.comments, shoppingCartToBePersisted.client_id, promotionCodeId = shoppingCartToBePersisted.promotion_code_id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
         const string sqlItems = "INSERT INTO shopping_cart_item (shopping_cart_id, product_id, quantity, comments, created) VALUES (@shoppingCartId, @productId, @quantity, @comments, GETDATE());";
         foreach (var shoppingCartItem in shoppingCart.Items)
         {
            _dataProvider.Database.Connection.Execute(sqlItems, new { shoppingCartId = id, productId = shoppingCartItem.ProductId, quantity = shoppingCartItem.Quantity, comments = shoppingCartItem.Comments }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);   
         }
         return id;
         }
         catch (Exception exception)
         {
            throw new Exception(string.Format("An error happened when trying to insert a Shoppin Cart. UserId: {0}. AnonymousId: {1}. Status: {2}. Comments: {3}. ClientId: {4}. Promo Code Id: {5}", shoppingCart.UserId, shoppingCart.AnonymousId, shoppingCart.Status, shoppingCart.Comments, shoppingCart.ClientId, shoppingCart.PromotionCodeId), exception);
         }
      }

      public IList<ShoppingCart> GetByUserId(string userId)
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT SC.id FROM shopping_cart SC (NOLOCK) WHERE SC.user_id = @userId", new { userId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Select(GetById).ToList();
      }

      public IList<ShoppingCart> GetByAnonymousId(string anonymousId)
      {
         var ids = _dataProvider.Database.Connection.Query<int>("SELECT SC.id FROM shopping_cart SC (NOLOCK) WHERE SC.anonymous_id = @anonymousId", new { anonymousId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
         return ids.Select(GetById).ToList();
      }

        public ShoppingCart GetByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
