namespace GA.BDC.Shared.Entities
{
   public class SaleItem
   {
      /// <summary>
      /// Sale Id
      /// </summary>
      public int SaleId { get; set; }
      /// <summary>
      /// Quantity
      /// </summary>
      public int Quantity { get; set; }
      /// <summary>
      /// Number
      /// </summary>
      public int Number { get; set; }
      /// <summary>
      /// Scratchbook Id
      /// </summary>
      public int ScratchBookId { get; set; }
      /// <summary>
      /// Product
      /// </summary>
      public Product Product { get; set; }
      /// <summary>
      /// Unit Price Sold
      /// </summary>
      public double UnitPrice { get; set; }
      /// <summary>
      /// Total Pricr item Sold
      /// </summary>
      public double SaleAmount { get; set; }
   }
}
