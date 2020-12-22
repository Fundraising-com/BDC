using System.Web.Mvc;
using GA.BDC.Web.Lisa.Helpers.Attributes;

namespace GA.BDC.Web.Lisa.Controllers
{
   public class HomeController : Controller
   {
      [CustomAuthorize(Roles = "Lisa - Manager, Lisa - Admin, Lisa - User")]
      public ActionResult Index()
      {
         return View();
      }
   }
}