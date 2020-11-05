using System;
using System.Web;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;

namespace GA.BDC.WebApi.EzFund.Filters
{
   public class ExceptionHandlerAttribute : ExceptionFilterAttribute
   {
      public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
         //  if (actionExecutedContext.Exception != null && !(actionExecutedContext.Exception is OperationCanceledException))
         //   {
         //   new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, actionExecutedContext.Exception);
         //}
            base.OnException(actionExecutedContext);
            var request = actionExecutedContext.ActionContext.Request;

            var response = new
            {
                Exception = actionExecutedContext.Exception
            };
            actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.BadRequest, response);
            //actionExecutedContext.ActionContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //actionExecutedContext.Response..Result = new JsonResult
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = new
            //    {
            //        actionExecutedContext.Exception.Message,
            //        actionExecutedContext.Exception.StackTrace
            //    }
            //};
        }
   }
}