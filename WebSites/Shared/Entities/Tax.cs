namespace GA.BDC.Shared.Entities
{
   /// <summary>
   /// Tax Catalog
   /// </summary>
   public class Tax
   {
      /// <summary>
      /// Code
      /// </summary>
      public string Code { get; set; }
      /// <summary>
      /// Description in English
      /// </summary>
      public string DescriptionEnglish { get; set; }
      /// <summary>
      /// Description in French
      /// </summary>
      public string DescriptionFrench { get; set; }
      /// <summary>
      /// Bank Account
      /// </summary>
      public string Account { get; set; }
   }
}
