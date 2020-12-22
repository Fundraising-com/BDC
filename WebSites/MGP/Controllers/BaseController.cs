using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace GA.BDC.Web.MGP.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetNoStore();
            filterContext.HttpContext.Response.Cache.AppendCacheExtension("no-cache");
            filterContext.HttpContext.Response.Expires = 0;
            var titleFilters = ConfigurationManager.AppSettings["titleFilters"].Split(',');
            var title = titleFilters.Aggregate(Request.Url.Host, (current, titleFilter) => current.Replace(titleFilter, ""));
            var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            ViewBag.PageTitle = textInfo.ToTitleCase(title);
            ViewBag.IsMobile = HttpContext.Request.Browser.IsMobileDevice;
        }
    }
}
