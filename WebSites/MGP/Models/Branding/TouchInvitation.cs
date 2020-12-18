namespace GA.BDC.Web.MGP.Models.Branding
{
    public class TouchInvitation
    {
        public int TouchId { get; set; }
        public int EventParticipationId { get; set; }
        public int TouchInfoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}