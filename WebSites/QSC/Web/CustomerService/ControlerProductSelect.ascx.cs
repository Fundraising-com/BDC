namespace QSPFulfillment.CustomerService
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
    using Business.Objects;
    using System.Collections;
	
	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class ControlerProductSelect : CustomerServiceControlDataGrid
	{
		private const int CHECKBOX_COLUMN = 0;
		private const int TERM_COLUMN = 4;
		private const int QUANTITY_COLUMN = 5;
		private const int LANGUAGE_COLUMN = 7;
		private const int CATALOGNAME_COLUMN = 8;
		private const int PRICE_COLUMN = 10;
		private const int OVERRIDE_COLUMN = 11;
        private const int REPLACEMENTREASON_COLUMN = 12;

		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.TextBox tbxTitleCode;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.TextBox tbxTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox tbxTerm;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblProductCode;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblTerm;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblMagInstance;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSearch;
		protected DataGridObject dtgMain;
		private object dataSource;
		private DataTable overrideReasonTable = null;
		private DataView overrideReasonView = null;
		protected System.Web.UI.WebControls.Button btnReset;
        Business.Objects.CodeDetail cd = null;
        private int selectedProductReplacementReason = 0;
        private ArrayList arrSelectedProductReplacementReason = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
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
			this.List = null;

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

		protected DataView OverrideReasons 
		{
			get 
			{
				if(overrideReasonView == null) 
				{
					LoadDataDDLOverride();
				}

				return overrideReasonView;
			}
		}

		public int ProductType
		{
			get 
			{
				if(ViewState["ProductType"] == null)
					return 46001;

				return Convert.ToInt32(ViewState["ProductType"]);
			}
			set 
			{
				this.ViewState["ProductType"] = value;
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

		public bool ShowTerm 
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
		}

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
				this.dtgMain.Columns[QUANTITY_COLUMN].Visible = value;
				this.dtgMain.Columns[PRICE_COLUMN].Visible = value;
				//this.dtgMain.Columns[OVERRIDE_COLUMN].Visible = value;
			}
		}

        public bool ShowProductReplacementReason
        {
            get
            {
                return this.dtgMain.Columns[REPLACEMENTREASON_COLUMN].Visible;
            }
            set
            {
                this.dtgMain.Columns[REPLACEMENTREASON_COLUMN].Visible = value;
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

		#endregion

		#region Fields

		private string ProductCodeSearch 
		{
			get 
			{
				string productCodeSearch = "%";

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

		private int TermSearch 
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
		}

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
            cd = new Business.Objects.CodeDetail(CodeHeaderInstance.ProductReplacementReason);
			if(this.DataSource is ProductItemCollection) 
			{
                if (this.DataSource != null && (this.DataSource as ProductItemCollection).Count > 0)
                {
                    arrSelectedProductReplacementReason = new ArrayList();
                    foreach (ProductItem dataSource in this.DataSource as ProductItemCollection)
                    {
                        arrSelectedProductReplacementReason.Add(dataSource.ProductReplacementReason);
                    }                    
                }
                    this.dtgMain.DataSource = this.DataSource;
                    this.dtgMain.DataBind();
                
			}
			else 
			{
				base.DataBind ();
			}
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Product");

			try 
			{
				this.Page.BusProduct.SelectByCampaignTitleCode((DataTable) DataSource, this.ProductCodeSearch, CampaignID, this.ProductType, 0);
			}
			catch(QSPFulfillment.DataAccess.Common.ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadDataDDLOverride()
		{
			overrideReasonTable = new DataTable("OverrideReasons");
			this.Page.BusCodeDetail.SelectByCodeHeaderInstance(overrideReasonTable, 45000);
			
			overrideReasonView = new DataViewObject();
			overrideReasonView.Table = overrideReasonTable;
			overrideReasonView.Sort = CodeDetailTable.FLD_INSTANCE + " DESC";
		}

		private void FilterData() 
		{
			this.Page.NewSearch = true;
			this.dtgMain.FilterExpression ="";

			if(this.TitleSearch != String.Empty) 
			{
				this.dtgMain.FilterExpression ="Product_Sort_Name like '%" + this.TitleSearch.Replace("'", "''") +"%'";
			}
			
			if(this.TermSearch != 0)
			{
				if(this.dtgMain.FilterExpression != string.Empty)
				{
					this.dtgMain.FilterExpression += "	and ";
				}

				this.dtgMain.FilterExpression +=  "Term = " + this.TermSearch.ToString();
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
					collection.Add(ProductItemFactory.Instance.GetProductItem(item));
				}
			}

			return collection;
		}

		private bool GetItemChecked(DataGridItem e) 
		{
			return ((System.Web.UI.WebControls.CheckBox) e.FindControl("chkSelect")).Checked;
		}

		private void ResetCheckBoxes() 
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

			try 
			{
				FilterData();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}		
		}

      protected void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
      {
          if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
          {
              if (cd != null)
              { 
                  DropDownList list = (DropDownList)e.Item.FindControl("ddlProductReplacementReason");
                  list.DataSource = cd.dataSet.Tables[0];
                  list.DataTextField = "Description";
                  list.DataValueField = "Instance";
                  if (arrSelectedProductReplacementReason != null && arrSelectedProductReplacementReason.Count > 0 && Convert.ToInt32(arrSelectedProductReplacementReason[0].ToString()) > 0)
                  {
                      list.SelectedValue = arrSelectedProductReplacementReason[selectedProductReplacementReason].ToString();
                      selectedProductReplacementReason = selectedProductReplacementReason +1;
                  }
                  list.DataBind();
              }
          }
      }

	}
}