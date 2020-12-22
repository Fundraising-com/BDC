using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace GA.BDC.Web.Custcare
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "EventApi",
                routeTemplate: "api/event/{param}",
                defaults: new { controller = "event" }
            );

            config.Routes.MapHttpRoute(
                name: "SponsorApi",
                routeTemplate: "api/sponsor/{param}",
                defaults: new { controller = "sponsor" }
            );

            config.Routes.MapHttpRoute(
                name: "AccountApi",
                routeTemplate: "api/account/{param}",
                defaults: new { controller = "account" }
            );

            config.Routes.MapHttpRoute(
                name: "MembersApi",
                routeTemplate: "api/members/{param}",
                defaults: new { controller = "members" }
            );

            config.Routes.MapHttpRoute(
                name: "OrdersApi",
                routeTemplate: "api/orders/{param}",
                defaults: new { controller = "orders" }
            );

            config.Routes.MapHttpRoute(
                name: "LinksApi",
                routeTemplate: "api/links/{param}",
                defaults: new { controller = "links" }
            );

            config.Routes.MapHttpRoute(
                name: "ToolsApi",
                routeTemplate: "api/tools/{param}",
                defaults: new { controller = "tools" }
            );            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}