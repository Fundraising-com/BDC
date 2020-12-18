namespace GA.BDC.Web.MGP.Models.Branding
{
    public class Lead
    {
        public int EventParticipationID { get; set; }
        public int GroupID { get; set; }
        public int SponsorID { get; set; }
        public int PartnerID { get; set; }
        public int LeadID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string SubdivisionCode { get; set; }
        public string CountryCode { get; set; }
        public string ZipCode { get; set; }
        public string DayPhone { get; set; }
        public string Email { get; set; }
        public int ExpectedMembership { get; set; }
        public string GroupUrl { get; set; }
        private int ConsultantID { get; set; }
    }
}