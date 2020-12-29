using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
   public static class ProductMapper
   {

      public static Product Hydrate(product product, product_desc productDesc, int parentPackageId, product_price_info productPriceInfo, IList<product_profit> productProfits, currency currency)
      {
         var result = new Product
         {
            Id = product.product_id,
            Name = product.name,
            Url = productDesc?.url,
            Description = productDesc?.description,
            Price = productPriceInfo != null && productPriceInfo.unit_price != null ? (double)productPriceInfo.unit_price : 0,
            RetailPrice = productDesc?.retail_price ?? 0,
            CategoryId = parentPackageId,
            CanBePurchased = productDesc?.is_store_purchasable ?? true,
            Flavors = productDesc?.flavors,
            Packaging = productDesc?.packaging,
            RaisingPotential = product.raising_potential == null ? 0 : (double)product.raising_potential,
            MinimumQuantity = productDesc?.minimum_quantity ?? 0,
            ExtraInformation = productDesc?.extra_information,
            ScratchBookId = product.scratch_book_id,
            Code = product.product_code,
            METADescription = productDesc?.meta_description,
            METAKeywords = productDesc?.meta_keywords,
            METATitle = productDesc?.meta_title ?? string.Empty,
            CanonicalUrl = productDesc?.canonical_url ?? string.Empty,
            DisplayOrder = productDesc?.display_order ?? 0,
            DisplayOrderFeatured = productDesc?.display_order_featured ?? 0,
            CountryCode = productPriceInfo != null ? productPriceInfo.country_code : "US",
            ProfitPercentage = (product.raising_potential == null || productPriceInfo == null || product.raising_potential == 0 || productPriceInfo.unit_price == 0) ? 0 : (double)((product.raising_potential - productPriceInfo.unit_price) / product.raising_potential),
            CurrencyCode = currency.currency_code
         };
         if (productProfits != null)
         {
            foreach (var productProfit in productProfits)
            {
               result.Profits.Add(new Profit { Price = productProfit.price, Min = productProfit.min_qty, Max = productProfit.max_qty });
            }
         }

         return result;
      }

      public static IList<Image> HydrateImages(IList<product_image> images)
      {
         if (!images.Any())
         {
            images.Add(new product_image { is_cover = true, url = "http://placehold.it/150x150" });
         }
         var result = new List<Image>();
         foreach (var image in images)
         {
            result.Add(new Image { Id = image.id, Url = image.url, Order = image.sort, IsCover = image.is_cover, AlternativeText = image.alternative_text });
         }
         return result;
      }
   }
}
