using GA.BDC.Data.Fundraising.EFRCommon.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFRCommon.Mappers
{
   public static class OAuthClientMapper
   {
      public static OAuthClient Hydrate(oauth_client row)
      {
         return new OAuthClient
         {
            Id = row.id,
            Secret = row.secret,
            Name = row.name,
            Active = row.is_active,
            AllowedOrigin = row.allowed_origin,
            ApplicationType = (OAuthClientApplicationType)row.application_type,
            RefreshTokenLifeTime = row.refresh_token_life_time
         };
      }

      public static oauth_client Dehydrate(OAuthClient model)
      {
         var row = new oauth_client
         {
            id = model.Id,
            is_active = model.Active,
            refresh_token_life_time = model.RefreshTokenLifeTime,
            allowed_origin = model.AllowedOrigin,
            secret = model.Secret,
            application_type = (int)model.ApplicationType,
            name = model.Name
         };
         return row;
      }
   }
}
