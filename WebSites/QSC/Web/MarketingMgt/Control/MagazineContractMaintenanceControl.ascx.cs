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

	public enum MultiPage {General = 1,Pricing=3, Catalog=2};

	public enum MainControl 
	{
		ctrlGeneral
		
	}
	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class MagazineContractMaintenanceControl : ProductContractMaintenanceControl
	{
		private const string DROPDOWNLIST_PREMIUM_BLANK_ENTRY = "None";

		protected Microsoft.Web.UI.WebControls.Tab iewcGeneral;
		

		
		protected	QSPFulfillment.CommonWeb.UC.DateEntry dteEffectiveDate;
		protected	QSPFulfillment.CommonWeb.UC.DateEntry dteEndDate;
		protected	QSPFulfillment.CommonWeb.UC.DateEntry dteDateSubmitted;
		//protected QSP.WebControl.TextBoxFloat tbxBasePrice;

		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.TextBox tbxCatalogCoverFilename;
		protected System.Web.UI.WebControls.TextBox tbxCatalogOfferNumber;
		protected System.Web.UI.WebControls.TextBox tbxCatalogListingNumber;
		protected System.Web.UI.WebControls.CheckBox cbCatalogPhoto;
		private const string GENERAL_RESULT = "pavGeneral";

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
			//this.Page.SelectResultClicked +=new SelectResultMarketingEventHandler(ctrl_SelectClicked);
			
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ProductInstance = 0;

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
		public MultiPage SelectedPage
		{
			get
			{
				return (MultiPage)this.mupMainPage.SelectedIndex;
			}
			set
			{
				
				this.mupMainPage.SelectedIndex = (int)value-1;
				this.tbsMainPage.SelectedIndex = (int)value-1;
			}
		}
	

		public override ProductType ProductType
		{
			get
			{
				return ProductType.Magazine;
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
				return this.dteEffectiveDate.Date;
			}
			set
			{
				this.dteEffectiveDate.Date = value;
			}
		}

		protected override DateTime EndDate
		{
			get
			{
				return this.dteEndDate.Date;
			}
			set
			{
				this.dteEndDate.Date = value;
			}
		}

		protected override DateTime DateSubmitted
		{
			get
			{
				return this.dteDateSubmitted.Date;
			}
			set
			{
				this.dteDateSubmitted.Date = value;
			}
		}

		protected override double RemitRate
		{
			get
			{
				//string x = Currency;

				//if(x.Equals("CAD") == true)
				//	return BaseRemitRate;
				//else
				//{
					double BaseRemit =  BasePriceSansPostage * ((BaseRemitRate)/100.00);
					double RemittedAmount = BaseRemit + (PostageAmount * PostageRemitRate/100.00);
					return Convert.ToDouble(Math.Round(RemittedAmount/BasePrice * 100.00, 2));
				//}
			}
			set
			{
				this.tbxRemitRate.Text = value.ToString();
			}
		}

		protected override double ConversionRate
		{
			get
			{
				return this.tbxConversionRate.Value;
			}
			set
			{
				this.tbxConversionRate.Value = value;
			}
		}

		protected override double NewsStandPrice
		{
			get
			{
				return this.tbxNewstandPricePerIssue.Value;
			}
			set
			{
				this.tbxNewstandPricePerIssue.Value = value;
			}
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
				int effortKeyRequired = 0;

				if(EffortKey.Length != 0) 
				{
					effortKeyRequired = 1;
				}

				return effortKeyRequired;
			}
			set { }
		}

		protected override string EffortKey
		{
			get
			{
				return this.tbxEffortKey.Text;
			}
			set
			{
				this.tbxEffortKey.Text = value;
			}
		}

		protected override int NumberOfIssues
		{
			get
			{
				return this.tbxNumberOfIssues.Value;
			}
			set
			{
				this.tbxNumberOfIssues.Value = value;
			}
		}

		protected override string Currency
		{
			get
			{
				return this.txtCurrency.Text;
			}
			set
			{
				this.txtCurrency.Text =  value.ToString();
			}
		}
		protected override double BasePrice
		{
			get
			{
				return BasePriceSansPostage + PostageAmount;
			}
			set
			{
				this.tbxBasePrice.Text =  value.ToString();
			}
		}

		protected override double QSPPriceGST
		{
			get
			{
				return Math.Ceiling(Math.Round(((BasePrice * ConversionRate / 100.00) * (1 + GSTTaxRate)), 2, MidpointRounding.AwayFromZero));
			}
			set
			{
				this.lblQSPPriceGST.Text = value.ToString("N");
			}
		}

		protected override double QSPPriceHST
		{
			get
			{
				return Math.Ceiling(Math.Round(((BasePrice * ConversionRate / 100.00) * (1 + HSTTaxRate)), 2, MidpointRounding.AwayFromZero));
			}
			set
			{
				this.lblQSPPriceHST.Text = value.ToString("N");
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
				if(this.ddlPremium.Items.Count == 0) 
				{
					SetValueDDLPremium();
				}

				this.ddlPremium.Value = value;
			}
		}

		protected override string OracleCode
		{
			get
			{
				return String.Empty;
			}
			set { }
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
				return this.tbxPremiumIndicator.Text;
			}
			set
			{
				this.tbxPremiumIndicator.Text = value;
			}
		}

		protected override string PremiumCode
		{
			get
			{
				return this.tbxPremiumCode.Text;
			}
			set
			{
				this.tbxPremiumCode.Text = value;
			}
		}

		protected override string PremiumCopy
		{
			get
			{
				return this.tbxPremiumCopy.Text;
			}
			set
			{
				this.tbxPremiumCopy.Text = value;
			}
		}

        protected override string ListAgentCode
        {
            get
            {
                return this.tbxListAgentCode.Text;
            }
            set
            {
                this.tbxListAgentCode.Text = value;
            }
        }

        protected override string QSPAgencyCode
        {
            get
            {
                return this.tbxQSPAgencyCode.Text;
            }
            set
            {
                this.tbxQSPAgencyCode.Text = value;
            }
        }

		protected override string FSProvinceCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}
		protected override int ContractFormReceived 
		{
			get
			{
				return Convert.ToInt32(this.rblContractFormReceived.SelectedValue);
			}
			set
			{
				this.rblContractFormReceived.SelectedIndex = this.rblContractFormReceived.Items.IndexOf(this.rblContractFormReceived.Items.FindByValue(value.ToString()));
			}
		}
		protected override string MagazineCoverFilename 
		{
			get
			{
				return this.tbxMagazineCoverFilename.Text;
			}
			set
			{
				this.tbxMagazineCoverFilename.Text = value;
			}
		}
		protected override string CatalogAdFilename 
		{
			get
			{
				return this.tbxCatalogAdFilename.Text;
			}
			set
			{
				this.tbxCatalogAdFilename.Text = value;
			}
		}
		protected override int CatalogPageNumber 
		{
			get
			{
				int number = 0;

				try 
				{
					number = Convert.ToInt32(this.tbxCatalogPageNumber.Text);
				} 
				catch { }

				return number;
				 
			}
			set
			{
				this.tbxCatalogPageNumber.Text = value.ToString();
			}
		} 
		
		protected override int PlacementLevel 
		{
			get
			{
				return this.ddlPlacementLevel.Value;
			}
			set
			{
				this.ddlPlacementLevel.Value=value;
			}
		} 
		
		protected override string ContractComments 
		{
			get
			{
				return this.tbxContractComment.Text;
			}
			set
			{
				this.tbxContractComment.Text = value;
			}
		}
		protected override string PrinterComments 
		{
			get
			{
				return this.tbxPrinterComment.Text;
			}
			set
			{
				this.tbxPrinterComment.Text = value;
			}
		}
		protected override string QspcaListingCopyText 
		{
			get
			{
				return this.tbxQSPCAListingCopyText.Text;
			}
			set
			{
				this.tbxQSPCAListingCopyText.Text = value;
			}
		}
		protected override double BasePriceSansPostage 
		{
			get 
			{
				double number = 0;

				try 
				{
					number = Convert.ToDouble(this.tbxBasePriceSansPostage.Text);
				} 
				catch { }

				return number;
			}
			set 
			{
				this.tbxBasePriceSansPostage.Text=value.ToString();
			}
		} 
		protected override double BaseRemitRate 
		{
			get 
			{
				double number = 0;

				try 
				{
					number = Convert.ToDouble(this.tbxBaseRemitRate.Text);
				} 
				catch { }

				return number;
			}
			set 
			{
				this.tbxBaseRemitRate.Text=value.ToString();
			}
		} 
		protected override double PostageRemitRate 
		{
			get 
			{
				double number = 0;

				try 
				{
					number = Convert.ToDouble(this.tbxPostageRemitRate.Text);
				} 
				catch { }

				return number;
			}
			set 
			{
				this.tbxPostageRemitRate.Text=value.ToString();
			}
		} 
		protected override double PostageAmount 
		{
			get 
			{
				double number = 0;

				try 
				{
					number = Convert.ToDouble(this.tbxPostageAmount.Text);
				} 
				catch { }

				return number;
			}
			set 
			{
				this.tbxPostageAmount.Text=value.ToString();
			}
		} 
		#endregion
		
		private double GSTTaxRate 
		{
			get 
			{
				if(ViewState["GSTTaxRate"] == null) 
				{
					ViewState["GSTTaxRate"] = this.Page.BusCatalog.SelectGSTTaxRate();
				}

				return Convert.ToDouble(ViewState["GSTTaxRate"]);
			}
		}

		private double HSTTaxRate 
		{
			get 
			{
				if(ViewState["HSTTaxRate"] == null) 
				{
					ViewState["HSTTaxRate"] = this.Page.BusCatalog.SelectHSTTaxRate();
				}

				return Convert.ToDouble(ViewState["HSTTaxRate"]);
			}
		}
		
		#region Javascript

		protected override void AddJavaScript()
		{
			string script;

			base.AddJavaScript ();

			script  = "<script language=\"javascript\">\n";
			script += "  function CalculatePrices() {\n";
			script += "    var BasePriceSansPostage = document.getElementById(\"" + this.tbxBasePriceSansPostage.ClientID + "\").value;\n";
			script += "    var NbrIssues = document.getElementById(\"" + this.tbxNumberOfIssues.ClientID + "\").value;\n";
			script += "    var NewsStand = document.getElementById(\"" + this.tbxNewstandPricePerIssue.ClientID + "\").value;\n";
			script += "    var BaseRemitRate = document.getElementById(\"" + this.tbxBaseRemitRate.ClientID + "\").value;\n";
			script += "    var PostageAmount = document.getElementById(\"" + this.tbxPostageAmount.ClientID + "\").value;\n";
			script += "    var Postageremit = document.getElementById(\"" + tbxPostageRemitRate.ClientID + "\").value;\n";
			script += "    var BasePrice = parseFloat(BasePriceSansPostage) + parseFloat(PostageAmount); ";
			script += "    var Baseremit = parseFloat(BasePriceSansPostage) * (parseFloat(BaseRemitRate)/100.00);\n ";
			script += "    var RemittedAmount = parseFloat(Baseremit) + (parseFloat(PostageAmount)*parseFloat(Postageremit/100.00)) ; \n";
			script += "    var ConversionRate = document.getElementById(\"" + this.tbxConversionRate.ClientID + "\").value;\n";
			script += "    document.getElementById(\"" + this.tbxRemitRate.ClientID + "\").innerHTML =  Math.round((parseFloat(RemittedAmount)/parseFloat(BasePrice)*100)*Math.pow(10,2))/Math.pow(10,2);\n";
			script += "    var GST = " + GSTTaxRate.ToString() + ";\n";
			script += "    var HST = " + HSTTaxRate.ToString() + ";\n";
			script += "    var xxxGST = (Math.ceil((NbrIssues * NewsStand * (1 + GST)) * 100) / 100) - (Math.ceil(Math.round(BasePrice * (ConversionRate / 100.00) * (1 + GST) * 100) / 100));\n";
			script += "    var xxxHST = (Math.ceil((NbrIssues * NewsStand * (1 + HST)) * 100) / 100) - (Math.ceil(Math.round(BasePrice * (ConversionRate / 100.00) * (1 + HST) * 100) / 100));\n";
			

			script += "    if (BasePrice == parseFloat(BasePrice)) {\n";
			script += "      if (ConversionRate == parseFloat(ConversionRate)) {\n";
			script += "        document.getElementById(\"" + lblConvertedCADBasePrice.ClientID + "\").innerHTML = Math.round(BasePrice * ConversionRate) / 100.00;\n";
			script += "        document.getElementById(\"" + lblQSPPriceGST.ClientID + "\").innerHTML = Math.ceil(Math.round(((BasePrice * ConversionRate / 100.00) * (1 + GST)) * 100) / 100) + \".00\";\n";
			script += "        document.getElementById(\"" + lblQSPPriceHST.ClientID + "\").innerHTML = Math.ceil(Math.round(((BasePrice * ConversionRate / 100.00) * (1 + HST)) * 100) / 100) + \".00\";\n";
			//Math.round(num*Math.pow(10,dec))/Math.pow(10,dec)
			script += "        document.getElementById(\"" + lblTotCatNewsGST.ClientID + "\").innerHTML = Math.ceil((NbrIssues * NewsStand * (1 + GST))*Math.pow(10,2))/Math.pow(10,2) ;\n";
			script += "        document.getElementById(\"" + lblTotCatNewsHST.ClientID + "\").innerHTML = Math.ceil((NbrIssues * NewsStand * (1 + HST))*Math.pow(10,2))/Math.pow(10,2);\n";	
			script += "        document.getElementById(\"" + this.tbxBasePrice.ClientID + "\").innerHTML = BasePrice.toString();\n";
			script += "      }\n";		
			script += "      if(Math.ceil((NbrIssues * NewsStand * (1 + GST)) * 100) / 100 == 0) {";
			script += "        document.getElementById(\"" + lblTotSavingsGST.ClientID + "\").innerHTML = \"0.00\";\n";
			script += "        document.getElementById(\"" + lblTotSavingsHST.ClientID + "\").innerHTML = \"0.00\";\n";	
			script += "      }else{\n";
			script += "        var yyy = Math.ceil((NbrIssues * NewsStand * (1 + GST)) * 100) / 100;\n"; 
			script += "        var yyy2 = Math.ceil((NbrIssues * NewsStand * (1 + HST)) * 100) / 100;\n"; 
			//script += "        document.getElementById(\"" + lblTotSavingsGST.ClientID + "\").innerHTML = Math.floor(xxxGST/yyy)*100+ \".00\";\n";
			script += "        document.getElementById(\"" + lblTotSavingsGST.ClientID + "\").innerHTML = Math.floor(parseFloat(xxxGST)/parseFloat(yyy) * 100)+ \"%\";\n";
			script += "        document.getElementById(\"" + lblTotSavingsHST.ClientID + "\").innerHTML =  Math.floor(parseFloat(xxxHST)/parseFloat(yyy2) * 100)+ \"%\";\n";
			
			script += "      }\n";		
					
			script += "    }\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("CalculatePrices", script);
			this.Page.RegisterStartupScript("OnLoadCalculatePrices", "<script language=\"javascript\">CalculatePrices();</script>");
			this.tbxConversionRate.Attributes["onBlur"] = "CalculatePrices();";
			this.tbxNewstandPricePerIssue.Attributes["onBlur"] = "CalculatePrices();";
			this.tbxBasePriceSansPostage.Attributes["onBlur"] = "CalculatePrices();";
			this.tbxPostageAmount.Attributes["onBlur"] = "CalculatePrices();";
			this.tbxPostageRemitRate.Attributes["onBlur"] = "CalculatePrices();";
			this.tbxBaseRemitRate.Attributes["onBlur"] = "CalculatePrices();";
		}

		#endregion

		public override void DataBind()
		{
			SetValueDDL();

			base.DataBind ();
		}

		private void SetValueDDL() 
		{
			SetValueDDLAdPageSize();
			SetValueDDLAdPaymentCurrency();
			SetValueDDLListingLevel();
			SetValueDDLPlacementLevel();
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
		}private void SetValueDDLPlacementLevel() 
		 {
			 if(this.ddlPlacementLevel.Items.Count == 0)
			 {
				 DataTable Table = new DataTable();
				 this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table, 68000);

				 this.ddlPlacementLevel.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				 foreach(DataRow row in Table.Rows) 
				 {
					 this.ddlPlacementLevel.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(), row[CodeDetailTable.FLD_INSTANCE].ToString()));
				 }
			 }
		 }


		private void SetValueDDLPremium() 
		{
			if(this.ddlPremium.Items.Count == 0)
			{
				DataTable table = new DataTable();

				if(ProductContractID.MagPriceInstanceGST != 0 && ProductContractID.MagPriceInstanceHST != 0) 
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
