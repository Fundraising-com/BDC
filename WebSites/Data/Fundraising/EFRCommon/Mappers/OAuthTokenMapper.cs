using GA.BDC.Data.Fundraising.EFRCommon.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFRCommon.Mappers
{
   public static class OAuthTokenMapper
   {
      public static OAuthToken Hydrate(oauth_token row)
      {
         return new OAuthToken
         {
            Id = row.id,
            ProtectedTicket = row.protected_ticket,
            ClientId = row.client_id,
            Subject = row.subject,
            ExpiresUtc = row.expires_utc,
            IssuedUtc = row.issued_utc            
         };
      }

      public static oauth_token Dehydrate(OAuthToken model)
      {
         var token = new oauth_token
         {
            id = model.Id,
            issued_utc = model.IssuedUtc,
            subject = model.Subject,
            protected_ticket = model.ProtectedTicket,
            client_id = model.ClientId,
            expires_utc = model.ExpiresUtc
         };
         return token;
      }
   }
}
