using System.Web.Mvc;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Scratchcard.MVC.Controllers
{

    [RoutePrefix("resources"), AllowAnonymous]

    public class ResourcesController : Controller
    {

        [Inject]
        public ILogger Logger { get; set; }

        // GET: Resources
        public ActionResult Index()
        {
            return View();
        }


        [MvcSiteMapNode(Title = "Payment & Shipping", ParentKey = "Resources", Protocol = "http"), Route("payment-and-shipping")]
        public ActionResult PaymentShipping()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Privacy", ParentKey = "Resources", Protocol = "http"), Route("privacy")]
        public ActionResult Privacy()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Tips", ParentKey = "Resources", Protocol = "http"), Route("tips")]
        public ActionResult Tips()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "FAQ", ParentKey = "Resources", Protocol = "http"), Route("faq")]
        public ActionResult FAQ()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Business Opportunity", ParentKey = "Resources", Protocol = "http"), Route("business-opportunity")]
        public ActionResult BusinessOpportunity()
        {
            return View();
        }
    }
}