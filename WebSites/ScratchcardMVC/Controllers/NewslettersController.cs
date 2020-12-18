using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;
using GA.BDC.Shared.Entities;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Scratchcard.MVC.Controllers
{
    public class NewslettersController : Controller
    {

        [Inject]
        public ILogger Logger { get; set; }



        /// <summary>
        /// Shows a specific newsletter
        /// </summary>
        /// <param name="url">newsletter db title</param>
        /// <returns></returns>
        public ActionResult Newsletters(string query = "")
        {
            if (!string.IsNullOrEmpty(query))
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format("{0}/newsletters/?url={1}", ConfigurationManager.AppSettings["fundraising.webapi.host"], query));
                    var newsletterFound = client
                                    .GetAsync(uri)
                                    .Result
                                    .Content.ReadAsAsync<Newsletter>().Result;
                    return View("Newsletters", newsletterFound);
                }

            }
            return View();
        }


        // Newsletter Post Methods  
        public ActionResult NewSubscription()
        {
            return View();
        }


    }
}
