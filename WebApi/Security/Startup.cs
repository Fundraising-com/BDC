using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using GA.BDC.WebApi.Security.Providers;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;

[assembly: OwinStartup(typeof(GA.BDC.WebApi.Security.Startup))]

namespace GA.BDC.WebApi.Security
{
    public class Startup
    {
      public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
      public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }
      public static FacebookAuthenticationOptions FacebookAuthOptions { get; private set; }

      public void Configuration(IAppBuilder app)
      {
         var config = new HttpConfiguration();

         ConfigureOAuth(app);

         WebApiConfig.Register(config);
         app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
         app.UseWebApi(config);
      }

      public void ConfigureOAuth(IAppBuilder app)
      {
         //use a cookie to temporarily store information about a user logging in with a third party login provider
         app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
         OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

         var oAuthServerOptions = new OAuthAuthorizationServerOptions()
         {
            AllowInsecureHttp = true,
            TokenEndpointPath = new PathString("/token"),
            Provider = new ApplicationOAuthProvider(),
            AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(0), //This parameter is going to be overwritten in ApplicationOAuthProvider's ValidateClientAuthentication
            RefreshTokenProvider = new ApplicationRefreshTokenProvider()
         };

         // Token Generation
         app.UseOAuthAuthorizationServer(oAuthServerOptions);
         app.UseOAuthBearerAuthentication(OAuthBearerOptions);

         //Configure Google External Login
         GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions()
         {
            ClientId = "xxxxxx",
            ClientSecret = "xxxxxx",
            Provider = new GoogleOAuth2AuthenticationProvider()
         };
         app.UseGoogleAuthentication(GoogleAuthOptions);

         //Configure Facebook External Login
         FacebookAuthOptions = new FacebookAuthenticationOptions()
         {
            AppId = "xxxxxx",
            AppSecret = "xxxxxx",
            Provider = new FacebookAuthenticationProvider()
         };
         app.UseFacebookAuthentication(FacebookAuthOptions);
      }
   }
}
