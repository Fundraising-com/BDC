using System;
using QSPFulfillment.DataAccess.Common;

namespace QSPFulfillment.DataAccess.Business
{
	/// <summary>
	/// Summary description for MagazineContractBusiness.
	/// </summary>
	public class FieldSuppliesContractBusiness : ProductContractSingleBusiness
	{
		public FieldSuppliesContractBusiness(Message messageManager) : base(messageManager) { }
		
		public FieldSuppliesContractBusiness(bool asMessageManager) : base(asMessageManager) { }

		protected override bool ValidateRequiredFields(int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode, int placementLevel)
		{
			bool isValid = base.ValidateRequiredFields (status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, QSPPriceHST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode, placementLevel);

			isValid &= IsValid_RequiredField(fsApplicabilityId, "Applicability");
			isValid &= IsValid_RequiredField(fsDistributionLevelID, "Distribution Level");

			if((fsContentCatalogCode != String.Empty || taxRegionID != 0 || fsProvinceCode != String.Empty) && !fsIsBrochure) 
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, "Is Brochure"));
				isValid = false;
			}

			if((fsIsBrochure || taxRegionID != 0 || fsProvinceCode != String.Empty) && fsContentCatalogCode == String.Empty) 
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, "Content Catalog"));
				isValid = false;
			}

			if(fsProvinceCode != String.Empty && taxRegionID == 0) 
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, "Tax Region"));
				isValid = false;
			}

			return isValid;
		}
	}
}
