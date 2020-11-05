using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using GA.BDC.WebApi.Security.Filters;
using Newtonsoft.Json.Serialization;

namespace GA.BDC.WebApi.Security
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
         // Web API routes
         config.MapHttpAttributeRoutes();
         config.EnableCors();
         config.Filters.Add(new CustomHttpsAttribute());
         config.Filters.Add(new ExceptionHandlerAttribute());
         config.Formatters.Remove(config.Formatters.XmlFormatter);
         config.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
         );           
         var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
         jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      }
    }
}
