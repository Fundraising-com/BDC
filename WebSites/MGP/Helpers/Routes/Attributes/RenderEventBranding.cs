using System;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Security;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Routing;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Data.MGP.esubs_global_v2.Models;

namespace GA.BDC.Web.MGP.Helpers.Routes.Attributes
{
    public class RenderEventBranding : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           if (filterContext.ActionDescriptor.ActionName == "OAuthCallback")
           {
              filterContext.Controller.ViewBag.Event = new Event() { ParticipantId = 0 };
              return;
           }
            if (filterContext.HttpContext.Session == null) throw new Exception("Session is null");
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID) == null)
            {
                filterContext.HttpContext.Response.Redirect(string.Format("http://{0}/{1}", filterContext.HttpContext.Request.Url.Authority, "Home/SignIn"));
                return;
            }
            var partnerId = Convert.ToInt32(filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTNER_ID]);
            var participantId = Convert.ToInt32(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID).AttemptedValue);
            var eventId = 0;
            var branding = new Event();
            UserType userType;
            using (var dataProvider = new DataProvider())
            {
                #region Perform Data Integrity Check                
                #endregion

                #region Security Validation, we verify that the User logged in belongs to the ParticipantId that he is trying to access
                var loggedUser = filterContext.Controller.ViewBag.User as User;
                
                // Validate the logged users' partner Id matches the sessions partner Id
                var userIsValid = (from u in dataProvider.users
                                  where u.user_id == loggedUser.Id && u.partner_id == partnerId
                                 select u.user_id).Any();
                if (!userIsValid)
                {
                    var email = loggedUser.Email;
                    loggedUser = (from u in dataProvider.users
                                  where u.email_address == email && u.partner_id == partnerId
                                  select new User { Id = u.user_id, FirstName = u.first_name, LastName = u.last_name, Email = u.email_address, Password = u.password, IsLoggedIn = true }).FirstOrDefault();
                    if (loggedUser == null)
                    {
                        loggedUser = (from u in dataProvider.users
                                      where u.email_address == email
                                      select new User { Id = u.user_id, FirstName = u.first_name, LastName = u.last_name, Email = u.email_address, Password = u.password, IsLoggedIn = true }).FirstOrDefault();
                        if (loggedUser == null)
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "403" } });
                            return;
                        }
                    }
                }
                var found = (from ep in dataProvider.event_participation
                            from mh in dataProvider.member_hierarchy
                            from m in dataProvider.members
                            from u in dataProvider.users
                            where ep.member_hierarchy_id == mh.member_hierarchy_id
                                  && mh.member_id == m.member_id
                                  && m.user_id == u.user_id
                                  && ep.event_participation_id == participantId
                                  && u.user_id == loggedUser.Id
                            select u).FirstOrDefault();
                if (found == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "403" } });
                    return;
                }
                #endregion
                var @event = (from ep in dataProvider.event_participation
                              from e in dataProvider.events
                              where ep.event_id == e.event_id
                                    && ep.event_participation_id == participantId
                              select e).First();
                eventId = @event.event_id;
                branding.Id = eventId;
                branding.Name = @event.event_name;
                branding.LaunchDate = @event.start_date;
                branding.Status = @event.active;
                branding.EventType = (EventTypeInfo)Enum.Parse(typeof(EventTypeInfo), @event.event_type_id.ToString(CultureInfo.InvariantCulture));

                var sponsorPersonalization = (from ep in dataProvider.event_participation
                                              from p in dataProvider.personalizations
                                              where ep.event_participation_id == p.event_participation_id
                                                    && ep.event_id == @event.event_id
                                                    && ep.participation_channel_id == 3
                                              select p).FirstOrDefault();
                if (sponsorPersonalization == null)
                {
                    // this means the sponsor doesnt have a personalization, probably because it was autocreated, its a weird scenario
                    default_personalization defaultPersonalization;
                    var defaultPartnerPersonalization =
                        dataProvider.default_personalizations.Where(
                            p => p.PartnerId == partnerId && p.EventTypeId == @event.event_type_id && p.ParticipantTypeId == 1);
                    if (defaultPartnerPersonalization.Any())
                    {
                        defaultPersonalization = defaultPartnerPersonalization.First();
                    }
                    else
                    {
                        defaultPersonalization =
                        dataProvider.default_personalizations.Single(
                            p => p.PartnerId == 0 && p.EventTypeId == @event.event_type_id && p.ParticipantTypeId == 1);
                    }
                    sponsorPersonalization = new personalization
                    {
                        create_date = DateTime.Now,
                        event_participation_id = participantId,
                        body = defaultPersonalization.Body,
                        fundraising_goal = (decimal)defaultPersonalization.Goal,
                        redirect = @event.redirect,
                        header_title1 = defaultPersonalization.HeaderTitle1,
                        header_title2 = defaultPersonalization.HeaderTitle2,
                        displayGroupMessage = true,
                    };
                    dataProvider.personalizations.Add(sponsorPersonalization);
                    dataProvider.SaveChanges();
                }
                branding.Created = sponsorPersonalization.create_date;
                branding.Message = sponsorPersonalization.body;
                branding.Goal = sponsorPersonalization.fundraising_goal.HasValue ? (decimal)sponsorPersonalization.fundraising_goal : 0M;
                branding.Redirect = sponsorPersonalization.redirect;                

                branding.IsGASavingsCard = (from n in dataProvider.event_gasavingcard
                                            where n.event_id == @event.event_id
                                            select n).Any();
                var memberTypeId = (from ep in dataProvider.event_participation
                                    from mh in dataProvider.member_hierarchy
                                    from cc in dataProvider.creation_channel
                                    where mh.member_hierarchy_id == ep.member_hierarchy_id
                                        && mh.creation_channel_id == cc.creation_channel_id
                                        && ep.event_participation_id == participantId
                                    select cc.member_type_id).FirstOrDefault();
                userType = memberTypeId == 0 ? UserType.UNKNOWN : (UserType)memberTypeId;

                branding.ParticipantId = participantId;
                branding.UserType = userType;
                if (branding.UserType == UserType.PARTICIPANT)
                {
                    var participantName = (from m in dataProvider.members
                                           from mh in dataProvider.member_hierarchy
                                           from ep in dataProvider.event_participation
                                           where ep.event_participation_id == participantId
                                              && ep.member_hierarchy_id == mh.member_hierarchy_id
                                              && mh.member_id == m.member_id
                                           select string.Concat(m.first_name, m.last_name).Trim()).FirstOrDefault(); 
                    var participantPersonalization = (from e in dataProvider.events
                                                      from ep in dataProvider.event_participation
                                                      from p in dataProvider.personalizations
                                                      where e.event_id == ep.event_id
                                                            && ep.event_participation_id == p.event_participation_id
                                                            && ep.event_participation_id == participantId
                                                            && ep.event_id == e.event_id
                                                      select p).FirstOrDefault();
                    if (participantPersonalization == null)
                    {
                        default_personalization defaultPersonalization;
                        var defaultPartnerPersonalization =
                            dataProvider.default_personalizations.Where(
                                p => p.PartnerId == partnerId && p.EventTypeId == @event.event_type_id && p.ParticipantTypeId == 2);
                        if (defaultPartnerPersonalization.Any())
                        {
                            defaultPersonalization = defaultPartnerPersonalization.First();
                        }
                        else
                        {
                            defaultPersonalization =
                            dataProvider.default_personalizations.Single(
                                p => p.PartnerId == 0 && p.EventTypeId == @event.event_type_id && p.ParticipantTypeId == 2);
                        }
                        participantPersonalization = new personalization
                        {
                            create_date = DateTime.Now,
                            event_participation_id = participantId,
                            body = defaultPersonalization.Body,
                            fundraising_goal = (decimal)defaultPersonalization.Goal,
                            redirect = participantName.CleanupRedirect(),
                            header_title1 = defaultPersonalization.HeaderTitle1,
                            header_title2 = defaultPersonalization.HeaderTitle2,
                            displayGroupMessage = true,
                        };
                        dataProvider.personalizations.Add(participantPersonalization);
                        dataProvider.SaveChanges();
                    }
                    branding.TwitterWidgetId = participantPersonalization == null ? string.Empty : participantPersonalization.twitter_widget_id;
                    branding.FacebookEmbededPost = participantPersonalization == null ? string.Empty : participantPersonalization.facebook_embeded_post;
                    branding.Created = participantPersonalization == null ? DateTime.Now : participantPersonalization.create_date;
                    branding.Goal = participantPersonalization == null ? 0M : participantPersonalization.fundraising_goal.HasValue ? (decimal)participantPersonalization.fundraising_goal : 0M;
                    if (participantPersonalization != null && participantPersonalization.redirect.IsNotEmpty())
                    {
                        if (sponsorPersonalization.redirect.IsEmpty())
                            sponsorPersonalization.redirect = string.Empty;
                        branding.Redirect = string.Concat(sponsorPersonalization.redirect.Trim(), "/", participantPersonalization.redirect.Trim());
                    }
                    else
                    {
                        var url = new UrlHelper(filterContext.RequestContext);
                        branding.Redirect = url.Action("Participant", "Group", new { eventId = branding.Id, participantId });
                    }
                }
                if (userType == UserType.SPONSOR)
                {
                    branding.TwitterWidgetId = sponsorPersonalization.twitter_widget_id;
                    branding.FacebookEmbededPost = sponsorPersonalization.facebook_embeded_post;
                }
                branding.ShowSocialMediaColumn = !string.IsNullOrEmpty(branding.TwitterWidgetId) ||
                                                 !string.IsNullOrEmpty(branding.FacebookEmbededPost);
            }
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                // Pull sales and activity data for this event
                if (branding.UserType == UserType.PARTICIPANT)
                {
                    var @results = dataProvider.es_rpt_supporters_invited(participantId).ToList();
                    if (@results.Any())
                    {
                        branding.TotalNumberOfItemSold = @results.Sum(p => p.nb_subs);
                        branding.TotalAmount = @results.Sum(p => p.amount);
                        branding.TotalAmountGross = 0M;
                        branding.TotalProfit = (decimal)@results.Sum(p => p.profit);
                        branding.TotalDonationAmount = @results.Sum(p => p.donation_amount);
                        branding.SupportersInvited = @results.Where(p => p.is_supp == 1)
                                                             .Select(p => new Models.Views.SupporterInvited
                                                             {
                                                                 FirstName = p.first_name,
                                                                 LastName = p.last_name,
                                                                 EmailAddress = p.email_address,
                                                                 Amount = p.amount,
                                                                 DonationAmount = p.donation_amount,
                                                                 NumberOfSubs = p.nb_subs,
                                                                 Profit = p.profit
                                                             }).ToList();
                    }
                }
                else
                {
                    var @results = dataProvider.es_rpt_campaign_summary_report(eventId).ToList();
                    var eventSummary = @results.Any() ? @results.Single() : null;
                    if (eventSummary != null)
                    {
                        branding.TotalNumberOfEmailSentToGroupMembers = eventSummary.nb_group_members.HasValue ? (int)eventSummary.nb_group_members : 0;
                        branding.TotalNumberOfEmailSentToSupporters = eventSummary.nb_supporters.HasValue ? (int)eventSummary.nb_supporters : 0;
                        branding.TotalNumberOfItemSold = eventSummary.nb_subs.HasValue ? (int)eventSummary.nb_subs : 0;
                        branding.TotalAmount = eventSummary.amount_sold.HasValue ? (decimal)eventSummary.amount_sold : 0M;
                        branding.TotalAmountGross = 0M;
                        branding.TotalProfit = eventSummary.profit.HasValue ? (decimal)eventSummary.profit : 0M;
                        branding.LastActivity = eventSummary.last_activity.HasValue ? (DateTime)eventSummary.last_activity : System.DateTime.MinValue;
                        branding.TotalDonationAmount = eventSummary.donation_amount_sold.HasValue ? (decimal)eventSummary.donation_amount_sold : 0M;
                    }
                }
            }
            filterContext.Controller.ViewBag.Event = branding;
            if (filterContext.Controller.ViewBag.User != null)
            {
                (filterContext.Controller.ViewBag.User as User).UserTypeInfo = userType;
                (filterContext.Controller.ViewBag.User as User).EventParticipationId = participantId;
            }
        }
    }
}