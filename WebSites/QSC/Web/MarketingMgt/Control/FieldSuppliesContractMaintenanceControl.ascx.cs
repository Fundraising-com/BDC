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
	using QSP.WebControl.DataAccess.Business;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class FieldSuppliesContractMaintenanceControl : ProductContractMaintenanceControl
	{
		private const int FS_APPLICABILITY_ID_CODE_HEADER_INSTANCE = 43100;
		private const int FS_DISTRIBUTION_LEVEL_ID_CODE_HEADER_INSTANCE = 44000;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddJavaScript();
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

		protected void ddlTaxRegionID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try 
			{
				SetValueDDLFSProvinceCode();

				SetEnabled();
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
				return ProductType.FieldSupplies;
			}
			set { }
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
				return 0;
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
				return 0;
			}
			set { }
		}

		protected override string ListingCopyText
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int AdInCatalog
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int AdPageSizeID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int AdPaymentCurrency
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override double AdCost
		{
			get
			{
				return 0;
			}
			set { }
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
				return 0;
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
				double qspPriceGST = 0;

				try 
				{
					qspPriceGST = Convert.ToDouble(this.tbxCatalogPrice.Text);
				} 
				catch { }

				return qspPriceGST;
			}
			set
			{
				this.tbxCatalogPrice.Text = value.ToString("N");
			}
		}

		protected override double QSPPriceHST
		{
			get
			{
				double qspPriceHST = 0;

				try 
				{
					qspPriceHST = Convert.ToDouble(this.tbxCatalogPrice.Text);
				}
				catch { }

				return qspPriceHST;
			}
			set { }
		}

		protected override int InternetApproval
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string ABCCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int QSPPremiumID
		{
			get
			{
				return 0;
			}
			set { }
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
				return this.ddlFSApplicabilityId.Value;
			}
			set
			{
				this.ddlFSApplicabilityId.Value = value;
			}
		}

		protected override int FSDistributionLevelID
		{
			get
			{
				return this.ddlFSDistributionLevelID.Value;
			}
			set
			{
				this.ddlFSDistributionLevelID.Value = value;
			}
		}

		protected override int FSExtraLimitRate
		{
			get
			{
				return this.tbxFSExtraLimitRate.Value;
			}
			set
			{
				this.tbxFSExtraLimitRate.Value = value;
			}
		}

		protected override bool FSIsBrochure
		{
			get
			{
				return this.chkFSIsBrochure.Checked;
			}
			set
			{
				this.chkFSIsBrochure.Checked = value;
			}
		}

		protected override string FSCatalogProductCode
		{
			get
			{
				return this.lblProductCode.Text;
			}
			set { }
		}

		protected override string FSContentCatalogCode
		{
			get
			{
				return this.ddlFSContentCatalogCode.Value;
			}
			set
			{
				this.ddlFSContentCatalogCode.Value = value;
			}
		}

		protected override int FSProgramID
		{
			get
			{
				return this.Page.CatalogSectionInfo.FSProgramID;
			}
			set { }
		}

		protected override int TaxRegionID
		{
			get
			{
				return this.ddlTaxRegionID.Value;
			}
			set
			{
				this.ddlTaxRegionID.Value = value;
				SetValueDDLFSProvinceCode();
			}
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
				return this.ddlFSProvinceCode.SelectedValue;
			}
			set
			{
				this.ddlFSProvinceCode.SelectedIndex = this.ddlFSProvinceCode.Items.IndexOf(this.ddlFSProvinceCode.Items.FindByValue(value));
			}
		}
		protected override int ContractFormReceived 
		{
			get
			{
				return 0;
			}
			set { }
		}
		protected override string MagazineCoverFilename 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override string CatalogAdFilename 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override int CatalogPageNumber 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		
		protected override int PlacementLevel 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		
		protected override string ContractComments 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override string PrinterComments 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override string QspcaListingCopyText 
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override double BasePriceSansPostage 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		protected override double BaseRemitRate 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		protected override double PostageRemitRate 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		protected override double PostageAmount 
		{
			get
			{
				return 0;
			}
			set { }
		} 
		protected override string Currency
		{
			get
			{
				return String.Empty;
			}
			set
			{
				
			}
		}
		#endregion

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript();

			AddJavaScriptEnableContentCatalogInformation();
		}

		private void AddJavaScriptEnableContentCatalogInformation() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function EnableContentCatalogInformation() {\n";
			script += "    var enabled = document.getElementById(\"" + this.chkFSIsBrochure.ClientID + "\").checked;\n";
			script += "    document.getElementById(\"" + this.ddlFSContentCatalogCode.ClientID + "\").disabled = !enabled;\n";
			script += "    document.getElementById(\"" + this.ddlTaxRegionID.ClientID + "\").disabled = !enabled;\n";
			
			script += "    if(!enabled) {\n";
			script += "      document.getElementById(\"" + this.ddlFSContentCatalogCode.ClientID + "\").selectedIndex = 0;\n";
			script += "      document.getElementById(\"" + this.ddlTaxRegionID.ClientID + "\").selectedIndex = 0;\n";
			script += "    }\n";

			script += "    enabled = (document.getElementById(\"" + this.ddlTaxRegionID.ClientID + "\").selectedIndex != 0);\n";
			script += "    document.getElementById(\"" + this.ddlFSProvinceCode.ClientID + "\").disabled = !enabled;\n";
			
			script += "    if(!enabled) {\n";
			script += "      document.getElementById(\"" + this.ddlFSProvinceCode.ClientID + "\").selectedIndex = 0;\n";
			script += "    }\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("EnableContentCatalogInformation", script);
			this.Page.RegisterStartupScript("StartupEnableContentCatalogInformation", "<script language=\"javascript\">EnableContentCatalogInformation();</script>");
			this.chkFSIsBrochure.Attributes["onClick"] = "EnableContentCatalogInformation();";
		}

		#endregion

		public override void DataBind()
		{
			SetValueDDL();

			base.DataBind ();

			SetEnabled();
		}

		private void SetEnabled() 
		{
			this.ddlFSProvinceCode.Enabled = (this.ddlTaxRegionID.SelectedIndex != 0);
		}

		private void SetValueDDL()
		{
			SetValueDDLFSApplicabilityId();
			SetValueDDLFSDistributionLevelID();
			SetValueDDLFSContentCatalogCode();
			SetValueDDLTaxRegionID();
		}

		private void SetValueDDLFSApplicabilityId()
		{
			DataTable table;
			
			if(this.ddlFSApplicabilityId.Items.Count == 0) 
			{
				table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(table, FS_APPLICABILITY_ID_CODE_HEADER_INSTANCE);

				this.ddlFSApplicabilityId.DataSource = table;
				this.ddlFSApplicabilityId.DataTextField = "Description";
				this.ddlFSApplicabilityId.DataValueField = "Instance";
				this.ddlFSApplicabilityId.DataBind();
			}
		}

		private void SetValueDDLFSDistributionLevelID()
		{
			DataTable table;
			
			if(this.ddlFSDistributionLevelID.Items.Count == 0) 
			{
				table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(table, FS_DISTRIBUTION_LEVEL_ID_CODE_HEADER_INSTANCE);

				table.DefaultView.RowFilter = "Instance >= 44030";
				this.ddlFSDistributionLevelID.DataSource = table.DefaultView;
				this.ddlFSDistributionLevelID.DataTextField = "Description";
				this.ddlFSDistributionLevelID.DataValueField = "Instance";
				this.ddlFSDistributionLevelID.DataBind();
			}
		}

		private void SetValueDDLFSContentCatalogCode() 
		{
			DataTable table;
			
			if(this.ddlFSContentCatalogCode.Items.Count == 0) 
			{
				table = new DataTable();
				this.Page.BusCatalog.SelectSearch(table, String.Empty, String.Empty, 0, String.Empty, 0, String.Empty, 0, 0, String.Empty);
				table.DefaultView.Sort = "Code";

				this.ddlFSContentCatalogCode.DataSource = table.DefaultView;
				this.ddlFSContentCatalogCode.DataTextField = "Code";
				this.ddlFSContentCatalogCode.DataValueField = "Code";
				this.ddlFSContentCatalogCode.DataBind();
			}
		}

		private void SetValueDDLTaxRegionID() 
		{
			DataTable table;
			
			if(this.ddlTaxRegionID.Items.Count == 0) 
			{
				table = new DataTable();
				this.Page.BusTaxRegion.SelectAll(table);

				this.ddlTaxRegionID.DataSource = table;
				this.ddlTaxRegionID.DataTextField = "Description";
				this.ddlTaxRegionID.DataValueField = "ID";
				this.ddlTaxRegionID.DataBind();
			}
		}

		private void SetValueDDLFSProvinceCode() 
		{
			this.ddlFSProvinceCode.TaxRegion = (TaxRegion) TaxRegionID;
			this.ddlFSProvinceCode.DataBind();
		}
	}
}
