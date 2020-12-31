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
	public class ProductContractSingleBusiness : ProductContractBusiness
	{
		public ProductContractSingleBusiness(Message messageManager) : base(messageManager) { }

		public ProductContractSingleBusiness(bool asMessageManager) : base(asMessageManager) { }

		public override void SelectAll(DataTable table)
		{
			try
			{
				DataAccess.SelectAllSingle(table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public override void SelectAll(DataTable table, CatalogType catalogType, CatalogSectionType catalogSectionType, int productInstance, string productCode, string remitCode, string productName, int year, string season, int productStatus, ProductType productType, string oracleCode, int numberOfIssues, string catalogCode, int publisherID, int fulfillmentHouseID, int programSectionID, bool includeBlank)
		{
			try
			{
				DataAccess.SelectAllSingle(table, productInstance, CleanString(productCode), CleanString(remitCode), CleanString(productName), year, season, productStatus, Convert.ToInt32(productType), CleanString(oracleCode), numberOfIssues, CleanString(catalogCode), publisherID, fulfillmentHouseID, programSectionID, includeBlank);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public override void SelectOne(DataTable table, ProductContractID productContractID)
		{
			try
			{
				DataAccess.SelectOneSingle(table, productContractID.MagPriceInstanceGST);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public override bool Insert(int productInstance, string productCode, int year, string season, int catalogSectionID, int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode, string userID,
			int contractFormReceived, string magazineCoverFilename, string catalogAdFilename,
			int catalogPageNumber,
			int placementLevel,  string contractComments,
            string printerComments, string qspcaListingCopyText, double dBasePriceSansPostage, double dPostageRemitRate, double dPostageAmount, double BaseRemitRate, double dAdlHandlingFee, string listAgentCode, string QSPAgencyCode)
		{
			bool isSuccess = false;
			
			try
			{
				if (Validate(status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, QSPPriceHST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode, placementLevel)) 
				{
					NbRowAffected = 0;
                    NbRowAffected = DataAccess.InsertSingle(productInstance, productCode, year, season, catalogSectionID, status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode, userID, qspcaListingCopyText, dAdlHandlingFee);
					if (NbRowAffected != 0)
					{
						isSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
						isSuccess = false;
					}
				} 
				else 
				{
					isSuccess = false;
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			if ( !isSuccess )
			{
				throw new ValidationException(messageManager);
			}
			return isSuccess;
		}

		public override bool Update(ProductContractID productContractID, int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode,
			int contractFormReceived, string magazineCoverFilename, string catalogAdFilename,
			int catalogPageNumber, 
			int placementLevel,  string contractComments,
            string printerComments, string qspcaListingCopyText, double dBasePriceSansPostage, double dPostageRemitRate, double dPostageAmount, double BaseRemitRate, double dAdlHandlingFee, string listAgentCode, string QSPAgencyCode)
		{
			bool isSuccess = false;
			
			try
			{
				if (Validate(status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, QSPPriceHST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode, placementLevel)) 
				{
					NbRowAffected = 0;
                    NbRowAffected = DataAccess.UpdateSingle(productContractID.MagPriceInstanceGST, status, effectiveDate, endDate, dateSubmitted, remitRate, conversionRate, newsStandPrice, listingLevelID, listingCopyText, adInCatalog, adPageSizeID, adPaymentCurrency, adCost, comment, effortKeyRequired, effortKey, numberOfIssues, basePrice, QSPPriceGST, internetApproval, ABCCode, QSPPremiumID, oracleCode, fsApplicabilityId, fsDistributionLevelID, fsExtraLimitRate, fsIsBrochure, fsCatalogProductCode, fsContentCatalogCode, fsProgramID, taxRegionID, premiumIndicator, premiumCode, premiumCopy, fsProvinceCode, qspcaListingCopyText, dAdlHandlingFee);
					if (NbRowAffected != 0)
					{
						isSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
						isSuccess = false;
					}
				} 
				else 
				{
					isSuccess = false;
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			if ( !isSuccess )
			{
				throw new ValidationException(messageManager);
			}
			return isSuccess;
		}

		public override bool Import(ProductContractID productContractID, int productInstance, ProductType productType, int newCatalogSectionID, string importForSeason, string userID, string newProductCode)
		{
			bool isSuccess = false;
			
			try
			{
				NbRowAffected = 0;
				NbRowAffected = DataAccess.ImportSingle(productContractID.MagPriceInstanceGST, productInstance, newCatalogSectionID, importForSeason, userID, newProductCode);
				if (NbRowAffected != 0)
				{
					isSuccess = true; 
				}
				else
				{
					messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
					messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
					isSuccess = false;
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			if ( !isSuccess )
			{
				throw new ValidationException(messageManager);
			}
			return isSuccess;
		}

		protected override ProductContractID GetProductContractID(DataRow row)
		{
			return ProductContractIDFactory.Instance.GetProductContractID(Convert.ToInt32(row["MagPrice_Instance"]), ProductContractID.EmptyValue, ProductContractTypeFactory.Instance.GetProductContractType((ProductType) Convert.ToInt32(row["ProductTypeInstance"])));
		}

		public override bool ValidateDelete(ProductContractID productContractID)
		{
			bool isValid = false;

			if(DataAccess.SelectCustomerOrderDetailCountSingle(productContractID.MagPriceInstanceGST) == 0) 
			{
				isValid = true;
			} 
			else 
			{
				messageManager.Add(Message.ERRMSG_CANNOT_DELETE_CONTRACT_0);
				messageManager.PrepareErrorMessage();
				throw new ExceptionFulf(messageManager);
			}

			return isValid;
		}

		public override bool Delete(ProductContractID productContractID)
		{
			bool isSuccess = false;
			
			try
			{
				if(ValidateDelete(productContractID))
				{

					NbRowAffected = 0;
					NbRowAffected = DataAccess.DeleteSingle(productContractID.MagPriceInstanceGST);
					if (NbRowAffected != 0)
					{
						isSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
						isSuccess = false;
					}
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			if ( !isSuccess )
			{
				throw new ValidationException(messageManager);
			}
			return isSuccess;
		}
	}
}