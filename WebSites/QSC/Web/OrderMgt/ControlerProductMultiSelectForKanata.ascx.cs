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
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.CustomerService;
	
	public delegate void SelectProductItemsEventHandler(object sender, OrderHeader orderHeader);
	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public partial class ControlerProductMultiSelectForKanata : QSPFulfillment.CustomerService.CustomerServiceControl
	{
		protected QSPFulfillment.OrderMgt.ControlerProductSelectForKanata ctrlControlerProductSelect;
		protected QSPFulfillment.OrderMgt.ControlerProductSelectForKanata ctrlControlerProductDisplay;
		protected ProductItemCollectionFilter productItemCollectionFilter;

		public event EventHandler StepBackClicked;
		public event SelectProductItemsEventHandler ProductItemsSelected;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.ctrlControlerProductDisplay.QtyFieldEditable= true;
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlControlerProductSelect.DataBound += new EventHandler(ctrlControlerProductSelect_DataBound);
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

		protected void btnBack_Click(object sender, System.EventArgs e)
		{
			try 
			{
				this.ctrlControlerProductSelect.Reset();

				if(StepBackClicked != null) 
				{
					StepBackClicked(this, EventArgs.Empty);
				}
			}
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnSelectList_Click(object sender, System.EventArgs e)
		{
			try 
			{
				SaveCurrentSelection();

				if(ProductItemsSelected != null) 
				{
					ProductItemsSelected(this, this.CurrentOrderHeader);
				}
				
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
			
		}

		protected void btnAddToList_Click(object sender, System.EventArgs e)
		{
			try 
			{
				AddItemsToList();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnRemoveFromList_Click(object sender, System.EventArgs e)
		{
			try 
			{
				RemoveItemsFromList();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnClearList_Click(object sender, System.EventArgs e)
		{
			try 
			{
				ClearList();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlControlerProductSelect_DataBound(object sender, EventArgs e)
		{
			SetVisibleProductSelect();
		}

		public int ProductType 
		{
			get 
			{
				int productType = 0;

				try 
				{
					productType = Convert.ToInt32(this.ViewState["ProductType"]);
				} 
				catch { }

				return productType;
			}
			set 
			{
				this.ViewState["ProductType"] = value;
			}
		}

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

		public bool BtnAddToList 
		{
			get 
			{
				return btnAddToList.Visible;
			}
			set 
			{
				btnAddToList.Visible = value;
			}
		}

		public bool BtnRemoveFromList 
		{
			get 
			{
				return btnRemoveFromList.Visible;
			}
			set 
			{
				btnRemoveFromList.Visible = value;
			}
		}

		public bool BtnClearList 
		{
			get 
			{
				return btnClearList.Visible;
			}
			set 
			{
				btnClearList.Visible = value;
			}
		}

		public bool BtnBackTop 
		{
			get 
			{
				return btnBackTop.Visible;
			}
			set 
			{
				btnBackTop.Visible = value;
			}
		}

		public bool BtnSelectListTop
		{
			get 
			{
				return btnSelectListTop.Visible;
			}
			set 
			{
				btnSelectListTop.Visible = value;
			}
		}

		public bool BtnBackBottom 
		{
			get 
			{
				return btnBackBottom.Visible;
			}
			set 
			{
				btnBackBottom.Visible = value;
			}
		}

		public bool BtnSelectListBottom
		{
			get 
			{
				return btnSelectListBottom.Visible;
			}
			set 
			{
				btnSelectListBottom.Visible = value;
			}
		}

		public OrderHeader CurrentOrderHeader 
		{
			get 
			{
				if(this.ViewState["CurrentOrderHeader"] == null) 
				{
					this.ViewState["CurrentOrderHeader"] = new OrderHeader();
				}

				return (OrderHeader) this.ViewState["CurrentOrderHeader"];
			}
			set 
			{
				this.ViewState["CurrentOrderHeader"] = value;
			}
		}

		public bool ProductDisplayQtyFieldEditable
		{
			get 
			{
				return ctrlControlerProductDisplay.QtyFieldEditable;
			}
			set 
			{
				ctrlControlerProductDisplay.QtyFieldEditable = value;
			}
		}
		
		public bool ProductDisplayShowCheckBoxes
		{
			get 
			{
				return ctrlControlerProductDisplay.ShowCheckBoxes;
			}
			set 
			{
				ctrlControlerProductDisplay.ShowCheckBoxes = value;
			}
		}

		public string DDLCatalogType
		{
			get
			{
				return this.ctrlControlerProductSelect.DDLCatalogType;
			}
			set
			{
				this.ctrlControlerProductSelect.DDLCatalogType = value;
			}
		}

		public override void DataBind()
		{
			this.ctrlControlerProductSelect.CampaignID = CampaignID;
			this.ctrlControlerProductSelect.IsFMAccount= IsFMAccount;
			this.ctrlControlerProductSelect.ProductType = this.ProductType;
			this.ctrlControlerProductSelect.LoadDataDDLCatalog();
			this.ctrlControlerProductSelect.DataBind();
			if (this.ctrlControlerProductDisplay.Items.Count==0)
			{
				this.ctrlControlerProductSelect.ShowFooter=false;
			}
			
			SetVisibleProductSelect();

			this.ctrlControlerProductDisplay.DataSource = ProductItemCollectionFiltered;
			this.ctrlControlerProductDisplay.DataBind();
			SetVisibleDisplayList();
			SetEditableCatalogSelection();
		}

		private void AddItemsToList() 
		{
			SaveCurrentSelection();
			
			foreach(ProductItem item in this.ctrlControlerProductSelect.SelectedItems) 
			{
				int index = this.CurrentOrderHeader.ProductItems.IndexOf(item);
				if (index >= 0) //update existing item
				{
					if (this.CurrentOrderHeader.ProductItems[index].IsDeleted == 1)
						item.IsDeleted = 0;
					item.TransID = this.CurrentOrderHeader.ProductItems[index].TransID;
					this.CurrentOrderHeader.ProductItems.Update(index, item);
				}
				else //add item
				{
					this.CurrentOrderHeader.ProductItems.Add(item, -1); //ignores in determining if needed to be deleted in DB
				}
			}

			this.ctrlControlerProductSelect.ResetCheckBoxes();

			this.ctrlControlerProductDisplay.DataSource = ProductItemCollectionFiltered;
			this.ctrlControlerProductDisplay.DataBind();

			SetVisibleDisplayList();
			SetEditableCatalogSelection();
		}
		public ProductItemCollection ProductItemCollectionFiltered
		{
			get
			{
				productItemCollectionFilter = new ProductItemCollectionFilter();
				return productItemCollectionFilter.Filter(this.CurrentOrderHeader.ProductItems, 1);
			}
		}
		public void InitProductSelect()
		{
			if (!QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM
				&& QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999")
			{
				this.ctrlControlerProductDisplay.ShowPriceInformation=true;
			}
			else
			{
				this.ctrlControlerProductDisplay.ShowPriceInformation=false;
			}

			this.ctrlControlerProductDisplay.ShowQuantityTextBox=true;
			this.ctrlControlerProductDisplay.ShowQuantitylabel=false;
			this.ctrlControlerProductDisplay.ShowSearch=false;
			this.ctrlControlerProductDisplay.ShowFooter=false;
			this.ctrlControlerProductDisplay.ShowEnterredPriceLbl = false;
			this.ctrlControlerProductDisplay.AllowPaging = false;

			SetVisibleDisplayList();
			SetEditableCatalogSelection();
		}
		private void RemoveItemsFromList() 
		{
			SaveCurrentSelection();

			foreach(ProductItem item in this.ctrlControlerProductDisplay.SelectedItems) 
			{
				if (item.IsDeleted == -1)
				{
					this.CurrentOrderHeader.ProductItems.Remove(item);
				}
				else // item exists in original order, so must keep track to soft delete in DB
				{
					int index = this.CurrentOrderHeader.ProductItems.IndexOf(item);
					this.CurrentOrderHeader.ProductItems[index].IsDeleted = 1;
				}
			}

			this.ctrlControlerProductDisplay.DataSource = ProductItemCollectionFiltered;
			this.ctrlControlerProductDisplay.DataBind();
			SetVisibleDisplayList();
			SetEditableCatalogSelection();
		}

		private void ClearList() 
		{
			SaveCurrentSelection();

			foreach(ProductItem item in this.ctrlControlerProductDisplay.Items) 
			{
				if (item.IsDeleted == -1)
				{
					this.CurrentOrderHeader.ProductItems.Remove(item);
				}
				else // item exists in original order, so must keep track to soft delete in DB
				{
					int index = this.CurrentOrderHeader.ProductItems.IndexOf(item);
					this.CurrentOrderHeader.ProductItems[index].IsDeleted = 1;
				}
			}

			this.ctrlControlerProductDisplay.DataSource = ProductItemCollectionFiltered;
			this.ctrlControlerProductDisplay.DataBind();
			SetVisibleDisplayList();
			SetEditableCatalogSelection();
		}

		private void SetVisibleProductSelect() 
		{
			bool isVisible = false;
			DataView view = this.ctrlControlerProductSelect.DataGrid.DataSource as DataView;

			if(view != null) 
			{
				isVisible = (view.Count != 0);
			}

			this.btnAddToList.Visible = isVisible;
		}

		private void SetVisibleDisplayList() 
		{
			bool isVisible = (this.ProductItemCollectionFiltered.Count != 0) ;

			this.divSelectedList.Visible = isVisible;
			this.btnSelectListTop.Enabled = isVisible;
			this.btnSelectListBottom.Enabled = isVisible;
		}

		private void SetEditableCatalogSelection()
		{
			//prevent selecting another catalog once product is chosen
			bool isEditable = (this.ProductItemCollectionFiltered.Count == 0) ;
			this.ctrlControlerProductSelect.DDLCatalogTypeEditable = isEditable;
		}

		private void SaveCurrentSelection()
		{
			//Save the items that the user has modified so that on databind it doesn't wipe out info
			OrderHeader tempOrderHeader = new OrderHeader(); 
			tempOrderHeader.ProductItems = this.ctrlControlerProductDisplay.Items;
			
			//Save the items that are marked to be deleted in the DB
			foreach (ProductItem item in this.CurrentOrderHeader.ProductItems)
			{
				if (item.IsDeleted == 1)
				{
					tempOrderHeader.ProductItems.Add(item, 1);
				}
			}

			this.CurrentOrderHeader.ProductItems = tempOrderHeader.ProductItems;
		}
	}
}