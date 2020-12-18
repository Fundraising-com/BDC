using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GA.BDC.Web.SportsApparel.MVC.Controllers
{
    public class ApparelController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Tops()
        {

            ViewBag.Message = "Tops";
            return View();
        }

        public ActionResult Bottoms()
        {

            ViewBag.Message = "Bottoms";
            return View();
        }

        public ActionResult Outerwear()
        {

            ViewBag.Message = "Outerwear";
            return View();
        }
    }
}