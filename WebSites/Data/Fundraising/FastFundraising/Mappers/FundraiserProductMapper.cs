using GA.BDC.Data.Fundraising.FastFundraising.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.FastFundraising.Mappers
{
    public static class FundraiserProductMapper
    {
        public static FundraiserProduct Hydrate(fundraiser_product fp)
        {
            var result = new FundraiserProduct
            {
                Id = fp.id,
                Name = fp.name,
                Image = fp.image,
                HasNutritionInformationSheet = fp.has_nutrition_information_sheet,
                HasSellSheet = fp.has_sell_sheet,
                IsPurchasable = fp.is_purchasable,
                NutritionInformationSheetPath = fp.nutrition_information_sheet_path,
                SellSheetPath = fp.sell_sheet_path,
                StoreUrl = fp.store_url,
                FundraiserCategoryId = fp.fundraiser_category_id
            };
            return result;
        }
    }
}
