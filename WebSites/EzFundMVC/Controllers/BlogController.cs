using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Routing;
using GA.BDC.Shared.Entities;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.EzFundMVC.Controllers
{
    [RoutePrefix("blog"), AllowAnonymous]
    public class BlogController : Controller
    {
        [Inject]

        public ILogger Logger { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Sports Teams: Personalize your Fundraiser", ParentKey = "Root", Protocol = "https"), Route("Sports-Teams-fundraisers")]
        public ActionResult sportsteams()
        {
            return View();
        }



        [MvcSiteMapNode(Title = "blog-spanish", ParentKey = "Root", Protocol = "https"), Route("blog-spanish")]
        public ActionResult blogspanish()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Low-Cost or Even No-Cost Fundraising Ideas", ParentKey = "Root", Protocol = "https"), Route("Low-Cost-fundraisers-ideas")]
        public ActionResult LowCostFundraisings()
        {
            return View();
        }


        [MvcSiteMapNode(Title = "4 Fundraising Ideas for Middle School Coaches", ParentKey = "Root", Protocol = "https"), Route("4-Fundraising-Ideas-Middle-School-Coaches")]
        public ActionResult fundraisingscoaches()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "How to Put the Fun in Fundraising", ParentKey = "Root", Protocol = "https"), Route("fun-fundraisers")]
        public ActionResult funfundraisers()
        {
            return View();
        }


        [MvcSiteMapNode(Title = "4 Tips Eco-Friendly Fundraisers for your School", ParentKey = "Root", Protocol = "https"), Route("4-Tips-Eco-Friendly-Fundraisers")]
        public ActionResult tips()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Football Fundraisers Guaranteed To Make You Money", ParentKey = "Root", Protocol = "https"), Route("Football-Fundraisers")]
        public ActionResult football()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "4 Tips to Sell More at your Fundraiser", ParentKey = "Root", Protocol = "https"), Route("4-Tips-to-Sell-More")]
        public ActionResult tipstosell()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "3 Ways to Get the Word Out About Your Next Fundraiser", ParentKey = "Root", Protocol = "https"), Route("3-Ways-to-Get-the-Word-Out")]
        public ActionResult getthewordout()
        {
            return View();
        }

    }
}
