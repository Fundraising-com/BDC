using System;
using System.Web;
using System.Web.Http.Filters;


namespace GA.BDC.WebApi.Reports.Filters
{
   public class ExceptionHandlerAttribute : ExceptionFilterAttribute
   {
      public override void OnException(HttpActionExecutedContext actionExecutedContext)
      {
         if (actionExecutedContext.Exception != null && !(actionExecutedContext.Exception is OperationCanceledException))
         {
            //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, actionExecutedContext.Exception);
         }
         base.OnException(actionExecutedContext);
      }
   }
}