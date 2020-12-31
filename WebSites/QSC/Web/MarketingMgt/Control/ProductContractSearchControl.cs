using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.MarketingMgt.Control
{
	/// <summary>
	/// Summary description for ProductContractSearchControl.
	/// </summary>
	public class ProductContractSearchControl : MarketingMgtControlDataGrid
	{
		private const string DELETE_CONTRACT_CONFIRMATION_MESSAGE = "Are you sure you want to delete this contract?";

		private QSPFulfillment.CustomerService.DataGridObject dtgMain;

		public event SelectProductEventHandler SelectProductClick;
		public event SelectProductContractEventHandler ProductContractDeleted;
		public event ProductTypeChangedEventHandler ProductTypeChanged;

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try 
			{
				if(e.CommandName == "ModifyContract")
				{
					ModifyContract(GetProductContractID(e.Item), GetProductType(e.Item));
				} 
				else if(e.CommandName == "DeleteContract") 
				{
                    ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(GetProductType(e.Item), this.Page.MessageManager);

                    if (productContractBusiness.ValidateDelete(GetProductContractID(e.Item)))
                    {
                        this.ProductContractIDToDelete = GetProductContractID(e.Item);
                        this.ProductTypeToDelete = GetProductType(e.Item);

                        DeleteContract(ProductContractIDToDelete, ProductTypeToDelete);
                    }

					//ShowDeleteContractConfirmation(GetProductContractID(e.Item), GetProductType(e.Item));
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
				if(this.ProductContractIDToDelete != null) 
				{
					DeleteContract(this.ProductContractIDToDelete, this.ProductTypeToDelete);
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

		public virtual bool ShowCheckBoxes
		{
			get 
			{
				bool showCheckBoxes = false;

				if(CheckBoxColumn != null) 
				{
					showCheckBoxes = CheckBoxColumn.Visible;
				}

				return showCheckBoxes;
			}
			set 
			{
				if(CheckBoxColumn != null) 
				{
					CheckBoxColumn.Visible = value;
				}
			}
		}

		public virtual bool ShowProductCodeEdit
		{
			get 
			{
				bool showProductCodeEdit = false;

				if(ProductCodeEditColumn != null) 
				{
					showProductCodeEdit = ProductCodeEditColumn.Visible;
				}

				return showProductCodeEdit;
			}
			set 
			{
				if(ProductCodeEditColumn != null) 
				{
					ProductCodeEditColumn.Visible = value;
				}
			}
		}

		public virtual bool ShowSelect 
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

		public virtual bool ShowDelete
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

		public virtual bool ShowModify
		{
			get 
			{
				bool showModify = false;

				if(ModifyColumn != null) 
				{
					showModify = ModifyColumn.Visible;
				}

				return showModify;
			}
			set 
			{
				if(ModifyColumn != null) 
				{
					ModifyColumn.Visible = value;
				}
			}
		}

		public virtual bool ShowYear 
		{
			get 
			{
				bool showYear = false;

				if(YearControl != null && YearColumn != null) 
				{
					showYear = (YearControl.Visible && YearColumn.Visible);
				}

				return showYear;
			}
			set 
			{
				if(YearControl != null && YearColumn != null) 
				{
					YearControl.Visible = value;
					YearColumn.Visible = value;
				}
			}
		}

		public virtual bool ShowSeason 
		{
			get 
			{
				bool showSeason = false;
				
				if(SeasonControl != null && SeasonColumn != null) 
				{
					showSeason = (SeasonControl.Visible && SeasonColumn.Visible);
				}

				return showSeason;
			}
			set 
			{
				if(SeasonControl != null && SeasonColumn != null) 
				{
					SeasonControl.Visible = value;
					SeasonColumn.Visible = value;
				}
			}
		}

		public virtual bool ShowProductStatus 
		{
			get 
			{
				bool showProductStatus = false;

				if(ProductStatusControl != null && ProductStatusColumn != null) 
				{
					showProductStatus = (ProductStatusControl.Visible && ProductStatusColumn.Visible);
				}

				return showProductStatus;
			}
			set 
			{
				if(ProductStatusControl != null && ProductStatusColumn != null) 
				{
					ProductStatusControl.Visible = value;
					ProductStatusColumn.Visible = value;
				}
			}
		}

		public virtual bool ShowCatalogInformation 
		{
			get 
			{
				bool showCatalogInformation = false;

				if(CatalogCodeControl != null && CatalogCodeColumn != null && CatalogNameColumn != null) 
				{
					showCatalogInformation = (CatalogCodeControl.Visible && CatalogCodeColumn.Visible && CatalogNameColumn.Visible);
				}

				return showCatalogInformation;
			}
			set 
			{
				if(CatalogCodeControl != null && CatalogCodeColumn != null && CatalogNameColumn != null) 
				{
					CatalogCodeControl.Visible = value;
					CatalogCodeColumn.Visible = value;
					CatalogNameColumn.Visible = value;
				}
			}
		}

		public virtual bool ProductTypeEnabled 
		{
			get 
			{
				bool productTypeEnabled = false;

				if(ProductTypeControl != null) 
				{
					productTypeEnabled = ProductTypeControl.Enabled;
				}

				return productTypeEnabled;
			}
			set 
			{
				if(ProductTypeControl != null) 
				{
					ProductTypeControl.Enabled = value;
				}
			}
		}

		public virtual int PageSize 
		{
			get 
			{
				int pageSize = 20;

				if(ViewState["PageSize"] != null) 
				{
					pageSize = Convert.ToInt32(ViewState["PageSize"]);
				}

				return pageSize;
			}
			set 
			{
				ViewState["PageSize"] = value;
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

		public virtual string OracleCodeSearch 
		{
			get 
			{
				throw new NotImplementedException("OracleCodeSearch");
			}
			set 
			{
				throw new NotImplementedException("OracleCodeSearch");
			}
		}

		public virtual int NumberOfIssuesSearch
		{
			get 
			{
				throw new NotImplementedException("NumberOfIssuesSearch");
			}
			set 
			{
				throw new NotImplementedException("NumberOfIssuesSearch");
			}
		}

		public virtual string CatalogCodeSearch 
		{
			get 
			{
				throw new NotImplementedException("CatalogCodeSearch");
			}
			set 
			{
				throw new NotImplementedException("CatalogCodeSearch");
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

		protected virtual System.Web.UI.Control YearControl 
		{
			get 
			{
				return null;
			}
		}

		protected virtual System.Web.UI.Control SeasonControl 
		{
			get 
			{
				return null;
			}
		}

		protected virtual System.Web.UI.Control ProductStatusControl 
		{
			get 
			{
				return null;
			}
		}

		protected virtual System.Web.UI.WebControls.WebControl ProductTypeControl 
		{
			get 
			{
				return null;
			}
		}

		protected virtual System.Web.UI.Control CatalogCodeControl 
		{
			get 
			{
				return null;
			}
		}

		#endregion

		#region Columns

		protected virtual DataGridColumn YearColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn SeasonColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn ProductStatusColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn CatalogCodeColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn CatalogNameColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn CheckBoxColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn ProductCodeEditColumn 
		{
			get 
			{
				return null;
			}
		}

		protected virtual DataGridColumn SelectColumn 
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

		protected virtual DataGridColumn ModifyColumn 
		{
			get 
			{
				return null;
			}
		}

		#endregion

		public int ProgramSectionID 
		{
			get 
			{
				int programSectionID = 0;

				if(ViewState["ProgramSectionID"] != null) 
				{
					programSectionID = Convert.ToInt32(ViewState["ProgramSectionID"]);
				}

				return programSectionID;
			}
			set 
			{
				ViewState["ProgramSectionID"] = value;
			}
		}

		private ProductContractID ProductContractIDToDelete 
		{
			get 
			{
				ProductContractID productContractIDToDelete = null;

				if(ViewState["ProductContractIDToDelete"] != null)
				{
					productContractIDToDelete = (ProductContractID) this.ViewState["ProductContractIDToDelete"];
				} 

				return productContractIDToDelete;
			}
			set 
			{
				this.ViewState["ProductContractIDToDelete"] = value;
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

			base.OnInit(e, Grid, LabelMessage);
		}

		protected virtual void OnSelectProductClicked(object sender, SelectProductClickedArgs e) 
		{
			if(SelectProductClick != null) 
			{
				SelectProductClick(sender, e);
			}
		}

		protected virtual void OnProductContractDeleted(object sender, SelectProductContractClickedArgs e) 
		{
			if(ProductContractDeleted != null) 
			{
				ProductContractDeleted(sender, e);
			}
		}

		protected virtual void OnProductTypeChanged(object sender, ProductTypeChangedArgs e) 
		{
			if(ProductTypeChanged != null) 
			{
				ProductTypeChanged(sender, e);
			}

			dtgMain.ItemCommand -= new DataGridCommandEventHandler(dtgMain_ItemCommand);
			ControlerConfirmationPageDelete.Confirmed -= new QSPFulfillment.CustomerService.ConfirmEventHandler(ControlerConfirmationPageDelete_Confirmed);
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
			this.dtgMain.PageSize = PageSize;
		}

		public override void DataBind()
		{
			DataBindInitialData();

			base.DataBind();
		}

		protected override void LoadData()
		{
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(ProductTypeSearch, this.Page.MessageManager);
			DataSource = new DataTable("ProductContracts");

			productContractBusiness.SelectAll(DataSource, this.Page.CatalogInfo.Type, this.Page.CatalogSectionInfo.Type, 0, ProductCodeSearch, RemitCodeSearch, ProductNameSearch, YearSearch, SeasonSearch, ProductStatusSearch, ProductTypeSearch, OracleCodeSearch, NumberOfIssuesSearch, CatalogCodeSearch, PublisherSearch, FulfillmentHouseSearch, ProgramSectionID, true);
		}

		public void CopyTo(ProductContractSearchControl control) 
		{
			control.ProductCodeSearch = this.ProductCodeSearch;
			control.RemitCodeSearch = this.RemitCodeSearch;
			control.ProductNameSearch = this.ProductNameSearch;
			control.YearSearch = this.YearSearch;
			control.SeasonSearch = this.SeasonSearch;
			control.ProductStatusSearch = this.ProductStatusSearch;
			control.OracleCodeSearch = this.OracleCodeSearch;
			control.NumberOfIssuesSearch = this.NumberOfIssuesSearch;
			control.CatalogCodeSearch = this.CatalogCodeSearch;
			control.PublisherSearch = this.PublisherSearch;
			control.FulfillmentHouseSearch = this.FulfillmentHouseSearch;
		}

		public DataGridItemCollection SelectedItems 
		{
			get 
			{
				ArrayList alSelected = new ArrayList();

				foreach(DataGridItem item in this.dtgMain.Items) 
				{
					if(((CheckBox) item.FindControl("chkSelect")).Checked) 
					{
						alSelected.Add(item);
					}
				}

				return new DataGridItemCollection(alSelected);
			}
		}

		protected virtual void ModifyContract(ProductContractID productContractID, ProductType productType) 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  OpenBig('ProductContractMaintenance.aspx?IsNewWindow=true&MagPriceInstanceGST=" + productContractID.MagPriceInstanceGST.ToString() + "&MagPriceInstanceHST=" + productContractID.MagPriceInstanceHST.ToString() + "&ProductType=" + ((int) productType).ToString() + "');\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("ModifyContract", script);
		}

		private void ShowDeleteContractConfirmation(ProductContractID productContractID, ProductType productType) 
		{
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(productType, this.Page.MessageManager);
			
			if(productContractBusiness.ValidateDelete(productContractID))
			{
				this.ProductContractIDToDelete = productContractID;
				this.ProductTypeToDelete = productType;

				this.ControlerConfirmationPageDelete.Message = DELETE_CONTRACT_CONFIRMATION_MESSAGE;
				this.ControlerConfirmationPageDelete.ShowConfirmationWindow();
			}
		}

		protected virtual void DeleteContract(ProductContractID productContractID, ProductType productType)
		{
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(productType, this.Page.MessageManager);
			SelectProductContractClickedArgs args = null;

			productContractBusiness.Delete(productContractID);

			args = new SelectProductContractClickedArgs(productContractID);

			OnProductContractDeleted(this, args);
		}

		public virtual ProductContractID GetProductContractID(DataGridItem e)
		{
			throw new NotImplementedException("GetProductContractID");
		}

		public virtual ProductType GetProductType(DataGridItem e) 
		{
			throw new NotImplementedException("GetProductType");
		}

		public virtual int GetProductInstance(DataGridItem e) 
		{
			throw new NotImplementedException("GetProductInstance");
		}

		public virtual string GetProductCode(DataGridItem e) 
		{
			throw new NotImplementedException("GetProductCode");
		}
	}
}
