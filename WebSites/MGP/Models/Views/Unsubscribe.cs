using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class Unsubscribe
    {        
        public int MemberId { get; set; }
        public bool MemberSubscribed { get; set; }
        public CampaignInfo[] Campaigns { get; set; }
        public CampaignInfo[] SelectedCampaigns { get; set; }
    }

    public struct CampaignInfo
    {
        public int EventId { get; set; }
        public string CampaignName { get; set; }
        public int MemberHierarchyId { get; set; }
        public bool MemberHierarchySubscribed { get; set; }
    }
}