using System.Web.Mvc;
using System;
using System.Net.Http;
using System.Configuration;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;
using System.Collections.Generic;
using GA.BDC.Shared.Entities;
using System.Web;
using System.Text;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{
    [RoutePrefix("ideas"), AllowAnonymous]
    public class IdeasController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }

        [MvcSiteMapNode(Title = "Healthy Fundraisers", ParentKey = "Ideas", Protocol = "https"), Route("healthy-fundraisers")]
        public ActionResult HealthyFundraisers()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Tips", ParentKey = "Ideas", Protocol = "https"), Route("tips")]
        public ActionResult Tips()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Events", ParentKey = "Ideas", Protocol = "https"), Route("events")]
        public ActionResult Events()
        {
            return View();
        }
        [Route("newsletters")]
        public ActionResult News()
        {
                 
            //using (var client = new HttpClient())
            //{
            //    var uri = new Uri($"{ConfigurationManager.AppSettings["fundraising.webapi.host"]}/newsletters/");
            //    var newsletterFound = client
            //                    .GetAsync(uri)
            //                    .Result
            //                    .Content.ReadAsAsync<IEnumerable<Newsletter>>().Result;
            //        return View("News", newsletterFound);
            //}
            return RedirectToActionPermanent("Index", "Home");

        }
        [MvcSiteMapNode(Title = "Unique Fundraisers", ParentKey = "Ideas", Protocol = "https"), Route("unique-fundraisers")]
        public ActionResult UniqueFundraisers()
        {
            return View();

        }
        [MvcSiteMapNode(Title = "Easy Fundraising Ideas", ParentKey = "Ideas", Protocol = "https"), Route("fundraising-idea")]
        public ActionResult Ideas2020()
        {
            return View();
        }

        [Route("fundraising-ideas")]
        public ActionResult Ideas()
        {
            return RedirectToActionPermanent("Ideas2020", "Ideas");
        }

        [MvcSiteMapNode(Title = "Easy Fundraisers", ParentKey = "Ideas", Protocol = "https"), Route("easy-fundraisers")]
        public ActionResult EasyFundraisers()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Non-Profit Fundraising", ParentKey = "Ideas", Protocol = "https"), Route("non-profit-fundraising")]
        public ActionResult NonProfitFundraising()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "A to Z of Fundraising Ideas", ParentKey = "Ideas", Protocol = "https"), Route("A-to-Z-fundraising-ideas")]
        // ReSharper disable once InconsistentNaming
        public ActionResult FundraisingIdeasAZ()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "105 Fundraising Ideas", ParentKey = "Ideas", Protocol = "https"), Route("105-fundraising-ideas")]
        public ActionResult FundraisingIdeas105()
        {
            return View();
        }


        public ActionResult Test()
        {

            //string clientId = "535131743146-i0hrm21v2lmnifr5kus0ss5tl5qctb9d.apps.googleusercontent.com";
            //string redirectUrl = "http://local.fundraising.com/ideas/text";
            //Response.Redirect("https://accounts.google.com/o/oauth2/auth?redirect_uri=" + redirectUrl + "&response_type=code&client_id=" + clientId + "&scope=https://www.google.com/m8/feeds/&approval_prompt=force&access_type=offline");





            // ServerXMLHTTP emailProcessing = new ServerXMLHTTP();
            // //var strJSONToSend2 = "{ \"Type\":\"" + 19 +
            // //          "\",\"ExternalId\":\"" + 1210917 +
            // //               "\"}";


            //     var strJSONToSend2 = "{ \"Type\":\"" + 17 +
            //                 "\",\"ExternalId\":\"" + 73329 +
            //                 "\",\"Email\":\"fake@fake.com" +
            //                 "\",\"ExtraData\":\"1|" + "UI" + "|" + 1100222 + "|" + 173205 +
            //                 "\"}";


            //     var serverNotificationUrl = ConfigurationManager.AppSettings["core.webapi.host"];
            //var postDataBytes2 = Encoding.Default.GetBytes(strJSONToSend2);

            // emailProcessing.open("POST", serverNotificationUrl);
            //             emailProcessing.setRequestHeader("Content-Type", "application/json");
            //             emailProcessing.setRequestHeader("Content-Length", postDataBytes2.Length.ToString());
            //             emailProcessing.send(postDataBytes2);

            return View();
    }

}
}