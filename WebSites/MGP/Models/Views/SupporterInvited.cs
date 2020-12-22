using System;
namespace GA.BDC.Web.MGP.Models.Views
{
    public class SupporterInvited
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int NumberOfSubs { get; set; }
        public Decimal Amount { get; set; }
        public Decimal DonationAmount { get; set; }
        public double Profit { get; set; }
    }
}