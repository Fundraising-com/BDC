using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
//using EfundraisingCRM.com.efundraising.webservices;
using efundraising.EFundraisingCRM;
using efundraising.EFundraisingCRMWeb;
using efundraising.Diagnostics;
using QSP.Business.Fulfillment;
using efundraising.eFundData;
using EFundraisingCRMWeb.Components.Server;
using EfundraisingCRM.ePay.WebReference;
using MyExtensionMethods;
using EFundraisingCRMWeb.Components.Server;
//using  EfundraisingCRM.com.qsp.dev_wsi;

//using efundraising.EFundraisingCRMWeb.localhost;


namespace efundraising.EFundraisingCRMWeb.Sales.CreditCard
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page
	{
     		
        
		private Sale[] sales = null;
		private const string _CLIENT_ = "ClientObject";
		private const string _SETTLE_ARRAY_ = "SettleArray";
		private const string _AFFECTED_SALES_ = "AffectedSales";
		private const string _POSITIVE_PAYMENTS_ = "PositivePayments";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			if (!IsPostBack)
			{
                versionLabel.Text = "v.2.2";
				FillControls();
				FillSalesDataGrid();

                DevLabel.Visible = false;
                bool isProd = Convert.ToBoolean(ManageSaleScreen.GetValueFromWebConfig("ePay.Production", "isProduction"));
                if (!isProd)
                {
                    DevLabel.Visible = true;
                }
              
			}
			
		}
		
		// Fill the datagrid with the Clients pending sales
		private void FillControls()
		{
			// get client info from QueryString
			int clientId = int.Parse(Request.QueryString["clientId"].ToString());
			string clientSequenceCode = Request.QueryString["clientSequenceCode"].ToString();
				
			// get the Client Object filled from the database
			Client client = Client.GetClientByID(clientId, clientSequenceCode);
			
			if (client != null)
			{
				Session[_CLIENT_] = client;
				
				// get the client's billing address and use it to fill the fields
				ClientAddress billingAddress = ClientAddress.GetClientAddressByIdSequenceAddressType(client.ClientId, client.ClientSequenceCode, ClientAddressType.BillingAddress.AddressType);
				
				if (billingAddress != null)
				{
				//if (billingAddress.CountryCode == efundraising.EFundraisingCRM.Country.UnitedStates.CountryCode)
				//	{
						sales = Sale.GetSalesByClient(client);
						
						// get the most recent sale for the client and use it's credit card info to fill the fields
						Sale latestSale = Sale.GetLatestSaleByLeadID(client.LeadId);
						if (latestSale != null)
						{
                            if (latestSale.PaymentMethodId == efundraising.EFundraisingCRM.PaymentMethod.VISA.PaymentMethodId
                                || latestSale.PaymentMethodId == efundraising.EFundraisingCRM.PaymentMethod.MASTERCARD.PaymentMethodId
                                || latestSale.PaymentMethodId == efundraising.EFundraisingCRM.PaymentMethod.Discover.PaymentMethodId
                                || latestSale.PaymentMethodId == efundraising.EFundraisingCRM.PaymentMethod.AMEX.PaymentMethodId)
							{
								DropDownListCCType.SelectedValue = latestSale.PaymentMethodId.ToString();
							}
						
							TextBoxCCNumber.Text = latestSale.CreditCardNo;
							
							if (TextBoxCCNumber.Text.Length == 15 || TextBoxCCNumber.Text.Length == 16)
							{
								try
								{
									string[] expiryDate = latestSale.ExpiryDate.Split("/".ToCharArray());
									DropDownListMonth.SelectedValue =  expiryDate[0];
									DropDownListYear.SelectedValue =  expiryDate[1].Substring(expiryDate[1].Length - 2, 2);
								}
								catch
								{
									Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('Could not get the Expiry Date from the sale infos, please enter it manually.');</script>");
								}
							}
							else
								TextBoxCCNumber.Text = "";
						}

						
						
						TextBoxStreetAddress.Text = billingAddress.StreetAddress;
						TextBoxCity.Text = billingAddress.City;
						TextBoxState.Text = billingAddress.StateCode;
						TextBoxZipCode.Text = billingAddress.ZipCode;
                        DropDownListCountry.SelectedValue = billingAddress.CountryCode;

					/*}
					else
					{
						Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('Invalid country, the billing address of the client must be in the US.');</script>");
					}*/
				}
			}
		}

		private void FillSalesDataGrid()
		{
			// Fill the sales DataGrid
			if (sales != null)
			{		

				DataTable dt = new DataTable();
				dt.Columns.Add("sale_id");
				dt.Columns.Add("product_class");
				dt.Columns.Add("sale_status");
				dt.Columns.Add("amount");
				dt.Columns.Add("amount_display");
					
					
				for (int i = 0; i < sales.Length; i++)
				{
						
					if (sales[i].SalesStatusId != SalesStatus.Cancelled.SalesStatusID 
						&& sales[i].SalesStatusId != SalesStatus.PendingCancellation.SalesStatusID 
						&& sales[i].SalesStatusId != SalesStatus.Unreachable.SalesStatusID)
					{
						DataRow dr = dt.NewRow();
						dr["sale_id"] =	 sales[i].SalesId;
						EFundraisingCRM.ProductClass pdCl = sales[i].GetProductClass();
						if (pdCl != null)
							dr["product_class"] = pdCl.Description;
						EFundraisingCRM.SalesStatus SSt = SalesStatus.GetSalesStatusByID(sales[i].SalesStatusId);
						if (SSt != null)
							dr["sale_status"] = SSt.Description;
						double amountToDisplay = sales[i].TotalAmount - sales[i].CalculateTotalPaymentsAndAdjustmentsAmount();
						dr["amount"] = amountToDisplay.ToString();
						dr["amount_display"] = "$" + amountToDisplay.ToString("N", new CultureInfo("en-US", false).NumberFormat);
							
						//if (amountToDisplay > 0) // for now we display all the sales even if already paid in full
							dt.Rows.Add(dr);
					}
				}
					
				SalesDataGrid.DataSource = dt;
				SalesDataGrid.DataBind();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Private Methods

		private void Process()
		{/*
			double amountToProcess = double.Parse(AmountTextBox.Text);
			double realTotal = 0;
            double selectedSaleTotal = 0;


            if (amountToProcess > 0)
            {
                Client client = (Client)Session[_CLIENT_];
                ArrayList salesInfo = new ArrayList();
                SaleCollection sales = new SaleCollection();
                PaymentCollection payments = new PaymentCollection();
                CommentsCollection comments = new CommentsCollection();

                // check which sales are checked for processing
                for (int i = 0; i < SalesDataGrid.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
                    realTotal += double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
                    // if sale is selected, add its saleId and Amount to an array of all selected sales
                    if (chk.Checked)
                    {
                        salesInfo.Add(new string[] { SalesDataGrid.Items[i].Cells[1].Text, SalesDataGrid.Items[i].Cells[4].Text });
                        selectedSaleTotal += double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
                    }
                }

                // if at least one sale was select, begin processing				
                if (salesInfo.Count > 0)
                {

                    // ******* Process the Credit Card
                    string response = null;
                    PaymenTech paymenTech = new PaymenTech();


                    // authorize the payment
                    // response = "0|062552|1878877|1454848|";
                    response = paymenTech.Authorize(amountToProcess, TextBoxCCNumber.Text, int.Parse(DropDownListMonth.SelectedValue), int.Parse(DropDownListYear.SelectedValue), TextBoxCVV2.Text, TextBoxName.Text, TextBoxStreetAddress.Text, "", TextBoxCity.Text, TextBoxState.Text, TextBoxZipCode.Text, "", bool.Parse(SecurityRadioButtonList.SelectedValue));
                    string[] authArray = response.Split("|".ToCharArray());

                    // check if authorization was successful
                    if (authArray[0] == "0" && authArray[3] != "-1")
                    {
                        response = paymenTech.Settle(int.Parse(authArray[3]));
                        string[] settleArray = response.Split("|".ToCharArray());

                        // check if settle was successful
                        if (settleArray[0] == "0")
                        {

                            if (amountToProcess <= selectedSaleTotal + 0.01)
                            {
                                // if we have partial payments to do, split the amount equally between sales
                                double amountPerSale = 0;

                                // create the Payments and Comments, and Update the sales from salesInfo(the list of selected sales) then commit to the DB.
                                foreach (string[] sale in salesInfo)
                                {
                                    try
                                    {
                                        double processedAmountForSale = 0;
                                        Sale currentSale = Sale.GetSaleByID(int.Parse(sale[0]));


                                        //
                                        double amt = Convert.ToDouble(sale[1]);// sale.
                                        amountPerSale = amountToProcess * (amt / selectedSaleTotal);

                                        efundraising.EFundraisingCRM.Payment currentPayment = new efundraising.EFundraisingCRM.Payment();
                                        currentPayment.SalesId = currentSale.SalesId;
                                        currentPayment.PaymentNo = currentPayment.GetNextPaymentNo();
                                        currentPayment.PaymentMethodId = byte.Parse(DropDownListCCType.SelectedValue);
                                        currentPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
                                        currentPayment.PaymentEntryDate = DateTime.Now;
                                        currentPayment.CashableDate = DateTime.Now;
                                        currentPayment.NameOnCard = TextBoxName.Text;
                                        //get last 4 numbers
                                        string last4 = TextBoxCCNumber.Text.Substring(TextBoxCCNumber.Text.Length - 4, 4);
                                        currentPayment.CreditCardNo = "****" + last4;
                                        
                                        currentPayment.ExpiryDate = DropDownListMonth.SelectedValue + "/" + DropDownListYear.SelectedValue;
                                        currentPayment.AuthorizationNumber = settleArray[1];
                                        currentPayment.ForeignOrderId = int.Parse(settleArray[2]);
                                        // check if the amount to be processed covers the whole balance
                                        // or is just partial
                                        currentPayment.PaymentAmount = amountPerSale;
                                        processedAmountForSale = amountPerSale;

                                        //dont confirm
                                        if (amt > amountPerSale + 0.01)
                                        {
                                            //currentPayment.PaymentAmount = amountPerSale;
                                             //processedAmountForSale = amountPerSale;
                                        }
                                        else
                                        {
                                            // currentPayment.PaymentAmount = amountToProcess; // full payment
                                            // processedAmountForSale = amountToProcess;
                                            // update the sale infos to close it
                                            if (currentSale.SalesStatusId != SalesStatus.Confirmed.SalesStatusID)
                                            {
                                                //UPDATE: DO NOT AUTO CONFIRM 
                                                currentSale.SalesStatusId = SalesStatus.Confirmed.SalesStatusID;
                                                currentSale.ConfirmedDate = DateTime.Now;
                                            }
                                            if (currentSale.UpfrontPaymentRequired < 0)
                                                currentSale.UpfrontPaymentRequired = 0;
                                            currentSale.Fuelsurcharge = 0;
                                        }
                                        currentPayment.CommissionPaid = false;

                                        // create a comment for the sale
                                        Comments currentComment = new Comments(int.MinValue,
                                            3,  // high priority
                                            currentSale.SalesId,
                                            currentSale.ConsultantId,
                                            currentSale.LeadId,
                                            9, // other 
                                            DateTime.Now,
                                            "CC processed by Paymentech OrderID: " + settleArray[2] + " for $" + amountToProcess + ". $" + processedAmountForSale + " of this amount was applied to this sale.");


                                        payments.Add(currentPayment);
                                        sales.Add(currentSale);
                                        comments.Add(currentComment);

                                    }
                                    catch (Exception ex)
                                    {
                                        PublishError("Error on Sales and Payments creation.", "", ex);
                                        ProcessButton.Enabled = true;
                                        Session["ClickOnce"] = 0;
                                    }
                                }//for each


                                if (payments.Count > 0)
                                {
                                    try
                                    {
                                        Session[_POSITIVE_PAYMENTS_] = payments;

                                        // commit to DB
                                        string message = "";
                                        int createUserID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID"));
                                        TransactionController trans = new TransactionController();
                                        bool success = trans.InsertPaymentsAndCommentsAndUpdateSales(payments, sales, comments, createUserID,ref message);

                                        if (success)
                                        {
                                            // display success message to the user
                                            LabelStatus.CssClass = "BigTextBold Success";
                                            LabelStatus.Text = "Transaction Successful!<br>"
                                                + "Authorization Number: " + settleArray[1] + "<br>"
                                                + "PaymenTech OrderID: " + settleArray[2] + "<br>"
                                                + "Affected Sale(s):<br>";
                                            foreach (string[] sale in salesInfo)
                                            {
                                                LabelStatus.Text += sale[0] + "<br>";
                                            }
                                            Session[_SETTLE_ARRAY_] = settleArray;
                                            Session[_AFFECTED_SALES_] = salesInfo;
                                            VoidTransactionLinkButton.Visible = true;
                                            LabelStatus.Visible = true;
                                        }
                                        else
                                        {
                                            ManageSaleScreen.SendMail(message, 0, Request.Url.Authority, "SaleSupport");
                                            LabelStatus.CssClass = "BigTextBold Alert";
                                            LabelStatus.Text = message;
                                            LabelStatus.Visible = true;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        PublishError("Error on Transaction", "", ex);
                                        LabelStatus.CssClass = "BigTextBold Alert";
                                        LabelStatus.Text = "The credit card was billed but an error occured after the processing, please contact support.<br><br>";
                                        LabelStatus.Visible = true;
                                    }
                                    FillSalesDataGrid();
                                }
                            }
                            else
                            {
                                // display message on settle error
                                LabelStatus.CssClass = "BigTextBold Alert";
                                LabelStatus.Text = "Total amount cannot exceed sale amounts!<br>";
                                LabelStatus.Visible = true;
                                ProcessButton.Enabled = true;
                                Session["ClickOnce"] = 0;
                            }

                            }
                            else
                            {
                                // display message on settle error
                                LabelStatus.CssClass = "BigTextBold Alert";
                                LabelStatus.Text = "Settle Failure!<br>"
                                    + "Message: " + settleArray[4];
                                LabelStatus.Visible = true;
                                ProcessButton.Enabled = true;
                                Session["ClickOnce"] = 0;
                            }
                        }
                        else
                        {

                            // display message on authorization error
                            string message = "";
                            switch (authArray[0])
                            {
                                case "-1": message = "General Failure";
                                    break;
                                case "-2": message = "Authentication Failed";
                                    break;
                                case "-3": message = "Action Disallowed";
                                    break;
                                case "-4": message = "Malformed Request";
                                    break;
                                case "-5": message = "Action Failed";
                                    break;
                            }
                            LabelStatus.CssClass = "BigTextBold Alert";
                            LabelStatus.Text = message + "<br>"
                                + "Message: " + authArray[4];
                            LabelStatus.Visible = true;
                            ProcessButton.Enabled = true;
                            Session["ClickOnce"] = 0;
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You must select at least one sale.');</script>");
                        ProcessButton.Enabled = true;
                        Session["ClickOnce"] = 0;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You must enter an amount greater than 0.');</script>");
                    ProcessButton.Enabled = true;
                    Session["ClickOnce"] = 0;
                }
            */
		}

		private void VoidTransaction(int paymentId)
		{
			string response = null;
			string[] settleArray = (string[])Session[_SETTLE_ARRAY_];
			ArrayList affectedSales = (ArrayList)Session[_AFFECTED_SALES_];
			PaymenTech paymenTech = new PaymenTech();
			SaleCollection sales = new SaleCollection();
			PaymentCollection newPayments = new PaymentCollection();
			CommentsCollection comments = new CommentsCollection();
			PaymentCollection payments = (PaymentCollection)Session[_POSITIVE_PAYMENTS_];
										
			// void a transaction
			response = paymenTech.Refund(paymentId);
			string[] authArray = response.Split("|".ToCharArray());

			// check if authorization was successful
			if (authArray[0] == "0" && authArray[1] == settleArray[1] && authArray[2] == paymentId.ToString())
			{
				
				try
				{
					
					// update the sales to revert them back to their unpaid status
					foreach (string[] sale in affectedSales)
					{
						Sale currentSale = Sale.GetSaleByID(int.Parse(sale[0]));
						currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID;
						currentSale.ConfirmedDate = DateTime.MinValue;
						currentSale.Fuelsurcharge = 0;
						currentSale.ArStatusId = ARStatus.NotPaid.ARStatusID;

						sales.Add(currentSale);

						// create a comment for the sale
						Comments currentComment = new Comments(int.MinValue, 
							3,  // high priority
							currentSale.SalesId, 
							currentSale.ConsultantId, 
							currentSale.LeadId, 
							9, // other 
							DateTime.Now, 
							"CC transaction voided through Paymentech OrderID: " + settleArray[2]);
						
						comments.Add(currentComment);

					}

					
					// create new negative amount payments to cancel the previous payments
                    foreach (efundraising.EFundraisingCRM.Payment p in payments)
					{
                        efundraising.EFundraisingCRM.Payment currentPayment = new efundraising.EFundraisingCRM.Payment();
						currentPayment.SalesId = p.SalesId;
						currentPayment.PaymentNo = currentPayment.GetNextPaymentNo();
						currentPayment.PaymentMethodId = p.PaymentMethodId;
						currentPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
						currentPayment.PaymentEntryDate = DateTime.Now;
						currentPayment.CashableDate = DateTime.Now;
						currentPayment.NameOnCard = p.NameOnCard;
                        //get last 4 numbers
                        string last4 = p.CreditCardNo.Substring(p.CreditCardNo.Length - 4, 4);
                        currentPayment.CreditCardNo = "****" + last4;
                        
						currentPayment.ExpiryDate = p.ExpiryDate;
						currentPayment.AuthorizationNumber = p.AuthorizationNumber;
						currentPayment.PaymentAmount = (p.PaymentAmount * -1);

						newPayments.Add(currentPayment);
					}

					// commit to DB
					TransactionController trans = new TransactionController();
                    int createUserID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID"));

                    string message = "";
					bool success = trans.InsertPaymentsAndCommentsAndUpdateSales(newPayments, sales, comments,createUserID, ref message);
                    if (!success)
                    {
                       ManageSaleScreen.SendMail("An error occured while voiding a payment..." + message, 0, Request.Url.Authority, "SaleSupport");
                    }
				
				}
				catch (Exception ex)
				{
					Logger.LogError("Unable to update sale after voiding.", ex);
				}

				// display success message to the user
               bool isSecure = IsDataSecure(authArray[1]);
                isSecure = IsDataSecure(authArray[2]);
                if (isSecure)
                {
                    LabelStatus.CssClass = "BigTextBold Success";
                    LabelStatus.Text = "Transaction was successfully voided.<br>"
                        + "Authorization Number: " + authArray[1] + "<br>"
                        + "PaymenTech OrderID: " + authArray[2] + "<br>";
                }
                else
                {
                    LabelStatus.CssClass = "BigTextBold Success";
                    LabelStatus.Text = "Transaction returned a security error.<br>"
                        + "Authorization Number: <br>"
                        + "PaymenTech OrderID: <br>";
                }
			}
			else
			{
				// display error message to the user
				LabelStatus.CssClass = "BigTextBold Alert";
				LabelStatus.Text = "Transaction is not voidable.<br>"
					+ "Message: " + authArray[4] + "<br>";
			}
		}
		
		#endregion

        private bool IsDataSecure(string text)
        {
            int pos = text.IndexOf("%");
            if (pos > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

		#region Protected Methods
		
		protected void ProcessButton_Click(object sender, System.EventArgs e)
		{
            if (Page.IsValid)
            {
                int nbClick = Convert.ToInt32(Session["ClickOnce"]) + 1;
                Session["ClickOnce"] = nbClick;
                ProcessButton.Enabled = false;
                if (nbClick == 1)
                {
                    Process();

                
                
                }
            }
                
                
		}


		// updates the total when a include checkbox is changed
		protected void CheckBoxInclude_CheckeckChanged(object sender, System.EventArgs e)
		{
            //current one
            RadioButton oRb1 = (RadioButton)sender;


			double amountToProcess = 0;
			
			for (int i=0; i < SalesDataGrid.Items.Count; i++) 
			{
				RadioButton chk = (RadioButton) SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
                if (chk.Equals(oRb1))
                {
                    double currentSaleAmount = double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
                    if (chk.Checked)
                        amountToProcess += currentSaleAmount;
                }
                else
                {
                    chk.Checked = false;
                }
			}
			
			AmountTextBox.Text = amountToProcess.ToString("N", new CultureInfo("en-US", false).NumberFormat);
		}

		// checks all sales and updates the Amount
		protected void LinkButtonAll_Click(object sender, System.EventArgs e)
		{
			bool check = true;
			double amountToProcess = 0;
			
			for (int i=0; i < SalesDataGrid.Items.Count; i++) 
			{
				CheckBox chk = (CheckBox) SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
				double currentSaleAmount = double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
				chk.Checked = check;
				if (check)
					amountToProcess += currentSaleAmount;
			}
			
			AmountTextBox.Text = amountToProcess.ToString("N", new CultureInfo("en-US", false).NumberFormat);
		}

		// unchecks all sales and updates the Amount to 0
		protected void LinkButtonNone_Click(object sender, System.EventArgs e)
		{
			bool check = false;
			
			for (int i=0; i < SalesDataGrid.Items.Count; i++) 
			{
				CheckBox chk = (CheckBox) SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
				double currentSaleAmount = double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
				chk.Checked = check;
			}
			
			AmountTextBox.Text = "0";
		}

		protected void VoidTransactionLinkButton_Click(object sender, System.EventArgs e)
		{
			string[] settleArray = (string[])Session[_SETTLE_ARRAY_];
			
			if (settleArray[2] != null)
			{
				VoidTransactionLinkButton.Visible = false;
				try
				{
					VoidTransaction(int.Parse(settleArray[2]));
				}
				catch (Exception ex)
				{
					Logger.LogError("Could not get orderId from session to void a transaction", ex);
				}
			}
		}

		#endregion
		
		#region Logger

		private void PublishError(string message)
		{
			PublishError(message, "");
		}

		private void PublishError(string message, string methodCall)
		{
			PublishError(message, methodCall, null);
		}

		private void PublishError(string message, string methodCall, Exception ex)
		{
			Logger.LogError(message + " " + methodCall, ex);
		}
		#endregion

        protected void ProcessButtontest_Click(object sender, EventArgs e)
        {
            
         	double amountToProcess = double.Parse(AmountTextBox.Text);
			double realTotal = 0;
            double selectedSaleTotal = 0;
        


				Client client = (Client)Session[_CLIENT_];
				ArrayList salesInfo = new ArrayList();
				SaleCollection sales = new SaleCollection();
				PaymentCollection payments = new PaymentCollection();
				CommentsCollection comments = new CommentsCollection();
					
				// check which sales are checked for processing
				for (int i=0; i < SalesDataGrid.Items.Count; i++) 
				{
					CheckBox chk = (CheckBox) SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
					realTotal += double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
					// if sale is selected, add its saleId and Amount to an array of all selected sales
                    if (chk.Checked)
                    {
                        salesInfo.Add(new string[] { SalesDataGrid.Items[i].Cells[1].Text, SalesDataGrid.Items[i].Cells[4].Text });
                        selectedSaleTotal += double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
                    }
                    

                        
				}
				

			
						
					// ******* Process the Credit Card
					string response = null;
					//PaymenTech paymenTech = new PaymenTech();

                    EfundraisingCRM.com.efundraising.webservices.PaymenTech paymenTech = new EfundraisingCRM.com.efundraising.webservices.PaymenTech();

                    

					// authorize the payment
					// response = "0|062552|1878877|1454848|";
					response = paymenTech.Authorize(amountToProcess, TextBoxCCNumber.Text, int.Parse(DropDownListMonth.SelectedValue), int.Parse(DropDownListYear.SelectedValue), TextBoxCVV2.Text, TextBoxName.Text, TextBoxStreetAddress.Text, "", TextBoxCity.Text, TextBoxState.Text, TextBoxZipCode.Text, "", bool.Parse(SecurityRadioButtonList.SelectedValue));
					//string[] authArray = response.Split("|".ToCharArray());
                    string[] authArray = { "0", "0", "0", "0" };

					
						//response = paymenTech.Settle(int.Parse(authArray[3]));
						//string[] settleArray = response.Split("|".ToCharArray());
                    string[]  settleArray = authArray;
 

                    if (amountToProcess <= selectedSaleTotal + 0.01)
                    {
                        // if we have partial payments to do, split the amount equally between sales
                        double amountPerSale = 0;

                        // create the Payments and Comments, and Update the sales from salesInfo(the list of selected sales) then commit to the DB.
                        foreach (string[] sale in salesInfo)
                        {
                            try
                            {
                                double processedAmountForSale = 0;
                                Sale currentSale = Sale.GetSaleByID(int.Parse(sale[0]));


                                //
                                double amt = Convert.ToDouble(sale[1]);// sale.
                                 amountPerSale = amountToProcess * (amt / selectedSaleTotal);



                                efundraising.EFundraisingCRM.Payment currentPayment = new efundraising.EFundraisingCRM.Payment();
                                currentPayment.SalesId = currentSale.SalesId;
                                currentPayment.PaymentNo = currentPayment.GetNextPaymentNo();
                                currentPayment.PaymentMethodId = byte.Parse(DropDownListCCType.SelectedValue);
                                currentPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
                                currentPayment.PaymentEntryDate = DateTime.Now;
                                currentPayment.CashableDate = DateTime.Now;
                                currentPayment.NameOnCard = TextBoxName.Text;
                                //get last 4 numbers
                                string last4 = TextBoxCCNumber.Text.Substring(TextBoxCCNumber.Text.Length - 4, 4);
                                currentPayment.CreditCardNo = "****" + last4;
                                currentPayment.ExpiryDate = DropDownListMonth.SelectedValue + "/" + DropDownListYear.SelectedValue;
                                currentPayment.AuthorizationNumber = settleArray[1];
                                currentPayment.ForeignOrderId = int.Parse(settleArray[2]);



                                                             
                                // check if the amount to be processed covers the whole balance
                                // or is just partial

                                currentPayment.PaymentAmount = amountPerSale; 
                                processedAmountForSale = amountPerSale;

                                if (selectedSaleTotal > amountPerSale + 0.01 )
                                {
                                    //currentPayment.PaymentAmount = amountPerSale;
                                    //processedAmountForSale = amountPerSale;
                                }
                                else
                                {
                                   // currentPayment.PaymentAmount = amountToProcess; // full payment
                                   // processedAmountForSale = amountToProcess;
                                    // update the sale infos to close it
                                    if (currentSale.SalesStatusId != SalesStatus.Confirmed.SalesStatusID)
                                    {
                                        //UPDATE: DO NOT AUTO CONFIRM 
                                        currentSale.SalesStatusId = SalesStatus.Confirmed.SalesStatusID;
                                        currentSale.ConfirmedDate = DateTime.Now;
                                    }
                                    if (currentSale.UpfrontPaymentRequired < 0)
                                        currentSale.UpfrontPaymentRequired = 0;
                                    currentSale.Fuelsurcharge = 0;
                                }
                                currentPayment.CommissionPaid = false;

                                // create a comment for the sale
                                Comments currentComment = new Comments(int.MinValue,
                                    3,  // high priority
                                    currentSale.SalesId,
                                    currentSale.ConsultantId,
                                    currentSale.LeadId,
                                    9, // other 
                                    DateTime.Now,
                                    "CC processed by Paymentech OrderID: " + settleArray[2] + " for $" + amountToProcess + ". $" + processedAmountForSale + " of this amount was applied to this sale.");


                                payments.Add(currentPayment);
                                sales.Add(currentSale);
                                comments.Add(currentComment);

                            }
                            catch (Exception ex)
                            {
                                PublishError("Error on Sales and Payments creation.", "", ex);
                            }
                        }


                        if (payments.Count > 0)
                        {
                            try
                            {
                                Session[_POSITIVE_PAYMENTS_] = payments;

                                // commit to DB
                                TransactionController trans = new TransactionController();
                                int createUserID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID"));
                                string message = "";
                                bool success = trans.InsertPaymentsAndCommentsAndUpdateSales(payments, sales, comments, createUserID, ref message);
                                if (success)
                                {
                                    // display success message to the user
                                    LabelStatus.CssClass = "BigTextBold Success";
                                    LabelStatus.Text = "Transaction Successful!<br>"
                                        + "Authorization Number: " + settleArray[1] + "<br>"
                                        + "PaymenTech OrderID: " + settleArray[2] + "<br>"
                                        + "Affected Sale(s):<br>";
                                    foreach (string[] sale in salesInfo)
                                    {
                                        LabelStatus.Text += sale[0] + "<br>";
                                    }
                                    Session[_SETTLE_ARRAY_] = settleArray;
                                    Session[_AFFECTED_SALES_] = salesInfo;
                                    VoidTransactionLinkButton.Visible = true;
                                    LabelStatus.Visible = true;
                                }
                                else
                                {
                                    ManageSaleScreen.SendMail(message, 0, Request.Url.Authority, "SaleSupport");
                                    LabelStatus.CssClass = "BigTextBold Alert";
                                    LabelStatus.Text = message;
                                    LabelStatus.Visible = true;

                                }

                            }
                            catch (Exception ex)
                            {
                                PublishError("Error on Transaction", "", ex);
                                LabelStatus.CssClass = "BigTextBold Alert";
                                LabelStatus.Text = "The credit card was billed but an error occured after the processing, please contact support.<br><br>";
                                LabelStatus.Visible = true;
                            }
                            FillSalesDataGrid();
                        }
                    }
                    else
                    {
                        //ERROR
                        LabelStatus.Text = "The amount cannot be greater than the sale total.<br><br>";
                        LabelStatus.Visible = true;
                         
                    }
								
        }

        protected void newProcessButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int nbClick = Convert.ToInt32(Session["ClickOnce"]) + 1;
                Session["ClickOnce"] = nbClick;
                newProcessButton.Enabled = false;
                if (nbClick == 1)
                {
                    ProcessNew();



                }
            }
        }

        private void ProcessNew()
        {
            double amountToProcess = double.Parse(AmountTextBox.Text);
            double realTotal = 0;
            double selectedSaleTotal = 0;
            int si = 0;
            
            //this linw will make .net ignore the out of data web service certificate
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };

                       


            if (amountToProcess > 0)
            {
                Client client = (Client)Session[_CLIENT_];
                ArrayList salesInfo = new ArrayList();
                SaleCollection sales = new SaleCollection();
                PaymentCollection payments = new PaymentCollection();
                CommentsCollection comments = new CommentsCollection();

                // check which sales are checked for processing
                for (int i = 0; i < SalesDataGrid.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
                    realTotal += double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
                    // if sale is selected, add its saleId and Amount to an array of all selected sales
                    if (chk.Checked)
                    {
                      //  si = SalesDataGrid.Items[i].Cells[1].Text;
                      //  si = SalesDataGrid.Items[i].Cells[4].Text;

                        salesInfo.Add(new string[] { SalesDataGrid.Items[i].Cells[1].Text, SalesDataGrid.Items[i].Cells[4].Text });
                        selectedSaleTotal += double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
                    }
                }

                // if at least one sale was select, begin processing				
                if (salesInfo.Count > 0)
                {

                    // ******* Process the Credit Card
                    string response = null;
                    BatchPaymentSystemWebservice oClient = new BatchPaymentSystemWebservice();
                    
                               
                    //name
                    int pos = TextBoxName.Text.IndexOf(" ", 0);
                    string firstName = TextBoxName.Text;
                    string lastname = "";
                    if (pos > -1)
                    {
                        firstName = TextBoxName.Text.Substring(0, pos);
                        lastname = TextBoxName.Text.Substring(pos + 1, TextBoxName.Text.Length - pos - 1);
                    }

                    if (firstName.Length > 15)
                    {
                        firstName = firstName.Substring(0, 15);
                    }

                    if (lastname.Length > 15)
                    {
                        lastname = lastname.Substring(0, 15);
                    } 
                    
                    string address = TextBoxStreetAddress.Text;
                    if (address.Length > 30)
                    {
                        address = address.Substring(0, 30);
                    }
                    
                    //amount
                    string amnt = amountToProcess.ToString();
                    string cents = "";
                    pos = amnt.IndexOf(".");
                    if (pos > 0)
                    {
                        cents = (amnt + "00").Substring(pos + 1, 2);
                        amnt = amnt.Substring(0, pos) + cents;
                    }
                    else
                    {
                        amnt = amnt + "00";

                    }
                 


                    int amount = Convert.ToInt32(amnt);
                    //credit card
                    CardType ccType;
                    switch (DropDownListCCType.SelectedItem.Text.ToUpper())
                    {
                        case "VISA":
                            ccType = CardType.VISA;
                            break;
                        case "MASTERCARD":
                            ccType = CardType.MASTERCARD;
                            break;
                        case "AMEX":
                            ccType = CardType.AMEX;
                            break;
                        case "DISCOVER":
                            ccType = CardType.DISCOVER;
                            break;
                        default:
                            ccType = CardType.DISCOVER;
                            break;

                    }

                    int salesScreenCode = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("saleScreenCode", "value"));
                    CountryCode country = CountryCode.US;
                    bool canada = false;
                    if (DropDownListCountry.SelectedValue == "CA")
                    {
                        country = CountryCode.CA;
                        canada = true;
                        salesScreenCode = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("saleScreenCodeCanada", "value"));
                    }

                 
                    string appNo = "";
                    bool isProd = Convert.ToBoolean(ManageSaleScreen.GetValueFromWebConfig("ePay.Production", "isProduction"));
                    if (isProd && !canada)
                    {
                        appNo = ManageSaleScreen.GetValueFromWebConfig("ePayAppNo", "prod");
                    }
                    else if(!isProd && !canada)
                    {
                        appNo = ManageSaleScreen.GetValueFromWebConfig("ePayAppNo", "dev");
                    }
                    else if (isProd && canada)
                    {
                        appNo = ManageSaleScreen.GetValueFromWebConfig("ePayAppNoCanada", "prod");
                    }
                    else //test canada
                    {
                        appNo = ManageSaleScreen.GetValueFromWebConfig("ePayAppNoCanada", "dev");
                    }


                    bool isSaleSupport = ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_SaleSupport);
                    if (amountToProcess <= selectedSaleTotal + 0.01 || isSaleSupport)
                    {
                          //limit zip code to 5 char for US
                        string zip = "";
                        if (!canada && TextBoxZipCode.Text.Trim().Length > 5)
                        {

                           zip = TextBoxZipCode.Text.Trim().Substring(0, 5);
                        }
                        else
                        {
                            zip = TextBoxZipCode.Text.Trim();
                            zip = zip.Replace(" ","");
                        }

                        bool badZip = false;
                        string stateCode = TextBoxState.Text.Trim();
                        
                        if (stateCode.Length != 2)
                        {
                            badZip = true;
                        }
                        if (!canada && !zip.IsNumeric())
                        {
                            badZip = true;
                        }


                        BPPSTxResponse oResponse = null;
                        if (!badZip)
                        {

                          oResponse = oClient.AuthDepositRealTime(
                                               salesScreenCode,
                                               firstName.Trim(),
                                               lastname.Trim(),
                                               address.Trim(),
                                               "",
                                               TextBoxCity.Text.Trim(),
                                               stateCode,
                                               zip,
                                               country,
                                               ccType,
                                               TextBoxCCNumber.Text,
                                               int.Parse(DropDownListMonth.SelectedValue),
                                               int.Parse(DropDownListYear.SelectedValue),
                                               amount, //last 2 digits are cents
                                               "efundraising.com",
                                               "",
                                               //salesInfo[0].ToString(),
                                               "0",
                                               appNo); //sale screen app no 

                  
                            

                           // response = paymenTech.Authorize(amountToProcess, TextBoxCCNumber.Text, int.Parse(DropDownListMonth.SelectedValue), int.Parse(DropDownListYear.SelectedValue), TextBoxCVV2.Text, TextBoxName.Text, TextBoxStreetAddress.Text, "", TextBoxCity.Text, TextBoxState.Text, TextBoxZipCode.Text, "", bool.Parse(SecurityRadioButtonList.SelectedValue));
                        /*   string[] authArray = new string[4];
                           authArray[0] = oResponse.responseCode;
                           authArray[1] = oResponse.authNumber;
                           authArray[2] = oRespojnse.orderid*/
                           int orderID = oResponse.BPPS_Tx_Id;
                           /*authArray[3] = "0"; //oResponse.paymentId 
                           authArray[4] = oResponse.ErrorMessage;*/

                           Logger.LogInfo("CC Request for sale: " + salesInfo[0].ToString() + " bppsID:" + orderID);

                            // check if authorization was successful
                            if (oResponse.responseCode == "100")
                            {   
                          
                                // if we have partial payments to do, split the amount equally between sales
                                double amountPerSale = 0;

                                // create the Payments and Comments, and Update the sales from salesInfo(the list of selected sales) then commit to the DB.
                                foreach (string[] sale in salesInfo)
                                {
                                    try
                                    {
                                        double processedAmountForSale = 0;
                                        Sale currentSale = Sale.GetSaleByID(int.Parse(sale[0]));


                                        //
                                        double amt = Convert.ToDouble(sale[1]);// sale.
                                        amountPerSale = amountToProcess; //* (amt / selectedSaleTotal);

                                        efundraising.EFundraisingCRM.Payment currentPayment = new efundraising.EFundraisingCRM.Payment();
                                        currentPayment.SalesId = currentSale.SalesId;
                                        currentPayment.PaymentNo = currentPayment.GetNextPaymentNo();
                                        currentPayment.PaymentMethodId = byte.Parse(DropDownListCCType.SelectedValue);
                                        currentPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
                                        currentPayment.PaymentEntryDate = DateTime.Now;
                                        currentPayment.CashableDate = DateTime.Now;
                                        currentPayment.NameOnCard = TextBoxName.Text;
                                        //get last 4 numbers
                                        string last4 = TextBoxCCNumber.Text.Substring(TextBoxCCNumber.Text.Length - 4, 4);
                                        currentPayment.CreditCardNo = "****" + last4;

                                        currentPayment.ExpiryDate = DropDownListMonth.SelectedValue + "/" + DropDownListYear.SelectedValue;
                                        currentPayment.AuthorizationNumber = oResponse.authNumber;
                                        currentPayment.ForeignOrderId = orderID;
                                        // check if the amount to be processed covers the whole balance
                                        // or is just partial
                                        currentPayment.PaymentAmount = amountPerSale;
                                        processedAmountForSale = amountPerSale;

                                        //dont confirm
                                        if (amt > amountPerSale + 0.01)
                                        {
                                            //currentPayment.PaymentAmount = amountPerSale;
                                            //processedAmountForSale = amountPerSale;
                                        }
                                        else
                                        {
                                            // currentPayment.PaymentAmount = amountToProcess; // full payment
                                            // processedAmountForSale = amountToProcess;
                                            // update the sale infos to close it
                                            if (currentSale.SalesStatusId != SalesStatus.Confirmed.SalesStatusID)
                                            {
                                                //UPDATE: DO NOT AUTO CONFIRM 
                                                currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID;
                                               //currentSale.ConfirmedDate = DateTime.Now;
                                            }
                                            if (currentSale.UpfrontPaymentRequired < 0)
                                                currentSale.UpfrontPaymentRequired = 0;
                                            currentSale.Fuelsurcharge = 0;
                                        }
                                        currentPayment.CommissionPaid = false;

                                        // create a comment for the sale
                                        Comments currentComment = new Comments(int.MinValue,
                                            3,  // high priority
                                            currentSale.SalesId,
                                            currentSale.ConsultantId,
                                            currentSale.LeadId,
                                            9, // other 
                                            DateTime.Now,
                                            "CC processed by Paymentech OrderID: " + orderID + " for $" + amountToProcess + ". $" + processedAmountForSale + " of this amount was applied to this sale.");


                                        payments.Add(currentPayment);
                                        sales.Add(currentSale);
                                        comments.Add(currentComment);

                                    }
                                    catch (Exception ex)
                                    {
                                        PublishError("Error on Sales and Payments creation.", "", ex);
                                        ProcessButton.Enabled = true;
                                        Session["ClickOnce"] = 0;
                                    }
                                }//for each

                            
                            
                                    
                                if (payments.Count > 0)
                                {
                                    try
                                    {
                                        Session[_POSITIVE_PAYMENTS_] = payments;

                                        // commit to DB
                                        int createUserID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID"));
                                        TransactionController trans = new TransactionController();
                                        string message = "";
                                        bool success = trans.InsertPaymentsAndCommentsAndUpdateSales(payments, sales, comments, createUserID, ref message);
                                        if (success)
                                        {

                                            // display success message to the user
                                            LabelStatus.CssClass = "BigTextBold Success";
                                            LabelStatus.Text = "Transaction Successful!<br>"
                                                + "Authorization Number: " + oResponse.authNumber + "<br>"
                                                + "PaymenTech OrderID: " + orderID + "<br>"
                                                + "Affected Sale(s):<br>";
                                            foreach (string[] sale in salesInfo)
                                            {
                                                LabelStatus.Text += sale[0] + "<br>";
                                            }
                                            Session[_SETTLE_ARRAY_] = null; //removed it
                                            Session[_AFFECTED_SALES_] = salesInfo;
                                            VoidTransactionLinkButton.Visible = true;
                                            LabelStatus.Visible = true;
                                        }
                                        else
                                        {
                                            ManageSaleScreen.SendMail(message, 0, Request.Url.Authority, "SaleSupport");
                                            LabelStatus.CssClass = "BigTextBold Alert";
                                            LabelStatus.Text = message;
                                            LabelStatus.Visible = true;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        
                                        ManageSaleScreen.SendMail("Error on payment transaction." + " ePAy ID:" + orderID, 0, Request.Url.Authority, "SaleSupport");
                                        PublishError("Error on Transaction", "", ex);
                                        //check for timeout on db, if s o display it
                                        int p = ex.StackTrace.IndexOf("timeout expired");
                                        if (p > -1)
                                        {
                                            LabelStatus.Text = "The credit card was billed but a database timeout occured after the processing, please contact support.<br><br>";
                                        }
                                        else
                                        {
                                            LabelStatus.Text = "The credit card was billed but an error occured after the processing, please contact support.<br><br>";
                                        }
                                        LabelStatus.CssClass = "BigTextBold Alert";
                                        LabelStatus.Visible = true;

                                        if (oResponse.authNumber != null)
                                        {
                                            PublishError("Error on CC Transaction", "Auth number=" + oResponse.authNumber.ToString(),ex);
                                        }
                                        
                                        

                                    }
                                    FillSalesDataGrid();
                                }
                          
                      
                    }
                    else  //if code <> 100
                    {
                    
                        // display message on authorization error
                        string message = "";
                        switch (oResponse.responseCode)
                        {
                            case "201": message = "Bad Credit Card Number";
                                break;
                            case "233": message = "Does Not Match MOP";
                                break;
                            
                            case "303": message = "Declined Transaction";
                                break;
                            case "501": message = "Card should be returned";
                                break;
                            case "502": message = "Card reported lost or stolen";
                                break;
                            case "509": message = "Over limit exceeded";
                                break;
                            default: message = "General Failure";
                                break;
                        }
                        LabelStatus.CssClass = "BigTextBold Alert";
                        LabelStatus.Text = message + "<br>"
                            + "Message: " + oResponse.ErrorMessage + " Error Code:" + oResponse.responseCode;
                        LabelStatus.Visible = true;
                        ProcessButton.Enabled = true;
                        Session["ClickOnce"] = 0;
                    }

                }
                else
                {   //if bad zip
                    LabelStatus.CssClass = "BigTextBold Alert";
                    LabelStatus.Text = "Bad zip or state code";
                    LabelStatus.Visible = true;
                    ProcessButton.Enabled = true;
                }
                        
                    }
                    else
                    {
                        // display message on settle error
                        LabelStatus.CssClass = "BigTextBold Alert";
                        LabelStatus.Text = "Total amount cannot exceed sale amounts!<br>";
                        LabelStatus.Visible = true;
                        ProcessButton.Enabled = true;
                        Session["ClickOnce"] = 0;
                    }

                }
                else
                {
                    Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You must select at least one sale.');</script>");
                    ProcessButton.Enabled = true;
                    Session["ClickOnce"] = 0;
                }
            }
            else
            {
                Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You must enter an amount greater than 0.');</script>");
                ProcessButton.Enabled = true;
                Session["ClickOnce"] = 0;
            }
}



	}
}
