using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Routing;

namespace GA.BDC.Web.MGP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
            // Touch Emails Open Tracking

            routes.MapRoute("Touch Emails", "pixel",
                            new { controller = "Home", action = "TouchEmailOpenTracking" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*allpng}", new { allpng = @".*\.png(/.*)?" });
            routes.IgnoreRoute("{*alljpg}", new { alljpg = @".*\.jpg(/.*)?" });
            routes.IgnoreRoute("{*allgif}", new { allgif = @".*\.gif(/.*)?" });
            routes.IgnoreRoute("{*alltxt}", new { alltxt = @".*\.txt(/.*)?" });
            routes.IgnoreRoute("{*allico}", new { allico = @".*\.ico(/.*)?" });
            routes.IgnoreRoute("{*alljs}", new { alljs = @".*\.js(/.*)?" });
            routes.IgnoreRoute("{*allxml}", new { allxml = @"^(sitemap.xml)&&.*\.xml(/.*)?" });
            routes.IgnoreRoute("{*allcss}", new { allcss = @".*\.css(/.*)?" });
            routes.IgnoreRoute("{*allhtm}", new { allhtm = @".*\.htm(/.*)?" });
            routes.IgnoreRoute("{*allscanImageUrl}", new { allscanImageUrl = @"(.*/)?scanImageUrl(/.*)?" });
            routes.IgnoreRoute("dumpTemp/{*catchall}");
            routes.IgnoreRoute("content/{*catchall}");
            routes.IgnoreRoute("scripts/{*catchall}");

            

            routes.MapRoute("GAO Redeem","efunds" ,new { controller = "Home", action = "RedirectExternal" });
           //Corporate Initiative pages. //TODO: Javier Arellano. This will be removed to a dynamic Group Page when the contract is signed.
            routes.MapRoute("United Airlines", "c/unitedairlines",
                             new { controller = "Home", action = "UnitedAirlines" });
            routes.MapRoute("MB Financial", "c/mbfinancial",
                              new { controller = "Home", action = "MBFinancial" });
            routes.MapRoute("Scholarship Fund Approach", "c/ScholarshipFundApproach",
                               new { controller = "Home", action = "ScholarshipFundApproach" });
            routes.MapRoute("Charity Fund Approach", "c/CharityFundApproach",
                               new { controller = "Home", action = "CharityFundApproach" });
            routes.MapRoute("InVest USA", "c/investusa",
                             new { controller = "Home", action = "InVestUsa" });
            //Restaurant Cards redirection
            routes.MapRoute("Restaurant", "Restaurant",
                            new { controller = "Home", action = "Restaurant" });

            routes.MapRoute("Redirect route", "{file}.aspx",
                            new { controller = "Home", action = "Redirect" });
            routes.MapRoute("Redirect route 2", "{path}/{file}.aspx",
                            new { controller = "Home", action = "Redirect" });
            routes.MapRoute("Redirect route 3", "{path}/{path2}/{file}.aspx",
                            new { controller = "Home", action = "Redirect" });
            routes.MapRoute("Redirect route 4", "{path}/{path2}/{path3}/{file}.aspx",
                            new { controller = "Home", action = "Redirect" });
            routes.MapRoute("Redirect route 5", "{path}/{path2}/{path3}/{path4}/{file}.aspx",
                            new { controller = "Home", action = "Redirect" });

         routes.MapRoute(
                name: "Partners Landing Page",
                url: "p/{partner_id}",
                defaults: new { controller = "Home", action = "Groups" }
                 
            );
         routes.MapRoute(
                name: "Participants",
                url: "{event}/{participant}",
                defaults: new { controller = "Group", action = "Participant" },
                constraints: new { group = new Helpers.Routes.Constraints.ParticipantConstraint() }
            );

            routes.MapRoute(
                name: "Events",
                url: "{event}",
                defaults: new { controller = "Group", action = "Index" },
                constraints: new { group = new Helpers.Routes.Constraints.GroupConstraint() }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}