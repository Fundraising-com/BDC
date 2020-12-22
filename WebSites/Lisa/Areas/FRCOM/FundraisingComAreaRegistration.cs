using System.Web.Mvc;

namespace GA.BDC.Web.Lisa.Areas.FRCOM
{
    public class FundraisingComAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "FRCOM";

       public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FRCOM_default",
                "FRCOM/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "GA.BDC.Web.Lisa.Areas.FRCOM.Controllers" }
            );
        }
    }
}