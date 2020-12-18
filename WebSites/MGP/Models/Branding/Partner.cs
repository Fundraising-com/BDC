using GA.BDC.Web.MGP.Helpers.Extensions;
namespace GA.BDC.Web.MGP.Models.Branding
{
   public class Partner
   {
      /// <summary>
      /// Partner ID
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// PartnerTypeId
      /// </summary>
      public int PartnerTypeId { get; set; }
      /// <summary>
      /// Culture Code
      /// </summary>
      public string CultureCode { get; set; }
      /// <summary>
      /// Profit Percentage
      /// </summary>
      public double ProfitPercentage { get; set; }
      /// <summary>
      /// Profit Group Id
      /// </summary>
      public int? ProfitGroupId { get; set; }
      /// <summary>
      /// Profit Description
      /// </summary>
      public string ProfitDescription { get; set; }
      /// <summary>
      /// Custom Disclamimer
      /// </summary>
      public string Disclaimer { get; set; }
      /// <summary>
      /// Show only the Partner Logo on the Top
      /// </summary>
      public bool ShowOnlyPartnerLogo { get; set; }
      /// <summary>
      /// Image URL for the Logo
      /// </summary>
      public string Logo { get; set; }
      /// <summary>
      /// Hide the Main Menu at the Top of the Page
      /// </summary>
      public bool HideMainMenu { get; set; }
      /// <summary>
      /// Show the Popular Items at the Group Pages
      /// </summary>
      public bool ShowPopularItems { get; set; }
      /// <summary>
      /// Show the Popular Items Banner
      /// </summary>
      public bool ShowPopularItemsBanner { get; set; }
      /// <summary>
      /// Hides the Canadian Registration link
      /// </summary>
      public bool HideCanadianRegistrationLink { get; set; }
      /// <summary>
      /// Prize program
      /// </summary>
      public bool PrizeProgram { get; set; }
      /// <summary>
      /// Product Type Offered
      /// </summary>
      public ProductOffer ProductOffer { get; set; }
      /// <summary>
      /// Name
      /// </summary>
      public string Name { get; set; }
      /// <summary>
      /// Insert leads for this partner
      /// </summary>
      public bool InsertAsLead { get; set; }
      /// <summary>
      /// Program the the Partner belongs to
      /// </summary>
      public Program Program { get; set; }
      /// <summary>
      /// Payment to Partner or Group
      /// </summary>
      public PaymentTo PaymentTo { get; set; }
      /// <summary>
      /// Hide the Sponsor Check Report
      /// </summary>
      public bool HideCheckReport { get; set; }
      /// <summary>
      /// Group Display
      /// </summary>
      public string GroupDisplay { get; set; }
      /// <summary>
      /// Redirect To Landing Page
      /// </summary>
      public bool RedirectToLandingPage { get; set; }
      /// <summary>
      /// ESubs Url
      /// </summary>
      public string ESubsUrl { get; set; }
      /// <summary>
      /// Check if partner can redirect to a landing page
      /// </summary>
      public bool DoRedirectToLandingPage
      {
         get
         {
            if (RedirectToLandingPage && ESubsUrl.IsNotEmpty())
               return true;
            else
               return false;
         }
      }
      /// <summary>
      /// Show Receipt Link
      /// </summary>
      public bool ShowReceiptLink { get; set; }
      /// <summary>
      /// PAP Affiliate Id
      /// </summary>
      public string PAPAffiliateId { get; set; }
      /// <summary>
      /// Hides all profit related information
      /// </summary>
      public bool HideProfit { get; set; }
      /// <summary>
      /// a customized message for the goal
      /// </summary>
      public bool HideGoal { get; set; }
      /// <summary>
      /// User can't create an event by himself
      /// </summary>
      public bool CanNotCreateEvent { get; set; }
      /// <summary>
      /// Hides all the how it works links
      /// </summary>
      public bool HideHowItWorks { get; set; }
      /// <summary>
      /// Message for the Shop Now for our cause text
      /// </summary>
      public string ShopNowForOurCauseMessage { get; set; }
      /// <summary>
      /// Hides all the reports in the Campaign Manager
      /// </summary>
      public bool HideAllReports { get; set; }
      /// <summary>
      /// Hides the FAQ link
      /// </summary>
      public bool HideFaq { get; set; }
      /// <summary>
      /// Hides the Promote your Page menu
      /// </summary>
      public bool HidePromotionPage { get; set; }
      /// <summary>
      /// Hide the Browse Participants link
      /// </summary>
      public bool HideBrowseParticipants { get; set; }
      /// <summary>
      /// Custom message for the participant register message
      /// </summary>
      public bool CustomParticipantRegisterMessage { get; set; }
      /// <summary>
      /// Some events can hide the JOIN button
      /// </summary>
      public string EventsToHideJOINButton { get; set; }
      // <summary>
      /// Display Total Amount in Amount Raised Thermometer instead of Profit
      /// </summary>
      public bool ShowTotalAmountInThermometer { get; set; }
      /// <summary>
      /// Overrides some CSS rules
      /// </summary>
      public string OverrideCSSFile { get; set; }
      /// <summary>
      /// Overrides the Demo Page Image
      /// </summary>
      public string DemoPageImage { get; set; }
   }

   public enum ProductOffer
   {
      All = 1,
      MagazineOnly = 2,
      MagazineResto = 3,
      RestoMagazine = 4,
      DonationOnly = 5,
      MagazineAndMore = 6,
      BoxTops = 7
   }

   public enum Program
   {
      Undefined = 0,
      ESubs = 1,
      Mvp = 2,
      Alumni = 3,
      Traditional = 4,
      Schools = 5
   }

   public enum PaymentTo : int
   {
      Group = 0,
      Partner = 1
   }
}