using System;
using System.Web;
using System.Web.Mvc;
using GA.BDC.Web.Lisa.Helpers.Attributes;

namespace GA.BDC.Web.Lisa.Controllers
{
   [CustomAuthorize]
   public class AccountController : Controller
   {
      public AccountController()
      {
      }

      [AllowAnonymous]
      public ActionResult Login()
      {
         return View();
      }

      [HttpGet]
      public ActionResult Logout()
      {
         if (Request.Cookies["lisa.auth"] != null)
         {
            var c = new HttpCookie("lisa.auth") {Expires = DateTime.Now.AddDays(-1)};
            Response.Cookies.Add(c);
         }
         return RedirectToAction("Login");
      }
      
   }
}