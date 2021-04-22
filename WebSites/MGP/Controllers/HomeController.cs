using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Transactions;
using System.Web;
using System.Web.Mvc.Async;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Web.MGP.Helpers;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Web.MGP.Helpers.Routes.Attributes;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Models.Views;
using Newtonsoft.Json;
using WebMatrix.WebData;
using System.Net;
using System.Text;
using System.IO;


using Google.GData.Client;
using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Extensions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;

namespace GA.BDC.Web.MGP.Controllers
{
   [RenderPartnerBranding]
   public class HomeController : BaseController
   {

      public ActionResult UnitedAirlines()
      {
         var eventId = int.Parse(ConfigurationManager.AppSettings["UnitedAirlines.EventId"]);
         var branding = new PublicEvent();
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
            var eventSummary = @results.Any() ? @results.Single() : null;
            if (eventSummary != null)
            {
               branding.TotalProfit = eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
               branding.TotalNumberOfItemSold = eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
               branding.TotalAmount = eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
               branding.TotalAmountGross = 0M;
               branding.LastActivity = eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
               branding.TotalDonationAmount = eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
            }
         }
         using (var dataProvider = new DataProvider())
         {
            var personalization = (from ep in dataProvider.event_participation
                                   from p in dataProvider.personalizations
                                   where ep.event_participation_id == p.event_participation_id
                                         && ep.participation_channel_id == 3
                                         && ep.event_id == eventId
                                   select p).Single();
            branding.Goal = personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
         }
         return View(branding);
      }
      public ActionResult MBFinancial()
      {
         var eventId = int.Parse(ConfigurationManager.AppSettings["MBFinancial.EventId"]);
         var branding = new PublicEvent();
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
            var eventSummary = @results.Any() ? @results.Single() : null;
            if (eventSummary != null)
            {
               branding.TotalProfit = eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
               branding.TotalNumberOfItemSold = eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
               branding.TotalAmount = eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
               branding.TotalAmountGross = 0M;
               branding.LastActivity = eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
               branding.TotalDonationAmount = eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
            }
         }
         using (var dataProvider = new DataProvider())
         {
            var personalization = (from ep in dataProvider.event_participation
                                   from p in dataProvider.personalizations
                                   where ep.event_participation_id == p.event_participation_id
                                         && ep.participation_channel_id == 3
                                         && ep.event_id == eventId
                                   select p).Single();
            branding.Goal = personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
         }
         return View(branding);
      }

        public ActionResult InVestUsa()
        {
            var eventId = int.Parse(ConfigurationManager.AppSettings["InVestUsa.EventId"]);
            var branding = new PublicEvent();
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
                var eventSummary = @results.Any() ? @results.Single() : null;
                if (eventSummary != null)
                {
                    branding.TotalProfit = eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
                    branding.TotalNumberOfItemSold = eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
                    branding.TotalAmount = eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
                    branding.TotalAmountGross = 0M;
                    branding.LastActivity = eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
                    branding.TotalDonationAmount = eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
                }
            }
            using (var dataProvider = new DataProvider())
            {
                var personalization = (from ep in dataProvider.event_participation
                                       from p in dataProvider.personalizations
                                       where ep.event_participation_id == p.event_participation_id
                                             && ep.participation_channel_id == 3
                                             && ep.event_id == eventId
                                       select p).Single();
                branding.Goal = personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
            }
            return View(branding);
        }


        public ActionResult CharityFundApproach()
      {
         var eventId = int.Parse(ConfigurationManager.AppSettings["CharityFundApproach.EventId"]);
         var branding = new PublicEvent();
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
            var eventSummary = @results.Any() ? @results.Single() : null;
            if (eventSummary != null)
            {
               branding.TotalProfit = eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
               branding.TotalNumberOfItemSold = eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
               branding.TotalAmount = eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
               branding.TotalAmountGross = 0M;
               branding.LastActivity = eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
               branding.TotalDonationAmount = eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
            }
         }
         using (var dataProvider = new DataProvider())
         {
            var personalization = (from ep in dataProvider.event_participation
                                   from p in dataProvider.personalizations
                                   where ep.event_participation_id == p.event_participation_id
                                         && ep.participation_channel_id == 3
                                         && ep.event_id == eventId
                                   select p).Single();
            branding.Goal = personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
         }
         return View(branding);
      }
      public ActionResult AlumniAssociationApproach()
      {
         var eventId = int.Parse(ConfigurationManager.AppSettings["AlumniAssociationApproach.EventId"]);
         var branding = new PublicEvent();
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
            var eventSummary = @results.Any() ? @results.Single() : null;
            if (eventSummary != null)
            {
               branding.TotalProfit = eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
               branding.TotalNumberOfItemSold = eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
               branding.TotalAmount = eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
               branding.TotalAmountGross = 0M;
               branding.LastActivity = eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
               branding.TotalDonationAmount = eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
            }
         }
         using (var dataProvider = new DataProvider())
         {
            var personalization = (from ep in dataProvider.event_participation
                                   from p in dataProvider.personalizations
                                   where ep.event_participation_id == p.event_participation_id
                                         && ep.participation_channel_id == 3
                                         && ep.event_id == eventId
                                   select p).Single();
            branding.Goal = personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
         }
         return View(branding);
      }
      public ActionResult ScholarshipFundApproach()
      {
         var eventId = int.Parse(ConfigurationManager.AppSettings["ScholarshipFundApproach.EventId"]);
         var branding = new PublicEvent();
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
            var eventSummary = @results.Any() ? @results.Single() : null;
            if (eventSummary != null)
            {
               branding.TotalProfit = eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
               branding.TotalNumberOfItemSold = eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
               branding.TotalAmount = eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
               branding.TotalAmountGross = 0M;
               branding.LastActivity = eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
               branding.TotalDonationAmount = eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
            }
         }
         using (var dataProvider = new DataProvider())
         {
            var personalization = (from ep in dataProvider.event_participation
                                   from p in dataProvider.personalizations
                                   where ep.event_participation_id == p.event_participation_id
                                         && ep.participation_channel_id == 3
                                         && ep.event_id == eventId
                                   select p).Single();
            branding.Goal = personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
         }
         return View(branding);
      }
      public ActionResult RaceWalkApproach()
      {
         var eventId = int.Parse(ConfigurationManager.AppSettings["RaceWalkApproach.EventId"]);
         var branding = new PublicEvent();
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
            var eventSummary = @results.Any() ? @results.Single() : null;
            if (eventSummary != null)
            {
               branding.TotalProfit = eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
               branding.TotalNumberOfItemSold = eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
               branding.TotalAmount = eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
               branding.TotalAmountGross = 0M;
               branding.LastActivity = eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
               branding.TotalDonationAmount = eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
            }
         }
         using (var dataProvider = new DataProvider())
         {
            var personalization = (from ep in dataProvider.event_participation
                                   from p in dataProvider.personalizations
                                   where ep.event_participation_id == p.event_participation_id
                                         && ep.participation_channel_id == 3
                                         && ep.event_id == eventId
                                   select p).Single();
            branding.Goal = personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
         }
         return View(branding);
      }
      public ActionResult CysticFibrosisFoundation()
      {
         var eventId = int.Parse(ConfigurationManager.AppSettings["CysticFibrosisFoundation.EventId"]);
         var branding = new PublicEvent();
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
            var eventSummary = @results.Any() ? @results.Single() : null;

            branding.TotalProfit = eventSummary != null && eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
            branding.TotalNumberOfItemSold = eventSummary != null && eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
            branding.TotalAmount = eventSummary != null && eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
            branding.TotalAmountGross = 0M;
            branding.LastActivity = eventSummary != null && eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
            branding.TotalDonationAmount = eventSummary != null && eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
         }
         using (var dataProvider = new DataProvider())
         {
            var personalization = (from ep in dataProvider.event_participation
                                   from p in dataProvider.personalizations
                                   where ep.event_participation_id == p.event_participation_id
                                         && ep.participation_channel_id == 3
                                         && ep.event_id == eventId
                                   select p).FirstOrDefault();
            branding.Goal = personalization != null && personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
         }
         return View(branding);
      }
      public ActionResult RedirectExternal()
      {
         var externalUrls = new Dictionary<string, string> { { "efunds", "http://www.gaoredeem.com" } };
         var found = "efunds";
         var redirect = externalUrls[found];
         //UNDONE: Javi, This should manage the desired URL and map it to an external URL in a dictionary
         return RedirectPermanent(redirect);
      }

      public ActionResult Redirect()
      {
         var accessedPage = string.Empty;
         for (int i = 0; i < Request.Url.Segments.Length; i++)
         {
            if (Request.Url.Segments[i] != "/")
               accessedPage = string.Concat(accessedPage, Request.Url.Segments[i]).ToLower();
         }
         using (var dataProvider = new DataProvider())
         {
            var @map = (from prm in dataProvider.page_route_mapper
                        where prm.file_path_extension == ".aspx"
                           && prm.source_file_path.ToLower() == accessedPage
                        select prm).FirstOrDefault();
            if (@map != null)
            {
               var destinationFilePath = UrlMapHelper.MapPath(Request, @map);
               return Redirect(string.Format("http://{0}/{1}", Request.Url.Authority, destinationFilePath));
            }
         }
         return RedirectToAction("Index");
      }

      public ActionResult AutoCreate()
      {
         return RedirectToActionPermanent("Index");
      }

	   [HttpGet]
	   [Route("registration/step-0")]
	   public ActionResult Step0()
	   {
		   ViewBag.RegisterExternal = TempData["register"] as RegisterExternal;
		   return View("Step0");
	   }

        [HttpPost]
        [Route("registration/step-0")]
        public JsonResult Step1(RegisterExternal register)
        {
				var securityController = new SecurityController();
	        var partner = ViewBag.Partner as Partner;
	        var res = securityController.Register(register, register.ProviderName, register.ProviderUserId, partner) as JsonResult;
	        var data = res?.Data;
	        if (data != null)
	        {
		        var status = data.GetType().GetProperty("success")?.GetValue(data, null);
		        if (status != null && Convert.ToBoolean(status))
		        {
			        var participantId = data.GetType().GetProperty("participantId")?.GetValue(data, null);
			        if (participantId != null && Convert.ToInt32(participantId) > 0)
			        {
				        return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId } };
			        }
		        }
	        }
	        return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false } };
        }

      public ActionResult Index()
      {
         var partner = ViewBag.Partner as Partner;
         if (partner.ProductOffer == ProductOffer.DonationOnly)
         {
            if (partner.DoRedirectToLandingPage && partner.ESubsUrl.IsNotEmpty())
            {
               return Redirect(partner.ESubsUrl);
            }
         }

         //verification
         if (WebSecurity.IsAuthenticated)
         {
            var email = WebSecurity.CurrentUserName.Split('|')[0];
            var partnerId = int.Parse(WebSecurity.CurrentUserName.Split('|')[1]);
            using (var dataProvider = new DataProvider())
            {
               var user = (from u in dataProvider.users
                           where u.email_address == email && u.partner_id == partnerId
                           select new User { Id = u.user_id, FirstName = u.first_name, LastName = u.last_name, Email = u.email_address, Password = u.password }).First();
               ViewBag.User = user;
            }
         }
         return View();
      }

      public ActionResult EventNotFound()
      {
         return View();
      }

	   public ActionResult ExternalLoginError()
	   {
		   return View();
	   }

      public RedirectResult Canada()
      {
         var partnerId = Convert.ToInt32(Session[Constants.SESSION_KEY_PARTNER_ID]);
         var isTestEnvironment = Request.Url.Host.Contains("test.");
         var query = string.Empty;
         using (var dataProvider = new DataProvider())
         {
            var result = (from pcl in dataProvider.partner_culture_link
                          where pcl.partner_id == partnerId
                          select pcl).FirstOrDefault();
            if (result != null)
               query = "?p=" + result.linked_partner_id + "&reset=1";
            else
               query = "?reset=1";

            if (isTestEnvironment)
               return Redirect("http://test.my.fundraising.com/Welcome.aspx" + query + "&devcan=1");
            else
               return Redirect("http://www.efundraisingonline.ca/Welcome.aspx" + query);
         }
      }
      public ActionResult FAQ()
      {
         return View();
      }

        public ActionResult googleButton()
        {


            string clientId = "34406547756-7397auosogs5e7gmm6vjk0mvevhbia6l.apps.googleusercontent.com";
            string redirectUrl = "https://www.efundraising.com/home/test";
            Response.Redirect("https://accounts.google.com/o/oauth2/auth?redirect_uri=" + redirectUrl + "&response_type=code&client_id=" + clientId + "&scope=https://www.googleapis.com/auth/contacts.readonly&approval_prompt=force&access_type=offline");

            return View();
        }



        public ActionResult test()
        {

            string clientId = "125707099774-icekvt6t86k0kin63rbcraq337agmjjd.apps.googleusercontent.com";
            string clientSecret = "7niA4ZhzlcVuAKmhRP_XqkAl";


            string[] scopes = new string[] { "https://www.googleapis.com/auth/contacts.readonly/" };     // view your basic profile info.
            try
            {
                string credPath = @"TEMPLATE\LAYOUTS\Gmail";
                // Use the current Google .net client library to get the Oauth2 stuff.
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret }
                                                                                             , scopes
                                                                                             , "user"
                                                                                             , CancellationToken.None
                                                                                             , new FileDataStore(credPath,true)).Result;

                // Translate the Oauth permissions to something the old client libray can read
                OAuth2Parameters parameters = new OAuth2Parameters();
                parameters.AccessToken = credential.Token.AccessToken;
                parameters.RefreshToken = credential.Token.RefreshToken;
                RunContactsSample(parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return View();
        }

        private static void RunContactsSample(OAuth2Parameters parameters)
        {
            try
            {
                RequestSettings settings = new RequestSettings("Google contacts tutorial", parameters);
                ContactsRequest cr = new ContactsRequest(settings);
                var f = cr.GetContacts();
                foreach (Contact c in f.Entries)
                {
                    Console.WriteLine(c.Name.FullName);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("A Google Apps error occurred.");
                Console.WriteLine();
            }
        }

        public ActionResult HowItWorks()
      {
         return View();
      }
      public ActionResult ContactUs()
      {
         return View();
      }

      [HttpGet]
      public ActionResult FindAGroup()
      {
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var subdivisions = dataProvider.es_get_subdivision("US", "EN");
            var states = new Dictionary<string, string> { { "", "All States" } };
            foreach (var subdivision in subdivisions)
            {
               states.Add(subdivision.subdivision_code, subdivision.subdivision_name);
            }
            ViewBag.States = states;
         }
         return View();
      }
      /// <summary>
      /// Finds the Groups given the filters received
      /// </summary>
      /// <returns>List of Groups found</returns>
      [HttpPost]
      public ContentResult FindAGroup(FindAGroup model)
      {
         if (!ModelState.IsValid)
         {
            var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
            var data = JsonConvert.SerializeObject(
                new
                {
                   success = false,
                   responseText = errorMessage
                });
            return Content(data, "application/json");
         }
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var partnerId = Convert.ToInt32(Session[Constants.SESSION_KEY_PARTNER_ID]);

            var results = dataProvider.es_find_something(model.Name, model.SearchType.Equals("ALL") ? null : model.SearchType, null, "US", string.IsNullOrEmpty(model.State) ? null : model.State, partnerId, model.GroupId).ToList();
            var data = JsonConvert.SerializeObject(
                new
                {
                   groups =
                       results.Select(p => new
                       {
                          EventId = p.event_id,
                          EventName = p.name,
                          EventState = string.IsNullOrEmpty(p.subdivision_code) ? string.Empty : p.subdivision_code.Replace("US-", ""),
                          EventTotalDonationAmount = p.total_donation_amount,
                          // ReSharper disable once PossibleNullReferenceException
                          EventAmount = (ViewBag.Partner as Partner).ProductOffer == ProductOffer.DonationOnly ? p.total_donation_amount : p.total_profit,
                          EventImage = FindEventCoverImage(p.event_participation_id == null ? 0 : (int)p.event_participation_id, p.event_id),
                          EventUrl = p.Type == "G" ? p.participant_count == 1 ? new UrlHelper(Request.RequestContext).Action("FindAParticipant", "Group", new { eventId = p.event_id }) : new UrlHelper(Request.RequestContext).Action("Index", "Group", new { eventId = p.event_id }) : new UrlHelper(Request.RequestContext).Action("Participant", "Group", new { eventId = p.event_id, participantId = p.event_participation_id })
                       }),
                   success = true
                });
            return Content(data, "application/json");
         }
      }
      /// <summary>
      /// Returns the event cover image according
      /// </summary>
      /// <param name="eventParticipationId">Event Participation Id</param>
      /// <param name="eventId">Event Id</param>
      /// <returns></returns>
      private string FindEventCoverImage(int eventParticipationId, int eventId)
      {
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            if (eventParticipationId > 0)
            {
               var personalization = dataProvider.es_get_personalization(eventParticipationId).ToList();
               if (personalization.Any())
               {
                  var personalizationCover =
                      dataProvider.es_get_personalization_coveralbum_image(
                          personalization.First().personalization_id, eventId).ToList();
                  if (personalizationCover.Any() && !string.IsNullOrEmpty(personalizationCover.First().image_url))
                  {
                     return personalizationCover.First().image_url.TransformToMGPImagePath();
                  }
               }
            }
            var personalization2 = dataProvider.es_get_personalization_coveralbum_image(null, eventId).ToList();
            if (personalization2.Any() && !string.IsNullOrEmpty(personalization2.First().image_url))
            {
               return personalization2.First().image_url.TransformToMGPImagePath();
            }
         }
         return string.Concat(ConfigurationManager.AppSettings["Groups.ImagesFolder"], ConfigurationManager.AppSettings["Groups.DefaultParticipantCoverImage"]);
      }
      private void ConvertName(string fullName, out string firstName, out string lastName)
      {
         var name = fullName.Split(' ');
         firstName = lastName = string.Empty;
         var pFirstName = string.Empty;
         var pLastName = string.Empty;
         var isFirstName = true;
         foreach (string splittedName in name)
         {
            if (splittedName.Trim() != string.Empty)
            {
               if (isFirstName)
               {
                  pFirstName = splittedName;
                  isFirstName = false;
               }
               else
               {
                  pLastName = string.Concat(pLastName, " ", splittedName);
               }
            }
         }
         firstName = pFirstName.Trim();
         lastName = pLastName.Trim();
      }
      [HttpGet, RenderLeadBranding, RenderOutputText]
      public ActionResult Register()
      {
         ViewBag.HideMenu = true;
         var partner = ViewBag.Partner as Partner;
         // Check if user is an internal staff member at EFR 
         if (!IsInternalUser(Request.UserHostAddress))
         {
            if (partner.DoRedirectToLandingPage && partner.ESubsUrl.IsNotEmpty())
            {
               return Redirect(partner.ESubsUrl);
            }
         }

         if (partner.ProductOffer != ProductOffer.DonationOnly)
         {
            ViewBag.DisableRenderTextOutput = "true";
         }

         var model = new Registration { GroupType = 1, State = "", Terms = true, Newsletter = true };
         var leadBranding = ViewBag.Lead as Lead;

         if (leadBranding != null)
         {
            // If already registered, redirect to Campaign Manager Home page
            if (leadBranding.EventParticipationID > 0)
               return RedirectToAction("Index", "CampaignManager", new { participantId = leadBranding.EventParticipationID });
            else
            {
               string firstName, lastName;
               ConvertName(leadBranding.Name, out firstName, out lastName);
               model.FirstName = firstName;
               model.LastName = lastName;
               model.Email = leadBranding.Email;
               model.Phone = leadBranding.DayPhone;
               model.State = leadBranding.SubdivisionCode;
               model.CampaignName = leadBranding.GroupName;
            }
         }
         return View(model);
      }

      [HttpGet, RenderPublicEventBranding, RenderTouchBranding]
      public ActionResult ParticipantJoin(int eventId, string username)
      {
         ViewBag.HideMenu = true;
         var model = new JoinParticipant { EventId = eventId, RememberMe = true, Username = username };
         return View(model);
      }

      [RenderTouchBranding, RenderPublicEventBranding, RenderOutputText, HttpGet]
      public ActionResult ParticipantRegister(int eventId)
      {
         ViewBag.HideMenu = true;
         var partner = ViewBag.Partner as Partner;

         if (partner.ProductOffer != ProductOffer.DonationOnly)
         {
            ViewBag.DisableRenderTextOutput = "true";
         }

         var model = new RegistrationParticipant();
         var touchBranding = ViewBag.TouchInvitation as TouchInvitation;

         if (touchBranding != null)
         {
            model.Email = touchBranding.Email;
            model.FirstName = touchBranding.FirstName;
            model.LastName = touchBranding.LastName;
            model.EventId = eventId;
         }
         else
         {
            model.EventId = eventId;
         }
         return View(model);
      }
      public ActionResult Incentives()
      {
            return RedirectToActionPermanent("Index", "Home");
        }

      public ActionResult SignIn(string email, string returnUrl)
      {
         if (returnUrl.IsNotEmpty())
         {
            ViewBag.ReturnUrl = returnUrl;
            if (ControllerContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID) != null)
            {
               int participantId; //
               if (int.TryParse(ControllerContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID).AttemptedValue, out participantId))
               {
                  using (var dataProvider = new DataProvider())
                  {
                     var member = (from ep in dataProvider.event_participation
                                   from mh in dataProvider.member_hierarchy
                                   from m in dataProvider.members
                                   where ep.event_participation_id == participantId
                                      && ep.member_hierarchy_id == mh.member_hierarchy_id
                                      && mh.member_id == m.member_id
                                   select m).SingleOrDefault();
                     if (member != null && member.user_id == null)
                     {
                        using (var data = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
                        {
                           data.es_validate_login(member.email_address, member.password, member.partner_id);
                        }
                     }
                     var user = (from ep in dataProvider.event_participation
                                 from mh in dataProvider.member_hierarchy
                                 from m in dataProvider.members
                                 from u in dataProvider.users
                                 where ep.event_participation_id == participantId
                                    && ep.member_hierarchy_id == mh.member_hierarchy_id
                                    && mh.member_id == m.member_id
                                    && m.user_id == u.user_id
                                 select u).SingleOrDefault();

                     // Check if user is an internal staff member at EFR 
                     if (IsInternalUser(Request.UserHostAddress))
                     {
                        WebSecurity.Login(string.Concat(user.username, "|", user.partner_id), user.password);
                        return Redirect(returnUrl);
                     }
                     if (user != null)
                        ViewBag.Email = user.username;
                     return View();
                  }
               }
            }
         }
         if (email.IsNotEmpty())
            ViewBag.Email = email;
         return View();
      }
      /// <summary>
      /// Returns true only for staff members located strickly inside the office space & not for those accessing remotely
      /// </summary>
      /// <param name="userHostAddress">Request user's Host Address</param>
      /// <returns>bool</returns>
      private bool IsInternalUser(string userHostAddress)
      {
         var networkPrefix = ConfigurationManager.AppSettings["InternalNetworkPrefix"].Split('.');
         var currentUser = userHostAddress.Split('.');
         for (int i = 0; i <= 3; i++)
         {
            if ((networkPrefix[i] != currentUser[i]) && (networkPrefix[i] != "0".ToString()))
            {
               return false;
            }
         }
         return true;
      }
      public ActionResult SiteMap()
      {
         return View();
      }
      public ActionResult PrivacyPolicy()
      {
         return View();
      }

      public ActionResult LoginExternalFailed()
      {
         return View();
      }

      public ActionResult CampaignClosed()
      {
         return View();
      }

      public ActionResult Unsubscribe()
      {
         var touchId = Session[Constants.SESSION_KEY_TOUCH_ID] != null
                         ? Convert.ToInt32(Session[Constants.SESSION_KEY_TOUCH_ID])
                         : 0;
         if (touchId == 0)
            return RedirectToAction("Index", "Home");
         using (var dataProvider = new DataProvider())
         {
            var @results = (from m in dataProvider.members
                            from mh in dataProvider.member_hierarchy
                            from ep in dataProvider.event_participation
                            from t in dataProvider.touches
                            from e in dataProvider.events
                            where m.member_id == mh.member_id
                               && mh.member_hierarchy_id == ep.member_hierarchy_id
                               && ep.event_participation_id == t.event_participation_id
                               && t.touch_id == touchId
                               && ep.event_id == e.event_id
                               && mh.active
                               && e.active
                            orderby e.event_name
                            select new
                            {
                               MemberId = m.member_id,
                               MemberHierarchyId = mh.member_hierarchy_id,
                               EventId = e.event_id,
                               MemberUnsubscribe = m.unsubscribe.HasValue ? m.unsubscribe.Value : false,
                               MemberHierarchyUnsubscribe = mh.unsubscribe,
                               CampaignName = e.event_name
                            }).ToList();
            if (@results.Any())
            {
               var campaigns = new Unsubscribe { MemberId = @results[0].MemberId, MemberSubscribed = !@results[0].MemberUnsubscribe };
               campaigns.Campaigns = @results.Select(p => new CampaignInfo
               {
                  CampaignName = p.CampaignName,
                  EventId = p.EventId,
                  MemberHierarchyId = p.MemberHierarchyId,
                  MemberHierarchySubscribed = !p.MemberHierarchyUnsubscribe
               }).ToArray();
               return View(campaigns);
            }
            else
               return RedirectToAction("Index", "Home");
         }
      }
      [HttpPost]
      public ActionResult Unsubscribe(Unsubscribe model)
      {
         if (!ModelState.IsValid)
         {
            var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
         }
         using (var dataProvider = new DataProvider())
         {
            using (var transactionScope = new TransactionScope())
            {
               var member = dataProvider.members.Single(p => p.member_id == model.MemberId);
               member.unsubscribe = !model.MemberSubscribed;
               member.unsubscribe_date = DateTime.Now;
               if (member.user_id.HasValue)
               {
                  var user = dataProvider.users.Single(p => p.user_id == member.user_id);
                  user.unsubscribe = !model.MemberSubscribed;
                  user.unsubscribe_date = DateTime.Now;
               }
               if (model.SelectedCampaigns != null)
               {
                  foreach (var campaign in model.SelectedCampaigns)
                  {
                     var memberHierarchy = dataProvider.member_hierarchy.First(p => p.member_hierarchy_id == campaign.MemberHierarchyId);
                     memberHierarchy.unsubscribe = !model.Campaigns.Single(p => p.MemberHierarchyId == campaign.MemberHierarchyId
                                                                             && p.EventId == campaign.EventId).MemberHierarchySubscribed;
                     memberHierarchy.unsubscribe_date = DateTime.Now;
                  }
               }
               dataProvider.SaveChanges();
               transactionScope.Complete();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Your subscription has been successfully updated." } };
         }
      }

      public ActionResult Landing(int groupId = 0)
      {
         var partner = ViewBag.Partner as Partner;
         var partnerId = partner.Id;
         ViewBag.GroupId = groupId;
         using (var dataProvider = new DataProvider())
         {
            var landingPage = (from n in dataProvider.landingPages
                               where n.partner_id == partnerId
                               select new LandingPage
                               {
                                  PartnerId = n.partner_id,
                                  GroupId = n.group_id,
                                  Description = n.description,
                                  SideBarImageUrl = n.side_bar_image,
                                  DonateImageUrl = n.donate_image,
                                  RaiseFundsImageUrl = n.raise_funds_image,
                                  Goal = n.goal,
                                  ShowCreateFundraising = n.show_create_fundraising,
                                  ShowFeaturedGroups = n.show_featured_groups,
                                  ShowFindGroups = n.show_find_groups,
                                  ShowTopParticipants = n.show_top_participants,
                                  ShowImageMotivator = n.show_image_motivator,
                                  LearnMoreText = n.learn_more_text,
                                  ApplyPartnerPercentOnTotalAmount = n.apply_partner_percent_on_total_amount,
                                  GroupDisplay = partner.GroupDisplay
                               }).FirstOrDefault();
            if (landingPage == null)
            {
               return RedirectToActionPermanent("404", "Error");
            }
            var @result = (from eta in dataProvider.event_total_amount
                           from e in dataProvider.events
                           from eg in dataProvider.event_group
                           from g in dataProvider.groups
                           where eta.event_id == e.event_id
                              && e.event_id == eg.event_id
                              && eg.group_id == g.group_id
                              && g.partner_id == partnerId
                              && (groupId == 0 || g.group_id == groupId)
                           select new
                           {
                              TotalRaised = partner.ProductOffer == ProductOffer.DonationOnly ? eta.total_donation_amount : eta.total_amount,
                              TotalNumberOfSupporters = partner.ProductOffer == ProductOffer.DonationOnly ? eta.total_donars : eta.total_supporters
                           }).ToList();
            var defaultEventId = (from e in dataProvider.events
                                  from eg in dataProvider.event_group
                                  where eg.group_id == landingPage.GroupId
                                     && e.event_id == eg.event_id
                                     && e.active
                                  select e.event_id).FirstOrDefault();
            var supportBox = new SupportBox
            {
               Goal = landingPage.Goal,
               TotalRaised = @result.Sum(p => p.TotalRaised),
               TotalNumberOfSupporters = @result.Sum(p => p.TotalNumberOfSupporters),
               ShowImageMotivator = landingPage.ShowImageMotivator,
               ApplyPartnerPercentOnTotalAmount = landingPage.ApplyPartnerPercentOnTotalAmount,
               LearnMoreText = !string.IsNullOrEmpty(landingPage.LearnMoreText) ? landingPage.LearnMoreText : null,
               DefaultEventId = defaultEventId
            };
            supportBox.TotalRaised = supportBox.TotalRaised * (landingPage.ApplyPartnerPercentOnTotalAmount ? Convert.ToDecimal(partner.ProfitPercentage / 100) : 1);
            if (partner.ProductOffer == ProductOffer.DonationOnly)
               supportBox.DonarComments = FindDonarComments(partnerId);
            ViewBag.SupportBox = supportBox;
            if (landingPage.ShowTopParticipants)
            {
               var topParticipants = new List<TopParticipant>();
               using (var esubsDataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
               {
                  var results = esubsDataProvider.es_get_top3_participant_total_amount_by_partner_id(partnerId, 3, groupId > 0 ? (int?)groupId : null).ToList();
                  if (results.Any())
                  {
                     foreach (var entry in results)
                     {
                        var participant = new TopParticipant
                        {
                           Id = entry.event_participation_id,
                           Name = entry.participant_name,
                           AmountRaised = 
                              (partner.ProductOffer == ProductOffer.DonationOnly
                                 ? entry.total_donation_amount
                                 : entry.total_amount) * (landingPage.ApplyPartnerPercentOnTotalAmount ? Convert.ToDecimal(partner.ProfitPercentage / 100) : 1),
                           EventId = entry.event_id,
                           EventName = entry.event_name,
                           Goal = entry.goal,
                           Image = !string.IsNullOrEmpty(entry.image_url) ? entry.image_url.TransformToMGPImagePath() : string.Concat(ConfigurationManager.AppSettings["Groups.ImagesFolder"], ConfigurationManager.AppSettings["Groups.DefaultParticipantCoverImage"])
                        };
                        topParticipants.Add(participant);
                     }
                  }
               }
               ViewBag.TopParticipants = topParticipants; ;
            }

            if (landingPage.ShowFeaturedGroups)
            {
               var topEvents = new List<FeaturedGroup>();
               using (var esubsDataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
               {
                  var results = esubsDataProvider.es_get_top3_event_total_amount_by_partner_id(partnerId, 3, groupId > 0 ? (int?)groupId : null).ToList();
                  if (results.Any())
                  {
                     foreach (var entry in results)
                     {
                        var fg = new FeaturedGroup
                        {
                           AmountRaised = (partner.ProductOffer == ProductOffer.DonationOnly ? entry.total_donation_amount : entry.total_amount) * (landingPage.ApplyPartnerPercentOnTotalAmount ? Convert.ToDecimal(partner.ProfitPercentage / 100) : 1),
                           Id = entry.event_id,
                           Name = entry.event_name,
                           Image = !string.IsNullOrEmpty(entry.image_url) ? entry.image_url.TransformToMGPImagePath() : string.Concat(ConfigurationManager.AppSettings["Groups.ImagesFolder"], ConfigurationManager.AppSettings["Groups.DefaultGroupCoverImage"])
                        };
                        topEvents.Add(fg);
                     }
                  }
               }
               ViewBag.TopEvents = topEvents;
            }
            return View(landingPage);
         }
      }
      /// <summary>
      /// Returns a list of comments made to a donation partner
      /// </summary>
      /// <param name="partnerId">Partner Id</param>
      /// <returns></returns>
      private List<Models.Views.Comments> FindDonarComments(int partnerId)
      {
         List<Models.Views.Comments> comments = null;
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            if (partnerId > 0)
            {
               var results = dataProvider.es_get_donor_comments_by_partner_id(partnerId).ToList();
               if (results.Any())
               {
                  comments = results.Select(p => new Models.Views.Comments
                  {
                     EventParticipationID = p.event_participation_id,
                     EventID = p.event_id,
                     MemberHierarchyID = p.member_hierarchy_id,
                     Name = p.donor_name,
                     Amount = p.donation_amount.HasValue ? (decimal)p.donation_amount : 0M,
                     Comment = p.donor_comments
                  }).ToList();
               }
            }
         }
         return comments;
         //fixed for the merge
      }
      public ActionResult Random()
      {
         int eventId;
         using (var dataProvider = new DataProvider())
         {
            var randomActiveEvents = (from e in dataProvider.events
                                      where e.active
                                      select e.event_id).ToList();
            var random = new Random();
            eventId = randomActiveEvents[random.Next(randomActiveEvents.Count) - 1];
         }
         return RedirectToAction("Index", "Group", new { eventId });
      }
      [RenderPublicEventBranding]
      public ActionResult PublicGroup(int eventId)
      {
         var partnerBranding = ViewBag.Partner as GA.BDC.Web.MGP.Models.Branding.Partner;
         partnerBranding.HideMainMenu = true;
         return View();
      }

      public ActionResult Restaurant()
      {
         return RedirectPermanent("http://restaurant.efundraising.com");
      }

      public FileResult TouchEmailOpenTracking(int touchId)
      {
         using (var dc = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            //UrlParameters.Decrypt(emailParameter)
            dc.es_touch_add_action(touchId, 108);
         }
         return File("~/Content/images/pixel.gif", "image/gif");
      }

      public ActionResult Groups(int partner_id)
      {
         var result = new List<GroupViewModel>();
         
         using (var dataProvider = new DataProvider())
         {
            var groups = dataProvider.groups.Where(p => p.partner_id == partner_id && p.event_group.Any(x => x._event.active)).ToList();
            if (!groups.Any())
            {
               return View("GroupNotFound");
            }
            foreach (var @group in groups)
            {
               dataProvider.Database.Connection.Open();
               var command = dataProvider.Database.Connection.CreateCommand();
               command.CommandText = $"EXEC sp_group_cardbox {@group.group_id}";
               var cardboxResult = command.ExecuteReader(CommandBehavior.SingleRow);
               cardboxResult.Read();
                  var model = new GroupViewModel
                  {
                     Id = @group.group_id,
                     Name = @group.group_name,
                     AmountRaised = double.Parse(cardboxResult[1].ToString()),
                     Goal = double.Parse(cardboxResult[0].ToString()),
                     NumberOfMembers = double.Parse(cardboxResult[2].ToString()),
                     EventDate = @group.event_date,
                     Image = $"/dumpTemp/groups/{@group.image}"
                  };
               
               dataProvider.Database.Connection.Close();
               var events =dataProvider.event_group.Where(p => p.group_id == @group.group_id && p._event.active).ToList();
               model.TotalEvents = events.Count;
               model.EventId = events.Count > 1
                  ? 0
                  : events.First().event_id;
               result.Add(model);
            }
            var totalamounts = (from eta in dataProvider.event_total_amount
                                from e in dataProvider.events
                                from eg in dataProvider.event_group
                                from g in dataProvider.groups
                                where eta.event_id == e.event_id
                                   && e.event_id == eg.event_id
                                   && eg.group_id == g.group_id
                                   && g.partner_id == partner_id
                                select new
                                {
                                   TotalRaised = eta.total_amount,
                                }).ToList();
            var landingPage = (from n in dataProvider.landingPages
                               where n.partner_id == partner_id
                               select new LandingPage
                               {
                                  PartnerId = n.partner_id,
                                  GroupId = n.group_id,
                                  Description = n.description,
                                  SideBarImageUrl = n.side_bar_image,
                                  ImageTopUrl = n.image_top,
                                  DonateImageUrl = n.donate_image,
                                  RaiseFundsImageUrl = n.raise_funds_image,
                                  Goal = n.goal,
                                  ShowCreateFundraising = n.show_create_fundraising,
                                  ShowFeaturedGroups = n.show_featured_groups,
                                  ShowFindGroups = n.show_find_groups,
                                  ShowTopParticipants = n.show_top_participants,
                                  ShowImageMotivator = n.show_image_motivator,
                                  LearnMoreText = n.learn_more_text,
                                  ApplyPartnerPercentOnTotalAmount = n.apply_partner_percent_on_total_amount,                                  
                               }).FirstOrDefault();
            landingPage.AmountRaised = (double) totalamounts.Sum(p => p.TotalRaised);
            ViewBag.LandingPage = landingPage;
         }
         var controllerDescriptor = new ReflectedControllerDescriptor(GetType());
         var actionDescriptor = controllerDescriptor.FindAction(ControllerContext, "Groups");
         var context = new ActionExecutingContext(ControllerContext, new ReflectedActionDescriptor((actionDescriptor as ReflectedActionDescriptor).MethodInfo, "Groups", controllerDescriptor), new Dictionary<string, object>());
         context.HttpContext.Session.Add(Constants.SESSION_KEY_PARTNER_ID, partner_id);
         var renderPartnerBranding = new RenderPartnerBrandingAttribute();
         renderPartnerBranding.OnActionExecuting(context);
         return View("Groups", result);
      }

        private class GooglePlusAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }

        }
    }




}
