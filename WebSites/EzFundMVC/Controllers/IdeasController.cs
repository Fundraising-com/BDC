using System.Web.Mvc;
using MvcSiteMapProvider;

namespace GA.BDC.Web.EzFundMVC.Controllers
{
    [RoutePrefix("ideas"), AllowAnonymous]
    public class IdeasController : Controller
    {
        // GET: Ideas
        public ActionResult Index()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Fundraising A to Z", ParentKey = "Root", Protocol = "http"), Route("a-to-z-fundraising-ideas")]
        public ActionResult FundraisingAz()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Non-Profit Fundraising", ParentKey = "Root", Protocol = "http"), Route("non-profit-fundraising")]
        public ActionResult NonProfitFundraising()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Fundraisers for Schools", ParentKey = "Root", Protocol = "http"), Route("fundraisers-for-schools")]
        public ActionResult FundraisersForSchools()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "How To Raise Funds Online.", ParentKey = "Root", Protocol = "http"), Route("raise-funds-online")]
        public ActionResult RaiseFundsOnline()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Summer Fundraisers Ideas", ParentKey = "Root", Protocol = "http"), Route("summer-fundraising-ideas")]
        public ActionResult SummerFundIdeas()
        {
            return View();
        }

    }
}