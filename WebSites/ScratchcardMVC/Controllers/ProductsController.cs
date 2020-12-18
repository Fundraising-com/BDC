using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;
using GA.BDC.Shared.Entities;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Scratchcard.MVC.Controllers
{

   
    public class ProductsController : Controller
    {


        [Inject]
        public ILogger Logger { get; set; }



        // GET: Products
        public ActionResult church()
        {
            return View();
        }

        public ActionResult dance()
        {
            return View();
        }

        public ActionResult school()
        {
            return View();
        }

        public ActionResult sports()
        {
            return View();
        }

        public ActionResult cheerleading()
        {
            return View();
        }

        public ActionResult others()
        {
            return View();
        }
    }
}