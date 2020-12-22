using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web.Mvc;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Web.MGP.Helpers;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Web.MGP.Helpers.Routes.Attributes;
using GA.BDC.Web.MGP.Helpers.Security;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Models.Views;
using WebMatrix.WebData;
using SWCorporate.AppServices.Shared;
using SWCorporate.AppServices.Shared.Contracts;
using Partner = GA.BDC.Web.MGP.Models.Branding.Partner;

namespace GA.BDC.Web.MGP.Controllers
{
   [RenderPartnerBranding]
   public class SecurityController : BaseController
   {
      [HttpPost]
      public JsonResult Login(SignIn model)
      {
         if (!ModelState.IsValid)
         {
            var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
         }
         var partnerId = Convert.ToInt32(Session[Constants.SESSION_KEY_PARTNER_ID]);
         var isAuthenticated = WebSecurity.Login(string.Concat(model.Username, "|", partnerId), model.Password, model.RememberMe);
         if (!isAuthenticated)
         {
            using (var dataProvider = new DataProvider())
            {
               var users = (from u in dataProvider.users
                            where u.email_address == model.Username && u.password == model.Password
                            group u by new { u.email_address, u.password, u.partner_id }
                               into g
                            select new { g.Key.partner_id });

               if (users.Count() == 1)
               {
                  var newPartnerId = users.Single().partner_id.HasValue ? users.Single().partner_id.Value : 0;
                  Session[Constants.SESSION_KEY_PARTNER_ID] = newPartnerId;
                  isAuthenticated = WebSecurity.Login(string.Concat(model.Username, "|", newPartnerId), model.Password, model.RememberMe);
               }
               else
               {
                  // Find the last partner id used
                  var partners = users.ToList();
                  int newPartnerId = 0;
                  var userCreateDate = DateTime.MinValue;
                  foreach (var id in partners)
                  {
                     var partnerIdExtra = id.partner_id.HasValue ? id.partner_id.Value : 0;
                     var createDate = (from u in dataProvider.users
                                       where u.email_address == model.Username && u.password == model.Password
                                          && u.partner_id == partnerIdExtra
                                       select u.create_date).First();
                     if (createDate > userCreateDate)
                     {
                        userCreateDate = createDate;
                        newPartnerId = partnerIdExtra;
                     }
                  }
                  Session[Constants.SESSION_KEY_PARTNER_ID] = newPartnerId;
                  isAuthenticated = WebSecurity.Login(string.Concat(model.Username, "|", newPartnerId), model.Password, model.RememberMe);
               }
            }
         }
         if (isAuthenticated)
         {
            Session[Constants.SESSION_KEY_PASSWORD] = model.Password;
         }
         return isAuthenticated ? new JsonResult { Data = new { success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet } : new JsonResult { Data = new { success = false, responseText = "Email Address or Password invalid" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
      }

      [HttpPost]
      public ActionResult LoginExternal(string providerName)
      {
         var mgpOAuthWebSecurity = new MGPOAuthWebSecurity();
         if (string.IsNullOrEmpty(providerName) || string.IsNullOrEmpty(mgpOAuthWebSecurity.GetOAuthClientData(providerName)))
         {
            return RedirectToActionPermanent("Index", "Home");
         }
         return new ExternalLoginResult(providerName, "https://" + Request.Url.Host + Url.Action("LoginExternalCallback", new { providerName }));
      }

		public ActionResult LoginExternalCallback(string providerName)
		{
			var mgpOAuthWebSecurity = new MGPOAuthWebSecurity();
			var result = mgpOAuthWebSecurity.VerifyAuthentication(providerName);
			var partnerId = Convert.ToInt32(Session[Constants.SESSION_KEY_PARTNER_ID]);
			if (!result.IsSuccesful)
			{
				return RedirectToAction("LoginExternalFailed", "Home");
			}
			var mgpSimpleMembershipProvider = new MGPSimpleMembershipProvider();
			var userId = mgpSimpleMembershipProvider.GetUserIdFromOAuth(result.Provider, result.ProviderUserId);
			if (userId > 0)
			{
				user userFound;
				using (var dataProvider = new DataProvider())
				{
					userFound = dataProvider.users.FirstOrDefault(p => p.user_id == userId && p.partner_id == partnerId);
				}
				if (userFound != null)
				{
					WebSecurity.Login(string.Concat(userFound.email_address, "|", partnerId), userFound.password, true);
					return RedirectToAction("Campaigns", "CampaignManager");
				}
				else
				{
					return RedirectToAction("LoginExternalFailed", "Home");
				}
			}
			if (!string.IsNullOrEmpty(result.UserName))
			{
				using (var dataProvider = new DataProvider())
				{

					var users = dataProvider.users.Where(p => p.email_address == result.UserName);
					if (users.Any())
					{
						var userFound = users.First();
						mgpSimpleMembershipProvider.CreateOrUpdateOAuthAccount(result.Provider, result.ProviderUserId, $"{userFound.email_address}|{partnerId}");
						WebSecurity.Login(string.Concat(userFound.email_address, "|", partnerId), userFound.password, true);
						return RedirectToAction("Campaigns", "CampaignManager");
					}
				}
			}
			if (User.Identity.IsAuthenticated)
			{
				mgpSimpleMembershipProvider.CreateOrUpdateOAuthAccount(result.Provider, result.ProviderUserId, $"{User.Identity.Name}|{partnerId}");
				return RedirectToAction("Campaigns", "CampaignManager");
			}
			
			var reg = new RegisterExternal(result);
			if (string.IsNullOrEmpty(reg.Email))
			{
				TempData["register"] = reg;
				return RedirectToAction("Step0", "Home");
			}
			var res = Register(reg, reg.ProviderName, reg.ProviderUserId) as JsonResult;
			var data = res?.Data;
			if (data != null)
			{
				var status = data.GetType().GetProperty("success")?.GetValue(data, null);
				if (status != null && Convert.ToBoolean(status))
				{
					var participantId = data.GetType().GetProperty("participantId")?.GetValue(data, null);
					if (participantId != null && Convert.ToInt32(participantId) > 0)
					{
						return RedirectToAction("Step1", "CampaignManager", new { participantId });
					}
				}
			}
			var errorMessage = $"Error while registering with: <{reg.ProviderName}>";
			return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
		}

      [HttpGet, MGPAuthentication]
      public ActionResult Logout()
      {
         WebSecurity.Logout();

         return RedirectToAction("Index", "Home");
      }

      [HttpGet]
      public ActionResult RecoverPassword()
      {
         return View();
      }

      [HttpPost]
      public JsonResult RecoverPassword(string email)
      {
         if (string.IsNullOrEmpty(email))
         {
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "Please specify an Email." } };
         }
         using (var dataProvider = new DataProvider())
         {
            var userFound = dataProvider.users.FirstOrDefault(p => p.username == email);
            if (userFound == null)
            {
               return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "The Email doesn't exist." } };
            }
            var message =
	            $@"We have received a request to recover your lost password. This email is being sent to your registered email address only.<br><br><strong>Password: {userFound.password}</strong><br><br>To login with your account information, please visit <a href=http://www.efundraising.com>http://www.efundraising.com</a>";
            using (var dispatchClient = new DispatchClient("BDC MassMailer"))
            {
               var mail = new MailEntity($"{"Fundraising.com"} <{"SalesSupport.EFR@fundraising.com"}>", $"{"Fundraising.com"} <{"SalesSupport.EFR@fundraising.com"}>", "Your fundraising password", message, true)
               {
                  AlternateView = message
               };
               mail.MailRecipients.Add(new MailRecipientEntity(MailRecipientType.To, email, string.Format("{0} {1}", userFound.first_name, userFound.last_name)));
               dispatchClient.QueueMailForDispatch(mail);
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = true, responseText = "Your password was sent. You will receive an Email in a couple of minutes." } };
         }
      }

      [HttpPost]
      public ActionResult Register(Registration model, string providerName = "", string providerUserId = "", Partner overridenPartner = null)
      {
         var partner = ViewBag.Partner as Partner ?? overridenPartner;
	      var partnerId = partner.Id;
         if (partner.CanNotCreateEvent)
         {
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "We're sorry, you are not authorized to create campaigns." } };
         }
         if (!ModelState.IsValid)
         {
            var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
         }

         bool userExists = WebSecurity.UserExists(string.Concat(model.Email, "|", partnerId));

         if (userExists && string.IsNullOrEmpty(providerName) && string.IsNullOrEmpty(providerUserId))
         {
            return new JsonResult
            {
               JsonRequestBehavior = JsonRequestBehavior.AllowGet,
               Data =
                   new
                   {
                      success = false,
                      responseText = "You are already registered. Please go to Sign In page to enter."
                   }
            };
         }

         var creationChannelId = 1;
         creationChannelId = Session?[Constants.SESSION_KEY_CREATION_CHANNEL_ID] != null
                                         ? Convert.ToInt32(Session[Constants.SESSION_KEY_CREATION_CHANNEL_ID])
                                         : creationChannelId;
         if (!userExists)
         {
            WebSecurity.CreateUserAndAccount(model.Email, model.Password1,
                new
                {
                   model.FirstName,
                   model.LastName,
                   model.Terms,
                   model.Newsletter,
                   PartnerId = partnerId,
                   ChannelId = creationChannelId
                });
         }
         var leadId = Session?[Constants.SESSION_KEY_LEAD_ID] != null
                         ? Convert.ToInt32(Session[Constants.SESSION_KEY_LEAD_ID])
                         : int.MinValue;
         int participantId;
         using (var dataProvider = new DataProvider())
         {
            using (var transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 5, 0)))
            {
               var userRegistered = (from u in dataProvider.users
                                     from m in dataProvider.members
                                     from mh in dataProvider.member_hierarchy
                                     where u.email_address == model.Email
                                           && u.partner_id == partnerId
                                           && m.user_id == u.user_id
                                           && mh.member_id == m.member_id
                                     select
                                         new { UserId = u.user_id, MemberId = m.member_id, MemberHiearchyId = mh.member_hierarchy_id })
                   .First();
               var temporaryGroupName = DateTime.Now.Ticks.ToString();
               var group = model.GroupId > 0 ? dataProvider.groups.First(p => p.group_id == model.GroupId) : new group
               {
                  create_date = DateTime.Now,
                  group_name = temporaryGroupName, //defaulted to temporary name
                  partner_id = partnerId,
                  group_url = string.Empty,
                  redirect = temporaryGroupName, //defaulted to temporary name
                  sponsor_id = userRegistered.MemberHiearchyId,
                  comments = string.Empty,
                  lead_id = (leadId < 0 ? null : (int?)leadId)
               };
               if (group.group_id == 0)
               {
                  dataProvider.groups.Add(group);
                  dataProvider.SaveChanges();
               }

               var @event = new _event
               {
                  active = true,
                  create_date = DateTime.Now,
                  culture_code = "en-US",
                  event_name = temporaryGroupName, //Defaulted to temporary name
                  group_type_id = 1,
                  start_date = DateTime.Now,
                  event_type_id = 1, // Defaulted to Event with Subpages
                  event_status_id = 1,
                  redirect = temporaryGroupName, //defaulted to temporary name
                  displayable = true,
                  referral_application = 1,
                  profit_calculated = @partner.ProfitPercentage,
                  profit_group_id = @partner.ProfitGroupId ?? 2
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

               var useDefaultPartnerPaymentConfiguration = @partner.PaymentTo == PaymentTo.Partner;
               if (useDefaultPartnerPaymentConfiguration)
               {
                  if (Convert.ToBoolean(@partner.ProductOffer == ProductOffer.DonationOnly))
                  {
                     var groupIdString = ConfigurationManager.AppSettings["DP_GroupID_" + partnerId];
                     if (!string.IsNullOrEmpty(groupIdString))
                     {
                        var donationGroupId = int.Parse(groupIdString);

                        var phoneDonation = (from g in dataProvider.groups
                                             from pi in dataProvider.payment_info
                                             from p in dataProvider.phone_number
                                             where pi.group_id == g.group_id
                                                   && g.group_id == donationGroupId
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
                                                           && g.group_id == donationGroupId
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
                                                         && g.group_id == donationGroupId
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
                        matching_code = "12919We20",
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
                        on_behalf_of_name = string.Concat(model.FirstName, " ", model.LastName),
                        payment_name = string.Concat(model.FirstName, " ", model.LastName),
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
                     phone_number1 = "000000000", // Defaulted to zeros for now
                  };
                  dataProvider.phone_number.Add(groupPaymentInfoPhone);
                  dataProvider.SaveChanges();

                  var groupPostalAddress = new postal_address
                  {
                     country_code = "US",
                     subdivision_code = "US-NY", // Defaulted to NY
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
                     on_behalf_of_name = string.Concat(model.FirstName, " ", model.LastName),
                     payment_name = string.Concat(model.FirstName, " ", model.LastName),
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
                  member_hierarchy_id = userRegistered.MemberHiearchyId,
                  agree_term_services = true,
                  holiday_reminders = false,
                  salutation = string.Concat(model.FirstName, " ", model.LastName)
               };
               dataProvider.event_participation.Add(eventParticipation);
               dataProvider.SaveChanges();
               default_personalization defaultPersonalization = dataProvider.default_personalizations.FirstOrDefault(
                       p => p.PartnerId == partnerId && p.EventTypeId == 1 && p.ParticipantTypeId == 1);
               if (defaultPersonalization == null)
               {
                  defaultPersonalization = dataProvider.default_personalizations.First(p => p.PartnerId == 0 && p.EventTypeId == 1 && p.ParticipantTypeId == 1);
               }
               var personalization = new personalization
               {
                  create_date = DateTime.Now,
                  event_participation_id = eventParticipation.event_participation_id,
                  body = defaultPersonalization != null ? defaultPersonalization.Body.Replace("[++redirect++]", temporaryGroupName) : string.Empty,
                  fundraising_goal = defaultPersonalization != null ? (decimal)defaultPersonalization.Goal : (decimal)0.0,
                  redirect = temporaryGroupName,
                  header_title1 = defaultPersonalization != null ? defaultPersonalization.HeaderTitle1 : string.Empty,
                  header_title2 = defaultPersonalization != null ? defaultPersonalization.HeaderTitle2 : string.Empty,
                  displayGroupMessage = true,
               };
               dataProvider.personalizations.Add(personalization);
               dataProvider.SaveChanges();
               participantId = eventParticipation.event_participation_id;
               transactionScope.Complete();
            }
         }
         if (!string.IsNullOrEmpty(providerName) && !string.IsNullOrEmpty(providerUserId))
         {
            var mgpSimpleMembershipProvider = new MGPSimpleMembershipProvider();
            mgpSimpleMembershipProvider.CreateOrUpdateOAuthAccount(providerName, providerUserId, $"{model.Email}|{partnerId}");
         }
         WebSecurity.Login(string.Concat(model.Email, "|", partnerId), model.Password1, true);
         return new JsonResult { Data = new { success = true, participantId }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
      }

      [HttpPost, RenderPublicEventBranding]
      public JsonResult ParticipantJoin(int eventId, JoinParticipant model)
      {
         if (!ModelState.IsValid)
         {
            var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
         }
         var partnerId = Convert.ToInt32(Session[Constants.SESSION_KEY_PARTNER_ID]);
         var @event = ViewBag.PublicEvent as PublicEvent;
         int participantId;
         using (var dataProvider = new DataProvider())
         {
            if (WebSecurity.Login(string.Concat(model.Username, "|", partnerId), model.Password, true))
            {
               var sponsorMemberHierarchy = (from mh in dataProvider.member_hierarchy
                                             from ep in dataProvider.event_participation
                                             where mh.member_hierarchy_id == ep.member_hierarchy_id
                                                && ep.event_id == @event.Id
                                                && ep.participation_channel_id == 3
                                             select mh).Single();
               var existingUser = (from m in dataProvider.members
                                   from mh in dataProvider.member_hierarchy
                                   from cc in dataProvider.creation_channel
                                   from ep in dataProvider.event_participation
                                   where m.email_address == model.Username
                                          && m.partner_id == partnerId
                                          && mh.member_id == m.member_id
                                          && mh.creation_channel_id == cc.creation_channel_id
                                          && mh.member_hierarchy_id == ep.member_hierarchy_id
                                   select
                                       new
                                       {
                                          UserId = m.user_id,
                                          MemberId = m.member_id,
                                          MemberHierarchyId = mh.member_hierarchy_id,
                                          EventId = ep.event_id,
                                          MemberTypeId = cc.member_type_id,
                                          Email = m.email_address,
                                          Password = m.password,
                                          Deleted = m.deleted,
                                          Unsubscribe = m.unsubscribe,
                                          EventParticipationId = ep.event_participation_id,
                                          FirstName = m.first_name,
                                          LastName = m.last_name
                                       }).ToList();

               if (existingUser.Any(p => p.MemberTypeId == 2 && p.EventId == eventId && p.UserId > 0))
               {
                  var urlHome = new UrlHelper(Request.RequestContext).Action("SignIn", "Home", new { Email = model.Username });
                  return new JsonResult
                  {
                     Data = new { success = true, url = urlHome },
                     JsonRequestBehavior = JsonRequestBehavior.AllowGet
                  };
               }

               var userId = existingUser.First(p => p.UserId > 0).UserId;
               default_personalization defaultPersonalization;
               var defaultPartnerPersonalization = dataProvider.default_personalizations.Where(p => p.PartnerId == partnerId && p.EventTypeId == (int)@event.EventType && p.ParticipantTypeId == 2);
               if (defaultPartnerPersonalization.Any())
               {
                  defaultPersonalization = defaultPartnerPersonalization.First();
               }
               else
               {
                  defaultPersonalization =
                  dataProvider.default_personalizations.Single(
                      p => p.PartnerId == 0 && p.EventTypeId == (int)@event.EventType && p.ParticipantTypeId == 2);
               }
               /* Member Invited */
               if (existingUser.Any(p => p.MemberTypeId == 2 && p.EventId == eventId))
               {
                  // Please keep the following Single() because if it bombs than there is a bigger issue with data
                  var userInvited = existingUser.Single(p => p.EventId == eventId && p.MemberTypeId == 2);
                  var @memberInvited = dataProvider.members.Single(m => m.member_id == userInvited.MemberId);
                  @memberInvited.user_id = userId;
                  @memberInvited.email_address = model.Username;
                  @memberInvited.password = model.Password;
                  @memberInvited.deleted = false;
                  dataProvider.SaveChanges();

                  if (
                      !dataProvider.personalizations.Any(
                          p => p.event_participation_id == userInvited.EventParticipationId))
                  {
                     var personalization = new personalization
                     {
                        create_date = DateTime.Now,
                        event_participation_id = userInvited.EventParticipationId,
                        body = defaultPersonalization.Body,
                        fundraising_goal = (decimal)defaultPersonalization.Goal,
                        redirect = FindSuggestedCampaignRedirect(string.Concat(userInvited.FirstName, userInvited.LastName).CleanupRedirect()),
                        header_title1 = defaultPersonalization.HeaderTitle1,
                        header_title2 = defaultPersonalization.HeaderTitle2,
                        displayGroupMessage = true,
                     };
                     dataProvider.personalizations.Add(personalization);
                     dataProvider.SaveChanges();
                  }
                  participantId = userInvited.EventParticipationId;
                  var urlRegFromSignIn = new UrlHelper(Request.RequestContext).Action("Register",
                      "CampaignManager", new { participantId });
                  return new JsonResult
                  {
                     Data = new { success = true, url = urlRegFromSignIn },
                     JsonRequestBehavior = JsonRequestBehavior.AllowGet
                  };
               }
               else
               {
                  var signInUser = existingUser.First(p => p.UserId == userId);
                  var member = new member
                  {
                     create_date = DateTime.Now,
                     password = model.Password,
                     partner_id = partnerId,
                     user_id = userId,
                     email_address = model.Username,
                     last_name = WebUtility.HtmlDecode(WebUtility.HtmlDecode(signInUser.LastName)),
                     first_name = WebUtility.HtmlDecode(WebUtility.HtmlDecode(signInUser.FirstName)),
                     culture_code = "en-US",
                     opt_status_id = 1,
                     unsubscribe = !Convert.ToBoolean(model.Terms),
                     comments = "Member created automatically by MGP Website after participant registration",
                  };
                  dataProvider.members.Add(member);
                  dataProvider.SaveChanges();
                  var memberHierarchy = new member_hierarchy
                  {
                     create_date = DateTime.Now,
                     active = true,
                     creation_channel_id = 22,
                     member_id = member.member_id,
                     parent_member_hierarchy_id = sponsorMemberHierarchy.member_hierarchy_id,
                     unsubscribe = !Convert.ToBoolean(model.Terms)
                  };
                  dataProvider.member_hierarchy.Add(memberHierarchy);
                  dataProvider.SaveChanges();
                  var eventParticipation = new event_participation
                  {
                     participation_channel_id = 2,
                     create_date = DateTime.Now,
                     event_id = eventId,
                     member_hierarchy_id = memberHierarchy.member_hierarchy_id,
                     agree_term_services = true,
                     holiday_reminders = false,
                     salutation = string.Concat(signInUser.FirstName, " ", signInUser.LastName)
                  };
                  dataProvider.event_participation.Add(eventParticipation);
                  dataProvider.SaveChanges();
                  var personalization = new personalization
                  {
                     create_date = DateTime.Now,
                     event_participation_id = eventParticipation.event_participation_id,
                     body = defaultPersonalization.Body,
                     fundraising_goal = (decimal)defaultPersonalization.Goal,
                     redirect = FindSuggestedCampaignRedirect(string.Concat(signInUser.FirstName, signInUser.LastName).CleanupRedirect()),
                     header_title1 = defaultPersonalization.HeaderTitle1,
                     header_title2 = defaultPersonalization.HeaderTitle2,
                     displayGroupMessage = true,
                  };
                  dataProvider.personalizations.Add(personalization);
                  dataProvider.SaveChanges();
                  participantId = eventParticipation.event_participation_id;
                  var urlRegFromSignIn = new UrlHelper(Request.RequestContext).Action("Register",
                      "CampaignManager", new { participantId });
                  return new JsonResult
                  {
                     Data = new { success = true, url = urlRegFromSignIn },
                     JsonRequestBehavior = JsonRequestBehavior.AllowGet
                  };
               }

            }
            return new JsonResult { Data = new { success = false, responseText = "Email Address or Password invalid" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
         }
      }

      [HttpPost, RenderPublicEventBranding]
      public ActionResult ParticipantRegister(int eventId, RegistrationParticipant model)
      {
         if (!ModelState.IsValid)
         {
            var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
         }

         var partnerId = Convert.ToInt32(Session[Constants.SESSION_KEY_PARTNER_ID]);
         var @event = ViewBag.PublicEvent as PublicEvent;
         var invitedMember = false;
         int participantId;

         using (var dataProvider = new DataProvider())
         {
            var sponsorMemberHierarchies = (from mh in dataProvider.member_hierarchy
                                            from ep in dataProvider.event_participation
                                            where mh.member_hierarchy_id == ep.member_hierarchy_id
                                               && ep.event_id == @event.Id
                                               && ep.participation_channel_id == 3
                                            select mh).ToList();
            var sponsorMemberHierarchy = sponsorMemberHierarchies.First();

            var existingUser = (from m in dataProvider.members
                                from mh in dataProvider.member_hierarchy
                                from cc in dataProvider.creation_channel
                                from ep in dataProvider.event_participation
                                where m.email_address == model.Email
                                       && m.partner_id == partnerId
                                       && mh.member_id == m.member_id
                                       && mh.creation_channel_id == cc.creation_channel_id
                                       && mh.member_hierarchy_id == ep.member_hierarchy_id
                                select
                                    new
                                    {
                                       UserId = m.user_id,
                                       MemberId = m.member_id,
                                       MemberHierarchyId = mh.member_hierarchy_id,
                                       EventId = ep.event_id,
                                       MemberTypeId = cc.member_type_id,
                                       Email = m.email_address,
                                       Password = m.password,
                                       Deleted = m.deleted,
                                       Unsubscribe = m.unsubscribe,
                                       EventParticipationId = ep.event_participation_id,
                                       FirstName = m.first_name,
                                       LastName = m.last_name
                                    }).ToList();
            if (existingUser.Any())
            {
               if (existingUser.Any(p => p.UserId > 0 && p.MemberTypeId == 2 && p.EventId == @event.Id))
               {
                  var urlHome = new UrlHelper(Request.RequestContext).Action("SignIn", "Home", new { model.Email });
                  return new JsonResult { Data = new { success = true, url = urlHome }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
               }
               if (existingUser.Any(p => p.UserId > 0))
               {
                  return new JsonResult
                  {
                     JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                     Data =
                         new
                         {
                            success = false,
                            loginEmail = existingUser.First(p => p.UserId > 0).Email,
                            responseText = "An account already exists under that email address. Simply Join the campaign."
                         }
                  };
               }
               if (existingUser.Any(p => p.Password.IsNotEmpty()))
               {
                  using (var data = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
                  {
                     data.es_validate_login(model.Email, model.Password1, partnerId);
                  }
               }
               if (existingUser.Any(p => p.MemberTypeId == 2 && p.EventId == @event.Id))
               {
                  invitedMember = true;
               }
               if (existingUser.Any(p => p.MemberTypeId == 2 && p.Deleted))
               {
                  existingUser.Where(p => p.MemberTypeId == 2 && p.Deleted).ToList().ForEach(p => UndeleteParticipantMember(p.MemberId));
               }
            }
            default_personalization defaultPersonalization;
            var defaultPartnerPersonalization = dataProvider.default_personalizations.Where(p => p.PartnerId == partnerId && p.EventTypeId == (int)@event.EventType && p.ParticipantTypeId == 2);
            if (defaultPartnerPersonalization.Any())
            {
               defaultPersonalization = defaultPartnerPersonalization.First();
            }
            else
            {
               defaultPersonalization =
               dataProvider.default_personalizations.Single(
                   p => p.PartnerId == 0 && p.EventTypeId == (int)@event.EventType && p.ParticipantTypeId == 2);
            }
            if (!invitedMember)
            {
               WebSecurity.CreateUserAndAccount(model.Email, model.Password1,
                       new
                       {
                          model.FirstName,
                          model.LastName,
                          model.Terms,
                          Newsletter = true,
                          PartnerId = Session[Constants.SESSION_KEY_PARTNER_ID],
                          ChannelId = 22,
                          ParentMemberHierarchyId = sponsorMemberHierarchy.member_hierarchy_id
                       });
               var usersRegistered = (from u in dataProvider.users
                                      from m in dataProvider.members
                                      from mh in dataProvider.member_hierarchy
                                      where u.email_address == model.Email
                                            && u.partner_id == partnerId
                                            && m.user_id == u.user_id
                                            && mh.member_id == m.member_id
                                            && mh.parent_member_hierarchy_id == sponsorMemberHierarchy.member_hierarchy_id
                                            && mh.creation_channel_id == 22
                                      select
                                          new { UserId = u.user_id, MemberId = m.member_id, MemberHiearchyId = mh.member_hierarchy_id }).ToList();
               var userRegistered = usersRegistered.First();
               var eventParticipation = new event_participation
               {
                  participation_channel_id = 2,
                  create_date = DateTime.Now,
                  event_id = @event.Id,
                  member_hierarchy_id = userRegistered.MemberHiearchyId,
                  agree_term_services = true,
                  holiday_reminders = false,
                  salutation = string.Concat(model.FirstName, " ", model.LastName)
               };
               dataProvider.event_participation.Add(eventParticipation);
               dataProvider.SaveChanges();
               var personalization = new personalization
               {
                  create_date = DateTime.Now,
                  event_participation_id = eventParticipation.event_participation_id,
                  body = defaultPersonalization.Body,
                  fundraising_goal = (decimal)defaultPersonalization.Goal,
                  redirect = FindSuggestedCampaignRedirect(string.Concat(model.FirstName, model.LastName).CleanupRedirect()),
                  header_title1 = defaultPersonalization.HeaderTitle1,
                  header_title2 = defaultPersonalization.HeaderTitle2,
                  displayGroupMessage = true,
               };
               dataProvider.personalizations.Add(personalization);
               dataProvider.SaveChanges();
               participantId = eventParticipation.event_participation_id;
            }
            else
            {
               int touchId = Session[Constants.SESSION_KEY_TOUCH_ID] != null
                       ? Convert.ToInt32(Session[Constants.SESSION_KEY_TOUCH_ID])
                       : int.MinValue;
               int? invitedEpId = null;
               if (touchId > 0)
               {
                  invitedEpId = dataProvider.touches.Single(t => t.touch_id == touchId).event_participation_id;
               }
               var userInvited = existingUser.First(p => p.EventId == @event.Id && p.MemberTypeId == 2 &&
                                                    (invitedEpId == null || invitedEpId != null && p.EventParticipationId == invitedEpId.Value));
               var user = new user
               {
                  username = model.Email,
                  agree_term_services = model.Terms,
                  create_date = DateTime.Now,
                  culture_code = "en-US",
                  email_address = model.Email,
                  first_name = WebUtility.HtmlDecode(WebUtility.HtmlDecode(model.FirstName)),
                  last_name = WebUtility.HtmlDecode(WebUtility.HtmlDecode(model.LastName)),
                  password = model.Password1,
                  unsubscribe = userInvited.Unsubscribe,
                  partner_id = partnerId,
                  opt_status_id = false,
               };
               dataProvider.users.Add(user);
               dataProvider.SaveChanges();
               var @memberInvited = dataProvider.members.Single(m => m.member_id == userInvited.MemberId);
               @memberInvited.user_id = user.user_id;
               @memberInvited.email_address = model.Email;
               @memberInvited.first_name = model.FirstName;
               @memberInvited.last_name = model.LastName;
               @memberInvited.password = model.Password1;
               dataProvider.SaveChanges();

               if (!dataProvider.personalizations.Any(p => p.event_participation_id == userInvited.EventParticipationId))
               {
                  var personalization = new personalization
                  {
                     create_date = DateTime.Now,
                     event_participation_id = userInvited.EventParticipationId,
                     body = defaultPersonalization.Body,
                     fundraising_goal = (decimal)defaultPersonalization.Goal,
                     redirect = FindSuggestedCampaignRedirect(string.Concat(model.FirstName, model.LastName).CleanupRedirect()),
                     header_title1 = defaultPersonalization.HeaderTitle1,
                     header_title2 = defaultPersonalization.HeaderTitle2,
                     displayGroupMessage = true,
                  };
                  dataProvider.personalizations.Add(personalization);
                  dataProvider.SaveChanges();
               }
               participantId = userInvited.EventParticipationId;
            }
         }

         WebSecurity.Login(string.Concat(model.Email, "|", partnerId), model.Password1, true);
         return new JsonResult { Data = new { success = true, url = new UrlHelper(Request.RequestContext).Action("Register", "CampaignManager", new { participantId }) }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
      }

      private void UndeleteParticipantMember(int memberId)
      {
         using (var dataProvider = new DataProvider())
         {
            var @member = dataProvider.members.Single(m => m.member_id == memberId);
            @member.deleted = false;
            dataProvider.SaveChanges();
         }
      }
      
      private string FindSuggestedCampaignRedirect(string campaignName)
      {
         var incremental = 1;

         using (var dataProvider = new DataProvider())
         {
            var personalizations = dataProvider.personalizations.Select(p => p.redirect).ToList();
            if (personalizations.All(p => !String.Equals(p, campaignName, StringComparison.CurrentCultureIgnoreCase)))
            {
               return campaignName;
            }
            while (true)
            {
               var newName = string.Concat(campaignName, incremental);
               var existingRedirects = personalizations.Any(p => String.Equals(p, newName, StringComparison.CurrentCultureIgnoreCase));
               if (!existingRedirects)
               {
                  return newName;
               }
               incremental++;
            }
         }
      }
   }

   internal class ExternalLoginResult : ActionResult
   {
      public ExternalLoginResult(string provider, string returnUrl)
      {
         Provider = provider;
         ReturnUrl = returnUrl;
      }

      public string Provider { get; private set; }
      public string ReturnUrl { get; private set; }

      public override void ExecuteResult(ControllerContext context)
      {
         var mgpOAuthWebSecurity = new MGPOAuthWebSecurity();
         mgpOAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
      }
   }
}
