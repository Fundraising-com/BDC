using System;
using System.Web.Mvc;

namespace GA.BDC.Web.MGP.Helpers.Routes.Attributes
{
   /// <summary>
   /// Finds and renders all the branding attributes for a Partner to be displayed
   /// </summary>
   public class RenderPartnerBrandingAttribute : ActionFilterAttribute
   {
      public override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         if (filterContext.HttpContext.Session == null) throw new Exception("Session is null");
         var partnerId = Convert.ToInt32(filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTNER_ID]);
	      var branding = PartnerHelper.CreatePartnerBranding(partnerId);
         filterContext.Controller.ViewBag.Partner = branding;
      }
   }
}