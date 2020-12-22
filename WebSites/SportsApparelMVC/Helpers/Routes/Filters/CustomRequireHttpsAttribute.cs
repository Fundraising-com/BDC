using System;
using System.Configuration;
using System.Web.Mvc;

namespace GA.BDC.Web.Fundraising.MVC.Helpers.Routes.Filters
{
    public class CustomRequireHttpsAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            if (bool.Parse(ConfigurationManager.AppSettings["allowHttpsOnly"]) && !request.IsSecureConnection)
            {
                var builder = new UriBuilder($"https://{request.Url.Host}{request.RawUrl}");
                filterContext.Result = new RedirectResult(builder.ToString());
            }
        }
    }
}