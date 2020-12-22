using System.Web.Mvc;
using MvcSiteMapProvider;

namespace GA.BDC.Web.EzFundMVC.Controllers
{
    [RoutePrefix("resources"), AllowAnonymous]
    public class ResourcesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Contact Us", ParentKey = "Root", Protocol = "http"), Route("contact-us")]
        public ActionResult ContactUs()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "About Us", ParentKey = "Root", Protocol = "http"), Route("about-us")]
        public ActionResult AboutUs()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Full Service Fundraising", ParentKey = "Root", Protocol = "http"), Route("full-service-fundraising")]
        public ActionResult FullServiceFundraising()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Fundraising Sites", ParentKey = "Root", Protocol = "http"), Route("fundraising-sites")]
        public ActionResult FundraisingSites()
        {
            return View("Links");
        }
        [MvcSiteMapNode(Title = "Privacy Policy", ParentKey = "Root", Protocol = "http"), Route("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Fundraising Guide", ParentKey = "Root", Protocol = "http"), Route("fundraising-guide")]
        public ActionResult FundraisingGuide()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Site Map", ParentKey = "Root", Protocol = "http"), Route("site-map")]
        public ActionResult SiteMap()
        {
            return View();
        }
    }
}

