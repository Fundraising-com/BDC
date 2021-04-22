using System.Web.Mvc;
using GA.BDC.Web.Lisa.Helpers.Attributes;

namespace GA.BDC.Web.Lisa.Areas.Reports.Controllers
{
   [TopMenuItem(Name = "REPORTS")]
   [CustomAuthorize(Roles = "Lisa - Manager, Lisa - Admin")]
   public class ReportController : Controller
   {
      public ActionResult Spider()
      {
         return View();
      }
      public ActionResult RepeatedBusiness()
      {
         return View();
      }
      public ActionResult GrossProfit()
      {
         return View();
      }

    public ActionResult SalesToProcess()
    {
        return View();
    }

        public ActionResult SalesToProcessNEW()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult CustomerList()
        {
            return View();
        }

        public ActionResult TraditionalConfirmedSalesByProductClass()
        {
            return View();
        }
    }
}