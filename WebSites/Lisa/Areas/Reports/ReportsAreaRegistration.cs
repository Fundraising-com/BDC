using System.Web.Mvc;

namespace GA.BDC.Web.Lisa.Areas.Reports
{
    public class ReportsAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Reports";

       public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Reports_default",
                "Reports/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "GA.BDC.Web.Lisa.Areas.Reports.Controllers" }
            );
        }
    }
}