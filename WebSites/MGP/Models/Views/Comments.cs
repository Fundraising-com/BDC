namespace GA.BDC.Web.MGP.Models.Views
{
    public class Comments
    {
        public int EventParticipationID { get; set; }
        public int EventID { get; set; }
        public int MemberHierarchyID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}