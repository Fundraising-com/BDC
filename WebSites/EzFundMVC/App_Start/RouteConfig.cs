using System.Web.Mvc;
using System.Web.Routing;

namespace GA.BDC.Web.EzFundMVC
{
    public class RouteConfig
    {
			  public static void RegisterRoutes(RouteCollection routes)
			  {
					routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

					//routes.MapRoute(
					//    name: "Blog",
					//    url: "blog/{*query}",
					//    defaults: new { controller = "Blog", action = "Redirect", query = "" }
					//);
					routes.MapRoute(
						 name: "Products",
						 url: "products/{*query}",
						 defaults: new { controller = "Products", action = "CleanUrlRedirect", query = "" }
					);
					routes.MapMvcAttributeRoutes();
					routes.MapRoute(
						 name: "Default",
						 url: "{controller}/{action}/{id}",
						 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
						 constraints: new { controller = "Home|Products|Ideas|Resources|ShoppingCarts|Blog" }
					);

					//Redirection, it catches old calls from the ASP.Net site and redirect them to the new MVC one
					routes.MapRoute(
					 name: "Redirect route",
					 url: "{*.}",
					 defaults: new { controller = "Home", action = "Redirect" });
			}
    }
}
