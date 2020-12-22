using System.Web.Mvc;
using GA.BDC.Web.Lisa.Helpers.Attributes;

namespace GA.BDC.Web.Lisa.Areas.FRCOM.Controllers
{
   [TopMenuItem(Name = "FRCOM")]
   [CustomAuthorize(Roles = "Lisa - Manager, Lisa - Admin")]
   public class BlogController : Controller
   {
      public ActionResult Index()
      {
         return RedirectToAction("Posts");
      }

      public ActionResult Posts()
      {
         return View();
      }

      public ActionResult Categories()
      {
         return View();
      }

      public ActionResult Tags()
      {
         return View();
      }
   }
}