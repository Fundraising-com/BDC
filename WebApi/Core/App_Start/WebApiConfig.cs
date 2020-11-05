using System.Linq;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Net.Http.Headers;
using System.Web.Http;
using GA.BDC.WebApi.Fundraising.Core.Filters;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;

namespace GA.BDC.WebApi.Fundraising
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            config.EnableCors();
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new CustomHttpsAttribute());
            //config.Filters.Add(new ExceptionHandlerAttribute());
            config.Filters.Add(new ModelStateValidationAttribute());
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
    
}
