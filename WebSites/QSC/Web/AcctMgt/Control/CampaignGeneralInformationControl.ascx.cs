namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;
	using Common;
	using Common.TableDef;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for AccountGeneralInformationControl.
	/// </summary>
	public partial class CampaignGeneralInformationControl : AcctMgtControl
	{
		private const int INCENTIVES_BILL_TO_DEFAULT = 51004;
	
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteStartDate;
		protected QSPFulfillment.CommonWeb.UC.DateEntry dteEndDate;
        protected QSPFulfillment.CommonWeb.UC.DateEntry dteCookieDoughDeliveryDate;
        protected QSPFulfillment.CommonWeb.UC.DateEntry dteDateSubmitted;
		protected QSP.WebControl.TextBoxInteger tbxNbrOfClassrooms;
		protected QSP.WebControl.TextBoxInteger tbxNumbrOfParticipants;
		protected QSP.WebControl.TextBoxInteger tbxNumeebrOfStaff;
		protected QSPFulfillment.AcctMgt.Control.AddressViewerControl ctrlAddressViewerControlShipTo;
		protected QSPFulfillment.AcctMgt.Control.AddressViewerControl ctrlAddressViewerControlBillTo;
		protected QSPFulfillment.AcctMgt.Control.ContactMaintenanceControl ctrlContactMaintenanceControl;

		private Campaign ca;
		private CAccount oShipToAccount;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label8;
		protected QSP.WebControl.TextBoxInteger tbxExtra1ups;
		protected QSP.WebControl.TextBoxInteger tbxExtraGiftForm;
      protected QSP.WebControl.TextBoxInteger tbxCoolCardsBoxes;
      
        protected System.Web.UI.WebControls.Label Label19;
		protected QSP.WebControl.RadioButtonListItemAttributes rblOnlineOnly;
		private Contact contact;

		public event System.EventHandler StaffOrderStateChanged;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddJavaScript();
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
			this.rblStaffOrder.SelectedIndexChanged += new System.EventHandler(this.rblStaffOrder_SelectedIndexChanged);
			this.rblOnlineOnly.SelectedIndexChanged += new System.EventHandler(this.rblOnlineOnly_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		protected void rblStaffOrder_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(StaffOrderStateChanged != null)
				StaffOrderStateChanged(sender, e);
		}
		private void rblOnlineOnly_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		#region Fields

		private string FMID 
		{
			get 
			{
				return this.ddlFieldManager.SelectedValue;
			}
			set 
			{
				this.ddlFieldManager.SelectedIndex = this.ddlFieldManager.Items.IndexOf(this.ddlFieldManager.Items.FindByValue(value));
			}
		}

		private string Language 
		{
			get 
			{
				return this.ddlLanguage.SelectedValue;
			}
			set 
			{
				this.ddlLanguage.SelectedIndex = this.ddlLanguage.Items.IndexOf(this.ddlLanguage.Items.FindByValue(value));
			}
		}

		private bool StaffOrder
		{
			get 
			{
				return Convert.ToBoolean(this.rblStaffOrder.SelectedValue);
			}
			set 
			{
				this.rblStaffOrder.SelectedIndex = this.rblStaffOrder.Items.IndexOf(this.rblStaffOrder.Items.FindByValue(value.ToString()));
			}
		}

		public bool OnlineOnly
		{
			get 
			{
				return Convert.ToBoolean(this.rblOnlineOnly.SelectedValue);
			}
			set 
			{
				this.rblOnlineOnly.SelectedIndex = this.rblOnlineOnly.Items.IndexOf(this.rblOnlineOnly.Items.FindByValue(value.ToString()));
			}
		}

      private bool OnlineNutFree
      {
         get
         {
            return Convert.ToBoolean(this.rblOnlineNutFree.SelectedValue);
         }
         set
         {
            this.rblOnlineNutFree.SelectedIndex = this.rblOnlineNutFree.Items.IndexOf(this.rblOnlineNutFree.Items.FindByValue(value.ToString()));
         }
      }

        private bool OnlineMagazineTRTOnly
        {
            get
            {
                return Convert.ToBoolean(this.rblOnlineMagazineTRTOnly.SelectedValue);
            }
            set
            {
                this.rblOnlineMagazineTRTOnly.SelectedIndex = this.rblOnlineMagazineTRTOnly.Items.IndexOf(this.rblOnlineMagazineTRTOnly.Items.FindByValue(value.ToString()));
            }
        }

        private int BillIncentivesTo
		{
			get 
			{
				return Convert.ToInt32(this.ddlIncentivesBillTo.SelectedValue);
			}
			set 
			{
				this.ddlIncentivesBillTo.SelectedIndex = this.ddlIncentivesBillTo.Items.IndexOf(this.ddlIncentivesBillTo.Items.FindByValue(value.ToString()));
			}
		}

		private int IncentivesDistribution 
		{
			get 
			{
				return Convert.ToInt32(this.ddlIncentivesDistribution.SelectedValue);
			}
			set 
			{
				this.ddlIncentivesDistribution.SelectedIndex = this.ddlIncentivesDistribution.Items.IndexOf(this.ddlIncentivesDistribution.Items.FindByValue(value.ToString()));
			}
		}

		public int Status 
		{
			get 
			{
				return Convert.ToInt32(this.ddlStatus.SelectedValue);
			}
			set 
			{
				this.ddlStatus.SelectedIndex = this.ddlStatus.Items.IndexOf(this.ddlStatus.Items.FindByValue(value.ToString()));
			}
		}

		private bool Renewal 
		{
			get 
			{
				return Convert.ToBoolean(this.rblRenewalStatus.SelectedValue);
			}
			set 
			{
				this.rblRenewalStatus.SelectedIndex = this.rblRenewalStatus.Items.IndexOf(this.rblRenewalStatus.Items.FindByValue(value.ToString()));
			}
		}

		private DateTime StartDate 
		{
			get 
			{
				DateTime dtStartDate;

				if(this.dteStartDate.Date != DateTime.MinValue) 
				{
					dtStartDate = this.dteStartDate.Date;
				} 
				else 
				{
					dtStartDate = new DateTime(1995, 1, 1);
				}

				return dtStartDate;
			}
			set
			{
				if(value != new DateTime(1995, 1, 1)) 
				{
					this.dteStartDate.Date = value;
				} 
				else 
				{
					this.dteStartDate.ClearDate();
				}
			}
		}

		public DateTime EndDate 
		{
			get 
			{
				DateTime dtEndDate;

				if(this.dteEndDate.Date != DateTime.MinValue) 
				{
					dtEndDate = dteEndDate.Date;
				} 
				else 
				{
					dtEndDate = new DateTime(1995, 1, 1);
				}

				return dtEndDate;
			}
			set
			{
				if(value != new DateTime(1995, 1, 1)) 
				{
					this.dteEndDate.Date = value;
				} 
				else 
				{
					this.dteEndDate.ClearDate();
				}
			}
		}

        public DateTime CookieDoughDeliveryDate
        {
            get
            {
                DateTime dtCookieDoughDeliveryDate;

                if (this.dteCookieDoughDeliveryDate.Date != DateTime.MinValue)
                {
                    dtCookieDoughDeliveryDate = dteCookieDoughDeliveryDate.Date;
                }
                else
                {
                    dtCookieDoughDeliveryDate = new DateTime(1995, 1, 1);
                }

                return dtCookieDoughDeliveryDate;
            }
            set
            {
                if (value != new DateTime(1995, 1, 1))
                {
                    this.dteCookieDoughDeliveryDate.Date = value;
                }
                else
                {
                    this.dteCookieDoughDeliveryDate.ClearDate();
                }
            }
        }

		private double EstimatedGross
		{
			get 
			{
				double dEstimatedGross = 0.0;

				try 
				{
					dEstimatedGross = Convert.ToDouble(this.tbxEstimatedGross.Text);
				} 
				catch { }

				return dEstimatedGross;
			}
			set 
			{
				if(value != 0.0) 
				{ 
					this.tbxEstimatedGross.Text = value.ToString("#,#.00");
				} 
				else 
				{
					this.tbxEstimatedGross.Text = String.Empty;
				}
			}
		}

		private int NumberOfParticipants
		{
			get 
			{
				int iNumberOfParticipants = 0;

				try 
				{
					iNumberOfParticipants = Convert.ToInt32(this.tbxNumberOfParticipants.Text);
				} 
				catch { }

				return iNumberOfParticipants;
			}
			set 
			{
				if(value != 0) 
				{
					this.tbxNumberOfParticipants.Text = value.ToString();
				} 
				else 
				{
					this.tbxNumberOfParticipants.Text = String.Empty;
				}
			}
		}

		private int NumberOfClassrooms
		{
			get 
			{
				int iNumberOfClassrooms = -1;

				try 
				{
					iNumberOfClassrooms = Convert.ToInt32(this.tbxNumberOfClassrooms.Text);
				} 
				catch { }

				return iNumberOfClassrooms;
			}
			set  
			{
				if(value != -1) 
				{
					this.tbxNumberOfClassrooms.Text = value.ToString();
				} 
				else 
				{
					this.tbxNumberOfClassrooms.Text = String.Empty;
				}
			}
		}
		private int NumberOfStaff 
		{
			get 
			{
				int iNumberOfStaff = 0;

				try 
				{
					iNumberOfStaff = Convert.ToInt32(this.tbxNumberOfStaff.Text);
				} 
				catch { }

				return iNumberOfStaff;
			}
			set 
			{
				if(value != 0) 
				{
					this.tbxNumberOfStaff.Text = value.ToString();
				} 
				else 
				{
					this.tbxNumberOfStaff.Text = String.Empty;
				}
			}
		}

		private int Extra1Ups 
		{
			get 
			{
				int iExtra1Ups = 0;

				try 
				{
					iExtra1Ups = Convert.ToInt32(this.tbxExtra1ups.Text);
					if (iExtra1Ups >5000)
					{
					 iExtra1Ups=5000;
					}
				} 
				catch { }

				return iExtra1Ups;
			}
			set 
			{
				if(value != 0) 
				{
					this.tbxExtra1ups.Text = value.ToString();
				} 
				else 
				{
					this.tbxExtra1ups.Text = String.Empty;
				}
			}
		}

		private int ExtraGiftForm
		{
			get 
			{
				int iExtraGiftForm = 0;

				try 
				{
					iExtraGiftForm = Convert.ToInt32(this.tbxExtraGiftForm.Text);

					if (iExtraGiftForm >5000)
					{
						iExtraGiftForm=5000;
					}
				} 
				catch { }

				return iExtraGiftForm;
			}
			set 
			{
				if(value != 0) 
				{
					this.tbxExtraGiftForm.Text = value.ToString();
				} 
				else 
				{
					this.tbxExtraGiftForm.Text = String.Empty;
				}
			}
		}

        private int ExtraMagBrochure
        {
            get
            {
                int iExtraMagBrochure = 0;

                try
                {
                    iExtraMagBrochure = Convert.ToInt32(this.tbxExtraMagBrochure.Text);

                    if (iExtraMagBrochure > 5000)
                    {
                        iExtraMagBrochure = 5000;
                    }
                }
                catch { }

                return iExtraMagBrochure;
            }
            set
            {
                if (value != 0)
                {
                    this.tbxExtraMagBrochure.Text = value.ToString();
                }
                else
                {
                    this.tbxExtraMagBrochure.Text = String.Empty;
                }
            }
        }

      private int CoolCardsBoxes
      {
         get
         {
            return tbxCoolCardsBoxes.Value;
         }
         set
         {
            if (value == -1)
            {
               this.tbxCoolCardsBoxes.Text = String.Empty;
            }
            else
            {
               this.tbxCoolCardsBoxes.Text = value.ToString();
            }
         }
      }

      private string SpecialInstructions 
		{
			get
			{
				return this.tbxSpecialInstructions.Text;
			}
			set 
			{
				this.tbxSpecialInstructions.Text = value;
			}
		}

		private DateTime DateSubmitted 
		{
			get 
			{
				if(this.dteDateSubmitted.Date != DateTime.MinValue) 
				{
					return this.dteDateSubmitted.Date;
				} 
				else 
				{
					return new DateTime(1995, 1, 1);
				}
			}
			set 
			{
				if(value != new DateTime(1995, 1, 1)) 
				{
					this.dteDateSubmitted.Date = value;
				} 
				else 
				{
					this.dteDateSubmitted.ClearDate();
				}
			}
		}

        private bool ForceStatementPrint
        {
            get
            {
                return this.ForceStatementPrintCheckBox.Checked;
            }
            set
            {
                this.ForceStatementPrintCheckBox.Checked = value;
            }
        }

        private bool DisableStatementPrint
        {
            get
            {
                return this.DisableStatementPrintCheckBox.Checked;
            }
            set
            {
                this.DisableStatementPrintCheckBox.Checked = value;
            }
        }

        private string Notes
        {
            get
            {
                return this.tbxNotes.Text;
            }
            set
            {
                this.tbxNotes.Text = value;
            }
        }

		#endregion

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

		public int SelectedAccountID 
		{
			get 
			{
				if(this.ViewState["SelectedAccountID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["SelectedAccountID"]);
			}
			set 
			{
				this.ViewState["SelectedAccountID"] = value;
			}
		}

		public Campaign oCampaign
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

		public bool IsStaffOrder 
		{
			get 
			{
				return this.StaffOrder;
			}
		}

		public bool OnlineOnlyPrograms 
		{
			get 
			{
				return this.OnlineOnly;
			}
		}

		private string LastFMIDState
		{
			get 
			{
				if(Session["LastFMID"] == null)
					return String.Empty;

				return Session["LastFMID"].ToString();
			}
			set 
			{
				Session["LastFMID"] = value;
			}
		}

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript ();

			AddJavaScriptDefaultContactLanguage();
		}

		private void AddJavaScriptDefaultContactLanguage() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function SetDefaultContactLanguage() {\n";
			script += "    var ddlLanguage = document.getElementById(\"" + this.ddlLanguage.ClientID + "\");\n";
			script += "    var language = ddlLanguage.options[ddlLanguage.selectedIndex].value;\n";
			script += "    var contactFirstName = document.getElementById(\"" + this.ctrlContactMaintenanceControl.FirstNameControl.ClientID + "\").value;\n";
			script += "    var contactLastName = document.getElementById(\"" + this.ctrlContactMaintenanceControl.LastNameControl.ClientID + "\").value;\n";

			script += "    if(language == \"FR\" && contactFirstName == \"" + Contact.GetDefaultContactFirstName("EN") + "\" && contactLastName == \"" + Contact.GetDefaultContactLastName("EN") + "\") {\n";
			script += "      document.getElementById(\"" + this.ctrlContactMaintenanceControl.FirstNameControl.ClientID + "\").value = \"" + Contact.GetDefaultContactFirstName("FR") + "\";\n";
			script += "      document.getElementById(\"" + this.ctrlContactMaintenanceControl.LastNameControl.ClientID + "\").value = \"" + Contact.GetDefaultContactLastName("FR") + "\";\n";
			script += "    } else if(language != \"FR\" && contactFirstName == \"" + Contact.GetDefaultContactFirstName("FR") + "\" && contactLastName == \"" + Contact.GetDefaultContactLastName("FR") + "\") {\n";
			script += "      document.getElementById(\"" + this.ctrlContactMaintenanceControl.FirstNameControl.ClientID + "\").value = \"" + Contact.GetDefaultContactFirstName("EN") + "\";\n";
			script += "      document.getElementById(\"" + this.ctrlContactMaintenanceControl.LastNameControl.ClientID + "\").value = \"" + Contact.GetDefaultContactLastName("EN") + "\";\n";
			script += "    }\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("SetDefaultContactLanguage", script);
			this.ddlLanguage.Attributes["onChange"] = "SetDefaultContactLanguage()";
		}

		#endregion

		public override void DataBind()
		{
			LoadData();

			if(this.CampaignID != 0) 
			{
				this.ctrlContactMaintenanceControl.DataBind();
			}
			this.ctrlAddressViewerControlShipTo.DataBind();
			this.ctrlAddressViewerControlBillTo.DataBind();
		}

		private void LoadData() 
		{
			try 
			{
				LoadDataDDL();

				if(this.CampaignID != 0 && oCampaign != null) 
				{
					oShipToAccount = oCampaign.GetShipToAccount(this.CampaignID);
					LoadDataContact();
					
					SetValue();
				}
				else 
				{
					SetValueEmpty();

					DataBindLanguage();
					DataBindEmptyContact();
				}

				SetEnabled();
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadDataContact() 
		{
			contact = new Contact(this.Page.CurrentMessageManager, this.Page.CurrentTransaction);
			contact.GetOneByID(oCampaign.dataSet.Campaign[0].ShipToCampaignContactID);
		}

		private void SetValue() 
		{
			AddressDataSet.AddressRow rowAddress;

			this.FMID = oCampaign.dataSet.Campaign[0].FMID;
			this.LastFMIDState = oCampaign.dataSet.Campaign[0].FMID;
			this.Language = oCampaign.dataSet.Campaign[0].Lang;
			this.StaffOrder = oCampaign.dataSet.Campaign[0].IsStaffOrder;
			this.BillIncentivesTo = oCampaign.dataSet.Campaign[0].IncentivesBillToID;
			this.IncentivesDistribution = oCampaign.dataSet.Campaign[0].IncentivesDistributionID;
			this.Status = oCampaign.dataSet.Campaign[0].Status;
			this.Renewal = oCampaign.dataSet.Campaign[0].Renewal;
			this.StartDate = oCampaign.dataSet.Campaign[0].StartDate;
			this.EndDate = oCampaign.dataSet.Campaign[0].EndDate;
			this.EstimatedGross = Convert.ToDouble(oCampaign.dataSet.Campaign[0].EstimatedGross);
			this.NumberOfParticipants = oCampaign.dataSet.Campaign[0].NumberOfParticipants;
			this.NumberOfClassrooms = oCampaign.dataSet.Campaign[0].NumberOfClassroooms;
			this.NumberOfStaff = oCampaign.dataSet.Campaign[0].NumberOfStaff;
			this.SpecialInstructions = oCampaign.dataSet.Campaign[0].SpecialInstructions;
			this.DateSubmitted = oCampaign.dataSet.Campaign[0].DateSubmitted;
			this.Extra1Ups=oCampaign.dataSet.Campaign[0].Extra_1Ups;
			this.ExtraGiftForm=	oCampaign.dataSet.Campaign[0].Extra_GiftForm;
            this.ExtraMagBrochure = oCampaign.dataSet.Campaign[0].Extra_MagBrochure;
         this.CoolCardsBoxes = oCampaign.dataSet.Campaign[0].CoolCardsBoxes;
         this.OnlineOnly=oCampaign.dataSet.Campaign[0].OnlineOnlyPrograms;
            this.OnlineNutFree = oCampaign.dataSet.Campaign[0].OnlineNutFree;
            this.OnlineMagazineTRTOnly = oCampaign.dataSet.Campaign[0].OnlineMagazineTRTOnly;
            this.ForceStatementPrint = oCampaign.dataSet.Campaign[0].ForceStatementPrint;
            this.DisableStatementPrint = oCampaign.dataSet.Campaign[0].DisableStatementPrint;
            this.CookieDoughDeliveryDate = oCampaign.dataSet.Campaign[0].CookieDoughDeliveryDate;
            //this.LoomisCheckBox.Checked = oCampaign.dataSet.Campaign[0].CarrierID == 53017 ? true : false;

            this.Notes = oCampaign.dataSet.Campaign[0].Notes;
			this.ctrlContactMaintenanceControl.ContactID = oCampaign.dataSet.Campaign[0].ShipToCampaignContactID;
			if(contact.dataSet.Contact.Count > 0) 
			{
				this.ctrlContactMaintenanceControl.oContact = contact;
			}
			this.ctrlContactMaintenanceControl.IsPhoneRequired = false;

			if(oShipToAccount.dataSet.CAccount.Count > 0) 
			{
				rowAddress = oShipToAccount.AddressList.GetOneByType(AddressType.ShipTo);

				if(rowAddress != null) 
				{
					this.ctrlAddressViewerControlShipTo.AddressID = rowAddress.address_id;
				}

				rowAddress = oShipToAccount.AddressList.GetOneByType(AddressType.BillTo);

				if(rowAddress != null) 
				{
					this.ctrlAddressViewerControlBillTo.AddressID = rowAddress.address_id;
				}
			}
		}

		private void SetValueEmpty() 
		{
			if(QSPPage.aUserProfile.IsFM) 
			{
				this.FMID = QSPPage.aUserProfile.FMID;
			} 
			else 
			{
				this.FMID = this.LastFMIDState;
			}
			this.Language = "";
			this.StaffOrder = false;
			this.BillIncentivesTo = INCENTIVES_BILL_TO_DEFAULT;
			this.IncentivesDistribution = 0;
			this.Status = Convert.ToInt32(CampaignStatus.PendingIncomplete);
			this.Renewal = false;
			this.dteStartDate.ClearDate();
			this.dteEndDate.ClearDate();
            this.dteCookieDoughDeliveryDate.ClearDate();
            this.tbxEstimatedGross.Text = "";
			this.tbxNumberOfParticipants.Text = "";
			this.tbxNumberOfClassrooms.Text = "";
			this.tbxNumberOfStaff.Text = "";
			this.SpecialInstructions = "";
			this.DateSubmitted = DateTime.Now;
			this.tbxExtra1ups.Text = "";
			this.tbxExtraGiftForm.Text = "";
            this.tbxExtraMagBrochure.Text = "";
         this.tbxCoolCardsBoxes.Text = "";
         this.OnlineOnly = false;
            this.OnlineNutFree = false;
            this.OnlineMagazineTRTOnly = false;
            this.ForceStatementPrint = false;
            this.DisableStatementPrint = false;

			this.ctrlContactMaintenanceControl.ContactID = 0;
			this.ctrlContactMaintenanceControl.IsPhoneRequired = false;

			this.ctrlAddressViewerControlShipTo.AddressID = 0;
			this.ctrlAddressViewerControlBillTo.AddressID = 0;
		}

		private void SetEnabled() 
		{
            bool IsFM = QSPPage.aUserProfile.IsFM && !(QSPPage.aUserProfile.FMID == "9999");
			//this.ddlFieldManager.Enabled = !QSPPage.aUserProfile.IsFM; MS Feb 28            
            if (IsFM)
            {
                this.ddlFieldManager.Enabled = false;
                this.ForceStatementPrintCheckBox.Visible = false;
                this.ForceStatementPrintLabel.Visible = false;
                this.DisableStatementPrintCheckBox.Visible = false;
                this.DisableStatementPrintLabel.Visible = false;
            }
            this.tdNotes.Visible = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("HomeOffice") ? true : false;
            this.rblStaffOrder.Enabled = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("HomeOffice") ? true : false;            
 
		}

		private void DataBindLanguage() 
		{
			CAccount oCAccount = LoadDataLanguage();
			SetValueLanguage(oCAccount);
		}

		private CAccount LoadDataLanguage() 
		{
			CAccount oCAccount = new CAccount(this.SelectedAccountID, this.Page.CurrentTransaction);

			return oCAccount;
		}

		private void SetValueLanguage(CAccount oCAccount) 
		{
			this.Language = oCAccount.dataSet.CAccount[0].Lang;
		}



		private void DataBindEmptyContact() 
		{
			ContactDataSet.ContactRow rowContact = null;
			Contact accountContacts = new Contact(this.Page.CurrentTransaction);

			accountContacts.GetLastByAccountID(this.SelectedAccountID);

			if(accountContacts.dataSet.Contact.Count == 1) 
			{
				rowContact = accountContacts.dataSet.Contact[0];
			} 
			else 
			{
				// Creates new default contact
				rowContact = accountContacts.AddDefaultCampaignContact(Language);
			}

			this.ctrlContactMaintenanceControl.ContactID = rowContact.Id;
			this.ctrlContactMaintenanceControl.oContact = accountContacts;
			this.ctrlContactMaintenanceControl.DataBind();

			this.ctrlContactMaintenanceControl.ContactID = 0;
		}

		private void LoadDataDDL() 
		{
			LoadDataDDLFieldManager();
			LoadDataDDLIncentivesBillTo(this, null);
			LoadDataDDLIncentivesDistribution();
			LoadDataDDLStatus();
        }

		private void LoadDataDDLFieldManager() 
		{
			try 
			{
				if(this.ddlFieldManager.Items.Count == 0) 
				{
					
					CAccount oCAccount = new CAccount(this.SelectedAccountID, this.Page.CurrentTransaction);
					string country = oCAccount.dataSet.CAccount[0].Country;

					FieldManager fm = new FieldManager();
					//fm.GetAll();
					fm.GetAllByCountryCode(country);

					this.ddlFieldManager.DataSource = fm.dataSet;
					this.ddlFieldManager.DataMember = fm.dataSet.FieldManager.TableName;
					this.ddlFieldManager.DataTextField = fm.dataSet.FieldManager.ListNameColumn.ColumnName;
					this.ddlFieldManager.DataValueField = fm.dataSet.FieldManager.FMIDColumn.ColumnName;

					this.ddlFieldManager.DataBind();
				}
			} 
			catch (MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		public void LoadDataDDLIncentivesBillTo(object sender, System.EventArgs e) 
		{
			try 
			{
                this.ddlIncentivesBillTo.Items.Clear();

                CodeDetail cd = new CodeDetail();

                bool IsCampaign2014OrLater = true;
                /*if (this.CampaignID != 0 && this.oCampaign != null)
                {
              		if (oCampaign.dataSet.Campaign[0].StartDate < new DateTime(2013, 7, 1))
                        IsCampaign2014OrLater = false;
                }*/

                if (StartDate != new DateTime(1995, 1, 1) && StartDate < new DateTime(2013, 7, 1))
                    IsCampaign2014OrLater = false;

                cd.GetIncentivesBillTo(IsCampaign2014OrLater);

				this.ddlIncentivesBillTo.DataSource = cd.dataSet;
				this.ddlIncentivesBillTo.DataMember = cd.dataSet.CodeDetail.TableName;
				this.ddlIncentivesBillTo.DataTextField = cd.dataSet.CodeDetail.DescriptionColumn.ColumnName;
				this.ddlIncentivesBillTo.DataValueField = cd.dataSet.CodeDetail.InstanceColumn.ColumnName;

				this.ddlIncentivesBillTo.DataBind();
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadDataDDLIncentivesDistribution() 
		{
			try 
			{
				if(this.ddlIncentivesDistribution.Items.Count == 0) 
				{
					CodeDetail cd = new CodeDetail(CodeHeaderInstance.IncentivesDistribution);

					this.ddlIncentivesDistribution.DataSource = cd.dataSet;
					this.ddlIncentivesDistribution.DataMember = cd.dataSet.CodeDetail.TableName;
					this.ddlIncentivesDistribution.DataTextField = cd.dataSet.CodeDetail.DescriptionColumn.ColumnName;
					this.ddlIncentivesDistribution.DataValueField = cd.dataSet.CodeDetail.InstanceColumn.ColumnName;

					this.ddlIncentivesDistribution.DataBind();
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		private void LoadDataDDLStatus() 
		{
			try 
			{
				if(this.ddlStatus.Items.Count == 0) 
				{
					CodeDetail cd = new CodeDetail(CodeHeaderInstance.CampaignStatus);

					this.ddlStatus.DataSource = cd.dataSet;
					this.ddlStatus.DataMember = cd.dataSet.CodeDetail.TableName;
					this.ddlStatus.DataTextField = cd.dataSet.CodeDetail.DescriptionColumn.ColumnName;
					this.ddlStatus.DataValueField = cd.dataSet.CodeDetail.InstanceColumn.ColumnName;

					this.ddlStatus.DataBind();
				}
			} 
			catch(MessageException ex) 
			{
				this.Page.SetPageError(ex);
			}
		}

		public void Save() 
		{
			bool isValid = true;
			CampaignDataSet.CampaignRow row;

			if(this.CampaignID != 0 && this.oCampaign != null) 
			{
				row = oCampaign.dataSet.Campaign[0];
				FillCampaignRow(row);

				// Save campaign contact information
				LoadDataContact();
				this.ctrlContactMaintenanceControl.oContact = contact;
				this.ctrlContactMaintenanceControl.ContactID = row.ShipToCampaignContactID;
				this.ctrlContactMaintenanceControl.Save();

				try 
				{
					contact.Validate();
				} 
				// 03/26/2006 - Ben :	Tweak to save contact but not show errors before
				//						all is saved.
				catch(MessageException) 
				{
					isValid = false;
				}

				contact.SaveWithoutValidation();

				row.ShipToCampaignContactID = contact.dataSet.Contact[0].Id;
				row.BillToCampaignContactID = contact.dataSet.Contact[0].Id;

				if(isValid) 
				{
					contact.SaveCampaignToAccount(row.ShipToAccountID);
				}
			} 
			else 
			{
				SaveNew();
			}
		}

		public void SaveNew() 
		{
			bool isValid = true;
			CampaignDataSet.CampaignRow row;

			this.DateSubmitted = DateTime.Now;

			row = oCampaign.dataSet.Campaign.NewCampaignRow();
			FillCampaignRow(row);
			row.ShipToAccountID = this.SelectedAccountID;
			row.BillToAccountID = this.SelectedAccountID;

			oCampaign.dataSet.Campaign.AddCampaignRow(row);

			// Save campaign contact information
			contact = new Contact(this.Page.CurrentMessageManager);
			contact.CurrentTransaction = this.Page.CurrentTransaction;
			this.ctrlContactMaintenanceControl.oContact = contact;
			this.ctrlContactMaintenanceControl.ContactID = 0;
			this.ctrlContactMaintenanceControl.Save();

			try 
			{
				contact.Validate();
			} 
			// 03/26/2006 - Ben :	Tweak to save contact but not show errors before
			//						all is saved.
			catch(MessageException) 
			{
				isValid = false;
			}

			contact.SaveWithoutValidation();

			row.ShipToCampaignContactID = contact.dataSet.Contact[0].Id;
			row.BillToCampaignContactID = contact.dataSet.Contact[0].Id;

			if(isValid) 
			{
				contact.SaveCampaignToAccount(row.ShipToAccountID);
			}
		}

		private void FillCampaignRow(CampaignDataSet.CampaignRow row) 
		{
			row.FMID = this.FMID;
			row.Lang = this.Language;
			row.IsStaffOrder = this.StaffOrder;
			row.IncentivesBillToID = this.BillIncentivesTo;
			row.IncentivesDistributionID = this.IncentivesDistribution;
			row.Status = this.Status;
			row.Renewal = this.Renewal;
			row.StartDate = this.StartDate;
			row.EndDate = this.EndDate;
			row.EstimatedGross = Convert.ToDecimal(this.EstimatedGross);
			row.NumberOfParticipants = this.NumberOfParticipants;
			row.NumberOfClassroooms = this.NumberOfClassrooms;
			row.NumberOfStaff = this.NumberOfStaff;
			row.SpecialInstructions = this.SpecialInstructions;
			row.DateChanged = DateTime.Now.ToString();
			row.DateModified = DateTime.Now;
			row.UserIDModified = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.Instance;
			row.DateSubmitted = this.DateSubmitted;
			row.Extra_1Ups=this.Extra1Ups;
			row.Extra_GiftForm=this.ExtraGiftForm;
            row.Extra_MagBrochure = this.ExtraMagBrochure;
         row.CoolCardsBoxes = this.CoolCardsBoxes;
         row.OnlineOnlyPrograms=this.OnlineOnlyPrograms;
            row.OnlineNutFree = this.OnlineNutFree;
            row.OnlineMagazineTRTOnly = this.OnlineMagazineTRTOnly;
            row.ForceStatementPrint = this.ForceStatementPrint;
            row.DisableStatementPrint = this.DisableStatementPrint;
            row.CookieDoughDeliveryDate = this.CookieDoughDeliveryDate;
            row.CarrierID = 0;
            if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("HomeOffice"))
            {
                row.Notes = this.Notes;
            }
		}
	}
}
