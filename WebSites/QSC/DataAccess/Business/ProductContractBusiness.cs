namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.ProductContractData;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public abstract class ProductContractBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		private dataAccessRef dataAccess = new dataAccessRef();

		public ProductContractBusiness(Message messageManager) : base(messageManager) { }

		public ProductContractBusiness(bool asMessageManager) : base(asMessageManager) { }

		protected dataAccessRef DataAccess 
		{
			get 
			{
				return dataAccess;
			}
		}

		public abstract void SelectAll(DataTable table);

		public abstract void SelectAll(DataTable table, CatalogType catalogType, CatalogSectionType catalogSectionType, int productInstance, string productCode, string remitCode, string productName, int year, string season, int productStatus, ProductType productType, string oracleCode, int numberOfIssues, string catalogCode, int publisherID, int fulfillmentHouseID, int programSectionID, bool includeBlank);

		public abstract void SelectOne(DataTable table, ProductContractID productContractID);

		public bool Insert(int productInstance, string productCode, int year, string season, 
			int catalogSectionID, int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, 
			double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, 
			int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, 
			string effortKey, int numberOfIssues, double basePrice, double QSPPrice, int internetApproval, string ABCCode, 
			int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate,
			bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID,
			string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode, string userID,
			int contractFormReceived, string magazineCoverFilename, string catalogAdFilename,
			int catalogPageNumber, 
			int placementLevel,  string contractComments,
			string printerComments, string qspcaListingCopyText,
            double dBasePriceSansPostage, double dPostageRemitRate, double dPostageAmount, double dBaseRemitRate, double dAdlHandlingFee, string listAgentCode, string QSPAgencyCode)
		{
			return Insert(productInstance, productCode, year, season, catalogSectionID, status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPrice, 0, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode, userID,
				contractFormReceived,  magazineCoverFilename,  catalogAdFilename,
				catalogPageNumber,  
				placementLevel,  contractComments,
                printerComments, qspcaListingCopyText, dBasePriceSansPostage, dPostageRemitRate, dPostageAmount, dBaseRemitRate, dAdlHandlingFee, listAgentCode, QSPAgencyCode);
		}

		public abstract bool Insert(int productInstance, string productCode, int year, string season, 
			int catalogSectionID, int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, 
			double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, 
			int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, 
			string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval,
			string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate,
			bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, 
			string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode, string userID,
			int contractFormReceived, string magazineCoverFilename, string catalogAdFilename,			int catalogPageNumber,
			int placementLevel, string contractComments,			string printerComments, string qspcaListingCopyText,
            double dBasePriceSansPostage, double dPostageRemitRate, double dPostageAmount, double BaseRemitRate, double dAdlHandlingFee, string listAgentCode, string QSPAgencyCode);

		public bool Update(ProductContractID productContractID, int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPrice, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode,	int contractFormReceived, string magazineCoverFilename, string catalogAdFilename,
			int catalogPageNumber,
			int placementLevel, string contractComments,
            string printerComments, string qspcaListingCopyText, double dBasePriceSansPostage, double dPostageRemitRate, double dPostageAmount, double BaseRemitRate, double dAdlHandlingFee, string listAgentCode, string QSPAgencyCode)

		{
				return Update(productContractID, status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPrice, 0, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode,contractFormReceived,  magazineCoverFilename,  catalogAdFilename,
					catalogPageNumber,  
					placementLevel,  contractComments,
                    printerComments, qspcaListingCopyText, dBasePriceSansPostage, dPostageRemitRate, dPostageAmount, BaseRemitRate, dAdlHandlingFee, listAgentCode, QSPAgencyCode);
		}

		public abstract bool Update(ProductContractID productContractID, int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode,
			int contractFormReceived, string magazineCoverFilename, string catalogAdFilename,
			int catalogPageNumber, 
			int placementLevel, string contractComments,
            string printerComments, string qspcaListingCopyText, double dBasePriceSansPostage, double dPostageRemitRate, double dPostageAmount, double BaseRemitRate, double dAdlHandlingFee, string listAgentCode, string QSPAgencyCode);

		public abstract bool Import(ProductContractID productContractID, int productInstance, ProductType productType, int newCatalogSectionID, string importForSeason, string userID, string newProductCode);

		public virtual void ImportList(int newCatalogSectionID, CatalogType catalogType, CatalogSectionType catalogSectionType, int productInstance, string productCode, string remitCode, string productName, int year, string season, int productStatus, ProductType productType, int numberOfIssues, string oracleCode, string catalogCode, int publisherID, int fulfillmentHouseID, string importForSeason, string userID) 
		{
			DataTable productContractTable = new DataTable("ProductContract");
			ConnectionProvider connectionProvider = new ConnectionProvider();
			
			try 
			{
				SelectAll(productContractTable, catalogType, catalogSectionType, productInstance, productCode, remitCode, productName, year, season, productStatus, productType, oracleCode, numberOfIssues, catalogCode, publisherID, fulfillmentHouseID, 0, true);

				this.MainConnectionProvider = connectionProvider;
				connectionProvider.OpenConnection();
				connectionProvider.BeginTransaction("ImportProductContractList");

				foreach(DataRow row in productContractTable.Rows) 
				{
					Import(GetProductContractID(row), Convert.ToInt32(row["Product_Instance"]), (ProductType) Convert.ToInt32(row["ProductTypeInstance"]), newCatalogSectionID, importForSeason, userID, Convert.ToString(row["Product_Code"]));
				}
				connectionProvider.CommitTransaction();
				connectionProvider.CloseConnection(false);

				this.MainConnectionProvider = null;
			}
			catch(Exception ex)
			{	
				if (connectionProvider.DBConnection.State != ConnectionState.Closed) 
				{
					connectionProvider.RollbackTransaction("ImportProductContractList");
					connectionProvider.CloseConnection(false);
				}

				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		protected abstract ProductContractID GetProductContractID(DataRow row);

		public abstract bool ValidateDelete(ProductContractID productContractID);

		public abstract bool Delete(ProductContractID productContractID);

		public virtual void DeleteByCatalogSectionID(int catalogSectionID) 
		{
			try
			{
				DataAccess.DeleteByCatalogSectionID(catalogSectionID);
			}
			catch(Exception EX)
			{
				ManageError(EX);
			}
		}

		public virtual void DeleteByProductInstance(int productInstance) 
		{
			try
			{
				DataAccess.DeleteByProductInstance(productInstance);
			}
			catch(Exception EX)
			{
				ManageError(EX);
			}
		}

		protected virtual bool Validate(int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode, int placementLevel) 
		{
			bool isValid = true;

			isValid &= ValidateRequiredFields(status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, QSPPriceHST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode, placementLevel);
			isValid &= ValidateFieldLength(status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, QSPPriceHST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode);
			isValid &= ValidateCustom(status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, QSPPriceHST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode);

			if(!isValid) 
			{
				messageManager.PrepareErrorMessage();
			}

			return isValid;
		}

		protected virtual bool ValidateRequiredFields(int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode, int placementLevel) 
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(effectiveDate, "Effective Date");
			isValid &= IsValid_RequiredField(endDate, "End Date");
			isValid &= IsValid_RequiredField(dateSubmitted, "Date Submitted");

			return isValid;
		}

		protected virtual bool ValidateFieldLength(int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode) 
		{
			bool isValid = true;

			isValid &= IsValid_FieldLength(listingCopyText, "Listing Copy Text", 0, 500);
			isValid &= IsValid_FieldLength(comment, "Comment", 0, 200);
			isValid &= IsValid_FieldLength(ABCCode, "ABC Code", 0, 20);
			isValid &= IsValid_FieldLength(oracleCode, "Oracle Code", 0, 50);

			return isValid;
		}

		protected virtual bool ValidateCustom(int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode) 
		{
			bool isValid = true;

			if(effectiveDate > endDate) 
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_CANNOT_BE_HIGHER_2, new string[] {"End Date", "Effective Date"}));
				isValid = false;
			}

			return isValid;
		}
		
		protected bool IsValid_FieldLength(object FieldToValidate, string FieldForErrorMessage, short minLen, short maxLen)
		{
			bool isValid;

			short i = (short)(FieldToValidate.ToString().Trim().Length);
			if ( (i < minLen) || (i > maxLen) )
			{
				//
				// Mark the field as invalid
				//
				if(minLen != maxLen) 
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_LENGTH_RANGE_VAR_3, new String[] {FieldForErrorMessage, minLen.ToString(), maxLen.ToString()}));
				}
				else 
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2, new String[] {FieldForErrorMessage, maxLen.ToString()}));
				}
				messageManager.ValidationExceptionType = ExceptionType.MaxLength;
				isValid = false;
			}
			else 
			{
				isValid = true;
			}

			return isValid;
		}

		protected bool IsValid_RequiredField(object FieldToValidate, string FieldForErrorMessage)
		{
			bool isValid;

			if (FieldToValidate.ToString() == string.Empty)
			{
				//
				// Mark the field as invalid
				//
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, FieldForErrorMessage));
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
				isValid = false;
			} 
			else 
			{
				isValid = true;
			}

			return isValid;
		}

		protected bool IsValid_RequiredField(int FieldToValidate, string FieldForErrorMessage)
		{
			bool isValid;

			if (FieldToValidate == 0)
			{
				//
				// Mark the field as invalid
				//
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, FieldForErrorMessage));
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
				isValid = false;
			} 
			else 
			{
				isValid = true;
			}

			return isValid;
		}

		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}

	}
}