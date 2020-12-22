using System;
using System.Configuration;
using System.Linq;
using System.Web;
using GA.BDC.Data.MGP.EFRCommon.Models;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Properties;

namespace GA.BDC.Web.MGP.Helpers
{
    public static class PartnerHelper
    {
	    public static Partner CreatePartnerBranding(int partnerId)
	    {
		    var branding = new Partner { Id = partnerId, ShowPopularItems = true, ShowPopularItemsBanner = true, InsertAsLead = true, PrizeProgram = true, };
         using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
         {
            var partnerInformation = dataProvider.es_get_partner_branding(branding.Id).FirstOrDefault();
            if (partnerInformation != null)
            {
               branding.ProductOffer = (ProductOffer)partnerInformation.product_offer_id;
               branding.Program = (Program)partnerInformation.program_id;
               branding.PaymentTo = (PaymentTo)partnerInformation.payment_to;
               branding.CultureCode = partnerInformation.culture_code;
            }
            else
            {
               throw new Exception($"Partner {branding.Id} is not configured correctly.");
            }
         }
         using (var dataProvider = new Data.MGP.EFRCommon.LINQ.DataProviderDataContext(ConfigurationManager.ConnectionStrings["EFRCommon"].ConnectionString))
         {
            var customAttributeValues = dataProvider.es_get_partner_custom_attribute_values(branding.Id).ToList();
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_SHOW_ONLY_PARTNER_LOGO))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_SHOW_ONLY_PARTNER_LOGO).value;
               branding.ShowOnlyPartnerLogo = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PARTNER_PATH))
            {
               var partnerPath = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PARTNER_PATH).value;
               branding.Logo = string.IsNullOrEmpty(partnerPath) ? string.Empty
                                                             : branding.ShowOnlyPartnerLogo
                                                                  ? ImageExist($"{Settings.Default.LogoImagesDirectory}/{partnerPath}/banner.gif")
                                                                    ? $"{Settings.Default.LogoImagesDirectory}/{partnerPath}/banner.gif"
			               : ImageExist($"{Settings.Default.LogoImagesDirectory}/{partnerPath}/logo.gif")
                                                                       ? $"{Settings.Default.LogoImagesDirectory}/{partnerPath}/logo.gif"
				               : $"{Settings.Default.LogoImagesDirectory}/efundraising/logo.gif"
		               : ImageExist($"{Settings.Default.LogoImagesDirectory}/{partnerPath}/logo.gif")
                                                                    ? $"{Settings.Default.LogoImagesDirectory}/{partnerPath}/logo.gif"
			               : $"{Settings.Default.LogoImagesDirectory}/efundraising/logo.gif";
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_MAIN_MENU))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_MAIN_MENU).value;
               branding.HideMainMenu = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_HOWITWORKS))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_HOWITWORKS).value;
               branding.HideHowItWorks = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_CUSTOMIZE_PARTICIPANT_REGISTER_MESSAGE))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_CUSTOMIZE_PARTICIPANT_REGISTER_MESSAGE).value;
               branding.CustomParticipantRegisterMessage = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_CUSTOMIZE_SHOP_NOW_FOR_OUR_CAUSE))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_CUSTOMIZE_SHOP_NOW_FOR_OUR_CAUSE).value;
               branding.ShopNowForOurCauseMessage = value;
            }
            else
            {
               branding.ShopNowForOurCauseMessage = "Shop Now For Our Cause";
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_GROUP_PAGE_SHOW_POPULAR_ITEMS))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_GROUP_PAGE_SHOW_POPULAR_ITEMS).value;
               branding.ShowPopularItems = string.IsNullOrEmpty(value) || bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_GROUP_PAGE_SHOW_POPULAR_ITEMS_BANNER))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_GROUP_PAGE_SHOW_POPULAR_ITEMS_BANNER).value;
               branding.ShowPopularItemsBanner = string.IsNullOrEmpty(value) || bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_CANADIAN_REGISTRATION_LINK))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_CANADIAN_REGISTRATION_LINK).value;
               branding.HideCanadianRegistrationLink = string.IsNullOrEmpty(value) || bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_GROUP_PAGE_HIDE_ALL_REPORTS))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_GROUP_PAGE_HIDE_ALL_REPORTS).value;
               branding.HideAllReports = string.IsNullOrEmpty(value) || bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_PROMOTION_PAGE))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_PROMOTION_PAGE).value;
               branding.HidePromotionPage = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_CSS_OVERRIDE_FILE))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_CSS_OVERRIDE_FILE).value;
               branding.OverrideCSSFile = value;
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_DEMO_PAGE_IMAGE))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_DEMO_PAGE_IMAGE).value;
               branding.DemoPageImage = value;
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_BROWSE_PARTICIPANTS))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_BROWSE_PARTICIPANTS).value;
               branding.HideBrowseParticipants = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_GROUP_PAGE_HIDE_FAQ))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_GROUP_PAGE_HIDE_FAQ).value;
               branding.HideFaq = string.IsNullOrEmpty(value) || bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_INSERT_AS_LEAD))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_INSERT_AS_LEAD).value;
               branding.InsertAsLead = string.IsNullOrEmpty(value) || bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PRIZE_PROGRAM))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PRIZE_PROGRAM).value;
               branding.PrizeProgram = string.IsNullOrEmpty(value) || bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_PROFIT))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_PROFIT).value;
               branding.HideProfit = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_GOAL))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_GOAL).value;
               branding.HideGoal = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_ESUBS_CAN_NOT_CREATE_EVENT))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_ESUBS_CAN_NOT_CREATE_EVENT).value;
               branding.CanNotCreateEvent = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_CHECK_REPORT))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_CHECK_REPORT).value;
               branding.HideCheckReport = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_SHOW_RECEIPT_LINK))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_SHOW_RECEIPT_LINK).value;
               branding.ShowReceiptLink = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_REDIRECT_TO_LANDING_PAGE))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_REDIRECT_TO_LANDING_PAGE).value;
               branding.RedirectToLandingPage = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_ESUBS_URL))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_ESUBS_URL).value;
               branding.ESubsUrl = value;
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PAP_A_AID))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PAP_A_AID).value;
               branding.PAPAffiliateId = value;
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_JOIN_BUTTON_BY_EVENTS))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_HIDE_JOIN_BUTTON_BY_EVENTS).value;
               branding.EventsToHideJOINButton = value;
            }
            if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_SHOW_TOTAL_AMOUNT_IN_THERMOMETER))
            {
               var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_SHOW_TOTAL_AMOUNT_IN_THERMOMETER).value;
               branding.ShowTotalAmountInThermometer = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }

            var profits = dataProvider.es_get_partner_profit(partnerId).ToList();
            if (profits.Any())
            {
               var profit = profits.First();
               branding.ProfitPercentage = profit.Percentage;
               branding.ProfitGroupId = profit.ProfitGroupId;
               branding.ProfitDescription = profit.Description;
               branding.Disclaimer = profit.Disclaimer;
               branding.Name = profit.Name;
               branding.PartnerTypeId = profit.PartnerType;
            }
         }
         using (var dataProvider = new DataProvider())
         {
            dataProvider.Configuration.LazyLoadingEnabled = false;
            dataProvider.Configuration.AutoDetectChangesEnabled = false;
            var groupDisplay =
               dataProvider.partner_type_culture.FirstOrDefault(
                  p => p.partner_type_id == branding.PartnerTypeId && p.culture_code == "en-US")?.partner_type_name ??
               "Event";
            branding.GroupDisplay = groupDisplay;
         }
		    return branding;
	    }

	    private static bool ImageExist(string imagePath)
	    {
		    try
		    {
			    System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(imagePath.Trim()));
			    return true;
		    }
		    catch
		    {
			    return false;
		    }
	    }
    }
}