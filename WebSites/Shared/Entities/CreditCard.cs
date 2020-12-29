namespace GA.BDC.Shared.Entities
{
   public class CreditCard
   {
      /// <summary>
      /// Number
      /// </summary>
      public string Number { get; set; }
      /// <summary>
      /// Expiration Date
      /// </summary>
      public string ExpirationDate { get; set; }
      /// <summary>
      /// CVV
      /// </summary>
      public string CVV { get; set; }
      /// <summary>
      /// Name's Holder
      /// </summary>
      public string Holder { get; set; }
      /// <summary>
      /// Internal Payment Method
      /// </summary>
      public InternalPaymentMethod InternalPaymentMethod { get; set; }
      /// <summary>
      /// Address
      /// </summary>
      public Address Address { get; set; }
      /// <summary>
      /// Amount
      /// </summary>
      public double Amount { get; set; }
      /// <summary>
      /// External Reference Number
      /// </summary>
      public string Reference { get; set; }
   }
}
