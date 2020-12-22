using GA.BDC.Web.Lisa.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace GA.BDC.Web.Lisa.Areas.EzFund.Controllers
{
    [TopMenuItem(Name = "EzFund")]
    [CustomAuthorize(Roles = "Lisa - Manager, Lisa - Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
