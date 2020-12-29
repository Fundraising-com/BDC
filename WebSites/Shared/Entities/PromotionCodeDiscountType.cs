namespace GA.BDC.Shared.Entities
{
   public enum PromotionCodeDiscountType
   {
      /// <summary>
      /// Unknown
      /// </summary>
      Unknown = 0,
      /// <summary>
      /// Promotion Code applies as an Amount
      /// </summary>
      Amount = 1,
      /// <summary>
      /// Promotion Code applies as a Percentage
      /// </summary>
      Percentage = 2,
      /// <summary>
      /// Promotion Codes removes any shipping fee
      /// </summary>
      FreeShipping = 3
   }
}
