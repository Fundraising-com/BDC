namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;
	using Business.com.ses.ws.AddressHygiene;

	/// <summary>
	///		Summary description for refundsub.
	/// </summary>
	public partial class IssueCustomerRefund : CustomerServiceActionControl
	{
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.Label lblSubscription;
		protected QSP.WebControl.Currency Currency1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblCreatedBy;
		protected System.Web.UI.WebControls.Label lblDateCreated;
		protected System.Web.UI.WebControls.Label lblRefundSubscription;
		protected const string MSG_HEADER = "Customer Refund";
		private double Amount = 0;
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene ctrlAddressHygiene;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				SetDefaultValue();
			}

			AddressHygieneStatusLabel.Text = String.Empty;

            SetAddressHygieneEnabled();

			//Only enable confirm button after Address has been checked
            this.Page.ConfirmButton.Enabled = !this.ctrlAddressHygiene.IsAddressHygieneEnabled;
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			AddJavaScript();
		}

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript();

			if (!AddressHygiened)
			{
				AddJavaScriptDisableSubmitButton();
				AddAddressAttributes();
			}
			else
				RemoveAddressAtrributes();
		}

		private void AddJavaScriptDisableSubmitButton() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function DisableSubmitButton() {\n";
			script += "     document.getElementById(\"" + this.Page.ConfirmButton.ClientID + "\").disabled = true;\n";
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
			this.ddlCountry.Attributes["onchange"] = "DisableConfirmButton();";
		}

		private void RemoveAddressAtrributes()
		{
			this.tbxStreet1.Attributes["onchange"] = "";
			this.tbxStreet2.Attributes["onchange"] = "";
			this.tbxCity.Attributes["onchange"] = "";
			this.ddlProvince.Attributes["onchange"] = "";
			this.tbxPostalCode.Attributes["onchange"] = "";
			this.ddlCountry.Attributes["onchange"] = "";
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

        public void SetAddressHygieneEnabled()
        {
            bool enabled = this.ctrlAddressHygiene.IsAddressHygieneEnabled;
            this.btnValidateAddress.Visible = enabled;
            this.AddressHygieneStatusLabel.Visible = enabled;
            this.ctrlAddressHygiene.Visible = enabled;
        }

		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
		protected override void DoAction()
		{
			DataTable CreditCardTable = new DataTable("CreditCard");
//			DataRow row;
//			DataAccess.Common.ActionObject.CreditCard cc;

			DataAccess.Common.ActionObject.Address add = new DataAccess.Common.ActionObject.Address(this.tbxStreet1.Text,this.tbxStreet2.Text,this.tbxCity.Text,this.ddlProvince.SelectedItem.Value,this.tbxPostalCode.Text,this.ddlCountry.SelectedItem.Value);
			Customer cCustomer = new Customer(this.tbxLastName.Text,this.tbxFirstName.Text,add);
			RefundCustomer rcRefund = new RefundCustomer(this.Page.OrderInfo.CustomerOrderHeaderInstance,
														 this.Page.OrderInfo.TransID,
														cCustomer,
														GetRegularPrice(),
														GetRefundAmout(),
														this.tbxRefundReason.Text,
														this.Page.IncidentID,
														this.Page.UserID);
									
			this.Page.BusCustomer.ValidateRefundCustomer(rcRefund);

			// Credit Card Web Service Validation and Refund
/*			this.Page.BusPayment.SelectCustomerCreditCardInformation(CreditCardTable, this.Page.OrderInfo.CustomerOrderHeaderInstance);
			row = CreditCardTable.Rows[0];

			cc = new QSPFulfillment.DataAccess.Common.ActionObject.CreditCard(
				(PaymentMethod) Enum.ToObject(typeof(PaymentMethod), Convert.ToInt32(row["PaymentMethodID"])),
				row["CardholderName"].ToString(),
				row["CreditCardNumber"].ToString(),
				"",
				"",
				row["AuthorizationCode"].ToString());

			if(row["ExpirationDate"].ToString().Length >= 4) {
				cc.ExpirationMonth = row["ExpirationDate"].ToString().Substring(0, 2);
				cc.ExpirationYear = row["ExpirationDate"].ToString().Substring(2, 2);
			}

			if(cc.IsCreditCardPayment) {
				this.Page.BusCreditCard.Refund(rcRefund, cc);
			}
*/
			this.Page.BusCustomer.RefundCustomer(rcRefund);								 
		}

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

		private float GetRegularPrice()
		{
			if(this.lblRegularPrice.Text == String.Empty)
				return 0f;

			try 
			{
				return Convert.ToSingle(this.lblRegularPrice.Text);
			}
			catch 
			{
				this.Page.MessageManager.Add(QSPFulfillment.DataAccess.Common.Message.ERRMSG_SYSTEM_VAR_0);
				throw new ExceptionFulf(this.Page.MessageManager);
			}
		}
		private float GetRefundAmout()
		{
			if(this.tbxRefundAmount.Text == String.Empty)
				return 0f;

			try 
			{
				return Convert.ToSingle(this.tbxRefundAmount.Text);
			}
			catch 
			{
				this.Page.MessageManager.Add(this.Page.MessageManager.FormatErrorMessage(QSPFulfillment.DataAccess.Common.Message.ERRMSG_INVALID_CHARACTER_SEARCH_1, "Refund Amount"));
				throw new ExceptionFulf(this.Page.MessageManager);
			}
		}
		private void SetDefaultValue()
		{
			LoadData();
	
			if(DataSource.Rows.Count != 0)
			{
				DataRow row = DataSource.Rows[0];
				this.tbxFirstName.Text = row["FirstName"].ToString();
					
				this.tbxLastName.Text =  row["LastName"].ToString();
				this.tbxStreet1.Text= row["Address1"].ToString();
				this.tbxStreet2.Text = row["Address2"].ToString();
				this.tbxCity.Text = row["City"].ToString();
				this.ddlProvince.SetSelectedValue(row["state"].ToString());
				this.tbxPostalCode.Text= row["Zip"].ToString();
				this.tbxRefundAmount.Text = String.Format("{0:N2}",Amount.ToString());
				this.lblRegularPrice.Text = String.Format("{0:N2}",Amount.ToString());
			
			}
		}
		private void LoadData()
		{
			DataSource = new DataTable("Address");
			
			this.Page.BusCustomerOrderHeader.SelectShipToAddress(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);

            double totalAmountAlreadyRefunded = this.Page.BusPayment.SelectRefundTotalAmountByCOD(this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);

            double maxRefundAmount = GetMaxRefundAmount(this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
            Amount = maxRefundAmount - totalAmountAlreadyRefunded;
		}

        private double GetMaxRefundAmount(int cohInstance, int transID)
        {
            double returnValue = 0.00d;

            returnValue = this.Page.BusPayment.SelectMaxRefundAmountByCustomerOrderDetail(cohInstance, transID);

            return returnValue;
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
			this.Page.ConfirmButton.Enabled = true;
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

		public void SetConfirmButtonVisibility()
		{
			this.Page.ConfirmButton.Enabled = this.AddressHygiened;
			this.Page.ConfirmButton.Visible = this.AddressHygiened;
			this.Page.ConfirmButton.CausesValidation = this.AddressHygiened;
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

				string country;
				switch (OutputAddress.Country)
				{
					case "CANADA":
						country = "CA";
						break;
					case "UNITED STATES":
						country = "US";
						break;
					default:
						country = OutputAddress.Country;
						break;
				}
				bool countryFound = false;
				for(int i = 0; i < this.ddlCountry.Items.Count; i++)
				{
					if(ddlCountry.Items[i].Value == country)
					{
						ddlCountry.SelectedIndex = i;
						countryFound = true;
						break;
					}
				}
				if (countryFound == false)
					ddlCountry.SelectedIndex = 0;

				//Display Status 
				if (ChangeStatus)
					AddressHygieneStatusLabel.Text = "Address Updated";
				else
					AddressHygieneStatusLabel.Text = "Address Validated";
			}
		}
	}
}
