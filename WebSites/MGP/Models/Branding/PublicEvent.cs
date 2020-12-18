using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.MGP.Models.Branding
{
    public class PublicEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime LaunchDate { get; set; }
        public bool Status { get; set; }
        public EventTypeInfo EventType { get; set; }
        public DateTime Created { get; set; }
        public decimal Goal { get; set; }
        public string Redirect { get; set; }
        public bool IsGASavingsCard { get; set; }
        public bool ShowSocialMediaColumn { get; set; }
        public string TwitterWidgetId { get; set; }
        public string FacebookEmbededPost { get; set; }
        public string SponsorName { get; set; }
        public decimal TotalNumberOfItemSold { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalAmountGross { get; set; }
        public Decimal TotalProfit { get; set; }
        public Decimal TotalDonationAmount { get; set; }
        public DateTime LastActivity { get; set; }
        public string CoverImage { get; set; }
    }
}