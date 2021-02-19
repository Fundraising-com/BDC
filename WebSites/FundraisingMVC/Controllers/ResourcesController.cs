using System.Web.Mvc;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("resources"), AllowAnonymous]
    public class ResourcesController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }

        [MvcSiteMapNode(Title = "Fundraising Resources", ParentKey = "Root", Key = "Resources", Protocol = "https"), Route("fundraising-resources")]
        public ActionResult FundraisingResources()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Consultants", ParentKey = "Resources", Protocol = "https"), Route("Consultants")]
        public ActionResult Consultants()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Activities", ParentKey = "Resources", Protocol = "https"), Route("fundraising-activities")]
        public ActionResult Activities()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Sports", ParentKey = "Resources", Protocol = "https"), Route("sports-fundraisers")]
        public ActionResult Sports()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Fundraising 101", ParentKey = "Resources", Protocol = "https"), Route("fundraising-101")]
        public ActionResult Fundraising101()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Girl Scout", ParentKey = "Resources", Protocol = "https"), Route("girl-scouts-fundraisers")]
        public ActionResult GirlScout()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Boy Scout", ParentKey = "Resources", Protocol = "https"), Route("boy-scouts-fundraisers")]
        public ActionResult BoyScout()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Checklist", ParentKey = "Resources", Protocol = "https"), Route("fundraising-checklist")]
        public ActionResult Checklist()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Schools", ParentKey = "Resources", Protocol = "https"), Route("school-fundraising-ideas")]
        public ActionResult Schools()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "School Fundraising 101", ParentKey = "Resources", Protocol = "https"), Route("School-Fundraising-101")]
        public ActionResult SchoolFundraising101()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Candy (Chocolate) FAQs", ParentKey = "Resources", Protocol = "https"), Route("Candy-Faqs")]
        public ActionResult CandyFaqs()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Lollipop FAQs", ParentKey = "Resources", Protocol = "https"), Route("Lollipop-Faqs")]
        public ActionResult LollipopFaqs()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Cookie Dough FAQs", ParentKey = "Resources", Protocol = "https"), Route("Cookie-Dough-Faqs")]
        public ActionResult CookieDoughFaq()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Charity", ParentKey = "Resources", Protocol = "https"), Route("Charity-Fundraising")]
        public ActionResult Charity()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Church Fundraising Ideas", ParentKey = "Resources", Protocol = "https"), Route("Church-Fundraising-Ideas")]
        public ActionResult ChurchFundraising101()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Cheer Fundraising", ParentKey = "Resources", Protocol = "https"), Route("Cheer-Fundraising")]
        public ActionResult Cheer()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "League Fundraising", ParentKey = "Resources", Protocol = "https"), Route("League-Fundraising")]
        public ActionResult League()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Fundraising Coupon", ParentKey = "Resources", Protocol = "https"), Route("Fundraising-Coupon")]
        public ActionResult Coupon()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Spring Fundraising", ParentKey = "Resources", Protocol = "https"), Route("Spring-Fundraising")]
        public ActionResult Spring()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Valentines Fundraising", ParentKey = "Resources", Protocol = "https"), Route("Valentines-Fundraising")]
        public ActionResult Valentines()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "3 Fundraising Formulas", ParentKey = "Resources", Protocol = "https"), Route("3-Fundraising-Formulas")]
        public ActionResult FundraisingFormulas()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Power Products", ParentKey = "Resources", Protocol = "https"), Route("Power-Products")]
        public ActionResult PowerProducts()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Cheerleading Fundraising", ParentKey = "Resources", Protocol = "https"), Route("Cheerleading-Fundraisers")]
        public ActionResult CheerFundraising()
        {
            return View();
        }

    }
}