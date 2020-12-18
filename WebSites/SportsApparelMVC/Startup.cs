using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GA.BDC.Web.SportsApparel.MVC;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace GA.BDC.Web.SportsApparel.MVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}