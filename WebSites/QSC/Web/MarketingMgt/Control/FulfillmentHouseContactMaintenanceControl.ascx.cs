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
	///		Summary description for FulfillmentHouseContactMaintenanceControl.
	/// </summary>
	public partial class FulfillmentHouseContactMaintenanceControl : MarketingMgtControl
	{
		protected QSPFulfillment.MarketingMgt.Control.FulfillmentHouseContactProductListControl ctrlFulfillmentHouseContactProductListControl;

		public event SelectFulfillmentHouseContactEventHandler FulfillmentHouseContactSaved;
		public event System.EventHandler FulfillmentHouseContactCancelled;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlFulfillmentHouseContactProductListControl.NewContactProductPreClick += new EventHandler(ctrlFulfillmentHouseContactProductListControl_NewContactProductPreClick);
			this.ctrlFulfillmentHouseContactProductListControl.NewContactProductClicked += new SelectContactProductEventHandler(ctrlFulfillmentHouseContactProductListControl_NewContactProductClicked);
			this.ctrlFulfillmentHouseContactProductListControl.DeleteContactProductClicked += new SelectContactProductEventHandler(ctrlFulfillmentHouseContactProductListControl_DeleteContactProductClicked);
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

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			SelectFulfillmentHouseContactClickedArgs args;

			try 
			{
				SaveFulfillmentHouseContactInformations(true);
				args = new SelectFulfillmentHouseContactClickedArgs(new FulfillmentHouseContact());

				if(FulfillmentHouseContactSaved != null)
					FulfillmentHouseContactSaved(sender, args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(FulfillmentHouseContactCancelled != null)
					FulfillmentHouseContactCancelled(sender, e);
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

		private void ctrlFulfillmentHouseContactProductListControl_NewContactProductPreClick(object sender, EventArgs e)
		{
			SaveFulfillmentHouseContactInformations(false);
			this.ctrlFulfillmentHouseContactProductListControl.ContactID = FulfillmentHouseContactInfo.Instance;
		}

		private void ctrlFulfillmentHouseContactProductListControl_NewContactProductClicked(object sender, SelectContactProductClickedArgs e)
		{
			DataBind();
		}

		private void ctrlFulfillmentHouseContactProductListControl_DeleteContactProductClicked(object sender, SelectContactProductClickedArgs e)
		{
			DataBind();
		}

		public FulfillmentHouseContact FulfillmentHouseContactInfo 
		{
			get 
			{
				return (FulfillmentHouseContact) this.ViewState["FulfillmentHouseContactInfo"];
			}
			set 
			{
				this.ViewState["FulfillmentHouseContactInfo"] = value;
			}
		}

		public int FulfillmentHouseID
		{
			get 
			{
				if(this.ViewState["FulfillmentHouseID"] == null)
					this.ViewState["FulfillmentHouseID"] = 0;

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

		#region Fields

		private string FulfillmentHouseName 
		{
			get 
			{
				return this.lblFulfillmentHouseName.Text;
			}
			set 
			{
				this.lblFulfillmentHouseName.Text = value;
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

		private string WorkPhone 
		{
			get 
			{
				return this.tbxWorkPhone.Text;
			}
			set 
			{
				this.tbxWorkPhone.Text = value;
			}
		}

		private string Fax 
		{
			get 
			{
				return this.tbxFax.Text;
			}
			set 
			{
				this.tbxFax.Text = value;
			}
		}

		private string QSPContactFirstName 
		{
			get 
			{
				return this.tbxQSPContactFirstName.Text;
			}
			set 
			{
				this.tbxQSPContactFirstName.Text = value;
			}
		}

		private string QSPContactLastName 
		{
			get 
			{
				return this.tbxQSPContactLastName.Text;
			}
			set 
			{
				this.tbxQSPContactLastName.Text = value;
			}
		}

		private string QSPContactEmail 
		{
			get 
			{
				return this.tbxQSPContactEmail.Text;
			}
			set 
			{
				this.tbxQSPContactEmail.Text = value;
			}
		}

		private string QSPContactPhone 
		{
			get 
			{
				return this.tbxQSPContactPhone.Text;
			}
			set 
			{
				this.tbxQSPContactPhone.Text = value;
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
			if(FulfillmentHouseContactInfo.Instance != 0)
			{
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}

			this.ctrlFulfillmentHouseContactProductListControl.DataBind();

			SetVisible();
		}

		private void SetValue() 
		{
			FulfillmentHouseName = FulfillmentHouseContactInfo.FulfillmentHouseName;
			FirstName = FulfillmentHouseContactInfo.FirstName;
			LastName = FulfillmentHouseContactInfo.LastName;
			Email = FulfillmentHouseContactInfo.Email;
			PositionTitle = FulfillmentHouseContactInfo.PositionTitle;
			WorkPhone = FulfillmentHouseContactInfo.WorkPhone;
			Fax = FulfillmentHouseContactInfo.Fax;
			QSPContactFirstName = FulfillmentHouseContactInfo.CustomerServiceContactFirstName;
			QSPContactLastName = FulfillmentHouseContactInfo.CustomerServiceContactLastName;
			QSPContactEmail = FulfillmentHouseContactInfo.CustomerServiceContactEmail;
			QSPContactPhone = FulfillmentHouseContactInfo.CustomerServiceContactPhone;
			MainContact = (MainContactID == FulfillmentHouseContactInfo.Instance);
			this.ctrlFulfillmentHouseContactProductListControl.ContactID = FulfillmentHouseContactInfo.Instance;
			this.ctrlFulfillmentHouseContactProductListControl.FulfillmentHouseID = FulfillmentHouseID;
		}

		private void SetValueEmpty() 
		{
			FulfillmentHouseName = FulfillmentHouseContactInfo.FulfillmentHouseName;
			FirstName = String.Empty;
			LastName = String.Empty;
			Email = String.Empty;
			PositionTitle = String.Empty;
			WorkPhone = String.Empty;
			Fax = String.Empty;
			QSPContactFirstName = String.Empty;
			QSPContactLastName = String.Empty;
			QSPContactEmail = String.Empty;
			QSPContactPhone = String.Empty;
			MainContact = (MainContactID == 0);
			this.ctrlFulfillmentHouseContactProductListControl.ContactID = 0;
			this.ctrlFulfillmentHouseContactProductListControl.FulfillmentHouseID = FulfillmentHouseID;
		}

		private void SetVisible() 
		{
			if(MainContactID != 0 && MainContactID != FulfillmentHouseContactInfo.Instance) 
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

		private bool SaveFulfillmentHouseContactInformations(bool checkMainContactValidity) 
		{
			
			bool CreateNew = false;

			if(FulfillmentHouseContactInfo.Instance != 0) 
			{
				this.Page.BusFulfillmentHouseContact.Update(FulfillmentHouseContactInfo.Instance, FirstName, LastName, PositionTitle, Email, WorkPhone, Fax, QSPContactFirstName, QSPContactLastName, QSPContactEmail, QSPContactPhone, MainContact);

				if(!MainContact && MainContactID == FulfillmentHouseContactInfo.Instance) 
				{
					MainContactID = 0;
				}
			} 
			else 
			{
				if(!checkMainContactValidity || MainContact) 
				{
					FulfillmentHouseContactInfo = new FulfillmentHouseContact();
					FulfillmentHouseContactInfo.Instance = this.Page.BusFulfillmentHouseContact.Insert(FulfillmentHouseID, FirstName, LastName, PositionTitle, Email, WorkPhone, Fax, QSPContactFirstName, QSPContactLastName, QSPContactEmail, QSPContactPhone);

					if(MainContact && MainContactID == 0) 
					{
						MainContactID = FulfillmentHouseContactInfo.Instance;
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

			FulfillmentHouseContactInfo.FirstName = FirstName;
			FulfillmentHouseContactInfo.LastName = LastName;
			FulfillmentHouseContactInfo.PositionTitle = PositionTitle;
			FulfillmentHouseContactInfo.Email = Email;
			FulfillmentHouseContactInfo.WorkPhone = WorkPhone;
			FulfillmentHouseContactInfo.Fax = Fax;
			FulfillmentHouseContactInfo.CustomerServiceContactFirstName = QSPContactFirstName;
			FulfillmentHouseContactInfo.CustomerServiceContactLastName = QSPContactLastName;
			FulfillmentHouseContactInfo.CustomerServiceContactEmail = QSPContactEmail;
			FulfillmentHouseContactInfo.CustomerServiceContactPhone = QSPContactPhone;

			return !CreateNew;
		}
	}
}
