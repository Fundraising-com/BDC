using System;
using System.Linq;
using System.Configuration;
using System.Web.Mvc;

namespace GA.BDC.Web.EzFundMVC.Helpers.Routes.Filters
{
    public class RequireWWWAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var request = filterContext.HttpContext.Request;
            //Get ezfund referal http
            if (filterContext.HttpContext.Session["ReferralDomain"] == null)
            {
                filterContext.HttpContext.Session["ReferralDomain"] = request.UrlReferrer != null ? request.UrlReferrer.Host : string.Empty;
            }
            

            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["forceWWW"]) || request.Url.Host.StartsWith("www") || request.Url.IsLoopback) return;
            var builder = new UriBuilder(request.Url) { Host = "www." + request.Url.Host };
            filterContext.Result = new RedirectResult(builder.ToString());

           

        }
    }
}