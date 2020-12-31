namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSP.WebControl;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class PublisherContactMaintenanceControl : MarketingMgtControl
	{

		protected System.Web.UI.HtmlControls.HtmlTable Table3;
		protected QSPFulfillment.MarketingMgt.Control.PhoneListMaintenanceControl ctrlPhoneListMaintenanceControl;
		protected QSPFulfillment.MarketingMgt.Control.PublisherContactProductListControl ctrlPublisherContactProductListControl;

		public event SelectPublisherContactEventHandler PublisherContactSaved;
		public event System.EventHandler PublisherContactCancelled;

		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlPublisherContactProductListControl.NewContactProductPreClick += new EventHandler(ctrlPublisherContactProductListControl_NewContactProductPreClick);
			this.ctrlPublisherContactProductListControl.NewContactProductClicked += new SelectContactProductEventHandler(ctrlPublisherContactProductListControl_NewContactProductClicked);
			this.ctrlPublisherContactProductListControl.DeleteContactProductClicked += new SelectContactProductEventHandler(ctrlPublisherContactProductListControl_DeleteContactProductClicked);
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

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			SelectPublisherContactClickedArgs args;

			try 
			{
				SavePublisherContactInformations(true);
				args = new SelectPublisherContactClickedArgs(new PublisherContact());

				if(PublisherContactSaved != null)
					PublisherContactSaved(sender, args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			try 
			{
				if(PublisherContactCancelled != null)
					PublisherContactCancelled(sender, e);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void rblMainContact_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try 
			{
				SetVisible();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlPublisherContactProductListControl_NewContactProductPreClick(object sender, EventArgs e)
		{
			SavePublisherContactInformations(false);
			this.ctrlPublisherContactProductListControl.ContactID = PublisherContactInfo.PublisherContactInstance;
		}

		private void ctrlPublisherContactProductListControl_NewContactProductClicked(object sender, SelectContactProductClickedArgs e)
		{
			DataBind();
		}

		private void ctrlPublisherContactProductListControl_DeleteContactProductClicked(object sender, SelectContactProductClickedArgs e)
		{
			DataBind();
		}

		public PublisherContact PublisherContactInfo 
		{
			get 
			{
				return (PublisherContact) this.ViewState["PublisherContactInfo"];
			}
			set 
			{
				this.ViewState["PublisherContactInfo"] = value;
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

		#region Fields

		private string PublisherName 
		{
			get 
			{
				return this.lblPublisherName.Text;
			}
			set 
			{
				this.lblPublisherName.Text = value;
			}
		}

		private string ContactFirstName 
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
		
		private string ContactLastName 
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
		
		private string PositionTitle 
		{
			get 
			{
				return this.tbxPositionTitle.Text;
			}
			set 
			{
				this.tbxPositionTitle.Text = value;
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

		private bool MainContact 
		{
			get 
			{
				return Convert.ToBoolean(this.rblMainContact.SelectedValue);
			}
			set 
			{
				this.rblMainContact.SelectedIndex = this.rblMainContact.Items.IndexOf(this.rblMainContact.Items.FindByValue(value.ToString()));
			}
		}
		
		#endregion

		public override void DataBind()
		{
			if(PublisherContactInfo.PublisherContactInstance != 0)
			{
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}

			this.ctrlPhoneListMaintenanceControl.DataBind();
			this.ctrlPublisherContactProductListControl.DataBind();

			SetVisible();
		}

		private void SetValue() 
		{
			PublisherName = PublisherContactInfo.PublisherName;
			ContactFirstName = PublisherContactInfo.FirstName;
			ContactLastName = PublisherContactInfo.LastName;
			Email = PublisherContactInfo.Email;
			PositionTitle = PublisherContactInfo.PositionTitle;
			MainContact = (MainContactID == PublisherContactInfo.PublisherContactInstance);
			this.ctrlPhoneListMaintenanceControl.PhoneListID = this.PublisherContactInfo.PhoneListID;
			this.ctrlPublisherContactProductListControl.ContactID = PublisherContactInfo.PublisherContactInstance;
			this.ctrlPublisherContactProductListControl.PublisherID = PublisherID;
		}

		private void SetValueEmpty() 
		{
			PublisherName = this.PublisherContactInfo.PublisherName;
			ContactFirstName = String.Empty;
			ContactLastName = String.Empty;
			Email = String.Empty;
			PositionTitle = String.Empty;
			MainContact = (MainContactID == 0);
			this.ctrlPhoneListMaintenanceControl.PhoneListID = 0;
			this.ctrlPublisherContactProductListControl.ContactID = 0;
			this.ctrlPublisherContactProductListControl.PublisherID = PublisherID;
		}

		private void SetVisible() 
		{
			if(MainContactID != 0 && MainContactID != PublisherContactInfo.PublisherContactInstance) 
			{
				this.tableRowMainContact.Visible = false;
				this.tableRowProductCode.Visible = true;
			} 
			else 
			{
				this.tableRowMainContact.Visible = true;
				this.tableRowProductCode.Visible = !MainContact;
			}
		}

		private bool SavePublisherContactInformations(bool checkMainContactValidity) 
		{
			bool CreateNew = false;
			DataTable publisherContactTable;

			if(PublisherContactInfo.PublisherContactInstance != 0) 
			{
				this.Page.BusPublisherContact.Update(PublisherContactInfo.PublisherContactInstance, ContactFirstName, ContactLastName, PositionTitle, Email, MainContact);
				this.ctrlPhoneListMaintenanceControl.Save();

				if(!MainContact && MainContactID == PublisherContactInfo.PublisherContactInstance) 
				{
					MainContactID = 0;
				}
			} 
			else 
			{
				if(!checkMainContactValidity || MainContact) 
				{
					PublisherContactInfo = new PublisherContact();
					publisherContactTable = new DataTable("PublisherContact");

					this.Page.BusPublisherContact.Insert(publisherContactTable, PublisherID, ContactFirstName, ContactLastName, PositionTitle, Email);
					PublisherContactInfo.PublisherContactInstance = Convert.ToInt32(publisherContactTable.Rows[0]["PContact_Instance"]);
					PublisherContactInfo.PhoneListID = Convert.ToInt32(publisherContactTable.Rows[0]["PhoneListID"]);

					this.ctrlPhoneListMaintenanceControl.PhoneListID = PublisherContactInfo.PhoneListID;
					this.ctrlPhoneListMaintenanceControl.Save();

					if(MainContact && MainContactID == 0) 
					{
						MainContactID = PublisherContactInfo.PublisherContactInstance;
					}

					CreateNew = true;
				} 
				else 
				{
					this.Page.MessageManager.Add(Message.ERRMSG_PRODUCT_AT_LEAST_ONE_0);
					this.Page.MessageManager.PrepareErrorMessage();
					throw new ExceptionFulf(this.Page.MessageManager);
				}
			}

			PublisherContactInfo.FirstName = ContactFirstName;
			PublisherContactInfo.LastName = ContactLastName;
			PublisherContactInfo.PositionTitle = PositionTitle;
			PublisherContactInfo.Email = Email;

			return !CreateNew;
		}
	}
}
