using System;
using QSPFulfillment.DataAccess.Common;

namespace QSPFulfillment.DataAccess.Business
{
	/// <summary>
	/// Summary description for MagazineContractBusiness.
	/// </summary>
	public class MagazineContractBusiness : ProductContractGST_HSTBusiness
	{
		public MagazineContractBusiness(Message messageManager) : base(messageManager) { }
		
		public MagazineContractBusiness(bool asMessageManager) : base(asMessageManager) { }

		protected override bool ValidateRequiredFields(int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode, int placementLevel)
		{
			bool isValid = base.ValidateRequiredFields (status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, QSPPriceHST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode, placementLevel);

			isValid &= IsValid_RequiredField(remitRate, "Remit Rate");
			isValid &= IsValid_RequiredField(conversionRate, "Conversion Rate");
			isValid &= IsValid_RequiredField(newsStandPrice, "News Stand Price per Issue");
			isValid &= IsValid_RequiredField(numberOfIssues, "Number of Issues");
			isValid &= IsValid_RequiredField(basePrice, "Base Price");
			isValid &= IsValid_RequiredField(placementLevel, "Placement Level");

			return isValid;
		}

		protected override bool ValidateFieldLength(int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode)
		{
			bool isValid = base.ValidateFieldLength (status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, QSPPriceHST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode);

			isValid &= IsValid_FieldLength(listingCopyText, "Listing Copy Text", 0, 500);
			isValid &= IsValid_FieldLength(effortKey, "Effort Key", 0, 40);
			isValid &= IsValid_FieldLength(ABCCode, "ABC Code", 0, 20);

			return isValid;
		}
	}
}
