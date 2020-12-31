namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///		Summary description for ResultSubscriber.
	/// </summary>
	public class ResultSubscription : ControlerResult
	{
		protected QSPFulfillment.CustomerService.CustomerServiceDataGridObjectSelect dtgMain;
		int CustomerOrderHeaderInstance;
		int TransID;
		protected Label lblMessage;

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
			this.dtgMain.ItemCommand +=new DataGridCommandEventHandler(dtgMain_ItemCommand);
			this.dtgMain.PageIndexChanged +=new DataGridPageChangedEventHandler(dtgMain_PageIndexChanged);
			this.dtgMain.ItemDataBound +=new DataGridItemEventHandler(dtgMain_ItemDataBound);
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
			this.DataBinding += new System.EventHandler(this.List_DataBinding);

		}
		#endregion
		public override void DataBind()
		{
			if(!this.Page.PageChanged) 
			{
				this.dtgMain.CurrentPageIndex = this.Page.GetPageIndexSubNested(this.ClientID);
				if(this.Page.ResultSelected)
				{
					this.CustomerOrderHeaderInstance = this.Page.OrderInfo.CustomerOrderHeaderInstance;
					this.TransID = this.Page.OrderInfo.TransID;
			
				}
			}

			this.dtgMain.DataBind();

			base.DataBind ();
		}

		private void List_DataBinding(object sender, System.EventArgs e)
		{
			DataGridItem dgi = (DataGridItem) this.BindingContainer;
			DataSet ds = (DataSet) dgi.DataItem;
			dtgMain.DataSource = ds;
			dtgMain.DataMember = "Subscription";
			if(this.Page.ResultSelected)
			{
				this.CustomerOrderHeaderInstance = this.Page.OrderInfo.CustomerOrderHeaderInstance;
				this.TransID = this.Page.OrderInfo.TransID;
			
			}
			else
			{
				this.dtgMain.SelectedIndex	=-1;
			}
			
			dtgMain.DataBind();
		}
		public void DataBindNotNested()
		{
			try
			{
				DataTable Table = new DataTable();
				
				this.Page.BusSearch.SelectSearchSubscription(Table,List);
				dtgMain.DataSource = Table;
				if(Table.Rows.Count!=0)
				{
					this.dtgMain.Visible = true;
					this.lblMessage.Visible= false;
					if(this.Page.ResultSelected)
					{
						this.CustomerOrderHeaderInstance = this.Page.OrderInfo.CustomerOrderHeaderInstance;
						this.TransID = this.Page.OrderInfo.TransID;
			
					}

					dtgMain.DataBind();
				}
				else
				{
					lblMessage.Text = "No result";
					this.dtgMain.Visible = false;
					this.lblMessage.Visible= true;
					
				}
			}
			catch(QSPFulfillment.DataAccess.Common.ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == DataGrid.SelectCommandName)
			{
				CurrentOrderInfo coi = new CurrentOrderInfo();
				coi.OrderID = GetOrderID(e.Item);
				coi.TransID = GetTransID(e.Item);
				coi.CustomerOrderHeaderInstance = GetCustomerOrderHeaderInstance(e.Item);
				coi.CampaignID = GetCampaignID(e.Item);
				coi.Status =GetOrderStatus(e.Item);
				coi.QualifierName =GetQualifierName(e.Item);
			    this.Page.CustomerInfo = new Customer();
				this.Page.CustomerInfo.CustomerInstance = GetCustomerInstance(e.Item);
				coi.AccountID = GetAccoutID(e.Item);
				InsertIncidentAction(coi.CustomerOrderHeaderInstance, coi.TransID);
				this.Page.IsMagazine = (GetProductType(e.Item).Equals("Magazine"));
				this.Page.IsMagazineBeforeRemit = (GetProductType(e.Item).Equals("Magazine") && GetRemitBatchID(e.Item) == 0);
				this.Page.StudentName = GetStudentFirstName(e.Item) + " " + GetStudentLastName(e.Item);
				this.Page.RecipientName = GetRecipientFirstName(e.Item) + " " + GetRecipientLastName(e.Item);
				this.Page.FireEventSelect(new SelectResultClickedArgs(coi,false));	
			}
		}
		private int GetOrderID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblOrderID")).Text);
		}
		private int GetTransID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblTransID")).Text);
		}
		private int GetCustomerOrderHeaderInstance(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblCustomerOrderHeaderInstance")).Text);
		}
		private int GetCampaignID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblCampaignID")).Text);
		}
		private string GetOrderStatus(DataGridItem e)
		{
			return ((Label)e.FindControl("lblOrderStatus")).Text;
		}
		private string GetRecipientLastName(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblRecipientLastName")).Text;
		}
		private string GetRecipientFirstName(DataGridItem e)
		{
			return ((Label)e.FindControl("lblRecipientFirstName")).Text;
		}
		private string GetStudentLastName(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblStudentLastName")).Text;
		}
		private string GetStudentFirstName(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblStudentFirstName")).Text;
		}
		private string GetQualifierName(DataGridItem e)
		{
			return ((Label)e.FindControl("lblQualifierName")).Text;
		}
		private int GetCustomerInstance(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblCustomerInstance")).Text);
		}
		private int GetAccoutID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblAccountID")).Text);
		}
		private int GetRemitBatchID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblRemitBatchID")).Text);
		}
		private string GetProductType(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblProductType")).Text;
		}

		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if(Page.ResultType == SearchMultiPage.Subscription) 
			{
				DataBindNotNested();
			} 
			else 
			{
				this.Page.AddPageIndexSubNested(this.ClientID, e.NewPageIndex);
			}
		}

		private void dtgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			/*if(this.Page.ResultSelected)
			{
				if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.SelectedItem)
				{
				
					if(GetCustomerOrderHeaderInstance(e.Item)==CustomerOrderHeaderInstance && GetTransID(e.Item)==TransID)
						this.dtgMain.SelectedIndex = e.Item.ItemIndex;
				
				}
			}*/
		}

		protected bool InsertIncidentAction(int CustomerOrderHeaderInstance, int TransID)
		{
			int incidentID = this.Page.BusIncident.InsertItemSelected(CustomerOrderHeaderInstance, TransID, this.Page.UserID);
			this.Page.BusIncidentAction.InsertItemSelected(CustomerOrderHeaderInstance, TransID, incidentID, this.Page.UserID);
			return true;
		}
	}
}
