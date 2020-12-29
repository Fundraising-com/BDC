using System;

namespace GA.BDC.Shared.Entities
{
   public class OAuthToken
   {
      public string Id { get; set; }
      public string ClientId { get; set; }
      public string Subject { get; set; }
      public DateTime IssuedUtc { get; set; }
      public DateTime ExpiresUtc { get; set; }
      public string ProtectedTicket { get; set; }
   }
}
