using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;
using SWCorporate.SystemEx;
using SWCorporate.SystemEx.Web;
using System.Linq;
namespace GA.BDC.Web.MGP.Helpers.Routes.Filters
{
    /// <summary>
    /// Handles all the exceptions that may happen in any controller
    /// </summary>
   // public class ExceptionFilter : IExceptionFilter
   // {
   //     public void OnException(ExceptionContext filterContext)
   //     {
   //      var actionName = filterContext.RouteData.Values["action"].ToString();
   //      var controllerType = filterContext.Controller.GetType();
   //      var methods = controllerType.GetMethods().Where(p => p.Name == actionName);
   //      var method = filterContext.RequestContext.HttpContext.Request.HttpMethod == "POST"
   //          ? methods.FirstOrDefault(p => p.GetCustomAttributes(typeof(HttpPostAttribute), false).Length > 0)
   //          : methods.FirstOrDefault(p => p.GetCustomAttributes(typeof(HttpPostAttribute), false).Length == 0);
   //      var returnType = method != null ? method.ReturnType : typeof(ContentResult);         
   //      if (filterContext.Exception != null && !ExceptionFilterHelper.IgnoreException(filterContext))
   //      {
   //         var validationException = filterContext.Exception as DbEntityValidationException;
   //         if (validationException != null)
   //         {
   //            foreach (var error in validationException.EntityValidationErrors.SelectMany(p => p.ValidationErrors))
   //            {
   //               if (!validationException.Data.Contains($"ValidationError_{error.PropertyName}"))
   //               {
   //                  validationException.Data.Add($"ValidationError_{error.PropertyName}", error.ErrorMessage);
   //               }
   //            }
   //         }
   //         new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, filterContext.Exception);
   //      }
   //      if (returnType == typeof(JsonResult) || returnType == typeof(ContentResult))
   //      {
   //         filterContext.Result = new JsonResult
   //         {
   //            Data = new
   //            {
   //               success = false,
   //               responseText = "An exception has ocurred in the server. We are working to fix it.",
   //               ExceptionMessage = filterContext.Exception.Message
   //            },
   //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
   //         };
   //      }
   //      else
   //      {
   //         filterContext.Result = new RedirectResult(string.Format("http://{0}{1}", filterContext.HttpContext.Request.Url.Host, "/Error/500"), false);
   //      }
   //      filterContext.ExceptionHandled = true;
   //   }
   // }

   //public static class ExceptionFilterHelper
   //{
   //   /// <summary>
   //   /// Indicates if the Exception should be ignored and not logged in our Instrumentation Service
   //   /// </summary>
   //   /// <param name="exceptionContext">Exception to be examined</param>
   //   /// <returns>True if it must be ignored, False otherwise</returns>
   //   public static bool IgnoreException(ExceptionContext exceptionContext)
   //   {
   //      var filteredExceptionMessages = ConfigurationManager.AppSettings["FilteredExceptionMessages"].Split('|');
   //      var filteredExceptionTypes = ConfigurationManager.AppSettings["FilteredExceptionTypes"].Split('|');
   //      var filteredUserAgents = ConfigurationManager.AppSettings["FilteredUserAgents"].Split('|');
   //      return filteredExceptionMessages.Any(p => exceptionContext.Exception.Message.Contains(p)) || filteredExceptionTypes.Any(p => typeof(Exception).ToString().Equals(p, StringComparison.InvariantCultureIgnoreCase)) || filteredUserAgents.Any(p => exceptionContext.HttpContext.Request.UserAgent.Contains(p));
   //   }
   //}
}