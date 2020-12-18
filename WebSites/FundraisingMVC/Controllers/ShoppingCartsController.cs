using System.Web.Mvc;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
   [RoutePrefix("shopping-cart"), AllowAnonymous]
   public class ShoppingCartsController : Controller
   {
      [Inject]
      public ILogger Logger { get; set; }

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