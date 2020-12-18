using System.Web.Mvc;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("partners"), AllowAnonymous]
    public class PartnersController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }

               
        [MvcSiteMapNode(Title = "Partnerships", ParentKey = "Root", Key = "Partners", Protocol = "https"), Route("our-partners")]
        public ActionResult OurPartners()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Affiliate Program", ParentKey = "Partners", Protocol = "https"), Route("affiliate-program")]
        public ActionResult AffiliateProgram()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Donation Fundraising", ParentKey = "Partners", Protocol = "https"), Route("donation-fundraising")]
        public ActionResult DonationFundraising()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Login", ParentKey = "Partners", Protocol = "https"), Route("login")]
        public ActionResult Login()
        {
            return RedirectPermanent("http://fundraising.postaffiliatepro.com/affiliates/login.php#login");
        }
        [MvcSiteMapNode(Title = "Envision Fundraising", ParentKey = "Partners", Protocol = "https"), Route("Envision-Fundraising")]
        public ActionResult EnvisionFundraising()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Cheerleading.com", ParentKey = "Partners", Protocol = "https"), Route("Cheerleading")]
        public ActionResult Cheerleading()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "USAFootball", ParentKey = "Partners", Protocol = "https"), Route("usafootball")]
        public ActionResult usafootball()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "ASA Fundraising", ParentKey = "Partners", Protocol = "https"), Route("about-asa")]
        public ActionResult aboutasa()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Girls Got Game Fundraising", ParentKey = "Partners", Protocol = "https"), Route("girlsgotgame")]
        public ActionResult girlsgotgame()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "SportDecals Fundraising", ParentKey = "Partners", Protocol = "https"), Route("sportdecals")]
        public ActionResult sportdecals()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Teamcheer Fundraising", ParentKey = "Partners", Protocol = "https"), Route("teamcheer")]
        public ActionResult teamcheer()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "USAFundraising.com Fundraising", ParentKey = "Partners", Protocol = "https"), Route("usafundraising")]
        public ActionResult usafundraising()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "cheerdeals.com Fundraising", ParentKey = "Partners", Protocol = "https"), Route("cheerdeals")]
        public ActionResult cheerdeals()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Southwestern Real Estate", ParentKey = "Partners", Protocol = "https"), Route("sw-real-estate")]
        public ActionResult swrealestate()
        {
            return View();
        }
    }
}