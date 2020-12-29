using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Shared.Entities;
using System.Collections.Generic;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class ProductMapper
    {
        /// <param name="subProducts"></param>
        /// <param name="shippingFeeDetails"></param>
        public static Product Hydrate(product product, IList<item_lkup_tbl> subProducts, IList<shipping_fee_detail> shippingFeeDetails)
        {
            var result = new Product
            {
                Id = product.product_id,
                Name = product.name,
                Description = product.description,
                Url = product.url,
                Image = new Image
                {
                    Url = product.image_url,
                    AlternativeText = product.image_alternative_text
                },
                BannerImage = new Image
                {
                    Url = product.image_banner
                },
                Category = new Category
                {
                    Name = "TBD" //TODO: Discuss if it is needed
                },
                ExtraInformation = product.order_information != null ? product.order_information : "",
                FeaturedInformation = product.extra_information,
                METADescription = product.meta_description,
                METAKeywords = product.meta_keywords,
                METATitle = product.meta_title,
                CanBePurchased = product.is_store_purchasable,
                CountryCode = "US",
                CurrencyCode = "USD",
                MinimumQuantity = product.minimum_quantity != null ? (int)product.minimum_quantity : 1,
                SuggestedPrice = product.suggested_price!=null?(double)product.suggested_price:0,
                MinimumDivisor = product.minimum_divisor == null ? 0 : (int)product.minimum_divisor,
                IsStackedProduct = product.is_stacked_product,
            };
            if (subProducts != null)
            {
                foreach (var subProduct in subProducts)
                {
                    result.SubProducts.Add(
                        HydrateSubProduct(subProduct));



                        /*new SubProduct
                    {
                        ItemCode = subProduct.ITEM_CDE,
                        ProductCode = subProduct.PDCT_CDE,
                        Name = subProduct.ITEM_NME,
                        SizeName = subProduct.SIZE_TXT,
                        SapNumber = subProduct.SAPMaterialNo,
                        Price = (double)subProduct.ITEM_INVOIC_AMT,
                        SkuCode = subProduct.SKU_CDE,
                        ParentId = subProduct.PARENT_ID,
                        SelectedQuantity = 0,
                        ProductQuantity = subProduct.PDCT_ITEM_QTY == null ?1:(int)subProduct.PDCT_ITEM_QTY,
                        ProductSuggestedPrice = subProduct.PDCT_SUGG_PRICE==null? 1:(double)subProduct.PDCT_SUGG_PRICE
                    });*/
                }
            }

            if (shippingFeeDetails != null)
            {
                foreach (var shippingFeeDetail in shippingFeeDetails)
                {
                    result.ShippingFees.Add(new ShippingFee { MinimumQuantity = shippingFeeDetail.minimum_quantity, MaximumQuantity = shippingFeeDetail.maximum_quantity, Fee = shippingFeeDetail.fee });
                }
            }


            return result;
        }
        public static SubProduct HydrateSubProduct(item_lkup_tbl subProduct)
        {
            return new SubProduct
            {
                ItemCode = subProduct.ITEM_CDE,
                ProductCode = subProduct.PDCT_CDE,
                Name = subProduct.ITEM_NME,
                SizeName = subProduct.SIZE_TXT,
                SapNumber = subProduct.SAPMaterialNo,
                Price = (double)subProduct.ITEM_INVOIC_AMT,
                SkuCode = subProduct.SKU_CDE,
                ItemSeqNbr = subProduct.ITEM_SEQ_NBR,
                ParentId = subProduct.PARENT_ID,
                SelectedQuantity = 0,
                ProductQuantity = subProduct.PDCT_ITEM_QTY == null ? 1 : (int)subProduct.PDCT_ITEM_QTY,
                ProductSuggestedPrice = subProduct.PDCT_SUGG_PRICE == null ? 1 : (double)subProduct.PDCT_SUGG_PRICE
            };
        }

    }
}