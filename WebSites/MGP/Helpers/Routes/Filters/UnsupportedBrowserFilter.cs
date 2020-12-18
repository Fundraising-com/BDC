using System.Web.Mvc;
namespace GA.BDC.Web.MGP.Helpers.Routes.Filters
{
    /// <summary>
    /// Redirects the request to a 406 error if the browser is not supported
    /// </summary>
    public class UnsupportedBrowserFilter : IActionFilter
    {
        

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var browser = filterContext.HttpContext.Request.Browser.Browser;
            var version = filterContext.HttpContext.Request.Browser.Version;
            double numericVersion;
            if (browser == "IE" && double.TryParse(version, out numericVersion) && numericVersion < 9.0)
            {
                filterContext.Result =
                         new RedirectResult(
                             string.Format("http://{0}{1}", filterContext.HttpContext.Request.Url.Host, "/Error/406"),
                             false);
                return;
            }

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}