namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common;
	using Common.TableDef;
	using Business.Objects;
	using QSPFulfillment.CommonWeb;
	using DAL;
	/// <summary>
	///		Summary description for AccountMaintenance.
	/// </summary>
	public class CampaignMaintenanceControl : AcctMgtControl
	{
		private const string TRANSACTION_NAME = "SaveCampaign";

		protected System.Web.UI.WebControls.Button btnSubmitBottom;
		protected System.Web.UI.WebControls.Button btnSubmitTop;
		protected System.Web.UI.WebControls.Label lblTitle2;
		protected QSPFulfillment.AcctMgt.Control.CampaignGeneralInformationControl ctrlCampaignGeneralInformationControl;
		protected QSPFulfillment.AcctMgt.Control.CampaignProgramMaintenanceControl ctrlCampaignProgramMaintenanceControl;
		protected QSPFulfillment.AcctMgt.Control.FieldSuppliesMaintenanceControl ctrlFieldSuppliesMaintenanceControl;

		public event System.EventHandler CampaignSaved;

		private Campaign ca;
		private CAccount caccount;
		protected System.Web.UI.WebControls.Button btnSaveNewTop;
		protected System.Web.UI.WebControls.Button btnSaveNewBottom;
		protected System.Web.UI.WebControls.Label lblAccountInfo;
		protected System.Web.UI.WebControls.Label lblCampaignID;
        protected System.Web.UI.WebControls.Label lblOrderCount;
		protected QSPFulfillment.AcctMgt.Control.CASummaryHyperLink hypCASummaryTop;
		protected QSPFulfillment.AcctMgt.Control.CASummaryHyperLink hypCASummaryBottom;
		protected QSPFulfillment.AcctMgt.Control.ConfirmationAgreementLinkButton hypCATop;
		protected QSPFulfillment.AcctMgt.Control.ConfirmationAgreementLinkButton hypCABottom;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divReportLinksTop;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divReportLinksBottom;
		private CampaignDataSet.CampaignRow row;
		
		/*public int ProgramID 
		{
			get 
			{
				if(this.ViewState["ProgramID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["ProgramID"]);
			}
			set 
			{
				this.ViewState["ProgramID"] = value;
			}
		}*/

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//this.btnSubmitTop.Enabled=true;
			//this.btnSubmitBottom.Enabled=true;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlCampaignGeneralInformationControl.StaffOrderStateChanged += new EventHandler(ctrlCampaignGeneralInformationControl_StaffOrderStateChanged);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSubmitTop.Click += new System.EventHandler(this.btnSubmit_Click);
			this.btnSaveNewTop.Click += new System.EventHandler(this.btnSaveNew_Click);
			this.btnSubmitBottom.Click += new System.EventHandler(this.btnSubmit_Click);
			this.btnSaveNewBottom.Click += new System.EventHandler(this.btnSaveNew_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{	//int OnlineExists = this.OrderExists();
			//int MagCaId;
			// MagCaId = CheckAccountMagCAForDuplicate(); 

			//bool MagSelected;
			//MagSelected = this.ctrlCampaignProgramMaintenanceControl.MagProgramRunning();

			/*if(OnlineExists>0  && ctrlCampaignGeneralInformationControl.Status==37005)
			{
				this.Page.CurrentMessageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
                this.Page.CurrentMessageManager.Add(this.Page.CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_CANNOT_CANCEL_A_CAMPAIGN_WITH_ORDERS, Convert.ToString(this.ctrlCampaignGeneralInformationControl.CampaignID)));
			
			}*/

			this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);
			try 
			{
				this.Page.CurrentTransaction.Open();

				Save();

				this.Page.CurrentTransaction.Save();
				
				this.Page.CurrentTransaction = null;

				this.DataBind();

				if(CampaignSaved != null)
					CampaignSaved(sender, e);
			} 
			catch(Exception ex) 
			{
				//bool MagProgSelected;
				//MagProgSelected = this.ctrlCampaignProgramMaintenanceControl.MagProgramRunning();

				//if(!(ex is MessageException))//  ||(MagProgSelected &&  MagCaId !=0)) 
				//if (!(ex is MessageException) || (OnlineExists >0 && ctrlCampaignGeneralInformationControl.Status==37005))
				if (!(ex is MessageException))
                {
					this.Page.CurrentTransaction.Cancel();
					this.Page.CurrentTransaction = null;
					//this.btnSubmitTop.Enabled=false;
					//this.btnSubmitBottom.Enabled=false;
				} 
				else 
				{
					this.Page.CurrentTransaction.Save();
					this.Page.CurrentTransaction = null;

					this.DataBind();

					if(CampaignSaved != null)
						CampaignSaved(sender, e);
				}

				this.Page.ManageError(ex);
			} 
		}

		private void btnSaveNew_Click(object sender, System.EventArgs e)
		{
			this.Page.CurrentTransaction = new Transaction(TRANSACTION_NAME, DataBaseName.QSPCanadaCommon);

			try 
			{
				this.Page.CurrentTransaction.Open();

				SaveNew();

				this.Page.CurrentTransaction.Save();
				
				this.Page.CurrentTransaction = null;

				this.DataBind();

				if(CampaignSaved != null)
					CampaignSaved(sender, e);
			} 
			catch(Exception ex) 
			{
				//bool MagProgSelected;
				//MagProgSelected = this.ctrlCampaignProgramMaintenanceControl.MagProgramRunning();

				if(!(ex is MessageException))//  || (MagProgSelected &&  MagCaId !=0))
				
				{
					this.Page.CurrentTransaction.Cancel();
					this.Page.CurrentTransaction = null;
					//this.btnSubmitTop.Enabled=false;
					//this.btnSubmitBottom.Enabled=false;
				} 
				else 
				{
					this.Page.CurrentTransaction.Save();
					this.Page.CurrentTransaction = null;
					this.DataBind();

					if(CampaignSaved != null)
						CampaignSaved(sender, e);
				}

				this.Page.ManageError(ex);
			} 
		}

		private void ctrlCampaignGeneralInformationControl_StaffOrderStateChanged(object sender, EventArgs e)
		{
			SetValueStaffOrder();
			this.ctrlCampaignProgramMaintenanceControl.DataBind();
		}

		private Campaign oCampaign
		{
			get 
			{
				return ca;
			}
			set 
			{
				ca = value;
			}
		}

		private CAccount oCAccount 
		{
			get 
			{
				return caccount;
			}
			set 
			{
				caccount = value;
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

		public override void DataBind()
		{
			try 
			{
				LoadData();
			
				this.ctrlCampaignGeneralInformationControl.DataBind();

				SetValueStaffOrder();

				this.ctrlCampaignProgramMaintenanceControl.DataBind();
				this.ctrlFieldSuppliesMaintenanceControl.DataBind();
				this.hypCATop.DataBind();
				this.hypCABottom.DataBind();
				this.hypCASummaryTop.DataBind();
				this.hypCASummaryBottom.DataBind();

				SetVisible();
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadData() 
		{
			if(this.CampaignID != 0)
			{
				LoadDataCampaign();
			}
			else if(this.AccountID != 0)
			{
				LoadDataAccount();
			} 
			else 
			{
				RedirectToList();
			}
		}

		private void LoadDataCampaign() 
		{
			oCampaign = new Campaign(CampaignID, this.Page.CurrentTransaction);
            FieldManager oFieldManager = new FieldManager(this.Page.CurrentTransaction);
            oFieldManager.GetOneByFMID(oCampaign.dataSet.Campaign[0].FMID);

            if (CheckRights(oCampaign.dataSet.Campaign[0].FMID, oFieldManager.dataSet.FieldManager[0].DMID)) 
			{
				oCAccount = new CAccount(oCampaign.dataSet.Campaign[0].ShipToAccountID, this.Page.CurrentTransaction);

				SetValue();
			}
		}

		private void LoadDataAccount() 
		{
			if(CheckAccountRights()) 
			{
				oCAccount = new CAccount(this.AccountID, this.Page.CurrentTransaction);

				SetValueEmpty();
			}
		}

		private bool CheckAccountRights() 
		{
			bool hasRight = true;

            //2009-06-02: Anyone can create a new campaign for any account
			/*if(QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999") 
			{
				oCampaign = new Campaign(this.Page.CurrentTransaction);
				oCampaign.GetAllByShipToAccountID(this.AccountID);

				if(oCampaign.dataSet.Campaign.Count > 0) 
				{
					oCampaign.dataSet.Campaign.DefaultView.Sort = oCampaign.dataSet.Campaign.StartDateColumn.ColumnName + " DESC";

					hasRight = (CheckRights(((CampaignDataSet.CampaignRow) oCampaign.dataSet.Campaign.DefaultView[0].Row).FMID));
				} 
			}*/

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
			this.lblAccountInfo.Text = "Group ID: " + oCAccount.dataSet.CAccount[0].Id.ToString() + " - " + oCAccount.dataSet.CAccount[0].Name;
			this.lblCampaignID.Text = "Campaign ID: " + oCampaign.dataSet.Campaign[0].ID.ToString();
            this.lblOrderCount.Text = "Order Count: " + OrderExists();

			this.ctrlCampaignGeneralInformationControl.CampaignID = this.CampaignID;
			this.ctrlCampaignGeneralInformationControl.oCampaign = this.oCampaign;
			this.ctrlCampaignGeneralInformationControl.SelectedAccountID = oCampaign.dataSet.Campaign[0].ShipToAccountID;

			this.ctrlCampaignProgramMaintenanceControl.CampaignID = this.CampaignID;
			this.ctrlFieldSuppliesMaintenanceControl.oCampaign = this.oCampaign;

			this.hypCATop.Visible = true;
			this.hypCABottom.Visible = true;
			this.hypCATop.CampaignID = this.CampaignID;
			this.hypCABottom.CampaignID = this.CampaignID;
			this.hypCASummaryTop.CampaignID = this.CampaignID;
			this.hypCASummaryBottom.CampaignID = this.CampaignID;

			row = oCampaign.dataSet.Campaign[0];
		}

		private void SetValueEmpty() 
		{
			this.lblAccountInfo.Text = "Group ID: " + oCAccount.dataSet.CAccount[0].Id.ToString() + " - " + oCAccount.dataSet.CAccount[0].Name;
			this.lblCampaignID.Text = String.Empty;

			this.ctrlCampaignGeneralInformationControl.CampaignID = 0;
			this.ctrlCampaignGeneralInformationControl.oCampaign = null;
			this.ctrlCampaignGeneralInformationControl.SelectedAccountID = this.AccountID;

			this.ctrlCampaignProgramMaintenanceControl.CampaignID = 0;
			this.ctrlFieldSuppliesMaintenanceControl.oCampaign = null;

			row = null;
			this.hypCATop.Visible = false;
			this.hypCABottom.Visible = false;
			this.hypCASummaryTop.HRef = String.Empty;
			this.hypCASummaryBottom.HRef = String.Empty;
		}

		private void SetValueStaffOrder() 
		{
			this.ctrlCampaignProgramMaintenanceControl.IsStaffOrder = this.ctrlCampaignGeneralInformationControl.IsStaffOrder;
		}

		private void SetVisible() 
		{
			bool bVisible = (this.CampaignID != 0);
			/* 03/17/2006 - Ben : FMs can now save their own CAs
			//bool bVisibleFM = !QSPPage.aUserProfile.IsFM; MS Feb28
			bool bVisibleFM = !QSPPage.aUserProfile.IsFM || QSPPage.aUserProfile.FMID == "9999";
			*/

			this.divReportLinksTop.Visible = bVisible;
			this.divReportLinksBottom.Visible = bVisible;
			this.btnSaveNewTop.Visible = bVisible;
			this.btnSaveNewBottom.Visible = bVisible;

			//11/07/2006 - Jeff: Once campaign end date reached, don't let FM's save campaign
			bool bIsFM = QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999";
			DateTime campaignEndDate = this.ctrlCampaignGeneralInformationControl.EndDate;
			bool campaignEndDateReached = (campaignEndDate != new DateTime(1995, 1, 1) && (campaignEndDate < DateTime.Now.AddDays(-1)));
			bool fieldSuppliesGenerated = (!this.ctrlFieldSuppliesMaintenanceControl.EditMode);
			bool fieldSuppliesDelivered = ((this.ctrlFieldSuppliesMaintenanceControl.DeliveryDate < DateTime.Now.AddDays(-1))
				&& this.ctrlFieldSuppliesMaintenanceControl.FieldSuppliesRequired);

			//06/14/2007 - MS If Campaign is not approved FM should be able to update
            //bool bIsApproved;
            /*if (bVisible)
            {
                bIsApproved = (this.oCampaign.dataSet.Campaign[0].Status == Convert.ToInt32(CampaignStatus.Approved));
            }
            else 
            {
                bIsApproved = false;
            }*/

			//if (campaignEndDateReached && bIsFM && bIsApproved)
			//MS June 03 if ((campaignEndDateReached && bIsFM) || (bIsApproved && bIsFM))
			//
            /*****
             * 
             * 
			if (!campaignEndDateReached)
			{	
				if (fieldSuppliesGenerated && fieldSuppliesDelivered)	
				{
					this.btnSubmitTop.Enabled = false;
					this.btnSubmitBottom.Enabled = false;
				}
				else
				{
					//this.btnSubmitTop.Enabled = false;
					//this.btnSubmitBottom.Enabled = false;
					this.btnSubmitTop.Enabled = true;
					this.btnSubmitBottom.Enabled = true;
				}

			}
			else
			{
				this.btnSubmitTop.Enabled = false;
				this.btnSubmitBottom.Enabled = false;
				//this.btnSubmitTop.Enabled = true;
				//this.btnSubmitBottom.Enabled = true;
			}
            */
            this.btnSubmitTop.Enabled = true;
            this.btnSubmitBottom.Enabled = true;
		}

		
		private void Save() 
		{
			//bool MagProgramSelected ;
			//int  MagCampaignId;

			//MS Sept 11,2003 Issue#2687	
			//MagCampaignId = CheckAccountMagCAForDuplicate();

			//int a = this.DupMagazineCampaignID;

			oCampaign = new Campaign(CampaignID, this.Page.CurrentMessageManager, this.Page.CurrentTransaction);

			this.ctrlCampaignGeneralInformationControl.CampaignID = this.CampaignID;
			this.ctrlCampaignGeneralInformationControl.oCampaign = this.oCampaign;
			this.ctrlCampaignGeneralInformationControl.Save();

			this.ctrlFieldSuppliesMaintenanceControl.oCampaign = this.oCampaign;
			this.ctrlFieldSuppliesMaintenanceControl.Save();

			try 
			{
				oCampaign.Save();
			} 
			// 03/26/2006 - Ben :	Tweak to save CA but not show errors before
			//						all is saved.
			catch(MessageException) { }

			this.CampaignID = oCampaign.dataSet.Campaign[0].ID;

			this.ctrlCampaignGeneralInformationControl.CampaignID = this.CampaignID;
			this.ctrlCampaignProgramMaintenanceControl.CampaignID = this.CampaignID;
			this.ctrlCampaignProgramMaintenanceControl.Save();
			
			//MS Sept 14, 2007 Issue#2687
			//MagProgramSelected = this.ctrlCampaignProgramMaintenanceControl.MagProgramRunning();

			//if (MagCampaignId !=0 && MagProgramSelected)
			//{
			//	this.Page.CurrentMessageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
			//	this.Page.CurrentMessageManager.Add(this.Page.CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_ACCOUNT_MAG_CA_EXIST_1, Convert.ToString(MagCampaignId)));
			//}

			//if (oCampaign.dataSet.Campaign.Count > 0)
			//{
			row = oCampaign.dataSet.Campaign[0];
			//}
			SetVisible();

			if ((this.Page.CurrentMessageManager.Count > 0)) 
			{
				this.Page.CurrentMessageManager.PrepareErrorMessage();
				throw new MessageException(this.Page.CurrentMessageManager);
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

		public int OrderExists()
		{
			//Check if OnlineOrder Exists if so add exception message
			DataTable CAs = new DataTable();
			DAL.CampaignDataAccess OnlineCAs =	new DAL.CampaignDataAccess();
			CAs = OnlineCAs.GetCAOrderCount(this.CampaignID); 

			 return(Convert.ToInt32(CAs.Rows[0]["cnt"]));
			 
			//int a =  Convert.ToInt32(CAs.Rows[0]["cnt"]);
			//if (a !=0 && ctrlCampaignGeneralInformationControl.Status==37005)
			//{
			//this.Page.CurrentMessageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
			//this.Page.CurrentMessageManager.Add(this.Page.CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_ACCOUNT_MAG_CA_EXIST_1, Convert.ToString(CampaignID)));
		} 
	

		private void SaveNew() 
		{
			//bool MagProgramSelected ;
			//int  MagCampaignId;

			//MS Sept 11,2003 Issue#2687	
			//MagCampaignId = CheckAccountMagCAForDuplicate();

			oCampaign = new Campaign(this.Page.CurrentMessageManager, this.Page.CurrentTransaction);

			this.ctrlCampaignGeneralInformationControl.CampaignID = 0;
			this.ctrlCampaignGeneralInformationControl.oCampaign = this.oCampaign;
			this.ctrlCampaignGeneralInformationControl.SaveNew();

			this.ctrlFieldSuppliesMaintenanceControl.oCampaign = this.oCampaign;
			this.ctrlFieldSuppliesMaintenanceControl.Save();

			CloneCampaignContact();

			try 
			{
				oCampaign.Save();
			} 
			// 03/26/2006 - Ben :	Tweak to save CA but not show errors before
			//						all is saved.
			catch(MessageException) { }

			this.CampaignID = oCampaign.dataSet.Campaign[0].ID;

			this.ctrlCampaignGeneralInformationControl.CampaignID = this.CampaignID;
			this.ctrlCampaignProgramMaintenanceControl.CampaignID = this.CampaignID;

			this.ctrlCampaignProgramMaintenanceControl.Save();

			//MS Sept 14, 2007 Issue#2687
			//MagProgramSelected = this.ctrlCampaignProgramMaintenanceControl.MagProgramRunning();

			//if (MagCampaignId !=0 && MagProgramSelected)
			//{
			//	this.Page.CurrentMessageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
			//	this.Page.CurrentMessageManager.Add(this.Page.CurrentMessageManager.FormatErrorMessage(Message.ERRMSG_ACCOUNT_MAG_CA_EXIST_1, Convert.ToString(MagCampaignId)));
			//}

			
			row = oCampaign.dataSet.Campaign[0];

			SetVisible();

			if(this.Page.CurrentMessageManager.Count > 0) 
			{
				this.Page.CurrentMessageManager.PrepareErrorMessage();
				throw new MessageException(this.Page.CurrentMessageManager);
			}
		}

		private void CloneCampaignContact() 
		{
			Campaign oBaseCampaign;
			Contact oContact;
			CampaignDataSet.CampaignRow rowBaseCampaign;
			CampaignDataSet.CampaignRow rowNewCampaign;
			
			oBaseCampaign = new Campaign(this.CampaignID, this.Page.CurrentTransaction);

			if(oBaseCampaign.dataSet.Campaign.Count == 1) 
			{
				rowBaseCampaign = oBaseCampaign.dataSet.Campaign[0];
				rowNewCampaign = oCampaign.dataSet.Campaign[0];

				oContact = new Contact(this.Page.CurrentTransaction);
				oContact.GetOneByID(rowBaseCampaign.ShipToCampaignContactID);

				rowNewCampaign.ShipToCampaignContactID = oContact.Clone(rowBaseCampaign.ShipToCampaignContactID);

				if(rowBaseCampaign.ShipToCampaignContactID != rowBaseCampaign.BillToCampaignContactID) 
				{
					oContact.GetOneByID(rowBaseCampaign.BillToCampaignContactID);

					rowNewCampaign.BillToCampaignContactID = oContact.Clone(rowBaseCampaign.BillToCampaignContactID);
				} 
				else 
				{
					rowNewCampaign.BillToCampaignContactID = rowNewCampaign.ShipToCampaignContactID;
				}
			}
		}
	}
}
