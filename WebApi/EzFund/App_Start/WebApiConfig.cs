using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using GA.BDC.WebApi.EzFund.Filters;
using System.Web.Http.Cors;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server.Owin;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using Microsoft.Owin.Security.OAuth;

namespace GA.BDC.WebApi.EzFund
{
   public static class WebApiConfig
   {
      public static void Register(HttpConfiguration config)
      {
         // Configure Web API to use only bearer token authentication.
         config.SuppressDefaultHostAuthentication();
         config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

         // Web API routes
         config.MapHttpAttributeRoutes();
         //config.EnableCors(new EnableCorsAttribute("*","*","*"));
         var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
         config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
         config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
         config.Filters.Add(new CustomHttpsAttribute());
         config.Filters.Add(new ModelStateValidationAttribute());
         config.Filters.Add(new ExceptionHandlerAttribute());

         config.MessageHandlers.Insert(0, new OwinServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
         config.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
         );
      }
   }
}
