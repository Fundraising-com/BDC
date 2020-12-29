namespace GA.BDC.Shared.Entities
{
   public enum PromotionCodePartnerScopeType
   {
      /// <summary>
      /// Unknown
      /// </summary>
      Unknown = 0,
      /// <summary>
      /// Promotion Code applies to all Partners
      /// </summary>
      All = 1,
      /// <summary>
      /// Promotion Code applies to the Partner specified at PartnerId
      /// </summary>
      SinglePartner = 2
   }
}
