using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Web.Lisa.Helpers.Attributes;
using System.Web.Mvc;

namespace GA.BDC.Web.Lisa.Areas.EzFund.Controllers
{
    [TopMenuItem(Name = "EzFund")]
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