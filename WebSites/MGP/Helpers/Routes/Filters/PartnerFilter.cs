using System;
using System.Configuration;
using System.Web.Mvc;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using efundraisingProd = GA.BDC.Data.MGP.EfundraisingProd.Models;
using eFundweb = GA.BDC.Data.MGP.eFundweb.Models;
using eFREcommerce = GA.BDC.Data.MGP.EFREcommerce.Models;
using System.Linq;

namespace GA.BDC.Web.MGP.Helpers.Routes.Filters
{
    /// <summary>
    /// Handles the Partner ID in the application and saves the Partner ID in the session so it can be consumed later on
    /// </summary>
    public class PartnerFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var partnerId = int.MinValue;
            var partnerGuid = Guid.Empty;

            // Check each parameter in correct order of importance:
            // Touch ID, Participant ID, Event ID, Partner ID, GUID
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID) != null)
            {
                int touchId;
                if (int.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID).AttemptedValue, out touchId))
                {
                    if (touchId > 0)
                    {
                        partnerId = GetPartnerIdByTouchId(touchId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
                else if (int.TryParse(EnvironmentFilter.DecryptInteger(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID).AttemptedValue).ToString(), out touchId))
                {
                    if (touchId > 0)
                    {
                        partnerId = GetPartnerIdByTouchId(touchId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
            }
            else if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID_2) != null)
            {
                int touchId;
                if (int.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID_2).AttemptedValue, out touchId))
                {
                    if (touchId > 0)
                    {
                        partnerId = GetPartnerIdByTouchId(touchId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
                else if (int.TryParse(EnvironmentFilter.DecryptInteger(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_TOUCH_ID_2).AttemptedValue).ToString(), out touchId))
                {
                    if (touchId > 0)
                    {
                        partnerId = GetPartnerIdByTouchId(touchId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
            }
            else if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID) != null)
            {
                int epId;
                if (int.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_PARTICIPANT_ID).AttemptedValue, out epId))
                {
                    if (epId > 0)
                    {
                        partnerId = GetPartnerIdByEventParticipationId(epId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
            }
            else if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_ID) != null)
            {
                int eventId;
                if (int.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_ID).AttemptedValue, out eventId))
                {
                    if (eventId > 0)
                    {
                        partnerId = GetPartnerIdByEventId(eventId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
                else if (int.TryParse(EnvironmentFilter.DecryptInteger(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_EVENT_ID).AttemptedValue).ToString(), out eventId))
                {
                    if (eventId > 0)
                    {
                        partnerId = GetPartnerIdByEventId(eventId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
            }
            else if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_LEADID) != null)
            {
                int leadId;
                if (int.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_LEADID).AttemptedValue, out leadId))
                {
                    if (leadId > 0)
                    {
                        partnerId = GetPartnerIdByLeadId(leadId);
                        if (partnerId > 0)
                        {
                            if(ValidatePartnerId(partnerId))
                                SaveSpecificPartner(filterContext, partnerId);
                            else
                                SaveDefaultPartner(filterContext);
                        }
                    }
                }
            }
            else if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_ORDER_ID) != null)
            {
                int orderId;
                if (int.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_ORDER_ID).AttemptedValue, out orderId))
                {
                    if (orderId > 0)
                    {
                        partnerId = GetPartnerIdByDonationOrderId(orderId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
                else if (int.TryParse(EnvironmentFilter.DecryptInteger(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_ORDER_ID).AttemptedValue).ToString(), out orderId))
                {
                    if (orderId > 0)
                    {
                        partnerId = GetPartnerIdByDonationOrderId(orderId);
                        SaveSpecificPartner(filterContext, partnerId);
                    }
                }
            }
            else if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_P) == null && filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_GID) == null)
            {
                //No PartnerID received so we save the Default one
                SaveDefaultPartner(filterContext);
            }
            
            if (partnerId < 0)
            {                
                if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_P) != null)
                {
                    int.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_P).AttemptedValue, out partnerId);
                }
                if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_GID) != null)
                {
                    Guid.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_GID).AttemptedValue, out partnerGuid);
                }
                if (partnerId == int.Parse(ConfigurationManager.AppSettings["DefaultPartnerID"]) ||
                    partnerGuid == new Guid(ConfigurationManager.AppSettings["DefaultPartnerGUID"]))
                {
                    SaveDefaultPartner(filterContext);
                }
                else
                {
                    using (var dataProvider = new Data.MGP.EFRCommon.Models.DataProvider())
                    {
                        partnerId = (from n in dataProvider.partners
                                     where (partnerId > 0 && n.partner_id == partnerId) || (partnerGuid != Guid.Empty && n.guid == partnerGuid)
                                     select n.partner_id).SingleOrDefault();
                        if (partnerId == 0)
                        {
                            SaveDefaultPartner(filterContext);
                        }
                        else
                        {
                            SaveSpecificPartner(filterContext, partnerId);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves in Session the default Partner ID ONLY if no other Partner ID already exists in Session 
        /// </summary>
        /// <param name="filterContext">Filter Context</param>
        private void SaveDefaultPartner(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
                filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTNER_ID] =
                    filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTNER_ID] ??
                    int.Parse(ConfigurationManager.AppSettings["DefaultPartnerID"]);
        }
        /// <summary>
        /// Saves in Session the specific Partner ID
        /// </summary>
        /// <param name="filterContext">Filter Context</param>
        private void SaveSpecificPartner(ActionExecutingContext filterContext, int partnerId)
        {
            if (filterContext.HttpContext.Session != null)
                filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTNER_ID] = partnerId;
        }

        private bool ValidatePartnerId(int partnerId)
        {
            using (var dataProvider = new Data.MGP.EFRCommon.Models.DataProvider())
            {
                var @partner = (from p in dataProvider.partners
                               where p.partner_id == partnerId
                              select p).SingleOrDefault();
                return @partner != null;
            }
        }
        private int GetPartnerIdByTouchId(int touchId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @pId = (from t in dataProvider.touches
                             from ep in dataProvider.event_participation
                             from eg in dataProvider.event_group
                             from g in dataProvider.groups
                             where t.touch_id == touchId
                                && t.event_participation_id == ep.event_participation_id
                                && ep.event_id == eg.event_id
                                && eg.group_id == g.group_id
                            select g.partner_id).FirstOrDefault();
                return @pId;
            }
        }
        private int GetPartnerIdByEventParticipationId(int epId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @pId = (from ep in dataProvider.event_participation
                            from eg in dataProvider.event_group
                            from g in dataProvider.groups
                            where ep.event_participation_id == epId
                               && ep.event_id == eg.event_id
                               && eg.group_id == g.group_id
                            select g.partner_id).FirstOrDefault();
                return @pId;
            }
        }
        private int GetPartnerIdByEventId(int eventId)
        {
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var partners = dataProvider.es_get_partner_by_event_id(eventId).ToList();
                return partners.Any() ? partners.First().partner_id : 0;
            }
        }
        private int GetPartnerIdByLeadId(int leadId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @grp = (from g in dataProvider.groups
                            where g.lead_id == leadId
                            select g).FirstOrDefault();
                if (@grp == null)
                {
                    using (var efundraisingProdDataProvider = new efundraisingProd.DataProvider())
                    {
                        var lead = (from l in efundraisingProdDataProvider.leads
                                    where l.lead_id == leadId
                                    select new { l.lead_id, l.promotion_id }).FirstOrDefault();
                        if (lead == null)
                            return -1;
                        else
                        {
                            using (var efundWebDataProvder = new eFundweb.DataProvider())
                            {
                                var promo = (from pr in efundWebDataProvder.promotions
                                             from p in efundWebDataProvder.partners
                                             where pr.partner_id == p.partner_id
                                                && pr.promotion_id == lead.promotion_id
                                             select new
                                             {
                                                 p.partner_id,
                                                 p.has_collection_site
                                             }).SingleOrDefault();
                                return !promo.has_collection_site ? 0 : promo.partner_id;
                            }
                        }
                    }
                }
                else
                    return @grp.partner_id;
            }
        }
        private int GetPartnerIdByDonationOrderId(int orderId)
        {
            int? epId = 0;
            using (var dataProvider = new eFREcommerce.DataProvider())
            {
                epId = dataProvider.orders.Where(o => o.order_id == orderId).Select(o => o.ext_participation_id).SingleOrDefault();
            }
            if (epId == null)
                return 0;
            using (var dataProvider = new DataProvider())
            {
                var @pId = (from ep in dataProvider.event_participation
                            from eg in dataProvider.event_group
                            from g in dataProvider.groups
                            where ep.event_participation_id == (int)epId
                               && ep.event_id == eg.event_id
                               && eg.group_id == g.group_id
                            select g.partner_id).FirstOrDefault();
                return @pId;
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}