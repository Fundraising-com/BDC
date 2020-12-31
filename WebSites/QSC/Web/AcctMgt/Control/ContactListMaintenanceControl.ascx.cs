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

	/// <summary>
	///		Summary description for ContactListMaintenance.
	/// </summary>
	public partial class ContactListMaintenanceControl : AcctMgtControl
	{
		private Contact oContact;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		protected void ContactListMaintenance_Init(object sender, EventArgs e)
		{
			ContactMaintenanceControl ctrlContactMaintenanceControl;

			foreach(string ID in ContactControlIDCollection) 
			{
				ctrlContactMaintenanceControl = (ContactMaintenanceControl) LoadControl("ContactMaintenanceControl.ascx");
				ctrlContactMaintenanceControl.ID = ID;
				this.plhContactList.Controls.Add(ctrlContactMaintenanceControl);
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Init += new System.EventHandler(this.ContactListMaintenance_Init);

		}
		#endregion

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			ContactMaintenanceControl ctrlContactMaintenanceControl;

			ctrlContactMaintenanceControl = (ContactMaintenanceControl) LoadControl("ContactMaintenanceControl.ascx");
			this.plhContactList.Controls.Add(ctrlContactMaintenanceControl);
			ctrlContactMaintenanceControl.ID = "ctrlContactMaintenanceControl" + (this.plhContactList.Controls.Count + 1);
			ctrlContactMaintenanceControl.ContactID = 0;
			ctrlContactMaintenanceControl.DataBind();

			this.ContactControlIDCollection.Add(ctrlContactMaintenanceControl.ID);
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

		private ArrayList ContactControlIDCollection 
		{
			get 
			{
				if(Session[this.ClientID + "ContactControlIDCollection"] == null)
					Session[this.ClientID + "ContactControlIDCollection"] = new ArrayList();

				return (ArrayList) Session[this.ClientID + "ContactControlIDCollection"];
			}
		}

		public void CreateControls() 
		{
			ContactMaintenanceControl ctrlContactMaintenanceControl;

			this.ContactControlIDCollection.Clear();
			this.plhContactList.Controls.Clear();
			if(this.AccountID != 0) 
			{
				oContact = new Contact(AccountID, this.Page.CurrentTransaction);

				if(this.BillToCampaignContactID == 0) 
				{
					foreach(ContactDataSet.ContactRow row in oContact.dataSet.Contact.Rows) 
					{
						ctrlContactMaintenanceControl = (ContactMaintenanceControl) LoadControl("ContactMaintenanceControl.ascx");
						this.plhContactList.Controls.Add(ctrlContactMaintenanceControl);
						ctrlContactMaintenanceControl.ID = "ctrlContactMaintenanceControl" + (this.plhContactList.Controls.Count + 1);
						ctrlContactMaintenanceControl.ContactID = row.Id;

						this.ContactControlIDCollection.Add(ctrlContactMaintenanceControl.ID);
					}
				} 
				else 
				{
					ctrlContactMaintenanceControl = (ContactMaintenanceControl) LoadControl("ContactMaintenanceControl.ascx");
					this.plhContactList.Controls.Add(ctrlContactMaintenanceControl);
					ctrlContactMaintenanceControl.ID = "ctrlContactMaintenanceControl" + (this.plhContactList.Controls.Count + 1);
					ctrlContactMaintenanceControl.ContactID = this.ShipToCampaignContactID;
					this.ContactControlIDCollection.Add(ctrlContactMaintenanceControl.ID);

					ctrlContactMaintenanceControl = (ContactMaintenanceControl) LoadControl("ContactMaintenanceControl.ascx");
					this.plhContactList.Controls.Add(ctrlContactMaintenanceControl);
					ctrlContactMaintenanceControl.ID = "ctrlContactMaintenanceControl" + (this.plhContactList.Controls.Count + 1);
					ctrlContactMaintenanceControl.ContactID = this.BillToCampaignContactID;
					this.ContactControlIDCollection.Add(ctrlContactMaintenanceControl.ID);
				}
			}
		}

		public override void DataBind()
		{
			ContactMaintenanceControl ctrlContactMaintenanceControl;

			CreateControls();

			if(this.AccountID != 0) 
			{
				foreach(System.Web.UI.Control ctrl in this.plhContactList.Controls) 
				{
					if(ctrl is ContactMaintenanceControl) 
					{
						ctrlContactMaintenanceControl = (ContactMaintenanceControl) ctrl;
						ctrlContactMaintenanceControl.oContact = oContact;
						ctrlContactMaintenanceControl.DataBind();
					}
				}
			}
		}

		/*public void Save() 
		{
			ContactMaintenanceControl ctrlContactMaintenanceControl;

			if(this.AccountID != 0) 
			{
				ph = new Phone(this.PhoneListID);

				foreach(Control ctrl in this.plhPhoneList.Controls) 
				{
					if(ctrl is PhoneMaintenanceControl) 
					{
						ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) ctrl;
						ctrlPhoneMaintenanceControl.DataSource = ph;

						ctrlPhoneMaintenanceControl.PhoneListID = this.PhoneListID;
						ctrlPhoneMaintenanceControl.Save();
					}
				}

				ph.Save();
			}
		}

		public void SaveNew() 
		{
			PhoneMaintenanceControl ctrlPhoneMaintenanceControl;

			foreach(Control ctrl in this.plhPhoneList.Controls) 
			{
				if(ctrl is PhoneMaintenanceControl) 
				{
					ctrlPhoneMaintenanceControl = (PhoneMaintenanceControl) ctrl;
					ctrlPhoneMaintenanceControl.PhoneID = 0;
				}
			}

			Save();
		}*/
	}
}
