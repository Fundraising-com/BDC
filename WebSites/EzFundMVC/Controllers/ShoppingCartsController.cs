using System.Web.Mvc;

namespace GA.BDC.Web.EzFundMVC.Controllers
{
    [RoutePrefix("shopping-cart"), AllowAnonymous]
    public class ShoppingCartsController : Controller
    {
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("checkout/{doCallback?}")]
        public ActionResult CheckOut(bool doCallback = false)
        {
            if (!User.Identity.IsAuthenticated && doCallback)
            {
                return RedirectToAction("RegistrationInvitation");
            }
            return View();
        }
        [Route("do-you-want-to-register")]
        public ActionResult RegistrationInvitation()
        {
            return View();
        }
        [Route("order-confirmation/{id}")]
        public ActionResult Confirmation(int id)
        {
            ViewBag.ClientId = id;
            return View();
        }

        [Route("paypal-return")]
        public ActionResult PaypalConfirmation()
        {
            ViewBag.PaymentId = Request.Params["PaymentId"];
            ViewBag.PayerId = Request.Params["PayerID"];
            return View();
        }
    }
}
