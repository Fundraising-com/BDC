using System.Web.Mvc;
using Ninject;
using Ninject.Extensions.Logging;
using MvcSiteMapProvider;
using System;
using System.Configuration;
using System.Net.Http;
using GA.BDC.Shared.Entities;
using System.Collections.Generic;

namespace GA.BDC.Web.SportsApparel.MVC.Controllers
{
    public class CatalogController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }

        public ActionResult Index()
        {
            return View();
        }



        [MvcSiteMapNode(Title = "All Catalogs", ParentKey = "Root", Protocol = "https"), Route("all-catalogs")]
        public ActionResult AllCatalogs()
        {

            return View();
        }

        public ActionResult lowminimum6()
        {

            return View();
        }

        public ActionResult quickeasy8()
        {

            return View();
        }

        public ActionResult lowretail10()
        {

            return View();
        }
        public ActionResult standard12()
        {

            return View();
        }

        public ActionResult classic18()
        {

            return View();
        }

        public ActionResult adidas()
        {

            return View();
        }

        public ActionResult createyourown()
        {
            int rootPackageID = 0;
            rootPackageID = Convert.ToInt32(ConfigurationManager.AppSettings["Easyapparel.Root.PackageId"]);

            using (var client = new HttpClient())
            {
                var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/products?categoryId=966");
                var product = client
                            .GetAsync(uri)
                            .Result
                             .Content.ReadAsAsync<IEnumerable<Product>>().Result;
                ViewBag.Products = product;

                return View(product);
            }
        }

    }

   
}