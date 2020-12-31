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
	
	public delegate void SelectPublisherContactEventHandler(object sender, SelectPublisherContactClickedArgs e);

	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class PublisherContactSearchControl : MarketingMgtControlDataGrid
	{
		private const string DELETE_CONTACT_CONFIRMATION_MESSAGE = "Are you sure you want to delete this contact?";

		protected System.Web.UI.WebControls.Label lblMessage;
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected QSPFulfillment.CustomerService.ControlerConfirmationPage ctrlControlerConfirmationPageDelete;

		public event SelectPublisherContactEventHandler SelectPublisherContactClick;
		public event SelectPublisherContactEventHandler DeletePublisherContactClick;
		
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
					SelectPublisherContactClickedArgs args;

					args = new SelectPublisherContactClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.PublisherContact(GetPublisherContactInstance(e.Item), GetPublisherNumber(e.Item), GetPublisherName(e.Item), GetContactFirstName(e.Item), GetContactLastName(e.Item), GetEmail(e.Item), GetPositionTitle(e.Item), GetPhoneListID(e.Item)));
				
					if(SelectPublisherContactClick != null)
						SelectPublisherContactClick(source,args);
				} 
				else if(e.CommandName == "Delete") 
				{
					ShowDeleteContactConfirmation(GetPublisherContactInstance(e.Item));
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
				if(PublisherContactIDToDelete != 0) 
				{
					DeleteContact(PublisherContactIDToDelete);
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public bool ShowPublisherName 
		{
			get 
			{
				return this.dtgMain.Columns[2].Visible;
			}
			set 
			{
				this.dtgMain.Columns[2].Visible = value;
			}
		}

		public int PublisherID
		{
			get 
			{
				if(this.ViewState["PublisherID"] == null)
					this.ViewState["PublisherID"] = 0;

				return Convert.ToInt32(this.ViewState["PublisherID"]);
			}
			set 
			{
				this.ViewState["PublisherID"] = value;
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

		private int PublisherContactIDToDelete 
		{
			get 
			{
				int publisherContactIDToDelete = 0;

				if(ViewState["PublisherContactIDToDelete"] != null) 
				{
					publisherContactIDToDelete = Convert.ToInt32(ViewState["PublisherContactIDToDelete"]);
				}

				return publisherContactIDToDelete;
			}
			set 
			{
				ViewState["PublisherContactIDToDelete"] = value;
			}
		}

		protected override void LoadData()
		{
			DataView dv;
			DataSource = new DataTable("PublisherContact");

			this.Page.BusPublisherContact.SelectAllByPublisherID(DataSource, PublisherID);

			dv = new DataView(DataSource, "IsMainContact = 'Y'", String.Empty, DataViewRowState.CurrentRows);
			
			if(dv.Count > 0) 
			{
				MainContactID = Convert.ToInt32(dv[0]["PContact_Instance"]);
			} 
			else 
			{
				MainContactID = 0;
			}
		}

		private void ShowDeleteContactConfirmation(int publisherContactID) 
		{
			this.PublisherContactIDToDelete = publisherContactID;

			this.ctrlControlerConfirmationPageDelete.Message = DELETE_CONTACT_CONFIRMATION_MESSAGE;
			this.ctrlControlerConfirmationPageDelete.ShowConfirmationWindow();
		}

		protected virtual void DeleteContact(int publisherContactID)
		{
			SelectPublisherContactClickedArgs args = null;
			PublisherContact publisherContact = null;

			this.Page.BusPublisherContact.Delete(publisherContactID);

			publisherContact = new PublisherContact();
			publisherContact.PublisherContactInstance = publisherContactID;
			args = new SelectPublisherContactClickedArgs(publisherContact);

			if(DeletePublisherContactClick != null) 
			{
				DeletePublisherContactClick(this, args);
			}
		}

		private int GetPublisherContactInstance(DataGridItem e) 
		{
			return Convert.ToInt32(((Label)e.FindControl("lblPublisherContactInstance")).Text);
		}

		private int GetPublisherNumber(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblPublisherNumber")).Text);
		}

		private string GetPublisherName(DataGridItem e)
		{
			return ((Label)e.FindControl("lblPublisherName")).Text;
		}

		private string GetContactFirstName(DataGridItem e)
		{
			return ((Label)e.FindControl("lblContactFirstName")).Text;
		}

		private string GetContactLastName(DataGridItem e)
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

		private int GetPhoneListID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label)e.FindControl("lblPhoneListID")).Text);
		}

		private string GetProductCode(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblProductCode")).Text;
		}
	}
}