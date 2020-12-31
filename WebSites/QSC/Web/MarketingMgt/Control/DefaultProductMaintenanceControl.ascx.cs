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
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class DefaultProductMaintenanceControl : ProductMaintenanceControl
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor A1;

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

		protected void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
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

                if (value == ProductType.ToRememberThis)
                {
                    tbxVendorProductCode.Attributes.Add("style", "display: ");
                    LabelVendorProductCode.Attributes.Add("style", "display: ");
                }
                else
                {
                    tbxVendorProductCode.Attributes.Add("style", "display: none");
                    LabelVendorProductCode.Attributes.Add("style", "display: none");
                }
			}
		}

		#region Fields

		protected override ProductType EnteredProductType
		{
			get
			{
				return (ProductType) Convert.ToInt32(this.ddlProductType.SelectedValue);
			}
			set
			{
				this.ddlProductType.SelectedIndex = this.ddlProductType.Items.IndexOf(this.ddlProductType.Items.FindByValue(Convert.ToInt32(value).ToString()));
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
				return Convert.ToInt32(this.ddlYear.SelectedValue);
			}
			set
			{
				this.ddlYear.SelectedIndex = this.ddlYear.Items.IndexOf(this.ddlYear.Items.FindByValue(value.ToString()));
			}
		}

		protected override string ProductName
		{
			get
			{
				return this.tbxProductName.Text.ToUpper();
			}
			set
			{
				this.tbxProductName.Text = value.ToUpper();
			}
		}

		protected override string ProductSortName
		{
			get
			{
				return ProductName;
			}
			set
			{
				ProductName = value;
			}
		}

		protected override string Language
		{
			get
			{
				return this.ddlLanguage.SelectedValue;
			}
			set
			{
				this.ddlLanguage.SelectedIndex = this.ddlLanguage.Items.IndexOf(this.ddlLanguage.Items.FindByValue(value));
			}
		}

		protected override int CategoryID
		{
			get
			{
				return 0;
			}
			set { }
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
				return 0;
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

		protected override int PublisherID
		{
			get
			{
				return 0;
			}
			set { }
		}

		protected override int FulfillmentHouseID
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

		protected override string VendorNumber
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string VendorSiteName
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string PayGroupLookUpCode
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override int Currency
		{
			get
			{
				return 801;
			}
			set { }
		}

		protected override string GSTRegistrationNumber
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string HSTRegistrationNumber
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string PSTRegistrationNumber
		{
			get
			{
				return String.Empty;
			}
			set { }
		}

		protected override string OracleCode
		{
			get
			{
				return this.tbxOracleCode.Text;
			}
			set
			{
				this.tbxOracleCode.Text = value;
			}
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
				return String.Empty;
			}
			set { }
		}

		protected override bool IsQSPExclusive
		{
			get
			{
				return false;
			}
			set { }
		}

		protected override string EnglishDescription
		{
			get
			{
				return this.tbxEnglishDescription.Text;
			}
			set
			{
				this.tbxEnglishDescription.Text = value;
			}
		}

		protected override string FrenchDescription
		{
			get
			{
				return this.tbxFrenchDescription.Text;
			}
			set
			{
				this.tbxFrenchDescription.Text = value;
			}
		}

        protected override string VendorProductCode
        {
            get
            {
                return this.tbxVendorProductCode.Text;
            }
            set
            {
                this.tbxVendorProductCode.Text = value;
            }
        }

		#endregion

		public override void DataBind()
		{
			SetValueDDL();

			base.DataBind ();
		}

		private void SetValueDDL() 
		{
			SetValueDDLYear();
			SetValueDDLSeason();
			SetValueDDLProductType();
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

		private void SetValueDDLProductType() 
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
	}
}
