using System.Web.Mvc;

namespace GA.BDC.Web.MGP.Controllers
{
    public class ErrorController : BaseController
    {
        [ActionName("500")]
        public ActionResult _500()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        [ActionName("404")]
        public ActionResult _404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        [ActionName("403")]
        public ActionResult _403()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        [ActionName("406")]
        public ActionResult _406()
        {
            Response.StatusCode = 406;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}
