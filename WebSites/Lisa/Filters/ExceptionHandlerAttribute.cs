using System;
using System.Web;
using System.Web.Mvc;


namespace GA.BDC.Web.Lisa.Filters
{
   public class ExceptionHandlerAttribute : HandleErrorAttribute
   {
      public override void OnException(ExceptionContext filterContext)
      {
         if (filterContext.Exception != null && !(filterContext.Exception is OperationCanceledException))
         {
            //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, filterContext.Exception);
         }
         base.OnException(filterContext);
      }
   }
}