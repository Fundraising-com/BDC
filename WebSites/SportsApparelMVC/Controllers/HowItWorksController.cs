using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GA.BDC.Web.SportsApparel.MVC.Controllers
{
    public class HowItWorksController : Controller
    {
        
        public ActionResult Program()
        {
            ViewBag.Message = "Program";

            return View();
        }

        public ActionResult Testimonials()
        {
            ViewBag.Message = "Testimonials";

            return View();
        }

        public ActionResult HowItWorks()
        {
            ViewBag.Message = "How It Works";

            return View();
        }
    }
}