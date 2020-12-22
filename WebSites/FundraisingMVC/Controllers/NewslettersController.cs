using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;
using GA.BDC.Shared.Entities;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
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
            //if (!string.IsNullOrEmpty(query))
            //{
            //   using (var client = new HttpClient())
            //   {
            //      var uri = new Uri(string.Format("{0}/newsletters/?url={1}", ConfigurationManager.AppSettings["fundraising.webapi.host"], query));
            //      var result = client.GetAsync(uri).Result;
            //      if (result.IsSuccessStatusCode)
            //      {
            //         var newsletter = result.Content.ReadAsAsync<Newsletter>().Result;
            //         return View("Newsletters", newsletter);
            //      }
            //   }

            //}
            return RedirectToActionPermanent("Index", "Home");
        }


      // Newsletter Post Methods  
      public ActionResult NewSubscription()
      {
         return View();
      }


   }
}
