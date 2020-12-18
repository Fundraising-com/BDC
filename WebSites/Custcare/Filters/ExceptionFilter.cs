using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using log4net;

namespace GA.BDC.Web.Custcare.Filters
{
    /// <summary>
    /// Handles all the exceptions that may happen in any controller
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        public ILog Log { get; set; }

        public ExceptionFilter()
        {
            Log = LogManager.GetLogger(typeof(ExceptionFilter));
        }

        public void OnException(ExceptionContext filterContext)
        {
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var controllerType = filterContext.Controller.GetType();
            var methods = controllerType.GetMethods().Where(p => p.Name == actionName);
            var method = filterContext.RequestContext.HttpContext.Request.HttpMethod == "POST"
                ? methods.First(p => p.GetCustomAttributes(typeof(HttpPostAttribute), false).Length > 0)
                : methods.First(p => p.GetCustomAttributes(typeof(HttpPostAttribute), false).Length == 0);
            var returnType = method.ReturnType;
            if (!(filterContext.Exception is System.Web.HttpRequestValidationException))
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat(
                    "Server: {3}. Url: {4}. User IP: {5}. User Agent:{6}. Controller: {0}. Action: {1}. Parameters: {2}.",
                    controllerType.Name, actionName,
                    string.Join(", ",
                        filterContext.RequestContext.RouteData.Values.Select(p => string.Join("=", p.Key, p.Value))),
                    filterContext.HttpContext.Server.MachineName, filterContext.HttpContext.Request.RawUrl,
                    filterContext.HttpContext.Request.UserHostName, filterContext.HttpContext.Request.UserAgent);
                Log.Error(LogError(stringBuilder, filterContext.Exception), filterContext.Exception);
            }
            if (returnType == typeof(JsonResult) || returnType == typeof(ContentResult))
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        responseText = "An exception has ocurred in the server. We are working to fix it.",
                        ExceptionMessage = filterContext.Exception.Message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = new RedirectResult(string.Format("http://{0}{1}", filterContext.HttpContext.Request.Url.Host, "/Error/500"), false);
            }
            filterContext.ExceptionHandled = true;
        }
        /// <summary>
        /// Improves the Exception logging by writting particular properties that the Exception may have
        /// </summary>
        /// <param name="stringBuilder">Exception string</param>
        /// <param name="exception">Exception</param>
        /// <returns>Exception string</returns>
        private static string LogError(StringBuilder stringBuilder, Exception exception)
        {
            if (exception is DbEntityValidationException)
            {
                stringBuilder.AppendFormat("Validation Errors: {0}", string.Join(", ", (exception as DbEntityValidationException).EntityValidationErrors.SelectMany(p => p.ValidationErrors.Select(a => string.Join("=", a.PropertyName, a.ErrorMessage)))));
            }
            return stringBuilder.ToString();
        }
    }
}