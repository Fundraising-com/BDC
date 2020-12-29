namespace GA.BDC.Shared.Entities
{
   public class OAuthClient
   {
      public string Id { get; set; }
      public string Secret { get; set; }
      public string Name { get; set; }
      public OAuthClientApplicationType ApplicationType { get; set; }
      public bool Active { get; set; }
      public int RefreshTokenLifeTime { get; set; }
      public string AllowedOrigin { get; set; }
      public bool AllowsTokenRefresh { get; set; }
   }

   public enum OAuthClientApplicationType
   {
      Unknown = 0,
      Javascript = 1,
      NativeConfidential = 2
   }
}
