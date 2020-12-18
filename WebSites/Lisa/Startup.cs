using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartupAttribute(typeof(GA.BDC.Web.Lisa.Startup))]
namespace GA.BDC.Web.Lisa
{
   public partial class Startup
    {
      public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
      public void Configuration(IAppBuilder app)
        {

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
            AreaRegistration.RegisterAllAreas();
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);
         
      }
    }
}
