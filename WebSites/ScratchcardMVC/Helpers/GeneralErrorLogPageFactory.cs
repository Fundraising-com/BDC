using System;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using Elmah;

namespace GA.BDC.Web.Scratchcard.MVC.Helpers
{
    public class GeneralErrorLogPageFactory : ErrorLogPageFactory
    {
        public override IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            var appname = context.Request.Params["app"];
            if (String.IsNullOrEmpty(appname))
            {
                // appname was not found - let's check the referrer
                //if (context.Request.UrlReferrer != null && !"/stylesheet".Equals(context.Request.PathInfo, StringComparison.OrdinalIgnoreCase))
                //{
                //    appname = HttpUtility.ParseQueryString(context.Request.UrlReferrer.Query)["app"];
                //    context.Response.Redirect(String.Format("{0}{1}app={2}", context.Request.RawUrl,
                //                                            context.Request.Url.Query.Length > 0 ? "&" : "?", appname));
                //}
            }

            IHttpHandler handler = base.GetHandler(context, requestType, url, pathTranslated);
            var err = new SqlErrorLog(WebConfigurationManager.ConnectionStrings["EFRCommon"].ConnectionString);
            err.ApplicationName = appname ?? "GA.BDC.Web.Scratchcard.MVC";

            // we need to fool elmah completely
            object v = typeof(ErrorLog).GetField("_contextKey", BindingFlags.NonPublic | BindingFlags.Static)
                                            .GetValue(null);
            context.Items[v] = err;

            return handler;
        }
    }
}