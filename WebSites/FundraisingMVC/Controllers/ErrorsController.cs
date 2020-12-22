using System.Web.Mvc;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("Errors")]
    public class ErrorsController : Controller
    {
        [Route("404")]
        public ActionResult ResourceNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        [Route("500")]
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}