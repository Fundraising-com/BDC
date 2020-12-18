using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GA.BDC.Web.Fundraising.MVC;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace GA.BDC.Web.Fundraising.MVC
{
   public class Startup
   {
      public void Configuration(IAppBuilder app)
      {
         System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
         AreaRegistration.RegisterAllAreas();
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      }
   }
}
