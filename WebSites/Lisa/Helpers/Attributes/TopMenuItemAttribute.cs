using System.Web.Mvc;

namespace GA.BDC.Web.Lisa.Helpers.Attributes
{
   public class TopMenuItemAttribute : ActionFilterAttribute
   {
      public string Name { get; set; }
      public override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         if (!string.IsNullOrEmpty(Name))
         {
            filterContext.Controller.ViewBag.TopMenuItem = Name;
         }
      }
      
   }
}