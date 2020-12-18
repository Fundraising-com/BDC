using System;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class CampaignReportMember
    {
        public string MemberName { get; set; }
        public string SupporterName { get; set; }
        public int EmailsSent { get; set; }
        public int ItemsSold { get; set; }
        public decimal AmountSold { get; set; }
        public decimal Profit { get; set; }
        public decimal Donation { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ProductDescription { get; set; }
        public string SaleDate { get; set; }
    }

    public class CampaignDonationReportMember
    {
        public string MemberName { get; set; }
        public string SupporterName { get; set; }
        public int EmailsSent { get; set; }
        public decimal Donations { get; set; }
    }

    public class MyCampaignReport
    {
        public string SupporterName { get; set; }
        public int ItemsSold { get; set; }
        public decimal AmountSold { get; set; }
        public decimal Profit { get; set; }
        public decimal Donation { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ProductDescription { get; set; }
        public string SaleDate { get; set; }
    }

    public class MyCampaignDonationReport
    {
        public string SupporterName { get; set; }
        public decimal Profit { get; set; }
        public decimal Donations { get; set; }
        public string DonationDate { get; set; }
    }
}