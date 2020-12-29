
namespace GA.BDC.Shared.Entities
{
   /// <summary>
   /// Tax applied to the Sale
   /// </summary>
   public class AppliedTax
   {
      /// <summary>
      /// Sale Id
      /// </summary>
      public int SaleId { get; set; }
      /// <summary>
      /// Tax Code
      /// </summary>
      public string TaxCode { get; set; }
      /// <summary>
      /// Amount
      /// </summary>
      public double Amount { get; set; }
   }
}
