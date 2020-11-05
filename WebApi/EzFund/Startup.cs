using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(GA.BDC.WebApi.EzFund.Startup))]
namespace GA.BDC.WebApi.EzFund
{
   public class Startup
   {
      public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
      public void Configuration(IAppBuilder app)
        {
         var config = new HttpConfiguration();

         OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
         app.UseOAuthBearerAuthentication(OAuthBearerOptions);
         config.EnableCors();
         WebApiConfig.Register(config);
         
         app.UseWebApi(config);
      }
   }
}
