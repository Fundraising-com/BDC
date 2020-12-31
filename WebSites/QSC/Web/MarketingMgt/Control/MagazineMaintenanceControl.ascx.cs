namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Text;
	using System.Web.Mail;
	using System.Configuration;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class MagazineMaintenanceControl : ProductMaintenanceControl
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor A1;

		private DataTable FulfillmentHouseTable;

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
			this.ddlProductType.SelectedIndexChanged += new EventHandler(ddlProductType_SelectedIndexChanged);
			this.ddlFulfillmentHouse.SelectedIndexChanged += new EventHandler(ddlFulfillmentHouse_SelectedIndexChanged);
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
			try 
			{
				SaveProductInformation();

				OnProductSaved(e);
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
				OnProductCancelled(e);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try 
			{
				OnProductTypeChanged(new ProductTypeChangedArgs((ProductType) Convert.ToInt32(this.ddlProductType.SelectedValue)));
				this.btnSubmit.Click -= new System.EventHandler(this.btnSubmit_Click);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ddlFulfillmentHouse_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try 
			{
				if(FulfillmentHouseID != 0) 
				{
					LoadDataFulfillmentHouse();
					SetValueVendorSiteName();
					SetValuePayGroupLookUpCode();
				} 
				else 
				{
					SetValueVendorSiteNameEmpty();
					SetValuePayGroupLookUpCodeEmpty();
				}
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
				return ProductType.Magazine;
			}
			set { }
		}

		#region Fields

		protected override ProductType EnteredProductType
		{
			get
			{
				return (ProductType) this.ddlProductType.Value;
			}
			set
			{
				this.ddlProductType.Value = Convert.ToInt32(value);
			}
		}

		protected override string EnteredProductCode
		{
			get 
			{
				return this.tbxUMCCode.Text;
			}
			set 
			{
				this.tbxUMCCode.Text = value;
			}		
		}

		protected override string EnteredSeason
		{
			get
			{
				return this.ddlSeason.SelectedValue;
			}
			set
			{
				this.ddlSeason.SelectedIndex = this.ddlSeason.Items.IndexOf(this.ddlSeason.Items.FindByValue(value));
			}
		}

		protected override int EnteredYear
		{
			get
			{
				return this.ddlYear.Value;
			}
			set
			{
				this.ddlYear.Value = value;
			}
		}

		protected override string ProductName
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string ProductSortName
		{
			get
			{
				return this.tbxProductName.Text;
			}
			set
			{
				this.tbxProductName.Text = value;
			}
		}

		protected override string Language
		{
			get
			{
				return this.rblLanguage.SelectedValue;
			}
			set
			{
				this.rblLanguage.SelectedIndex = this.rblLanguage.Items.IndexOf(this.rblLanguage.Items.FindByValue(value));
			}
		}

		protected override int CategoryID
		{
			get
			{
				return this.ddlCategory.Value;
			}
			set
			{
				this.ddlCategory.Value = value;
			}
		}

		protected override int Status
		{
			get
			{
				return Convert.ToInt32(this.rblStatus.SelectedValue);
			}
			set
			{
				this.rblStatus.SelectedIndex = this.rblStatus.Items.IndexOf(this.rblStatus.Items.FindByValue(value.ToString()));
			}
		}

		protected override int DaysLeadTime
		{
			get
			{
				return this.tbxDaysLeadTime.Value;
			}
			set
			{
				this.tbxDaysLeadTime.Value = value;
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

		protected override int PublisherID
		{
			get
			{
				return this.ddlPublisher.Value;
			}
			set
			{
				this.ddlPublisher.Value = value;
			}
		}

		protected override int FulfillmentHouseID
		{
			get
			{
				return this.ddlFulfillmentHouse.Value;
			}
			set
			{
				this.ddlFulfillmentHouse.Value = value;
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

		protected override string VendorNumber
		{
			get
			{
				return this.tbxVendorNumber.Text;
			}
			set
			{
				this.tbxVendorNumber.Text = value;
			}
		}

		protected override string VendorSiteName
		{
			get
			{
				return this.hidVendorSiteName.Value;
			}
			set
			{
				this.hidVendorSiteName.Value = value;
			}
		}

		protected override string PayGroupLookUpCode
		{
			get
			{
				return this.hidPayGroupLookUpCode.Value;
			}
			set
			{
				this.hidPayGroupLookUpCode.Value = value;
			}
		}

		protected override int Currency
		{
			get
			{
				return this.ddlCurrency.Value;
			}
			set
			{
				this.ddlCurrency.Value = value;
			}
		}

		protected override string GSTRegistrationNumber
		{
			get
			{
				return this.tbxGST.Text;
			}
			set
			{
				this.tbxGST.Text = value;
			}
		}

		protected override string HSTRegistrationNumber
		{
			get
			{
				return this.tbxHST.Text;
			}
			set
			{
				this.tbxHST.Text =  value;
			}
		}

		protected override string PSTRegistrationNumber
		{
			get
			{
				return this.tbxPST.Text;
			}
			set
			{
				this.tbxPST.Text = value;
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

		protected override string PrizeLevel
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int PrizeLevelQuantity
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override string RemitCode
		{
			get
			{
				return this.tbxRemitCode.Text;
			}
			set
			{
				this.tbxRemitCode.Text = value;
			}
		}

		protected override bool IsQSPExclusive
		{
			get
			{
				return this.chkIsQSPExclusive.Checked;
			}
			set
			{
				this.chkIsQSPExclusive.Checked = value;
			}
		}

		protected override string EnglishDescription
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string FrenchDescription
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		#endregion

		#region Initial Values

		private string InitialRemitCode 
		{
			get 
			{
				string initialRemitCode = String.Empty;

				if(ViewState["InitialRemitCode"] != null) 
				{
					initialRemitCode = ViewState["InitialRemitCode"].ToString();
				}

				return initialRemitCode;
			}
			set 
			{
				ViewState["InitialRemitCode"] = value;
			}
		}

		private string InitialProductSortName 
		{
			get 
			{
				string initialProductSortName = String.Empty;

				if(this.ViewState["InitialProductSortName"] != null) 
				{
					initialProductSortName = this.ViewState["InitialProductSortName"].ToString();
				}

				return initialProductSortName;
			}
			set 
			{
				this.ViewState["InitialProductSortName"] = value;
			}
		}

		private string InitialVendorNumber 
		{
			get 
			{
				string initialVendorNumber = String.Empty;

				if(this.ViewState["InitialVendorNumber"] != null) 
				{
					initialVendorNumber = this.ViewState["InitialVendorNumber"].ToString();
				}

				return initialVendorNumber;
			}
			set 
			{
				this.ViewState["InitialVendorNumber"] = value;
			}
		}

		private string InitialVendorSiteName
		{
			get 
			{
				string initialVendorSiteName = String.Empty;

				if(this.ViewState["InitialVendorSiteName"] != null) 
				{
					initialVendorSiteName = this.ViewState["InitialVendorSiteName"].ToString();
				}

				return initialVendorSiteName;
			}
			set 
			{
				this.ViewState["InitialVendorSiteName"] = value;
			}
		}

		private string InitialPayGroupLookUpCode 
		{
			get 
			{
				string initialPayGroupLookUpCode = String.Empty;

				if(this.ViewState["InitialPayGroupLookUpCode"] != null) 
				{
					initialPayGroupLookUpCode = this.ViewState["InitialPayGroupLookUpCode"].ToString();
				}

				return initialPayGroupLookUpCode;
			}
			set 
			{
				this.ViewState["InitialPayGroupLookUpCode"] = value;
			}
		}

		#endregion

		#region Catalyst Data

		private bool IsCatalystDataChanged 
		{
			get 
			{
				return (IsRemitCodeChanged || IsProductSortNameChanged || IsVendorNumberChanged || IsVendorSiteNameChanged || IsPayGroupLookUpCodeChanged);
			}
		}

		private bool IsRemitCodeChanged 
		{
			get 
			{
				return (this.InitialRemitCode != String.Empty && this.InitialRemitCode != RemitCode);
			}
		}

		private bool IsProductSortNameChanged 
		{
			get 
			{
				return (this.InitialProductSortName != String.Empty && this.InitialProductSortName != ProductSortName);
			}
		}

		private bool IsVendorNumberChanged 
		{
			get 
			{
				return (this.InitialVendorNumber != String.Empty && this.InitialVendorNumber != VendorNumber);
			}
		}

		private bool IsVendorSiteNameChanged 
		{
			get 
			{
				return (this.InitialVendorSiteName != String.Empty && this.InitialVendorSiteName != VendorSiteName);
			}
		}

		private bool IsPayGroupLookUpCodeChanged 
		{
			get 
			{
				return (this.InitialPayGroupLookUpCode != String.Empty && this.InitialPayGroupLookUpCode != PayGroupLookUpCode);
			}
		}

		#endregion

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript ();

			AddJavaScriptPayGroupLookUpCodeCurrency();
			AddJavaScriptCopyGST_HST();
		}

		private void AddJavaScriptPayGroupLookUpCodeCurrency() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function SetPayGroupLookUpCodeCurrency() {\n";
			script += "    var ddlCurrency = document.getElementById(\"" + ddlCurrency.ClientID + "\");\n";
			script += "    if(ddlCurrency.selectedIndex != 0) {\n";
			script += "      var currency = ddlCurrency.options[ddlCurrency.selectedIndex].innerHTML;\n";
		
			script += "      var payGroup = document.getElementById(\"" + hidPayGroup.ClientID + "\").value;\n";
			script += "      var payGroupLookUpCode = document.getElementById(\"" + lblPayGroupLookUpCode.ClientID + "\").innerHTML;\n";
			script += "      var index = payGroupLookUpCode.indexOf(\" \", 7);\n";

			script += "      if(ddlCurrency.options[ddlCurrency.selectedIndex].value == \"801\") {\n";
			script += "        payGroupLookUpCode = \"CA QSP \" + payGroup + \" CAD REMIT\";\n";
			script += "      } else {\n";
			script += "        payGroupLookUpCode = \"CA QSP OTHER USD REMIT\";\n";
			script += "      }\n";
		
			script += "      document.getElementById(\"" + lblPayGroupLookUpCode.ClientID + "\").innerHTML = payGroupLookUpCode;\n";
			script += "      document.getElementById(\"" + hidPayGroupLookUpCode.ClientID + "\").value = payGroupLookUpCode;\n";
			script += "    }\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("SetPayGroupLookUpCodeCurrency", script);

			this.ddlCurrency.Attributes["onchange"] = "SetPayGroupLookUpCodeCurrency();";
		}

		private void AddJavaScriptCopyGST_HST() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function CopyGST_HST(newRegistrationNbr) {\n";
			script += "    var tbxGST = document.getElementById(\"" + this.tbxGST.ClientID + "\");\n";
			script += "    var tbxHST = document.getElementById(\"" + this.tbxHST.ClientID + "\");\n";
			script += "    if(tbxGST.value == \"\") {\n";
			script += "      tbxGST.value = newRegistrationNbr;\n";
			script += "    }\n";
			script += "    if(tbxHST.value == \"\") {\n";
			script += "      tbxHST.value = newRegistrationNbr;\n";
			script += "    }\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("CopyGST_HST", script);

			this.tbxGST.Attributes["onBlur"] = "CopyGST_HST(document.getElementById(\"" + this.tbxGST.ClientID + "\").value);";
			this.tbxHST.Attributes["onBlur"] = "CopyGST_HST(document.getElementById(\"" + this.tbxHST.ClientID + "\").value);";
		}

		#endregion

		public override void DataBind()
		{
			SetValueDDL();

			base.DataBind ();
		}

		private void LoadDataFulfillmentHouse() 
		{
			FulfillmentHouseTable = new DataTable("FulfillmentHouse");

			this.Page.BusFulfillmentHouse.SelectOne(FulfillmentHouseTable, FulfillmentHouseID);
		}

		protected override void SetValue()
		{
			string payGroupLookUpCode = String.Empty;

			base.SetValue ();

			this.lblVendorSiteName.Text = VendorSiteName;
			this.lblPayGroupLookUpCode.Text = PayGroupLookUpCode;
			this.hidPayGroup.Value = PayGroupLookUpCode.Substring(7, PayGroupLookUpCode.IndexOf(" ", 7) - 7);

			SetInitialValues();
		}

		protected override void SetValueEmpty()
		{
			base.SetValueEmpty ();

			this.lblVendorSiteName.Text = String.Empty;
			this.lblPayGroupLookUpCode.Text = String.Empty;
		}

		private void SetInitialValues() 
		{
			this.InitialRemitCode = RemitCode;
			this.InitialProductSortName = ProductSortName;
			this.InitialVendorNumber = VendorNumber;
			this.InitialVendorSiteName = VendorSiteName;
			this.InitialPayGroupLookUpCode = PayGroupLookUpCode;
		}

		private void SetInitialValuesEmpty() 
		{
			this.InitialRemitCode = String.Empty;
			this.InitialProductSortName = String.Empty;
			this.InitialVendorNumber = String.Empty;
			this.InitialVendorSiteName = String.Empty;
			this.InitialPayGroupLookUpCode = String.Empty;
		}

		private void SetValueDDL() 
		{
			SetValueDDLYear();
			SetValueDDLSeason();
			SetValueDDLCategory();
			SetValueDDLProductLine();
			SetValueDDLPublisher();
			SetValueDDLFulfillmentHouse();
			SetValueDDLCurrency();
		}

		private void SetValueDDLYear() 
		{
			if(this.ddlYear.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogFinancialYears(Table);

				this.ddlYear.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in Table.Rows)
				{
					this.ddlYear.Items.Add(new ListItem(row["FiscalYear"].ToString(), row["FiscalYear"].ToString()));
				}	
			}
		}

		private void SetValueDDLSeason() 
		{
			if(this.ddlSeason.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogSeasons(Table);

				this.ddlSeason.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, ""));

				foreach(DataRow row in Table.Rows)
				{
					if(row["Season"].ToString() != "Y") 
					{
						this.ddlSeason.Items.Add(new ListItem(row["Season"].ToString(), row["Season"].ToString()));
					}
				}	
			}
		}

		private void SetValueDDLCategory() 
		{
			if(this.ddlCategory.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusProduct.SelectAllProductCategories(Table);

				this.ddlCategory.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in Table.Rows) 
				{
					this.ddlCategory.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(), row[CodeDetailTable.FLD_INSTANCE].ToString()));
				}
			}
		}

		private void SetValueDDLProductLine() 
		{
			if(this.ddlProductType.Items.Count == 0)
			{
				DataTable table = new DataTable();
				
				if(this.Page.CatalogInfo != null && this.Page.CatalogSectionInfo != null) 
				{
					this.Page.BusProductType.SelectAllProductTypes(table, this.Page.CatalogInfo.Type, this.Page.CatalogSectionInfo.Type);
				} 
				else 
				{
					this.Page.BusProductType.SelectAllProductTypes(table);
				}

				this.ddlProductType.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in table.Rows) 
				{
					this.ddlProductType.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(), row[CodeDetailTable.FLD_INSTANCE].ToString()));
				}
			}
		}

		private void SetValueDDLPublisher()
		{
	
			if(ddlPublisher.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusAccount.SelectAllPublisher(Table, 0);

				this.ddlPublisher.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in Table.Rows) 
				{
					this.ddlPublisher.Items.Add(new ListItem(row["pub_name"].ToString(), row["pub_nbr"].ToString()));
				}
			}
		}

		private void SetValueDDLFulfillmentHouse()
		{
	
			if(ddlFulfillmentHouse.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusFulfillmentHouse.SelectAll(table);

				this.ddlFulfillmentHouse.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in table.Rows) 
				{
					this.ddlFulfillmentHouse.Items.Add(new ListItem(row["ful_name"].ToString(), row["ful_nbr"].ToString()));
				}
			}
		}

		private void SetValueDDLCurrency() 
		{
			if(this.ddlCurrency.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table, 800);

				this.ddlCurrency.Items.Add(new ListItem(MarketingMgtPage.DROPDOWNLIST_BLANK_ENTRY, "0"));

				foreach(DataRow row in Table.Rows) 
				{
					this.ddlCurrency.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(), row[CodeDetailTable.FLD_INSTANCE].ToString()));
				}
			}
		}

		private void SetValueVendorSiteName() 
		{
			DataRow row;

			if(FulfillmentHouseTable.Rows.Count > 0) 
			{
				row = FulfillmentHouseTable.Rows[0];

				this.lblVendorSiteName.Text = this.Page.BusFulfillmentHouse.ProcessVendorSiteName(row["Ful_State"].ToString(), row["Ful_City"].ToString());
				VendorSiteName = this.lblVendorSiteName.Text;
			}
		}

		private void SetValuePayGroupLookUpCode() 
		{
			DataRow row;

			if(FulfillmentHouseTable.Rows.Count > 0) 
			{
				row = FulfillmentHouseTable.Rows[0];

				this.lblPayGroupLookUpCode.Text = this.Page.BusFulfillmentHouse.ProcessPayGroupLookUpCode(Currency, row["PayGroupLookUpCode"].ToString());

				PayGroupLookUpCode = this.lblPayGroupLookUpCode.Text;
				this.hidPayGroup.Value = row["PayGroupLookUpCode"].ToString();
			}
		}

		private void SetValueVendorSiteNameEmpty() 
		{
			this.lblVendorSiteName.Text = String.Empty;
			VendorSiteName = String.Empty;
		}

		private void SetValuePayGroupLookUpCodeEmpty() 
		{
			this.lblPayGroupLookUpCode.Text = String.Empty;
			PayGroupLookUpCode = String.Empty;
			this.hidPayGroup.Value = String.Empty;
		}

		protected override void UpdateProductInformation()
		{
			base.UpdateProductInformation();
			
			if(this.IsCatalystDataChanged) 
			{
				SendMail();
				SetInitialValuesEmpty();
			}
		}

		private void SendMail() 
		{
			CatalystDataMailMessage catalystDataMailMessage = new CatalystDataMailMessage();
			CatalystDataProduct catalystDataProduct = new CatalystDataProduct(EnteredProductCode, EnteredYear, EnteredSeason, InitialRemitCode, RemitCode, InitialProductSortName, ProductSortName, InitialVendorNumber, VendorNumber, InitialVendorSiteName, VendorSiteName, InitialPayGroupLookUpCode, PayGroupLookUpCode);

			catalystDataMailMessage.AddProduct(catalystDataProduct);
			catalystDataMailMessage.Send();
		}
	}
}
