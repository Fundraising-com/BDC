namespace GA.BDC.Shared.Entities
{
    public class FundraiserProduct
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Is Purchasable
        /// </summary>
        public bool IsPurchasable { get; set; }
        /// <summary>
        /// Has Sell Sheet
        /// </summary>
        public bool HasSellSheet { get; set; }
        /// <summary>
        /// Has Nutrition Information Sheet
        /// </summary>
        public bool HasNutritionInformationSheet { get; set; }
        /// <summary>
        /// Store Url
        /// </summary>
        public string StoreUrl { get; set; }
        /// <summary>
        /// Sell Sheet Path
        /// </summary>
        public string SellSheetPath { get; set; }
        /// <summary>
        /// Nutrition Information Sheet Path
        /// </summary>
        public string NutritionInformationSheetPath { get; set; }
        /// <summary>
        /// Fundraiser Category Id
        /// </summary>
        public int FundraiserCategoryId { get; set; }
    }
}
