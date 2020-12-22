using System.Web.Mvc;
using GA.BDC.Shared.Entities;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("about-us"), AllowAnonymous]
    public class AboutUsController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }

        [MvcSiteMapNode(Title = "About Us", ParentKey = "Root", Key = "OurCompany", Protocol = "https"), Route("our-company")]
        public ActionResult OurCompany()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Referral Program", ParentKey = "OurCompany", Protocol = "https"), Route("referral-program")]
        public ActionResult ReferralProgram()
        {
            var referralProgram = new ReferralProgram { AlreadyPurchased = 1, Friends = new[] { new Friend() } };
            return View(referralProgram);
        }

        [MvcSiteMapNode(Title = "Shipping Policies", ParentKey = "OurCompany", Protocol = "https"), Route("shipping-policies")]
        public ActionResult ShippingPolicies()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Testimonials", ParentKey = "OurCompany", Protocol = "https"), Route("testimonials")]
        public ActionResult Testimonials()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Guarantees", ParentKey = "OurCompany", Protocol = "https"), Route("guarantees")]
        public ActionResult Guarantees()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Contact Us", ParentKey = "OurCompany", Protocol = "https"), Route("contact-us")]
        public ActionResult ContactUs()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Privacy Policy", ParentKey = "OurCompany", Protocol = "https"), Route("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Sitemap", ParentKey = "OurCompany", Protocol = "https"), Route("site-map")]
        public ActionResult Sitemap()
        {
            return View();
        }
    }
}