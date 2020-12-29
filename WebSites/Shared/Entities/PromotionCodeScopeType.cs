namespace GA.BDC.Shared.Entities
{
   public enum PromotionCodeScopeType
   {
      /// <summary>
      /// Unknown
      /// </summary>
      Unknown = 0,
      /// <summary>
      /// Promotion Code applies to all the Shopping Cart
      /// </summary>
      ShoppingCart = 1,
      /// <summary>
      /// Promotion Code applies ONLY to the Product specified in the Scratchbook Id
      /// </summary>
      Products = 2
   }
}
