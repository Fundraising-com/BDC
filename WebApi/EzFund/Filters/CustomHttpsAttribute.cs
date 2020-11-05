using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
namespace GA.BDC.WebApi.EzFund.Filters
{
   public class CustomHttpsAttribute : ActionFilterAttribute
   {
      public override void OnActionExecuting(HttpActionContext actionContext)
      {
         //HTTPS validation, it only accepts requests coming from HTTPS.
         if (bool.Parse(ConfigurationManager.AppSettings["allowHttpsOnly"]) && !String.Equals(actionContext.Request.RequestUri.Scheme, "https", StringComparison.OrdinalIgnoreCase))
         {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
               Content = new StringContent("HTTPS Required")
            };
            return;
         }
      }
   }
}