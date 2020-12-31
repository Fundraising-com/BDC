namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.CustomerService;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	
	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class MagazineContractSearchControl : ProductContractSearchControl
	{
		private const int COLUMN_CHECKBOX = 0;
		private const int COLUMN_PRODUCTCODEEDIT = 1;
		private const int COLUMN_YEAR = 7;
		private const int COLUMN_SEASON = 8;
		private const int COLUMN_PRODUCT_STATUS = 9;
		private const int COLUMN_CATALOG_CODE = 15;
		private const int COLUMN_CATALOG_NAME = 16;
		private const int COLUMN_SELECT = 20;
		private const int COLUMN_DELETE = 21;
		private const int COLUMN_MODIFY = 22;

		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.TextBox tbxTitleCode;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.TextBox tbxTitle;
		protected QSP.WebControl.TextBoxInteger tbxTerm;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblTerm;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblMagInstance;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSearch;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label1;
		protected QSP.WebControl.DropDownListInteger ddlYear;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected System.Web.UI.WebControls.DropDownList ddlSeason;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected QSP.WebControl.DropDownListInteger ddlPublisher;
		protected System.Web.UI.WebControls.Label Label7;
		protected QSP.WebControl.DropDownListInteger ddlFulfillmentHouse;
		protected System.Web.UI.WebControls.Label Label8;
		protected QSP.WebControl.DropDownListInteger ddlProductType;
		protected System.Web.UI.WebControls.Label Label9;
		protected QSP.WebControl.DropDownListInteger ddlProductStatus;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divYear;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSeason;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divProductStatus;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divCatalogCode;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox tbxRemitCode;
		protected QSP.WebControl.DropDownListReq ddlCatalogCode;
		protected QSPFulfillment.CustomerService.ControlerConfirmationPage ctrlControlerConfirmationPageDelete;
		protected System.Web.UI.WebControls.TextBox tbxProductCode;
		protected System.Web.UI.WebControls.Label lblProductCode;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,dtgMain,lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			try 
			{
				Search();
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
				OnProductTypeChanged(sender, new ProductTypeChangedArgs(ProductTypeSearch));
				this.btnSearch.Click -= new System.EventHandler(this.btnSearch_Click);

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

		public override string ProductCodeSearch
		{
			get
			{
				return this.tbxTitleCode.Text;
			}
			set
			{
				this.tbxTitleCode.Text = value;
			}
		}

		public override string RemitCodeSearch
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

		public override string ProductNameSearch
		{
			get
			{
				return this.tbxTitle.Text;
			}
			set
			{
				this.tbxTitle.Text = value;
			}
		}

		public override int YearSearch
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

		public override string SeasonSearch
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

		public override int ProductStatusSearch
		{
			get
			{
				return this.ddlProductStatus.Value;
			}
			set
			{
				this.ddlProductStatus.Value = value;
			}
		}

		public override ProductType ProductTypeSearch
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

		public override string OracleCodeSearch
		{
			get
			{
				return String.Empty;
			}
			set { }
		}


		public override int NumberOfIssuesSearch
		{
			get
			{
				return this.tbxTerm.Value;
			}
			set
			{
				this.tbxTerm.Value = value;
			}
		}

		public override string CatalogCodeSearch
		{
			get
			{
				return this.ddlCatalogCode.SelectedValue;
			}
			set
			{
				this.ddlCatalogCode.SelectedIndex = this.ddlCatalogCode.Items.IndexOf(this.ddlCatalogCode.Items.FindByValue(value));
			}
		}

		public override int PublisherSearch
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

		public override int FulfillmentHouseSearch
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

		#endregion
		
		#region Controls

		protected override System.Web.UI.Control SearchControl
		{
			get
			{
				return this.divSearch;
			}
		}

		protected override ControlerConfirmationPage ControlerConfirmationPageDelete
		{
			get
			{
				return this.ctrlControlerConfirmationPageDelete;
			}
		}

		protected override System.Web.UI.Control YearControl
		{
			get
			{
				return this.divYear;
			}
		}

		protected override System.Web.UI.Control SeasonControl
		{
			get
			{
				return this.divSeason;
			}
		}

		protected override System.Web.UI.Control ProductStatusControl
		{
			get
			{
				return this.divProductStatus;
			}
		}

		protected override WebControl ProductTypeControl
		{
			get
			{
				return this.ddlProductType;
			}
		}

		protected override System.Web.UI.Control CatalogCodeControl
		{
			get
			{
				return this.divCatalogCode;
			}
		}

		#endregion

		#region Columns

		protected override DataGridColumn YearColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_YEAR];
			}
		}

		protected override DataGridColumn SeasonColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_SEASON];
			}
		}

		protected override DataGridColumn ProductStatusColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_PRODUCT_STATUS];
			}
		}

		protected override DataGridColumn CatalogCodeColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_CATALOG_CODE];
			}
		}

		protected override DataGridColumn CatalogNameColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_CATALOG_NAME];
			}
		}

		protected override DataGridColumn CheckBoxColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_CHECKBOX];
			}
		}

		protected override DataGridColumn ProductCodeEditColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_PRODUCTCODEEDIT];
			}
		}

		protected override DataGridColumn SelectColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_SELECT];
			}
		}

		protected override DataGridColumn DeleteColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_DELETE];
			}
		}

		protected override DataGridColumn ModifyColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_MODIFY];
			}
		}

		#endregion

		public override void DataBindInitialData()
		{
			SetValueDDL();

			base.DataBindInitialData();
		}

		private void SetValueDDL() 
		{
			SetValueDDLYear();
			SetValueDDLSeason();
			SetValueDDLStatus();
			SetValueDDLPublisher();
			SetValueDDLFulfillmentHouse();
			SetValueDDLProductType();
			SetValueDDLCatalogCode();
		}

		private void SetValueDDLYear() 
		{
			if(this.ddlYear.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCatalog.SelectAllCatalogFinancialYears(Table);

				this.ddlYear.Items.Add(new ListItem("", "0"));

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

				this.ddlSeason.Items.Add(new ListItem("", ""));

				foreach(DataRow row in Table.Rows)
				{
					this.ddlSeason.Items.Add(new ListItem(row["Season"].ToString(), row["Season"].ToString()));
				}	
			}
		}

		private void SetValueDDLStatus() 
		{
			if(ddlProductStatus.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(Table, 30600);

				this.ddlProductStatus.Items.Add(new ListItem("", "0"));

				foreach(DataRow row in Table.Rows) 
				{
					this.ddlProductStatus.Items.Add(new ListItem(row["Description"].ToString(), row["Instance"].ToString()));
				}
			}
		}

		private void SetValueDDLPublisher()
		{
	
			if(ddlPublisher.Items.Count == 0)
			{
				DataTable Table = new DataTable();
				this.Page.BusAccount.SelectAllPublisher(Table, 0);

				this.ddlPublisher.Items.Add(new ListItem("", "0"));

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

				this.ddlFulfillmentHouse.Items.Add(new ListItem("", "0"));

				foreach(DataRow row in table.Rows) 
				{
					this.ddlFulfillmentHouse.Items.Add(new ListItem(row["ful_name"].ToString(), row["ful_nbr"].ToString()));
				}
			}
		}

		private void SetValueDDLProductType()
		{
			if(this.ddlProductType.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusProductType.SelectAllProductTypes(table, this.Page.CatalogInfo.Type, this.Page.CatalogSectionInfo.Type);

				this.ddlProductType.DataSource = table;
				this.ddlProductType.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlProductType.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlProductType.DataBind();
			}
		}

		private void SetValueDDLCatalogCode() 
		{
			DataTable table;
			
			if(this.CatalogCodeControl.Visible && this.ddlCatalogCode.Items.Count == 0) 
			{
				table = new DataTable();
				this.Page.BusCatalog.SelectSearch(table, String.Empty, String.Empty, 0, String.Empty, 0, String.Empty, 0, 0, String.Empty);
				table.DefaultView.Sort = "Code";

				this.ddlCatalogCode.DataSource = table.DefaultView;
				this.ddlCatalogCode.DataTextField = "Code";
				this.ddlCatalogCode.DataValueField = "Code";
				this.ddlCatalogCode.DataBind();
			}
		}

		public override ProductContractID GetProductContractID(DataGridItem e)
		{
			return ProductContractIDFactory.Instance.GetProductContractID(GetMagPriceInstanceGST(e), GetMagPriceInstanceHST(e), GetProductContractType(e));
		}

		private int GetMagPriceInstanceGST(DataGridItem e)
		{
			return Convert.ToInt32(((Label) e.FindControl("lblMagPriceInstanceGST")).Text);
		}

		private int GetMagPriceInstanceHST(DataGridItem e)
		{
			return Convert.ToInt32(((Label) e.FindControl("lblMagPriceInstanceHST")).Text);
		}

		public override ProductType GetProductType(DataGridItem e)
		{
			return (ProductType) Convert.ToInt32(((Label) e.FindControl("lblProductTypeInstance")).Text);
		}

		private ProductContractType GetProductContractType(DataGridItem e)
		{
			return ProductContractTypeFactory.Instance.GetProductContractType(GetProductType(e));
		}

		public override int GetProductInstance(DataGridItem e)
		{
			return Convert.ToInt32(((Label) e.FindControl("lblProductInstance")).Text);
		}

		public override string GetProductCode(DataGridItem e)
		{
			return Convert.ToString(((TextBox) e.FindControl("tbxProductCode")).Text);
		}
	}
}