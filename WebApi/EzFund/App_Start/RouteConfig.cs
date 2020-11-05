﻿using System.Web.Mvc;
using System.Web.Routing;

namespace GA.BDC.WebApi.EzFund.App_Start
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

            //Redirection, it catches old calls from the ASP.Net site and redirect them to the new MVC one
            routes.MapRoute(
                name: "Redirect route",
                url: "{*.}",
                defaults: new { controller = "Home", action = "Redirect" });


            //routes.MapRoute(
            //       name: "Blog",
            //       url: "blog/{*query}",
            //       defaults: new { controller = "Blog", action = "Redirect", query = "" }
            //   );
            //routes.MapRoute(
            //    name: "Newsletters",
            //    url: "newsletters/{*query}",
            //    defaults: new { controller = "Newsletters", action = "Newsletters", query = "" }
            //);
            //routes.MapRoute(
            //    name: "Categories",
            //    url: "products/{*query}",
            //    defaults: new { controller = "Products", action = "Redirect", query = "", country = 2 }
            //);
            //routes.MapRoute(
            //    name: "Canadian Categories",
            //    url: "canada/{*query}",
            //    defaults: new { controller = "Products", action = "Redirect", query = "", country = 1 }
            //);
            //routes.MapRoute(
            //    name: "Default Route",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    constraints: new { controller = "Home|Products|Canada|AboutUs|Ideas|OnlineFundraising|Partners|Resources|Representatives|ShoppingCarts|Blog|Administration" }
            //);
            
        }
    }
}
