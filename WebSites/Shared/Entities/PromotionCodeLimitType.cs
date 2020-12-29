namespace GA.BDC.Shared.Entities
{
   public enum PromotionCodeLimitType
   {
      Unknown = 0,
      /// <summary>
      /// Promotion Code is always valid until it gets disabled or deleted
      /// </summary>
      Unlimited =1,
      /// <summary>
      /// Promotion Code expires after the Date Limit
      /// </summary>
      Date = 2,
      /// <summary>
      /// Promotion Code expires after the amount of Codes surpasses the Quantity Limit
      /// </summary>
      Quantity =3
   }
}
