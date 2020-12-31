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
	public class ProductContractAllBusiness : ProductContractBusiness
	{
		public ProductContractAllBusiness(Message messageManager) : base(messageManager) { }

		public ProductContractAllBusiness(bool asMessageManager) : base(asMessageManager) { }

		public override void SelectAll(DataTable table)
		{
			try
			{
				DataAccess.SelectAllTypes(table);
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
				DataAccess.SelectAllTypes(table, productInstance, CleanString(productCode), CleanString(productName), year, season, productStatus, GetProductTypeList(productType, catalogType, catalogSectionType), CleanString(catalogCode), programSectionID, includeBlank);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		private string GetProductTypeList(ProductType productType, CatalogType catalogType, CatalogSectionType catalogSectionType) 
		{
			ProductTypeBusiness productTypeBusiness = null;
			DataTable table = null;
			string productTypeList = String.Empty;

			if(productType != ProductType.None) 
			{
				productTypeList = ((int) productType).ToString();
			} 
			else 
			{
				productTypeBusiness = new ProductTypeBusiness(messageManager);
				table = new DataTable("ProductType");

				if(catalogType != CatalogType.None && catalogSectionType != CatalogSectionType.None) 
				{
					productTypeBusiness.SelectAllProductTypes(table, catalogType, catalogSectionType);
				} 
				else 
				{
					productTypeBusiness.SelectAllProductTypes(table);
				}

				foreach(DataRow row in table.Rows) 
				{
					if(productTypeList != String.Empty) 
					{
						productTypeList += ", ";
					}

					productTypeList += row["Instance"].ToString();
				}
			}

			return productTypeList;
		}

		public override void SelectOne(DataTable table, ProductContractID productContractID) { }

		public override bool Insert(int productInstance, string productCode, int year, string season, int catalogSectionID, int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode, string userID,
			int contractFormReceived, string magazineCoverFilename, string catalogAdFilename,
			int catalogPageNumber, 
			int placementLevel,  string contractComments,
            string printerComments, string qspcaListingCopyText, double dBasePriceSansPostage, double dPostageRemitRate, double dPostageAmount, double BaseRemitRate, double dAdlHandlingFee, string listAgentCode, string QSPAgencyCode)
		{
			return false;
		}

		public override bool Update(ProductContractID productContractID, int status, DateTime effectiveDate, DateTime endDate, DateTime dateSubmitted, double remitRate, double conversionRate, double newsStandPrice, int listingLevelID, string listingCopyText, int adInCatalog, int adPageSizeID, int adPaymentCurrency, double adCost, string comment, int effortKeyRequired, string effortKey, int numberOfIssues, double basePrice, double QSPPriceGST, double QSPPriceHST, int internetApproval, string ABCCode, int QSPPremiumID, string oracleCode, int fsApplicabilityId, int fsDistributionLevelID, int fsExtraLimitRate, bool fsIsBrochure, string fsCatalogProductCode, string fsContentCatalogCode, int fsProgramID, int taxRegionID, string premiumIndicator, string premiumCode, string premiumCopy, string fsProvinceCode,
			int contractFormReceived, string magazineCoverFilename, string catalogAdFilename,
			int catalogPageNumber, 
			int placementLevel, string contractComments,
            string printerComments, string qspcaListingCopyText, double dBasePriceSansPostage, double dPostageRemitRate, double dPostageAmount, double dBaseRemitRate, double dAdlHandlingFee, string listAgentCode, string QSPAgencyCode)
		{
			return false;
		}

		public override bool Import(ProductContractID productContractID, int productInstance, ProductType productType, int newCatalogSectionID, string importForSeason, string userID, string newProductCode)
		{
			bool isSuccess = false;
			
			try
			{
				NbRowAffected = 0;

				if(ProductContractTypeFactory.Instance.GetProductContractType(productType) == ProductContractType.GST_HST) 
				{
					NbRowAffected = DataAccess.ImportGST_HST(productContractID.MagPriceInstanceGST, productContractID.MagPriceInstanceHST, productInstance, newCatalogSectionID, importForSeason, userID, newProductCode);
				} 
				else 
				{
					NbRowAffected = DataAccess.ImportSingle(productContractID.MagPriceInstanceGST, productInstance, newCatalogSectionID, importForSeason, userID, newProductCode);
				}

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
			return ProductContractIDFactory.Instance.GetProductContractID(Convert.ToInt32(row["MagPrice_Instance"]), Convert.ToInt32(row["MagPrice_InstanceHST"]), ProductContractTypeFactory.Instance.GetProductContractType((ProductType) Convert.ToInt32(row["ProductTypeInstance"])));
		}

		public override bool ValidateDelete(ProductContractID productContractID)
		{
			return false;
		}

		public override bool Delete(ProductContractID productContractID)
		{
			return false;
		}
	}
}