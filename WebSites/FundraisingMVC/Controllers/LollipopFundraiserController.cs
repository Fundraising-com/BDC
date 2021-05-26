using System.Web.Mvc;
using MvcSiteMapProvider;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    public class LollipopFundraiserController : Controller
    {
        [MvcSiteMapNode(Title = "Lollipops", ParentKey = "Root", Protocol = "https"), Route("lollipop-fundraiser")]
        public ActionResult Index()
        {
            return View();
        }
    }
}