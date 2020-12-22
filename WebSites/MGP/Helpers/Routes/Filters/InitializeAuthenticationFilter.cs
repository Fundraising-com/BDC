using System;
using System.Web.Mvc;
using WebMatrix.WebData;
using IActionFilter = System.Web.Mvc.IActionFilter;

namespace GA.BDC.Web.MGP.Helpers.Routes.Filters
{
   /// <summary>
   /// Handles the initialization of the authentication process
   /// </summary>
   public class InitializeAuthenticationFilter : IActionFilter
   {

      public void OnActionExecuting(ActionExecutingContext filterContext)
      {
         if (!WebSecurity.Initialized)
         {
            WebSecurity.InitializeDatabaseConnection("esubs_global_v2", "users", "user_id", "username", false);
         }
      }

      public void OnActionExecuted(ActionExecutedContext filterContext)
      {
      }
   }
}