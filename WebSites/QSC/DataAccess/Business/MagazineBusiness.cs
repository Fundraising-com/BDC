namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class MagazineBusiness : ProductBusiness
	{
		public MagazineBusiness(Message messageManager) : base(messageManager) { }

		public MagazineBusiness(bool asMessageManager) : base(asMessageManager) { }

		protected override bool ValidateRequiredFields(string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription)
		{
			bool isValid = base.ValidateRequiredFields (productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription);

			isValid &= IsValid_RequiredField(remitCode, "Remit Code");
			isValid &= IsValid_RequiredField(categoryID, "Category");
			isValid &= IsValid_RequiredField(publisherID, "Publisher");
			isValid &= IsValid_RequiredField(fulfillmentHouseID, "Fulfillment House");
			isValid &= IsValid_RequiredField(numberOfIssues, "Number of issues published per year");

			return isValid;
		}

		protected override bool ValidateFieldLength(string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription)
		{
			bool isValid = base.ValidateFieldLength (productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription);

			isValid &= IsValid_FieldLength(GSTRegistrationNumber, "GST Registration Number", 0, 20);
			isValid &= IsValid_FieldLength(HSTRegistrationNumber, "HST Registration Number", 0, 20);
			isValid &= IsValid_FieldLength(PSTRegistrationNumber, "PST Registration Number", 0, 20);

			return isValid;
		}

		protected override bool ValidateCustom(string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription)
		{
			bool isValid = base.ValidateCustom (productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription);

			if(GSTRegistrationNumber.Trim().Length != 0 && HSTRegistrationNumber.Trim().Length == 0)
			{
				messageManager.Add(Message.ERRMSG_GSTWITHOUTHST_0);
				isValid = false;
			} 
			else if(GSTRegistrationNumber.Trim().Length == 0 && HSTRegistrationNumber.Trim().Length != 0) 
			{
				messageManager.Add(Message.ERRMSG_HSTWITHOUTGST_0);
				isValid = false;
			}

			return isValid;
		}
	}
}