using System;
using System.Web;
using System.Web.Mvc;


namespace GA.BDC.Web.Fundraising.MVC.Helpers.Routes.Filters
{
    public class ExceptionHandlerAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
         if (filterContext.Exception != null && !(filterContext.Exception is OperationCanceledException))
         {
            //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, filterContext.Exception);
         }
      }
    }
}