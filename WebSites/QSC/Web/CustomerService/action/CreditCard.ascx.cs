namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using Business.com.ses.ws.AddressHygiene;
   using Business;

	/// <summary>
	///		Update Credit Card Action
	/// </summary>
	public partial class CreditCard : CustomerServiceActionControl
	{
		protected const string MSG_HEADER = "Credit Card";
		protected ControlerPaymentInfo ctrlControlerPaymentInfo;
		protected ControlerSubscriptionForCOHI ctrlControlerSubscriptionForCOHI;
		protected QSPFulfillment.CommonWeb.UC.AddressHygiene ctrlAddressHygiene;

		private DataGridItem dgiHeader;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				this.ctrlControlerPaymentInfo.EditMode = true;		
				this.ctrlControlerPaymentInfo.DataBind();
				ctrlControlerSubscriptionForCOHI.DataBind();

				//Only show confirm button after Address has been checked
				SetConfirmButtonVisibility();
			}
		}

		protected void CreditCard_PreRender(object sender, EventArgs e)
		{
			/*if(!IsPostBack) 
			{
				SetValueAmount();
			}*/

            SetValueAmount();
            SetPriceToChargeVisibility();

			this.AddJavaScript();
			this.AddJavaScriptEventHandlers();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlControlerSubscriptionForCOHI.dtgMain.ItemCreated += new DataGridItemEventHandler(dtgMain_ItemCreated);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new System.EventHandler(this.CreditCard_PreRender);
			ctrlAddressHygiene.OutputAddress += new QSPFulfillment.CommonWeb.UC.OutputAddressEventHandler(AddressSelected);
		}
		#endregion

		private void dtgMain_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header) 
			{
				this.dgiHeader = e.Item;
			}
		}

        protected double GetTotal()
        {
            double total = 0.00;
            foreach (DataGridItem dgi in this.ctrlControlerSubscriptionForCOHI.dtgMain.Items)
            {
                if (((CheckBox) dgi.FindControl("chkSelect")).Checked)
                {
                    string amount;

                    if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("CCPrice"))
                        amount = ((TextBox)dgi.FindControl("tbxPriceToCharge")).Text;
                    else
                        amount = ((Label)dgi.FindControl("lblPrice")).Text;

                    total += Convert.ToDouble(amount);
                }
            }
            return total;
        }

		protected override void AddJavaScript()
		{
		    string script;

			base.AddJavaScript();

            script = "<script language=\"javascript\">\n";
            script += "  function CalculateTotalAmount() {\n";
            script += "    var amount = 0.0;\n";

            foreach (DataGridItem dgi in this.ctrlControlerSubscriptionForCOHI.dtgMain.Items)
            {
                script += "    if(document.getElementById(\"" + ((CheckBox)dgi.FindControl("chkSelect")).ClientID + "\").checked) {\n";
                script += "      amount += " + ((TextBox)dgi.FindControl("tbxPriceToCharge")).Text + ";\n";
                script += "    }\n";
            }

            script += "    document.getElementById(\"" + this.tbxAmount.ClientID + "\").value = amount;\n";
            script += "  }\n";
            script += "</script>\n";

			/*script  = "<script language=\"javascript\">\n";
			script += "  function CalculateTotalAmount() {\n";
			script += "  var amount = 0.0;\n";
            script += "  var regex = new RegExp('^[0-9]*[.][0-9]{0,2}$');";
            script += "\n var grid = document.getElementById('" + ctrlControlerSubscriptionForCOHI.dtgMain.ClientID + "');\n";
            script += "var inputs = grid.getElementsByTagName('input');\n";  
            //loop started with 1 because on 0th index we have chkSelect control which we don't need.
            script += "for (var i = 1; i < inputs.length-1; i++) {\n";
            script += "if (inputs[i].type == 'checkbox') {\n";               
            script += "if(inputs[i].checked){\n";
            script += "if ((inputs[i+1].value).match(regex) != null) {\n";
            script += "amount +=parseFloat(inputs[i+1].value)";
            script += "  }\n";
            script += "else{\n";
            script += " alert('Invalid field Price To Charge.');";
            script += "return false;";
            script += "  }\n";
            script += "  }\n";           
            script += "  }\n";
            script += "  }\n";
            script += "document.getElementById('" + tbxAmount.ClientID + "').value = amount;";
            script += "  }\n";       
			script += "</script>\n";*/

			this.Page.RegisterClientScriptBlock("CalculateTotalAmount", script);         

			if (!AddressHygiened && this.ctrlAddressHygiene.IsAddressHygieneEnabled)
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
			this.tbxPostalCode.Attributes["onchange"] = "DisableConfirmButton();";
		}

		private void RemoveAddressAtrributes()
		{
			this.tbxStreet1.Attributes["onchange"] = "";
			this.tbxStreet2.Attributes["onchange"] = "";
			this.tbxCity.Attributes["onchange"] = "";
			this.tbxPostalCode.Attributes["onchange"] = "";
		}

		private void AddJavaScriptEventHandlers() 
		{
			foreach(DataGridItem dgi in this.ctrlControlerSubscriptionForCOHI.dtgMain.Items) 
			{
				((CheckBox) dgi.FindControl("chkSelect")).Attributes.Add("onClick", "CalculateTotalAmount()");
                ((TextBox)dgi.FindControl("tbxPriceToCharge")).Attributes.Add("onBlur", "CalculateTotalAmount()");            
			}

			if(dgiHeader != null) 
			{
				((HtmlInputCheckBox) dgiHeader.FindControl("chkAllItems")).Attributes["onclick"] += "; CalculateTotalAmount()";
			}
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

		protected override void SetValueElement()
		{
			if(!IsPostBack) 
			{
				this.Page.Header = MSG_HEADER;

				SetValueAddress();
			}
		}

		private void SetValueAddress() 
		{
			DataTable AddressTable;
			DataRow row;

			AddressTable = new DataTable("Address");

			this.Page.BusCustomerOrderHeader.SelectBillToAddress(AddressTable, this.Page.OrderInfo.CustomerOrderHeaderInstance, 1);

			if(AddressTable.Rows.Count > 0) 
			{
				row = AddressTable.Rows[0];
				this.tbxStreet1.Text = row["Address1"].ToString();
				this.tbxStreet2.Text = row["Address2"].ToString();
				this.tbxCity.Text = row["City"].ToString();
				this.tbxPostalCode.Text = row["Zip"].ToString();
                this.ddlProvince.Value = row["State"].ToString();
			}
		}

		private void SetValueAmount() 
		{
            double total = 0.00;
            foreach (DataGridItem dgi in this.ctrlControlerSubscriptionForCOHI.dtgMain.Items)
            {
                if (((CheckBox)dgi.FindControl("chkSelect")).Checked)
                {
                   string amount;
                    
                    if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("CCPrice"))
                        amount = ((TextBox)dgi.FindControl("tbxPriceToCharge")).Text;
                    else
                        amount = ((Label)dgi.FindControl("lblPrice")).Text;
                    total += Convert.ToDouble(amount);
                }
            }           
            this.tbxAmount.Text = String.Format("{0:N2}", total.ToString());
		}

        private void SetPriceToChargeVisibility()
        {
            this.ctrlControlerSubscriptionForCOHI.dtgMain.Columns[7].Visible = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("CCPrice");
        }

		protected override void DoAction()
		{
			int NewCustomerOrderHeaderInstance = 0;
			int lastCheckedSub = 0;
			bool isValid = false;
			CheckBox[] chkSelect;
            double[] priceToChargeCOD;
			DataRow row;

			for(int i = 0; i < ctrlControlerSubscriptionForCOHI.dtgMain.Items.Count; i++) 
			{
				if(((CheckBox) ctrlControlerSubscriptionForCOHI.dtgMain.Items[i].FindControl("chkSelect")).Checked) 
				{
					lastCheckedSub = i;
					isValid = true;
				}
			}

            int priceToCharge = Convert.ToInt32(GetTotal() * 100);

            //If Price to charge was modified by User
            /*if ((GetTotal() * 100) != (Convert.ToInt32(Convert.ToDouble(this.tbxAmount.Text) * 100)) && QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("CCPrice"))
            {
                priceToCharge = Convert.ToInt32(Convert.ToDouble(this.tbxAmount.Text) * 100);
            }*/

            /*if ((GetTotal() * 100) != (Convert.ToInt32(Convert.ToDouble(this.tbxAmount.Text) * 100)))
            {
                isValid = false;
                throw new QSPFulfillment.DataAccess.Common.ExceptionFulf("Amount in text box not same amount as the items that are checked. Please enable Javascript and try again");
                // throw new Exception("Amount in text box not same amount as the items that are checked. Please enable Javascript and try again");
            }*/

            if(isValid)
            {                        
                bool isCustomerCardValid = ValidateCreditCardNumber(this.ctrlControlerPaymentInfo.CreditCardInfo.CreditCardNumber, this.ctrlControlerPaymentInfo.CreditCardInfo.PaymentMethodID);
                if (isCustomerCardValid)
                {
                   DataAccess.Common.ActionObject.Address address = new DataAccess.Common.ActionObject.Address(this.tbxStreet1.Text, this.tbxStreet2.Text, this.tbxCity.Text, this.ddlProvince.SelectedItem.Value, this.tbxPostalCode.Text, this.ddlCountry.SelectedItem.Value);

                   string authNumber = this.Page.BusCreditCard.AuthDepositRealTime(this.ctrlControlerPaymentInfo.CreditCardInfo, priceToCharge, address, this.Page.IncidentID);

                   this.ctrlControlerPaymentInfo.CreditCardInfo.UserID = this.Page.UserID;
                   this.ctrlControlerPaymentInfo.CreditCardInfo.AuthorizationCode = authNumber;

                   chkSelect = new CheckBox[ctrlControlerSubscriptionForCOHI.dtgMain.Items.Count];
                   priceToChargeCOD = new double[ctrlControlerSubscriptionForCOHI.dtgMain.Items.Count];

                   for (int i = 0; i < ctrlControlerSubscriptionForCOHI.dtgMain.Items.Count; i++)
                   {
                      chkSelect[i] = (CheckBox)ctrlControlerSubscriptionForCOHI.dtgMain.Items[i].FindControl("chkSelect");

                      if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.HasRole("CCPrice"))
                         priceToChargeCOD[i] = Convert.ToDouble(((TextBox)ctrlControlerSubscriptionForCOHI.dtgMain.Items[i].FindControl("tbxPriceToCharge")).Text);
                      else
                         priceToChargeCOD[i] = Convert.ToDouble(((Label)ctrlControlerSubscriptionForCOHI.dtgMain.Items[i].FindControl("lblPrice")).Text);
                   }

                   ctrlControlerSubscriptionForCOHI.DataBind();

                   for (int i = 0; i < chkSelect.Length; i++)
                   {
                      if (chkSelect[i].Checked)
                      {
                         row = ctrlControlerSubscriptionForCOHI.DataSource.Rows[i];

                         try
                         {
                            if (NewCustomerOrderHeaderInstance == 0)
                            {
                               if (Convert.ToInt32(row["CustomerOrderHeaderInstance"]) == this.Page.OrderInfo.CustomerOrderHeaderInstance && Convert.ToInt32(row["TransID"]) == this.Page.OrderInfo.TransID)
                               {
                                  NewCustomerOrderHeaderInstance = this.Page.BusPayment.UpdateCreditCardInformation(this.ctrlControlerPaymentInfo.CreditCardInfo, Convert.ToInt32(row["CustomerOrderHeaderInstance"]), Convert.ToInt32(row["TransID"]), NewCustomerOrderHeaderInstance, (lastCheckedSub == i), 0, this.Page.CommunicationChannelInstance, this.Page.CommunicationSourceInstance, priceToChargeCOD[i]);
                               }
                               else
                               {
                                  NewCustomerOrderHeaderInstance = this.Page.BusPayment.UpdateCreditCardInformation(this.ctrlControlerPaymentInfo.CreditCardInfo, Convert.ToInt32(row["CustomerOrderHeaderInstance"]), Convert.ToInt32(row["TransID"]), NewCustomerOrderHeaderInstance, (lastCheckedSub == i), this.Page.ProblemCode, this.Page.CommunicationChannelInstance, this.Page.CommunicationSourceInstance, priceToChargeCOD[i]);
                               }
                            }
                            else
                            {
                               if (Convert.ToInt32(row["CustomerOrderHeaderInstance"]) == this.Page.OrderInfo.CustomerOrderHeaderInstance && Convert.ToInt32(row["TransID"]) == this.Page.OrderInfo.TransID)
                               {
                                  this.Page.BusPayment.UpdateCreditCardInformation(this.ctrlControlerPaymentInfo.CreditCardInfo, Convert.ToInt32(row["CustomerOrderHeaderInstance"]), Convert.ToInt32(row["TransID"]), NewCustomerOrderHeaderInstance, (lastCheckedSub == i), 0, this.Page.CommunicationChannelInstance, this.Page.CommunicationSourceInstance, priceToChargeCOD[i]);
                               }
                               else
                               {
                                  this.Page.BusPayment.UpdateCreditCardInformation(this.ctrlControlerPaymentInfo.CreditCardInfo, Convert.ToInt32(row["CustomerOrderHeaderInstance"]), Convert.ToInt32(row["TransID"]), NewCustomerOrderHeaderInstance, (lastCheckedSub == i), this.Page.ProblemCode, this.Page.CommunicationChannelInstance, this.Page.CommunicationSourceInstance, priceToChargeCOD[i]);
                               }
                            }
                         }
                         catch (Exception ex)
                         {
                            DataAccess.Common.ApplicationError.ManageError(ex);
                            throw ex;
                         }
                      }
                   }
                }
                else
                {
                   DataAccess.Common.Message messageManager = new DataAccess.Common.Message(true);
                   messageManager.Add(messageManager.FormatErrorMessage(QSPFulfillment.DataAccess.Common.Message.ERRMSG_CREDIT_CARD_REJECTED_1, "The credit card number is not in a valid format."));
                   messageManager.PrepareErrorMessage();
                   throw new QSPFulfillment.DataAccess.Common.ExceptionFulf(messageManager);
                }
            }
		}

      private static bool ValidateCreditCardNumber(string sCreditCardNumberP, PaymentMethod enumCreditCardTypeP)
      {
          Boolean bResult = true;

          // Check for our test number
          if (sCreditCardNumberP != "112233445566778899")
          {
              // First filter out common test numbers that will pass criteria
              if ((System.Configuration.ConfigurationSettings.AppSettings["DevMode"] != "True") && (sCreditCardNumberP == "5105105105105100" ||
                                                 sCreditCardNumberP == "5555555555554444" ||
                                                 sCreditCardNumberP == "5555555555551111" ||
                                                 sCreditCardNumberP == "5500000000000004" ||
                                                 sCreditCardNumberP == "5424000000000015" ||
                                                 sCreditCardNumberP == "4222222222222" ||
                                                 sCreditCardNumberP == "4111111111111111" ||
                                                 sCreditCardNumberP == "4012888888881881" ||
                                                 sCreditCardNumberP == "4242424242424242" ||
                                                 sCreditCardNumberP == "4007000000027" ||
                                                 sCreditCardNumberP == "5431111111111111"))
              {
                  bResult = false;
              }

              else if (!Helper.Utility.IsNumeric(sCreditCardNumberP))
              {
                  bResult = false;
              }
              //else if ((CreditCardTypeEnum)Convert.ToByte((sCreditCardNumberP.Substring(0, 1)))
              //   != enumCreditCardTypeP)
              //{
              //   bResult = false;
              //}
              else if (!Helper.Utility.IsValidMod10Checksum(sCreditCardNumberP))
              {
                  bResult = false;
              }
              // Make sure the card number is at least 15 charcacters in length
              else if (sCreditCardNumberP.Length < 15)
              {
                  bResult = false;
              }
              else
              {
                  // Grab the initial partial strings
                  String sFirstChar = sCreditCardNumberP.Substring(0, 1);
                  String sFirstTwoChar = sCreditCardNumberP.Substring(0, 2);
                  String sFirstFourChar = sCreditCardNumberP.Substring(0, 4);

                  //CreditCardTypeEnum enCreditCardType = (CreditCardTypeEnum)Convert.ToByte(sCreditCardNumberP.Substring(0, 1));
                  switch (enumCreditCardTypeP)
                  {
                      // MasterCard - Must have a prefix of 51, 52, 53, 54, or 55, and must be 16 digits in length.
                      case PaymentMethod.MasterCard:
                          if (sCreditCardNumberP.Length != 16)
                          {
                              bResult = false;
                          }
                          else if (sFirstTwoChar != "51" && sFirstTwoChar != "52" && sFirstTwoChar != "53" &&
                                  sFirstTwoChar != "54" && sFirstTwoChar != "55")
                          {
                              bResult = false;
                          }
                          break;
                      // Visa - Must have a prefix of 4, and must be 16 digits in length.
                      case PaymentMethod.Visa:
                          if (sCreditCardNumberP.Length != 16)
                          {
                              bResult = false;
                          }
                          else if (sFirstChar != "4")
                          {
                              bResult = false;
                          }
                          break;                    
                      // We do not take any other card types at this time.
                      default:
                          bResult = false;
                          break;
                  }
              }
          }
          return bResult;
      }


		protected void btnValidateAddress_Click(object sender, System.EventArgs e)
		{
			AddressHygiened = true;
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

			SetConfirmButtonVisibility();
			AddressHygiened = true;
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

		private void SetConfirmButtonVisibility()
		{
            this.Page.ConfirmButton.Enabled = this.AddressHygiened || !this.ctrlAddressHygiene.IsAddressHygieneEnabled;
            this.Page.ConfirmButton.Visible = this.AddressHygiened || !this.ctrlAddressHygiene.IsAddressHygieneEnabled;
            this.btnValidateAddress.Visible = this.ctrlAddressHygiene.IsAddressHygieneEnabled;
            this.Page.ConfirmButton.CausesValidation = this.AddressHygiened || !this.ctrlAddressHygiene.IsAddressHygieneEnabled;
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
                for (int i = 0; i < ddlProvince.Items.Count; i++)
                {
                    if (ddlProvince.Items[i].Value == stateProvince)
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
                for (int i = 0; i < this.ddlCountry.Items.Count; i++)
                {
                    if (ddlCountry.Items[i].Value == country)
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
