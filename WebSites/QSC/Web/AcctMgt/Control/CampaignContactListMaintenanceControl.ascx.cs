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
	public partial class CampaignContactListMaintenanceControl : AcctMgtControl
	{
		private const string TRANSACTION_NAME = "SaveCampaignContactList";

		protected QSPFulfillment.AcctMgt.Control.ContactMaintenanceControl ctrlContactMaintenanceControlShipTo;
		protected QSPFulfillment.AcctMgt.Control.ContactMaintenanceControl ctrlContactMaintenanceControlBillTo;
		protected QSPFulfillment.AcctMgt.Control.ContactListControl ctrlContactListControl;

		public event System.EventHandler CampaignContactsSaved;
		public event System.EventHandler CampaignContactsCancelled;

		private Contact oContact;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddJavaScript();
		}

		protected void CampaignContactListMaintenanceControl_PreRender(object sender, EventArgs e)
		{
			this.ctrlContactMaintenanceControlBillTo.ClientVisible = !this.chkSameAsShipTo.Checked;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlContactListControl.CopyContactClicked += new CopyContactEventHandler(ctrlContactListControl_CopyContactClicked);
			this.ctrlContactListControl.DeleteContactClicked += new SelectContactEventHandler(ctrlContactListControl_DeleteContactClicked);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new System.EventHandler(this.CampaignContactListMaintenanceControl_PreRender);

		}
		#endregion

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);

			try 
			{
				this.Page.CurrentTransaction.Open();

				Save();

				this.Page.CurrentTransaction.Save();
				
				this.Page.CurrentTransaction = null;

				if(CampaignContactsSaved != null)
					CampaignContactsSaved(sender, e);
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
			if(CampaignContactsCancelled != null)
				CampaignContactsCancelled(sender, e);
		}

		private void ctrlContactListControl_CopyContactClicked(object sender, CopyContactClickedArgs e)
		{
			if(e.oType == ContactSelectionType.ShipTo) 
			{
				this.ShipToCampaignContactID = 0;

				this.ctrlContactMaintenanceControlShipTo.ContactID = e.oContact.dataSet.Contact[0].Id;
				this.ctrlContactMaintenanceControlShipTo.oContact = e.oContact;

				this.ctrlContactMaintenanceControlShipTo.DataBind();

				this.ctrlContactMaintenanceControlShipTo.UnbindIDs();
			} 
			else if(e.oType == ContactSelectionType.BillTo) 
			{
				this.BillToCampaignContactID = 0;

				this.ctrlContactMaintenanceControlBillTo.ContactID = e.oContact.dataSet.Contact[0].Id;
				this.ctrlContactMaintenanceControlBillTo.oContact = e.oContact;

				this.ctrlContactMaintenanceControlBillTo.DataBind();

				this.ctrlContactMaintenanceControlBillTo.UnbindIDs();

				this.chkSameAsShipTo.Checked = false;
				this.ctrlContactMaintenanceControlBillTo.ClientVisible = true;
			}
		}

		private void ctrlContactListControl_DeleteContactClicked(object sender, SelectContactClickedArgs e)
		{
			try 
			{
				e.oContact.dataSet.Contact[0].Delete();
				e.oContact.Save();

				this.ctrlContactListControl.DataBind();
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		public int CampaignID 
		{
			get 
			{
				if(this.ViewState["CampaignID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["CampaignID"]);
			}
			set 
			{
				this.ViewState["CampaignID"] = value;
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

		public int BillToCampaignContactID
		{
			get 
			{
				if(this.ViewState["BillToCampaignContactID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["BillToCampaignContactID"]);
			}
			set 
			{
				this.ViewState["BillToCampaignContactID"] = value;
			}
		}

		public int ShipToCampaignContactID
		{
			get 
			{
				if(this.ViewState["ShipToCampaignContactID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["ShipToCampaignContactID"]);
			}
			set 
			{
				this.ViewState["ShipToCampaignContactID"] = value;
			}
		}

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript ();

			AddJavaScriptSetVisibleBillToContact();
		}

		private void AddJavaScriptSetVisibleBillToContact() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function SetVisibleBillToContact() {\n";
			script += "    if(document.getElementById(\"" + this.chkSameAsShipTo.ClientID + "\").checked) {\n";
			script += "      document.getElementById(\"" + this.ctrlContactMaintenanceControlBillTo.ClientPanelID + "\").style.display = \"none\";\n";
			script += "    } else {\n";
			script += "      document.getElementById(\"" + this.ctrlContactMaintenanceControlBillTo.ClientPanelID + "\").style.display = \"\";\n";
			script += "    }\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("SetVisibleBillToContact", script);

			this.chkSameAsShipTo.Attributes["onClick"] = "SetVisibleBillToContact();";
		}

		#endregion

		public override void DataBind()
		{
			LoadData();
			SetValue();
			
			ctrlContactListControl.DataBind();
			ctrlContactMaintenanceControlShipTo.DataBind();
			ctrlContactMaintenanceControlBillTo.DataBind();

			SetVisible();
		}

		private void LoadData() 
		{
			oContact = new Contact(this.Page.CurrentTransaction);

			if(this.CampaignID != 0) 
			{
				oContact.GetOneByID(this.ShipToCampaignContactID);
				oContact.GetOneByID(this.BillToCampaignContactID);
			} 
		}

		private void SetValue() 
		{
			ctrlContactListControl.AccountID = this.AccountID;

			ctrlContactMaintenanceControlShipTo.ContactID = this.ShipToCampaignContactID;
			ctrlContactMaintenanceControlShipTo.oContact = oContact;

			ctrlContactMaintenanceControlBillTo.ContactID = this.BillToCampaignContactID;
			ctrlContactMaintenanceControlBillTo.oContact = oContact;
		}

		private void SetVisible() 
		{
			bool bVisible;

			if(this.ShipToCampaignContactID == this.BillToCampaignContactID) 
			{
				this.chkSameAsShipTo.Checked = true;
				this.ctrlContactMaintenanceControlBillTo.ClientVisible = false;
			} 
			else 
			{
				this.chkSameAsShipTo.Checked = false;
				this.ctrlContactMaintenanceControlBillTo.ClientVisible = true;
			}

			//bVisible = !QSPPage.aUserProfile.IsFM; MS Feb 28
			bVisible = !QSPPage.aUserProfile.IsFM || QSPPage.aUserProfile.FMID == "9999";
			this.btnSubmitTop.Visible = bVisible;
			this.btnSubmitBottom.Visible = bVisible;
			this.ctrlContactListControl.ShowDelete = bVisible;
		}

		public void Save() 
		{
			if(this.CampaignID != 0) 
			{
				LoadData();

				this.ctrlContactMaintenanceControlShipTo.ContactID = this.ShipToCampaignContactID;
				this.ctrlContactMaintenanceControlShipTo.oContact = oContact;
				this.ctrlContactMaintenanceControlShipTo.Save();

				if(!this.chkSameAsShipTo.Checked) 
				{
					if(this.ShipToCampaignContactID == this.BillToCampaignContactID) 
					{
						this.ctrlContactMaintenanceControlBillTo.ContactID = 0;
					} 
					else 
					{
						this.ctrlContactMaintenanceControlBillTo.ContactID = this.BillToCampaignContactID;
					}

					this.ctrlContactMaintenanceControlBillTo.oContact = oContact;
					this.ctrlContactMaintenanceControlBillTo.Save();

					oContact.Save();
					this.ShipToCampaignContactID = this.ctrlContactMaintenanceControlShipTo.rowContact.Id;
					this.BillToCampaignContactID = this.ctrlContactMaintenanceControlBillTo.rowContact.Id;
				} 
				else 
				{
					if(this.ShipToCampaignContactID != this.BillToCampaignContactID) 
					{
						this.ctrlContactMaintenanceControlBillTo.ContactID = this.BillToCampaignContactID;
						this.ctrlContactMaintenanceControlBillTo.oContact = oContact;
						this.ctrlContactMaintenanceControlBillTo.Visible = false;

						this.ctrlContactMaintenanceControlBillTo.Save();
						this.ctrlContactMaintenanceControlBillTo.Visible = true;
					}

					oContact.Save();

					this.ShipToCampaignContactID = this.ctrlContactMaintenanceControlShipTo.rowContact.Id;
					this.BillToCampaignContactID = this.ShipToCampaignContactID;
				}

				oContact.LinkToCampaign(this.CampaignID, this.ShipToCampaignContactID, this.BillToCampaignContactID);
				
				SetVisible();
				this.ctrlContactListControl.DataBind();
			}
		}
	}
}
