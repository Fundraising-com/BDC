using System;
using System.Collections.Generic;

namespace GA.BDC.Web.MGP.Models.Branding
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EventTypeInfo EventType { get; set; }
        public DateTime Created { get; set; }
        public string Message { get; set; }
        public string SponsorName { get; set; }
        public bool IsGASavingsCard { get; set; }
        public string Redirect { get; set; }
        public int TotalNumberOfEmailSentToGroupMembers { get; set; }
        public int TotalNumberOfEmailSentToSupporters { get; set; }
        public int TotalNumberOfItemSold { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalAmountGross { get; set; }
        public Decimal TotalProfit { get; set; }
        public DateTime LaunchDate { get; set; }
        public bool Status { get; set; }
        public Decimal Goal { get; set; }
        public DateTime LastActivity { get; set; }
        public Decimal TotalDonationAmount { get; set; }
        public bool ShowSocialMediaColumn { get; set; }
        public string TwitterWidgetId { get; set; }
        public string FacebookEmbededPost { get; set; }
        public int ParticipantId { get; set; }
        public List<Views.SupporterInvited> SupportersInvited { get; set; }
        public UserType UserType { get; set; }
    }

    public enum EventTypeInfo
    {
        GROUP_FUNDRAISER_WITH_SUBPAGE = 1,
        GROUP_FUNDRAISER_WITHOUT_SUBPAGE = 2,
        INDIVIDUAL_FUNDRAISER = 3
    }
}