namespace GA.BDC.Shared.Entities
{
   public enum PromotionCodeMinimumRequirementType
   {
      /// <summary>
      /// Unknown
      /// </summary>
      Unknown = 0,
      /// <summary>
      /// No minimum requirements apply
      /// </summary>
      None = 1,
      /// <summary>
      /// A minimum quantity requirement applies
      /// </summary>
      Quantity = 2,
      /// <summary>
      /// A minimum amount requirement applies
      /// </summary>
      Amount = 3,
   }
}
