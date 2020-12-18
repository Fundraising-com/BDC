using System.Web.Mvc;
using GA.BDC.Web.Lisa.Helpers.Attributes;

namespace GA.BDC.Web.Lisa.Areas.FRCOM.Controllers
{
   [TopMenuItem(Name = "FRCOM")]
   //[CustomAuthorize(Roles = "Lisa - Manager, Lisa - Admin")]
   public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}