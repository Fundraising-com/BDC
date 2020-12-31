namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using Common;
	using Common.TableDef;
	using Business.Objects;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for ContactListMaintenance.
	/// </summary>
	public partial class AccountContactListMaintenanceControl : AcctMgtControl
	{
		private const string TRANSACTION_NAME = "SaveAccountContactList";

		protected QSPFulfillment.AcctMgt.Control.ContactListControl ctrlContactListControl;
		protected QSPFulfillment.AcctMgt.Control.ContactMaintenanceControl ctrlContactMaintenanceControl;

		public event System.EventHandler AccountContactsSaved;
		public event System.EventHandler AccountContactsCancelled;
		public event System.EventHandler AccountContactsDeleted;

		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlContactListControl.DeleteContactClicked += new SelectContactEventHandler(ctrlContactListControl_DeleteContactClicked);
			this.ctrlContactListControl.SelectContactClicked += new SelectContactEventHandler(ctrlContactListControl_SelectContactClicked);
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

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			bool isContactTableVisible = this.tblContactMaintenance.Visible;
			this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);

			try 
			{
				this.Page.CurrentTransaction.Open();

				if(isContactTableVisible) 
				{
					Save();

					this.Page.CurrentTransaction.Save();
					
					this.Page.CurrentTransaction = null;

					this.ctrlContactListControl.DataBind();
				}

				this.ctrlContactMaintenanceControl.AccountID = this.AccountID;
				this.ctrlContactMaintenanceControl.ContactID = 0;
				this.ctrlContactMaintenanceControl.DataBind();

				this.tblContactMaintenance.Visible = true;

				if(isContactTableVisible) 
				{
					if(AccountContactsSaved != null)
						AccountContactsSaved(sender, e);
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();
				
				this.Page.CurrentTransaction = null;
			}
		}

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);

			try 
			{
				this.Page.CurrentTransaction.Open();

				if(this.tblContactMaintenance.Visible) 
				{
					Save();

					this.Page.CurrentTransaction.Save();
					
					this.Page.CurrentTransaction = null;

					this.ctrlContactListControl.DataBind();

					if(AccountContactsSaved != null)
						AccountContactsSaved(sender, e);
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();
				
				this.Page.CurrentTransaction = null;
			} 
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			if(AccountContactsCancelled != null)
				AccountContactsCancelled(sender, e);
		}

		private void ctrlContactListControl_DeleteContactClicked(object sender, SelectContactClickedArgs e)
		{
			try 
			{
				e.oContact.dataSet.Contact[0].Delete();
				//TODO: Delete address and phones too
				e.oContact.Save();

				this.ctrlContactListControl.DataBind();

				CheckForEmptyList();

				if(AccountContactsDeleted != null) 
				{
					AccountContactsDeleted(this, EventArgs.Empty);
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void ctrlContactListControl_SelectContactClicked(object sender, SelectContactClickedArgs e)
		{
			bool isContactTableVisible = this.tblContactMaintenance.Visible;

			try 
			{
				if(isContactTableVisible) 
				{
					this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);
					this.Page.CurrentTransaction.Open();

					Save();

					this.Page.CurrentTransaction.Save();
						
					this.Page.CurrentTransaction = null;

					this.ctrlContactListControl.DataBind();
				}

				this.ctrlContactMaintenanceControl.AccountID = this.AccountID;
				this.ctrlContactMaintenanceControl.oContact = e.oContact;
				this.ctrlContactMaintenanceControl.ContactID = e.oContact.dataSet.Contact[0].Id;

				this.ctrlContactMaintenanceControl.DataBind();

				this.tblContactMaintenance.Visible = true;

				if(isContactTableVisible) 
				{
					if(AccountContactsSaved != null) 
					{
						AccountContactsSaved(this, EventArgs.Empty);
					}
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();
				
				this.Page.CurrentTransaction = null;
			} 
		}

		public int AccountID 
		{
			get 
			{
				if(this.ViewState["AccountID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["AccountID"]);
			}
			set 
			{
				this.ViewState["AccountID"] = value;
			}
		}

		public override void DataBind()
		{
			SetValue();
			SetVisible();
			
			ctrlContactListControl.DataBind();
		}

		private void SetValue() 
		{
			ctrlContactListControl.AccountID = this.AccountID;
		}

		private void SetVisible() 
		{
			/* 03/20/2006 - Ben : FMs can now edit their own account information
			//bool bVisible = !QSPPage.aUserProfile.IsFM; MS Feb 28
			bool bVisible = !QSPPage.aUserProfile.IsFM || QSPPage.aUserProfile.FMID == "9999";

			this.btnAddNew.Visible = bVisible;
			this.btnSubmitTop.Visible = bVisible;
			this.btnSubmitBottom.Visible = bVisible;
			this.ctrlContactListControl.ShowDelete = bVisible;
			*/
		}

		public void Save() 
		{
			Contact oContact;

			oContact = new Contact(this.Page.CurrentTransaction);
			oContact.GetOneByID(this.ctrlContactMaintenanceControl.ContactID);

			this.ctrlContactMaintenanceControl.oContact = oContact;

			this.ctrlContactMaintenanceControl.Save();
			oContact.Save();

			this.ctrlContactMaintenanceControl.ContactID = oContact.dataSet.Contact[0].Id;

			// Update contact for all CAs for current season
			if(oContact.dataSet.Contact[0].TypeId == Convert.ToInt32(ContactTypeID.Primary)) 
			{
				UpdateContactsForAllCampaigns(this.AccountID, this.ctrlContactMaintenanceControl.ContactID);
			}
		}

		private void UpdateContactsForAllCampaigns(int accountID, int contactID)
		{
			Campaign campaign = new Campaign(this.Page.CurrentTransaction);
				
			campaign.UpdateContactForAllAccount(accountID, contactID);
		}

		private void CheckForEmptyList() 
		{
			Contact contact = new Contact(this.AccountID, this.Page.CurrentTransaction);

			if(contact.dataSet.Contact.Count == 0) 
			{
				UpdateContactsForAllCampaigns(this.AccountID, -1);
			}
		}
	}
}
