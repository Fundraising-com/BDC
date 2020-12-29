using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GA.BDC.Data.DataLayer
{
    public class Personalization
    {
        #region Public Properties
        public int PersonalizationId { get; set; }
        public int EventParticipationID { get; set; }
        public string HeaderTitle1 { get; set; }
        public string HeaderTitle2 { get; set; }
        public string Body { get; set; }
        public decimal FundraisingGoal { get; set; }
        public string SiteBackgroundColor { get; set; }
        public string HeaderBackgroundColor { get; set; }
        public string HeaderTextColor { get; set; }
        public string GroupUrl { get; set; }
        public string ImageUrl { get; set; }
        public byte ImageMotivator { get; set; }
        public string Redirect { get; set; }
        public byte DisplayGroupMessage { get; set; }
        public byte Skip { get; set; }
        public byte RemindLater { get; set; }
        #endregion

        #region Static Methods
        public static List<Personalization> GetPersonalizationRedirectStartsWith(string redirect)
        {
            using (EsubsGlobalV2DataContext dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
            {
                return (from personalization in dc.es_get_personalization_redirect_startswith(redirect)
                        select new Personalization
                        {
                            PersonalizationId = personalization.personalization_id,
                            EventParticipationID = personalization.event_participation_id,
                            HeaderTitle1 = personalization.header_title1,
                            HeaderTitle2 = personalization.header_title2,
                            Body = personalization.body,
                            FundraisingGoal = personalization.fundraising_goal != null ? (decimal)personalization.fundraising_goal : 0m,
                            SiteBackgroundColor = personalization.site_bgcolor,
                            HeaderBackgroundColor = personalization.header_bgcolor,
                            HeaderTextColor = personalization.header_color,
                            GroupUrl = personalization.group_url,
                            ImageUrl = personalization.image_url,
                            ImageMotivator = personalization.image_motivator,
                            Redirect = personalization.redirect,
                            DisplayGroupMessage = personalization.displayGroupMessage != null 
                                                    ? (bool)personalization.displayGroupMessage ? (byte)1 : (byte)0 
                                                    : (byte)0,
                            RemindLater = personalization.remind_later != null
                                            ? (bool)personalization.remind_later ? (byte)1 : (byte)0
                                            : (byte)0
                        }).ToList();
            }
        }
        #endregion
    }
}
