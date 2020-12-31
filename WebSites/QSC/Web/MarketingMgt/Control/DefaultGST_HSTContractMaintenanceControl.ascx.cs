namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.CommonWeb.UC;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class DefaultGST_HSTContractMaintenanceControl : ProductContractMaintenanceControl
	{
		private const string DROPDOWNLIST_PREMIUM_BLANK_ENTRY = "None";
		private const int DEFAULT_NUMBER_OF_ISSUES = 1;
		private const double DEFAULT_CONVERSION_RATE = 100.00;

		protected System.Web.UI.WebControls.Label Label15;
		protected QSP.WebControl.TextBoxFloat tbxRemitRate;
		protected System.Web.UI.WebControls.Label Label16;
		protected QSP.WebControl.TextBoxFloat tbxConversionRate;
		protected System.Web.UI.WebControls.Label Label17;
		protected QSP.WebControl.Currency tbxNewstandPricePerIssue;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.TextBox tbxEffortKey;
		protected QSP.WebControl.TextBoxInteger tbxNumberOfIssues;
		protected QSP.WebControl.TextBoxFloat tbxBasePrice;
		protected System.Web.UI.WebControls.Label lblQSPPriceGST;
		protected System.Web.UI.WebControls.Label lblQSPPriceHST;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label lblConvertedCADBasePrice;

		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			SelectProductContractClickedArgs args;

			try 
			{
				SaveProductContractInformations();

				args = new SelectProductContractClickedArgs(ProductContractID);

				OnProductContractSaved(args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				OnProductContractCancelled(EventArgs.Empty);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public override ProductType ProductType
		{
			get
			{
				ProductType productType = ProductType.Magazine;

				if(ViewState["ProductType"] != null) 
				{
					productType = (ProductType) ViewState["ProductType"];
				}

				return productType;
			}
			set 
			{
				ViewState["ProductType"] = value;
			}
		}

		#region Fields

		protected override string ProductCodeDisplay
		{
			get
			{
				return this.lblProductCode.Text;
			}
			set
			{
				this.lblProductCode.Text = value;
			}
		}

		protected override string ProductNameDisplay
		{
			get
			{
				return this.lblProductName.Text;
			}
			set
			{
				this.lblProductName.Text = value;
			}
		}

		protected override int YearDisplay
		{
			get
			{
				int year = 0;

				try 
				{
					year = Convert.ToInt32(this.lblYear.Text);
				} 
				catch { }

				return year;
			}
			set
			{
				this.lblYear.Text = value.ToString();
			}
		}

		protected override string SeasonDisplay
		{
			get
			{
				return this.lblSeason.Text;
			}
			set
			{
				this.lblSeason.Text = value;
			}
		}

		protected override int Status
		{
			get
			{
				return Convert.ToInt32(this.rblContractStatus.SelectedValue);
			}
			set
			{
				this.rblContractStatus.SelectedIndex = this.rblContractStatus.Items.IndexOf(this.rblContractStatus.Items.FindByValue(value.ToString()));
			}
		}

		protected override DateTime EffectiveDate
		{
			get
			{
				DateTime effectiveDate = new DateTime(1995, 1, 1);

				if(ViewState["EffectiveDate"] != null) 
				{
					effectiveDate = Convert.ToDateTime(ViewState["EffectiveDate"]);
				}

				return effectiveDate;
			}
			set
			{
				ViewState["EffectiveDate"] = value;
			}
		}

		protected override DateTime EndDate
		{
			get
			{
				DateTime endDate = new DateTime(1995, 1, 1);

				if(ViewState["EndDate"] != null) 
				{
					endDate = Convert.ToDateTime(ViewState["EndDate"]);
				}

				return endDate;
			}
			set
			{
				ViewState["EndDate"] = value;
			}
		}

		protected override DateTime DateSubmitted
		{
			get
			{
				DateTime dateSubmitted = new DateTime(1995, 1, 1);

				if(ViewState["DateSubmitted"] != null) 
				{
					dateSubmitted = Convert.ToDateTime(ViewState["DateSubmitted"]);
				}

				return dateSubmitted;
			}
			set
			{
				ViewState["DateSubmitted"] = value;
			}
		}

		protected override double RemitRate
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override double ConversionRate
		{
			get
			{
				return DEFAULT_CONVERSION_RATE;
			}
			set { }
		}

		protected override double NewsStandPrice
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int ListingLevelID
		{
			get
			{
				return this.ddlListingLevel.Value;
			}
			set
			{
				this.ddlListingLevel.Value = value;
			}
		}

		protected override string ListingCopyText
		{
			get
			{
				return this.tbxListingCopyText.Text;
			}
			set
			{
				this.tbxListingCopyText.Text = value;
			}
		}

		protected override int AdInCatalog
		{
			get
			{
				return Convert.ToInt32(this.rblAdvertising.SelectedValue);
			}
			set
			{
				this.rblAdvertising.SelectedIndex = this.rblAdvertising.Items.IndexOf(this.rblAdvertising.Items.FindByValue(value.ToString()));
			}
		}

		protected override int AdPageSizeID
		{
			get
			{
				return this.ddlAdPageSize.Value;
			}
			set
			{
				this.ddlAdPageSize.Value = value;
			}
		}

		protected override int AdPaymentCurrency
		{
			get
			{
				return this.ddlAdPaymentCurrency.Value;
			}
			set
			{
				this.ddlAdPaymentCurrency.Value = value;
			}
		}

		protected override double AdCost
		{
			get
			{
				return this.tbxAdCost.Value;
			}
			set
			{
				this.tbxAdCost.Value = value;
			}
		}

		protected override string Comment
		{
			get
			{
				return this.tbxComment.Text;
			}
			set
			{
				this.tbxComment.Text = value;
			}
		}

		protected override int EffortKeyRequired
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string EffortKey
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int NumberOfIssues
		{
			get
			{
				return DEFAULT_NUMBER_OF_ISSUES;
			}
			set { }
		}

		protected override double BasePrice
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override double QSPPriceGST
		{
			get
			{
				return this.tbxQSPPriceGST.Value;
			}
			set
			{
				this.tbxQSPPriceGST.Value = value;
			}
		}

		protected override double QSPPriceHST
		{
			get
			{
				return this.tbxQSPPriceHST.Value;
			}
			set
			{
				this.tbxQSPPriceHST.Value = value;
			}
		}

		protected override int InternetApproval
		{
			get
			{
				return Convert.ToBoolean(this.rblInternetApproval.SelectedValue) ? 1 : 0;
			}
			set
			{
				this.rblInternetApproval.SelectedIndex = this.rblInternetApproval.Items.IndexOf(this.rblInternetApproval.Items.FindByValue(((bool) (value == 1)).ToString()));
			}
		}

		protected override string ABCCode
		{
			get
			{
				return this.tbxABCCode.Text;
			}
			set
			{
				this.tbxABCCode.Text = value;
			}
		}

		protected override int QSPPremiumID
		{
			get
			{
				return this.ddlPremium.Value;
			}
			set
			{
				this.ddlPremium.Value = value;
			}
		}

		protected override string OracleCode
		{
			get
			{
				return this.lblOracleCode.Text;
			}
			set 
			{
				this.lblOracleCode.Text = value;
			}
		}

		protected override int FSApplicabilityId
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int FSDistributionLevelID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int FSExtraLimitRate
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override bool FSIsBrochure
		{
			get
			{
				return false;
			}
			set { }
		}

		protected override string FSCatalogProductCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string FSContentCatalogCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int FSProgramID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int TaxRegionID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string PremiumIndicator
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string PremiumCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string PremiumCopy
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string FSProvinceCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		#endregion

		public override void DataBind()
		{
			SetValueDDL();

			base.DataBind ();

			SetValueDDLPremium();
		}

		private void SetValueDDL() 
		{
			SetValueDDLAdPageSize();
			SetValueDDLAdPaymentCurrency();
			SetValueDDLListingLevel();
		}

		private void SetValueDDLAdPageSize() 
		{
			if(this.ddlAdPageSize.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusAdPageSize.SelectAll(table);
				
				this.ddlAdPageSize.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in table.Rows)
				{
					this.ddlAdPageSize.Items.Add(new ListItem(row["Description"].ToString(), row["ID"].ToString()));
				}	
			}
		}

		private void SetValueDDLAdPaymentCurrency() 
		{
			if(this.ddlAdPaymentCurrency.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table, 800);

				this.ddlAdPaymentCurrency.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in Table.Rows) 
				{
					this.ddlAdPaymentCurrency.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(), row[CodeDetailTable.FLD_INSTANCE].ToString()));
				}
			}
		}

		private void SetValueDDLListingLevel() 
		{
			if(this.ddlListingLevel.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusListingLevel.SelectAll(table);
				
				this.ddlListingLevel.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in table.Rows)
				{
					this.ddlListingLevel.Items.Add(new ListItem(row["Description"].ToString(), row["ID"].ToString()));
				}	
			}
		}

		private void SetValueDDLPremium() 
		{
			if(this.ddlPremium.Items.Count == 0)
			{
				DataTable table = new DataTable();

				if(ProductContractID.MagPriceInstanceGST != ProductContractID.EmptyValue && ProductContractID.MagPriceInstanceHST != 0) 
				{
					this.Page.BusCatalog.SelectAllPremiums(table, Convert.ToInt32(DataSource.Rows[0]["Pricing_Year"]), DataSource.Rows[0]["Pricing_Season"].ToString());
				} 
				else if(this.ProductInstance != 0) 
				{
					this.Page.BusCatalog.SelectAllPremiums(table, Convert.ToInt32(DataSource.Rows[0]["Product_Year"]), DataSource.Rows[0]["Product_Season"].ToString());
				}
				
				this.ddlPremium.Items.Add(new ListItem(DROPDOWNLIST_PREMIUM_BLANK_ENTRY, "0"));

				foreach(DataRow row in table.Rows)
				{
					this.ddlPremium.Items.Add(new ListItem(row["EnglishDescription"].ToString(), row["ID"].ToString()));
				}	
			}
		}
	}
}
