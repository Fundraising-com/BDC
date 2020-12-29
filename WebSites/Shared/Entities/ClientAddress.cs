namespace GA.BDC.Shared.Entities
{
   public class ClientAddress : Address
   {
      /// <summary>
      /// Id
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// Client Id
      /// </summary>
      public int ClientId { get; set; }
      /// <summary>
      /// Client Sequence Code
      /// </summary>
      public string ClientSequenceCode { get; set; }
      /// <summary>
      /// Type
      /// </summary>
      public string Type { get; set; }
      /// <summary>
      /// Attention Of
      /// </summary>
      public string AttentionOf { get; set; }
      /// <summary>
      /// Address Zone Id
      /// </summary>
      public int AddressZoneId { get; set; }
   }
}
