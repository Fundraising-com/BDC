namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.CommonWeb;
	using Common;
	using Common.TableDef;
	using Business.Objects;
	using dataAccessRef = DAL.AddressData;
    //using AccdataAccessRef = DAL.AccountDataAccess;
	/// <summary>
	///		Summary description for AccountMaintenance.
	/// </summary>
	public partial class AccountMaintenanceControl : AcctMgtControl
	{
		private const string NEW_CAMPAIGN_URL = "CampaignMaintenance.aspx?CampaignID=0&AccountID=";
		private const string TRANSACTION_NAME = "SaveAccount";

		protected QSPFulfillment.AcctMgt.Control.AccountGeneralInformationControl ctrlAccountGeneralInformationControl;
		protected QSPFulfillment.AcctMgt.Control.AddressListMaintenanceControl ctrlAddressListMaintenanceControl;
		public System.Web.UI.WebControls.Button btnSubmitTop;
		public System.Web.UI.WebControls.Button btnSaveNewTop;
		public System.Web.UI.WebControls.Button btnSubmitBottom;
		public System.Web.UI.WebControls.Button btnSaveNewBottom;
		public System.Web.UI.WebControls.Button btnNewCampaignTop;
		public System.Web.UI.WebControls.Button btnNewCampaignBottom;
		protected QSPFulfillment.AcctMgt.Control.PhoneListMaintenanceControl ctrlPhoneListMaintenanceControl;
		protected QSPFulfillment.AcctMgt.Control.VendorMaintenanceControl ctrlVendorMaintenanceControl;

		public event System.EventHandler AccountSaved;
		private Address addr;
		

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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

				this.DataBind();

				if(AccountSaved != null)
					AccountSaved(sender, e);
			} 
			catch (MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();
				
				this.Page.CurrentTransaction = null;
			} 
		}

		protected void btnSaveNew_Click(object sender, System.EventArgs e)
		{
			this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);

			try 
			{
				this.Page.CurrentTransaction.Open();

				SaveNew();

				this.Page.CurrentTransaction.Save();
				
				this.Page.CurrentTransaction = null;

				this.DataBind();

				if(AccountSaved != null)
					AccountSaved(sender, e);
			} 
			catch (MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();
				
				this.Page.CurrentTransaction = null;
			} 
		}

		protected void btnNewCampaign_Click(object sender, EventArgs e)
		{
			//int DupMagCaId;
			string script;
			this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);

			try 
			{
				this.Page.CurrentTransaction.Open();

				Save();

				this.Page.CurrentTransaction.Save();
				
				this.Page.CurrentTransaction = null;

				
				/*if(AccountID != 0) 
				{
				  DupMagCaId = CheckAccountMagCAForDuplicate(); 
				  if (DupMagCaId !=0)
					{
                      Response.Write(DupMagCaId.ToString());
					}
				  else
				  {
					  script  = "<script language=\"javascript\">\n";
					  script += "  window.location = \"" + NEW_CAMPAIGN_URL + this.AccountID.ToString() + "\";\n";
					  script += "</script>\n";

					  this.Page.RegisterClientScriptBlock("NewCampaignForAccount", script);
				  }

				}
				else
				{*/
					script  = "<script language=\"javascript\">\n";
					script += "  window.location = \"" + NEW_CAMPAIGN_URL + this.AccountID.ToString() + "\";\n";
					script += "</script>\n";

					this.Page.RegisterClientScriptBlock("NewCampaignForAccount", script);
				//}
			} 
			catch (MessageException ex) 
			{
				this.Page.SetPageError(ex);
				this.Page.CurrentTransaction.Cancel();
				
				this.Page.CurrentTransaction = null;
			} 
		}

		/*public int CheckAccountMagCAForDuplicate() 
		{
			int MagCAExists= 0 ;
			int MagCampaignID =0;
			if(AccountID != 0) 
			{
				DAL.AccountDataAccess AcctDataAccess = new DAL.AccountDataAccess();
				MagCampaignID= AcctDataAccess.IsMagCAExists(AccountID, 9, out MagCAExists);
			}
			return MagCampaignID;
		}*/
		
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
			if(CheckAccountRights()) 
			{
				
				this.ctrlAccountGeneralInformationControl.AccountID = this.AccountID;
				this.ctrlAccountGeneralInformationControl.DataBind();

				if(this.AccountID != 0)
				{
					SetValue();
				}
				else
				{
					SetValueEmpty();
				}

				this.ctrlAddressListMaintenanceControl.DataBind();
				this.ctrlPhoneListMaintenanceControl.DataBind();

				if(QSPPage.aUserProfile.Roles.Contains("HomeOffice")) 
				{
					this.ctrlVendorMaintenanceControl.AccountID = this.AccountID;
					this.ctrlVendorMaintenanceControl.oCAccount = this.ctrlAccountGeneralInformationControl.oCAccount;
					this.ctrlVendorMaintenanceControl.DataBind();
				}

				SetVisible();
			}
		}

		private bool CheckAccountRights() 
		{
			Campaign oCampaign;
			bool hasRight = true;
			//MS Oct 24, 2007
			//if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999") 
			if(QSPPage.aUserProfile.IsFM || QSPPage.aUserProfile.FMID == "9999") 
			{
				if(AccountID != 0) 
				{
					oCampaign = new Campaign(this.Page.CurrentTransaction);
					oCampaign.GetAllByShipToAccountID(this.AccountID);

					if(oCampaign.dataSet.Campaign.Count > 0) 
					{
						oCampaign.dataSet.Campaign.DefaultView.Sort = oCampaign.dataSet.Campaign.StartDateColumn.ColumnName + " DESC";

                        FieldManager oFieldManager = new FieldManager(this.Page.CurrentTransaction);
                        oFieldManager.GetOneByFMID(((CampaignDataSet.CampaignRow) oCampaign.dataSet.Campaign.DefaultView[0].Row).FMID);

						hasRight = (CheckRights(((CampaignDataSet.CampaignRow) oCampaign.dataSet.Campaign.DefaultView[0].Row).FMID, oFieldManager.dataSet.FieldManager[0].DMID));
					}
				} 
				//else 
				//{
					//RedirectToList();
				//}
			} 

			return hasRight;
		}

		private bool CheckRights(string FMID, string DMID) 
		{
            bool hasRight = (!QSPPage.aUserProfile.IsFM || QSPPage.aUserProfile.FMID == "9999" || QSPPage.aUserProfile.FMID == FMID || QSPPage.aUserProfile.FMID == DMID);

			if(!hasRight) 
			{
				RedirectToList();
			}

			return hasRight;
		}

		private void RedirectToList() 
		{
			Response.Redirect("/QSPFulfillment/AcctMgt/AccountList.aspx");
		}

		private void SetValue() 
		{
			this.lblAccountID.Text = "Group ID: " + this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].Id.ToString();
			this.ctrlAddressListMaintenanceControl.AddressListID = this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].AddressListID;
			this.ctrlPhoneListMaintenanceControl.PhoneListID = this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].PhoneListID;
		}
		
		private void SetValueEmpty() 
		{
			this.lblAccountID.Text = String.Empty;
			this.ctrlAddressListMaintenanceControl.AddressListID = 0;
			this.ctrlPhoneListMaintenanceControl.PhoneListID = 0;
		}

		private void SetVisible() 
		{
			//bool bVisible = (this.AccountID != 0 && !QSPPage.aUserProfile.IsFM);
			// 03/20/2006 - Ben : FMs can now edit their existing group information
			bool bVisible = (this.AccountID != 0);

			this.btnSaveNewTop.Visible = bVisible;
			this.btnSaveNewBottom.Visible = bVisible;
			this.btnNewCampaignTop.Visible = bVisible;
			this.btnNewCampaignBottom.Visible = bVisible;

			/*
			this.btnSubmitTop.Visible = !QSPPage.aUserProfile.IsFM;
			this.btnSubmitBottom.Visible = !QSPPage.aUserProfile.IsFM;
			*/

			this.divVendorInformation.Visible = QSPPage.aUserProfile.Roles.Contains("HomeOffice");
		}

		private void Save() 
		{
			bool isNew = (this.AccountID == 0);

			this.ctrlAccountGeneralInformationControl.Save();

			if(QSPPage.aUserProfile.Roles.Contains("HomeOffice")) 
			{
				this.ctrlVendorMaintenanceControl.AccountID = this.AccountID;
				this.ctrlVendorMaintenanceControl.oCAccount = this.ctrlAccountGeneralInformationControl.oCAccount;
				this.ctrlVendorMaintenanceControl.Save();
			}

			this.ctrlAccountGeneralInformationControl.oCAccount.Save();
			this.AccountID = this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].Id;
			this.ctrlAccountGeneralInformationControl.SetEnabled();
			
			this.ctrlAddressListMaintenanceControl.AddressListID = this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].AddressListID;
			this.ctrlAddressListMaintenanceControl.Save();
			
			//Update Account address after saving in address control
			addr = new Address(this.ctrlAddressListMaintenanceControl.AddressListID, this.Page.CurrentTransaction);
			DataRow row = addr.GetOneByType(AddressType.ShipTo);
			
			this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].Country = row.ItemArray.GetValue(7).ToString();
			this.ctrlAccountGeneralInformationControl.oCAccount.Save();

			this.ctrlPhoneListMaintenanceControl.PhoneListID = this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].PhoneListID;
			this.ctrlPhoneListMaintenanceControl.Save();

			if(isNew) 
			{
				this.ctrlAccountGeneralInformationControl.oCAccount.PopulatePayLaterAccount();
			}
		}

		private void SaveNew() 
		{
			this.ctrlAccountGeneralInformationControl.SaveNew();

			if(QSPPage.aUserProfile.Roles.Contains("HomeOffice")) 
			{
				this.ctrlVendorMaintenanceControl.AccountID = 0;
				this.ctrlVendorMaintenanceControl.oCAccount = this.ctrlAccountGeneralInformationControl.oCAccount;
				this.ctrlVendorMaintenanceControl.Save();
			}

			this.ctrlAccountGeneralInformationControl.oCAccount.Save();
			this.AccountID = this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].Id;
			this.ctrlAccountGeneralInformationControl.AccountID = this.AccountID;
			this.ctrlAccountGeneralInformationControl.SetEnabled();
			
			this.ctrlAddressListMaintenanceControl.AddressListID = this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].AddressListID;
			this.ctrlAddressListMaintenanceControl.SaveNew();

			this.ctrlPhoneListMaintenanceControl.PhoneListID = this.ctrlAccountGeneralInformationControl.oCAccount.dataSet.CAccount[0].PhoneListID;
			this.ctrlPhoneListMaintenanceControl.SaveNew();

			this.ctrlAccountGeneralInformationControl.oCAccount.PopulatePayLaterAccount();
		}
	}
}
