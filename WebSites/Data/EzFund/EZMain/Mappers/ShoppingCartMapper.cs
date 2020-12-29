using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class ShoppingCartMapper
    {
        public static shopping_cart Dehydrate(ShoppingCart cart)
        {
            return new shopping_cart
            {
                user_id = cart.UserId,
                status = (int)cart.Status,
                comments = cart.Comments,
                created = DateTime.Now,
                order_id =  cart.OrderId,
                promotion_code_id = cart.PromotionCodeId
            };

        }
        public static shopping_cart_item DehydrateParentItem(ShoppingCartItem item, int cartId)
        {
            return new shopping_cart_item
            {
                parent_id = null,
                shopping_cart_id = cartId,
                product_id = item.ProductId,
                item_code = null,
                quantity = item.Quantity,
                comments = item.Comments,
                created = DateTime.Now
            };
        }
        public static shopping_cart_item DehydrateItem(SubProduct item, int parentId, int cartId)
        {
            return new shopping_cart_item
            {
                parent_id = parentId,
                shopping_cart_id = cartId,
                product_id = null,
                item_code = item.ItemCode,
                quantity =  item.SelectedQuantity,
                comments = null,
                created = DateTime.Now
            };
        }

        public static ShoppingCart Hydrate(shopping_cart cart)
        {
            return new ShoppingCart
            {
                Id =  cart.id,
                UserId = cart.user_id,
                Status = (ShoppingCartStatus)cart.status,
                Comments = cart.comments,
                Created = cart.created,
                OrderId = cart.order_id??0,
                PromotionCodeId = cart.promotion_code_id??0,
                Items = new List<ShoppingCartItem>()
            };
        }

        public static ShoppingCartItem HydrateItem(shopping_cart_item item)
        {
            return new ShoppingCartItem
            {
                Id = item.id,
                ParentId = item.parent_id??0,
                ShoppingCartId = item.shopping_cart_id,
                ProductId = item.product_id??0,
                ItemCode = item.item_code,
                Quantity = item.quantity,
                Comments = item.comments,
                Created =  item.created
            };
        }

    }
}
