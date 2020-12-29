using System;

namespace GA.BDC.Shared.Entities
{
   /// <summary>
   /// Relation between the State and the Tax to be applied
   /// </summary>
   public class StateTax
   {
      /// <summary>
      /// State/Province Code
      /// </summary>
      public string StateCode { get; set; }
      /// <summary>
      /// Tax Code
      /// </summary>
      public string TaxCode { get; set; }
      /// <summary>
      /// Tax
      /// </summary>
      public Tax Tax { get; set; }
      /// <summary>
      /// Rate
      /// </summary>
      public double Rate { get; set; }
      /// <summary>
      /// Effective Date
      /// </summary>
      public DateTime EffectiveDate { get; set; }
   }
}
