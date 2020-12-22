using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Transactions;
using System.Web.Mvc;
using System.Web;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Shared.Entities;
using GA.BDC.Web.MGP.Helpers;
using GA.BDC.Web.MGP.Helpers.Routes.Attributes;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Models.Views;
using GA.BDC.Web.MGP.Properties;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Web.MGP.Helpers.Context;
using GA.Core.Shared.Contracts;
using GA.Store.Shared;
using GA.Store.Shared.Contracts;
using WebMatrix.WebData;
using TagParsor = GA.BDC.Web.MGP.Helpers.EmailTemplate.TagParsor;
using Constants = GA.BDC.Web.MGP.Helpers.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Address = GA.BDC.Web.MGP.Helpers.Address;
using Country = GA.BDC.Shared.Entities.Country;
using Partner = GA.BDC.Web.MGP.Models.Branding.Partner;
using Region = GA.BDC.Shared.Entities.Region;

namespace GA.BDC.Web.MGP.Controllers
{
    [MGPAuthentication, RenderPartnerBranding]
    public class CampaignManagerController : BaseController
    {
        [RenderEventBranding]
        [HttpGet]
        [Route("registration/step-2")]
        public ActionResult Step2(int participantId)
        {
            using (var dataProvider = new DataProvider())
            {
                var user =  (Models.Branding.User)ViewBag.User;
                var eventBranding = (Models.Branding.Event)ViewBag.Event;
                var result = (from p in dataProvider.personalizations
                              from ep in dataProvider.event_participation
                              from mh in dataProvider.member_hierarchy
                              from m in dataProvider.members
                              where p.event_participation_id == ep.event_participation_id
                                    && ep.member_hierarchy_id == mh.member_hierarchy_id
                                    && mh.member_id == m.member_id
                                    && ep.event_id == eventBranding.Id
                                    && m.user_id == user.Id
                                    && !m.deleted
                                    && (ep.event_participation_id == user.EventParticipationId ||
                                        user.EventParticipationId == 0)
                              select new
                              {
                                  Personalization = new Personalization
                                  {
                                      Id = p.personalization_id,
                                      Body = p.body,
                                      Goal = (double)p.fundraising_goal,
                                      Redirect = p.redirect
                                  },
                                  PersonalizationImage = p.personalization_image
                              }).Single();
                result.Personalization.Body = result.Personalization.Body.ReplaceSingleQuoteToAlternativeVersion();
                var personalization = result.Personalization;
                personalization.Body = personalization.Body.ReplaceNewLineToBR();
                if (user.IsSponsor)
                {
                    if (eventBranding.EventType != EventTypeInfo.INDIVIDUAL_FUNDRAISER)
                    {
                        personalization.DefaultImage = $"{Settings.Default.PersonalizationImageDirectory}/{Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME}";
                    }
                    else
                    {
                        personalization.DefaultImage = $"{Settings.Default.PersonalizationImageDirectory}/{Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME}";
                    }
                }
                else if (user.IsParticipant)
                {
                    personalization.DefaultImage = $"{Settings.Default.PersonalizationImageDirectory}/{Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME}";
                    var sponsorPersonalization = (from mh in dataProvider.member_hierarchy
                                                  from ep in dataProvider.event_participation
                                                  from p in dataProvider.personalizations
                                                  where mh.member_hierarchy_id == ep.member_hierarchy_id
                                                     && ep.event_participation_id == p.event_participation_id
                                                     && ep.event_id == eventBranding.Id
                                                     && ep.participation_channel_id == 3
                                                  select p).Single();
                    ViewBag.SponsorRedirect = sponsorPersonalization.redirect;
                }
                personalization.PersonalizationImages = result.PersonalizationImage.Any(p => p.image_approval_status_id != 4 && !p.deleted)
                                                         ? result.PersonalizationImage
                                                           .Where(p => p.image_approval_status_id != 4 && !p.deleted)
                                                           .OrderByDescending(p => p.isCoverAlbum)
                                                           .Select(x => new PersonalizationImage
                                                           {
                                                               ImageId = x.image_id,
                                                               PersonalizationId = x.personalization_id,
                                                               IsCoverAlbum = x.isCoverAlbum.HasValue ? x.isCoverAlbum.Value : false,
                                                               ImageURL = x.image_url.TransformToMGPImagePath()
                                                           }).ToArray()
                                                         : (new List<PersonalizationImage>
                                                               {
                                                               new PersonalizationImage
                                                               {
                                                                   ImageId = 0,
                                                                   PersonalizationId = 0,
                                                                   ImageURL = personalization.DefaultImage
                                                               }
                                                               }).ToArray();
                personalization.PersoImgsJSON = JsonConvert.SerializeObject(personalization.PersonalizationImages);
                return View(personalization);
            }
        }

        [RenderEventBranding]
        [HttpPost]
        [Route("registration/step-2")]
        public JsonResult Step2(int participantId, string imageUrl)
        {
            using (var dataProvider = new DataProvider())
            {
                var personalization = dataProvider.personalizations.First(p => p.event_participation_id == participantId);
                foreach (var pi in dataProvider.personalization_image.Where(p => p.personalization_id == personalization.personalization_id).ToList())
                {
                    dataProvider.Entry(pi).State = EntityState.Deleted;
                    personalization.personalization_image.Remove(pi);
                }
	            string targetWebPath;
	            string monthDirectory;
	            if (imageUrl.StartsWith(Settings.Default.UploadedFileTempPath, StringComparison.InvariantCultureIgnoreCase))
                {
                    var pathGroup = Server.MapPath(string.Concat(Settings.Default.ImagesVirtualDirectory, Settings.Default.GroupFileShareDirectory));
	                var targetSaveDirectory = ImageHelper.GetMonthPersonalizationFolder(pathGroup, personalization.personalization_id, out monthDirectory);
                    targetWebPath = string.Concat(Settings.Default.ImagesVirtualDirectory, Settings.Default.GroupFileShareDirectory, monthDirectory, "/", personalization.personalization_id);
                    targetWebPath = targetWebPath.Replace('\\', '/');
                    FileInfo fi = new FileInfo(Server.MapPath(imageUrl));
                    if (fi.Exists)
                    {
                        if (fi.Directory != null && fi.Directory.Name == "temp")
                        {
                            targetWebPath = string.Concat(targetWebPath, "/", fi.Name);
                            var copyToTarget = string.Concat(targetSaveDirectory, "\\", fi.Name);
                            fi.CopyTo(copyToTarget, true);
                            fi.Delete();
                        }
                    }
                }
                else
                {
                    targetWebPath = imageUrl;
                }
                var personalizationImage = new personalization_image
                {
                    image_approval_status_id = 3,
                    image_url = targetWebPath,
                    isCoverAlbum = true,
                    personalization_id = personalization.personalization_id,
                    create_date = DateTime.Now
                };
                dataProvider.personalization_image.Add(personalizationImage);
                dataProvider.SaveChanges();
            }

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId } };
        }

        [RenderEventBranding]
        [HttpGet]
        [Route("registration/step-3")]
        public ActionResult Step3(int participantId)
        {
            using (var dataProvider = new DataProvider())
            {
                var personalization = dataProvider.personalizations.First(p => p.event_participation_id == participantId);
                var body = personalization.body.ReplaceSingleQuoteToAlternativeVersion().ReplaceNewLineToBR();
                ViewBag.Body = body;
                return View("Step3");
            }

        }

        [RenderEventBranding]
        [HttpPost]
        [Route("registration/step-3")]
        public JsonResult Step3(int participantId, string body)
        {
            using (var dataProvider = new DataProvider())
            {
                var personalization = dataProvider.personalizations.First(p => p.event_participation_id == participantId);
                personalization.body = body;
                dataProvider.SaveChanges();
            }

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId } };
        }

        [RenderEventBranding]
        [HttpGet]
        [Route("registration/step-4")]
        public ActionResult Step4(int participantId)
        {
            using (var dataProvider = new DataProvider())
            {
                var personalization = dataProvider.personalizations.First(p => p.event_participation_id == participantId);
                ViewBag.Redirect = personalization.redirect;
                return View("Step4");
            }
        }

        [RenderEventBranding]
        [HttpGet]
        [Route("registration/step-5")]
        public ActionResult Step5(int participantId)
        {
            return View("Step5");
        }

        [RenderEventBranding]
        [HttpGet]
        [Route("registration/step-6")]
        public ActionResult Step6(int participantId)
        {
            using (var dataProvider = new DataProvider())
            {
                var user = (Models.Branding.User) ViewBag.User;
                var eventBranding = (Models.Branding.Event) ViewBag.Event;
                var result = (from p in dataProvider.personalizations
                              from ep in dataProvider.event_participation
                              from mh in dataProvider.member_hierarchy
                              from m in dataProvider.members
                              where p.event_participation_id == ep.event_participation_id
                                    && ep.member_hierarchy_id == mh.member_hierarchy_id
                                    && mh.member_id == m.member_id
                                    && ep.event_id == eventBranding.Id
                                    && m.user_id == user.Id
                                    && !m.deleted
                                    && (ep.event_participation_id == user.EventParticipationId ||
                                        user.EventParticipationId == 0)
                              select new
                              {
                                  Personalization = new Personalization
                                  {
                                      Id = p.personalization_id,
                                      Body = p.body,
                                      Goal = (double)p.fundraising_goal,
                                      Redirect = p.redirect
                                  },
                                  PersonalizationImage = p.personalization_image
                              }).Single();
                result.Personalization.Body = result.Personalization.Body.ReplaceSingleQuoteToAlternativeVersion();
                var personalization = result.Personalization;
                personalization.Body = personalization.Body.ReplaceNewLineToBR();
                if (user.IsSponsor)
                {
                    if (eventBranding.EventType != EventTypeInfo.INDIVIDUAL_FUNDRAISER)
                    {
                        personalization.DefaultImage = string.Format("{0}/{1}",
                                                            Settings.Default.PersonalizationImageDirectory,
                                                            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);
                    }
                    else
                    {
                        personalization.DefaultImage = string.Format("{0}/{1}",
                                                        Settings.Default.PersonalizationImageDirectory,
                                                        Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);
                    }
                }
                else if (user.IsParticipant)
                {
                    personalization.DefaultImage = string.Format("{0}/{1}",
                                                        Settings.Default.PersonalizationImageDirectory,
                                                        Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);
                    var sponsorPersonalization = (from mh in dataProvider.member_hierarchy
                                                  from ep in dataProvider.event_participation
                                                  from p in dataProvider.personalizations
                                                  where mh.member_hierarchy_id == ep.member_hierarchy_id
                                                     && ep.event_participation_id == p.event_participation_id
                                                     && ep.event_id == eventBranding.Id
                                                     && ep.participation_channel_id == 3
                                                  select p).Single();
                    ViewBag.SponsorRedirect = sponsorPersonalization.redirect;
                }
                personalization.PersonalizationImages = result.PersonalizationImage.Any(p => p.image_approval_status_id != 4 && !p.deleted)
                                                         ? result.PersonalizationImage
                                                           .Where(p => p.image_approval_status_id != 4 && !p.deleted)
                                                           .OrderByDescending(p => p.isCoverAlbum)
                                                           .Select(x => new PersonalizationImage
                                                           {
                                                               ImageId = x.image_id,
                                                               PersonalizationId = x.personalization_id,
                                                               IsCoverAlbum = x.isCoverAlbum.HasValue ? x.isCoverAlbum.Value : false,
                                                               ImageURL = x.image_url.TransformToMGPImagePath()
                                                           }).ToArray()
                                                         : (new List<PersonalizationImage>
                                                               {
                                                               new PersonalizationImage
                                                               {
                                                                   ImageId = 0,
                                                                   PersonalizationId = 0,
                                                                   ImageURL = personalization.DefaultImage
                                                               }
                                                               }).ToArray();
                personalization.PersoImgsJSON = JsonConvert.SerializeObject(personalization.PersonalizationImages);
                return View(personalization);
            }
        }

        [RenderEventBranding]
        [HttpPost]
        [Route("registration/step-5")]
        public JsonResult Step5(int participantId, Recipient[] recipients, int reminderFrecuency)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0);
                var responseInfo = new ResponseInfo
                {
                    Status = 200,
                    Type = ResponseType.ERROR,
                    ContentType = "application/json; charset=utf-8",
                    Body = string.Empty,
                    ModelStateError = errorMessage
                };
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { success = false, responseText = errorMessage.Value.Errors[0].ErrorMessage, responseInfo = JsonConvert.SerializeObject(responseInfo) }
                };
            }
            if (recipients == null)
            {
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId } };
            }
            var user = (Models.Branding.User) ViewBag.User;
            var @event = (Models.Branding.Event) ViewBag.Event;
            var emailFlowId = GetEmailFlowBy(0, user.UserTypeInfo);
            var partner = (Partner) ViewBag.Partner;
            using (var dataProvider = new DataProvider())
            {
                var eventType = (int)@event.EventType;
                var templates = (from ef in dataProvider.email_flows
                                 from eft in dataProvider.email_flow_template
                                 from et in dataProvider.email_template
                                 from etc in dataProvider.email_template_culture
                                 from br in dataProvider.business_rule
                                 where ef.email_flow_id == emailFlowId
                                       && ef.email_flow_id == eft.email_flow_id
                                       && eft.event_type_id == eventType
                                       && eft.email_template_id == et.email_template_id
                                       && etc.email_template_id == et.email_template_id
                                       && etc.culture_code == "en-US"
                                       && et.email_template_id == br.email_template_id
                                       && br.active
                                 orderby etc.email_template_id ascending
                                 select new { TemplateId = et.email_template_id, From = et.from_name, Subject = etc.subject, BodyHtml = etc.body_html, BodyText = etc.body_text, CreationChannelManual = ef.manual_creation_channel_id, CreationChannelManualOverride = eft.override_manual_creation_channel_id, CreationChannelImport = ef.import_creation_channel_id, CreationChannelImportOverride = eft.override_import_creation_channel_id, BusinessRuleId = eft.business_rule_id, UserTypeFrom = ef.user_type_from, UserTypeTo = ef.user_type_to }).ToList();
                var mainTemplate = templates[0];
                var model = new KickOff
                {
                    ReminderRecurrency = reminderFrecuency,
                    CustomMessage = @event.Message,
                    TemplateId = mainTemplate.TemplateId,
                    From = mainTemplate.From,
                    Subject = mainTemplate.Subject.TransformQuotesForUI(),
                    Message = mainTemplate.BodyHtml.TransformQuotesForUI().ReplaceNewLineToBR(),
                    TextMessage = mainTemplate.BodyText,
                    CreationChannelImport = mainTemplate.CreationChannelImportOverride ?? mainTemplate.CreationChannelImport,
                    CreationChannelManual = mainTemplate.CreationChannelManualOverride ?? mainTemplate.CreationChannelManual,
                    BusinessRuleId = mainTemplate.BusinessRuleId
                };

                var isTestEnvironment = Request.Url.Host.Contains("test.") || Request.Url.Host.Contains("localhost") || Request.Url.Host.Contains("local.");

                TagParsor.ParseEmail(@event.Id, mainTemplate.UserTypeFrom, ViewBag.Partner as Partner, user, model,
                   isTestEnvironment);
                foreach (var recipient in recipients)
                {
                    var touchEmailProcessQueue = new touch_email_process_queue
                    {
                        bussiness_rule_id = model.BusinessRuleId,
                        created = DateTime.Now,
                        custom_message = string.Empty,
                        email_address = recipient.Email,
                        event_id = @event.Id,
                        first_name = !string.IsNullOrEmpty(recipient.FirstName) ? new string(recipient.FirstName.Take(100).ToArray()) : string.Empty,
                        last_name = !string.IsNullOrEmpty(recipient.LastName) ? new string(recipient.LastName.Take(100).ToArray()) : string.Empty,
                        participation_channel_id = null,
                        status = recipients.Length <= int.Parse(ConfigurationManager.AppSettings["MaxAmountOfEmailsAllowedPerEvent"]) ? (short)touch_email_process_queue_status.Scheduled : (short)touch_email_process_queue_status.Flagged,
                        creation_channel_id = recipient.IsManual ? model.CreationChannelManual : model.CreationChannelImport,
                        email_flow_id = emailFlowId,
                        sponsor_event_participation_id = user.EventParticipationId,
                        event_type = (int)@event.EventType,
                        is_sponsor = user.IsSponsor,
                        message_type = 0,
                        partner_id = partner.Id,
                        reminder_recurrency = reminderFrecuency,
                        subject = model.Subject
                    };
                    dataProvider.touch_emails_process_queue.Add(touchEmailProcessQueue);
                }
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId } };
        }

        [RenderEventBranding]
        [HttpPost]
        [Route("registration/step-4")]
        public JsonResult Step4(int participantId, string newLink)
        {
            using (var dataProvider = new DataProvider())
            {
                var personalization = dataProvider.personalizations.First(p => p.event_participation_id == participantId);
                var redirectExists = (from p in dataProvider.personalizations
                                      where p.personalization_id != personalization.personalization_id
                                      && p.redirect == newLink
                                      select p.personalization_id).Any();
                if (redirectExists)
                {
                    return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "The Link is already being used :(" } };
                }
                personalization.redirect = newLink;
                dataProvider.SaveChanges();
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId } };
            }
        }

        [RenderEventBranding]
        [HttpGet]
        [Route("registration/step-1")]
        public ActionResult Step1(int participantId)
        {
            return View("Step1");
        }

        [RenderEventBranding]
        [HttpPost]
        [Route("registration/step-1")]
        public JsonResult Step1(int participantId, double goal, string title, string postalCode)
        {
            var eventBranding = (Models.Branding.Event) ViewBag.Event;
            var partnerBranding = (Partner) ViewBag.Partner;
            if (goal < 1)
            {
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "Goal must be greater than zero" } };
            }
            int numericPostalCode;
            if (string.IsNullOrEmpty(postalCode) || postalCode.Length != 5 || !int.TryParse(postalCode, out numericPostalCode))
            {
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "Zip Code is incorrect" } };
            }
            string subdivisionCode;
            using (var client = new HttpClient())
            {
                var uri = new Uri($"http://ziptasticapi.com/{postalCode}");
                var postalCodeResult = JObject.Parse(client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result);

                if (postalCodeResult.Property("error") != null)
                {
                    return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "Zip Code is incorrect" } };
                }
                subdivisionCode = $"US-{postalCodeResult.Property("state").Value.Value<string>()}";
            }

            using (var dataProvider = new DataProvider())
            {
                var @event = dataProvider.events.Find(eventBranding.Id);

                var @group = (from g in dataProvider.groups
                              from eg in dataProvider.event_group
                              where g.group_id == eg.group_id && eg.event_id == @event.event_id
                              select g).First();
                var postalAddress = (from pi in dataProvider.payment_info
                                     from pa in dataProvider.postal_address
                                     where pi.postal_address_id == pa.postal_address_id && pi.group_id == @group.group_id
                                     select pa).First();
                var personalization = dataProvider.personalizations.First(p => p.event_participation_id == participantId);
                var member = (from ep in dataProvider.event_participation
                              from mh in dataProvider.member_hierarchy
                              from m in dataProvider.members
                              where
                              ep.member_hierarchy_id == mh.member_hierarchy_id && mh.member_id == m.member_id &&
                              ep.event_participation_id == participantId
                              select m).First();

                var suggestedCampaignName = FindSuggestedCampaignRedirect(title.ToLower().CleanupRedirect());
                @event.event_name = title;
                group.group_name = title;
                @event.redirect = suggestedCampaignName;
                @group.redirect = suggestedCampaignName;
                personalization.fundraising_goal = (decimal)goal;
                personalization.redirect = suggestedCampaignName;
                postalAddress.zip_code = postalCode;


                postalAddress.subdivision_code = subdivisionCode;
                
                try
                {
                    /* Insert lead */
                    if (Convert.ToBoolean(partnerBranding.InsertAsLead) && partnerBranding.Program != Program.Mvp)
                    {
                        var state = subdivisionCode;
                        if (state.Length == 5 && state.Contains("-"))
                        {
                            state = state.Substring(state.IndexOf("-", StringComparison.InvariantCulture) + 1);
                        }
                        var promotionId = Session[Constants.SESSION_KEY_PROMOTION_ID] != null
                                      ? Convert.ToInt32(Session[Constants.SESSION_KEY_PROMOTION_ID])
                                      : GetSelfRegistrationPromotion(partnerBranding.Id);
                        var fcExternalId = Session[Constants.SESSION_KEY_FCEXTERNAL_ID] != null
                                                        ? Convert.ToInt32(Session[Constants.SESSION_KEY_FCEXTERNAL_ID])
                                                        : int.MinValue;
                        var lead = new Shared.Entities.Lead
                        {
                            PromotionId = promotionId,
                            Group = title,
                            FirstName = member.first_name,
                            LastName = member.last_name,
                            ChannelCode = "INT",
                            Address = new Shared.Entities.Address
                            {
                                Address1 = "-",
                                Region = new Region { Code = state },
                                Country = new Country { Code = "US" },
                                City = "-",
                                PostCode = "-"
                            },
                            Phone = string.Empty,
                            Email = member.email_address,
                            PartnerId = partnerBranding.Id,
                            KitType = 43,
                            ConsultantId = fcExternalId > 0 ? fcExternalId : 0
                        };
                        using (var client = new HttpClient())
                        {
                            var uri =
                               new Uri($"{ConfigurationManager.AppSettings["core.webapi.host"]}/leads");
	                        var response = client.PostAsJsonAsync(uri, lead).Result;
	                        if (response.IsSuccessStatusCode)
	                        {
		                        var leadCreated = response.Content.ReadAsAsync<Shared.Entities.Lead>();
		                        @group.lead_id = leadCreated.Result.Id;
	                        }	                        
                        }
                    }
                }
                catch
                {
                    // ignore
                }
	            dataProvider.SaveChanges();
            }

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId } };
        }

        [RenderEventBranding, RenderOutputText]
        public ActionResult Index(int participantId)
        {
            ViewBag.HideCreateCampaign = false;
            var partner = (Partner) ViewBag.Partner;
            if (!IsInternalUser(Request.UserHostAddress))
            {
                if (partner.DoRedirectToLandingPage && partner.ESubsUrl.IsNotEmpty())
                {
                    ViewBag.HideCreateCampaign = true;
                }
            }

            if (partner.ProductOffer != ProductOffer.DonationOnly)
            {
                ViewBag.DisableRenderTextOutput = "true";
            }

            var eventBranding = ViewBag.Event as Models.Branding.Event;
            ViewBag.AmountRaisedIndicator = new AmountRaisedIndicator
            {
                Goal = eventBranding.Goal,
                IsCampaignManagerView = true,
                ThermometerPercentage =
                  eventBranding.Goal > 0M
                     ? (int)(eventBranding.TotalAmount / eventBranding.Goal * 100.0M)
                     : eventBranding.TotalAmount > 0M ? 100 : 0
            };
            using (var dataProvider = new DataProvider())
            {
                var @user = ViewBag.User as Models.Branding.User;
                var userFound = dataProvider.users.Single(p => p.user_id == @user.Id);
                if (userFound.is_first_login != null && (bool)userFound.is_first_login)
                {
                    ViewBag.ShowFacebookLikeModal = 1;
                    userFound.is_first_login = false;
                    dataProvider.SaveChanges();
                }
                else
                {
                    ViewBag.ShowFacebookLikeModal = 0;
                }

                ViewBag.HasInvalidContacts = (from u in dataProvider.users
                                              from m in dataProvider.members
                                              from mh in dataProvider.member_hierarchy
                                              from ep in dataProvider.event_participation
                                              where u.user_id == @user.Id && u.user_id == m.user_id
                                                    && m.member_id == mh.member_id
                                                    && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                    && ep.event_id == eventBranding.Id
                                                    && ep.participation_channel_id == 1
                                                    && mh.active
                                                    && !m.deleted
                                                    && m.bounced
                                              select m.member_id).Any();

                ViewBag.NumberOfContactsBeingCreated =
                   dataProvider.touch_emails_process_queue.Count(
                      p => p.event_id == eventBranding.Id && p.status == (int)touch_email_process_queue_status.Scheduled);
                ViewBag.HasContactsFlagged =
                   dataProvider.touch_emails_process_queue.Any(
                      p => p.event_id == eventBranding.Id && p.status == (int)touch_email_process_queue_status.Flagged);
                ViewBag.HasImportedContacts =
                   ActionHelper.FindActions(user.EventParticipationId)
                      .Any(p => p.action == (int)ActionEnum.ImportedContacts) || ViewBag.HasContactsFlagged ||
                   ViewBag.NumberOfContactsBeingCreated > 0;
            }

            return View();
        }

        public ActionResult Campaigns()
        {
            var user = ViewBag.User as Models.Branding.User;

            ViewBag.HideCreateCampaign = false;
            var partner = ViewBag.Partner as Partner;
            if (!IsInternalUser(Request.UserHostAddress))
            {
                if (partner.DoRedirectToLandingPage && partner.ESubsUrl.IsNotEmpty())
                {
                    ViewBag.HideCreateCampaign = true;
                }
                if (IsParticipantUserOnly(user))
                {
                    ViewBag.HideCreateCampaign = true;
                }
            }

            // Must groups from ALL partners
            ViewBag.HideMenu = true;
            using (var dataProvider = new DataProvider())
            {
                var events = (from u in dataProvider.users
                              from m in dataProvider.members
                              from mh in dataProvider.member_hierarchy
                              from cc in dataProvider.creation_channel
                              from mt in dataProvider.member_type
                              from ep in dataProvider.event_participation
                              from e in dataProvider.events
                              from eg in dataProvider.event_group
                              from g in dataProvider.groups
                              where u.user_id == user.Id
                                    && m.user_id == u.user_id
                                    && m.member_id == mh.member_id
                                    && mh.creation_channel_id == cc.creation_channel_id
                                    && cc.member_type_id == mt.member_type_id
                                    && mh.member_hierarchy_id == ep.member_hierarchy_id
                                    && ep.event_id == e.event_id
                                    && e.event_id == eg.event_id
                                    && eg.group_id == g.group_id
                                    && e.active
                                    && !m.deleted
                              select
                              new Models.Views.Event
                              {
                                  Id = e.event_id,
                                  Create = e.create_date,
                                  IsActive = e.active,
                                  Name = e.event_name,
                                  UserType = mt.member_type_id == 1 ? "Administrator" : "Member",
                                  ParticipantId = ep.event_participation_id
                              }).ToList();
                return View(events);
            }
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

        /// <summary>
        /// Returns true if user email and password is a participant user only (not used for any sponsors)
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>bool</returns>
        private bool IsParticipantUserOnly(Models.Branding.User user)
        {
            using (var dataProvider = new DataProvider())
            {
                dataProvider.Configuration.LazyLoadingEnabled = false;
                dataProvider.Configuration.AutoDetectChangesEnabled = false;
                return (from u in dataProvider.users
                        from m in dataProvider.members
                        from mh in dataProvider.member_hierarchy
                        from cc in dataProvider.creation_channel
                        from mt in dataProvider.member_type
                        where u.email_address == user.Email && u.password == user.Password
                              && m.user_id == u.user_id
                              && m.member_id == mh.member_id
                              && mh.creation_channel_id == cc.creation_channel_id
                              && cc.member_type_id == mt.member_type_id
                              && mt.member_type_id == 1
                              && !m.deleted
                        select mt.member_type_id).Any();
                //return sponsor == default(short);
            }
        }

        [RenderEventBranding]
        public ActionResult Information(int participantId)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var user = ViewBag.User as Models.Branding.User;
                var model = (
                   from ep in dataProvider.event_participation
                   from mh in dataProvider.member_hierarchy
                   from m in dataProvider.members
                   where
                   ep.member_hierarchy_id == mh.member_hierarchy_id
                   && mh.member_id == m.member_id
                   && ep.event_participation_id == @event.ParticipantId
                   select new Information
                   {
                       Email = m.email_address,
                       FirstName = m.first_name,
                       LastName = m.last_name,
                       ChangePassword = false,
                       Newsletter = m.unsubscribe == null || !(bool)m.unsubscribe,
                       CampaignName = @event.Name,
                       ParticipantId = ep.event_participation_id
                   }).First();
                if (user.IsSponsor)
                {
                    var @results = (
                       from e in dataProvider.events
                       from eg in dataProvider.event_group
                       from g in dataProvider.groups
                       from pi in dataProvider.payment_info
                       from p in dataProvider.phone_number
                       from pc in dataProvider.postal_address
                       where e.event_id == @event.Id
                             && e.event_id == eg.event_id
                             && g.group_id == eg.group_id
                             && g.group_id == pi.group_id
                             && e.event_id == pi.event_id
                             && pi.phone_number_id == p.phone_number_id
                             && pi.postal_address_id == pc.postal_address_id
                             && pi.active
                       select new
                       {
                           Address = pc.address_1,
                           CampaignName = e.event_name,
                           City = pc.city,
                           PaymentType = 0,
                           PayTo = pi.payment_name,
                           Phone = p.phone_number1,
                           State = pc.subdivision_code,
                           ZipCode = pc.zip_code
                       }).FirstOrDefault();
                    if (@results != null)
                    {
                        model.Address = @results.Address;
                        model.CampaignName = @results.CampaignName;
                        model.City = @results.City;
                        model.PaymentType = @results.PaymentType;
                        model.PayTo = @results.PayTo;
                        model.Phone = @results.Phone;
                        model.State = @results.State;
                        model.ZipCode = @results.ZipCode;
                        model.PaymentType = @results.PayTo == string.Concat(model.FirstName, " ", model.LastName)
                           ? 1
                           : @results.PayTo == model.CampaignName ? 2 : 3;
                    }
                }
                return View(model);
            }
        }

        [HttpPost, RenderEventBranding]
        public JsonResult Information(int participantId, Information model)
        {
            var cmUser = ViewBag.User as Models.Branding.User;
            var @event = ViewBag.Event as Models.Branding.Event;
            var @partner = ViewBag.Partner as Models.Branding.Partner;
            var partnerId = @partner.Id;
            int groupId = 0, leadId = 0, paymentInfoId = 0;
            string userName = model.Email, password = "";
            string payeeName = "";
            var paymentToPartner = @partner.PaymentTo == PaymentTo.Partner;

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
                var success = false;
                if (cmUser.IsParticipant)
                {
                    var acceptedErrors = new[] { "City", "Address", "ZipCode", "State", "CampaignName", "PaymentType" };
                    if (acceptedErrors.Any(p => errorMessage.Contains(p)))
                    {
                        success = true;
                    }
                }
                else
                {
                    return new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { success = false, responseText = errorMessage }
                    };
                }
                if (!success)
                {
                    return new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { success = false, responseText = errorMessage }
                    };
                }
            }
            if (model.ChangePassword &&
                (string.IsNullOrEmpty(model.CurrentPassword) || string.IsNullOrEmpty(model.NewPassword1) ||
                 string.IsNullOrEmpty(model.NewPassword2) || model.NewPassword1 != model.NewPassword2))
            {
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { success = false, responseText = "The passwords can't be empty." }
                };
            }
            if (model.ChangePassword)
            {
                using (var dataProvider = new DataProvider())
                {
                    var user = (from u in dataProvider.users
                                from m in dataProvider.members
                                from mh in dataProvider.member_hierarchy
                                from ep in dataProvider.event_participation
                                where u.user_id == m.user_id
                                      && m.member_id == mh.member_id
                                      && mh.member_hierarchy_id == ep.member_hierarchy_id
                                      && ep.event_id == @event.Id
                                      && !m.deleted
                                      && ep.event_participation_id == participantId
                                select u).Single();
                    if (model.CurrentPassword != user.password)
                    {
                        return new JsonResult
                        {
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                            Data = new { success = false, responseText = "Your current password is incorrect." }
                        };
                    }
                }
            }


            if (cmUser.IsSponsor)
            {
                if (!paymentToPartner && !model.IgnoreAddressHygiene)
                {
                    var addressHygieneHelper = new AddressHygieneHelper();
                    var proposedAddress =
                       addressHygieneHelper.SendAddress(new Address
                       {
                           City = model.City,
                           Address1 = model.Address,
                           Country = "US",
                           PostCode = model.ZipCode,
                           Region = model.State.Replace("US-", "")
                       });
                    proposedAddress.Region = "US-" + proposedAddress.Region;
                    return new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { success = false, proposedAddress }
                    };
                }
                using (var dataProvider = new DataProvider())
                {
                    using (var transactionScope = new TransactionScope())
                    {
                        var user = (from u in dataProvider.users
                                    where u.user_id == cmUser.Id
                                    select u).Single();
                        user.first_name = model.FirstName;
                        user.last_name = model.LastName;
                        user.email_address = model.Email;
                        user.username = model.Email;
                        user.unsubscribe = !model.Newsletter;
                        password = user.password;
                        if ((bool)user.unsubscribe)
                        {
                            user.unsubscribe_date = DateTime.Now;
                        }
                        var member = (from m in dataProvider.members
                                      from mh in dataProvider.member_hierarchy
                                      from ep in dataProvider.event_participation
                                      where m.member_id == mh.member_id
                                            && mh.member_hierarchy_id == ep.member_hierarchy_id
                                            && ep.event_id == @event.Id
                                            && ep.participation_channel_id == 3
                                            && m.user_id == user.user_id
                                            && !m.deleted
                                      select m).Single();
                        member.first_name = model.FirstName;
                        member.last_name = model.LastName;
                        member.email_address = model.Email;
                        member.unsubscribe = !model.Newsletter;
                        if ((bool)member.unsubscribe)
                        {
                            member.unsubscribe_date = DateTime.Now;
                        }
                        if (model.ChangePassword)
                        {
                            user.password = model.NewPassword1;
                            member.password = model.NewPassword1;
                        }
                        groupId = dataProvider.event_group.Single(p => p.event_id == @event.Id).group_id;
                        var eventFound = dataProvider.events.Single(p => p.event_id == @event.Id);
                        var groupFound = dataProvider.groups.Single(p => p.group_id == groupId);
                        leadId = groupFound.lead_id.HasValue ? groupFound.lead_id.Value : int.MinValue;
                        eventFound.event_name = model.CampaignName;

                        payment_info paymentInfo = null;
                        if (!paymentToPartner)
                        {
                            paymentInfo = (from e in dataProvider.events
                                           from pi in dataProvider.payment_info
                                           where e.event_id == pi.event_id
                                                 && e.event_id == @event.Id
                                                 && pi.active
                                           select pi).SingleOrDefault();
                            if (paymentInfo == null)
                            {
                                paymentInfo = new payment_info
                                {
                                    active = true,
                                    create_date = System.DateTime.Now,
                                    event_id = @event.Id,
                                    group_id = groupId
                                };
                                dataProvider.payment_info.Add(paymentInfo);
                            }
                            payeeName =
                               paymentInfo.payment_name =
                                  paymentInfo.on_behalf_of_name =
                                     model.PaymentType == 1
                                        ? string.Concat(model.FirstName, " ", model.LastName)
                                        : model.PaymentType == 2 ? model.CampaignName : model.PayTo;
                            var postalAddress = (from e in dataProvider.events
                                                 from pi in dataProvider.payment_info
                                                 from pa in dataProvider.postal_address
                                                 where e.event_id == pi.event_id
                                                       && pi.postal_address_id == pa.postal_address_id
                                                       && e.event_id == @event.Id
                                                       && pi.active
                                                 select pa).SingleOrDefault();
                            if (postalAddress == null)
                            {
                                postalAddress = new postal_address { create_date = DateTime.Now };
                                dataProvider.postal_address.Add(postalAddress);
                            }


                            var address = (from pa in dataProvider.postal_address_validation
                                           select pa).ToList();
                            var count = 0;
                            foreach (var addresscheck in address)
                            {
                                //do comparision on address before saving
                                if (addresscheck.address_1.ToLower() == model.Address.ToLower())
                                {
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                return new JsonResult
                                {
                                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                                    Data = new { success = false, responseText = "Sorry!, We are a unable to save your changes. Please contact customer support at 1-877-275-8664 for further assistance." }
                                };
                            }

                            if (model.State.ToLower() == "us-ga")
                            {
                                return new JsonResult
                                {
                                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                                    Data = new { success = false, responseText = "Sorry!, We are a unable to save your changes. Please contact customer support at 1-877-275-8664 for further assistance." }
                                };
                            }



                            postalAddress.address_1 = model.Address;
                            postalAddress.city = model.City;
                            postalAddress.country_code = "US";
                            postalAddress.zip_code = model.ZipCode;
                            postalAddress.subdivision_code = model.State;
                            postalAddress.matching_code = GetMatchingCode(model.Address, model.ZipCode);
                            var phoneNumber = (from e in dataProvider.events
                                               from pi in dataProvider.payment_info
                                               from pn in dataProvider.phone_number
                                               where e.event_id == pi.event_id
                                                     && pi.phone_number_id == pn.phone_number_id
                                                     && e.event_id == @event.Id
                                                     && pi.active
                                               select pn).FirstOrDefault() ?? new phone_number { create_date = DateTime.Now };
                            phoneNumber.phone_number1 = string.IsNullOrEmpty(model.Phone) ? "000000000" : model.Phone;
                        }


                       

                        dataProvider.SaveChanges();
                        transactionScope.Complete();
                        if (paymentInfo != null)
                            paymentInfoId = paymentInfo.payment_info_id;
                    }
                }
                if (Convert.ToBoolean(@partner.InsertAsLead) && @partner.Program != Program.Mvp)
                {
                    Shared.Entities.Lead lead;
                    if (leadId > 0)
                    {
                        using (var dataProvider = new DataProvider())
                        {
                            var payment = dataProvider.payment_info.Single(p => p.payment_info_id == paymentInfoId);
                            if (payment.postal_address != null || payment.phone_number_id.HasValue)
                            {
                                using (var client = new HttpClient())
                                {
                                    var uri =
                                       new Uri(string.Format("{0}/leads?id={1}",
                                          ConfigurationManager.AppSettings["core.webapi.host"], leadId));
                                    lead = client.GetAsync(uri).Result.Content.ReadAsAsync<Shared.Entities.Lead>().Result;
                                    if (payment.postal_address != null)
                                    {
                                        lead.Address.City = payment.postal_address.city;
                                        lead.Address.Country = new Country { Code = payment.postal_address.country_code };
                                        lead.Address.Region = new Region
                                        {
                                            Code = payment.postal_address.subdivision_code.IndexOf("-") != -1
                                              ? payment.postal_address.subdivision_code.Substring(
                                                 payment.postal_address.subdivision_code.IndexOf("-") + 1)
                                              : payment.postal_address.subdivision_code
                                        };
                                        lead.Address.Address1 = payment.postal_address.address_1;
                                        lead.Address.PostCode = payment.postal_address.zip_code;
                                    }
                                    if (payment.phone_number_id.HasValue)
                                    {
                                        var @phone =
                                           dataProvider.phone_number.Single(p => p.phone_number_id == payment.phone_number_id);
                                        lead.Phone = @phone.phone_number1;
                                    }
                                    lead.KitType = 42;
                                    uri =
                                       new Uri(string.Format("{0}/leads", ConfigurationManager.AppSettings["core.webapi.host"]));
                                    var content = client.PutAsJsonAsync(uri, lead).Result.Content;
                                }
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            int promotionId = Session[Constants.SESSION_KEY_PROMOTION_ID] != null
                           ? Convert.ToInt32(Session[Constants.SESSION_KEY_PROMOTION_ID])
                           : GetSelfRegistrationPromotion(partnerId);
                            int fcExternalID = Session[Constants.SESSION_KEY_FCEXTERNAL_ID] != null
                               ? Convert.ToInt32(Session[Constants.SESSION_KEY_FCEXTERNAL_ID])
                               : int.MinValue;
                            var state = model.State;
                            if (state.Length == 5 && state.Contains("-"))
                            {
                                state = state.Substring(state.IndexOf("-") + 1);
                            }
                            lead = new Shared.Entities.Lead
                            {
                                PromotionId = promotionId,
                                Group = model.CampaignName,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                ChannelCode = "INT",
                                Address = new Shared.Entities.Address
                                {
                                    Address1 = "-",
                                    Region = new Region { Code = state },
                                    Country = new Country { Code = "US" },
                                    City = "-",
                                    PostCode = "-"
                                },
                                Phone = model.Phone,
                                Email = model.Email,
                                PartnerId = partnerId,
                                KitType = 43,
                                ConsultantId = fcExternalID > 0 ? fcExternalID : 0
                            };
                            using (var client = new HttpClient())
                            {
                                var uri =
                                   new Uri(string.Format("{0}/leads", ConfigurationManager.AppSettings["core.webapi.host"]));
                                var content = client.PostAsJsonAsync(uri, lead).Result.Content;
                            }
                        }
                        catch { }
                    }

                }

                if (!paymentToPartner && @partner.ProductOffer != ProductOffer.DonationOnly)
                {
                    int? SAPAccountNo = null;
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
                    var state = model.State;
                    // Check if its DEFAULT address, make the state is from New York
                    if (model.Address == "156 Lawrence Paquette Ind'l Dr PMW#5" && model.City == "Champlain" && model.ZipCode == "12919")
                    {
                        state = "US-NY";
                    }
                    string ga_state = (state != null && state.IndexOf("-", StringComparison.Ordinal) != -1) ? state.Split('-').ElementAt(1) : state;

                    var stateProvinceCode = (StateProvince_) Enum.Parse(typeof(StateProvince_), ga_state);

                    using (StoreServiceClient storeProxy = new StoreServiceClient())
                    {

                        storeProxy.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["serviceUN"];
                        storeProxy.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["servicePW"];
                        
                        var request = new AffiliateAddressGraphRequest(address1: model.Address,
                                                                      city: model.City,
                                                                      email: model.Email,
                                                                      externalAffiliateIdentifier: groupId,
                                                                      externalAffiliateName: model.CampaignName,
                                                                      externalPartnerIdentifier: partnerId,
                                                                      payeeName: payeeName,
                                                                      phone: string.IsNullOrEmpty(model.Phone) ? "000000000" : model.Phone,
                                                                      postalCode: model.ZipCode,
                                                                      stateProvinceCode: stateProvinceCode,
                                                                      salesRepSAPAcctNr: SAPAccountNo);
                        var response = storeProxy.UpdateAffiliateAddress(request);
                        var businessRuleViolation = response.businessRuleViolation;
                        if (businessRuleViolation != null)
                        {
                           var storeServiceErrorMessage = businessRuleViolation.ViolationMessage + " (Type=" + businessRuleViolation.BusinessRuleViolationTypeCode + ")";
                           throw new Exception(storeServiceErrorMessage);
                        }
                    }

                }
            }
            else if (cmUser.IsParticipant)
            {
                using (var dataProvider = new DataProvider())
                {
                    var user = (from u in dataProvider.users
                                where u.user_id == cmUser.Id
                                select u).Single();
                    user.first_name = model.FirstName;
                    user.last_name = model.LastName;
                    user.email_address = model.Email;
                    user.username = model.Email;
                    user.unsubscribe = !model.Newsletter;
                    password = user.password;
                    if ((bool)user.unsubscribe)
                    {
                        user.unsubscribe_date = DateTime.Now;
                    }
                    var member = (from m in dataProvider.members
                                  from mh in dataProvider.member_hierarchy
                                  from ep in dataProvider.event_participation
                                  where m.member_id == mh.member_id
                                        && mh.member_hierarchy_id == ep.member_hierarchy_id
                                        && ep.event_id == @event.Id
                                        && ep.participation_channel_id != 3
                                        && m.password != null
                                        && m.user_id == user.user_id
                                        && !m.deleted
                                  select m).Single();
                    member.first_name = model.FirstName;
                    member.last_name = model.LastName;
                    member.email_address = model.Email;
                    member.unsubscribe = !model.Newsletter;
                    if ((bool)member.unsubscribe)
                    {
                        member.unsubscribe_date = DateTime.Now;
                    }
                    if (model.ChangePassword)
                    {
                        user.password = model.NewPassword1;
                        member.password = model.NewPassword1;
                    }
                    dataProvider.SaveChanges();
                }
            }
            WebSecurity.Login(string.Concat(userName, "|", partnerId), password, true);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Your information has been successfully updated.", participantId } };
        }
        private string GetMatchingCode(string address, string zipCode)
        {
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                return dataProvider.es_generate_matching_code(address, zipCode);
            }
        }
        /// <summary>
        /// Returns a self-registered promotion Id
        /// </summary>
        /// <param name="partnerId">Partner Id</param>
        /// <returns></returns>
        private int GetSelfRegistrationPromotion(int partnerId)
        {
            int promotionId;
            using (var dataProvider = new Data.MGP.EFRCommon.Models.DataProvider())
            {
                promotionId = (from pr in dataProvider.promotions
                               from pp in dataProvider.partner_promotion
                               where pr.promotion_id == pp.promotion_id
                                  && pp.partner_id == partnerId
                                  && pr.promotion_type_code == "ON"
                               select pr.promotion_id).FirstOrDefault();
                if (promotionId < 0)
                    promotionId = 3136;
            }
            return promotionId;
        }

        [RenderEventBranding, HttpGet, RenderOutputText]
        public ActionResult Page(int participantId)
        {
            var partner = ViewBag.Partner as Partner;

            if (partner.ProductOffer != ProductOffer.DonationOnly)
            {
                ViewBag.DisableRenderTextOutput = "true";
            }

            using (var dataProvider = new DataProvider())
            {
                var user = ViewBag.User as Models.Branding.User;
                var eventBranding = ViewBag.Event as Models.Branding.Event;
                var memberTypeId = user.IsSponsor ? 1 : 2;
                var result = (from p in dataProvider.personalizations
                              from ep in dataProvider.event_participation
                              from mh in dataProvider.member_hierarchy
                              from cc in dataProvider.creation_channel
                              from m in dataProvider.members
                              where p.event_participation_id == ep.event_participation_id
                                    && ep.member_hierarchy_id == mh.member_hierarchy_id
                                    && mh.member_id == m.member_id
                                    && mh.creation_channel_id == cc.creation_channel_id
                                    && ep.event_id == eventBranding.Id
                                    && m.user_id == user.Id
                                    && cc.member_type_id == memberTypeId
                                    && (ep.event_participation_id == user.EventParticipationId ||
                                        user.EventParticipationId == 0)
                              select new
                              {
                                  Personalization = new Personalization
                                  {
                                      Id = p.personalization_id,
                                      Body = p.body,
                                      Goal = p.fundraising_goal == null ? 0.0 : (double)p.fundraising_goal,
                                      Redirect = p.redirect
                                  },
                                  PersonalizationImage = p.personalization_image
                              }).Single();
                result.Personalization.Body = result.Personalization.Body.ReplaceSingleQuoteToAlternativeVersion();
                var personalization = result.Personalization;
                personalization.Body = personalization.Body.ReplaceNewLineToBR();
                if (user.IsSponsor)
                {
                    if (eventBranding.EventType != EventTypeInfo.INDIVIDUAL_FUNDRAISER)
                    {
                        personalization.DefaultImage = string.Format("{0}/{1}",
                                                            Settings.Default.PersonalizationImageDirectory,
                                                            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);
                    }
                    else
                    {
                        personalization.DefaultImage = string.Format("{0}/{1}",
                                                        Settings.Default.PersonalizationImageDirectory,
                                                        Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);
                    }
                }
                else if (user.IsParticipant)
                {
                    personalization.DefaultImage = string.Format("{0}/{1}",
                                                        Settings.Default.PersonalizationImageDirectory,
                                                        Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);
                    var sponsorPersonalization = (from mh in dataProvider.member_hierarchy
                                                  from ep in dataProvider.event_participation
                                                  from p in dataProvider.personalizations
                                                  where mh.member_hierarchy_id == ep.member_hierarchy_id
                                                     && ep.event_participation_id == p.event_participation_id
                                                     && ep.event_id == eventBranding.Id
                                                     && ep.participation_channel_id == 3
                                                  select p).Single();
                    ViewBag.SponsorRedirect = sponsorPersonalization.redirect;
                }
                personalization.PersonalizationImages = result.PersonalizationImage.Any(p => p.image_approval_status_id != 4 && !p.deleted)
                                                         ? result.PersonalizationImage
                                                           .Where(p => p.image_approval_status_id != 4 && !p.deleted)
                                                           .OrderByDescending(p => p.isCoverAlbum)
                                                           .Select(p => new PersonalizationImage
                                                           {
                                                               ImageId = p.image_id,
                                                               PersonalizationId = p.personalization_id,
                                                               IsCoverAlbum = p.isCoverAlbum.HasValue ? p.isCoverAlbum.Value : false,
                                                               ImageURL = p.image_url.TransformToMGPImagePath()
                                                           }).ToArray()
                                                         : (new List<PersonalizationImage>
                                                               {
                                                               new PersonalizationImage
                                                               {
                                                                   ImageId = 0,
                                                                   PersonalizationId = 0,
                                                                   ImageURL = personalization.DefaultImage
                                                               }
                                                               }).ToArray();
                return View(personalization);
            }
        }

        [RenderEventBranding, HttpGet]
        public ActionResult Promote(int participantId)
        {
            var model = new Promote();
            return View(model);
        }

        [HttpPost, RenderEventBranding]
        public JsonResult GetBannerLink(int participantId, int bannerType)
        {
            var eventBranding = ViewBag.Event as Models.Branding.Event;
            var result = string.Empty;
            if (bannerType < 4)
            {
                var bannerLink = string.Empty;
                switch (bannerType)
                {
                    case 1:
                        bannerLink = "banner_468x60_small.jpg";
                        break;
                    case 2:
                        bannerLink = "banner_234x60.jpg";
                        break;
                    case 3:
                        bannerLink = "banner_120x120.jpg";
                        break;
                    default:
                        bannerLink = "banner_468x60_small.jpg";
                        break;
                }
                result =
                    string.Format(
                        "<a href='http://{0}/{1}?utm_source=selfpromotion&utm_medium=banner&utm_campaign={4}'><img src='http://www.efundraising.com/Content/images/groups/banners/{2}' alt='{3}' /></a>",
                        Request.Url.Host, eventBranding.Redirect, bannerLink, eventBranding.Name, eventBranding.Id);
            }
            else
            {
                result =
                    string.Format(
                        "<a href='http://{0}/{1}?utm_source=selfpromotion&utm_medium=banner&utm_campaign={3}'>Support our group {2}. Please click here.</a>", Request.Url.Host, eventBranding.Redirect, eventBranding.Name, eventBranding.Id);
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, bannerName = result } };
        }

        [RenderEventBranding, HttpGet]
        public ActionResult Prizes(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var eventBranding = ViewBag.Event as Models.Branding.Event;
            if (user.IsSponsor)
            {
                using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
                {
                    var earned = new List<ParticipantPrize>();
                    var notEarned = new List<ParticipantPrize>();
                    var results = dataProvider.es_rpt_event_member_prize_stats(eventBranding.Id);
                    foreach (var result in results)
                    {
                        if (result.nb_supp >= 12 && result.nb_subs >= 1)
                        {
                            earned.Add(new ParticipantPrize
                            {
                                Name = result.first_name + " " + result.last_name,
                                NumberOfEmail = result.nb_supp.HasValue ? result.nb_supp.Value : 0,
                                NumberOfSubscriptionSold = result.nb_subs.HasValue ? result.nb_subs.Value : 0
                            });
                        }
                        else
                        {
                            notEarned.Add(new ParticipantPrize
                            {
                                Name = result.first_name + " " + result.last_name,
                                NumberOfEmail = result.nb_supp.HasValue ? result.nb_supp.Value : 0,
                                NumberOfSubscriptionSold = result.nb_subs.HasValue ? result.nb_subs.Value : 0
                            });
                        }
                    }
                    ViewBag.PrizeEarned = earned;
                    ViewBag.PrizeNotEarned = notEarned;
                }
            }
            else if (user.IsParticipant)
            {
                using (var dataProvider = new DataProvider())
                {
                    var results = (from ep in dataProvider.earned_prize
                                   where ep.event_participation_id == user.EventParticipationId
                                   select new
                                   {
                                       PrizeItemID = ep.prize_item_id,
                                       EventParticipationId = ep.event_participation_id
                                   }).ToList();
                    var prizeQualifyText = string.Empty;
                    var prizeQualifyLevel = 1;
                    if (results.Any())
                    {
                        prizeQualifyLevel = 3;
                        foreach (var result in results)
                        {
                            var prize = (from pi in dataProvider.prize_item
                                         where pi.prize_item_id == result.PrizeItemID
                                         select pi).Single();
                            if (prize.prize_id == 13)
                            {
                                ViewBag.GASavingPass = prize.prize_item_code;
                            }
                        }
                    }
                    else
                    {
                        var suppInvited = eventBranding.SupportersInvited;
                        bool supporterInvided = (suppInvited.Count() >= 12);
                        bool generatedSales = (eventBranding.TotalNumberOfItemSold >= 1);

                        prizeQualifyLevel = supporterInvided && generatedSales ? 2 : 1;

                        string desc = string.Empty, num_subs_text = string.Empty;
                        if (!supporterInvided && !generatedSales)
                        {
                            prizeQualifyText = "Send <strong>[++REMINDER_EMAIL++]</strong> more email(s) and make just <strong>[++MIN_SUBS_TEXT++]</strong> to be eligible for your prize.";
                        }
                        else if (!supporterInvided)
                        {
                            prizeQualifyText = "You've made <strong>[++NUM_SUBS_TEXT++]</strong>. Now send just <strong>[++REMINDER_EMAIL++]</strong> more email(s) to be eligible for your prize.";
                        }
                        else if (!generatedSales)
                        {
                            prizeQualifyText = "You've sent <strong>[++NUM_EMAIL_SENT++]</strong> emails. Now make just <strong>[++MIN_SUBS_TEXT++]</strong> to be eligible for your prize.";
                        }
                        else
                        {
                            prizeQualifyText = "Congratulations! You've sent <span class='greenBold'>[++NUM_EMAIL_SENT++] emails</span> and made <span class='greenBold'>[++NUM_SUBS_TEXT++]</span>. Redeem your prize now.";
                        }

                        int reminderEmail = 12 - suppInvited.Count();
                        if (reminderEmail < 0)
                            reminderEmail = 0;

                        if (eventBranding.TotalNumberOfItemSold == 1)
                            num_subs_text = "1 sale";
                        else
                            num_subs_text = eventBranding.TotalNumberOfItemSold + " sales";

                        prizeQualifyText = prizeQualifyText.Replace("[++REMINDER_EMAIL++]", reminderEmail.ToString()).Replace("[++MIN_SUBS_TEXT++]", "1 sale").Replace("[++NUM_EMAIL_SENT++]", suppInvited.Count().ToString()).Replace("[++NUM_SUBS_TEXT++]", num_subs_text);
                    }
                    ViewBag.PrizeQualifyLevel = prizeQualifyLevel;
                    ViewBag.PrizeQualifyText = prizeQualifyText;
                }
            }
            return View();
        }
        [HttpPost, RenderEventBranding]
        public JsonResult RedeemPrize(int participantId)
        {
            using (var dataProvider = new DataProvider())
            {
                var currentPrize = (from prizeItem in dataProvider.prize_item
                                    from earnedPrize in dataProvider.earned_prize
                                    where earnedPrize.prize_item_id == prizeItem.prize_item_id
                                          && prizeItem.prize_id == 13
                                    select earnedPrize).FirstOrDefault();
                if (currentPrize == null)
                {
                    var earnedPrize = (from pi in dataProvider.prize_item
                                       join ep in dataProvider.earned_prize
                                         on pi.prize_item_id equals ep.prize_item_id
                                       into a
                                       from b in a.DefaultIfEmpty()
                                       where pi.prize_id == 13
                                       select pi).ToList();
                    if (earnedPrize.Any())
                    {
                        dataProvider.earned_prize.Add(new earned_prize
                        {
                            prize_item_id = earnedPrize.First().prize_item_id,
                            event_participation_id = participantId,
                            create_date = DateTime.Now
                        });
                        dataProvider.SaveChanges();
                    }
                    else
                    {
                        return new JsonResult { Data = new { success = false, participantId, responseText = "Thank you for your participation! Your voucher is temporarily not available to download. It will be available before the end of the month. We apologize for the inconvenience, and in the meantime, should you need assistance, please contact on-line support at 1-800-678-2673" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }


            }
            var url = new UrlHelper(Request.RequestContext).Action("Prizes", "CampaignManager", new { participantId });
            return new JsonResult { Data = new { success = true, participantId, url }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [RenderEventBranding, HttpGet, RenderOutputText]
        public ActionResult Register(int participantId)
        {
            ViewBag.HideMenu = true;
            var partner = ViewBag.Partner as Partner;

            if (partner.ProductOffer != ProductOffer.DonationOnly)
            {
                ViewBag.DisableRenderTextOutput = "true";
            }

            using (var dataProvider = new DataProvider())
            {
                var user = ViewBag.User as Models.Branding.User;
                var eventBranding = ViewBag.Event as Models.Branding.Event;
                var result = (from p in dataProvider.personalizations
                              from ep in dataProvider.event_participation
                              from mh in dataProvider.member_hierarchy
                              from m in dataProvider.members
                              where p.event_participation_id == ep.event_participation_id
                                    && ep.member_hierarchy_id == mh.member_hierarchy_id
                                    && mh.member_id == m.member_id
                                    && ep.event_id == eventBranding.Id
                                    && m.user_id == user.Id
                                    && !m.deleted
                                    && (ep.event_participation_id == user.EventParticipationId ||
                                        user.EventParticipationId == 0)
                              select new
                              {
                                  Personalization = new Personalization
                                  {
                                      Id = p.personalization_id,
                                      Body = p.body,
                                      Goal = (double)p.fundraising_goal,
                                      Redirect = p.redirect
                                  },
                                  PersonalizationImage = p.personalization_image
                              }).Single();
                result.Personalization.Body = result.Personalization.Body.ReplaceSingleQuoteToAlternativeVersion();
                var personalization = result.Personalization;
                personalization.Body = personalization.Body.ReplaceNewLineToBR();
                if (user.IsSponsor)
                {
                    if (eventBranding.EventType != EventTypeInfo.INDIVIDUAL_FUNDRAISER)
                    {
                        personalization.DefaultImage = string.Format("{0}/{1}",
                                                            Settings.Default.PersonalizationImageDirectory,
                                                            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);
                    }
                    else
                    {
                        personalization.DefaultImage = string.Format("{0}/{1}",
                                                        Settings.Default.PersonalizationImageDirectory,
                                                        Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);
                    }
                }
                else if (user.IsParticipant)
                {
                    personalization.DefaultImage = string.Format("{0}/{1}",
                                                        Settings.Default.PersonalizationImageDirectory,
                                                        Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);
                    var sponsorPersonalization = (from mh in dataProvider.member_hierarchy
                                                  from ep in dataProvider.event_participation
                                                  from p in dataProvider.personalizations
                                                  where mh.member_hierarchy_id == ep.member_hierarchy_id
                                                     && ep.event_participation_id == p.event_participation_id
                                                     && ep.event_id == eventBranding.Id
                                                     && ep.participation_channel_id == 3
                                                  select p).Single();
                    ViewBag.SponsorRedirect = sponsorPersonalization.redirect;
                }
                personalization.PersonalizationImages = result.PersonalizationImage.Any(p => p.image_approval_status_id != 4 && !p.deleted)
                                                         ? result.PersonalizationImage
                                                           .Where(p => p.image_approval_status_id != 4 && !p.deleted)
                                                           .OrderByDescending(p => p.isCoverAlbum)
                                                           .Select(x => new PersonalizationImage
                                                           {
                                                               ImageId = x.image_id,
                                                               PersonalizationId = x.personalization_id,
                                                               IsCoverAlbum = x.isCoverAlbum.HasValue ? x.isCoverAlbum.Value : false,
                                                               ImageURL = x.image_url.TransformToMGPImagePath()
                                                           }).ToArray()
                                                         : (new List<PersonalizationImage>
                                                               {
                                                               new PersonalizationImage
                                                               {
                                                                   ImageId = 0,
                                                                   PersonalizationId = 0,
                                                                   ImageURL = personalization.DefaultImage
                                                               }
                                                               }).ToArray();
                personalization.PersoImgsJSON = JsonConvert.SerializeObject(personalization.PersonalizationImages);
                return View(personalization);
            }
        }

        [HttpPost, RenderEventBranding]
        public JsonResult Register(int participantId, Personalization model)
        {
            var user = ViewBag.User as Models.Branding.User;
            var eventBranding = ViewBag.Event as Models.Branding.Event;
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
            }
            using (var dataProvider = new DataProvider())
            {
                //Check again if redirect is valid
                bool redirectExists = false;
                if (user.IsSponsor)
                {
                    redirectExists = (from p in dataProvider.personalizations
                                      from ep in dataProvider.event_participation
                                      from e in dataProvider.events
                                      from mh in dataProvider.member_hierarchy
                                      from m in dataProvider.members
                                      where p.personalization_id != model.Id
                                         && p.event_participation_id == ep.event_participation_id
                                         && ep.participation_channel_id == 3
                                         && ep.event_id == e.event_id
                                         && e.active
                                         && e.event_id != eventBranding.Id
                                         && ep.member_hierarchy_id == mh.member_hierarchy_id
                                         && mh.member_id == m.member_id
                                         && !m.deleted
                                         && p.redirect == model.Redirect
                                      select p.personalization_id).Any();
                }
                else if (user.IsParticipant)
                {
                    redirectExists = (from p in dataProvider.personalizations
                                      from ep in dataProvider.event_participation
                                      from e in dataProvider.events
                                      from mh in dataProvider.member_hierarchy
                                      from m in dataProvider.members
                                      where p.personalization_id != model.Id
                                         && p.event_participation_id == ep.event_participation_id
                                         && ep.participation_channel_id != 3
                                         && ep.event_id == e.event_id
                                         && e.active
                                         && e.event_id == eventBranding.Id
                                         && ep.member_hierarchy_id == mh.member_hierarchy_id
                                         && mh.member_id == m.member_id
                                         && !m.deleted
                                         && p.redirect == model.Redirect
                                      select p.personalization_id).Any();
                }
                if (redirectExists)
                {
                    return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "The web address is not available" } };
                }

                var personalization = (from p in dataProvider.personalizations
                                       where p.personalization_id == model.Id
                                       select p).Single();
                personalization.body = model.Body;
                personalization.fundraising_goal = (decimal)model.Goal;
                personalization.redirect = model.Redirect.CleanupRedirect();

                //model.PersonalizationImages = model.PersonalizationImages.Where(p => p.ImageURL != model.DefaultImage && p.ImageId == 0).ToArray();
                foreach (var pi in dataProvider.personalization_image.Where(p => p.personalization_id == personalization.personalization_id).ToList())
                {
                    dataProvider.Entry(pi).State = EntityState.Deleted;
                    personalization.personalization_image.Remove(pi);
                }
                foreach (var persoImg in model.PersonalizationImages)
                {
                    string monthDirectory = string.Empty, targetSaveDirectory = string.Empty, targetWebPath = string.Empty;
                    if (persoImg.ImageURL.StartsWith(Settings.Default.UploadedFileTempPath, StringComparison.InvariantCultureIgnoreCase))
                    {
                        var pathGroup = Server.MapPath(string.Concat(Settings.Default.ImagesVirtualDirectory, Settings.Default.GroupFileShareDirectory));
                        targetSaveDirectory = ImageHelper.GetMonthPersonalizationFolder(pathGroup, personalization.personalization_id, out monthDirectory);
                        targetWebPath = string.Concat(Settings.Default.ImagesVirtualDirectory, Settings.Default.GroupFileShareDirectory, monthDirectory, "/", personalization.personalization_id);
                        targetWebPath = targetWebPath.Replace('\\', '/');
                        FileInfo fi = new FileInfo(Server.MapPath(persoImg.ImageURL));
                        if (fi.Exists)
                        {
                            if (fi.Directory.Name == "temp")
                            {
                                targetWebPath = string.Concat(targetWebPath, "/", fi.Name);
                                var copyToTarget = string.Concat(targetSaveDirectory, "\\", fi.Name);
                                fi.CopyTo(copyToTarget, true);
                                fi.Delete();
                            }
                        }
                    }
                    else
                    {
                        targetWebPath = persoImg.ImageURL;
                    }
                    var pi = new personalization_image
                    {
                        image_approval_status_id = 3,
                        image_url = targetWebPath,
                        isCoverAlbum = true,
                        personalization_id = personalization.personalization_id,
                        create_date = System.DateTime.Now
                    };
                    dataProvider.personalization_image.Add(pi);
                }
                dataProvider.SaveChanges();

                // Refresh personalizationImages
                var image = new PersonalizationImage
                {
                    ImageId = 0,
                    PersonalizationId = 0,
                    IsCoverAlbum = true,
                    ImageURL = eventBranding.EventType != EventTypeInfo.INDIVIDUAL_FUNDRAISER ? $"{Settings.Default.PersonalizationImageDirectory}/{Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME}" : $"{Settings.Default.PersonalizationImageDirectory}/{Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME}"
                };
                if (model.PersonalizationImages.Any() &&
                    this.HttpContext.Request.UrlReferrer != null &&
                    this.HttpContext.Request.UrlReferrer.AbsolutePath.EndsWith("Page"))
                {
                    var p = personalization.personalization_image.First();
                    image = new PersonalizationImage
                    {
                        ImageId = p.image_id,
                        PersonalizationId = p.personalization_id,
                        IsCoverAlbum = p.isCoverAlbum.HasValue ? p.isCoverAlbum.Value : false,
                        ImageURL = p.image_url.TransformToMGPImagePath()
                    };
                }

                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId, image, responseText = "Your information has been successfully updated." } };
            }
        }

        [HttpPost]
        public JsonResult ImageFileUpload(HttpPostedFileBase file)
        {
            string savedImagePath = string.Empty, message = string.Empty;
            bool isSuccess = true;

            ImageHelper imgHelper = new ImageHelper();
            System.Drawing.Size newsize = new System.Drawing.Size(320, 320);
            byte[] bufferResize = imgHelper.Resize("jpeg", file, newsize);

            var filename = Guid.NewGuid().ToString().FormatGUID() + System.IO.Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath(Settings.Default.UploadedFileTempPath), filename);
            imgHelper.WriteByteArrayToFile(bufferResize, path);
            savedImagePath = string.Concat(Settings.Default.UploadedFileTempPath, "/", filename);

            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = isSuccess, savedImagePath, responseText = message } };
        }

        [HttpPost]
        public JsonResult CsvFileUpload(UploadedCSV csv)
        {
            string message = string.Empty;
            bool isSuccess = true;
            var importModel = new ImportAddressBookModel(ImportAddressBookModel.ProviderType.CSV);
            if (csv.CsvFile != null && csv.CsvFile.ContentLength > 0)
            {
                var filePath = string.Empty;
                try
                {
                    var fileName = Guid.NewGuid().ToString().FormatGUID() + Path.GetFileName(csv.CsvFile.FileName);
                    filePath = Path.Combine(Server.MapPath($"~/{Settings.Default.UploadedFileTempPath}"), fileName);
                    csv.CsvFile.SaveAs(filePath);
                    importModel.ProcessImport(filePath);
                    System.IO.File.Delete(filePath);
                }
                catch
                {
                    isSuccess = false;
                    message = "Failed to upload csv file";
                    System.IO.File.Delete(filePath);
                }
            }
            else
            {
                isSuccess = false;
                message = "Failed to upload csv file";
            }
            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    success = isSuccess,
                    responseText = message,
                    contacts = importModel.ContactsLoaded
                              ? importModel.Contacts
                              : null
                }
            };
        }

        [HttpPost]
        [RenderEventBranding]
        public JsonResult DeletePersonalizationImage(int participantId, int id)
        {
            string message = string.Empty;
            bool isSuccess = true;
            if (id > 0)
            {
                try
                {
                    using (var dataProvider = new DataProvider())
                    {
                        var persoImg = dataProvider.personalization_image.First(p => p.image_id == id);
                        dataProvider.Entry(persoImg).State = EntityState.Deleted;
                        dataProvider.SaveChanges();
                    }
                }
                catch
                {
                    isSuccess = false;
                    message = "Failed to delete image";
                }
            }
            var eventBranding = ViewBag.Event as Models.Branding.Event;
            var defaultImage = string.Empty;
            if (eventBranding.EventType != EventTypeInfo.INDIVIDUAL_FUNDRAISER)
            {
                defaultImage = $"{Settings.Default.PersonalizationImageDirectory}/{Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME}";
            }
            else
            {
                defaultImage = $"{Settings.Default.PersonalizationImageDirectory}/{Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME}";
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = isSuccess, responseText = message, defaultImage } };
        }
        [HttpPost]
        public JsonResult SetAlbumCover(int id)
        {
            string message = string.Empty;
            bool isSuccess = true;
            try
            {
                using (var dataProvider = new DataProvider())
                {
                    var personalization = (from pi in dataProvider.personalization_image
                                           from p in dataProvider.personalizations
                                           where pi.personalization_id == p.personalization_id
                                              && pi.image_id == id
                                           select p).SingleOrDefault();
                    if (personalization != null)
                    {
                        personalization.personalization_image.ToList().ForEach(p => p.isCoverAlbum = false);
                        personalization.personalization_image.Single(p => p.image_id == id).isCoverAlbum = true;
                        dataProvider.SaveChanges();
                    }
                }
            }
            catch
            {
                isSuccess = false;
                message = "Failed to set album cover";
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = isSuccess, responseText = message } };
        }

        [HttpPost]
        public JsonResult CheckEventRedirectAvailability(Personalization model)
        {
            using (var dataProvider = new DataProvider())
            {
                var redirectExists = (from p in dataProvider.personalizations
                                      where p.personalization_id != model.Id
                                      && p.redirect == model.Redirect
                                      select p.personalization_id).Any();
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, isAvailable = redirectExists ? 1 : 2 } };
            }
        }
        [RenderEventBranding, RenderOutputText]
        public ActionResult KickOff(int participantId)
        {
            ViewBag.HideMenu = true;
            var @event = ViewBag.Event as Models.Branding.Event;
            var @partner = ViewBag.Partner as Partner;

            if (@partner.ProductOffer != ProductOffer.DonationOnly)
            {
                ViewBag.DisableRenderTextOutput = "true";
            }

            using (var dataProvider = new DataProvider())
            {
                var user = ViewBag.User as Models.Branding.User;
                var emailFlowId = GetEmailFlowBy(0, user.UserTypeInfo);
                var eventType = (int)(ViewBag.Event as Models.Branding.Event).EventType;
                var templates = (from ef in dataProvider.email_flows
                                 from eft in dataProvider.email_flow_template
                                 from et in dataProvider.email_template
                                 from etc in dataProvider.email_template_culture
                                 from br in dataProvider.business_rule
                                 where ef.email_flow_id == emailFlowId
                                       && ef.email_flow_id == eft.email_flow_id
                                       && eft.event_type_id == eventType
                                       && eft.email_template_id == et.email_template_id
                                       && etc.email_template_id == et.email_template_id
                                       && etc.culture_code == "en-US"
                                       && et.email_template_id == br.email_template_id
                                       && br.active
                                 orderby etc.email_template_id ascending
                                 select new { TemplateId = et.email_template_id, From = et.from_name, Subject = etc.subject, BodyHtml = etc.body_html, BodyText = etc.body_text, CreationChannelManual = ef.manual_creation_channel_id, CreationChannelManualOverride = eft.override_manual_creation_channel_id, CreationChannelImport = ef.import_creation_channel_id, CreationChannelImportOverride = eft.override_import_creation_channel_id, BusinessRuleId = eft.business_rule_id, UserTypeFrom = ef.user_type_from, UserTypeTo = ef.user_type_to }).ToList();
                var model = new KickOff { ReminderRecurrency = 7, Reminders = new Reminder[0] };
                if (templates.Any())
                {
                    var mainTemplate = templates[0];
                    model.CustomMessage = @event.Message;
                    model.TemplateId = mainTemplate.TemplateId;
                    model.From = mainTemplate.From;
                    model.Subject = mainTemplate.Subject.TransformQuotesForUI();
                    model.Message = mainTemplate.BodyHtml.TransformQuotesForUI().ReplaceNewLineToBR();
                    model.TextMessage = mainTemplate.BodyText;
                    model.CreationChannelImport = mainTemplate.CreationChannelImportOverride == null
                        ? mainTemplate.CreationChannelImport
                        : (int)mainTemplate.CreationChannelImportOverride;
                    model.CreationChannelManual = mainTemplate.CreationChannelManualOverride == null
                        ? mainTemplate.CreationChannelManual
                        : (int)mainTemplate.CreationChannelManualOverride;
                    model.BusinessRuleId = mainTemplate.BusinessRuleId;
                    var isTestEnvironment = Request.Url.Host.Contains("test.") || Request.Url.Host.Contains("localhost");
                    TagParsor.ParseEmail(@event.Id, mainTemplate.UserTypeFrom, ViewBag.Partner as Partner, user, model, isTestEnvironment);
                    if (templates.Count > 1)
                    {
                        var reminders = new List<Reminder>();
                        Reminder reminder = null;
                        for (int i = 1; i < templates.Count; i++)
                        {
                            reminder = new Reminder { TemplateId = templates[i].TemplateId, From = templates[i].From, Subject = templates[i].Subject.TransformQuotesForUI(), Message = templates[i].BodyHtml.TransformQuotesForUI().ReplaceNewLineToBR(), TextMessage = templates[i].BodyText, CreationChannelImport = templates[i].CreationChannelImportOverride == null ? templates[i].CreationChannelImport : (int)templates[i].CreationChannelImportOverride, CreationChannelManual = templates[i].CreationChannelManualOverride == null ? templates[i].CreationChannelManual : (int)templates[i].CreationChannelManualOverride, DeleteReminder = false, BusinessRuleId = templates[i].BusinessRuleId };
                            reminders.Add(reminder);
                            TagParsor.ParseEmail(@event.Id, mainTemplate.UserTypeFrom, ViewBag.Partner as Partner, user, reminder, isTestEnvironment);
                        }
                        model.Reminders = reminders.ToArray();
                    }
                }
                return View(model);
            }
        }
        [HttpPost, RenderEventBranding]
        public JsonResult KickOff(int participantId, KickOff model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0);
                var responseInfo = new ResponseInfo
                {
                    Status = 200,
                    Type = ResponseType.ERROR,
                    ContentType = "application/json; charset=utf-8",
                    Body = string.Empty,
                    ModelStateError = errorMessage
                };
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { success = false, responseText = errorMessage.Value.Errors[0].ErrorMessage, responseInfo = JsonConvert.SerializeObject(responseInfo) }
                };
            }
            var user = ViewBag.User as Models.Branding.User;
            var @event = ViewBag.Event as Models.Branding.Event;
            var emailFlowId = GetEmailFlowBy(0, user.UserTypeInfo);
            var partner = ViewBag.Partner as Partner;
            using (var dataProvider = new DataProvider())
            {
                foreach (var recipient in model.Recipients)
                {
                    var touchEmailProcessQueue = new touch_email_process_queue
                    {
                        bussiness_rule_id = model.BusinessRuleId,
                        created = DateTime.Now,
                        custom_message = model.CustomMessage,
                        email_address = recipient.Email,
                        event_id = @event.Id,
                        first_name = recipient.FirstName,
                        last_name = recipient.LastName,
                        participation_channel_id = model.ParticipationChannelId > 0 ? (int?)model.ParticipationChannelId : null,
                        status = model.Recipients.Length <= int.Parse(ConfigurationManager.AppSettings["MaxAmountOfEmailsAllowedPerEvent"]) ? (short)touch_email_process_queue_status.Scheduled : (short)touch_email_process_queue_status.Flagged,
                        creation_channel_id = recipient.IsManual ? model.CreationChannelManual : model.CreationChannelImport,
                        email_flow_id = emailFlowId,
                        sponsor_event_participation_id = user.EventParticipationId,
                        event_type = (int)@event.EventType,
                        is_sponsor = user.IsSponsor,
                        message_type = model.MessageType,
                        partner_id = partner.Id,
                        reminder_recurrency = model.ReminderRecurrency,
                        subject = model.Subject
                    };
                    dataProvider.touch_emails_process_queue.Add(touchEmailProcessQueue);
                }
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, participantId } };
        }

        [HttpPost]
        public JsonResult GetGalleryImages()
        {
            var partnerId = Convert.ToInt32(Session[Constants.SESSION_KEY_PARTNER_ID]);
            using (var dataProvider = new DataProvider())
            {
                var galleryImages = (from gi in dataProvider.gallery_image
                                     where gi.partner_id == partnerId && gi.culture_code == "en-US"
                                     select new GalleryImage
                                     {
                                         PartnerId = gi.partner_id,
                                         CultureCode = gi.culture_code,
                                         Name = gi.name,
                                         CategoryName = gi.category_name,
                                         DirectoryName = gi.directory_name,
                                         FileName = gi.file_name
                                     }).ToList();
                if (!galleryImages.Any())
                    galleryImages = (from gi in dataProvider.gallery_image
                                     where gi.partner_id == 0 && gi.culture_code == "en-US"
                                     select new GalleryImage
                                     {
                                         PartnerId = gi.partner_id,
                                         CultureCode = gi.culture_code,
                                         Name = gi.name,
                                         CategoryName = gi.category_name,
                                         DirectoryName = gi.directory_name,
                                         FileName = gi.file_name
                                     }).ToList();
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, galleryImages } };
            }
        }

        [RenderEventBranding]
        public ActionResult OAuthCallback(string provider)
        {
            var model = new OAuth(provider);
            model.ProcessImport();

            // Error occured, stop OAuth flow
            if (model.ErrorMessage.IsNotEmpty())
            {
                return View(model);
            }
            // No error so continue on with the OAuth flow
            if (model.Contacts == null)
            {
                return new EmptyResult();
            }
            // If it reaches here then contacts succesfully imported            
            return View(model);
        }
        [RenderEventBranding, HttpPost]
        public JsonResult CreateAction(int action, int eventParticipationId)
        {
            ActionHelper.CreateAction((ActionEnum)action, eventParticipationId);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true } };
        }

        [RenderOutputText]
        public ActionResult Create()
        {
            var partner = ViewBag.Partner as Partner;

            if (partner.ProductOffer != ProductOffer.DonationOnly)
            {
                ViewBag.DisableRenderTextOutput = "true";
            }

            ViewBag.HideMenu = true;
            return View();
        }

        [HttpPost]
        public JsonResult Create(NewCampaign model)
        {
            var @partner = ViewBag.Partner as Partner;
            var partnerId = @partner.Id;
            int promotionId = Session[Constants.SESSION_KEY_PROMOTION_ID] != null
                                ? Convert.ToInt32(Session[Constants.SESSION_KEY_PROMOTION_ID])
                                : GetSelfRegistrationPromotion(partnerId);
            int fcExternalID = Session[Constants.SESSION_KEY_FCEXTERNAL_ID] != null
                                            ? Convert.ToInt32(Session[Constants.SESSION_KEY_FCEXTERNAL_ID])
                                            : int.MinValue;

            int groupId = 0;
            int participantId;
            var user = ViewBag.User as Models.Branding.User;
            if (partnerId == 760)
            {
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "We're sorry, you are not authorized to create campaigns." } };
            }
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Empty;
                if (ModelState.Any(p => p.Value.Errors.Count > 0))
                {
                    errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
                }

                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
            }
            using (var dataProvider = new DataProvider())
            {
                using (var transactionScope = new TransactionScope())
                {
                    var userRegistered = (from u in dataProvider.users
                                          from m in dataProvider.members
                                          from mh in dataProvider.member_hierarchy
                                          where u.user_id == user.Id
                                                && u.partner_id == partnerId
                                                && m.user_id == u.user_id
                                                && mh.member_id == m.member_id
                                          select
                                              new { MemberId = m.member_id, Unsubscribe = mh.unsubscribe, LeadId = m.lead_id })
                        .FirstOrDefault();
                    if (userRegistered == null)
                    {
                        //we try again but we don't care about the Partner
                        userRegistered = (from u in dataProvider.users
                                          from m in dataProvider.members
                                          from mh in dataProvider.member_hierarchy
                                          where u.user_id == user.Id
                                                && m.user_id == u.user_id
                                                && mh.member_id == m.member_id
                                          select
                                              new { MemberId = m.member_id, Unsubscribe = mh.unsubscribe, LeadId = m.lead_id }).FirstOrDefault();
                        if (userRegistered == null)
                        {
                            throw new Exception(string.Concat("Can't find the Registered User using UserId =", user.Id));
                        }
                    }
                    var campaignManagerSuggestion = FindSuggestedCampaignRedirect(model.CampaignName.ToLower().CleanupRedirect());

                    var member_hierarchy = new member_hierarchy
                    {
                        active = true,
                        create_date = DateTime.Now,
                        creation_channel_id = 1,
                        member_id = userRegistered.MemberId,
                        parent_member_hierarchy_id = null,
                        unsubscribe = userRegistered.Unsubscribe
                    };
                    dataProvider.member_hierarchy.Add(member_hierarchy);
                    dataProvider.SaveChanges();

                    var group = new group
                    {
                        create_date = DateTime.Now,
                        group_name = model.CampaignName,
                        partner_id = partnerId,
                        group_url = string.Empty,
                        redirect = campaignManagerSuggestion,
                        sponsor_id = member_hierarchy.member_hierarchy_id,
                        comments = string.Empty,
                        lead_id = userRegistered.LeadId
                    };
                    dataProvider.groups.Add(group);
                    dataProvider.SaveChanges();

                    groupId = group.group_id;

                    var @event = new _event
                    {
                        active = true,
                        create_date = DateTime.Now,
                        culture_code = "en-US",
                        event_name = model.CampaignName,
                        group_type_id = 1,
                        start_date = DateTime.Now,
                        event_type_id = model.GroupType,
                        event_status_id = 1,
                        redirect = campaignManagerSuggestion,
                        displayable = true,
                        referral_application = 1,
                        profit_calculated = @partner.ProfitPercentage,
                        profit_group_id = @partner.ProfitGroupId.HasValue ? @partner.ProfitGroupId.Value : 2
                    };
                    dataProvider.events.Add(@event);
                    dataProvider.SaveChanges();

                    var eventGroup = new event_group
                    {
                        create_date = DateTime.Now,
                        event_id = @event.event_id,
                        group_id = group.group_id
                    };
                    dataProvider.event_group.Add(eventGroup);
                    dataProvider.SaveChanges();

                    var useDefaultPartnerPaymentConfiguration = (from pc in dataProvider.partner_payment_config
                                                                 where pc.partner_id == partnerId
                                                                       && pc.payment_to == 1
                                                                 select pc.partner_payment_info_id).Any();
                    if (useDefaultPartnerPaymentConfiguration)
                    {
                        if (Convert.ToBoolean((ViewBag.Partner as Partner).ProductOffer == ProductOffer.DonationOnly))
                        {
                            var groupIdString = ConfigurationManager.AppSettings["DP_GroupID_" + partnerId];
                            if (!string.IsNullOrEmpty(groupIdString))
                            {
                                var donGroupId = int.Parse(groupIdString);

                                var phoneDonation = (from g in dataProvider.groups
                                                     from pi in dataProvider.payment_info
                                                     from p in dataProvider.phone_number
                                                     where pi.group_id == g.group_id
                                                           && g.group_id == donGroupId
                                                           && pi.phone_number_id == p.phone_number_id
                                                           && pi.active
                                                     select
                                                         new phone_number { create_date = DateTime.Now, phone_number1 = p.phone_number1 })
                                    .Single();
                                dataProvider.phone_number.Add(phoneDonation);
                                dataProvider.SaveChanges();

                                var postalAddressDonation = (from g in dataProvider.groups
                                                             from pi in dataProvider.payment_info
                                                             from pa in dataProvider.postal_address
                                                             where pi.group_id == g.group_id
                                                                   && g.group_id == donGroupId
                                                                   && pi.postal_address_id == pa.postal_address_id
                                                                   && pi.active
                                                             select
                                                                 new postal_address
                                                                 {
                                                                     create_date = DateTime.Now,
                                                                     address_1 = pa.address_1,
                                                                     address_2 = pa.address_2,
                                                                     city = pa.city,
                                                                     country_code = pa.country_code,
                                                                     is_validated = pa.is_validated,
                                                                     matching_code = pa.matching_code,
                                                                     subdivision_code = pa.subdivision_code,
                                                                     zip_code = pa.zip_code
                                                                 }).Single();
                                dataProvider.postal_address.Add(postalAddressDonation);
                                dataProvider.SaveChanges();

                                var paymentInfoDonation = (from g in dataProvider.groups
                                                           from pi in dataProvider.payment_info
                                                           where pi.group_id == g.group_id
                                                                 && g.group_id == donGroupId
                                                                 && pi.active
                                                           select
                                                               new payment_info
                                                               {
                                                                   active = true,
                                                                   create_date = DateTime.Now,
                                                                   event_id = @event.event_id,
                                                                   group_id = @group.group_id,
                                                                   on_behalf_of_name = pi.on_behalf_of_name,
                                                                   payment_name = pi.payment_name,
                                                                   phone_number_id = phoneDonation.phone_number_id,
                                                                   postal_address_id = postalAddressDonation.postal_address_id,
                                                                   ship_to_name = pi.ship_to_name,
                                                                   ssn = pi.ssn
                                                               }).Single();
                                dataProvider.payment_info.Add(paymentInfoDonation);
                                dataProvider.SaveChanges();
                            }
                        }
                        else
                        {
                            var groupPaymentInfoPhone = new phone_number
                            {
                                create_date = DateTime.Now,
                                phone_number1 = "1-877-275-8664",
                            };
                            dataProvider.phone_number.Add(groupPaymentInfoPhone);
                            dataProvider.SaveChanges();

                            var groupPostalAddress = new postal_address
                            {
                                address_1 = "156 Lawrence Paquette Ind'l Dr PMW#5",
                                city = "Champlain",
                                country_code = "US",
                                subdivision_code = "US-NY",
                                zip_code = "12919",
                                create_date = DateTime.Now,
                            };
                            dataProvider.postal_address.Add(groupPostalAddress);
                            dataProvider.SaveChanges();

                            var groupPaymentInfo = new payment_info
                            {
                                active = true,
                                create_date = DateTime.Now,
                                event_id = @event.event_id,
                                group_id = @group.group_id,
                                on_behalf_of_name = string.Concat(user.FirstName, " ", user.LastName),
                                payment_name = string.Concat(user.FirstName, " ", user.LastName),
                                phone_number_id = groupPaymentInfoPhone.phone_number_id,
                                postal_address_id = groupPostalAddress.postal_address_id,
                                ship_to_name = string.Empty,
                                ssn = string.Empty
                            };
                            dataProvider.payment_info.Add(groupPaymentInfo);
                            dataProvider.SaveChanges();
                        }
                    }
                    else
                    {
                        var groupPaymentInfoPhone = new phone_number
                        {
                            create_date = DateTime.Now,
                            phone_number1 = string.IsNullOrEmpty(model.Phone) ? "000000000" : model.Phone,
                        };
                        dataProvider.phone_number.Add(groupPaymentInfoPhone);
                        dataProvider.SaveChanges();

                        var groupPostalAddress = new postal_address
                        {
                            country_code = "US",
                            subdivision_code = model.State,
                            create_date = DateTime.Now,
                        };
                        dataProvider.postal_address.Add(groupPostalAddress);
                        dataProvider.SaveChanges();

                        var groupPaymentInfo = new payment_info
                        {
                            active = true,
                            create_date = DateTime.Now,
                            event_id = @event.event_id,
                            group_id = @group.group_id,
                            on_behalf_of_name = string.Concat(user.FirstName, " ", user.LastName),
                            payment_name = string.Concat(user.FirstName, " ", user.LastName),
                            phone_number_id = groupPaymentInfoPhone.phone_number_id,
                            postal_address_id = groupPostalAddress.postal_address_id,
                            ship_to_name = string.Empty,
                            ssn = string.Empty,
                        };
                        dataProvider.payment_info.Add(groupPaymentInfo);
                        dataProvider.SaveChanges();
                    }
                    var eventParticipation = new event_participation
                    {
                        participation_channel_id = 3,
                        create_date = DateTime.Now,
                        event_id = @event.event_id,
                        member_hierarchy_id = member_hierarchy.member_hierarchy_id,
                        agree_term_services = true,
                        holiday_reminders = false,
                        salutation = string.Concat(user.FirstName, " ", user.LastName)
                    };
                    dataProvider.event_participation.Add(eventParticipation);
                    dataProvider.SaveChanges();
                    default_personalization defaultPersonalization;
                    var defaultPartnerPersonalization =
                        dataProvider.default_personalizations.Where(
                            p => p.PartnerId == partnerId && p.EventTypeId == model.GroupType && p.ParticipantTypeId == 1).ToList();
                    if (defaultPartnerPersonalization.Any())
                    {
                        defaultPersonalization = defaultPartnerPersonalization.First();
                    }
                    else
                    {
                        defaultPersonalization =
                        dataProvider.default_personalizations.Single(
                            p => p.PartnerId == 0 && p.EventTypeId == model.GroupType && p.ParticipantTypeId == 1);
                    }
                    var personalization = new personalization
                    {
                        create_date = DateTime.Now,
                        event_participation_id = eventParticipation.event_participation_id,
                        body = defaultPersonalization.Body,
                        fundraising_goal = (decimal)defaultPersonalization.Goal,
                        redirect = campaignManagerSuggestion,
                        header_title1 = defaultPersonalization.HeaderTitle1,
                        header_title2 = defaultPersonalization.HeaderTitle2,
                        displayGroupMessage = true,
                    };
                    dataProvider.personalizations.Add(personalization);
                    dataProvider.SaveChanges();
                    participantId = eventParticipation.event_participation_id;
                    transactionScope.Complete();

                    /* Insert lead */
                    if (group.lead_id < 0)
                    {
                        if (Convert.ToBoolean(@partner.InsertAsLead) && @partner.Program != Program.Mvp)
                        {
                            var state = model.State;
                            if (state.Length == 5 && state.Contains("-"))
                            {
                                state = state.Substring(state.IndexOf("-", StringComparison.InvariantCulture) + 1);
                            }
                            var lead = new Shared.Entities.Lead
                            {
                                PromotionId = promotionId,
                                Group = group.group_name,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                ChannelCode = "INT",
                                Address = new Shared.Entities.Address
                                {
                                    Address1 = "-",
                                    Region = new Region { Code = state },
                                    Country = new Country { Code = "US" },
                                    City = "-",
                                    PostCode = "-"
                                },
                                Phone = model.Phone,
                                Email = user.Email,
                                PartnerId = partnerId,
                                KitType = 43,
                                ConsultantId = fcExternalID > 0 ? fcExternalID : 0
                            };
                            using (var client = new HttpClient())
                            {
                                var uri =
                                   new Uri(string.Format("{0}/leads", ConfigurationManager.AppSettings["core.webapi.host"]));
                                var content = client.PostAsJsonAsync(uri, lead).Result.Content;
                            }
                        }
                    }
                }
            }
            return new JsonResult { Data = new { success = true, participantId }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost, RenderEventBranding]
        public JsonResult End(int participantId)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var eventFound = dataProvider.events.Single(p => p.event_id == @event.Id);
                eventFound.end_date = DateTime.Now;
                eventFound.active = false;
                dataProvider.SaveChanges();
            }
            return new JsonResult { Data = new { success = true, participantId }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost, RenderEventBranding]
        public JsonResult ChangeGoal(int participantId, decimal goal)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            if (goal < 0)
            {
                return new JsonResult
                {
                    Data = new { success = false, participantId, responseText = "The Goal should be a positive number" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            var user = ViewBag.User as Models.Branding.User;
            using (var dataProvider = new DataProvider())
            {
                var @personalization = (from m in dataProvider.members
                                        from mh in dataProvider.member_hierarchy
                                        from ep in dataProvider.event_participation
                                        from p in dataProvider.personalizations
                                        where m.member_id == mh.member_id
                                           && mh.member_hierarchy_id == ep.member_hierarchy_id
                                           && p.event_participation_id == ep.event_participation_id
                                           && ep.event_id == @event.Id
                                           && m.user_id == user.Id
                                        select p).FirstOrDefault();
                if (@personalization != null)
                {
                    @personalization.fundraising_goal = goal;
                    dataProvider.SaveChanges();
                }
                else
                {
                    return new JsonResult { Data = new { success = false, participantId }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

            }
            return new JsonResult { Data = new { success = true, participantId }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private string FindSuggestedCampaignRedirect(string campaignName)
        {
            var incremental = 1;
            using (var dataProvider = new DataProvider())
            {

                if (!dataProvider.personalizations.Any(p => p.redirect.ToLower() == campaignName))
                {
                    return campaignName;
                }
                while (true)
                {
                    var newName = string.Concat(campaignName, incremental);
                    var existingRedirects = dataProvider.personalizations.Any(p => p.redirect.ToLower() == newName);
                    if (!existingRedirects)
                    {
                        return newName;
                    }
                    incremental++;
                }
            }
        }
        [HttpGet, RenderEventBranding]
        public ActionResult Relaunch(int participantId)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var result = dataProvider.es_rpt_campaign_summary_report(@event.Id).First();
                ViewBag.LastActivityDate = result.last_activity ?? DateTime.Now;
                ViewBag.NumberMembers = result.nb_group_members ?? 0;
                ViewBag.NumberSupporters = result.nb_supporters ?? 0;
                ViewBag.Profit = result.profit ?? 0;
                return View();
            }
        }
        [HttpPost, RenderEventBranding]
        public JsonResult Clone(int participantId)
        {
            var partnerId = int.Parse(Session[Constants.SESSION_KEY_PARTNER_ID].ToString());
            var profitGroupId = 0;
            var profitCalculated = 0.0;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new GA.BDC.Data.MGP.EFRCommon.Models.DataProvider())
            {
                var profit = (from pp in dataProvider.partner_profit
                              from p in dataProvider.profits
                              where pp.profit_group_id == p.profit_group_id
                              && pp.partner_id == partnerId
                              select p).First();
                profitGroupId = (int)profit.profit_group_id;
                profitCalculated = (double)profit.profit_percentage;
            }

            using (var dataProvider = new DataProvider())
            {
                using (var transactionScope = new TransactionScope())
                {
                    var currentEvent = dataProvider.events.Single(p => p.event_id == @event.Id);
                    currentEvent.active = false;
                    currentEvent.end_date = DateTime.Now;
                    dataProvider.SaveChanges();
                    var currentEventGroup = dataProvider.event_group.First(p => p.event_id == @event.Id);
                    var user = ViewBag.User as Models.Branding.User;
                    var userId = 0;
                    var memberId = 0;
                    var memberHierarchyId = 0;
                    event_participation currentEventParticipation;
                    personalization currentPersonalization;
                    payment_info currentPaymentInfo;
                    postal_address currentPostalAddress;
                    phone_number currentPhone;
                    try
                    {
                        var userFound = (from u in dataProvider.users
                                         from m in dataProvider.members
                                         from mh in dataProvider.member_hierarchy
                                         from ep in dataProvider.event_participation
                                         where u.email_address == user.Email
                                               && u.user_id == user.Id
                                               && u.partner_id == partnerId
                                               && m.user_id == u.user_id
                                               && mh.member_id == m.member_id
                                               && mh.member_hierarchy_id == ep.member_hierarchy_id
                                               && ep.event_id == @event.Id
                                               && ep.participation_channel_id == 3
                                         select
                                             new { UserId = u.user_id, MemberId = m.member_id, MemberHiearchyId = mh.member_hierarchy_id })
                        .Single();
                        userId = userFound.UserId;
                        memberId = userFound.MemberId;
                        memberHierarchyId = userFound.MemberHiearchyId;
                    }
                    catch (Exception exception)
                    {
                        throw new InvalidOperationException(string.Format("User Registered not found. Id: {0}. Email: {1}. PartnerId: {2}", user.Id, user.Email, partnerId), exception);
                    }

                    try
                    {
                        currentEventParticipation = dataProvider.event_participation.Single(p => p.member_hierarchy_id == memberHierarchyId && p.event_id == @event.Id);
                    }
                    catch (Exception exception)
                    {
                        throw new InvalidOperationException(string.Format("Event Participation not found. Member Hiearchy Id: {0}. Event id: {1}.", memberHierarchyId, @event.Id), exception);
                    }
                    try
                    {
                        currentPersonalization = dataProvider.personalizations.Single(p => p.event_participation_id == currentEventParticipation.event_participation_id);
                    }
                    catch (Exception exception)
                    {
                        throw new InvalidOperationException(string.Format("Personalization not found. Event Participation Id: {0}", currentEventParticipation.event_participation_id), exception);
                    }
                    try
                    {
                        currentPaymentInfo = dataProvider.payment_info.SingleOrDefault(p => p.event_id == @event.Id && p.active);
                    }
                    catch (Exception exception)
                    {
                        throw new InvalidOperationException(string.Format("Payment Info not found. Event Id: {0}", @event.Id), exception);
                    }
                    try
                    {
                        currentPostalAddress = currentPaymentInfo != null ? dataProvider.postal_address.Single(p => p.postal_address_id == currentPaymentInfo.postal_address_id) : null;
                    }
                    catch (Exception exception)
                    {
                        throw new InvalidOperationException(string.Format("Postal address not found. Postal Address Id: {0}", currentPaymentInfo.postal_address_id), exception);
                    }
                    try
                    {
                        currentPhone = currentPaymentInfo != null ? dataProvider.phone_number.Single(p => p.phone_number_id == currentPaymentInfo.phone_number_id) : null;
                    }
                    catch (Exception exception)
                    {
                        throw new InvalidOperationException(string.Format("Phone Number not found. Phone Number Id: {0}", currentPaymentInfo.phone_number_id), exception);
                    }

                    var newEvent = new _event
                    {
                        create_date = DateTime.Now,
                        start_date = DateTime.Now,
                        redirect = currentEvent.redirect,
                        active = true,
                        comments = "Campaign relaunched from the Campaign " + currentEvent.event_id,
                        culture_code = "en-US",
                        date_of_event = DateTime.Now,
                        event_name = currentEvent.event_name,
                        discount_site = currentEvent.discount_site,
                        displayable = currentEvent.displayable,
                        donation = currentEvent.donation,
                        event_status_id = 3,
                        event_type_id = currentEvent.event_type_id,
                        group_type_id = currentEvent.group_type_id,
                        humeur_representative = currentEvent.humeur_representative,
                        processing_fee = currentEvent.processing_fee,
                        profit_calculated = profitCalculated,
                        profit_group_id = profitGroupId,
                        want_sales_rep_call = currentEvent.want_sales_rep_call,
                        referral_application = 1
                    };
                    dataProvider.events.Add(newEvent);
                    dataProvider.SaveChanges();
                    var eventGroup = new event_group
                    {
                        create_date = DateTime.Now,
                        event_id = newEvent.event_id,
                        group_id = currentEventGroup.group_id
                    };
                    dataProvider.event_group.Add(eventGroup);
                    dataProvider.SaveChanges();
                    var eventParticipation = new event_participation
                    {
                        participation_channel_id = 3,
                        create_date = DateTime.Now,
                        event_id = newEvent.event_id,
                        member_hierarchy_id = memberHierarchyId,
                        agree_term_services = true,
                        holiday_reminders = false,
                        salutation = string.Concat(user.FirstName, " ", user.LastName)
                    };
                    dataProvider.event_participation.Add(eventParticipation);
                    dataProvider.SaveChanges();
                    var personalization = new personalization
                    {
                        create_date = DateTime.Now,
                        body = currentPersonalization.body,
                        displayGroupMessage = currentPersonalization.displayGroupMessage,
                        event_participation_id = eventParticipation.event_participation_id,
                        fundraising_goal = currentPersonalization.fundraising_goal,
                        group_url = currentPersonalization.group_url,
                        header_color = currentPersonalization.header_color,
                        header_bgcolor = currentPersonalization.header_bgcolor,
                        header_title1 = currentPersonalization.header_title1,
                        header_title2 = currentPersonalization.header_title2,
                        image_motivator = currentPersonalization.image_motivator,
                        image_url = currentPersonalization.image_url,
                        redirect = currentPersonalization.redirect,
                        remind_later = currentPersonalization.remind_later,
                        site_bgcolor = currentPersonalization.site_bgcolor,
                        skip = currentPersonalization.skip
                    };
                    dataProvider.personalizations.Add(personalization);
                    dataProvider.SaveChanges();
                    postal_address postalAddress = null;
                    if (currentPostalAddress != null)
                    {
                        postalAddress = new postal_address
                        {
                            address_1 = currentPostalAddress.address_1,
                            address_2 = currentPostalAddress.address_2,
                            city = currentPostalAddress.city,
                            country_code = currentPostalAddress.country_code,
                            create_date = DateTime.Now,
                            is_validated = currentPostalAddress.is_validated,
                            matching_code = currentPostalAddress.matching_code,
                            subdivision_code = currentPostalAddress.subdivision_code,
                            zip_code = currentPostalAddress.zip_code
                        };
                        dataProvider.postal_address.Add(postalAddress);
                        dataProvider.SaveChanges();
                    }
                    phone_number phoneNumber = null;
                    if (currentPhone != null)
                    {
                        phoneNumber = new phone_number
                        {
                            create_date = DateTime.Now,
                            phone_number1 = string.IsNullOrEmpty(currentPhone.phone_number1) ? "000000000" : currentPhone.phone_number1,
                        };
                        dataProvider.phone_number.Add(phoneNumber);
                        dataProvider.SaveChanges();
                    }
                    if (currentPaymentInfo != null)
                    {
                        var paymentInfo = new payment_info
                        {
                            postal_address_id = postalAddress.postal_address_id,
                            phone_number_id = phoneNumber.phone_number_id,
                            active = true,
                            create_date = DateTime.Now,
                            event_id = newEvent.event_id,
                            group_id = eventGroup.group_id,
                            payment_name = currentPaymentInfo.payment_name,
                            on_behalf_of_name = currentPaymentInfo.on_behalf_of_name,
                            ship_to_name = currentPaymentInfo.ship_to_name,
                            ssn = currentPaymentInfo.ssn
                        };
                        dataProvider.payment_info.Add(paymentInfo);
                        dataProvider.SaveChanges();
                    }
                    participantId = eventParticipation.event_participation_id;
                    transactionScope.Complete();
                    return new JsonResult { Data = new { success = true, participantId }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
        }
        [HttpGet, RenderEventBranding]
        public ActionResult RelaunchInformation(int participantId)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var user = ViewBag.User as Models.Branding.User;
                var model = (
                        from u in dataProvider.users
                        from m in dataProvider.members
                        from mh in dataProvider.member_hierarchy
                        from ep in dataProvider.event_participation
                        from e in dataProvider.events
                        where u.user_id == user.Id
                        && u.user_id == m.user_id
                        && m.member_id == mh.member_id
                        && mh.member_hierarchy_id == ep.member_hierarchy_id
                        && e.event_id == @event.Id
                        select new Information
                        {
                            Email = u.email_address,
                            FirstName = u.first_name,
                            LastName = u.last_name,
                            ChangePassword = false,
                            Newsletter = (bool)!u.unsubscribe,
                            CampaignName = e.event_name
                        }).First();
                var @results = (
                        from e in dataProvider.events
                        from eg in dataProvider.event_group
                        from g in dataProvider.groups
                        from pi in dataProvider.payment_info
                        from p in dataProvider.phone_number
                        from pc in dataProvider.postal_address
                        where e.event_id == @event.Id
                        && e.event_id == eg.event_id
                        && g.group_id == eg.group_id
                        && g.group_id == pi.group_id
                        && e.event_id == pi.event_id
                        && pi.phone_number_id == p.phone_number_id
                        && pi.postal_address_id == pc.postal_address_id
                        && pi.active
                        select new
                        {
                            Address = pc.address_1,
                            CampaignName = e.event_name,
                            City = pc.city,
                            PaymentType = 0,
                            PayTo = pi.payment_name,
                            Phone = p.phone_number1,
                            State = pc.subdivision_code,
                            ZipCode = pc.zip_code
                        }).FirstOrDefault();
                if (@results != null)
                {
                    model.Address = @results.Address;
                    model.CampaignName = @results.CampaignName;
                    model.City = @results.City;
                    model.PaymentType = @results.PaymentType;
                    model.PayTo = @results.PayTo;
                    model.Phone = @results.Phone;
                    model.State = @results.State;
                    model.ZipCode = @results.ZipCode;
                    model.PaymentType = @results.PayTo == string.Concat(model.FirstName, " ", model.LastName)
                                        ? 1
                                        : @results.PayTo == model.CampaignName ? 2 : 3;
                }
                return View(model);
            }
        }
        /// <summary>
        /// Return the file to be downloaded for Promoting a Campaign, according to the type received
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet, RenderEventBranding]
        public FilePathResult DownloadTool(int participantId, int type)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            var partnerId = Convert.ToInt32(Session[Constants.SESSION_KEY_PARTNER_ID]);
            string profitPercentage;
            using (var dataProvider = new Data.MGP.EFRCommon.Models.DataProvider())
            {
                profitPercentage = (from pp in dataProvider.partner_profit
                                    from pg in dataProvider.profit_group
                                    from p in dataProvider.profits
                                    where pp.partner_id == partnerId
                                          && pp.profit_group_id == pg.profit_group_id
                                          && pg.profit_group_id == p.profit_group_id
                                          && p.qsp_catalog_type_id == null
                                    select p.description).FirstOrDefault();
                if (profitPercentage.IsNotEmpty())
                    profitPercentage = profitPercentage.Replace("*", "");
            }
            switch (type)
            {
                case 1:
                    return new FilePathResult("~/Content/files/pdf/emailcollectpage.pdf", System.Net.Mime.MediaTypeNames.Application.Pdf);
                case 2:
                    return new FilePathResult("~/Content/files/pdf/listmag.pdf", System.Net.Mime.MediaTypeNames.Application.Pdf);
                case 3:
                    const string bodyMsg = "Hello friend!" + "\r\n\r\n" +
                                           "We’re raising funds for {0} and need your support. We've set up a fancy " +
                                           "online fundraising store where you can shop for popular magazines, gift cards. " +
                                           "Everything’s available at exclusive prices and {1} of " +
                                           "every purchase goes toward our final goal." + "\r\n\r\n" +
                                           "You can also show your support by sharing our fundraiser with friends + family via email, " +
                                           "or via your favorite social networks, like Facebook + Twitter." + "\r\n\r\n" +
                                           "Thanks!";
                    var groupurl = @event.Redirect;
                    if (groupurl.IsNotEmpty())
                        groupurl = string.Concat("Visit ", Request.Url.Host, "/", groupurl).Trim().ToLower();
                    return new FilePathResult(PDFSharpReport.CreateInviteToHelp(string.Format(bodyMsg, @event.Name, profitPercentage), @event.Name, groupurl, Server.MapPath(@"\Content\files\pdf\"), Server.MapPath(@"\temp\")), System.Net.Mime.MediaTypeNames.Application.Pdf);
                case 4:
                    return new FilePathResult(PDFSharpReport.CreateFreeKit(@event.Name, profitPercentage, @event.Redirect, Server.MapPath(@"\Content\files\pdf\"), Server.MapPath(@"\temp\")), System.Net.Mime.MediaTypeNames.Application.Pdf);
            }
            return null;
        }
        [HttpGet, RenderEventBranding, RenderOutputText]
        public ActionResult SendMessages(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            var @partner = ViewBag.Partner as Partner;

            if (@partner.ProductOffer != ProductOffer.DonationOnly)
            {
                ViewBag.DisableRenderTextOutput = "true";
            }
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                ViewBag.NumberOfInvalidContacts = (from m in dataProvider.members
                                                   from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   where m.member_id == mh.member_id
                                                         && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.participation_channel_id == 1
                                                         && mh.active
                                                         && !m.deleted
                                                         && m.bounced
                                                   select m.member_id).Count();
                ViewBag.NumberOfValidContacts = (from m in dataProvider.members
                                                 from mh in dataProvider.member_hierarchy
                                                 from cc in dataProvider.creation_channel
                                                 from ep in dataProvider.event_participation
                                                 where m.member_id == mh.member_id
                                                       && mh.creation_channel_id == cc.creation_channel_id
                                                       && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.participation_channel_id == 1
                                                       && mh.active
                                                       && !m.deleted
                                                       && !m.bounced
                                                 select m.member_id).Count();
                ViewBag.NumberOfPendingEmails = (from mh in dataProvider.member_hierarchy
                                                 from ep in dataProvider.event_participation
                                                 from t in dataProvider.touches
                                                 where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.event_participation_id == t.event_participation_id
                                                       && (t.processed == 12
                                                           || t.processed == 0)
                                                 select t.touch_id).Count();
                ViewBag.NumberOfProcessedEmails = (from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   from t in dataProvider.touches
                                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.event_participation_id == t.event_participation_id
                                                         && t.processed == 2
                                                   select t.touch_id).Count();
            }

            return View();
        }

        private int GetEmailFlowBy(int messageType, UserType userType)
        {
            int selectorId = 1, emailFlowId = 0;
            var partner = ViewBag.Partner as Partner;
            var partnerId = partner.Program == Program.Mvp ? 143 : partner.Id;
            using (var dataProvider = new DataProvider())
            {
                var selector = dataProvider.email_template_selector.FirstOrDefault(p => p.partner_id == partnerId && p.culture_code == "en-US");
                if (selector != null)
                {
                    selectorId = selector.email_template_selector_id;
                }
                var emailFlow = dataProvider.email_flows.Where(p => p.email_template_selector_id == selectorId)
                                                        .OrderBy(p => p.email_flow_id)
                                                        .Select(p => p)
                                                        .ToList();
                switch (userType)
                {
                    case UserType.SPONSOR:
                        emailFlowId = messageType == 0 || messageType == 1
                                        ? emailFlow[0].email_flow_id
                                        : messageType == 2
                                            ? emailFlow[0].email_flow_id + 3
                                            : emailFlow[0].email_flow_id + 2;
                        break;
                    case UserType.PARTICIPANT:
                        emailFlowId = messageType == 0
                                        ? emailFlow[4].email_flow_id
                                        : messageType == 2
                                            ? emailFlow[5].email_flow_id
                                            : emailFlow[5].email_flow_id + 1;
                        break;
                    case UserType.SUPPORTER:
                    case UserType.UNKNOWN:
                        throw new Exception("Invalid UserType for SendMessages");
                    default:
                        break;
                }
            }
            return emailFlowId;
        }

        [HttpGet, RenderEventBranding, RenderOutputText]
        public ActionResult SendNewMessage(int participantId, int messageType)
        {
            var user = ViewBag.User as Models.Branding.User;
            var @event = ViewBag.Event as Models.Branding.Event;
            var partner = ViewBag.Partner as Partner;
            var emailFlowId = GetEmailFlowBy(messageType, user.UserTypeInfo);
            var memberTypeId = user.IsSponsor ? 1 : 2;
            bool isTestEnvironment = Request.Url.Host.Contains("test.") || Request.Url.Host.Contains("localhost");

            if (partner.ProductOffer != ProductOffer.DonationOnly)
            {
                ViewBag.DisableRenderTextOutput = "true";
            }

            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                ViewBag.NumberOfInvalidContacts = (from m in dataProvider.members
                                                   from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   where m.member_id == mh.member_id
                                                         && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.participation_channel_id == 1
                                                         && mh.active
                                                         && !m.deleted
                                                         && m.bounced
                                                   select m.member_id).Count();
                ViewBag.NumberOfValidContacts = (from m in dataProvider.members
                                                 from mh in dataProvider.member_hierarchy
                                                 from cc in dataProvider.creation_channel
                                                 from ep in dataProvider.event_participation
                                                 where m.member_id == mh.member_id
                                                       && mh.creation_channel_id == cc.creation_channel_id
                                                       && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.participation_channel_id == 1
                                                       && mh.active
                                                       && !m.deleted
                                                       && !m.bounced
                                                 select m.member_id).Count();
                ViewBag.NumberOfPendingEmails = (from mh in dataProvider.member_hierarchy
                                                 from ep in dataProvider.event_participation
                                                 from t in dataProvider.touches
                                                 where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.event_participation_id == t.event_participation_id
                                                       && (t.processed == 12
                                                           || t.processed == 0)
                                                 select t.touch_id).Count();
                ViewBag.NumberOfProcessedEmails = (from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   from t in dataProvider.touches
                                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.event_participation_id == t.event_participation_id
                                                         && t.processed == 2
                                                   select t.touch_id).Count();
                var eventType = (int)(ViewBag.Event as Models.Branding.Event).EventType;
                var templates = (from ef in dataProvider.email_flows
                                 from eft in dataProvider.email_flow_template
                                 from et in dataProvider.email_template
                                 from etc in dataProvider.email_template_culture
                                 from br in dataProvider.business_rule
                                 where ef.email_flow_id == emailFlowId
                                       && ef.email_flow_id == eft.email_flow_id
                                       && eft.event_type_id == eventType
                                       && eft.email_template_id == et.email_template_id
                                       && etc.email_template_id == et.email_template_id
                                       && etc.culture_code == "en-US"
                                       && et.email_template_id == br.email_template_id
                                       && br.active
                                 orderby etc.email_template_id ascending
                                 select new { TemplateId = et.email_template_id, From = et.from_name, Subject = etc.subject, BodyHtml = etc.body_html, BodyText = etc.body_text, CreationChannelManual = ef.manual_creation_channel_id, CreationChannelManualOverride = eft.override_manual_creation_channel_id, CreationChannelImport = ef.import_creation_channel_id, CreationChannelImportOverride = eft.override_import_creation_channel_id, BusinessRuleId = eft.business_rule_id, UserTypeFrom = ef.user_type_from, UserTypeTo = ef.user_type_to }).ToList();
                var model = new KickOff { ReminderRecurrency = 7, ParticipationChannelId = 1, MessageType = messageType };
                if (templates.Any())
                {
                    var mainTemplate = templates[0];
                    model.TemplateId = mainTemplate.TemplateId;
                    model.From = mainTemplate.From;
                    model.Subject = mainTemplate.Subject.TransformQuotesForUI();
                    model.CustomMessage = @event.Message;
                    model.Message = mainTemplate.BodyHtml.TransformQuotesForUI().ReplaceNewLineToBR();
                    model.TextMessage = mainTemplate.BodyText;
                    model.CreationChannelImport = mainTemplate.CreationChannelImportOverride == null
                        ? mainTemplate.CreationChannelImport
                        : (int)mainTemplate.CreationChannelImportOverride;
                    model.CreationChannelManual = mainTemplate.CreationChannelManualOverride == null
                        ? mainTemplate.CreationChannelManual
                        : (int)mainTemplate.CreationChannelManualOverride;
                    model.BusinessRuleId = mainTemplate.BusinessRuleId;
                    TagParsor.ParseEmail(@event.Id, mainTemplate.UserTypeFrom, ViewBag.Partner as Partner, user, model, isTestEnvironment);
                    if (templates.Count > 1)
                    {
                        var reminders = new List<Reminder>();
                        Reminder reminder = null;
                        for (int i = 1; i < templates.Count; i++)
                        {
                            reminder = new Reminder { TemplateId = templates[i].TemplateId, From = templates[i].From, Subject = templates[i].Subject.TransformQuotesForUI(), Message = templates[i].BodyHtml.TransformQuotesForUI().ReplaceNewLineToBR(), TextMessage = templates[i].BodyText, CreationChannelImport = templates[i].CreationChannelImportOverride == null ? templates[i].CreationChannelImport : (int)templates[i].CreationChannelImportOverride, CreationChannelManual = templates[i].CreationChannelManualOverride == null ? templates[i].CreationChannelManual : (int)templates[i].CreationChannelManualOverride, DeleteReminder = false, BusinessRuleId = templates[i].BusinessRuleId };
                            reminders.Add(reminder);
                            TagParsor.ParseEmail(@event.Id, mainTemplate.UserTypeFrom, ViewBag.Partner as Partner, user, reminder, isTestEnvironment);
                        }
                        model.Reminders = reminders.ToArray();
                    }
                    else
                        model.Reminders = new List<Reminder>().ToArray();
                }
                ViewBag.ShowImportContactsModal = Request.QueryString.AllKeys.Any(p => p == "showImportContacts") &&
                                              Request.QueryString["showImportContacts"] == "1";
                return View(model);
            }
        }
        [HttpPost, RenderEventBranding]
        public JsonResult SendNewMessage(int participantId, KickOff model)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            var user = ViewBag.User as Models.Branding.User;
            var emailFlowId = GetEmailFlowBy(model.MessageType, user.UserTypeInfo);
            var partner = ViewBag.Partner as Partner;
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0);
                var responseInfo = new ResponseInfo
                {
                    Status = 200,
                    Type = ResponseType.ERROR,
                    ContentType = "application/json; charset=utf-8",
                    Body = string.Empty,
                    ModelStateError = errorMessage
                };
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { success = false, responseText = errorMessage.Value.Errors[0].ErrorMessage, responseInfo = JsonConvert.SerializeObject(responseInfo) }
                };
            }
            using (var dataProvider = new DataProvider())
            {
                foreach (var recipient in model.Recipients)
                {
                    var touchEmailProcessQueue = new touch_email_process_queue
                    {
                        bussiness_rule_id = model.BusinessRuleId,
                        created = DateTime.Now,
                        custom_message = model.CustomMessage,
                        email_address = recipient.Email,
                        event_id = @event.Id,
                        first_name = recipient.FirstName,
                        last_name = recipient.LastName,
                        participation_channel_id = model.ParticipationChannelId,
                        status = model.Recipients.Length <= int.Parse(ConfigurationManager.AppSettings["MaxAmountOfEmailsAllowedPerEvent"]) ? (short)touch_email_process_queue_status.Scheduled : (short)touch_email_process_queue_status.Flagged,
                        creation_channel_id = recipient.IsManual ? model.CreationChannelManual : model.CreationChannelImport,
                        email_flow_id = emailFlowId,
                        sponsor_event_participation_id = user.EventParticipationId,
                        event_type = (int)@event.EventType,
                        is_sponsor = user.IsSponsor,
                        message_type = model.MessageType,
                        partner_id = partner.Id,
                        reminder_recurrency = model.ReminderRecurrency,
                        subject = model.Subject
                    };
                    dataProvider.touch_emails_process_queue.Add(touchEmailProcessQueue);
                }
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "The emails scheduled sent correctly.", participantId } };
        }
        [HttpGet, RenderEventBranding]
        public ActionResult SentEmails(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                ViewBag.NumberOfInvalidContacts = (from m in dataProvider.members
                                                   from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   where m.member_id == mh.member_id
                                                         && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.participation_channel_id == 1
                                                         && mh.active
                                                         && !m.deleted
                                                         && m.bounced
                                                   select m.member_id).Count();
                ViewBag.NumberOfValidContacts = (from m in dataProvider.members
                                                 from mh in dataProvider.member_hierarchy
                                                 from cc in dataProvider.creation_channel
                                                 from ep in dataProvider.event_participation
                                                 where m.member_id == mh.member_id
                                                       && mh.creation_channel_id == cc.creation_channel_id
                                                       && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.participation_channel_id == 1
                                                       && mh.active
                                                       && !m.deleted
                                                       && !m.bounced
                                                 select m.member_id).Count();
                ViewBag.NumberOfPendingEmails = (from mh in dataProvider.member_hierarchy
                                                 from ep in dataProvider.event_participation
                                                 from t in dataProvider.touches
                                                 where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.event_participation_id == t.event_participation_id
                                                       && (t.processed == 12
                                                           || t.processed == 0)
                                                 select t.touch_id).Count();
                ViewBag.NumberOfProcessedEmails = (from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   from t in dataProvider.touches
                                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.event_participation_id == t.event_participation_id
                                                         && t.processed == 2
                                                   select t.touch_id).Count();
                var totalEmails = (from mh in dataProvider.member_hierarchy
                                   from ep in dataProvider.event_participation
                                   from t in dataProvider.touches
                                   from m in dataProvider.members
                                   from ti in dataProvider.touch_info
                                   from cet in dataProvider.custom_email_template
                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                         && ep.event_id == @event.Id
                                         && ep.event_participation_id == t.event_participation_id
                                         && t.processed == 2
                                         && mh.member_id == m.member_id
                                         && t.touch_info_id == ti.touch_info_id
                                         && ti.touch_info_id == cet.touch_info_id
                                   select t).Count();
                return View(totalEmails);
            }
        }
        [HttpGet, RenderEventBranding]
        public ActionResult PendingEmails(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                ViewBag.NumberOfInvalidContacts = (from m in dataProvider.members
                                                   from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   where m.member_id == mh.member_id
                                                         && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.participation_channel_id == 1
                                                         && mh.active
                                                         && !m.deleted
                                                         && m.bounced
                                                   select m.member_id).Count();
                ViewBag.NumberOfValidContacts = (from m in dataProvider.members
                                                 from mh in dataProvider.member_hierarchy
                                                 from cc in dataProvider.creation_channel
                                                 from ep in dataProvider.event_participation
                                                 where m.member_id == mh.member_id
                                                       && mh.creation_channel_id == cc.creation_channel_id
                                                       && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.participation_channel_id == 1
                                                       && mh.active
                                                       && !m.deleted
                                                       && !m.bounced
                                                 select m.member_id).Count();
                ViewBag.NumberOfPendingEmails = (from mh in dataProvider.member_hierarchy
                                                 from ep in dataProvider.event_participation
                                                 from t in dataProvider.touches
                                                 where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.event_participation_id == t.event_participation_id
                                                       && (t.processed == 12
                                                           || t.processed == 0)
                                                 select t.touch_id).Count();
                ViewBag.NumberOfProcessedEmails = (from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   from t in dataProvider.touches
                                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.event_participation_id == t.event_participation_id
                                                         && t.processed == 2
                                                   select t.touch_id).Count();
                ViewBag.NumberOfContactsBeingCreated = dataProvider.touch_emails_process_queue.Count(p => p.event_id == @event.Id && p.status == (int)touch_email_process_queue_status.Scheduled);
                ViewBag.HasContactsFlagged = dataProvider.touch_emails_process_queue.Any(p => p.event_id == @event.Id && p.status == (int)touch_email_process_queue_status.Flagged);
                var totalEmails = (from mh in dataProvider.member_hierarchy
                                   from ep in dataProvider.event_participation
                                   from t in dataProvider.touches
                                   from m in dataProvider.members
                                   from ti in dataProvider.touch_info
                                   from cet in dataProvider.custom_email_template
                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                         && ep.event_id == @event.Id
                                         && ep.event_participation_id == t.event_participation_id
                                         && (t.processed == 12
                                             || t.processed == 0)
                                         && mh.member_id == m.member_id
                                         && t.touch_info_id == ti.touch_info_id
                                         && ti.touch_info_id == cet.touch_info_id
                                   select t).Count();
                return View(totalEmails);
            }
        }
        [HttpGet, RenderEventBranding]
        public ActionResult Contacts(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                ViewBag.NumberOfInvalidContacts = (from m in dataProvider.members
                                                   from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   where m.member_id == mh.member_id
                                                         && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.participation_channel_id == 1
                                                         && mh.active
                                                         && !m.deleted
                                                         && m.bounced
                                                   select m.member_id).Count();
                ViewBag.NumberOfValidContacts = (from m in dataProvider.members
                                                 from mh in dataProvider.member_hierarchy
                                                 from cc in dataProvider.creation_channel
                                                 from ep in dataProvider.event_participation
                                                 where m.member_id == mh.member_id
                                                       && mh.creation_channel_id == cc.creation_channel_id
                                                       && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.participation_channel_id == 1
                                                       && mh.active
                                                       && !m.deleted
                                                       && !m.bounced
                                                 select m.member_id).Count();
                ViewBag.NumberOfPendingEmails = (from mh in dataProvider.member_hierarchy
                                                 from ep in dataProvider.event_participation
                                                 from t in dataProvider.touches
                                                 where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.event_participation_id == t.event_participation_id
                                                       && (t.processed == 12
                                                           || t.processed == 0)
                                                 select t.touch_id).Count();
                ViewBag.NumberOfProcessedEmails = (from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   from t in dataProvider.touches
                                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.event_participation_id == t.event_participation_id
                                                         && t.processed == 2
                                                   select t.touch_id).Count();

                var totalContacts = (
                    from m in dataProvider.members
                    from mh in dataProvider.member_hierarchy
                    from ep in dataProvider.event_participation
                    where m.member_id == mh.member_id
                          && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                          && ep.member_hierarchy_id == mh.member_hierarchy_id
                          && ep.event_id == @event.Id
                          & !m.deleted
                    orderby m.email_address
                    select m).Count();
                return View(totalContacts);
            }
        }
        [HttpGet, RenderEventBranding]
        public ActionResult InvalidContacts(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                ViewBag.NumberOfInvalidContacts = (from m in dataProvider.members
                                                   from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   where m.member_id == mh.member_id
                                                         && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.participation_channel_id == 1
                                                         && mh.active
                                                         && !m.deleted
                                                         && m.bounced
                                                   select m.member_id).Count();
                ViewBag.NumberOfValidContacts = (from m in dataProvider.members
                                                 from mh in dataProvider.member_hierarchy
                                                 from cc in dataProvider.creation_channel
                                                 from ep in dataProvider.event_participation
                                                 where m.member_id == mh.member_id
                                                       && mh.creation_channel_id == cc.creation_channel_id
                                                       && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.participation_channel_id == 1
                                                       && mh.active
                                                       && !m.deleted
                                                       && !m.bounced
                                                 select m.member_id).Count();
                ViewBag.NumberOfPendingEmails = (from mh in dataProvider.member_hierarchy
                                                 from ep in dataProvider.event_participation
                                                 from t in dataProvider.touches
                                                 where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                       && ep.event_id == @event.Id
                                                       && ep.event_participation_id == t.event_participation_id
                                                       && (t.processed == 12
                                                           || t.processed == 0)
                                                 select t.touch_id).Count();
                ViewBag.NumberOfProcessedEmails = (from mh in dataProvider.member_hierarchy
                                                   from ep in dataProvider.event_participation
                                                   from t in dataProvider.touches
                                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                         && ep.event_id == @event.Id
                                                         && ep.event_participation_id == t.event_participation_id
                                                         && t.processed == 2
                                                   select t.touch_id).Count();
                var invalidContactsFound = (from m in dataProvider.members
                                            from mh in dataProvider.member_hierarchy
                                            from ep in dataProvider.event_participation
                                            where m.member_id == mh.member_id
                                                    && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                                    && mh.member_hierarchy_id == ep.member_hierarchy_id
                                                    && ep.event_id == @event.Id
                                                    && mh.active
                                                    && !m.deleted
                                                    && m.bounced
                                            select m.member_id).Count();
                return View(invalidContactsFound);
            }

        }
        [HttpPost, RenderEventBranding]
        public JsonResult UpdateContact(int participantId, Recipient model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
            }
            using (var dataProvider = new DataProvider())
            {
                var memberFound = dataProvider.members.Single(p => p.member_id == model.Id);
                memberFound.first_name = model.FirstName;
                memberFound.last_name = model.LastName;
                memberFound.email_address = model.Email;
                memberFound.bounced = false;
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Contact updated correctly." } };
        }
        [HttpPost]
        public JsonResult DeleteContact(int memberId)
        {
            using (var dataProvider = new DataProvider())
            {
                var memberFound = dataProvider.members.Single(p => p.member_id == memberId);
                memberFound.deleted = true;
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Contact deleted correctly." } };
        }

        [HttpPost]
        public JsonResult DeleteInvalidContact(int memberId)
        {
            using (var dataProvider = new DataProvider())
            {
                var memberFound = dataProvider.members.Single(p => p.member_id == memberId);
                memberFound.deleted = true;
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Contact deleted correctly." } };
        }

        [HttpPost]
        public JsonResult DeletePendingEmail(int touchId)
        {
            using (var dataProvider = new DataProvider())
            {
                var touchFound = (from t in dataProvider.touches
                                  where t.touch_id == touchId
                                  select t).First();
                touchFound.processed = 4;
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Email deleted correctly." } };
        }

        [HttpPost, RenderEventBranding]
        public ContentResult FindContacts(int participantId, int pageSize, int page, string searchText)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                var contactsFound = (
                    from m in dataProvider.members
                    from mh in dataProvider.member_hierarchy
                    from cc in dataProvider.creation_channel
                    from ep in dataProvider.event_participation
                    where m.member_id == mh.member_id
                          && mh.creation_channel_id == cc.creation_channel_id
                          && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                          && ep.member_hierarchy_id == mh.member_hierarchy_id
                          && ep.participation_channel_id == 1
                          && ep.event_id == @event.Id
                          && !m.deleted
                          && (searchText == "" || m.first_name.Contains(searchText) || m.last_name.Contains(searchText) || m.email_address.Contains(searchText))
                    orderby m.email_address
                    select
                        new Recipient
                        {
                            Id = m.member_id,
                            Email = m.email_address,
                            FirstName = m.first_name,
                            LastName = m.last_name
                        });
                var totalContacts = contactsFound.Count();
                var contactsPaged = contactsFound.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var data = JsonConvert.SerializeObject(
                    new { contactsPaged, total = totalContacts }
                    );
                return Content(data, "application/json");
            }
        }

        [HttpPost, RenderEventBranding]
        public ContentResult FindInvalidContacts(int participantId, int pageSize, int page, string searchText)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && ep.member_hierarchy_id == mh.member_hierarchy_id
                                       && ep.event_id == @event.Id
                                       && u.user_id == user.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                var contactsFound = (
                    from m in dataProvider.members
                    from mh in dataProvider.member_hierarchy
                    from ep in dataProvider.event_participation
                    where m.member_id == mh.member_id
                          && mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                          && ep.member_hierarchy_id == mh.member_hierarchy_id
                          && ep.event_id == @event.Id
                          && ep.participation_channel_id == 1
                          && !m.deleted
                          && m.bounced
                          && (searchText == "" || m.first_name.Contains(searchText) || m.last_name.Contains(searchText) || m.email_address.Contains(searchText))
                    orderby m.email_address
                    select
                        new Recipient
                        {
                            Id = m.member_id,
                            Email = m.email_address,
                            FirstName = m.first_name,
                            LastName = m.last_name
                        });
                var totalContacts = contactsFound.Count();
                var invalidContactsPaged = contactsFound.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var data = JsonConvert.SerializeObject(
                    new { invalidContactsPaged, total = totalContacts }
                    );
                return Content(data, "application/json");
            }
        }

        [HttpPost, RenderEventBranding]
        public ContentResult FindPendingEmails(int participantId, int pageSize, int page, string searchText)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                var emailsFound = (from mh in dataProvider.member_hierarchy
                                   from ep in dataProvider.event_participation
                                   from t in dataProvider.touches
                                   from m in dataProvider.members
                                   from ti in dataProvider.touch_info
                                   from cet in dataProvider.custom_email_template
                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                         && ep.event_id == @event.Id
                                         && ep.event_participation_id == t.event_participation_id
                                         && (t.processed == 12
                                             || t.processed == 0)
                                         && mh.member_id == m.member_id
                                         && t.touch_info_id == ti.touch_info_id
                                         && ti.touch_info_id == cet.touch_info_id
                                   select new Models.Views.Email
                                   {
                                       Id = t.touch_id,
                                       Created = (DateTime)ti.launch_date,
                                       To = m.email_address,
                                       Subject = cet.subject,
                                       Body = cet.body_html,
                                       From = user.Email,
                                       Type = "TBD"
                                   }).ToList();
                var totalEmails = emailsFound.Count();
                var pendingEmailsPaged = emailsFound.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                foreach (var email in pendingEmailsPaged)
                {
                    var mTypeId = (from t in dataProvider.touches
                                   from ti in dataProvider.touch_info
                                   from br in dataProvider.business_rule
                                   where t.touch_id == email.Id
                                      && t.touch_info_id == ti.touch_info_id
                                      && ti.business_rule_id == br.business_rule_id
                                      && br.active
                                   select br.member_type_id).First();

                    TagParsor.ParseEmailForPreview(email, @event.Id, mTypeId, ViewBag.Partner as Partner);
                    email.Body = System.Web.HttpUtility.HtmlDecode(email.Body);
                    email.Body = email.Body.TransformQuotesForUI().ReplaceNewLineToBR();
                }
                var data = JsonConvert.SerializeObject(
                    new { pendingEmailsPaged, total = totalEmails }
                    );
                return Content(data, "application/json");
            }
        }
        [HttpPost, RenderEventBranding]
        public ContentResult FindSentEmails(int participantId, int pageSize, int page, string searchText)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                var emailsFound = (from mh in dataProvider.member_hierarchy
                                   from ep in dataProvider.event_participation
                                   from t in dataProvider.touches
                                   from m in dataProvider.members
                                   from ti in dataProvider.touch_info
                                   from cet in dataProvider.custom_email_template
                                   where mh.parent_member_hierarchy_id == userFound.MemberHierachyId
                                         && mh.member_hierarchy_id == ep.member_hierarchy_id
                                         && ep.event_id == @event.Id
                                         && ep.event_participation_id == t.event_participation_id
                                         && t.processed == 2
                                         && mh.member_id == m.member_id
                                         && t.touch_info_id == ti.touch_info_id
                                         && ti.touch_info_id == cet.touch_info_id
                                   select new Models.Views.Email { Id = t.touch_id, Created = (DateTime)t.create_date, To = m.email_address, Subject = cet.subject, Body = cet.body_html, From = user.Email, Type = "TBD" }).ToList();
                var totalEmails = emailsFound.Count();
                var sentEmailsPaged = emailsFound.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                foreach (var email in sentEmailsPaged)
                {
                    var mTypeId = (from t in dataProvider.touches
                                   from ti in dataProvider.touch_info
                                   from br in dataProvider.business_rule
                                   where t.touch_id == email.Id
                                      && t.touch_info_id == ti.touch_info_id
                                      && ti.business_rule_id == br.business_rule_id
                                      && br.active
                                   select br.member_type_id).First();

                    TagParsor.ParseEmailForPreview(email, @event.Id, mTypeId, ViewBag.Partner as Partner);
                    email.Body = System.Web.HttpUtility.HtmlDecode(email.Body);
                    email.Body = email.Body.TransformQuotesForUI().ReplaceNewLineToBR();
                }
                var data = JsonConvert.SerializeObject(
                    new { sentEmailsPaged, total = totalEmails }
                    );
                return Content(data, "application/json");
            }
        }
        [HttpGet, RenderEventBranding]
        public ActionResult Twitter(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                var personalization = (from e in dataProvider.events
                                       from ep in dataProvider.event_participation
                                       from p in dataProvider.personalizations
                                       where e.event_id == ep.event_id
                                             && ep.event_participation_id == p.event_participation_id
                                             && ep.member_hierarchy_id == userFound.MemberHierachyId
                                             && e.event_id == @event.Id
                                       select p).Single();
                ViewBag.Code = personalization.twitter_widget_id;
                return View();
            }

        }
        [HttpPost, RenderEventBranding]
        public JsonResult Twitter(int participantId, string widgetId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                var personalization = (from e in dataProvider.events
                                       from ep in dataProvider.event_participation
                                       from p in dataProvider.personalizations
                                       where e.event_id == ep.event_id
                                             && ep.event_participation_id == p.event_participation_id
                                             && ep.member_hierarchy_id == userFound.MemberHierachyId
                                             && e.event_id == @event.Id
                                       select p).Single();
                personalization.twitter_widget_id = widgetId;
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Twitter widget saved correctly." } };
        }
        [HttpGet, RenderEventBranding]
        public ActionResult Facebook(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                var personalization = (from e in dataProvider.events
                                       from ep in dataProvider.event_participation
                                       from p in dataProvider.personalizations
                                       where e.event_id == ep.event_id
                                             && ep.event_participation_id == p.event_participation_id
                                             && ep.member_hierarchy_id == userFound.MemberHierachyId
                                             && e.event_id == @event.Id
                                       select p).Single();
                ViewBag.Code = personalization.facebook_embeded_post;
                return View();
            }
        }
        [HttpPost, RenderEventBranding]
        public JsonResult Facebook(int participantId, string code)
        {
            var user = ViewBag.User as Models.Branding.User;
            var memberTypeId = user.IsSponsor ? 1 : 2;
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var userFound = (from u in dataProvider.users
                                 from m in dataProvider.members
                                 from mh in dataProvider.member_hierarchy
                                 from cc in dataProvider.creation_channel
                                 from ep in dataProvider.event_participation
                                 where u.user_id == m.user_id
                                       && m.member_id == mh.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                       && u.user_id == user.Id
                                       && ep.event_id == @event.Id
                                       && cc.member_type_id == memberTypeId
                                 select new { UserId = u.user_id, MemberHierachyId = mh.member_hierarchy_id }).First();
                var personalization = (from e in dataProvider.events
                                       from ep in dataProvider.event_participation
                                       from p in dataProvider.personalizations
                                       where e.event_id == ep.event_id
                                             && ep.event_participation_id == p.event_participation_id
                                             && ep.member_hierarchy_id == userFound.MemberHierachyId
                                             && e.event_id == @event.Id
                                       select p).Single();
                code = code.Replace("data-width=\"466\"", "data-width=\"300\"");
                personalization.facebook_embeded_post = Server.HtmlEncode(code);
                dataProvider.SaveChanges();
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Facebook post saved correctly." } };
        }
        [RenderEventBranding]
        public ActionResult CampaignReport(int participantId)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            var @partner = ViewBag.Partner as Models.Branding.Partner;
            if (!@partner.HideCheckReport)
            {
                using (var dataProvider = new DataProvider())
                {
                    var groupId = (from eg in dataProvider.event_group
                                   where eg.event_id == @event.Id
                                   select eg.group_id).Single();
                    ViewBag.HasStatement = SAP.ZBapiGa.Client.HasSAPStatement(groupId.ToString());
                }
            }
            return View();
        }
        [RenderEventBranding]
        public FileResult GetSAPProfitCheckReport(int participantId)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProvider())
            {
                var groupId = (from eg in dataProvider.event_group
                               where eg.event_id == @event.Id
                               select eg.group_id).Single();
                byte[] buffer = SAP.ZBapiGa.Client.GetSAPOnlineProfitStatement(groupId.ToString());
                string fileName = "Statement.pdf";
                return File(buffer, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
            }
        }
        [RenderEventBranding, HttpPost]
        public JsonResult CampaignReport(int participantId, string searchType, string memberName = "", DateTime? from = null, DateTime? to = null, bool isDetail = false)
        {
            var @event = ViewBag.Event as Models.Branding.Event;

            switch (searchType)
            {
                case "all":
                    from = @event.Created.AddDays(-2);
                    to = DateTime.Today.AddDays(1);
                    break;
                case "today":
                    from = DateTime.Today;
                    to = DateTime.Today.AddDays(1);
                    break;
                case "yesterday":
                    from = DateTime.Today.AddDays(-1);
                    to = DateTime.Today;
                    break;
                case "lastWeek":
                    from = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 7);
                    to = ((DateTime)from).AddDays(7);
                    break;
                case "lastMonth":
                    var lastMonth = DateTime.Today.AddMonths(-1);
                    from = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                    to = ((DateTime)from).AddMonths(1).AddDays(-1);
                    break;
                case "last7Days":
                    from = DateTime.Today.AddDays(-7);
                    to = DateTime.Today;
                    break;
                case "last30Days":
                    from = DateTime.Today.AddDays(-30);
                    to = DateTime.Today;
                    break;
                default:
                    break;
            }
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var data = new List<CampaignReportMember>();
                var detail = new List<CampaignReportMember>();
                var sales = dataProvider.es_rpt_sponsor_email_product_type_by_date_range(@event.Id, from, to)
                                        .ToList()
                                        .Where(p => (string.IsNullOrEmpty(memberName) || p.member_name == memberName));
                decimal totalAmount = 0M, totalProfit = 0M;
                if (!isDetail)
                {
                    data.AddRange(sales.GroupBy(p => p.member_name).Select(p => new CampaignReportMember { MemberName = p.Key, EmailsSent = (int)p.Sum(x => x.email_sent), ItemsSold = (int)p.Sum(x => x.nb_subs), AmountSold = (decimal)p.Sum(x => x.amount), Profit = (decimal)p.Sum(x => x.profit) }));
                    totalAmount = data.Sum(p => p.AmountSold);
                    totalProfit = data.Sum(p => p.Profit);
                }
                else
                {
                    foreach (var sale in sales)
                    {
                        sale.create_date = sale.create_date ?? DateTime.Today.AddDays(1);
                        sale.product_desc = sale.product_desc ?? string.Empty;
                        sale.product_type_desc = sale.product_type_desc ?? string.Empty;
                    }
                    detail.AddRange(sales.GroupBy(p => new { p.supporter_name, p.create_date, p.product_type_desc, p.product_desc }).Select(p => new CampaignReportMember { SupporterName = p.Key.supporter_name, SaleDate = ((DateTime)p.Key.create_date) > DateTime.Now ? string.Empty : ((DateTime)p.Key.create_date).ToString("yyyy-MM-dd"), ProductTypeDescription = p.Key.product_type_desc, ProductDescription = p.Key.product_desc, ItemsSold = (int)p.Sum(x => x.nb_subs), AmountSold = (decimal)p.Sum(x => x.amount), Profit = (decimal)p.Sum(x => x.profit) }));
                    totalAmount = detail.Sum(p => p.AmountSold);
                    totalProfit = detail.Sum(p => p.Profit);
                }
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, results = data, detail, totalAmount, totalProfit, from = ((DateTime)from).ToString("yyyy-MM-dd"), to = ((DateTime)to).ToString("yyyy-MM-dd") } };
            }
        }
        [RenderEventBranding]
        public ActionResult CampaignDonationReport(int participantId)
        {
            return View();
        }
        [RenderEventBranding, HttpPost]
        public JsonResult CampaignDonationReport(int participantId, bool isDetail = false)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var data = new List<CampaignDonationReportMember>();
                var detail = new List<CampaignDonationReportMember>();
                var donations = dataProvider.es_rpt_sponsor_email_donation_amount(@event.Id)
                                            .ToList();
                decimal totalAmount = 0M, totalProfit = 0M;
                if (!isDetail)
                {
                    data.AddRange(donations.GroupBy(p => p.member_name).Select(p => new CampaignDonationReportMember { MemberName = p.Key, EmailsSent = (int)p.Sum(x => x.email_sent), Donations = (int)p.Sum(x => x.donation_amount) }));
                    totalAmount = data.Sum(p => p.Donations);
                    totalProfit = data.Sum(p => p.Donations * 0.90M);
                }
                else
                {
                    detail.AddRange(donations.GroupBy(p => new { p.supporter_name }).Select(p => new CampaignDonationReportMember { SupporterName = p.Key.supporter_name, Donations = (decimal)p.Sum(x => x.donation_amount) }));
                    totalAmount = detail.Sum(p => p.Donations);
                    totalProfit = detail.Sum(p => p.Donations * 0.90M);
                }
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, results = data, detail, totalAmount, totalProfit } };
            }
        }
        [RenderEventBranding]
        public ActionResult GroupReport(int participantId)
        {
            return View();
        }
        [RenderEventBranding]
        public ActionResult GroupDonationReport(int participantId)
        {
            return View();
        }
        [RenderEventBranding]
        public ActionResult CampaignReportParticipant(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            ViewBag.MemberName = user.FirstName + " " + user.LastName;
            return View();
        }
        [RenderEventBranding]
        public ActionResult CampaignDonationReportParticipant(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            ViewBag.MemberName = user.FirstName + " " + user.LastName;
            return View();
        }
        [RenderEventBranding]
        public ActionResult CampaignReportParticipantMagazine(int participantId)
        {
            var user = ViewBag.User as Models.Branding.User;
            ViewBag.MemberName = user.FirstName + " " + user.LastName;
            return View();
        }
        [RenderEventBranding, HttpPost]
        public JsonResult MyCampaignReport(int participantId, string searchType, DateTime? from = null, DateTime? to = null)
        {
            var @event = ViewBag.Event as Models.Branding.Event;

            switch (searchType)
            {
                case "all":
                    from = @event.LaunchDate.AddDays(-2);
                    to = DateTime.Today.AddDays(1);
                    break;
                case "today":
                    from = DateTime.Today;
                    to = DateTime.Today.AddDays(1);
                    break;
                case "yesterday":
                    from = DateTime.Today.AddDays(-1);
                    to = DateTime.Today;
                    break;
                case "lastWeek":
                    from = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 7);
                    to = ((DateTime)from).AddDays(7);
                    break;
                case "lastMonth":
                    var lastMonth = DateTime.Today.AddMonths(-1);
                    from = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                    to = ((DateTime)from).AddMonths(1).AddDays(-1);
                    break;
                case "last7Days":
                    from = DateTime.Today.AddDays(-7);
                    to = DateTime.Today;
                    break;
                case "last30Days":
                    from = DateTime.Today.AddDays(-30);
                    to = DateTime.Today;
                    break;
                default:
                    break;
            }
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var data = new List<MyCampaignReport>();
                var sales = dataProvider.es_rpt_supporters_invited_product_type_by_date_range(participantId, from, to)
                                        .ToList();
                decimal totalAmount = 0M, totalProfit = 0M;
                data.AddRange(sales.GroupBy(p => new { p.first_name, p.last_name, p.create_date, p.product_type_desc, p.product_desc }).Select(p => new MyCampaignReport
                {
                    SupporterName = string.Concat(p.Key.first_name, " ", p.Key.last_name).Trim(),
                    SaleDate = ((DateTime)p.Key.create_date) > DateTime.Now ? string.Empty : ((DateTime)p.Key.create_date).ToString("yyyy-MM-dd"),
                    ProductTypeDescription = p.Key.product_type_desc,
                    ProductDescription = p.Key.product_desc,
                    ItemsSold = (int)p.Sum(x => x.nb_subs),
                    AmountSold = (decimal)p.Sum(x => x.amount),
                    Profit = (decimal)p.Sum(x => x.profit)
                }));
                totalAmount = data.Sum(p => p.AmountSold);
                totalProfit = data.Sum(p => p.Profit);
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, results = data, totalAmount, totalProfit, from = ((DateTime)from).ToString("yyyy-MM-dd"), to = ((DateTime)to).ToString("yyyy-MM-dd") } };
            }
        }
        [RenderEventBranding, HttpPost]
        public JsonResult MyCampaignDonationReport(int participantId)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var data = new List<MyCampaignDonationReport>();
                var sales = dataProvider.es_rpt_supporters_invited_donation_amount(participantId)
                                        .ToList();
                decimal totalAmount = 0M, totalProfit = 0M;
                data.AddRange(sales.GroupBy(p => new { p.first_name, p.last_name, p.create_date }).Select(p => new MyCampaignDonationReport
                {
                    SupporterName = string.Concat(p.Key.first_name, " ", p.Key.last_name).Trim(),
                    DonationDate = ((DateTime)p.Key.create_date) > DateTime.Now ? string.Empty : ((DateTime)p.Key.create_date).ToString("yyyy-MM-dd"),
                    Donations = (decimal)p.Sum(x => x.donation_amount),
                    Profit = (decimal)p.Sum(x => x.donation_amount * 0.90M)
                }));
                totalAmount = data.Sum(p => p.Donations);
                totalProfit = data.Sum(p => p.Profit);
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, results = data, totalAmount, totalProfit } };
            }
        }
        [RenderEventBranding, HttpPost]
        public JsonResult MyGroupReport(int participantId, string searchType, DateTime? from = null, DateTime? to = null)
        {
            var @event = ViewBag.Event as Models.Branding.Event;

            switch (searchType)
            {
                case "all":
                    from = @event.LaunchDate.AddDays(-2);
                    to = DateTime.Today.AddDays(1);
                    break;
                case "today":
                    from = DateTime.Today;
                    to = DateTime.Today.AddDays(1);
                    break;
                case "yesterday":
                    from = DateTime.Today.AddDays(-1);
                    to = DateTime.Today;
                    break;
                case "lastWeek":
                    from = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 7);
                    to = ((DateTime)from).AddDays(7);
                    break;
                case "lastMonth":
                    var lastMonth = DateTime.Today.AddMonths(-1);
                    from = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                    to = ((DateTime)from).AddMonths(1).AddDays(-1);
                    break;
                case "last7Days":
                    from = DateTime.Today.AddDays(-7);
                    to = DateTime.Today;
                    break;
                case "last30Days":
                    from = DateTime.Today.AddDays(-30);
                    to = DateTime.Today;
                    break;
                default:
                    break;
            }
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var data = new List<CampaignReportMember>();
                var sales = dataProvider.es_rpt_group_member_report(@event.Id)
                                        .ToList();
                decimal totalAmount = 0M, totalProfit = 0M;
                data.AddRange(sales.GroupBy(p => p.member_name).Select(p => new CampaignReportMember { MemberName = p.Key, EmailsSent = (int)p.Sum(x => x.email_sent), ItemsSold = (int)p.Sum(x => x.nb_subs), AmountSold = (decimal)p.Sum(x => x.amount), Profit = (decimal)p.Sum(x => x.profit) }));
                totalAmount = data.Sum(p => p.AmountSold);
                totalProfit = data.Sum(p => p.Profit);
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, results = data, totalAmount, totalProfit, from = ((DateTime)from).ToString("yyyy-MM-dd"), to = ((DateTime)to).ToString("yyyy-MM-dd") } };
            }
        }
        [RenderEventBranding, HttpPost]
        public JsonResult MyGroupDonationReport(int participantId)
        {
            var @event = ViewBag.Event as Models.Branding.Event;
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var data = new List<CampaignDonationReportMember>();
                var sales = dataProvider.es_rpt_group_member_donation_amount(@event.Id)
                                        .ToList();
                decimal totalAmount = 0M, totalProfit = 0M;
                data.AddRange(sales.GroupBy(p => p.member_name).Select(p => new CampaignDonationReportMember { MemberName = p.Key, EmailsSent = (int)p.Sum(x => x.email_sent), Donations = (decimal)p.Sum(x => x.donation_amount) }));
                totalAmount = data.Sum(p => p.Donations);
                totalProfit = data.Sum(p => p.Donations * 0.90M);
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, results = data, totalAmount, totalProfit } };
            }
        }
    }
}
