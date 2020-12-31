namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common;
	using Common.TableDef;
	using Business.Objects;
	using QSP.WebControl;

	[Serializable]
	public enum ContactLayout 
	{
		Horizontal,
		Vertical
	}

	/// <summary>
	///		Summary description for ContactMaintenance.
	/// </summary>
	public partial class ContactMaintenanceControl : AcctMgtControl
	{
		protected QSPFulfillment.AcctMgt.Control.AddressMaintenanceControl ctrlAddressMaintenanceControl;
		protected QSPFulfillment.AcctMgt.Control.PhoneListMaintenanceControl ctrlPhoneListMaintenanceControl;
		private Contact contact;
		private ContactDataSet.ContactRow row;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		protected void ContactMaintenanceControl_PreRender(object sender, EventArgs e)
		{
			if(Layout == ContactLayout.Vertical) 
			{
				plhLayout.Controls.Add(new LiteralControl("</tr><tr>"));
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
			this.PreRender += new System.EventHandler(this.ContactMaintenanceControl_PreRender);

		}
		#endregion

		protected void btnRemove_Click(object sender, System.EventArgs e)
		{
			this.Visible = false;
		}

		public ContactLayout Layout 
		{
			get 
			{
				ContactLayout contactLayout = ContactLayout.Vertical;

				try 
				{
					contactLayout = (ContactLayout) ViewState["ContactLayout"];
				} 
				catch { }

				return contactLayout;
			}
			set 
			{
				ViewState["ContactLayout"] = value;
			}
		}

		public int ContactID 
		{
			get 
			{
				if(this.ViewState["ContactID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["ContactID"]);
			}
			set 
			{
				this.ViewState["ContactID"] = value;
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

		public Contact oContact 
		{
			get 
			{
				return contact;
			}
			set 
			{
				contact = value;
			}
		}

		public ContactDataSet.ContactRow rowContact 
		{
			get 
			{
				return row;
			}
		}

		public bool FixedMode 
		{
			get
			{
				if(this.ViewState["FixedMode"] == null)
					return false;

				return Convert.ToBoolean(this.ViewState["FixedMode"]);
			}
			set 
			{
				this.ViewState["FixedMode"] = value;
			}
		}

		public bool Required 
		{
			get
			{
				if(this.ViewState["Required"] == null)
					return true;

				return Convert.ToBoolean(this.ViewState["Required"]);
			}
			set 
			{
				this.ViewState["Required"] = value;
				SetRequired();
			}
		}

		public bool Enabled 
		{
			get 
			{
				if(this.ViewState["Enabled"] == null)
					return true;

				return Convert.ToBoolean(this.ViewState["Enabled"]);
			}
			set 
			{
				this.ViewState["Enabled"] = value;
				SetEnabled();
			}
		}

		public string ContactType 
		{
			get 
			{
				return this.lblType.Text;
			}
			set 
			{
				this.lblType.Text = value;
			}
		}

		public bool ClientVisible 
		{
			get 
			{
				return this.divContact.Style["display"] == "";
			}
			set 
			{
				if(value) 
				{
					this.divContact.Style["display"] = "";
				} 
				else 
				{
					this.divContact.Style["display"] = "none";
				}
			}
		}

		public string ClientPanelID 
		{
			get 
			{
				return this.divContact.ClientID;
			}
		}

		public bool AddressClientVisible 
		{
			get 
			{
				return this.divAddressMaintenance.Style["display"] == "";
			}
			set 
			{
				if(value) 
				{
					this.divAddressMaintenance.Style["display"] = "";
				} 
				else 
				{
					this.divAddressMaintenance.Style["display"] = "none";
				}
			}
		}

		public bool RemoveButtonVisible 
		{
			get 
			{
				return this.divRemove.Visible;
			}
			set 
			{
				this.divRemove.Visible = value;
			}
		}

		public bool IsPrimaryVisible 
		{
			get 
			{
				return this.trPrimary.Visible;
			}
			set 
			{
				this.trPrimary.Visible = value;
			}
		}

		public bool ShowOnePhone 
		{
			get 
			{
				return this.ctrlPhoneListMaintenanceControl.ShowOne;
			}
			set 
			{
				this.ctrlPhoneListMaintenanceControl.ShowOne = value;
			}
		}

		public bool IsPhoneRequired 
		{
			get 
			{
				return this.ctrlPhoneListMaintenanceControl.Required;
			}
			set 
			{
				this.ctrlPhoneListMaintenanceControl.Required = value;
			}
		}

		#region Fields

		private bool IsPrimary 
		{
			get 
			{
				return this.chkPrimary.Checked;
			}
			set 
			{
				this.chkPrimary.Checked = value;
			}
		}

		private string Title 
		{
			get 
			{
				return this.tbxTitle.Text;
			}
			set
			{
				this.tbxTitle.Text = value;
			}
		}

		private string FirstName 
		{
			get 
			{
				return this.tbxFirstName.Text;
			}
			set 
			{
				this.tbxFirstName.Text = value;
			}
		}

		private string MiddleInitial 
		{
			get 
			{
				return this.tbxMiddleInitial.Text;
			}
			set 
			{
				this.tbxMiddleInitial.Text = value;
			}
		}

		private string LastName 
		{
			get 
			{
				return this.tbxLastName.Text;
			}
			set 
			{
				this.tbxLastName.Text = value;
			}
		}

		private string Email 
		{
			get 
			{
				return this.tbxEmail.Text;
			}
			set 
			{
				this.tbxEmail.Text = value;
			}
		}

		private string Function 
		{
			get 
			{
				return this.tbxFunction.Text;
			}
			set 
			{
				this.tbxFunction.Text = value;
			}
		}

		#endregion

		#region Controls

		public TextBoxReq FirstNameControl 
		{
			get 
			{
				return this.tbxFirstName;
			}
		}

		public TextBoxReq LastNameControl 
		{
			get 
			{
				return this.tbxLastName;
			}
		}

		#endregion

		public override void DataBind()
		{
			LoadData();

			this.ctrlAddressMaintenanceControl.DataBind();
			this.ctrlPhoneListMaintenanceControl.DataBind();
		}

		private void LoadData() 
		{
			if(this.ContactID != 0 && this.oContact != null) 
			{
				row = this.oContact.dataSet.Contact.FindById(ContactID);

				if(row != null) 
				{
					this.ctrlAddressMaintenanceControl.DataSource = new Address(this.Page.CurrentTransaction);
					this.ctrlAddressMaintenanceControl.DataSource.GetOneByID(row.AddressID);
					SetValue();
				} 
				else 
				{
					SetValueEmpty();
				}
			} 
			else 
			{
				SetValueEmpty();
			}

			SetVisible();
		}

		private void SetValue() 
		{
			try 
			{
				this.IsPrimary = (row.TypeId == Convert.ToInt32(ContactTypeID.Primary));
				this.Title = row.Title;
				this.FirstName = row.FirstName;
				this.MiddleInitial = row.MiddleInitial;
				this.LastName = row.LastName;
				this.Email = row.Email;
				this.Function = row.Function;
				this.ctrlAddressMaintenanceControl.AddressID = row.AddressID;
				this.ctrlPhoneListMaintenanceControl.PhoneListID = row.PhoneListID;
			} 
			catch (Exception ex) 
			{
				ApplicationError.ManageError(ex);
			}
		}

		private void SetValueEmpty() 
		{
			this.IsPrimary = IsPrimaryVisible;
			this.Title = "";
			this.FirstName = "";
			this.MiddleInitial = "";
			this.LastName = "";
			this.Email = "";
			this.Function = "";
			this.ctrlAddressMaintenanceControl.AddressID = 0;
			this.ctrlPhoneListMaintenanceControl.PhoneListID = 0;
		}

		private void SetVisible() 
		{
			this.lblType.Visible = this.FixedMode;
			this.divRemove.Visible &= !this.FixedMode;
			this.lblPhoneTitle.Visible = (this.Layout == ContactLayout.Horizontal);
		}

		private void SetRequired() 
		{
			this.tbxFirstName.Required = this.Required;
			this.tbxLastName.Required = this.Required;
			this.ctrlAddressMaintenanceControl.Required = this.Required;
			this.ctrlPhoneListMaintenanceControl.Required = this.Required;
		}

		private void SetEnabled() 
		{
			this.tbxTitle.ReadOnly = !this.Enabled;
			this.tbxFirstName.ReadOnly = !this.Enabled;
			this.tbxMiddleInitial.ReadOnly = !this.Enabled;
			this.tbxLastName.ReadOnly = !this.Enabled;
			this.tbxEmail.ReadOnly = !this.Enabled;
			this.tbxFunction.ReadOnly = !this.Enabled;
			this.ctrlAddressMaintenanceControl.Enabled = this.Enabled;
			this.ctrlPhoneListMaintenanceControl.Enabled = this.Enabled;
		}

		public void Save() 
		{
			if(this.Visible) 
			{
				if(this.ContactID != 0) 
				{
					row = this.oContact.dataSet.Contact.FindById(this.ContactID);

					if(row != null) 
					{
						this.ctrlAddressMaintenanceControl.DataSource = new Address(this.Page.CurrentTransaction);

						this.ctrlAddressMaintenanceControl.DataSource.GetOneByID(row.AddressID);

						this.ctrlAddressMaintenanceControl.Save();
						this.ctrlAddressMaintenanceControl.DataSource.Save();

						this.ctrlPhoneListMaintenanceControl.Save();

						FillContactRow(row);
					}
				} 
				else 
				{
					row = this.oContact.dataSet.Contact.NewContactRow();

					this.ctrlAddressMaintenanceControl.DataSource = new Address(this.Page.CurrentTransaction);

					this.ctrlAddressMaintenanceControl.AddressID = 0;
					this.ctrlAddressMaintenanceControl.Save();
					this.ctrlAddressMaintenanceControl.DataSource.Save();

					this.ctrlPhoneListMaintenanceControl.PhoneListID = 0;
					this.ctrlPhoneListMaintenanceControl.Save();

					FillContactRow(row);

					this.oContact.dataSet.Contact.AddContactRow(row);
				}
			} 
			else 
			{
				if(this.ContactID != 0) 
				{
					row = this.oContact.dataSet.Contact.FindById(this.ContactID);

					if(row != null) 
					{
						this.ctrlAddressMaintenanceControl.DataSource = new Address(this.Page.CurrentTransaction);

						this.ctrlAddressMaintenanceControl.DataSource.GetOneByID(row.AddressID);

						this.ctrlAddressMaintenanceControl.Visible = false;
						this.ctrlPhoneListMaintenanceControl.Visible = false;

						this.ctrlAddressMaintenanceControl.Save();
						this.ctrlAddressMaintenanceControl.DataSource.Save();
						this.ctrlPhoneListMaintenanceControl.Save();

						row.Delete();
					}
				}
			}
		}

		private void FillContactRow(ContactDataSet.ContactRow row) 
		{
			row.CAccountID = this.AccountID;
			row.AddressID = this.ctrlAddressMaintenanceControl.DataSource.dataSet.Address[0].address_id;
			row.PhoneListID = this.ctrlPhoneListMaintenanceControl.PhoneListID;
			row.Title = this.Title;
			row.FirstName = this.FirstName;
			row.MiddleInitial = this.MiddleInitial;
			row.LastName = this.LastName;
			row.Email = this.Email;
			if(this.IsPrimary) 
			{
				row.TypeId = Convert.ToInt32(ContactTypeID.Primary);
			} 
			else 
			{
				row.TypeId = Convert.ToInt32(ContactTypeID.Default);
			}
			row.Function = this.Function;
			row.DateChanged = DateTime.Now;
		}

		public void UnbindIDs() 
		{
			this.ContactID = 0;
			this.ctrlAddressMaintenanceControl.AddressID = 0;
			this.ctrlPhoneListMaintenanceControl.UnbindIDs();
		}
	}
}
