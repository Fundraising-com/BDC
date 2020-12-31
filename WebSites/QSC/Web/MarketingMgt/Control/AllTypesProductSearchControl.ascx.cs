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
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.CustomerService;
	using QSP.WebControl;

	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class AllTypesProductSearchControl : ProductSearchControl
	{
		private const int COLUMN_DELETE = 10;
		private const int COLUMN_SELECT = 11;
		private const int COLUMN_EDIT = 12;
		private const int CODE_HEADER_PRODUCT_STATUS = 30600;

		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.TextBox tbxTitleCode;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.TextBox tbxTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSearch;
		protected System.Web.UI.WebControls.Label Label6;
		protected QSP.WebControl.DropDownListInteger ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlSeason;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label8;
		protected QSP.WebControl.DropDownListInteger ddlProductType;
		protected DataGridObject dtgMain;
		protected QSP.WebControl.DropDownListInteger ddlProductStatus;
		protected System.Web.UI.WebControls.Label Label5;
		protected QSPFulfillment.CustomerService.ControlerConfirmationPage ctrlControlerConfirmationPageDelete;

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

		private void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
		{
			try 
			{
				OnProductTypeChanged(new ProductTypeChangedArgs(ProductTypeSearch));
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
				return ProductType.None;
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
				return String.Empty;
			}
			set { }
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

		public override int PublisherSearch
		{
			get
			{
				return 0;
			}
			set { }
		}

		public override int FulfillmentHouseSearch
		{
			get
			{
				return 0;
			}
			set { }
		}

		#endregion

		#region Controls

		protected override System.Web.UI.Control SearchControl
		{
			get
			{
				return divSearch;
			}
		}

		protected override ControlerConfirmationPage ControlerConfirmationPageDelete
		{
			get
			{
				return this.ctrlControlerConfirmationPageDelete;
			}
		}

		#endregion

		#region Columns

		protected override DataGridColumn SelectColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_SELECT];
			}
		}

		protected override DataGridColumn EditColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_EDIT];
			}
		}

		protected override DataGridColumn DeleteColumn
		{
			get
			{
				return this.dtgMain.Columns[COLUMN_DELETE];
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
			SetValueDDLProductStatus();
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

		private void SetValueDDLProductType() 
		{
			if(this.ddlProductType.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusProductType.SelectAllProductTypes(table);

				this.ddlProductType.DataSource = table;
				this.ddlProductType.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlProductType.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlProductType.DataBind();
			}
		}

		private void SetValueDDLProductStatus() 
		{
			if(this.ddlProductStatus.Items.Count == 0)
			{
				DataTable table = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(table, CODE_HEADER_PRODUCT_STATUS);

				this.ddlProductStatus.DataSource = table;
				this.ddlProductStatus.DataTextField = CodeDetailTable.FLD_DESCRIPTION;
				this.ddlProductStatus.DataValueField = CodeDetailTable.FLD_INSTANCE;
				this.ddlProductStatus.DataBind();
			}
		}

		protected override int GetProductInstance(DataGridItem e)
		{
			return Convert.ToInt32(((Label) e.FindControl("lblProductInstance")).Text);
		}

		protected override string GetProductCode(DataGridItem e)
		{
			return ((Label) e.FindControl("lblProductCode")).Text;
		}

		protected override int GetYear(DataGridItem e)
		{
			return Convert.ToInt32(((Label) e.FindControl("lblYear")).Text);
		}

		protected override string GetSeason(DataGridItem e)
		{
			return ((Label) e.FindControl("lblSeason")).Text;
		}

		protected override ProductType GetProductType(DataGridItem e)
		{
			return (ProductType) Convert.ToInt32(((Label) e.FindControl("lblProductType")).Text);
		}
	}
}