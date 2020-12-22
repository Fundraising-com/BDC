using System;
using System.Configuration;
using System.Web.Mvc;

namespace GA.BDC.Web.Fundraising.MVC.Helpers.Routes.Filters
{
    public class RequireWWWAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["forceWWW"]) || request.Url.Host.StartsWith("www") || request.Url.IsLoopback) return;
            var builder = new UriBuilder(request.Url) { Host = "www." + request.Url.Host };
            filterContext.Result = new RedirectResult(builder.ToString());
        }
    }
}