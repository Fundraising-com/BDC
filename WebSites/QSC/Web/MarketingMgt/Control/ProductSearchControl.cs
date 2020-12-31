using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.MarketingMgt.Control
{
	public delegate void SelectProductEventHandler(object sender, SelectProductClickedArgs e);

	/// <summary>
	/// Summary description for ProductSearchControl.
	/// </summary>
	public class ProductSearchControl : MarketingMgtControlDataGrid
	{
		private const string DELETE_PRODUCT_CONFIRMATION_MESSAGE = "Are you sure you want to delete this product and all its associated contracts?";

		private QSPFulfillment.CustomerService.DataGridObject dtgMain;

		public event SelectProductEventHandler SelectProductClick;
		public event SelectProductEventHandler EditProductClick;
		public event SelectProductEventHandler DeleteProductClick;
		public event ProductTypeChangedEventHandler ProductTypeChanged;

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try 
			{
				if(e.CommandName == "Select")
				{
					SelectProductClickedArgs args = new SelectProductClickedArgs(new Product(GetProductInstance(e.Item), GetProductCode(e.Item), GetYear(e.Item), GetSeason(e.Item), GetProductType(e.Item)));
				
					OnSelectProductClicked(source, args);
				} 
				else if(e.CommandName == "Edit")
				{
					SelectProductClickedArgs args = new SelectProductClickedArgs(new Product(GetProductInstance(e.Item), GetProductCode(e.Item), GetYear(e.Item), GetSeason(e.Item), GetProductType(e.Item)));
				
					OnEditProductClicked(source, args);
				} 
				else if(e.CommandName == "Delete") 
				{
                    if (this.Page.BusProduct.ValidateDelete(GetProductInstance(e.Item)))
                    {
                        this.ProductInstanceToDelete = GetProductInstance(e.Item);
                        this.ProductTypeToDelete = GetProductType(e.Item);

                        DeleteProduct(ProductInstanceToDelete, ProductTypeToDelete);
                    }

					//ShowDeleteProductConfirmation(GetProductInstance(e.Item), GetProductType(e.Item));
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ControlerConfirmationPageDelete_Confirmed(object sender, EventArgs e)
		{
			try 
			{
				if(ProductInstanceToDelete != 0) 
				{
					DeleteProduct(ProductInstanceToDelete, ProductTypeToDelete);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

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

		#region Appearance

		public virtual bool ShowSearch 
		{
			get 
			{
				bool showSearch = false;

				if(SearchControl != null) 
				{
					showSearch = SearchControl.Visible;
				}

				return showSearch;
			}
			set 
			{
				if(SearchControl != null) 
				{
					SearchControl.Visible = value;
				}
			}
		}

		public bool ShowSelect
		{
			get 
			{
				bool showSelect = false;

				if(SelectColumn != null) 
				{
					showSelect = SelectColumn.Visible;
				}

				return showSelect;
			}
			set 
			{
				if(SelectColumn != null) 
				{
					SelectColumn.Visible = value;
				}
			}
		}

		public bool ShowEdit
		{
			get 
			{
				bool showEdit = false;

				if(EditColumn != null) 
				{
					showEdit = EditColumn.Visible;
				}

				return showEdit;
			}
			set 
			{
				if(EditColumn != null) 
				{
					EditColumn.Visible = value;
				}
			}
		}

		public bool ShowDelete 
		{
			get 
			{
				bool showDelete = false;

				if(DeleteColumn != null) 
				{
					showDelete = DeleteColumn.Visible;
				}

				return showDelete;
			}
			set 
			{
				if(DeleteColumn != null) 
				{
					DeleteColumn.Visible = value;
				}
			}
		}

		#endregion

		#region Fields

		public virtual string ProductCodeSearch 
		{
			get 
			{
				throw new NotImplementedException("ProductCodeSearch");
			}
			set 
			{
				throw new NotImplementedException("ProductCodeSearch");
			}
		}

		public virtual string RemitCodeSearch 
		{
			get 
			{
				throw new NotImplementedException("RemitCodeSearch");
			}
			set 
			{
				throw new NotImplementedException("RemitCodeSearch");
			}
		}

		public virtual string ProductNameSearch 
		{
			get 
			{
				throw new NotImplementedException("ProductNameSearch");
			}
			set 
			{
				throw new NotImplementedException("ProductNameSearch");
			}
		}

		public virtual int YearSearch 
		{
			get 
			{
				throw new NotImplementedException("YearSearch");
			}
			set 
			{
				throw new NotImplementedException("YearSearch");
			}
		}

		public virtual string SeasonSearch 
		{
			get 
			{
				throw new NotImplementedException("SeasonSearch");
			}
			set 
			{
				throw new NotImplementedException("SeasonSearch");
			}
		}

		public virtual int ProductStatusSearch 
		{
			get 
			{
				throw new NotImplementedException("ProductStatusSearch");
			}
			set 
			{
				throw new NotImplementedException("ProductStatusSearch");
			}
		}

		public virtual ProductType ProductTypeSearch 
		{
			get 
			{
				throw new NotImplementedException("ProductTypeSearch");
			}
			set 
			{
				throw new NotImplementedException("ProductTypeSearch");
			}
		}

		public virtual int PublisherSearch 
		{
			get 
			{
				throw new NotImplementedException("PublisherSearch");
			}
			set 
			{
				throw new NotImplementedException("PublisherSearch");
			}
		}

		public virtual int FulfillmentHouseSearch 
		{
			get 
			{
				throw new NotImplementedException("FulfillmentHouseSearch");
			}
			set 
			{
				throw new NotImplementedException("FulfillmentHouseSearch");
			}
		}

		#endregion

		#region Controls

		protected virtual System.Web.UI.Control SearchControl 
		{
			get 
			{
				return null;
			}
		}

        protected virtual QSPFulfillment.CustomerService.ControlerConfirmationPage ControlerConfirmationPageDelete 
		{
			get 
			{
				return null;
			}
		}

		#endregion

		#region Columns

		protected virtual DataGridColumn SelectColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn EditColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn DeleteColumn 
		{
			get 
			{
				return null;
			}
		}

		#endregion

		private int ProductInstanceToDelete 
		{
			get 
			{
				int productInstanceToDelete = 0;

				if(ViewState["ProductInstanceToDelete"] != null)
				{
					productInstanceToDelete = Convert.ToInt32(this.ViewState["ProductInstanceToDelete"]);
				} 

				return productInstanceToDelete;
			}
			set 
			{
				this.ViewState["ProductInstanceToDelete"] = value;
			}
		}

		private ProductType ProductTypeToDelete 
		{
			get 
			{
				ProductType productTypeToDelete = ProductType.Magazine;

				if(ViewState["ProductTypeToDelete"] != null) 
				{
					productTypeToDelete = (ProductType) ViewState["ProductTypeToDelete"];
				}

				return productTypeToDelete;
			}
			set 
			{
				ViewState["ProductTypeToDelete"] = value;
			}
		}

		#region Events

		protected override void OnInit(EventArgs e, QSPFulfillment.CustomerService.DataGridObject Grid, System.Web.UI.WebControls.Label LabelMessage)
		{
			dtgMain = Grid;

			dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);

			if(ControlerConfirmationPageDelete != null) 
			{
				ControlerConfirmationPageDelete.Confirmed += new QSPFulfillment.CustomerService.ConfirmEventHandler(ControlerConfirmationPageDelete_Confirmed);
			}

			base.OnInit (e, Grid, LabelMessage);
		}

		protected virtual void OnSelectProductClicked(object sender, SelectProductClickedArgs e) 
		{
			if(SelectProductClick != null) 
			{
				SelectProductClick(sender, e);
			}
		}

		protected virtual void OnEditProductClicked(object sender, SelectProductClickedArgs e) 
		{
			if(EditProductClick != null) 
			{
				EditProductClick(sender, e);
			}
		}

		protected virtual void OnDeleteProductClicked(object sender, SelectProductClickedArgs e) 
		{
			if(DeleteProductClick != null) 
			{
				DeleteProductClick(sender, e);
			}
		}

		protected virtual void OnProductTypeChanged(ProductTypeChangedArgs e) 
		{
			if(ProductTypeChanged != null) 
			{
				ProductTypeChanged(this, e);
			}
		}

		#endregion

		protected void Search() 
		{
			this.Page.NewSearch = true;

			this.dtgMain.SelectedIndex = -1;

			DataBind();
		}

		public virtual void DataBindInitialData() 
		{
			ProductTypeSearch = ProductType;
		}

		public override void DataBind()
		{
			DataBindInitialData();

			base.DataBind();
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Product");

			this.Page.BusProduct.SelectAll(DataSource, ProductCodeSearch, RemitCodeSearch, ProductNameSearch, YearSearch, SeasonSearch, ProductStatusSearch, ProductTypeSearch, PublisherSearch, FulfillmentHouseSearch);
		}

		public void CopyTo(ProductSearchControl control) 
		{
			control.ProductCodeSearch = this.ProductCodeSearch;
			control.RemitCodeSearch = this.RemitCodeSearch;
			control.ProductNameSearch = this.ProductNameSearch;
			control.YearSearch = this.YearSearch;
			control.SeasonSearch = this.SeasonSearch;
			control.ProductStatusSearch = this.ProductStatusSearch;
			control.PublisherSearch = this.PublisherSearch;
			control.FulfillmentHouseSearch = this.FulfillmentHouseSearch;
		}

		private void ShowDeleteProductConfirmation(int productInstance, ProductType productType) 
		{
			if(this.Page.BusProduct.ValidateDelete(productInstance))
			{
				this.ProductInstanceToDelete = productInstance;
				this.ProductTypeToDelete = productType;

				this.ControlerConfirmationPageDelete.Message = DELETE_PRODUCT_CONFIRMATION_MESSAGE;
				this.ControlerConfirmationPageDelete.ShowConfirmationWindow();
			}
		}

		protected virtual void DeleteProduct(int productInstance, ProductType productType)
		{
			SelectProductClickedArgs args = null;
			Product product = null;

			this.Page.BusProduct.Delete(productInstance, productType);

			product = new Product();
			product.ProductInstance = productInstance;
			args = new SelectProductClickedArgs(product);

			OnDeleteProductClicked(this, args);
		}

		protected virtual int GetProductInstance(DataGridItem e)
		{
			throw new NotImplementedException("GetProductInstance");
		}

		protected virtual string GetProductCode(DataGridItem e) 
		{
			throw new NotImplementedException("GetProductCode");
		}

		protected virtual int GetYear(DataGridItem e) 
		{
			throw new NotImplementedException("GetYear");
		}

		protected virtual string GetSeason(DataGridItem e) 
		{
			throw new NotImplementedException("GetSeason");
		}

		protected virtual ProductType GetProductType(DataGridItem e) 
		{
			throw new NotImplementedException("GetProductType");
		}
	}
}
