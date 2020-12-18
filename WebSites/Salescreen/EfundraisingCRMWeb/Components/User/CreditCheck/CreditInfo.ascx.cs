using log4net;

namespace EFundraisingCRMWeb.Components.User.CreditCheck
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using efundraising.EnterpriseComponents;
    using efundraising.EFundraisingCRM;

	/// <summary>
	///		Summary description for CreditInfo.
	/// </summary>
	public partial class CreditInfo : System.Web.UI.UserControl
	{
        public ILog Logger { get; set; }

	    public CreditInfo()
	    {
            Logger = LogManager.GetLogger(GetType());
	    }
		public event System.EventHandler AfterCreditRequestSaved;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			ClearLabels();

            try
            {
                //	SendRequestButton.Enabled = true;
                if (!(IsPostBack))
                {
                    // Put user code to initialize the page here
                   /* if (Session[Global.SessionVariables.CLIENT_ID] == null)
                    {
                        efundraising.Diagnostics.Logger.Error("error in credit info control, client id is null");
                    }
                    else
                    {*/
                        //int leadID = Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);
                        int leadID = Convert.ToInt32(Request["lid"]);
                        Client c = Client.GetClientByLeadID(leadID);
                        int clientID = c.ClientId;
                        string clientSeqCode = c.ClientSequenceCode;
                        int saleID = 0;
                        int paymentMethodID = 0;

                        if (Request["sid"] != null)
                        {
                            saleID = Convert.ToInt32(Request["sid"]);
                            if (Convert.ToInt32(Request["sid"]) == int.MinValue)
                            {
                                SaleIdTextBox.Text = "";
                            }
                            else
                            {
                                SaleIdTextBox.Text = Request["sid"].ToString();
                            }

                        }

                        //payment method id
                        if (Request["pmid"] != null)
                        {
                            paymentMethodID = Convert.ToInt32(Request["pmid"]);
                        }

                        SetCountryStateDropDown();

                        //set client info
                        Client client = Client.GetClientByLeadIDAndSequenceCode(clientID, clientSeqCode);
                        ClientAddress ca = Client.GetBillingClientAddressByID(clientID, clientSeqCode);
                        FirstNameTextBox.Text = client.FirstName;
                        LastNameTextBox.Text = client.LastName;
                        AddressTextBox.Text = ca.StreetAddress;
                        CityTextBox.Text = ca.City;
                        ZipTextBox.Text = ca.ZipCode;
                        try
                        {
                            StateDropDownList.SelectedValue = ca.StateCode;
                        }
                        catch (Exception ex)
                        {
                            MessageLabel.Text = "The Client's State (" + ca.StateCode + ") Code did not match with any US States"; //wrong state code or canada
                            MessageLabel.Visible = true;
                        }

                        if (saleID > 0)
                        {
                            //check current sale
                            //if sc < 2000, not credit check needed
                            DataTable classTotals = (DataTable)Session[Global.SessionVariables.CLASS_TOTALS];
                            if (classTotals != null)
                            {
                                decimal totalAmount = Convert.ToDecimal(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", "1"));
                                decimal totalShipping = Convert.ToDecimal(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalShipping", "1"));
                                decimal totalSC = totalAmount + totalShipping;

                                //credit needed if something else than sc
                              /*  if ((totalSC > 0 && totalSC < 2000) && classTotals.Rows.Count == 1)
                                {
                                    MessageLabel.Text = "No Credit Check required for scratchcard sales under 2000$";
                                    MessageLabel.Visible = true;
                                    SendRequestButton.Enabled = false;
                                }*/

                                //check payment method
                                if (paymentMethodID == 2 || paymentMethodID == 3 || paymentMethodID == 8 || paymentMethodID == 9)
                                {
                                    MessageLabel.Text = "No Credit Check applies for credit card sales";
                                    MessageLabel.Visible = true;
                                    SendRequestButton.Enabled = false;
                                }
                            }
                        }


                   // }


                }
            }catch(Exception x){
                Logger.Error("Error in credit check control", x);
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

		}
		#endregion


		public void SetCountryStateDropDown()
		{
			
			if (StateDropDownList.Items.Count > 0)
				return;
			CountryCollections countryColl = (CountryCollections)Application[Global.shippingCountriesKey];
			StateDropDownList.Items.Clear();
			StateDropDownList.DataValueField = "StateCode";
			StateDropDownList.DataTextField = "StateName";
			Country c = countryColl["US"];
			if (c != null)
			{
				StateDropDownList.DataSource = c.CountryStates;
				StateDropDownList.DataBind();
			}
		}

		private void ClearLabels()
		{
		    MessageLabel.Visible = false;
			FirstNameValidatorLabel.Visible = false;
			LastNameValidatorLabel.Visible = false;
			AddressValidatorLabel.Visible =false;
			CityValidatorLabel.Visible = false;
			ZipValidatorLabel.Visible = false;
			AmountValidatorLabel.Visible = false;
			SSNValidatorLabel.Visible = false;
			
		}

		private bool Validate()
		{
			bool valid = true;
			if (FirstNameTextBox.Text.Trim() == "")
			{
				valid = false;
				FirstNameValidatorLabel.Visible = true;
			}
			if (LastNameTextBox.Text.Trim() == "")
			{
				valid = false;
				LastNameValidatorLabel.Visible = true;
			}
			if (AddressTextBox.Text.Trim() == "")
			{
				valid = false;
				AddressValidatorLabel.Visible = true;
			}
			if (CityTextBox.Text.Trim() == "")
			{
				valid = false;
				CityValidatorLabel.Visible = true;
			}
			if (AddressTextBox.Text.Trim() == "")
			{
				valid = false;
				ZipValidatorLabel.Visible = true;
			}

			return valid;

		}

		protected void SendRequestButton_Click(object sender, System.EventArgs e)
		{
			
				//validate fields
				bool valid = Validate();
				if (valid)
				{
					bool error = false;
			
					//check amount
					string amount = AmountTextBox.Text.Replace("$","").Replace(",","");

					if (!(Helper.IsNumeric(amount)))
					{
						MessageLabel.Text = "The amount entered is invalid.";
						MessageLabel.Visible = true;
						error = true;
						AmountValidatorLabel.Visible = true;
					}
			

					//check ssn number
					string ssn = SSNTextBox.Text;
					if (ssn.Length != 9)
					{
						MessageLabel.Text = "The ssn number is not in the correct format.";
						MessageLabel.Visible = true;
						error = true;
						SSNValidatorLabel.Visible = true;

					}


					if (!(error))
					{
						CreditCheckRequest ccr = new CreditCheckRequest();
						ccr.LeadID = Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);
						ccr.ConsultantID = Convert.ToInt32(Session[Global.SessionVariables.CONSULTANT_ID]);
						ccr.CreditStatusID = 4;
						ccr.RequestDate = DateTime.Now;
						ccr.FirstName = FirstNameTextBox.Text;
						ccr.LastName = LastNameTextBox.Text;
						ccr.Address = AddressTextBox.Text;
						ccr.City = CityTextBox.Text;
						ccr.State = StateDropDownList.SelectedItem.Value;
						ccr.SSN = SSNTextBox.Text;
						ccr.AmountRequested = Convert.ToDouble(AmountTextBox.Text.Replace("$","").Replace(",",""));
						ccr.Zip = ZipTextBox.Text;
						ccr.Insert();
		
						AfterCreditRequestSaved(this, new EventArgs());
		
					}
				}
				else
				{
					MessageLabel.Text = "Some information is missing.";
					MessageLabel.Visible = true;
				}
			
			
																								 
		}

		protected void SaleIdTextBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
