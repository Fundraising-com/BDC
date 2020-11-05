using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using GA.BDC.WebApi.Reports.Filters;
using GA.BDC.WebApi.Reports.Helpers.Formatters;

namespace GA.BDC.WebApi.Reports
{
   public static class WebApiConfig
   {
      public static void Register(HttpConfiguration config)
      {
         // Web API routes
         config.MapHttpAttributeRoutes();
         var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
         config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
         config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
         config.Formatters.Add(new DataTableCsvFormatter());
         config.EnableCors();
         config.Filters.Add(new CustomHttpsAttribute());
         config.Filters.Add(new ExceptionHandlerAttribute());
         config.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
         );
      }
   }
}
