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
	using QSPFulfillment.DataAccess.Common.ActionObject;
	
	public delegate void SelectProductItemsEventHandler(object sender, OrderHeader orderHeader);
	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public partial class ControlerProductMultiSelect : CustomerServiceControl
	{
		protected QSPFulfillment.CustomerService.ControlerProductSelect ctrlControlerProductSelect;
		protected QSPFulfillment.CustomerService.ControlerProductSelect ctrlControlerProductDisplay;

		public event EventHandler StepBackClicked;
		public event SelectProductItemsEventHandler ProductItemsSelected;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
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
				this.ctrlControlerProductSelect.Reset();

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

		public override void DataBind()
		{
			this.ctrlControlerProductSelect.CampaignID = CampaignID;
			this.ctrlControlerProductSelect.ProductType = this.ProductType;
			this.ctrlControlerProductSelect.DataBind();
			SetVisibleProductSelect();

			this.ctrlControlerProductDisplay.DataSource = this.CurrentOrderHeader.ProductItems;
			this.ctrlControlerProductDisplay.DataBind();
			SetVisibleDisplayList();
		}

		private void AddItemsToList() 
		{
			foreach(ProductItem item in this.ctrlControlerProductSelect.SelectedItems) 
			{
				if(!this.CurrentOrderHeader.ProductItems.Contains(item)) 
				{
					this.CurrentOrderHeader.ProductItems.Add(item);
				}
			}

			this.ctrlControlerProductDisplay.DataSource = this.CurrentOrderHeader.ProductItems;
			this.ctrlControlerProductDisplay.DataBind();
			SetVisibleDisplayList();
		}

		private void RemoveItemsFromList() 
		{
			foreach(ProductItem item in this.ctrlControlerProductDisplay.SelectedItems) 
			{
				if(this.CurrentOrderHeader.ProductItems.Contains(item))
				{
					this.CurrentOrderHeader.ProductItems.Remove(item);
				}
			}

			this.ctrlControlerProductDisplay.DataSource = this.CurrentOrderHeader.ProductItems;
			this.ctrlControlerProductDisplay.DataBind();
			SetVisibleDisplayList();
		}

		private void ClearList() 
		{
			this.CurrentOrderHeader.ProductItems.Clear();
			this.ctrlControlerProductDisplay.DataSource = this.CurrentOrderHeader.ProductItems;
			this.ctrlControlerProductDisplay.DataBind();
			SetVisibleDisplayList();
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
			bool isVisible = (this.CurrentOrderHeader.ProductItems.Count != 0) ;

			this.divSelectedList.Visible = isVisible;
			this.btnSelectList.Enabled = isVisible;
		}
	}
}