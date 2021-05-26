using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using GA.BDC.Shared.Entities;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;
using System.Net;
using Google.Cloud.Diagnostics.AspNet;
using System.Threading.Tasks;
using System.Xml;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("Fundraising-Products"), AllowAnonymous]
    public class FundraisingProductsController : Controller
    {
        // GET: FundraisingProducts
        public ActionResult Index()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "All Cotton Candy", ParentKey = "Root", Protocol = "https"), Route("all-cotton-candy")]
        public ActionResult AllCottonCandy()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "All Scented Fundaisers", ParentKey = "Root", Protocol = "https"), Route("all-scented-fundraisers")]
        public ActionResult AllScentedFundraisers()
        {
            return View();
        }




    }
}