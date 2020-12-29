using System.Collections.Generic;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class ShoppingCartMapper
    {
        public static ShoppingCart Hydrate(shopping_cart shoppingCart)
        {
             var result = new ShoppingCart
            {
                Id = shoppingCart.id,
                Status = (ShoppingCartStatus)shoppingCart.status,
                UserId = shoppingCart.user_id,
                AnonymousId = shoppingCart.anonymous_id,
                Created = shoppingCart.created,
                Items = new List<ShoppingCartItem>(),
                ClientId = shoppingCart.client_id ?? 0,
                PromotionCodeId = shoppingCart.promotion_code_id ?? 0
            };
            return result;
        }

        public static ShoppingCartItem HydrateItem(shopping_cart_item shoppingCartItem)
        {
            var result = new ShoppingCartItem
            {
                Id = shoppingCartItem.id,
                Quantity = shoppingCartItem.quantity,
                Comments = shoppingCartItem.comments,
                ProductId = shoppingCartItem.product_id,
                ShoppingCartId = shoppingCartItem.shopping_cart_id,
                Created = shoppingCartItem.created
            };
            return result;
        }

        public static shopping_cart Dehydrate(ShoppingCart shoppingCart)
        {
            var result = new shopping_cart
            {
                id = shoppingCart.Id,
                comments = shoppingCart.Comments,
                status = (int)shoppingCart.Status,
                user_id = shoppingCart.UserId,
                created = shoppingCart.Created,
                anonymous_id = shoppingCart.AnonymousId,
                client_id = shoppingCart.ClientId,
                promotion_code_id = shoppingCart.PromotionCodeId
            };
            return result;
        }

        public static shopping_cart_item DehydrateItem(ShoppingCartItem shoppingCartItem)
        {
            var result = new shopping_cart_item
            {
                id = shoppingCartItem.Id,
                comments = shoppingCartItem.Comments,
                created = shoppingCartItem.Created,
                product_id = shoppingCartItem.ProductId,
                quantity = shoppingCartItem.Quantity,
                shopping_cart_id = shoppingCartItem.ShoppingCartId
            };
            return result;
        }
    }
}
