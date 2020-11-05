using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(GA.BDC.WebApi.Reports.Startup))]

namespace GA.BDC.WebApi.Reports
{
   public class Startup
   {
      public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

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
         
         app.UseOAuthBearerAuthentication(OAuthBearerOptions);
      }
   }
}
