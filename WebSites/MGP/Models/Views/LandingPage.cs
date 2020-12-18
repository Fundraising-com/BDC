namespace GA.BDC.Web.MGP.Models.Views
{
   public class LandingPage
   {
      public int PartnerId { get; set; }
      public int GroupId { get; set; }
      public string GroupDisplay { get; set; }
      public string Description { get; set; }
      public string SideBarImageUrl { get; set; }
      public string DonateImageUrl { get; set; }
      public string RaiseFundsImageUrl { get; set; }
      public decimal Goal { get; set; }
      public bool ShowCreateFundraising { get; set; }
      public bool ShowFeaturedGroups { get; set; }
      public bool ShowFindGroups { get; set; }
      public bool ShowTopParticipants { get; set; }
      public bool ShowImageMotivator { get; set; }
      public string LearnMoreText { get; set; }
      public bool ApplyPartnerPercentOnTotalAmount { get; set; }
      public double AmountRaised { get; set; }
      public string ImageTopUrl { get; set; }
   }
}