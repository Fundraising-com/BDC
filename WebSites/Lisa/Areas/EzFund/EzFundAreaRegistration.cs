using System.Web.Mvc;

namespace GA.BDC.Web.Lisa.Areas.EzFund
{
    public class EzFundAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "EzFund";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EzFund_default",
                "EzFund/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "GA.BDC.Web.Lisa.Areas.EzFund.Controllers" }
            );

            //context.MapRoute(
            //    "EzFund_blog",
            //    "EzFund/{controller}/{action}/{id}",
            //    new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "GA.BDC.Web.Lisa.Areas.EzFund.Controllers" }
            //);

        }
    }
}