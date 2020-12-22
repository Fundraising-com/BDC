using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GA.BDC.Web.EzFundMVC;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace GA.BDC.Web.EzFundMVC
{
   public class Startup
   {
      public void Configuration(IAppBuilder app)
      {
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);
      }
   }
}
