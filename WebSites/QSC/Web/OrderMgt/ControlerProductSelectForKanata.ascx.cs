namespace QSPFulfillment.OrderMgt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSP.WebControl;
	using QSPFulfillment.CustomerService;
	using QSPFulfillment.OrderMgt.UC;
	
	public class ControlerProductSelectForKanata : QSPFulfillment.CustomerService.CustomerServiceControlDataGrid
	{
		private const int CHECKBOX_COLUMN = 0;
		private const int TERM_COLUMN = 5;
		private const int QUANTITY_COLUMN = 7;
		private const int LANGUAGE_COLUMN = 10;
		private const int CATALOGNAME_COLUMN = 11;
		private const int PRICE_COLUMN = 15;
		private const int ENTERREDPRICELBL_COLUMN = 16;
		private const int TOTALPRICE_COLUMN = 17;
	

		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.TextBox tbxTitleCode;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.TextBox tbxTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblProductCode;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblMagInstance;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSearch;
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		private object dataSource;
		//private DataTable overrideReasonTable = null;
		private DataTable CatalogForKanataTable = null;
		protected System.Web.UI.WebControls.DropDownList ddlCatalogType;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox tbxTerm;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		//private DataView overrideReasonView = null;
		private DataView CatalogForKanataView = null;
		
		private int iTotal = 0;
		private double iTotalPrice =0;
		protected System.Web.UI.WebControls.CheckBox chkSelect;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label lblTerm;
		protected System.Web.UI.WebControls.Label lblQty2;
		protected System.Web.UI.WebControls.Label Label8;
		protected QSP.WebControl.TextBoxInteger tbxQuantity;
		protected System.Web.UI.WebControls.RangeValidator ravQuantity;
		protected System.Web.UI.WebControls.Label lblTotalCount;
		protected System.Web.UI.WebControls.Label lblTotalAmount;
		protected System.Web.UI.WebControls.Label lblLang;
		protected System.Web.UI.WebControls.Label lblCatalogName;
		protected System.Web.UI.WebControls.Label lblProductType;
		protected QSP.WebControl.TextBoxFloat tbxPrice;
		protected System.Web.UI.WebControls.Label lblTotalPrice;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		private double iItemTotal =0;

		protected bool bQtyFieldEditable = false;
		protected System.Web.UI.WebControls.Button btnReset;

		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				tbxTitleCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('"+btnSearch.UniqueID+"').click();return false;}} else {return true}; ");
				this.tbxTitle.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('"+btnSearch.UniqueID+"').click();return false;}} else {return true}; ");
				LoadDataDDLCatalog();
			}
		}

		private void ControlerMagazineTermSelect_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(ShowCheckBoxes) 
				{
					ResetCheckBoxes();
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				//Format Dollar amounts
				Label lblPrice = (Label) e.Item.FindControl("lblPrice");
				double price = Convert.ToDouble(lblPrice.Text);
				lblPrice.Text = price.ToString("F");

				TextBoxFloat tbxPrice = (TextBoxFloat) e.Item.FindControl("tbxPrice");
				double enterredPrice = Convert.ToDouble(tbxPrice.Text);
				tbxPrice.Text = enterredPrice.ToString("F");

				Label lblEnterredPrice = (Label) e.Item.FindControl("lblEnterredPrice");
				double enterredPriceLbl = Convert.ToDouble(lblEnterredPrice.Text);
				lblEnterredPrice.Text = enterredPriceLbl.ToString("c");

				//Total Count and price for all 
				iTotal += Convert.ToInt32(((System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbxQuantity")).Text);
				iTotalPrice += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("Label5")).Text)*
					Convert.ToInt32(((System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbxQuantity")).Text);
				//Total for each item
				iItemTotal = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("Label5")).Text)*
					Convert.ToInt32(((System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbxQuantity")).Text);
				
				((Label) e.Item.FindControl("Label5")).Text = iItemTotal.ToString("c");
			} 
			else if(e.Item.ItemType == ListItemType.Footer) 
			{
				((Label) e.Item.FindControl("Label6")).Text = iTotalPrice.ToString("c");
				((Label) e.Item.FindControl("Label8")).Text = iTotal.ToString();
					
			}
		}

		private void ddlCatalogType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try 
			{
				FilterData();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}		
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			btnSearch.Click +=new EventHandler(btnSearch_Click);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			base.OnInit(e,dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dtgMain.PreRender += new System.EventHandler(this.dtgMain_PreRender);
			this.dtgMain.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgMain_ItemDataBound);
			this.ddlCatalogType.SelectedIndexChanged += new System.EventHandler(this.ddlCatalogType_SelectedIndexChanged);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.List = null;
			this.Load += new System.EventHandler(this.Page_Load);

		}
	
		private void dtgMain_PreRender(object sender, System.EventArgs e)
		{
			IsDataBound = true;
		}

		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			try 
			{
				FilterData();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public new object DataSource 
		{
			get 
			{
				return dataSource;
			}
			set 
			{
				if(value is DataTable) 
				{
					base.DataSource = (DataTable) value;
				}

				dataSource = value;
			}
		}

		public DataGrid DataGrid 
		{
			get 
			{
				return dtgMain;
			}
		}

		protected DataView CatalogForKanata
		{
			
			get 
			{
				if(CatalogForKanataView == null) 
				{
					LoadDataDDLCatalog();
					
				}

				return CatalogForKanataView;
			}
		}

		public int ProductType
		{
			get 
			{
				if(ViewState["ProductType"] == null)
					return 46008;

				return Convert.ToInt32(ViewState["ProductType"]);
			}
			set 
			{
				this.ViewState["ProductType"] = value;
			}
		}
		
		public string DDLCatalogType
		{
			get
			{
				return ddlCatalogType.SelectedValue;
			}
			set
			{
				ddlCatalogType.SelectedValue = value;
			}
		}

		#region Control Visibility

		public bool ShowSearch 
		{
			get 
			{
				return divSearch.Visible;
			}
			set 
			{
				divSearch.Visible = value;
			}
		}

		public bool ShowCheckBoxes 
		{
			get 
			{
				return this.dtgMain.Columns[CHECKBOX_COLUMN].Visible;
			}
			set 
			{
				this.dtgMain.Columns[CHECKBOX_COLUMN].Visible = value;
			}
		}

		/*public bool ShowTerm 
		{
			get 
			{
				return this.dtgMain.Columns[TERM_COLUMN].Visible;
			}
			set 
			{
				this.dtgMain.Columns[TERM_COLUMN].Visible = value;
			}
		}

		public bool ShowLanguage 
		{
			get 
			{
				return this.dtgMain.Columns[LANGUAGE_COLUMN].Visible;
			}
			set 
			{
				this.dtgMain.Columns[LANGUAGE_COLUMN].Visible = value;
			}
		}*/

		public bool ShowCatalogName 
		{
			get 
			{
				return this.dtgMain.Columns[CATALOGNAME_COLUMN].Visible;
			}
			set 
			{
				this.dtgMain.Columns[CATALOGNAME_COLUMN].Visible = value;
			}
		}


		public bool ShowPriceInformation 
		{
			get 
			{
				return this.dtgMain.Columns[PRICE_COLUMN].Visible;
			}
			set 
			{
				this.dtgMain.Columns[PRICE_COLUMN].Visible = value;
			
			}
		}
		public bool ShowQuantitylabel 
		{
			get 
			{
				return this.dtgMain.Columns[QUANTITY_COLUMN-1].Visible;
			}
			set 
			{
				this.dtgMain.Columns[QUANTITY_COLUMN-1].Visible = value;
			
			}
		}
		public bool ShowQuantityTextBox
		{
			get 
			{
				return this.dtgMain.Columns[QUANTITY_COLUMN].Visible;
			}
			set 
			{
				this.dtgMain.Columns[QUANTITY_COLUMN].Visible = value;
			
			}
		}
		public bool ShowFooter
		{
			get 
			{
				return this.dtgMain.ShowFooter;
			}
			set 
			{
				this.dtgMain.ShowFooter = value;
				
			}
		}
		public bool ShowEnterredPriceLbl
		{
			get 
			{
				return this.dtgMain.Columns[ENTERREDPRICELBL_COLUMN].Visible;
			}
			set 
			{
				this.dtgMain.Columns[ENTERREDPRICELBL_COLUMN].Visible = value;
					
			}
		}

		public bool ShowTotalPrice
		{
			get 
			{
				return this.dtgMain.Columns[TOTALPRICE_COLUMN].Visible;
			}
			set 
			{
				this.dtgMain.Columns[TOTALPRICE_COLUMN].Visible = value;
				
			}
		}
		public bool AllowPaging 
		{
			get 
			{
				return this.dtgMain.AllowPaging;
			}
			set 
			{
				this.dtgMain.AllowPaging = value;
			}
		}

		public bool DDLCatalogTypeEditable
		{
			get
			{
				return ddlCatalogType.Enabled;
			}
			set
			{
				ddlCatalogType.Enabled = value;
			}
		}

		#endregion

		#region Fields

		private string ProductCodeSearch 
		{
			get 
			{
				string productCodeSearch = "";

				if(this.tbxTitleCode.Text != String.Empty) 
				{
					productCodeSearch = this.tbxTitleCode.Text;
				}

				return productCodeSearch;
			}
			set 
			{
				this.tbxTitleCode.Text = value;
			}
		}

		private string TitleSearch 
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

		/*msprivate int TermSearch 
		{
			get 
			{
				int termSearch = 0;

				try 
				{
					termSearch = Convert.ToInt32(this.tbxTerm.Text);
				} 
				catch { }

				return termSearch;
			}
			set 
			{
				this.tbxTerm.Text = value.ToString();
			}
		}*/

		#endregion

		public int CampaignID 
		{
			get 
			{
				int campaignID = 0;

				try 
				{
					campaignID = Convert.ToInt32(ViewState["CampaignID"]);
				} 
				catch { }

				return campaignID;
			}
			set 
			{
				ViewState["CampaignID"] = value;
			}
		}

		public bool IsDataBound 
		{
			get 
			{
				bool isDataBound = false;

				try 
				{
					isDataBound = Convert.ToBoolean(ViewState["IsDataBound"]);
				} 
				catch { }

				return isDataBound;
			}
			set 
			{
				ViewState["IsDataBound"] = value;
			}
		}

		public int IsFMAccount 
		{
			get 
			{
				int isFMaccount = 0;

				try 
				{
					isFMaccount = Convert.ToInt32(ViewState["IsFMAccount"]);
				} 
				catch { }

				return isFMaccount;
			}
			set 
			{
				ViewState["IsFMAccount"] = value;
			}
		}
		
		public bool QtyFieldEditable
		{
			get 
			{
				 bQtyFieldEditable = false;

				try 
				{
					bQtyFieldEditable = Convert.ToBoolean(ViewState["QtyFieldEditable"]);
				} 
				catch { }

				return bQtyFieldEditable;
			}
			set 
			{
				ViewState["bQtyFieldEditable"] = value;
			}
		}
		public ProductItemCollection Items 
		{
			get 
			{
				return GetItems(false);
			}
			set 
			{
				this.DataSource = value;
			}
		}

		public ProductItemCollection SelectedItems 
		{
			get 
			{
				return GetItems(true);
			}
		}

		public override void DataBind()
		{
			if(this.DataSource is ProductItemCollection) 
			{
				this.dtgMain.DataSource = this.DataSource;
				this.dtgMain.DataBind();
			} 
			else 
			{
				base.DataBind();
			}
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Product");

			try 
			{
				this.Page.KanataOEBusiness.KanataSelectItemsByCatalogAndAccountType((DataTable) DataSource, this.ddlCatalogType.SelectedValue.ToString(),this.ProductCodeSearch, this.CampaignID , this.IsFMAccount);
			}
			catch(QSPFulfillment.DataAccess.Common.ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}

		public void LoadDataDDLCatalog()
		{
			CatalogForKanataTable = new DataTable("CatalogForKanata");
			KanataOrderEntry p =(KanataOrderEntry)this.Page;
			p.BusKanataOE.ListCatalogForKanataOrder(CatalogForKanataTable,  this.CampaignID , this.IsFMAccount);
			CatalogForKanataView = new DataViewObject();
			CatalogForKanataView.Table = CatalogForKanataTable;
			this.ddlCatalogType.DataSource=CatalogForKanataView;
			ddlCatalogType.DataBind();
			ddlCatalogType.Items.Insert(0, new ListItem("", String.Empty));
		}

		public string FilterExpression 
		{
			get 
			{
				return this.dtgMain.FilterExpression;
			}
			set 
			{
				this.dtgMain.FilterExpression = value;
			}
		}

		private void FilterData() 
		{
			this.Page.NewSearch = true;
			FilterExpression ="";

			if (this.ProductCodeSearch !=String.Empty)
			{
				FilterExpression +="Product_Code like '%"+this.ProductCodeSearch.Replace("'", "''") +"%'";
			}

			if (this.TitleSearch != String.Empty) 
			{
				if(FilterExpression != String.Empty) 
				{
					FilterExpression += " and ";
				}

				FilterExpression += "Product_Sort_Name like '%" + this.TitleSearch.Replace("'", "''") +"%'";
			}

			if (this.ddlCatalogType.SelectedValue !=String.Empty)
			{
				if(FilterExpression != String.Empty) 
				{
					FilterExpression += " and ";
				}

				FilterExpression += "Catalog_Name like '%"+this.ddlCatalogType.SelectedValue+"%'";
			}

			this.dtgMain.SelectedIndex = -1;

			DataBind();
		}

		public void Reset() 
		{
			this.dtgMain.CurrentPageIndex = 0;
		}

		private ProductItemCollection GetItems(bool isChecked)
		{
			ProductItemCollection collection = new ProductItemCollection();

			foreach(DataGridItem item in this.dtgMain.Items) 
			{
				if(!isChecked || GetItemChecked(item))
				{
					collection.Add(QSPFulfillment.CustomerService.KanataProductItemFactory.Instance.GetProductItem(item));
				}
			}

			return collection;
		}

		private bool GetItemChecked(DataGridItem e) 
		{
			return ((System.Web.UI.WebControls.CheckBox) e.FindControl("chkSelect")).Checked;
		}

		public void ResetCheckBoxes() 
		{
			foreach(DataGridItem item in this.dtgMain.Items) 
			{
				((System.Web.UI.WebControls.CheckBox) item.FindControl("chkSelect")).Checked = false;
			}
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			tbxTitleCode.Text = "";
			tbxTitle.Text = "";
			tbxTerm.Text = "";
			ddlCatalogType.SelectedIndex = 0;

			try 
			{
				FilterData();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}		
		}

	}
}