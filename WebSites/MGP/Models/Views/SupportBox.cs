using System.Collections.Generic;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class SupportBox
    {
        public decimal Goal { get; set; }
        public string RedirectURL { get; set; }
        public bool ApplyPartnerPercentOnTotalAmount { get; set; }
        public decimal TotalRaised { get; set; }
        public int TotalNumberOfSupporters { get; set; }
        public List<Comments> DonarComments { get; set; }
        public bool ShowImageMotivator { get; set; }
        public string LearnMoreText { get; set; }
        public int DefaultEventId { get; set; }
    }
}