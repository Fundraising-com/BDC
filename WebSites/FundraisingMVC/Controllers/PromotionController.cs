using System.Web.Mvc;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("promotion"), AllowAnonymous]
    public class PromotionController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }

        [MvcSiteMapNode(Title = "Promotions", ParentKey = "Root", Key = "Promotion", Protocol = "https"), Route("Promotions")]
        public ActionResult FundraisingPromotion()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Movember", ParentKey = "Promotion", Protocol = "https"), Route("Movember-Promotions")]
        public ActionResult Movember()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "T-shirt & Scratchcard Promotions", ParentKey = "Promotion", Protocol = "https"), Route("fundraising-promotions")]
        public ActionResult Promotions()
        {
            return View();
        }
    }
}