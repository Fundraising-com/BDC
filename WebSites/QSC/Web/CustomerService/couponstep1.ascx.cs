namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using Business.com.ses.ws.AddressHygiene;
	/// <summary>
	///		Summary description for couponstep1.
	/// </summary>
	public partial class couponstep1 : CustomerServiceControlCoupon
	{
		private const string FREE_SUB = "FREESUB";
		private const string NEW_ORDER = "NEWORDER";

		protected ControlerConfirmationPage ctrlControlerConfirmationPage;
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene ctrlAddressHygiene;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				Type = CustomerType.Certificate;

                SetAddressHygieneEnabled();

				//Only enable confirm button after Address has been checked
                this.Page.btnNext.Enabled = !this.ctrlAddressHygiene.IsAddressHygieneEnabled;
			}

			this.ctrlControlerConfirmationPage.Confirmed += new ConfirmEventHandler(this.CompleteStepAfterConfirm);

			AddressHygieneStatusLabel.Text = String.Empty;
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			AddJavaScript();
		}

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript();
            AddJavaScriptCampaginIdFindHyperLink();
			if (!AddressHygiened)
			{
				AddJavaScriptDisableSubmitButton();
				AddAddressAttributes();
			}
			else
				RemoveAddressAtrributes();
		}
        private void AddJavaScriptCampaginIdFindHyperLink()
        {
            this.hypFindCampaignID.Attributes.Add("onClick", "javascript:Open('/QSPFulfillment/AcctMgt/SearchCampaignByAccount.aspx?IsNewWindow=true&Id=true&caller="+tbxCampaignID.UniqueID+"');");
        }
		private void AddJavaScriptDisableSubmitButton() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function DisableSubmitButton() {\n";
			script += "     document.getElementById(\"" + this.Page.btnNext.ClientID + "\").disabled = true;\n";
			script += "  }\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("DisableSubmitButton", script);
		}

		private void AddAddressAttributes()
		{
			this.tbxStreet1.Attributes["onchange"] = "DisableConfirmButton();";
			this.tbxStreet2.Attributes["onchange"] = "DisableConfirmButton();";
			this.tbxCity.Attributes["onchange"] = "DisableConfirmButton();";
			this.ddlProvince.Attributes["onchange"] = "DisableConfirmButton();";
			this.tbxPostalCode.Attributes["onchange"] = "DisableConfirmButton();";
		}

		private void RemoveAddressAtrributes()
		{
			this.tbxStreet1.Attributes["onchange"] = "";
			this.tbxStreet2.Attributes["onchange"] = "";
			this.tbxCity.Attributes["onchange"] = "";
			this.ddlProvince.Attributes["onchange"] = "";
			this.tbxPostalCode.Attributes["onchange"] = "";
		}
		#endregion

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
			ctrlAddressHygiene.OutputAddress += new QSPFulfillment.CommonWeb.UC.OutputAddressEventHandler(AddressSelected);
		}
		#endregion

		private bool AddressHygiened
		{
			get 
			{
				if(ViewState["AddressHygiened"] != null) 
					return (bool)ViewState["AddressHygiened"];
				else
					return false;
			}
			set 
			{
				ViewState["AddressHygiened"] = value;
			}
		}
        private int CampaignID
        {
            get {
                if (ViewState["CampaignID"]!= null )
                    return (int)ViewState["CampaignID"];
                else 
                    return 0;
                 }
            set { ViewState["CampaignID"] = value; }
        }

        public void SetAddressHygieneEnabled()
        {
            bool enabled = this.ctrlAddressHygiene.IsAddressHygieneEnabled;
            this.btnValidateAddress.Visible = enabled;
            this.AddressHygieneStatusLabel.Visible = enabled;
            this.ctrlAddressHygiene.Visible = enabled;
        }

		private void SetValue()
		{
			this.tbxFirstName.Text = this.Page.CustomerInfo.FirstName;
			this.tbxLastName.Text = this.Page.CustomerInfo.LastName;
			this.tbxCity.Text = this.Page.CustomerInfo.CustomerAddress.City;
			this.tbxPostalCode.Text = this.Page.CustomerInfo.CustomerAddress.PostalCode;
			this.tbxStreet1.Text = this.Page.CustomerInfo.CustomerAddress.Street1;
			this.tbxStreet2.Text= this.Page.CustomerInfo.CustomerAddress.Street2;

			SetValueProvince(this.Page.CustomerInfo.CustomerAddress.StateProvinceCode);
		}
		
		private void SetValueProvince(string Value)
		{
			
			for(int i=0; i < this.ddlProvince.Items.Count; i++)
			{
				if(this.ddlProvince.Items[i].Value ==Value )
				{
					this.ddlProvince.SelectedIndex = i;
					break;
				}
			}
		}
		
		protected override void DoAction()
		{
		
			if(this.Page.CurrentStep ==1)
			{
                if (tbxCampaignID.Text.Trim() != "") { CampaignID = Convert.ToInt32(tbxCampaignID.Text); } else { CampaignID = 0; };

                this.Page.InvoiceOrder = InvoiceOrderCheckBox.Checked;
				if(this.tbxCoupon.Text.ToUpper() == FREE_SUB) 
				{
					CompleteStepFreeSub();
				}
				else if(this.tbxCoupon.Text.ToUpper() == NEW_ORDER) 
				{
					CompleteStepNewOrder();
				}
				else 
				{

					this.Page.CouponSetID = this.Page.BusCoupon.ValidateCoupon(this.tbxCoupon.Text);

					if(this.Page.CouponSetID  <= 0)
					{
					

						if(this.Page.CouponSetID == 0)
						{
							this.ctrlControlerConfirmationPage.Message = QSPFulfillment.DataAccess.Common.Message.ERRMSG_CERTIFICATE_ALREADY_USED;
							this.ctrlControlerConfirmationPage.ShowConfirmationWindow();
						}
					

					}
					else
					{
						CompleteStep();
					}	
				}
			}
		}

		private void CompleteStepAfterConfirm(object sender, EventArgs e) 
		{
			Type = CustomerType.ReIssue;
			this.Page.CouponSetID = 1;
			CompleteStep();
		}

		private void CompleteStepFreeSub() 
		{
			Type = CustomerType.NewSubForOrderCorrection;

            if (this.Page.InvoiceOrder)
                this.Page.OrderQualifierID = 39020; //Customer Service to Invoice
            else
                this.Page.OrderQualifierID = 39012; //Order Correction
            
            CompleteStep();
		}

		private void CompleteStepNewOrder() 
		{
			Type = CustomerType.NewOrderForNonExisting;

            if (this.Page.InvoiceOrder)
                this.Page.OrderQualifierID = 39020; //Customer Service to Invoice
			else
                this.Page.OrderQualifierID = 39012; //Order Correction

            CompleteStep();
		}

		private void CompleteStep() 
		{
			Customer cus = new Customer(this.tbxLastName.Text,this.tbxFirstName.Text,new DataAccess.Common.ActionObject.Address(this.tbxStreet1.Text,this.tbxStreet2.Text,this.tbxCity.Text,this.ddlProvince.SelectedItem.Value,this.tbxPostalCode.Text,this.ddlCountry.SelectedItem.Value));
			cus.CustomerAddress.StateProvince = this.ddlProvince.SelectedItem.Text;
			cus.CustomerAddress.Country = this.ddlCountry.SelectedItem.Text;
			cus.Type = Type;
			cus.Email = this.tbxEmail.Text;
			cus.PhoneNumber = this.tbxPhoneNumber.Text;
			cus.CustomerInstance = this.Page.CustomerInfo.CustomerInstance;
			this.Page.CustomerInfo = cus;


			if(this.Page.CustomerInfo.CustomerInstance != 0)
			{
				cus.CustomerInstance = this.Page.CustomerInfo.CustomerInstance;
				this.Page.BusCoupon.UpdateCustomer(cus,this.Page.UserID);
			}
			else
			{
												
				this.Page.BusCoupon.InsertCustomer(cus,this.Page.UserID);
				this.Page.CustomerInfo.CustomerInstance = (int)this.Page.BusCoupon.ResultSetReturned;
					
			}

			this.Page.OrderInfo = new CurrentOrderInfo();
			if (this.Page.InvoiceOrder)
				this.Page.OrderInfo.CampaignID = CampaignID;
			else
				this.Page.OrderInfo.CampaignID = this.Page.BusCoupon.GetCurrentQSPCampaign();
			this.Page.CustomerInfo = cus;
			this.Page.Coupon = this.tbxCoupon.Text;
				
			this.Page.Step1Completed = true;
			this.Page.ActionPerformed = true;
			this.Page.CurrentStep ++;
	}

		
		private CustomerType Type
		{
			get
			{	
				return (CustomerType)ViewState["CustomerType"];
			}
			set
			{
				ViewState["CustomerType"] = value;
			}
		}
		public void ClearValue()
		{
			
			this.tbxCity.Text ="";
			this.tbxCoupon.Text = "";
			this.tbxFirstName.Text ="";
			this.tbxLastName.Text ="";
			this.tbxPostalCode.Text ="";
			this.tbxStreet1.Text ="";
			this.tbxStreet2.Text ="";
			this.ddlProvince.SelectedItem.Selected = false;
			this.ddlProvince.SelectedIndex = -1;
			this.tbxPhoneNumber.Text = "";
			this.tbxEmail.Text = "";
            this.tbxCampaignID.Text = "";
            this.InvoiceOrderCheckBox.Checked = false;
			
		}
		public override void SetValueElement()
		{
			
				this.Page.Message = "Please fill in the customer information and the certificate number.";
				this.Page.Header ="Prepaid Subscription - Step 1";
			
		}

		protected void btnValidateAddress_Click(object sender, System.EventArgs e)
		{
			AddressHygieneStatusLabel.Text = String.Empty;
			AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Blue;
			
			bool enableSuggestionList = true;
			
			try
			{
				if (ctrlAddressHygiene.DoAddressHygiene(GetAddressFromFields(), enableSuggestionList))
				{
					ctrlAddressHygiene.Visible = true;
				}
				else
				{
					AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
					AddressHygieneStatusLabel.Text = "Error: Unable to perform Address Hygiene";
				}
			}
			catch (Exception)
			{
				AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
				AddressHygieneStatusLabel.Text = "Error: Unable to perform Address Hygiene";
			}

			AddressHygiened = true;
			this.Page.btnNext.Enabled = true;
		}

		private Business.com.ses.ws.AddressHygiene.Address GetAddressFromFields()
		{
			Business.com.ses.ws.AddressHygiene.Address address = new Business.com.ses.ws.AddressHygiene.Address();

			address.Address1 = this.tbxStreet1.Text.ToString();
			address.Address2 = this.tbxStreet2.Text.ToString();
			address.City = this.tbxCity.Text.ToString();
			address.County = "";
			address.Region = this.ddlProvince.SelectedValue;
			if (this.tbxPostalCode.Text.ToString().Length == 6) //Address Hygiene requires space within Canadian Postal Code
				address.PostCode = this.tbxPostalCode.Text.ToString().Substring(0, 3) + " " + this.tbxPostalCode.Text.ToString().Substring(3, 3);
			else
				address.PostCode = this.tbxPostalCode.Text.ToString();
			address.PostCode2 = "";
			address.Country = "CANADA";

			return address;
		}

		private void AddressSelected(object Sender, Business.com.ses.ws.AddressHygiene.OutputAddress OutputAddress, bool ChangeStatus)
		{
			//Check if there was an error with the address that came back
			if (OutputAddress.Fault != Fault.NoError)
			{
				AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
				AddressHygieneStatusLabel.Text = "Error: " + ctrlAddressHygiene.GetErrorFromFault(OutputAddress.Fault.ToString());
			}
				//Check if there was a Suggestion List error
			else if (OutputAddress.SuggestionListInformation.Error != SuggestionListError.None && OutputAddress.SuggestionListInformation.Error != SuggestionListError.MoreInformationRequired)
			{
				AddressHygieneStatusLabel.ForeColor = System.Drawing.Color.Red;
				AddressHygieneStatusLabel.Text = "Error: " + ctrlAddressHygiene.GetErrorFromFault(OutputAddress.SuggestionListInformation.Error.ToString());
			}
			else
			{
				this.tbxStreet1.Text = OutputAddress.Address1;
				this.tbxStreet2.Text = OutputAddress.Address2;
				this.tbxCity.Text = OutputAddress.City;
				
				if (OutputAddress.PostCode.Length == 7) //Address Hygiene returns a space between Canadian Postal Code
					this.tbxPostalCode.Text = OutputAddress.PostCode.Substring(0, 3) + OutputAddress.PostCode.Substring(4, 3);
				else
					this.tbxPostalCode.Text = OutputAddress.PostCode;

				string stateProvince = OutputAddress.Region;
				bool stateProvinceFound = false;
				for(int i = 0; i < ddlProvince.Items.Count; i++)
				{
					if(ddlProvince.Items[i].Value == stateProvince)
					{
						ddlProvince.SelectedIndex = i;
						stateProvinceFound = true;
						break;
					}
				}
				if (stateProvinceFound == false)
					ddlProvince.SelectedIndex = 0;

				//Display Status 
				if (ChangeStatus)
					AddressHygieneStatusLabel.Text = "Address Updated";
				else
					AddressHygieneStatusLabel.Text = "Address Validated";
			}
		}
	}
}
