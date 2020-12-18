using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Web.SportsApparel.MVC.Controllers
{
    [RoutePrefix("apparels"), AllowAnonymous]
    public class ApparelsController : Controller
    {
        // GET: Apparels
        public ActionResult Index()
        {
            return View();
        }

        


    }
}