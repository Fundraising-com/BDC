namespace QSPFulfillment.MarketingMgt.Control
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

	public delegate void SelectFulfillmentHouseContactEventHandler(object sender, SelectFulfillmentHouseContactClickedArgs e);

	/// <summary>
	///		Summary description for FulfillmentHouseContactSearchControl.
	/// </summary>
	public class FulfillmentHouseContactSearchControl : MarketingMgtControlDataGrid
	{
		private const string DELETE_CONTACT_CONFIRMATION_MESSAGE = "Are you sure you want to delete this contact?";

		protected System.Web.UI.WebControls.Label lblMessage;
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected QSPFulfillment.CustomerService.ControlerConfirmationPage ctrlControlerConfirmationPageDelete;

		public event SelectFulfillmentHouseContactEventHandler SelectFulfillmentHouseContactClick;
		public event SelectFulfillmentHouseContactEventHandler DeleteFulfillmentHouseContactClick;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlControlerConfirmationPageDelete.Confirmed += new ConfirmEventHandler(ctrlControlerConfirmationPageDelete_Confirmed);
			InitializeComponent();
			base.OnInit(e,dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dtgMain.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try 
			{
				if(e.CommandName == "Select")
				{
					SelectFulfillmentHouseContactClickedArgs args;
				
					args = new SelectFulfillmentHouseContactClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.FulfillmentHouseContact(GetInstance(e.Item), GetFulfillmentHouseID(e.Item), GetFulfillmentHouseName(e.Item), GetFirstName(e.Item), GetLastName(e.Item), GetEmail(e.Item), GetPositionTitle(e.Item), GetWorkPhone(e.Item), GetFax(e.Item), GetCustomerServiceContactFirstName(e.Item), GetCustomerServiceContactLastName(e.Item), GetCustomerServiceContactEmail(e.Item), GetCustomerServiceContactPhone(e.Item)));
				
					if(SelectFulfillmentHouseContactClick != null)
						SelectFulfillmentHouseContactClick(source,args);
				} 
				else if(e.CommandName == "Delete") 
				{
					ShowDeleteContactConfirmation(GetInstance(e.Item));
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlControlerConfirmationPageDelete_Confirmed(object sender, EventArgs e)
		{
			try 
			{
				if(FulfillmentHouseContactIDToDelete != 0) 
				{
					DeleteContact(FulfillmentHouseContactIDToDelete);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public int FulfillmentHouseID 
		{
			get 
			{
				if(this.ViewState["FulfillmentHouseID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["FulfillmentHouseID"]);
			}
			set 
			{
				this.ViewState["FulfillmentHouseID"] = value;
			}
		}

		public int MainContactID
		{
			get 
			{
				int mainContactID = 0;

				if(ViewState["MainContactID"] != null) 
				{
					mainContactID = Convert.ToInt32(ViewState["MainContactID"]);
				}

				return mainContactID;
			}
			set 
			{
				ViewState["MainContactID"] = value;
			}
		}

		private int FulfillmentHouseContactIDToDelete 
		{
			get 
			{
				int fulfillmentHouseContactIDToDelete = 0;

				if(ViewState["FulfillmentHouseContactIDToDelete"] != null) 
				{
					fulfillmentHouseContactIDToDelete = Convert.ToInt32(ViewState["FulfillmentHouseContactIDToDelete"]);
				}

				return fulfillmentHouseContactIDToDelete;
			}
			set 
			{
				ViewState["FulfillmentHouseContactIDToDelete"] = value;
			}
		}

		protected override void LoadData()
		{
			DataView dv;
			DataSource = new DataTable("FulfillmentHouseContact");

			this.Page.BusFulfillmentHouseContact.SelectAllByFulfillmentHouseID(DataSource, FulfillmentHouseID);

			dv = new DataView(DataSource, "IsMainContact = 'Y'", String.Empty, DataViewRowState.CurrentRows);
			
			if(dv.Count > 0) 
			{
				MainContactID = Convert.ToInt32(dv[0]["Instance"]);
			} 
			else 
			{
				MainContactID = 0;
			}
		}

		private void ShowDeleteContactConfirmation(int fulfillmentHouseContactID) 
		{
			this.FulfillmentHouseContactIDToDelete = fulfillmentHouseContactID;

			this.ctrlControlerConfirmationPageDelete.Message = DELETE_CONTACT_CONFIRMATION_MESSAGE;
			this.ctrlControlerConfirmationPageDelete.ShowConfirmationWindow();
		}

		protected virtual void DeleteContact(int fulfillmentHouseContactID)
		{
			SelectFulfillmentHouseContactClickedArgs args = null;
			FulfillmentHouseContact fulfillmentHouseContact = null;

			this.Page.BusFulfillmentHouseContact.Delete(fulfillmentHouseContactID);

			fulfillmentHouseContact = new FulfillmentHouseContact();
			fulfillmentHouseContact.Instance = fulfillmentHouseContactID;
			args = new SelectFulfillmentHouseContactClickedArgs(fulfillmentHouseContact);

			if(DeleteFulfillmentHouseContactClick != null) 
			{
				DeleteFulfillmentHouseContactClick(this, args);
			}
		}

		private int GetInstance(DataGridItem e) 
		{
			return Convert.ToInt32(((Label)e.FindControl("lblInstance")).Text);
		}

		private int GetFulfillmentHouseID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label)e.FindControl("lblFulfillmentHouseID")).Text);
		}

		private string GetFulfillmentHouseName(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblFulfillmentHouseName")).Text;
		}

		private string GetFirstName(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblContactFirstName")).Text;
		}

		private string GetLastName(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblContactLastName")).Text;
		}

		private string GetEmail(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblEmail")).Text;
		}

		private string GetPositionTitle(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblPositionTitle")).Text;
		}

		private string GetWorkPhone(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblWorkPhone")).Text;
		}

		private string GetFax(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblFax")).Text;
		}

		private string GetCustomerServiceContactFirstName(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblCustomerServiceContactFirstName")).Text;
		}

		private string GetCustomerServiceContactLastName(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblCustomerServiceContactLastName")).Text;
		}

		private string GetCustomerServiceContactEmail(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblCustomerServiceContactEmail")).Text;
		}

		private string GetCustomerServiceContactPhone(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblCustomerServiceContactPhone")).Text;
		}
	}
}
