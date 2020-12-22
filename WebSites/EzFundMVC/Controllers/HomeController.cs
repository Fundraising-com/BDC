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
   public class HomeController : Controller
   {
		[Inject]
		public ILogger Logger { get; set; }

		public HomeController()
		{
		}
		public ActionResult Index()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Request a Kit", ParentKey = "Root", Protocol = "http"), Route("request-a-kit")]
        public ActionResult KitRequest()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Request a Selling Kit", ParentKey = "Root", Protocol = "http"), Route("request-selling-kit")]
        public ActionResult SellingKits()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Request a Kit Confirmation", ParentKey = "Root", Protocol = "http"), Route("request-a-kit-confirmation")]
        public ActionResult KitRequestConfirmation()
        {
            HttpContext.Session.Abandon();
            return View();
        }

        [MvcSiteMapNode(Title = "Selling Kit Confirmation", ParentKey = "Root", Protocol = "http"), Route("selling-kit-confirmation")]
        public ActionResult SellingKitConfirmation()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Search", ParentKey = "Root", Protocol = "http"), Route("search")]
        public ActionResult Search()
        {
            return View();
        }

		public ActionResult Redirect()
		{
            var requestedPage = string.Join("", Request.Url.Segments).Substring(1);
            try
			{
				//1. Check if it's an obsolete Url call
				using (var client = new HttpClient())
				{
					var uri = new Uri(string.Format("{0}/routesmappers?url={1}", ConfigurationManager.AppSettings["ezfund.webapi.host"], requestedPage));
					var redirect = client
									.GetAsync(uri)
									.Result
									.Content.ReadAsAsync<string>().Result;
					if (!string.IsNullOrEmpty(redirect))
					{
						return RedirectPermanent(redirect);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.InfoException("Page Route Mapper redirection not found", exception);
			}

			Logger.Warn(string.Format("Redirection not found. Uri: {0}.", Request.Url));

			//2. Nothing found, throw 404
			return HttpNotFound();
		}
	}
}