using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.Fundraising.Core.Templates.Emails
{
   public partial class KitRequestedEmailTemplate
   {
      public string Subject { get; set; }

      public string PDFUrl { get; set; }
        public string SiteURL { get; set; }
        public string BannerImage { get; set; }
        public string Phone { get; set; }

        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public Lead Lead { get; set; }
      public string MGPWizardStep2 { get; set; }
      public KitRequestedEmailTemplate(string subject, Lead lead)
      {
         Subject = subject;
         Lead = lead;

            if (lead.Address.Country.Code == "US")
            {
                Phone = lead.Partner.Attributes["phone_number"];
                 BannerImage = "https://www.fundraising.com/Content/external/kitrequestimages/Email_confirmation_guide_fr-upd_04.jpg";
                SiteURL = "https://www.fundraising.com";
                PDFUrl = "https://www.fundraising.com/Content/external/partners/pdf/partnerkit/686.pdf";
                Text1 = "We are excited to help you achieve your goals and make fundraising easy and fun for you!";
                Text2 = "Our Fundraising Experts are there for you if you need more information. Call us with your questions or to request samples.";
            }
            else
            {
                Phone = "1-866-877-2777";
                BannerImage = "https://www.fundraising.com/Content/external/kitrequestimages/canadakitbanner_02.jpg";
                SiteURL = "https://www.fundraising.com/canada";
                PDFUrl = "https://www.fundraising.com/Content/external/partners/pdf/partnerkit/kitCan.pdf";
                Text1 = "IT’S READY! DON’T WASTE TIME";
                Text2 = "We’re excited to start helping you achieve your fundraising goals. Buying on Fundraising.com/CANADA is super easy but if you need some guidance don’t be shy, give us a call and our Fundraising Consultants will be happy to help you raise those big bucks.";


            }





        }
   }
}