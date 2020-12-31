using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Common;

namespace QSPFulfillment.OrderMgt
{
	/// <summary>
	/// Summary description for OrderHistory2.
	/// </summary>
	public class OrderHistory : OrderMgtPage
	{
		private const int FORCE_CLOSE_ORDER_COLUMN = 9;
		private const int APPROVE_ORDER_COLUMN = 10;
		private const int DISAPPROVE_ORDER_COLUMN = 11;
		
		protected System.Web.UI.WebControls.DropDownList fWareHouse;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox fOrderID;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox fCampaignID;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox fAccountID;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Panel Panel2;
		protected System.Web.UI.WebControls.Panel Panel3;
		protected System.Web.UI.WebControls.Label Label7;
		protected QSPFulfillment.CommonWeb.UC.DateEntry	fToDate;
		protected System.Web.UI.WebControls.Button Button1;
		protected QSPFulfillment.CommonWeb.UC.DateEntry	fFromDate;
		protected QSPFulfillment.OrderMgt.UC.OrderStatus ucOHOrderStatus;
		protected QSPFulfillment.OrderMgt.UC.OrderQualifier ucOHOrderQualifier;
		protected System.Web.UI.WebControls.Label LabelQualifier;
		protected System.Web.UI.WebControls.Panel PanelQualifier;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			if (!IsPostBack)
			{
				try 
				{
					populate_list_items();
					fFromDate.Date = Convert.ToDateTime("02/01/2007");
					fToDate.Date = DateTime.Now;
					ucOHOrderStatus.SelectedValue = 0;
					ucOHOrderQualifier.SelectedValue = 0;
				} 
				catch(Exception ex) 
				{
					DataAccess.Common.ApplicationError.ManageError(ex);

					this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
				}
			}
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged_1);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DataGrid1_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			OrderHistoryButtonForceCloseOrder btnForceCloseOrder;
			OrderHistoryButtonApproveDisapproveOrder btnApproveOrder;
			OrderHistoryButtonApproveDisapproveOrder btnDisapproveOrder;

			if(e.Item.ItemType == ListItemType.Item ||
				e.Item.ItemType == ListItemType.AlternatingItem ||
				e.Item.ItemType == ListItemType.EditItem ||
				e.Item.ItemType == ListItemType.SelectedItem) 
			{
				btnForceCloseOrder = (OrderHistoryButtonForceCloseOrder) e.Item.FindControl("btnForceCloseOrder");
				btnApproveOrder = (OrderHistoryButtonApproveDisapproveOrder) e.Item.FindControl("btnApproveOrder");
				btnDisapproveOrder = (OrderHistoryButtonApproveDisapproveOrder) e.Item.FindControl("btnDisapproveOrder");

				if(btnForceCloseOrder != null) 
				{
					btnForceCloseOrder.OrderClosed += new EventHandler(btnForceCloseOrder_OrderClosed);
				}

				if(btnApproveOrder != null) 
				{
					btnApproveOrder.OrderApproved += new EventHandler(btnApproveOrder_OrderApproved);
				}

				if(btnDisapproveOrder != null) 
				{
					btnDisapproveOrder.OrderDisapproved += new EventHandler(btnDisapproveOrder_OrderDisapproved);
				}

			}
		}

		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			OrderHistoryButtonForceCloseOrder btnForceCloseOrder;
			OrderHistoryButtonApproveDisapproveOrder btnApproveOrder;
			OrderHistoryButtonApproveDisapproveOrder btnDisapproveOrder;

			try 
			{
				if(e.Item.ItemType == ListItemType.Item ||
					e.Item.ItemType == ListItemType.AlternatingItem ||
					e.Item.ItemType == ListItemType.EditItem ||
					e.Item.ItemType == ListItemType.SelectedItem) 
				{
					btnForceCloseOrder = (OrderHistoryButtonForceCloseOrder) e.Item.FindControl("btnForceCloseOrder");
					btnApproveOrder = (OrderHistoryButtonApproveDisapproveOrder) e.Item.FindControl("btnApproveOrder");
					btnDisapproveOrder = (OrderHistoryButtonApproveDisapproveOrder) e.Item.FindControl("btnDisapproveOrder");

					btnForceCloseOrder.DataSource = (DataTable) DataGrid1.DataSource;
					btnForceCloseOrder.DataBind();

					btnApproveOrder.DataSource = (DataTable) DataGrid1.DataSource;
					btnApproveOrder.DataBind();

					btnDisapproveOrder.DataSource = (DataTable) DataGrid1.DataSource;
					btnDisapproveOrder.DataBind();
				}
			} 
			catch(MessageException ex) 
			{
				this.SetPageError(ex);
			}
		}

		private void btnForceCloseOrder_OrderClosed(object sender, EventArgs e)
		{
			try 
			{
				PopulateDG();
			}
			catch(Exception ex) 
			{
				DataAccess.Common.ApplicationError.ManageError(ex);

				this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
			}
		}

		private void btnApproveOrder_OrderApproved(object sender, EventArgs e)
		{
			try 
			{
				PopulateDG();
			}
			catch(Exception ex) 
			{
				DataAccess.Common.ApplicationError.ManageError(ex);

				this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
			}
		}

		private void btnDisapproveOrder_OrderDisapproved(object sender, EventArgs e)
		{
			try 
			{
				PopulateDG();
			}
			catch(Exception ex) 
			{
				DataAccess.Common.ApplicationError.ManageError(ex);

				this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
			}
		}

		public bool ShowForceCloseOrder 
		{
			get 
			{
				return this.DataGrid1.Columns[FORCE_CLOSE_ORDER_COLUMN].Visible;
			}
			set 
			{
				this.DataGrid1.Columns[FORCE_CLOSE_ORDER_COLUMN].Visible = value;
			}
		}

		public bool ShowApproveOrder 
		{
			get 
			{
				return this.DataGrid1.Columns[APPROVE_ORDER_COLUMN].Visible;
			}
			set 
			{
				this.DataGrid1.Columns[APPROVE_ORDER_COLUMN].Visible = value;
			}
		}

		public bool ShowDisapproveOrder 
		{
			get 
			{
				return this.DataGrid1.Columns[DISAPPROVE_ORDER_COLUMN].Visible;
			}
			set 
			{
				this.DataGrid1.Columns[DISAPPROVE_ORDER_COLUMN].Visible = value;
			}
		}

		private void populate_list_items()
		{
		
			this.ucOHOrderStatus.Bind();//user control
			this.ucOHOrderQualifier.Bind();//user control
		}

		private void PopulateDG()
		{
			int vStatus;
			int vQualifier;
			string vDate1;
			string vDate2;


			 vStatus = this.ucOHOrderStatus.SelectedValue;
			 vQualifier = this.ucOHOrderQualifier.SelectedValue;
			 vDate1  = fFromDate.Value;
			 vDate2  = fToDate.Value;

			if (fOrderID.Text == "") 
			{fOrderID.Text= "0";
			}

			if (fCampaignID.Text == "") 
			{
					fCampaignID.Text= "0";
			}

			if (fAccountID.Text == "") 
			{
					fAccountID.Text= "0";
			}
				
			DAL.OrderHistoryDataAccess oPrintDA = new DAL.OrderHistoryDataAccess();
			DataGrid1.DataSource = oPrintDA.GetOrderHistory(Convert.ToInt32(fOrderID.Text),
															Convert.ToInt32(fCampaignID.Text),
															Convert.ToInt32(fAccountID.Text),
															vStatus,
															vQualifier,
															Convert.ToInt32(fWareHouse.SelectedValue),
															fFromDate.Value,
															fToDate.Value,
															QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID);
									
			DataGrid1.DataBind();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			try 
			{
				ResetDG();
				PopulateDG();
				
				SetForceCloseOrderVisible();
				SetApproveOrderVisible();
				SetDisapproveOrderVisible();
			} 
			catch(Exception ex) 
			{
				DataAccess.Common.ApplicationError.ManageError(ex);

				this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
			}
		}

		private void ResetDG()
		{
			DataGrid1.CurrentPageIndex = 0;
		}


		private void DataGrid1_PageIndexChanged_1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try 
			{
				DataGrid1.CurrentPageIndex = e.NewPageIndex;
				PopulateDG();
			} 
			catch(Exception ex) 
			{
				DataAccess.Common.ApplicationError.ManageError(ex);

				this.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
			}
		}

		private void SetForceCloseOrderVisible() 
		{
			this.ShowForceCloseOrder = (!QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM ||
										QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999");
		}

		private void SetApproveOrderVisible() 
		{
			this.ShowApproveOrder = (!QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM ||
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999");
		}

		private void SetDisapproveOrderVisible() 
		{
         this.ShowDisapproveOrder = (!QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM ||
            QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999");
		}

	}
}
