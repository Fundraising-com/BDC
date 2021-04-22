using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Transactions;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Shared.Entities;
using GA.BDC.Web.MGP.Helpers;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Web.MGP.Helpers.Routes.Attributes;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Models.Views;
using GA.BDC.Web.MGP.Properties;
using Newtonsoft.Json;
using WebMatrix.WebData;
using IsolationLevel = System.Data.IsolationLevel;
using Partner = GA.BDC.Web.MGP.Models.Branding.Partner;
using GA.Core.Shared.Contracts;
using GA.Store.Shared;
using GA.Store.Shared.Contracts;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Net.Http.Headers;
using System.Text;
using System.Data;
using Sentry;
using Sentry.EntityFramework;

namespace GA.BDC.Web.MGP.Controllers
{
   [RenderPartnerBranding, RenderPublicEventBranding]
   public class GroupController : BaseController
   {
        private IDisposable _sentry;

        


        public ActionResult Index(int eventId = 0, int participantId = 0, bool isPreview = false)
      {

            //sentry error handling
            SentryDatabaseLogging.UseBreadcrumbs();
            _sentry = SentrySdk.Init(o =>
            {
                // We store the DSN inside Web.config; make sure to use your own DSN!
                o.Dsn = new Dsn(ConfigurationManager.AppSettings["SentryDSN"]);

                // Get Entity Framework integration
                o.AddEntityFramework();
                o.SendDefaultPii = true;

            });


            if (eventId == 0)
         {
            return RedirectToActionPermanent("EventNotFound", "Home");
         }
         if (participantId > 0)
         {
            using (var dataProvider = new DataProvider())
            {
               var memberTypeId = (from ep in dataProvider.event_participation
                                   from mh in dataProvider.member_hierarchy
                                   from cc in dataProvider.creation_channel
                                   where ep.event_participation_id == participantId
                                         && ep.member_hierarchy_id == mh.member_hierarchy_id
                                         && mh.creation_channel_id == cc.creation_channel_id
                                   select cc.member_type_id).FirstOrDefault();
               if (memberTypeId == 2)
               {
                  return Redirect("Participant?" + Request.QueryString);
               }
               else
               {
                  return Redirect("Index?eventId=" + eventId);
               }
            }
         }

         var carouselImages = new List<Caroussel>();
         var sponsorDefaultImg = string.Format("{0}/{1}",
            Settings.Default.PersonalizationImageDirectory,
            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);
         var partner = ViewBag.Partner as Partner;
         var eventBranding = ViewBag.PublicEvent as Models.Branding.PublicEvent;

         using (
            var dataProvider =
               new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var images = dataProvider.es_get_personalization_images_by_event_id(eventId).ToList();
            carouselImages.AddRange(
               images.Select(x => new Caroussel
               {
                  EventId = x.event_id,
                  ParticipantId = x.event_participation_id,
                  AnchorUrl =
                     x.event_participation_id > 0
                        ? new UrlHelper(Request.RequestContext).Action("Participant", "Group",
                           new { eventId, participantId = x.event_participation_id }).UrlEncode()
                        : "#",
                  AlternativeText = string.Concat(x.first_name, " ", x.last_name).Trim(),
                  ImageUrl = x.image_url.TransformToMGPImagePath(),
                  IsCoverAlbum = x.isCoverAlbum,
                  RedirectToStore = x.event_participation_id <= 0
               })
            );
         }
         if (!carouselImages.Any(p => p.IsCoverAlbum))
         {
            var firstImage = carouselImages.FirstOrDefault();
            if (firstImage != null)
            {
               firstImage.IsCoverAlbum = true;
            }
         }

         var groupId = 0;
         using (var dataProvider = new DataProvider())
         {
            ViewBag.Caroussel = carouselImages.OrderByDescending(x => x.IsCoverAlbum).ToList();
            if (!carouselImages.Any())
            {
               carouselImages.Add(new Caroussel()
               {
                  EventId = eventId,
                  IsCoverAlbum = true,
                  ImageUrl = sponsorDefaultImg,
                  AnchorUrl = string.Empty
               });
            }
            if (!carouselImages.Any(p => p.IsCoverAlbum))
            {
               carouselImages.First().IsCoverAlbum = true;
            }
            eventBranding.CoverImage = carouselImages.First(p => p.IsCoverAlbum).ImageUrl;
            ViewBag.CarousselCount = carouselImages.Count();

            ViewBag.ParticipantsRotator = (from pta in dataProvider.participant_total_amount
                                           from ep in dataProvider.event_participation
                                           from mh in dataProvider.member_hierarchy
                                           from m in dataProvider.members
                                           where pta.event_participation_id == ep.event_participation_id && ep.event_id == eventId
                                                 && ep.member_hierarchy_id == mh.member_hierarchy_id
                                                 && mh.member_id == m.member_id
                                                 && !m.deleted
                                           select
                                           new ParticipantRotator
                                           {
                                              AmountRaised = (double)pta.total_amount,
                                              Id = ep.event_participation_id,
                                              Name = pta.participant_name
                                           }).ToList();
            ViewBag.HasParticipants = (from e in dataProvider.events
                                       from ep in dataProvider.event_participation
                                       from mh in dataProvider.member_hierarchy
                                       from m in dataProvider.members
                                       where e.event_id == ep.event_id
                                             && e.event_id == eventId
                                             && ep.member_hierarchy_id == mh.member_hierarchy_id
                                             && mh.member_id == m.member_id
                                             && !m.deleted
                                       select ep).Count() > 1;
            groupId = dataProvider.event_group.Single(e => e.event_id == eventId).group_id;
         }
         ViewBag.PopularItems = CreatePopularItems(eventId, 0);

         ViewBag.AmountRaisedIndicator = new AmountRaisedIndicator
         {
            Goal = eventBranding.Goal,
            IsGroupPageView = true,
            ThermometerPercentage =
               eventBranding.Goal > 0M
                  ? (int)(eventBranding.TotalAmount / eventBranding.Goal * 100.0M)
                  : eventBranding.TotalAmount > 0M ? 100 : 0,
            HideJOIN = partner.EventsToHideJOINButton.IsEmpty()
               ? false
               : partner.EventsToHideJOINButton.Contains(groupId.ToString())
         };
         ViewBag.IsPreview = isPreview;
	      ViewBag.IsParticipant = false;
         return View();
      }

      public ActionResult Participant(int eventId, int participantId = 0)
      {
         if (participantId == 0)
         {
            return RedirectToActionPermanent("Index", new { eventId });
         }
         var participantPage = new ParticipantPage();
         using (var dataProvider = new DataProvider())
         {
            participantPage = (from ep in dataProvider.event_participation
                               from mh in dataProvider.member_hierarchy
                               from m in dataProvider.members
                               from u in dataProvider.users
                               where ep.member_hierarchy_id == mh.member_hierarchy_id
                                     && mh.member_id == m.member_id
                                     && m.user_id == u.user_id
                                     && ep.event_participation_id == participantId
                                     && !m.deleted
                               join perso in dataProvider.personalizations
                               on ep.event_participation_id equals perso.event_participation_id
                               into t
                               from p in t.DefaultIfEmpty()
                               select new ParticipantPage
                               {
                                  Id = ep.event_participation_id,
                                  PersonalizationId = p != null ? p.personalization_id : -1,
                                  FirstName = u.first_name,
                                  LastName = u.last_name,
                                  Message = p != null ? p.body : string.Empty
                               }).FirstOrDefault();
            if (participantPage == null)
            {
               return RedirectToActionPermanent("Index", new { eventId });
            }
            participantPage.GroupMessage = (from e in dataProvider.events
                                            from ep in dataProvider.event_participation
                                            from p in dataProvider.personalizations
                                            where e.event_id == ep.event_id
                                                  && ep.event_participation_id == p.event_participation_id
                                                  && e.event_id == eventId
                                                  && ep.participation_channel_id == 3
                                            select p.body).FirstOrDefault();
            var persoImgs = dataProvider.personalization_image.Where(
               p =>
                  p.personalization_id == participantPage.PersonalizationId && !p.deleted &&
                  p.image_approval_status_id != 4);
            var carouselImages = persoImgs.Any()
               ? persoImgs.Select(p => new Caroussel
               {
                  EventId = eventId,
                  ParticipantId = participantId,
                  AnchorUrl = "#",
                  AlternativeText = string.Concat(participantPage.FirstName, "|", participantPage.LastName).Trim(),
                  ImageUrl = p.image_url,
                  RedirectToStore = true
               }).ToList()
               : (new List<Caroussel>
               {
                  new Caroussel
                  {
                     EventId = eventId,
                     ParticipantId = participantId,
                     AnchorUrl = "#",
                     AlternativeText = string.Concat(participantPage.FirstName, "|", participantPage.LastName).Trim(),
                     ImageUrl = string.Format("{0}/{1}",
                        Settings.Default.PersonalizationImageDirectory,
                        Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME),
                     RedirectToStore = true
                  }
               });
            carouselImages.ForEach(p => p.ImageUrl = p.ImageUrl.TransformToMGPImagePath());
            carouselImages.ForEach(p => p.AlternativeText = FormatCompleteNameWithDelimeter(p.AlternativeText, "|"));
            ViewBag.Caroussel = carouselImages;
         }
         ViewBag.PopularItems = CreatePopularItems(eventId, participantId);

         var eventBranding = ViewBag.PublicEvent as Models.Branding.PublicEvent;
         ViewBag.AmountRaisedIndicator = new AmountRaisedIndicator
         {
            ParticipantId = participantId,
            Goal = eventBranding.Goal,
            IsGroupPageView = false,
            ThermometerPercentage =
               eventBranding.Goal > 0M
                  ? (int)(eventBranding.TotalAmount / eventBranding.Goal * 100.0M)
                  : eventBranding.TotalAmount > 0M ? 100 : 0
         };
	      ViewBag.IsParticipant = true;
         return View(participantPage);
      }

      private string FormatCompleteNameWithDelimeter(string name, string delimeter)
      {
         if (name.IsEmpty())
            return string.Empty;
         var n = name.Split(new[] { delimeter }, StringSplitOptions.RemoveEmptyEntries).ToList();
         if (n.Any())
            return String.Join(" ", n.Select(p => p.FirstLetterUpper()));
         else
            return string.Empty;
      }

      public ActionResult FindAParticipant(int eventId)
      {
         var carouselImages = new List<Caroussel>();
         var sponsorDefaultImg = string.Format("{0}/{1}",
            Settings.Default.PersonalizationImageDirectory,
            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);
         var partner = ViewBag.Partner as Partner;
         var eventBranding = ViewBag.PublicEvent as Models.Branding.PublicEvent;

         using (
            var dataProvider =
               new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var images = dataProvider.es_get_personalization_images_by_event_id(eventId).ToList();
            carouselImages.AddRange(
               images.Select(x => new Caroussel
               {
                  EventId = x.event_id,
                  ParticipantId = x.event_participation_id,
                  AnchorUrl =
                     x.event_participation_id > 0
                        ? new UrlHelper(Request.RequestContext).Action("Participant", "Group",
                           new { eventId, participantId = x.event_participation_id }).UrlEncode()
                        : "#",
                  AlternativeText =
                     string.Concat(x.first_name.FirstLetterUpper(), " ", x.last_name.FirstLetterUpper()).Trim(),
                  ImageUrl = x.image_url.TransformToMGPImagePath(),
                  IsCoverAlbum = x.isCoverAlbum,
                  RedirectToStore = x.event_participation_id <= 0
               })
            );
         }
         var participantDefaultImg = string.Format("{0}/{1}",
            Settings.Default.PersonalizationImageDirectory,
            Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);
         var groupId = 0;
         using (var dataProvider = new DataProvider())
         {
            if (!carouselImages.Any())
            {
               carouselImages.Add(new Caroussel()
               {
                  EventId = eventId,
                  IsCoverAlbum = true,
                  ImageUrl = sponsorDefaultImg,
                  AnchorUrl = string.Empty
               });
            }
            if (!carouselImages.Any(p => p.IsCoverAlbum))
            {
               carouselImages.First().IsCoverAlbum = true;
            }
            ViewBag.Caroussel = carouselImages.OrderByDescending(x => x.IsCoverAlbum)
               .Select(x => x)
               .ToList();

            eventBranding.CoverImage = carouselImages.First(p => p.IsCoverAlbum).ImageUrl;
            var participants = (from ev in dataProvider.event_participation
                                from p in dataProvider.personalizations
                                from mh in dataProvider.member_hierarchy
                                from cc in dataProvider.creation_channel
                                from m in dataProvider.members
                                from u in dataProvider.users
                                where
                                ev.event_participation_id == p.event_participation_id
                                && ev.member_hierarchy_id == mh.member_hierarchy_id
                                && mh.creation_channel_id == cc.creation_channel_id
                                && cc.member_type_id == 2
                                && mh.member_id == m.member_id
                                && m.user_id == u.user_id
                                && ev.event_id == eventId
                                && !m.deleted
                                select new Models.Views.Participant
                                {
                                   Id = ev.event_participation_id,
                                   Name = u.first_name + " " + u.last_name,
                                   AmountRaised = 0,
                                   Goal = p.fundraising_goal == null ? 0 : (decimal)p.fundraising_goal
                                }).ToList();
            var participantsTotalAmount = dataProvider.participant_total_amount.ToList();
            groupId = dataProvider.event_group.Single(e => e.event_id == eventId).group_id;

            foreach (var result in participants)
            {
               result.Url = new UrlHelper(Request.RequestContext).Action("Participant", "Group",
                  new { eventId, participantId = result.Id });
               result.AmountRaised =
                  participantsTotalAmount.FirstOrDefault(x => x.event_participation_id == result.Id) != null
                     ? participantsTotalAmount.FirstOrDefault(x => x.event_participation_id == result.Id).total_amount
                     : 0;
            }
            ViewBag.AmountRaisedIndicator = new AmountRaisedIndicator
            {
               Goal = eventBranding.Goal,
               IsGroupPageView = true,
               ThermometerPercentage =
                  eventBranding.Goal > 0M
                     ? (int)(eventBranding.TotalAmount / eventBranding.Goal * 100.0M)
                     : eventBranding.TotalAmount > 0M ? 100 : 0,
               HideJOIN = partner.EventsToHideJOINButton.IsEmpty()
                  ? false
                  : partner.EventsToHideJOINButton.Contains(groupId.ToString())
            };
            return View(participants);
         }
      }

      [HttpPost]
      public ContentResult FindAParticipant(int eventId, FindAParticipant model)
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

         using (var dataProvider = new DataProvider())
         {
            var participants = (from ev in dataProvider.event_participation
                                from p in dataProvider.personalizations
                                from mh in dataProvider.member_hierarchy
                                from m in dataProvider.members
                                where
                                ev.event_participation_id == p.event_participation_id
                                && ev.member_hierarchy_id == mh.member_hierarchy_id
                                && mh.member_id == m.member_id
                                && ev.event_id == model.EventId
                                && !m.deleted
                                &&
                                (m.first_name.ToLower().Contains(model.Name.ToLower()) ||
                                 m.last_name.ToLower().Contains(model.Name.ToLower()))
                                select new Models.Views.Participant
                                {
                                   Id = ev.event_participation_id,
                                   Name = m.first_name + " " + m.last_name,
                                   AmountRaised = 0,
                                   Goal = p.fundraising_goal == null ? 0 : (decimal)p.fundraising_goal
                                }).ToList();
            var participantsTotalAmount = dataProvider.participant_total_amount.ToList();

            foreach (var result in participants)
            {
               result.Url = new UrlHelper(Request.RequestContext).Action("Participant", "Group",
                  new { eventId = model.EventId, participantId = result.Id });
               result.AmountRaised =
                  participantsTotalAmount.FirstOrDefault(x => x.event_participation_id == result.Id) !=
                  null
                     ? participantsTotalAmount.FirstOrDefault(x => x.event_participation_id == result.Id).total_amount
                     : 0;
            }
            var data = JsonConvert.SerializeObject(
               new
               {
                  participants,
                  success = true
               });
            return Content(data, "application/json");
         }
      }

      public ActionResult RedirectToStoreDirect(int eventId, int? participantId, int? entityId,
         int? storefrontCategoryId)
      {
         bool success;
         int supporterId;
         var storeUrl = redirectToStore(eventId, participantId.HasValue ? participantId.Value : 0, entityId,
            storefrontCategoryId, out success, out supporterId);
         if (success)
            return Redirect(storeUrl);
         else
            return RedirectToAction("Index");
      }

      [HttpPost]
      public JsonResult RedirectToStore(int eventId, int participantId, int? entityId, int? storefrontCategoryId)
      {
         bool success;
         int supporterId;
         var storeUrl = redirectToStore(eventId, participantId, entityId, storefrontCategoryId, out success,
            out supporterId);
         return new JsonResult
         {
            Data = new { success = success, url = storeUrl, supporterId },
            JsonRequestBehavior = JsonRequestBehavior.AllowGet
         };
      }

      private string redirectToStore(int eventId, int participantId, int? entityId, int? storefrontCategoryId,
         out bool success, out int supporterId)
      {
         success = true;
         supporterId = 0;
         var partner = ViewBag.Partner as Partner;
         var eventPartnerId = partner.Id;
         var touchId = Session[Constants.SESSION_KEY_TOUCH_ID] != null
            ? Convert.ToInt32(Session[Constants.SESSION_KEY_TOUCH_ID])
            : 0;
         if (touchId > 0 && participantId <= 0)
         {
            using (var dataProvider = new DataProvider())
            {
               try
               {
                  var @result = (from t in dataProvider.touches
                                 from ep in dataProvider.event_participation
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 where t.touch_id == touchId
                                       && t.event_participation_id == ep.event_participation_id
                                       && ep.member_hierarchy_id == mh.member_hierarchy_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                 select
                                 new { UserType = (UserType)cc.member_type_id, EventParticipationId = ep.event_participation_id })
                     .Single();
                  if (@result.UserType == UserType.SPONSOR || @result.UserType == UserType.PARTICIPANT)
                     participantId = @result.EventParticipationId;
                  else
                  {
                     participantId = (from mh in dataProvider.member_hierarchy
                                      from ep in dataProvider.event_participation
                                      from mhp in dataProvider.member_hierarchy
                                      from epp in dataProvider.event_participation
                                      where mh.member_hierarchy_id == ep.member_hierarchy_id
                                            && ep.event_participation_id == @result.EventParticipationId
                                            && mh.parent_member_hierarchy_id == mhp.member_hierarchy_id
                                            && mhp.member_hierarchy_id == epp.member_hierarchy_id
                                            && epp.event_id == eventId
                                      select epp.event_participation_id).SingleOrDefault();
                  }
               }
               catch (Exception ex)
               {
                  ex.Data.Add("Event Id", eventId);
                  ex.Data.Add("Participant Id", participantId);
                  ex.Data.Add("Entity Id", entityId.HasValue ? entityId.Value.ToString() : string.Empty);
                  ex.Data.Add("Storefront Category Id",
                     storefrontCategoryId.HasValue ? storefrontCategoryId.Value.ToString() : string.Empty);
                  ex.Data.Add("Touch Id", touchId);

                        //new email notification sender Sendinblue
                        var clientBlue = new RestClient(ConfigurationManager.AppSettings["sendInBlueEmailProvider"]);
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");
                        var body = ex;
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                        //SWCorporate.SystemEx.InstrumentationProvider.Current.SendExceptionNotification(ex, null);
                  return new UrlHelper(Request.RequestContext).Action("Index", "Home");
               }
            }
         }
         int groupId = 0, leadId = 0, suppId = 0;
         var groupName = string.Empty;
         int? SAPAccountNo = null;
         try
         {
            using (var dataProvider = new DataProvider())
            {
               var @result = (from eg in dataProvider.event_group
                              from g in dataProvider.groups
                              where eg.event_id == eventId
                                    && eg.group_id == g.group_id
                              select g).Single();
               groupId = @result.group_id;
               groupName = @result.group_name;
               if (@result.partner_id != eventPartnerId)
                  eventPartnerId = @result.partner_id;
               if (@result.lead_id > 0)
                  leadId = @result.lead_id ?? 0;
            }
         }
         catch (Exception ex)
         {
            ex.Data.Add("Event Id", eventId);
            ex.Data.Add("Participant Id", participantId);
            ex.Data.Add("Entity Id", entityId.HasValue ? entityId.Value.ToString() : string.Empty);
            ex.Data.Add("Storefront Category Id",
               storefrontCategoryId.HasValue ? storefrontCategoryId.Value.ToString() : string.Empty);
            ex.Data.Add("Touch Id", touchId);
                SentrySdk.CaptureException(ex);
                //SWCorporate.SystemEx.InstrumentationProvider.Current.SendExceptionNotification(ex, null);
                return new UrlHelper(Request.RequestContext).Action("Index", "Home");
         }
         if (leadId > 0)
         {
            try
            {
                    using (var client = new HttpClient())
                    {
                        var uri =
                           new Uri(string.Format("{0}/representatives?leadid={1}", ConfigurationManager.AppSettings["core.webapi.host"], leadId));
                        var representative = client
                           .GetAsync(uri)
                           .Result
                           .Content.ReadAsAsync<Representative>().Result;
                        SAPAccountNo = representative.SAPAccount;
                    }
                }
            catch (Exception)
            {
            }
         }
         // Set user
         GA.BDC.Web.MGP.Models.Branding.User loggedInUser = null, participantUser = null, sponsorUser = null;
         if (WebSecurity.IsAuthenticated)
         {
            var email = WebSecurity.CurrentUserName.Split('|')[0];
            var p = int.Parse(WebSecurity.CurrentUserName.Split('|')[1]);
            using (var dataProvider = new DataProvider())
            {
               loggedInUser = (from u in dataProvider.users
                               from m in dataProvider.members
                               from mh in dataProvider.member_hierarchy
                               where u.email_address == email && u.partner_id == p
                                  && u.user_id == m.user_id
                                  && m.member_id == mh.member_id
                               select new GA.BDC.Web.MGP.Models.Branding.User { Id = u.user_id, HierarchyId = mh.member_hierarchy_id, FirstName = u.first_name, LastName = u.last_name, Email = u.email_address }).First();
            }
         }
         if (participantId > 0)
         {
            try
            {
               using (var dataProvider = new DataProvider())
               {
                  participantUser = (from u in dataProvider.users
                                     from m in dataProvider.members
                                     from mh in dataProvider.member_hierarchy
                                     from ep in dataProvider.event_participation
                                     where u.user_id == m.user_id
                                        && m.member_id == mh.member_id
                                        && mh.member_hierarchy_id == ep.member_hierarchy_id
                                        && ep.event_participation_id == participantId
                                     select new GA.BDC.Web.MGP.Models.Branding.User { Id = u.user_id, HierarchyId = mh.member_hierarchy_id, FirstName = u.first_name, LastName = u.last_name, Email = u.email_address }).SingleOrDefault();
                  if (participantUser == null)
                  {
                     participantUser = (from m in dataProvider.members
                                        from mh in dataProvider.member_hierarchy
                                        from ep in dataProvider.event_participation
                                        where m.member_id == mh.member_id
                                           && mh.member_hierarchy_id == ep.member_hierarchy_id
                                           && ep.event_participation_id == participantId
                                        select new GA.BDC.Web.MGP.Models.Branding.User { Id = m.member_id, HierarchyId = mh.member_hierarchy_id, FirstName = m.first_name, LastName = m.last_name, Email = m.email_address }).SingleOrDefault();
                  }
               }
            }
            catch (Exception ex)
            {
               ex.Data.Add("Event Id", eventId);
               ex.Data.Add("Participant Id", participantId);
               ex.Data.Add("Entity Id", entityId.HasValue ? entityId.Value.ToString() : string.Empty);
               ex.Data.Add("Storefront Category Id", storefrontCategoryId.HasValue ? storefrontCategoryId.Value.ToString() : string.Empty);
               ex.Data.Add("Touch Id", touchId);
                    //new email notification sender Sendinblue
                    var clientBlue = new RestClient(ConfigurationManager.AppSettings["sendInBlueEmailProvider"]);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("api-key", "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");
                    var body = ex;
                    request.AddBody(body);
                    var response = clientBlue.Execute(request);
                    //SWCorporate.SystemEx.InstrumentationProvider.Current.SendExceptionNotification(ex, null);
                    return new UrlHelper(Request.RequestContext).Action("Index", "Home");
            }
         }
         using (var dataProvider = new DataProvider())
         {
            sponsorUser = (from u in dataProvider.users
                           from m in dataProvider.members
                           from mh in dataProvider.member_hierarchy
                           from ep in dataProvider.event_participation
                           where u.user_id == m.user_id
                              && m.member_id == mh.member_id
                              && mh.member_hierarchy_id == ep.member_hierarchy_id
                              && ep.participation_channel_id == 3
                              && ep.event_id == eventId
                           select new GA.BDC.Web.MGP.Models.Branding.User { Id = u.user_id, HierarchyId = mh.member_hierarchy_id, FirstName = u.first_name, LastName = u.last_name, Email = u.email_address }).FirstOrDefault();
            if (sponsorUser == null)
            {
               sponsorUser = (from m in dataProvider.members
                              from mh in dataProvider.member_hierarchy
                              from ep in dataProvider.event_participation
                              where m.member_id == mh.member_id
                                 && mh.member_hierarchy_id == ep.member_hierarchy_id
                                 && ep.participation_channel_id == 3
                                 && ep.event_id == eventId
                              select new GA.BDC.Web.MGP.Models.Branding.User { Id = m.member_id, HierarchyId = mh.member_hierarchy_id, FirstName = m.first_name, LastName = m.last_name, Email = m.email_address }).FirstOrDefault();
            }
         }
         // generate a random password
         PasswordHelper ph = new PasswordHelper();
         ph.ExcludeSymbols = true;
         ph.Exclusions = "1234567890";
         string gen = ph.Generate();

         var firstName = loggedInUser != null ? loggedInUser.FirstName.FirstLetterUpper() : string.Empty;
         var lastName = loggedInUser != null ? loggedInUser.LastName.FirstLetterUpper() : string.Empty;
         var emailAddress = loggedInUser != null ? loggedInUser.Email : "esg" + gen + "@efundraising.com";

         // create the supporter
         using (var dataProvider = new DataProvider())
         {
            var parentMemberHierarchyId = 0;
            if (participantUser != null)
               parentMemberHierarchyId = participantUser.HierarchyId;
            else if (loggedInUser != null)
               parentMemberHierarchyId = loggedInUser.HierarchyId;
            else
               parentMemberHierarchyId = sponsorUser.HierarchyId;
            using (var transactionScope = new TransactionScope())
            {
               var member = new member
               {
                  create_date = DateTime.Now,
                  password = null,
                  partner_id = eventPartnerId,
                  user_id = null,
                  email_address = emailAddress,
                  last_name = WebUtility.HtmlDecode(WebUtility.HtmlDecode(lastName)),
                  first_name = WebUtility.HtmlDecode(WebUtility.HtmlDecode(firstName)),
                  culture_code = "en-US",
                  opt_status_id = 1,
                  unsubscribe = false,
                  comments = "Member created by Redirect to Store"
               };

               dataProvider.members.Add(member);
               dataProvider.SaveChanges();
               var memberHierarchy = new member_hierarchy
               {
                  create_date = DateTime.Now,
                  active = true,
                  creation_channel_id = 15,
                  member_id = member.member_id,
                  parent_member_hierarchy_id = parentMemberHierarchyId,
                  unsubscribe = false
               };
               dataProvider.member_hierarchy.Add(memberHierarchy);
               dataProvider.SaveChanges();
               var eventParticipation = new event_participation
               {
                  event_id = eventId,
                  member_hierarchy_id = memberHierarchy.member_hierarchy_id,
                  participation_channel_id = 2,
                  create_date = DateTime.Now,
                  salutation = string.Concat(firstName, " ", lastName).Trim(),
                  agree_term_services = true,
                  holiday_reminders = false
               };
               dataProvider.event_participation.Add(eventParticipation);
               dataProvider.SaveChanges();
               transactionScope.Complete();

               suppId = eventParticipation.event_participation_id;
               supporterId = suppId;
            }
         }

         // If donation partner, return url now
         if (partner.ProductOffer == ProductOffer.DonationOnly)
         {
            return new UrlHelper(Request.RequestContext).Action("CheckOut", "Donation", new { eventId, participantId = suppId });
         }

         string storeUrl = string.Empty, participantFirstName = string.Empty, participantLastName = string.Empty, storeItemID = string.Empty, storeCategoryID = string.Empty;
         if (participantUser != null)
         {
            participantFirstName = participantUser.FirstName.FirstLetterUpper();
            participantLastName = participantUser.LastName.FirstLetterUpper();
         }
         else if (loggedInUser != null)
         {
            participantFirstName = loggedInUser.FirstName.FirstLetterUpper();
            participantLastName = loggedInUser.LastName.FirstLetterUpper();
         }
         else
         {
            participantFirstName = sponsorUser.FirstName.FirstLetterUpper();
            participantLastName = sponsorUser.LastName.FirstLetterUpper();
         }
         if (participantFirstName.IsEmpty() && participantLastName.IsEmpty())
         {
            participantFirstName = "";
            participantLastName = "";
         }
         //Possible solving error fro N/A in Ga store confirmation email
         if (participantFirstName.IsEmpty())
         {
            participantFirstName = participantLastName;
         }
         //if (participantLastName.IsEmpty())
         //{
         //    participantLastName = participantFirstName;
         //}
         //Possible solving error fro N/A in Ga store confirmation email - end 

         if (Request["catalogitemId"] != null)
         {
            storeItemID = Request["catalogitemId"];
         }
         if (entityId.HasValue)
         {
            storeItemID = entityId.Value.ToString();
         }
         if (Request["categoryId"] != null)
         {
            storeCategoryID = Request["categoryId"];
         }
         if (storefrontCategoryId.HasValue)
         {
            storeCategoryID = storefrontCategoryId.Value.ToString();
         }
         if (storeCategoryID.IsEmpty())
         {
            if (partner.ProductOffer == ProductOffer.MagazineOnly || partner.ProductOffer == ProductOffer.MagazineAndMore)
               storeCategoryID = Settings.Default.GA_MagOnly_MagazineStorefrontCategoryID;
            else
               storeCategoryID = Settings.Default.GA_MagazineStorefrontCategoryID;
         }

         // Validate StorefrontCategoryId
         if ((partner.ProductOffer == ProductOffer.MagazineOnly || partner.ProductOffer == ProductOffer.MagazineAndMore) &&
             storeCategoryID != Settings.Default.GA_MagOnly_MagazineStorefrontCategoryID)
         {
            storeCategoryID = Settings.Default.GA_MagOnly_MagazineStorefrontCategoryID;
         }

         int? productEntityID = !string.IsNullOrEmpty(storeItemID) ? int.Parse(storeItemID) : (int?)null;
         int? storefrontCategoryID = !string.IsNullOrEmpty(storeCategoryID) ? int.Parse(storeCategoryID) : (int?)null;

         // Perform bacward compatibility for product entity Id
         if (productEntityID != null)
         {
            switch (productEntityID)
            {
               case 58822:
               case 62120:
               case 64871:
                  productEntityID = 100206; /*All You*/
                  break;
               case 59211:
                  productEntityID = 101077; /*Popular Science*/
                  break;
               case 58984:
                  productEntityID = 102551; /*Every Day with Rachael Ray*/
                  break;
               case 59382:
                  productEntityID = 101978; /*WW II History Magazine*/
                  break;
               case 59127:
                  productEntityID = 101097; /*Military Heritage*/
                  break;
               case 59796:
                  productEntityID = 101829; /*TIME*/
                  break;
               case 31226:
                  productEntityID = 100966; /*Better Homes & Gardens*/
                  break;
               case 59295:
               case 59294:
               case 65430:
               case 65461:
                  productEntityID = 101828; /*Sports Illustrated*/
                  break;
               case 59788:
               case 54702:
               case 65323:
                  productEntityID = 100798; /*People*/
                  break;
               case 59155:
               case 65276:
                  productEntityID = 101177; /*National Geographic*/
                  break;
               case 64942:
                  productEntityID = 100991; /*Bon Appetit*/
                  break;
               case 59226:
               case 65359:
                  productEntityID = 101216; /*Reader's Digest*/
                  break;
               case 59795:
               case 65431:
                  productEntityID = 101880; /*Sports Illustrated KIDS*/
                  break;
               case 58981:
                  productEntityID = 102548; /*ESPN, The Magazine*/
                  break;
               case 59311:
                  productEntityID = 101895; /*Taste of Home*/
                  break;
               case 65550:
                  productEntityID = 101242; /*Real Simple*/
                  break;
               case 65450:
                  productEntityID = 101896; /*Teen Vogue*/
                  break;
               case 65277:
                  productEntityID = 101178; /*National Geographic Kids*/
                  break;
               case 62366:
                  productEntityID = 100808; /*Health*/
                  break;
               case 62091:
               case 59128:
               case 51931:
               case 65179:
               case 52032:
                  productEntityID = null; /*Erase*/
                  break;
               default:
                  break;
            }
         }

         string storeServiceErrorMessage = string.Empty;
         try
         {
                if (SAPAccountNo == null)
                    SAPAccountNo = 0;
                
                var strNull = "null";
                
                HttpClient client = new HttpClient();
                var registerUserJSON = "{"
                        + "\"AffiliateURLGraphRequest\":{"
                        + "\"Email\": \"" + emailAddress + "\","
                        + "\"ExternalAffiliateIDentifier\": \"" + groupId + "\","
                        + "\"ExternalAffiliateName\": \"" + groupName + "\","
                        + "\"ExternalPartnerIdentifier\": \"" + eventPartnerId + "\","
                        + "\"ExternalSourceIdentifier\": \"" + touchId + "\","
                        + "\"ExternalSupporterIdentifier\": \"" + suppId.ToString() + "\","
                        + "\"Language\": \"" + Language_.English + "\","
                        + "\"IsRenewal\": \"" + "false" + "\","
                        + "\"ParticipantFirstName\": \"" + participantFirstName + "\","
                        + "\"ParticipantLastName\": \"" + participantLastName + "\","
                        + "\"ProductEntityID\":"  + strNull + ","
                        + "\"SalesRepSAPAcctNr\": \"" + SAPAccountNo + "\","
                        + "\"StorefrontCategoryID\": \"" + storefrontCategoryID + "\""
                        + "}}";


                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["StoreURLAPI"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                client.DefaultRequestHeaders.Add("Client-Id", ConfigurationManager.AppSettings["Client-Id"]);
                client.DefaultRequestHeaders.Add("Client-Secret", ConfigurationManager.AppSettings["Client-Secret"]);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
                request.Content = new StringContent(registerUserJSON,Encoding.UTF8,"application/json");//CONTENT-TYPE header
                var result = client.PostAsync(client.BaseAddress.ToString(), request.Content).Result;

                if (result.IsSuccessStatusCode)
                {
                    
                    //result returned example- {"AffiliateURLGraph":{"AffiliateURL":"http:\/\/test.mystore.fundraising.com\/storev2\/store\/product\/904\/0?t=bfef055e-000e-49a7-8af9-0aac002a9505","businessRuleViolation":null}}
                    var fixResult = result.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' });
                    var parseResult = JsonConvert.DeserializeObject<dynamic>(fixResult);
                    storeUrl = parseResult["AffiliateURLGraph"].Value<string>("AffiliateURL"); // get clean url
                    SentrySdk.CaptureMessage("MGP store url create - " + parseResult);
                    var businessRuleViolation = parseResult["AffiliateURLGraph"].Value<string>("businessRuleViolation");
                    if (businessRuleViolation != null)
                    {
                        storeServiceErrorMessage = businessRuleViolation.ViolationMessage + " (Type=" + businessRuleViolation.BusinessRuleViolationTypeCode.ToString() + ")";
                        SentrySdk.CaptureMessage(storeServiceErrorMessage + " " + "businessRuleViolation returned null grp controller");
                    }

                }
                else
                {
                    Exception ex = new Exception();
                    ex.Data.Add("Message", "Error getting store url from GA api");
                    ex.Data.Add("Email Address", emailAddress);
                    ex.Data.Add("Group Id", groupId);
                    ex.Data.Add("Group Name", groupName);
                    ex.Data.Add("Partner Id", eventPartnerId);
                    ex.Data.Add("Touch Id", touchId);
                    ex.Data.Add("Supporter Id", suppId);
                    ex.Data.Add("Language", Language_.English);
                    ex.Data.Add("Entity Id", productEntityID);
                    ex.Data.Add("SAP Account Number", SAPAccountNo);
                    ex.Data.Add("Storefront Category Id", storefrontCategoryID);
                    ex.Data.Add("Participant First Name", participantFirstName);
                    ex.Data.Add("Participant Last Name", participantLastName);
                    SentrySdk.CaptureException(ex);
                    success = false;
                    return new UrlHelper(Request.RequestContext).Action("Index", "Home");


                }


            }
         catch (Exception ex)
         {
                ex.Data.Add("Store Service Error Message", storeServiceErrorMessage);
                ex.Data.Add("Email Address", emailAddress);
                ex.Data.Add("Group Id", groupId);
                ex.Data.Add("Group Name", groupName);
                ex.Data.Add("Partner Id", eventPartnerId);
                ex.Data.Add("Touch Id", touchId);
                ex.Data.Add("Supporter Id", suppId);
                ex.Data.Add("Language", Language_.English);
                ex.Data.Add("Entity Id", productEntityID);
                ex.Data.Add("SAP Account Number", SAPAccountNo);
                ex.Data.Add("Storefront Category Id", storefrontCategoryID);
                ex.Data.Add("Participant First Name", participantFirstName);
                ex.Data.Add("Participant Last Name", participantLastName);
                SentrySdk.CaptureException(ex);
                success = false;
                return new UrlHelper(Request.RequestContext).Action("Index", "Home");
            }
           
            return storeUrl;
      }

      private IList<PopularItem> CreatePopularItems(int eventId, int participantId)
      {
         var partner = ViewBag.Partner as Partner;
         var selectorId = partner.Program == Program.Mvp ? 143 : partner.Id;
         var popularItems = new List<PopularItem>();
         using (var dataProvider = new DataProvider())
         {
            popularItems = (from pi in dataProvider.popular_item
                            where pi.culture_code == "en-US"
                            select new PopularItem
                            {
                               PartnerId = pi.partner_id,
                               EventId = eventId,
                               ParticipantId = participantId,
                               CultureCode = pi.culture_code,
                               EntityId = pi.entity_id,
                               StorefrontCategoryId = pi.storefront_category_id,
                               TypeId = pi.type_id,
                               Name = pi.name,
                               Description = pi.description,
                               ImageFileName = pi.image_file_name
                            }).ToList();
            if (popularItems.Any(p => p.PartnerId == selectorId))
               return popularItems.Where(p => p.PartnerId == selectorId).ToList();
            else
               return popularItems.Where(p => p.PartnerId == 0).ToList();
         }
      }
   }
}
