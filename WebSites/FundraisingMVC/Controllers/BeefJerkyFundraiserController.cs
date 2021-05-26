using System.Web.Mvc;
using MvcSiteMapProvider;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    public class BeefJerkyFundraiserController : Controller
    {
        [MvcSiteMapNode(Title = "Beef Jerky", ParentKey = "Root", Protocol = "https"), Route("beef-jerky-fundraiser")]
        public ActionResult Index()
        {
            return View();
        }
    }
}