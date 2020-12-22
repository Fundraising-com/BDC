using System;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Helpers.Extensions;
using efundraisingProd = GA.BDC.Data.MGP.EfundraisingProd.Models;
using eFundweb = GA.BDC.Data.MGP.eFundweb.Models;
using eSubs = GA.BDC.Data.MGP.esubs_global_v2.Models;

namespace GA.BDC.Web.MGP.Helpers.Routes.Attributes
{
    /// <summary>
    /// Finds and loads all lead information
    /// </summary>
    public class RenderLeadBranding : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.Session == null) throw new Exception("Session is null");
            if (filterContext.HttpContext.Session[Constants.SESSION_KEY_LEAD_ID] == null)
            {
                return;
            }
            else
            {
                var leadId = Convert.ToInt32(filterContext.HttpContext.Session[Constants.SESSION_KEY_LEAD_ID]);
                var lead = new Lead();
                using (var efundraisingProdDataProvider = new efundraisingProd.DataProvider())
                {
                    var promotionId = efundraisingProdDataProvider.leads.Single(l => l.lead_id == leadId).promotion_id;
                    using (var efundWebDataProvder = new eFundweb.DataProvider())
                    {
                        var promo = (from pr in efundWebDataProvder.promotions
                                     from p in efundWebDataProvder.partners
                                     where pr.partner_id == p.partner_id
                                        && pr.promotion_id == promotionId
                                     select new
                                     {
                                         p.partner_id,
                                         p.has_collection_site
                                     }).FirstOrDefault();

                        lead = (from l in efundraisingProdDataProvider.leads
                                where l.lead_id == leadId
                                select new Lead
                                {
                                    LeadID = l.lead_id,
                                    PartnerID = !promo.has_collection_site
                                                ? 0
                                                : promo.partner_id,
                                    Name = string.Concat(l.first_name ?? string.Empty, " ", l.last_name ?? string.Empty).Trim(),
                                    GroupName = l.organization == null && l.clients.Any()
                                                ? l.clients.Select(x => x.organization).FirstOrDefault()
                                                : l.organization,
                                    Address = l.street_address,
                                    City = l.city,
                                    SubdivisionCode = l.state_code,
                                    CountryCode = l.country_code,
                                    ZipCode = l.zip_code,
                                    DayPhone = l.day_phone,
                                    Email = l.email,
                                    ExpectedMembership = l.participant_count.HasValue
                                                            ? l.participant_count.Value
                                                            : 0
                                }).First();                        
                    }
                }
                using (var esubsDataProvider = new eSubs.DataProvider())
                {
                    var @result = (from ep in esubsDataProvider.event_participation
                                   from mh in esubsDataProvider.member_hierarchy
                                   from g in esubsDataProvider.groups
                                   where ep.member_hierarchy_id == mh.member_hierarchy_id
                                      && mh.member_hierarchy_id == g.sponsor_id
                                      && g.lead_id == leadId
                                   select new { ep.event_participation_id, g.group_id, g.sponsor_id }).FirstOrDefault();
                    if (@result != null)
                    {
                        lead.EventParticipationID = @result.event_participation_id;
                        lead.GroupID = @result.group_id;
                        lead.SponsorID = @result.sponsor_id;
                    }
                }
                filterContext.Controller.ViewBag.Lead = lead;
            }
        }
    }
}