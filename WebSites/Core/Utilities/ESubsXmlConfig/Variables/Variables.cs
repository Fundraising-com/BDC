using System;

namespace GA.BDC.Core.Utilities.ESubsXmlConfig.Variables
{

	/// <summary>
	/// 
	/// </summary>
	internal class TrackableVars {
			
		public const string __RequestAttribute = "[Name]";
		public const string __XPath_GetGroupType = "//TrackableObjects[@Name='[NameTrk]']/GroupTrack[@GroupObjectName='[Name]']/TrackingButton";
		public const string __NodeName = "[NameTrk]";
		public const string __XPATH_TrackableContainer = "//TrackableObjects[@Name='[NameTrk]']";
	}

	/// <summary>
	/// 
	/// </summary>
	internal class InstructionsVars {
		
		public const string __RequestTypeAttribute = "[Type]";
		public const string __RequestFileTypeAttribute = "[FileType]";
		public const string __RequestDisplayTextAttribute = "[DisplayText]";
		public const string __RequestCultureNameAttribute = "[CultureName]";
		public const string __XPath_Instructions = "//ImportInstructions[@CultureName='[CultureName]'/Instructions[@Type='[Type]' AND @FileType='[FileType]']";
	}

	/// <summary>
	/// 
	/// </summary>
	public class MailProvidersVars {

		public const string __XPath_MailProvider = "//iMailConfig/MailProvider[@Name='[Provider]']";
		public const string __Provider = "[Provider]";
	}

	internal class eSubsPages {

		public const string __XPATH_CampaignNeededPage = "//eSubsPages/CampaignNeededPage/webPage[@webPageID='[WebPageID]']";
		public const string __WebPageID = "[WebPageID]";
		
		public const string __XPATH_CampaignNeededPageByName = "//eSubsPages/CampaignNeededPage/webPage[@webPageName='[WebPageName]']";
		public const string __WebPageName = "[WebPageName]";

		public const string __TemplateID = "//eSubsPages/Templates/Template[@partnerID='[partnerID]']";
		public const string __PartnerID = "[partnerID]";

	}
}
