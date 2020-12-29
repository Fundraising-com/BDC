using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper;
using Dapper.Contrib.Extensions;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DataProvider _dataProvider;

        public ShoppingCartRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Delete(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public IList<ShoppingCart> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<ShoppingCart> GetByAnonymousId(string anonymousId)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetById(int id)
        {
            const string sql = "SELECT TOP 1 * FROM shopping_cart SC (NOLOCK) WHERE SC.id = @id;"
                + " SELECT * FROM shopping_cart_item SCI (NOLOCK) WHERE SCI.shopping_cart_id = @id;";
            var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new { id },
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

        public ShoppingCart GetByOrderId(int orderId)
        {
            const string sql = "SELECT TOP 1 * FROM shopping_cart SC (NOLOCK) WHERE SC.order_id = @orderId;";
            var cartRow = _dataProvider.Database.Connection.QueryFirstOrDefault<shopping_cart>(sql, new { orderId },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            if (cartRow == null) return null;
            var shoppingCart = ShoppingCartMapper.Hydrate(cartRow);

            const string sqlItems = " SELECT * FROM shopping_cart_item SCI (NOLOCK) WHERE SCI.shopping_cart_id = @id;";
            var shoppingCartItems = _dataProvider.Database.Connection.Query<shopping_cart_item>(sqlItems, new { id = shoppingCart.Id },
               _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            foreach (var item in shoppingCartItems.Select(ShoppingCartMapper.HydrateItem))
            {
                shoppingCart.Items.Add(item);
            }
            return shoppingCart;
        }

        public IList<ShoppingCart> GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public int Save(ShoppingCart shoppingCart)
        {
            var cart = ShoppingCartMapper.Dehydrate(shoppingCart);
            var cartId = (int)_dataProvider.Database.Connection.Insert(cart, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);

            foreach (var shoppingCartItem in shoppingCart.Items)
            {
                var parentItem = ShoppingCartMapper.DehydrateParentItem(shoppingCartItem, cartId);
                var parentId = (int)_dataProvider.Database.Connection.Insert(parentItem, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                foreach(var subProduct in shoppingCartItem.Product.SubProducts)
                {
                    if (subProduct.SelectedQuantity > 0) {
                        var subItem = ShoppingCartMapper.DehydrateItem(subProduct,parentId,cartId);
                        _dataProvider.Database.Connection.Insert(subItem, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
                    }
                }
            }
            return (int)cartId;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
