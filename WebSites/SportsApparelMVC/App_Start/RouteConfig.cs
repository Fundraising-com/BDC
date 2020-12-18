using System.Web.Mvc;
using System.Web.Routing;

namespace GA.BDC.Web.SportsApparel.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("fonts/{*pathInfo}");
            routes.IgnoreRoute("{*allpng}", new { allpng = @".*\.png(/.*)?" });
            routes.IgnoreRoute("{*alljpg}", new { alljpg = @".*\.jpg(/.*)?" });
            routes.IgnoreRoute("{*allgif}", new { allgif = @".*\.gif(/.*)?" });
            routes.IgnoreRoute("{*alltxt}", new { alltxt = @".*\.txt(/.*)?" });
            routes.IgnoreRoute("{*allico}", new { allico = @".*\.ico(/.*)?" });
            routes.IgnoreRoute("{*alljs}", new { alljs = @".*\.js(/.*)?" });
            routes.IgnoreRoute("{*allxml}", new { allxml = @"^(sitemap.xml)&&.*\.xml(/.*)?" });
            routes.IgnoreRoute("{*allcss}", new { allcss = @".*\.css(/.*)?" });
            routes.IgnoreRoute("{*allhtm}", new { allhtm = @".*\.htm(/.*)?" });
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
             name: "Categories",
             url: "products/{*query}",
             defaults: new { controller = "Products", action = "Redirect", query = "", country = 2 }
         );
            routes.MapRoute(
             name: "Default Route",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
             constraints: new { controller = "Home|Products|HowItWorks|Apparel|Catalog|Administration" }
         );
            //Redirection, it catches old calls from the ASP.Net site and redirect them to the new MVC one
            routes.MapRoute(
                name: "Redirect route",
                url: "{*.}",
                defaults: new { controller = "Home", action = "Redirect" });

        }
    }
}
