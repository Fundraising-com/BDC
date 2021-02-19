using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using GA.BDC.Shared.Entities;
using MvcSiteMapProvider;
using Ninject;
using Ninject.Extensions.Logging;
using System.Net;

namespace GA.BDC.Web.Fundraising.MVC.Controllers
{


    public class CheckModel
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public bool Checked
        {
            get;
            set;
        }
        public string Image
        {
            get;
            set;
        }
    }

    public class LeadID
    {
        public int LeadId
        {
            get;
            set;
        }
        
    }





    public class HomeController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }
        public IEnumerable<object> YourCheckBoxes { get; private set; }

        public HomeController()
        {
        }

        [AllowAnonymous]
        public ActionResult Index()
        {

            ViewBag.HideBreadcrumbs = true;
            return View();
        }




        [MvcSiteMapNode(Title = "Request a Kit", ParentKey = "Root", Protocol = "https"), Route("request-a-kit")]
        public ActionResult KitRequest()
        {
           return View();
        }

        [MvcSiteMapNode(Title = "Request a Kit Canada", ParentKey = "Root", Protocol = "https"), Route("request-a-kit-canada")]
        public ActionResult KitRequestCanada()
        {

            return View();
        }

        [MvcSiteMapNode(Title = "Request a Kit Canada Pop Up", ParentKey = "Root", Protocol = "https"), Route("request-a-kit-canada-pop-up")]
        public ActionResult KitRequestCanadaPopUp()
        {

            return RedirectToAction("KitRequestCanada");
        }

        [MvcSiteMapNode(Title = "Request a Kit Pop Up", ParentKey = "Root", Protocol = "https"), Route("request-a-kit-Pop_Up")]
        public ActionResult KitRequestPopUp()
        {
            return View("KitRequest");
        }

        [MvcSiteMapNode(Title = "Search", ParentKey = "Root", Protocol = "https"), Route("search")]
        public ActionResult Search()
        {
            return View();
        }



        [Route("kit-request-confirmation")]
        public ActionResult KitRequestConfirmation(string c, int partnerId)
        {


            // if (System.IO.File.Exists(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeImage)))
            if (c == "CA")
            {
                ViewBag.partnerkit = "../Content/external/partners/pdf/partnerkit/kitCan.pdf";
            }
            else
            {

                if (System.IO.File.Exists(Server.MapPath("/Content/external/partners/pdf/partnerkit/" + partnerId + ".pdf")))
                {
                    ViewBag.partnerkit = "../Content/external/partners/pdf/partnerkit/" + partnerId + ".pdf";
                }
                else
                {
                    ViewBag.partnerkit = "../Content/external/partners/pdf/partnerkit/686.pdf";
                }
            }
            return View();
        }

        [MvcSiteMapNode(Title = "Ordertaker Confirmation", ParentKey = "Root", Protocol = "https"), Route("ordertaker-confirmation")]
        public ActionResult OrdertakerConfirmation()
        {

            return View();
        }



  

        [Route("kit-request-confirmation-canada")]
        public ActionResult KitRequestConfirmationCanada(string c, int partnerId)
        {


            // if (System.IO.File.Exists(Server.MapPath("/Content/external/rep/SmallRepImage/" + representativeImage)))
            if (c == "CA")
            {
                ViewBag.partnerkit = "../Content/external/partners/pdf/partnerkit/kitCan.pdf";
            }
            else
            {

                if (System.IO.File.Exists(Server.MapPath("/Content/external/partners/pdf/partnerkit/" + partnerId + ".pdf")))
                {
                    ViewBag.partnerkit = "../Content/external/partners/pdf/partnerkit/" + partnerId + ".pdf";
                }
                else
                {
                    ViewBag.partnerkit = "../Content/external/partners/pdf/partnerkit/686.pdf";
                }
            }
            return View();
        }


        [Route("kit-request-confirmation-pop-up-Canada")]
        public ActionResult KitRequestPopUpConfirmationCanada(string c, int partnerId)
        {
            return KitRequestConfirmationCanada(c, partnerId);

        }


        [Route("kit-request-confirmation-pop-up")]
        public ActionResult KitRequestPopUpConfirmation(string c, int partnerId)
        {
            return KitRequestConfirmation(c, partnerId);
            
        }


        [MvcSiteMapNode(Title = "sports fundraising ideas", ParentKey = "Home", Protocol = "https"), Route("sports-fundraising-ideas")]
        public ActionResult sportsfundraisingideas()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "school fundraising ideas", ParentKey = "Home", Protocol = "https"), Route("school-fundraiser-ideas")]
        public ActionResult schoolfundraisingideas()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "non-profit fundraising ideas", ParentKey = "Home", Protocol = "https"), Route("nonprofit-fundraising-ideas")]
        public ActionResult nonprofitfundraisingideas()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Katydids fundraising ideas", ParentKey = "Home", Protocol = "https"), Route("katydids-fundraising-ideas")]
        public ActionResult katydidsfundraisingideas()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Best Sellers fundraising ideas", ParentKey = "Home", Protocol = "https"), Route("best-sellers-fundraising-ideas")]
        public ActionResult bestsellersfundraisingideas()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Buy Online fundraising ideas", ParentKey = "Home", Protocol = "https"), Route("buy-online")]
        public ActionResult buyonline()
        {
            return View();
        }



        public ActionResult Redirect()
        {
            var requestedPage = string.Join("", Request.Url.Segments).Substring(1);
            try
            {
                // 1. Check if it's a Rep Portal Call
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format("{0}/representatives?redirect={1}", ConfigurationManager.AppSettings["core.webapi.host"], requestedPage));
                    var representative = client
                                .GetAsync(uri)
                                .Result
                                .Content.ReadAsAsync<Representative>().Result;
                    if (representative != null)
                    {
                        return RedirectToActionPermanent("Index", "Representatives", new RouteValueDictionary { { "id", representative.Id } });
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.InfoException("Representative redirection not found", exception);
            }
            try
            {
                //2. Check if it's an obsolete Url call
                using (var client = new HttpClient())
                {
                    var uri = new Uri(string.Format("{0}/routesmappers?url={1}", ConfigurationManager.AppSettings["fundraising.webapi.host"], requestedPage));
                    var redirect = client
                                .GetAsync(uri)
                                .Result
                                .Content.ReadAsAsync<string>().Result;
                    if (!string.IsNullOrEmpty(redirect))
                    {
                        return RedirectPermanent(redirect + Request.Url.Query);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.InfoException("Page Route Mapper redirection not found", exception);
            }

            Logger.Warn(string.Format("Redirection not found. Uri: {0}.", requestedPage));
            
            //3. Nothing found, throw 404
            return HttpNotFound();
        }
    }
}