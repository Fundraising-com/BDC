using System.Web.Mvc;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("online-fundraising"), AllowAnonymous]
    public class OnlineFundraisingController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }

        [MvcSiteMapNode(Title = "How It Works", ParentKey = "Root", Key = "OnlineFundraising", Protocol = "https"), Route("how-it-works")]
        public ActionResult HowItWorks()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Features", ParentKey = "OnlineFundraising", Protocol = "https"), Route("features")]
        public ActionResult Features()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Products", ParentKey = "OnlineFundraising", Protocol = "https"), Route("products")]
        public ActionResult Products()
        {
            return View();
        }
    }
}