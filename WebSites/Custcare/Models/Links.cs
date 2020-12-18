using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Models
{
    public class Links
    {
        public string Redirect { get; set; }
        public int MemberHierarchyId { get; set; }
        public int EventId { get; set; }
        public string MGPDomain { get; set; }
        public string SponsorMGPRedirect
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Redirect))
                {
                    return String.Concat(MGPDomain, "/Group/Index?eventId=" + EventId);
                }
                else
                {
                    return String.Concat(MGPDomain, "/", Redirect);
                }
            }
        }
        public string WelcomePage { get; set; }
        public string CampaignManagerHomePage { get; set; }
        public string NewComments { get; set; }
        public string Comments { get; set; }
    }
}