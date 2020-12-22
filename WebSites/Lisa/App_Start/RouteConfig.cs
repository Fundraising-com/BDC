using System.Web.Mvc;
using System.Web.Routing;

namespace GA.BDC.Web.Lisa
{
   public class RouteConfig
   {
      public static void RegisterRoutes(RouteCollection routes)
      {
         routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
         routes.MapRoute(
             name: "Login",
             url: "login",
             defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
             namespaces: new[] { "GA.BDC.Web.Lisa.Controllers" }
         );
         routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "GA.BDC.Web.Lisa.Controllers" }
         );
      }
   }
}
