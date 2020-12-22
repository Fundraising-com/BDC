using System;
using System.Web.Mvc;

namespace GA.BDC.Web.Scratchcard.MVC.Helpers.Routes.Filters
{
    public class CustomRequireHttpsAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.HttpContext.Request.IsLocal)
            {
                // when connection to the application is local, don't do any HTTPS stuff
                return;
            }

            base.OnAuthorization(filterContext);

            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult(url, true);
            }
        }
    }
}