using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.MarketingMgt.Control
{
	public delegate void SelectProductContractEventHandler(object sender, SelectProductContractClickedArgs e);
	/// <summary>
	/// Summary description for ProductContractMaintenanceControl.
	/// </summary>
	public class ProductContractMaintenanceControl : MarketingMgtControl
	{
		public event SelectProductContractEventHandler ProductContractSaved;
		public event System.EventHandler ProductContractCancelled;

		private DataTable seasonTable;

		public virtual ProductType ProductType
		{
			get 
			{
				throw new NotImplementedException("ProductType");
			}
			set 
			{
				throw new NotImplementedException("ProductType");
			}
		}

		public ProductContractID ProductContractID
		{
			get 
			{
				ProductContractType productContractType;

				if(ViewState["ProductContractID"] == null) 
				{
					productContractType = ProductContractTypeFactory.Instance.GetProductContractType(ProductType);
					ViewState["ProductContractID"] = ProductContractIDFactory.Instance.GetProductContractID(ProductContractID.EmptyValue, ProductContractID.EmptyValue, productContractType);
				}

				return (ProductContractID) ViewState["ProductContractID"];
			}
			set 
			{
				this.ViewState["ProductContractID"] = value;
			}
		}

		public int ProductInstance
		{
			get 
			{
				int productInstance = 0;

				if(ViewState["ProductInstance"] != null) 
				{
					productInstance = Convert.ToInt32(ViewState["ProductInstance"]);
				}

				return productInstance;
			}
			set 
			{
				ViewState["ProductInstance"] = value;
			}
		}

		#region Fields

		protected virtual string ProductCodeDisplay 
		{
			get 
			{
				throw new NotImplementedException("ProductCodeDisplay");
			}
			set 
			{
				throw new NotImplementedException("ProductCodeDisplay");
			}
		}

		protected virtual string ProductNameDisplay 
		{
			get 
			{
				throw new NotImplementedException("ProductNameDisplay");
			}
			set 
			{
				throw new NotImplementedException("ProductNameDisplay");
			}
		}

		protected virtual int YearDisplay 
		{
			get 
			{
				throw new NotImplementedException("YearDisplay");
			}
			set 
			{
				throw new NotImplementedException("YearDisplay");
			}
		}

		protected virtual string SeasonDisplay 
		{
			get 
			{
				throw new NotImplementedException("SeasonDisplay");
			}
			set 
			{
				throw new NotImplementedException("SeasonDisplay");
			}
		}

		protected virtual int Status
		{
			get 
			{
				throw new NotImplementedException("Status");
			}
			set 
			{
				throw new NotImplementedException("Status");
			}
		}

		protected virtual DateTime EffectiveDate
		{
			get 
			{
				throw new NotImplementedException("EffectiveDate");
			}
			set 
			{
				throw new NotImplementedException("EffectiveDate");
			}
		}

		protected virtual DateTime EndDate
		{
			get 
			{
				throw new NotImplementedException("EndDate");
			}
			set 
			{
				throw new NotImplementedException("EndDate");
			}
		}

		protected virtual DateTime DateSubmitted
		{
			get 
			{
				throw new NotImplementedException("DateSubmitted");
			}
			set 
			{
				throw new NotImplementedException("DateSubmitted");
			}
		}

		protected virtual double RemitRate
		{
			get 
			{
				throw new NotImplementedException("RemitRate");
			}
			set 
			{
				throw new NotImplementedException("RemitRate");
			}
		}

		protected virtual double ConversionRate
		{
			get 
			{
				throw new NotImplementedException("ConversionRate");
			}
			set 
			{
				throw new NotImplementedException("ConversionRate");
			}
		}

		protected virtual double NewsStandPrice
		{
			get 
			{
				throw new NotImplementedException("NewsStandPrice");
			}
			set 
			{
				throw new NotImplementedException("NewsStandPrice");
			}
		}

		protected virtual int ListingLevelID
		{
			get 
			{
				throw new NotImplementedException("ListingLevelID");
			}
			set 
			{
				throw new NotImplementedException("ListingLevelID");
			}
		}

		protected virtual string ListingCopyText
		{
			get 
			{
				throw new NotImplementedException("ListingCopyText");
			}
			set
			{
				throw new NotImplementedException("ListingCopyText");
			}
		}

		protected virtual int AdInCatalog
		{
			get 
			{
				throw new NotImplementedException("AdInCatalog");
			}
			set 
			{
				throw new NotImplementedException("AdInCatalog");
			}
		}

		protected virtual int AdPageSizeID
		{
			get 
			{
				throw new NotImplementedException("AdPageSizeID");
			}
			set 
			{
				throw new NotImplementedException("AdPageSizeID");
			}
		}

		protected virtual int AdPaymentCurrency
		{
			get 
			{
				throw new NotImplementedException("AdPaymentCurrency");
			}
			set 
			{
				throw new NotImplementedException("AdPaymentCurrency");
			}
		}

		protected virtual double AdCost
		{
			get 
			{
				throw new NotImplementedException("AdCost");
			}
			set 
			{
				throw new NotImplementedException("AdCost");
			}
		}

		protected virtual string Comment
		{
			get
			{
				throw new NotImplementedException("Comment");
			}
			set 
			{
				throw new NotImplementedException("Comment");
			}
		}

		protected virtual int EffortKeyRequired
		{
			get
			{
				throw new NotImplementedException("EffortKeyRequired");
			}
			set 
			{
				throw new NotImplementedException("EffortKeyRequired");
			}
		}

		protected virtual string EffortKey
		{
			get 
			{
				throw new NotImplementedException("EffortKey");
			}
			set 
			{
				throw new NotImplementedException("EffortKey");
			}
		}

		protected virtual int NumberOfIssues
		{
			get 
			{
				throw new NotImplementedException("NumberOfIssues");
			}
			set 
			{
				throw new NotImplementedException("NumberOfIssues");
			}
		}

		protected virtual double BasePrice
		{
			get 
			{
				throw new NotImplementedException("BasePrice");
			}
			set 
			{
				throw new NotImplementedException("BasePrice");
			}
		}

		protected virtual double QSPPriceGST 
		{
			get 
			{
				throw new NotImplementedException("QSPPriceGST");
			}
			set 
			{
				throw new NotImplementedException("QSPPriceGST");
			}
		}

		protected virtual double QSPPriceHST 
		{
			get 
			{
				throw new NotImplementedException("QSPPriceHST");
			}
			set 
			{
				throw new NotImplementedException("QSPPriceHST");
			}
		}

		protected virtual int InternetApproval
		{
			get 
			{
				throw new NotImplementedException("InternetApproval");
			}
			set 
			{
				throw new NotImplementedException("InternetApproval");
			}
		}

		protected virtual string ABCCode
		{
			get 
			{
				throw new NotImplementedException("ABCCode");
			}
			set 
			{
				throw new NotImplementedException("ABCCode");
			}
		}

		protected virtual int QSPPremiumID
		{
			get 
			{
				throw new NotImplementedException("QSPPremiumID");
			}
			set 
			{
				throw new NotImplementedException("QSPPremiumID");
			}
		}

		protected virtual string OracleCode 
		{
			get 
			{
				throw new NotImplementedException("OracleCode");
			}
			set 
			{
				throw new NotImplementedException("OracleCode");
			}
		}

        protected virtual string VendorProductCode
        {
            get
            {
                return String.Empty;
            }
            set{}
        }

		protected virtual int FSApplicabilityId 
		{
			get 
			{
				throw new NotImplementedException("FSApplicabilityId");
			}
			set 
			{
				throw new NotImplementedException("FSApplicabilityId");
			}
		}

		protected virtual int FSDistributionLevelID 
		{
			get 
			{
				throw new NotImplementedException("FSDistributionLevelID");
			}
			set 
			{
				throw new NotImplementedException("FSDistributionLevelID");
			}
		}

		protected virtual int FSExtraLimitRate 
		{
			get 
			{
				throw new NotImplementedException("FSExtraLimitRate");
			}
			set 
			{
				throw new NotImplementedException("FSExtraLimitRate");
			}
		}

		protected virtual bool FSIsBrochure 
		{
			get 
			{
				throw new NotImplementedException("FSIsBrochure");
			}
			set 
			{
				throw new NotImplementedException("FSIsBrochure");
			}
		}

		protected virtual string FSCatalogProductCode 
		{
			get 
			{
				throw new NotImplementedException("FSCatalogProductCode");
			}
			set 
			{
				throw new NotImplementedException("FSCatalogProductCode");
			}
		}

		protected virtual string FSContentCatalogCode 
		{
			get 
			{
				throw new NotImplementedException("FSContentCatalogCode");
			}
			set 
			{
				throw new NotImplementedException("FSContentCatalogCode");
			}
		}

		protected virtual int FSProgramID 
		{
			get 
			{
				throw new NotImplementedException("FSProgramID");
			}
			set 
			{
				throw new NotImplementedException("FSProgramID");
			}
		}

		protected virtual int TaxRegionID 
		{
			get 
			{
				throw new NotImplementedException("TaxRegionID");
			}
			set 
			{
				throw new NotImplementedException("TaxRegionID");
			}
		}

		protected virtual string PremiumIndicator
		{
			get 
			{
				throw new NotImplementedException("PremiumIndicator");
			}
			set 
			{
				throw new NotImplementedException("PremiumIndicator");
			}
		}

		protected virtual string PremiumCode 
		{
			get 
			{
				throw new NotImplementedException("PremiumCode");
			}
			set 
			{
				throw new NotImplementedException("PremiumCode");
			}
		}

		protected virtual string PremiumCopy 
		{
			get 
			{
				throw new NotImplementedException("PremiumCopy");
			}
			set 
			{
				throw new NotImplementedException("PremiumCopy");
			}
		}

        protected virtual string ListAgentCode
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }

        protected virtual string QSPAgencyCode
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }


		protected virtual string FSProvinceCode 
		{
			get 
			{
				throw new NotImplementedException("FSProvinceCode");
			}
			set 
			{
				throw new NotImplementedException("FSProvinceCode");
			}
		}
		protected virtual int ContractFormReceived 
		{
			get 
			{
				return 0;
			}
			set 
			{
			}
		}
		protected virtual string MagazineCoverFilename 
		{
			get 
			{
				return String.Empty;
			}
			set 
			{
			}
		}
		protected virtual string CatalogAdFilename 
		{
			get 
			{
				return String.Empty;
			}
			set 
			{
			}
		}
		protected virtual int CatalogPageNumber 
		{
			get 
			{
				return 0;
			}
			set 
			{
			}
		} 
		protected virtual int PlacementLevel 
		{
			get 
			{
				return 0;
			}
			set 
			{
			}
		} 
		
		protected virtual string ContractComments 
		{
			get 
			{
				return String.Empty;
			}
			set 
			{
			}
		}
		protected virtual string PrinterComments 
		{
			get 
			{
				return String.Empty;
			}
			set 
			{
			}
		}
		protected virtual string QspcaListingCopyText 
		{
			get 
			{
				return String.Empty;
			}
			set 
			{
			}
		}
		protected virtual double BasePriceSansPostage 
		{
			get 
			{
				return 0;
			}
			set 
			{
			}
		} 
		protected virtual double BaseRemitRate 
		{
			get 
			{
				return 0;
			}
			set 
			{
			}
		} 
		protected virtual double PostageRemitRate 
		{
			get 
			{
				return 0;
			}
			set 
			{
			}
		} 
		protected virtual double PostageAmount 
		{
			get 
			{
				return 0;
			}
			set 
			{
			}
		} 
		protected virtual string Currency 
		{
			get 
			{
				return String.Empty;
			}
			set 
			{
			}
		}
        protected virtual double AddlHandlingFee
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }  
		#endregion

		protected virtual void OnProductContractSaved(SelectProductContractClickedArgs e) 
		{
			if(ProductContractSaved != null) 
			{
				ProductContractSaved(this, e);
			}
		}

		protected virtual void OnProductContractCancelled(System.EventArgs e) 
		{
			if(ProductContractCancelled != null) 
			{
				ProductContractCancelled(this, e);
			}
		}

		public override void DataBind()
		{
			if(ProductContractID.MagPriceInstanceGST != ProductContractID.EmptyValue) 
			{
				LoadDataPricingDetails();
				SetValuePricingDetails();
			} 
			else if(ProductInstance != 0) 
			{
				DataSource = new DataTable("ProductTable");

				LoadDataProduct(DataSource, ProductInstance);
				SetValueProduct();
			}
		}

		private void LoadDataPricingDetails() 
		{
			DataSource = new DataTable("PricingDetails");
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(ProductType, this.Page.MessageManager);

			productContractBusiness.SelectOne(DataSource, ProductContractID);
		}

		private void SetValuePricingDetails() 
		{
			DataRow row;
				
			if(DataSource.Rows.Count > 0) 
			{
				row	= DataSource.Rows[0];

				ProductCodeDisplay = row["Product_Code"].ToString();
				ProductNameDisplay = row["Product_Sort_Name"].ToString();
				YearDisplay = Convert.ToInt32(row["Pricing_Year"]);
				SeasonDisplay = row["Pricing_Season"].ToString();
				Status = Convert.ToInt32(row["Status"]);
				EffectiveDate = Convert.ToDateTime(row["EffectiveDate"]);
				EndDate = Convert.ToDateTime(row["EndDate"]);
				DateSubmitted = Convert.ToDateTime(row["DateSubmitted"]);
				RemitRate = Convert.ToDouble(row["Remit_Rate"]);
				ConversionRate = Convert.ToDouble(row["ConversionRate"]);
				NewsStandPrice = Convert.ToDouble(row["NewsStand_Price_Yr"]);
				ListingLevelID = Convert.ToInt32(row["ListingLevelID"]);
				ListingCopyText = row["ListingCopyText"].ToString();
				AdInCatalog = Convert.ToInt32(row["AdInCatalog"]);
				AdPageSizeID = Convert.ToInt32(row["AdPageSizeID"]);
				AdPaymentCurrency = Convert.ToInt32(row["AdCostCurrency"]);
				AdCost = Convert.ToDouble(row["AdCost"]);
				Comment = row["Comment"].ToString();
				EffortKey = row["Effort_Key"].ToString();
				EffortKeyRequired = Convert.ToInt32(row["EffortKeyRequired"]);
				NumberOfIssues = Convert.ToInt32(row["Nbr_of_Issues"]);
				BasePrice = Convert.ToDouble(row["Basic_Price_Yr"]);
				QSPPriceGST = Convert.ToDouble(row["QSP_PriceGST"]);
				QSPPriceHST = Convert.ToDouble(row["QSP_PriceHST"]);
				InternetApproval = Convert.ToInt32(row["InternetApproval"]);
				ABCCode = row["ABCCode"].ToString();
				QSPPremiumID = Convert.ToInt32(DataSource.Rows[0]["QSPPremiumID"]);
				OracleCode = row["OracleCode"].ToString();
				FSApplicabilityId = Convert.ToInt32(row["FSApplicabilityId"]);
				FSDistributionLevelID = Convert.ToInt32(row["FSDistributionLevelID"]);
				FSExtraLimitRate = Convert.ToInt32(row["FSExtra_Limit_Rate"]);
				FSIsBrochure = Convert.ToBoolean(row["FSIsBrochure"]);
				FSCatalogProductCode = row["FSCatalog_Product_Code"].ToString();
				FSContentCatalogCode = row["FSContent_Catalog_Code"].ToString();
				TaxRegionID = Convert.ToInt32(row["TaxRegionID"]);
				PremiumIndicator = row["PremiumInd"].ToString();
				PremiumCode = row["PremiumCode"].ToString();
				PremiumCopy = row["PremiumCopy"].ToString();
				FSProvinceCode = row["FSProvinceCode"].ToString();
				ContractFormReceived =Convert.ToInt32(row["ContractFormReceived"]);

				MagazineCoverFilename  = row["MagazineCoverFilename"].ToString();

				CatalogAdFilename = row["CatalogAdFilename"].ToString();
				CatalogPageNumber =Convert.ToInt32(row["CatalogPageNumber"]);
				PlacementLevel =Convert.ToInt32(row["PlacementLevel"]);
				ContractComments = row["ContractComment"].ToString();
				PrinterComments = row["PrinterComment"].ToString();
				QspcaListingCopyText = row["QspcaListingCopyText"].ToString();

				BasePriceSansPostage=Convert.ToDouble(row["BasePriceSansPostage"]);
				BaseRemitRate=Convert.ToDouble(row["BaseRemitRate"]);
				PostageRemitRate=Convert.ToDouble(row["PostageRemitRate"]);
				PostageAmount=Convert.ToDouble(row["PostageAmount"]);
				Currency = row["Currency"].ToString();
                VendorProductCode = row["VendorProductCode"].ToString();
			    AddlHandlingFee = Convert.ToDouble(row["AddlHandlingFee"]);
                ListAgentCode = row["ListAgentCode"].ToString();
                QSPAgencyCode = row["QSPAgencyCode"].ToString();
			}
		}

		private void LoadDataProduct(DataTable productTable, int productInstance) 
		{
			this.Page.BusProduct.SelectByProductInstance(productTable, productInstance);

			if(productTable.Rows.Count > 0) 
			{
				seasonTable = new DataTable("Season");

				this.Page.BusCatalog.SelectSeason(seasonTable, Convert.ToInt32(productTable.Rows[0]["Product_Year"]), productTable.Rows[0]["Product_Season"].ToString());
			}
		}

		private void SetValueProduct() 
		{
			DataRow row;
				
			if(DataSource.Rows.Count > 0) 
			{
				row	= DataSource.Rows[0];

				ProductCodeDisplay = row["Product_Code"].ToString();
				ProductNameDisplay = row["Product_Sort_Name"].ToString();
				YearDisplay = Convert.ToInt32(row["Product_Year"]);
				SeasonDisplay = row["Product_Season"].ToString();
				Status = Convert.ToInt32(row["Status"]);
				DateSubmitted = DateTime.Now;

				if(seasonTable.Rows.Count > 0) 
				{
					EffectiveDate = Convert.ToDateTime(seasonTable.Rows[0]["StartDate"]);
					EndDate = Convert.ToDateTime(seasonTable.Rows[0]["EndDate"]);
				} 
				else 
				{
					EffectiveDate = DateTime.Now;
					EndDate = DateTime.Now;
				}

				RemitRate = 0;
				if(Convert.ToInt32(row["Currency"]) == 801) 
				{
					ConversionRate = 100;
					Currency="CAD";
				}
				else if(Convert.ToInt32(row["Currency"]) == 802 && seasonTable.Rows.Count > 0) 
				{
					ConversionRate = Convert.ToDouble(seasonTable.Rows[0]["DefaultConversionRate"]) * 100.00;
					Currency="USD";
				} 
				else 
				{
					ConversionRate = 0;
				}
				NewsStandPrice = 0;
				ListingLevelID = 0;
				ListingCopyText = String.Empty;
				AdInCatalog = 0;
				AdPageSizeID = 0;
				AdPaymentCurrency = 0;
				AdCost = 0;
				Comment = String.Empty;
				EffortKey = String.Empty;
				NumberOfIssues = Convert.ToInt32(row["Nbr_Of_Issues_Per_Year"]);
				BasePrice = 0;
				QSPPriceGST = 0;
				QSPPriceHST = 0;
            if (Convert.ToInt32(row["Type"]) == 46001 || Convert.ToInt32(row["Type"]) == 46002 || Convert.ToInt32(row["Type"]) == 46018 || Convert.ToInt32(row["Type"]) == 46020 || Convert.ToInt32(row["Type"]) == 46024)
					InternetApproval = 1;
				else
					InternetApproval = 0;
				ABCCode = String.Empty;
				QSPPremiumID = 0;
				OracleCode = row["OracleCode"].ToString();
                VendorProductCode = row["VendorProductCode"].ToString();
				FSApplicabilityId = 0;
				FSDistributionLevelID = 0;
				FSExtraLimitRate = 0;
				FSIsBrochure = false;
				FSCatalogProductCode = String.Empty;
				FSContentCatalogCode = String.Empty;
				TaxRegionID = 0;
				PremiumIndicator = String.Empty;
				PremiumCode = String.Empty;
				PremiumCopy = String.Empty;
				FSProvinceCode = String.Empty;
				ContractFormReceived =0;

				MagazineCoverFilename  =  String.Empty;

				CatalogAdFilename =  String.Empty;
				CatalogPageNumber =0;
				PlacementLevel =0;
				ContractComments =  String.Empty;
				PrinterComments =  String.Empty;
				QspcaListingCopyText =  String.Empty;
				BasePriceSansPostage=0;
				PostageRemitRate=0;
				PostageAmount=0;
				BaseRemitRate =0.00;
				RemitRate = 0.00;
			    AddlHandlingFee = 0.00;
                ListAgentCode = String.Empty;
                QSPAgencyCode = String.Empty;
			}
		}

		protected virtual void SaveProductContractInformations() 
		{
			if(ProductContractID.MagPriceInstanceGST != ProductContractID.EmptyValue) 
			{
				UpdateProductContractInformations();
			} 
			else if(this.ProductInstance != 0) 
			{
				InsertProductContractInformations();
			}
		}

		protected virtual void InsertProductContractInformations() 
		{
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(ProductType, this.Page.MessageManager);

			productContractBusiness.Insert(ProductInstance, ProductCodeDisplay, YearDisplay, SeasonDisplay, 
				this.Page.CatalogSectionInfo.CatalogSectionID, Status, EffectiveDate, EndDate, DateSubmitted, RemitRate, 
				ConversionRate, NewsStandPrice, ListingLevelID, ListingCopyText, AdInCatalog, AdPageSizeID, AdPaymentCurrency, 
				AdCost, Comment, EffortKeyRequired, EffortKey, NumberOfIssues, BasePrice, QSPPriceGST, QSPPriceHST, 
				InternetApproval, ABCCode, QSPPremiumID, OracleCode, FSApplicabilityId, FSDistributionLevelID, 
				FSExtraLimitRate, FSIsBrochure, FSCatalogProductCode, FSContentCatalogCode, FSProgramID, TaxRegionID, 
				PremiumIndicator, PremiumCode, PremiumCopy, FSProvinceCode, this.Page.UserID,
				ContractFormReceived,  MagazineCoverFilename,  CatalogAdFilename,
				CatalogPageNumber,  
				Convert.ToInt32(PlacementLevel),   ContractComments,
                PrinterComments, QspcaListingCopyText, BasePriceSansPostage, PostageRemitRate, PostageAmount, BaseRemitRate, AddlHandlingFee, ListAgentCode, QSPAgencyCode);
		}

		protected virtual void UpdateProductContractInformations() 
		{
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(ProductType, this.Page.MessageManager);

			productContractBusiness.Update(this.ProductContractID, Status, EffectiveDate, EndDate, DateSubmitted, RemitRate, ConversionRate, NewsStandPrice, ListingLevelID, ListingCopyText, AdInCatalog, AdPageSizeID, AdPaymentCurrency, AdCost, Comment, EffortKeyRequired, EffortKey, NumberOfIssues, BasePrice, QSPPriceGST, QSPPriceHST, InternetApproval, ABCCode, QSPPremiumID, OracleCode, FSApplicabilityId, FSDistributionLevelID, FSExtraLimitRate, FSIsBrochure, FSCatalogProductCode, FSContentCatalogCode, FSProgramID, TaxRegionID, PremiumIndicator, PremiumCode, PremiumCopy, FSProvinceCode,
				ContractFormReceived,  MagazineCoverFilename,  CatalogAdFilename,
				CatalogPageNumber, 
				Convert.ToInt32(PlacementLevel),  ContractComments,
                PrinterComments, QspcaListingCopyText, BasePriceSansPostage, PostageRemitRate, PostageAmount, BaseRemitRate, AddlHandlingFee, ListAgentCode, QSPAgencyCode);
		}
	}
}
