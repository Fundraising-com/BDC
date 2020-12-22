using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Data.MGP.esubs_global_v2.Models;

namespace GA.BDC.Web.MGP.Helpers.Routes.Attributes
{
    public class RenderPublicEventBranding : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session == null) throw new Exception("Session is null");

            // Sanitize data
            var rawEventId = filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_ID) != null 
                             ? filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_ID).AttemptedValue 
                             : "0";
            var rawParticipantId = filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID) != null
                                   ? filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID).AttemptedValue
                                   : "0";
            if (rawEventId.Contains("#"))
                rawEventId = rawEventId.Substring(0, rawEventId.IndexOf("#"));
            if (rawParticipantId.Contains("#"))
                rawParticipantId = rawParticipantId.Substring(0, rawParticipantId.IndexOf("#"));
            if (rawEventId.Contains("'\""))
                rawEventId = rawEventId.Substring(0, rawEventId.IndexOf("'\""));
            if (rawParticipantId.Contains("'\""))
                rawParticipantId = rawParticipantId.Substring(0, rawParticipantId.IndexOf("'\""));
            if (rawEventId.Contains("?"))
                rawEventId = rawEventId.Substring(0, rawEventId.IndexOf("?"));
            if (rawParticipantId.Contains("?"))
                rawParticipantId = rawParticipantId.Substring(0, rawParticipantId.IndexOf("?"));

            var partnerId = Convert.ToInt32(filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTNER_ID]);
            var eventId =  Convert.ToInt32(rawEventId);
            var participantId = Convert.ToInt32(rawParticipantId);
            var branding = new PublicEvent();

            #region Perform Data Integrity Check
            if (participantId > 0)
            {
                using (var data =new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
                {
                    var members = data.es_get_member_by_event_participation_id(participantId).ToList();
                    if (members.Any() && members.First().user_id == null)
                    {
                        var member = members.First();
                        data.es_validate_login(member.email_address, member.password, member.partner_id);
                    }
                }
            }
            #endregion

            using (var dataProvider = new DataProvider())
            {
                var @event = dataProvider.events.Find(eventId);

                if (@event == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "EventNotFound" } });
                    return;
                }

                // If inactive, get latest event id
                if (!@event.active)
                {
                    var groupId = dataProvider.event_group.Single(p => p.event_id == @event.event_id).group_id;
                    var @activeEvent = (from e in dataProvider.events
                                        from eg in dataProvider.event_group
                                        from ep in dataProvider.event_participation
                                        where eg.group_id == groupId
                                           && e.event_id == eg.event_id
                                           && e.event_id == ep.event_id
                                           && ep.participation_channel_id == 3
                                           && e.active
                                        orderby e.start_date descending
                                        select e).FirstOrDefault();
                    if (@activeEvent != null)
                        @event = @activeEvent;
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "CampaignClosed"
                        }));
                        return;
                    }
                }
                branding.Id = @event.event_id;
                branding.Name = @event.event_name;
                branding.LaunchDate = @event.start_date;
                branding.Status = @event.active;
                branding.EventType = (EventTypeInfo)Enum.Parse(typeof(EventTypeInfo), @event.event_type_id.ToString(CultureInfo.InvariantCulture));
                personalization personalization;
                if (participantId > 0)
                {
                    personalization = (from ep in dataProvider.event_participation
                                       from p in dataProvider.personalizations
                                       where ep.event_participation_id == p.event_participation_id
                                             && ep.event_participation_id == participantId
                                       select p).SingleOrDefault();
                    if (personalization == null)
                    {
                        string firstName, lastName;
                        using (var data = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
                        {
                            var member = data.es_get_member_by_event_participation_id(participantId).First();
                            firstName = member.first_name;
                            lastName = member.last_name;
                            if (member.user_id > 0)
                            {
                                var @result = dataProvider.users.Single(u => u.user_id == member.user_id);
                                firstName = @result.first_name;
                                lastName = @result.first_name;
                            }
                        }
                        
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
                        personalization = new personalization
                        {
                            create_date = DateTime.Now,
                            event_participation_id = participantId,
                            body = defaultPersonalization.Body,
                            fundraising_goal = (decimal)defaultPersonalization.Goal,
                            redirect = string.Concat(firstName, lastName).CleanupRedirect(),
                            header_title1 = defaultPersonalization.HeaderTitle1,
                            header_title2 = defaultPersonalization.HeaderTitle2,
                            displayGroupMessage = true,
                        };
                        dataProvider.personalizations.Add(personalization);
                        dataProvider.SaveChanges();
                    }
                    var urlHelper = new UrlHelper(filterContext.RequestContext);
                    branding.Redirect = urlHelper.Action("Participant", "Group", new { eventId = branding.Id, participantId });
                }
                else
                {
                    personalization = (from ep in dataProvider.event_participation
                                       from p in dataProvider.personalizations
                                       where ep.event_participation_id == p.event_participation_id
                                             && ep.participation_channel_id == 3
                                             && ep.event_id == eventId
                                       select p).Single();
                    branding.Redirect = personalization.redirect;
                }

                branding.Created = personalization.create_date;
                branding.Message = string.IsNullOrEmpty(personalization.body) ? string.Empty : personalization.body;
                branding.Goal = personalization.fundraising_goal.HasValue ? (decimal)personalization.fundraising_goal : 0M;
                branding.IsGASavingsCard = (from n in dataProvider.event_gasavingcard
                                            where n.event_id == @event.event_id
                                            select n).Any();
                branding.TwitterWidgetId = personalization.twitter_widget_id;
                branding.FacebookEmbededPost = personalization.facebook_embeded_post;
                branding.Created = personalization.create_date;
                branding.ShowSocialMediaColumn = !string.IsNullOrEmpty(branding.TwitterWidgetId) ||
                                                 !string.IsNullOrEmpty(branding.FacebookEmbededPost);
                var sponsor = (from e in dataProvider.events
                               from ep in dataProvider.event_participation
                               from mh in dataProvider.member_hierarchy
                               from m in dataProvider.members
                               from u in dataProvider.users
                               where e.event_id == ep.event_id
                                     && ep.member_hierarchy_id == mh.member_hierarchy_id
                                     && mh.member_id == m.member_id
                                     && m.user_id == u.user_id
                                     && e.event_id == eventId
                                     && ep.participation_channel_id == 3
                               select u).FirstOrDefault();
                if (sponsor != null)
                {
                    branding.SponsorName = string.Concat(sponsor.first_name, " ", sponsor.last_name);
                }
            }
            using (var dataProvider =new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                // Pull sales and activity data for this event
                if (participantId > 0)
                {
                    var @results = dataProvider.es_rpt_supporters_invited(participantId).ToList();
                    if (@results.Any())
                    {
                        branding.TotalProfit = (decimal)@results.Sum(p => p.profit);
                        branding.TotalAmount = @results.Sum(p => p.amount);
                        branding.TotalAmountGross = 0M;
                        branding.TotalNumberOfItemSold = @results.Sum(p => p.nb_subs);
                        branding.TotalDonationAmount = @results.Sum(p => p.donation_amount);
                    }
                }
                else
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
            }
            filterContext.Controller.ViewBag.PublicEvent = branding;
        }
    }
}