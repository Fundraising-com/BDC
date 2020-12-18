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
using efundraising.eFundData;
using EFundraisingCRMWeb.Components.Server;


//using efundraising.EFundraisingCRMWeb.localhost;
using efundraising.EFundraisingCRM;
//using EfundraisingCRM.com.efundraising.webservices;

namespace efundraising.EFundraisingCRMWeb.Sales.CreditCard
{
	/// <summary>
	/// Summary description for Refund.
	/// </summary>
	public partial class Credit : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBoxOrderID;
		protected System.Web.UI.WebControls.Button ButtonVoid;

		private const string _CLIENT_ = "ClientObject";


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				FillControls();
			}
			
		}
		
		// Fill the datagrid with the Clients pending sales
		private void FillControls()
		{
			// get infos from QueryString
			int clientId = int.Parse(Request.QueryString["clientId"].ToString());
			string clientSequenceCode = Request.QueryString["clientSequenceCode"].ToString();
			// split the saleIds string at the "|" character into a string array
			string[] saleIds = Request.QueryString["saleIds"].ToString().Split("|".ToCharArray());
	

			// get the Client Object filled from the database
			Client client = Client.GetClientByID(clientId, clientSequenceCode);
			

			DataTable dt = new DataTable();
			dt.Columns.Add("sale_id");
			dt.Columns.Add("payment_no");
			dt.Columns.Add("payment_method");
			dt.Columns.Add("payment_entry_date");
			dt.Columns.Add("amount");
			dt.Columns.Add("amount_display");
			dt.Columns.Add("foreign_orderid");

			foreach(String s in saleIds)
			{
				PaymentCollection payments = Payment.GetPaymentsBySaleId(int.Parse(s));
				Sale currentSale = Sale.GetSaleByID(int.Parse(s));
					
					
				foreach(Payment p in payments)
				{
						
					if (p.PaymentMethodId == PaymentMethod.VISA.PaymentMethodId 
						|| p.PaymentMethodId == PaymentMethod.MASTERCARD.PaymentMethodId 
						|| p.PaymentMethodId == PaymentMethod.Discover.PaymentMethodId 
						|| p.PaymentMethodId == PaymentMethod.AMEX.PaymentMethodId)
					{
						DataRow dr = dt.NewRow();
						dr["sale_id"] = p.SalesId;
						dr["payment_no"] = p.PaymentNo;
						dr["payment_method"] = PaymentMethod.GetCreditCardName(p.PaymentMethodId);
						dr["payment_entry_date"] = p.PaymentEntryDate;
						dr["amount"] = p.PaymentAmount;
						dr["amount_display"] = "$" + p.PaymentAmount.ToString("N", new CultureInfo("en-US", false).NumberFormat);
						if (p.ForeignOrderId != int.MinValue)
							dr["foreign_orderid"] = p.ForeignOrderId;
						else
							dr["foreign_orderid"] = "NONE";
	
						dt.Rows.Add(dr);
					}
					
				}

			}

			PaymentsDataGrid.DataSource = dt;
			PaymentsDataGrid.DataBind();
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		
		// updates the total when a include checkbox is changed
		protected void CheckBoxInclude_CheckeckChanged(object sender, System.EventArgs e)
		{
			double amountToProcess = 0;
			
			for (int i=0; i < PaymentsDataGrid.Items.Count; i++) 
			{
				CheckBox chk = (CheckBox) PaymentsDataGrid.Items[i].FindControl("CheckBoxInclude");
				double currentSaleAmount = double.Parse(PaymentsDataGrid.Items[i].Cells[5].Text);
				if (chk.Checked)
					amountToProcess += currentSaleAmount;
			}
			
			TextBoxAmount.Text = amountToProcess.ToString("N", new CultureInfo("en-US", false).NumberFormat);
		}

		// checks all payments and updates the Amount
		protected void LinkButtonAll_Click(object sender, System.EventArgs e)
		{
			bool check = true;
			double amountToProcess = 0;

			for (int i=0; i < PaymentsDataGrid.Items.Count; i++) 
			{
				CheckBox chk = (CheckBox) PaymentsDataGrid.Items[i].FindControl("CheckBoxInclude");
				chk.Checked = check;
				double currentSaleAmount = double.Parse(PaymentsDataGrid.Items[i].Cells[5].Text);
				amountToProcess += currentSaleAmount;
			}

			TextBoxAmount.Text = amountToProcess.ToString();
		}

		// unchecks all sales and updates the Amount to 0
		protected void LinkButtonNone_Click(object sender, System.EventArgs e)
		{
			bool check = false;
			
			for (int i=0; i < PaymentsDataGrid.Items.Count; i++) 
			{
				CheckBox chk = (CheckBox) PaymentsDataGrid.Items[i].FindControl("CheckBoxInclude");
				chk.Checked = check;
			}

			TextBoxAmount.Text = "0";
		}

		protected void RadioButtonDataGrid_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButtonDataGrid.Checked)
			{
				PanelDataGrid.Enabled = true;
				TextBoxAmount.Enabled = false;
				PanelAmountInput.Enabled = false;
				RadioButtonAmountInput.Checked = false;
			}
		}

		protected void RadioButtonAmountInput_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButtonAmountInput.Checked)
			{
				PanelDataGrid.Enabled = false;
				PanelAmountInput.Enabled = true;
				TextBoxAmount.Enabled = true;
				RadioButtonDataGrid.Checked = false;
			}
		}

		protected void ButtonCredit_Click(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				// split the saleIds string at the "|" character into a string array
				string[] saleIds = Request.QueryString["saleIds"].ToString().Split("|".ToCharArray());
				SaleCollection sales = new SaleCollection();
				bool allowCredit = true;
				
				// Credit on specific payments
				if (PanelDataGrid.Enabled)
				{
					PaymentCollection payments = new PaymentCollection();
			
					for (int i=0; i < PaymentsDataGrid.Items.Count; i++) 
					{
						CheckBox chk = (CheckBox) PaymentsDataGrid.Items[i].FindControl("CheckBoxInclude");
						if (chk.Checked)
						{
							int currentPaymentSaleId = int.Parse(PaymentsDataGrid.Items[i].Cells[1].Text);
							int currentPaymentNumber = int.Parse(PaymentsDataGrid.Items[i].Cells[2].Text);
							Sale sale = Sale.GetSaleByID(currentPaymentSaleId);
							double amountDue = sale.TotalAmount - sale.CalculateTotalPaymentsAndAdjustmentsAmount();
							Payment currentPayment = Payment.GetPaymentBySaleIdAndPaymentNo(currentPaymentSaleId, currentPaymentNumber);
							if ((amountDue + currentPayment.PaymentAmount) <= sale.TotalAmount)
							{
								payments.Add(currentPayment);
							}
							else
							{
								Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You are trying to credit more than the original amount of the sale!');</script>");
								allowCredit = false;
								break;
							}
						}
					}

					if (allowCredit)
						if (payments.Count > 0)
							CreditPayments(payments);

				}
				// Credit a specific amount on the sales
				else if(PanelAmountInput.Enabled)
				{
					
					double amountPerSale = (double.Parse(TextBoxAmount.Text) / saleIds.Length) * -1;
		
					foreach (String s in saleIds)
					{
						Sale sale = Sale.GetSaleByID(int.Parse(s));
						double amountDue = sale.TotalAmount - sale.CalculateTotalPaymentsAndAdjustmentsAmount();
						
						// make sure to not credit more than the original total amount of the sale
						if ((amountDue - amountPerSale) <= sale.TotalAmount)
						{
							sales.Add(sale);
						}
						else
						{
							Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You are trying to credit more than the original amount of the sale!');</script>");
							allowCredit = false;
							break;
						}
					}
					
					if (allowCredit)
						if (sales.Count > 0)
							CreditSales(sales);
				}
			}
		}


        protected void ButtonCreditNew_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                // split the saleIds string at the "|" character into a string array
                string[] saleIds = Request.QueryString["saleIds"].ToString().Split("|".ToCharArray());
                SaleCollection sales = new SaleCollection();
                bool allowCredit = true;

                // Credit on specific payments
                if (PanelDataGrid.Enabled)
                {
                    PaymentCollection payments = new PaymentCollection();

                    for (int i = 0; i < PaymentsDataGrid.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)PaymentsDataGrid.Items[i].FindControl("CheckBoxInclude");
                        if (chk.Checked)
                        {
                            int currentPaymentSaleId = int.Parse(PaymentsDataGrid.Items[i].Cells[1].Text);
                            int currentPaymentNumber = int.Parse(PaymentsDataGrid.Items[i].Cells[2].Text);
                            Sale sale = Sale.GetSaleByID(currentPaymentSaleId);
                            double amountDue = sale.TotalAmount - sale.CalculateTotalPaymentsAndAdjustmentsAmount();
                            Payment currentPayment = Payment.GetPaymentBySaleIdAndPaymentNo(currentPaymentSaleId, currentPaymentNumber);
                            if ((amountDue + currentPayment.PaymentAmount) <= sale.TotalAmount)
                            {
                                payments.Add(currentPayment);
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You are trying to credit more than the original amount of the sale!');</script>");
                                allowCredit = false;
                                break;
                            }
                        }
                    }

                    if (allowCredit)
                        if (payments.Count > 0)
                            CreditPaymentsNew(payments);

                }
                // Credit a specific amount on the sales
                else if (PanelAmountInput.Enabled)
                {

                    double amountPerSale = (double.Parse(TextBoxAmount.Text) / saleIds.Length) * -1;

                    foreach (String s in saleIds)
                    {
                        Sale sale = Sale.GetSaleByID(int.Parse(s));
                        double amountDue = sale.TotalAmount - sale.CalculateTotalPaymentsAndAdjustmentsAmount();

                        // make sure to not credit more than the original total amount of the sale
                        if ((amountDue - amountPerSale) <= sale.TotalAmount)
                        {
                            sales.Add(sale);
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You are trying to credit more than the original amount of the sale!');</script>");
                            allowCredit = false;
                            break;
                        }
                    }

                    if (allowCredit)
                        if (sales.Count > 0)
                            CreditSalesNew(sales);
                }
            }
        }




		private void CreditPayments(PaymentCollection payments) 
		{/*
			try
			{

				// new collections to be inserted after Credit
				PaymentCollection newPayments = new PaymentCollection();
				SaleCollection sales = new SaleCollection();
				CommentsCollection comments = new CommentsCollection();

				// list of the sales
				ArrayList saleIds = new ArrayList();

				PaymenTech paymenTech = new PaymenTech();
				TransactionController trans = new TransactionController();
				
				// try to credit the amount at paymentech
				string response = paymenTech.Credit(double.Parse(TextBoxAmount.Text), 
													TextBoxCCNumber.Text, 
													int.Parse(DropDownListMonth.SelectedValue), 
													int.Parse(DropDownListYear.SelectedValue),
													TextBoxCVV2.Text,
													TextBoxName.Text,
													TextBoxStreetAddress.Text,
													"",
													TextBoxCity.Text,
													TextBoxState.Text,
													TextBoxZipCode.Text,
													"",
													bool.Parse(SecurityRadioButtonList.SelectedValue));
														
				string[] creditArray = response.Split("|".ToCharArray());

				// check if credit was successful
				if (creditArray[0] == "0" && creditArray[3] != "-1")
				{
					// a credit needs to be settled if successfully authorized
					response = paymenTech.Settle(int.Parse(creditArray[3]));
					string[] settleArray = response.Split("|".ToCharArray());
						
					// check if settle was successful
					if (settleArray[0] == "0")
					{
					
						// create a new negative payment for each payments of the order
						foreach (Payment p in payments)
						{
							if (p.PaymentAmount > 0)
							{
								if (saleIds.IndexOf(p.SalesId) < 0)
									saleIds.Add(p.SalesId);

								Payment newPayment = new Payment();
								newPayment.SalesId = p.SalesId;
								newPayment.PaymentNo = newPayment.GetNextPaymentNo();
								newPayment.AuthorizationNumber = creditArray[1];
								newPayment.PaymentEntryDate = DateTime.Now;
								newPayment.CashableDate = DateTime.Now;
                                newPayment.CreditCardNo = TextBoxCCNumber.Text + "Credit"; 
								newPayment.ExpiryDate = DropDownListMonth.SelectedValue + "/" + DropDownListYear.SelectedValue;
								newPayment.NameOnCard = TextBoxName.Text;
								newPayment.PaymentMethodId = byte.Parse(DropDownListCCType.SelectedValue);
								newPayment.PaymentAmount = double.Parse(TextBoxAmount.Text) * -1;
								newPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
								newPayment.ForeignOrderId = int.Parse(creditArray[2]);

								newPayments.Add(newPayment);
							}
							
						}
						
						// insert the new payments in the database
						trans.InsertPaymentsForCompletedTransactions(newPayments);

						// Update the sales and create a comment for each
						foreach (int i in saleIds)
						{
							
							Sale currentSale = Sale.GetSaleByID(i);
							if ((currentSale.TotalAmount - currentSale.CalculateTotalPaymentsAndAdjustmentsAmount()) > 0)
							{
								//currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID;
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
									"CC credited through Paymentech OrderID: " + settleArray[2]);

								comments.Add(currentComment);
							}

						}
	                    
						// insert the sales and comments
						trans.UpdateSalesAndInsertComments(sales, comments);
						
						// display success message to the user
						LabelStatus.CssClass = "BigTextBold Success";
						LabelStatus.Text = "The amount was Successfully credited<br>"
							+ "Authorization Number: " + settleArray[1] + "<br>"
							+ "PaymenTech OrderID: " + settleArray[2] + "<br>";
						LabelStatus.Visible = true;
					}
				}
				else
				{
					LabelStatus.CssClass = "BigTextBold Alert";
					LabelStatus.Text = "Could not credit the amount, please verify the credit card informations.<br><br>";
					LabelStatus.Visible = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}*/
		}




        private void CreditPaymentsNew(PaymentCollection payments)
        {/*
            try
            {

                // new collections to be inserted after Credit
                PaymentCollection newPayments = new PaymentCollection();
                SaleCollection sales = new SaleCollection();
                CommentsCollection comments = new CommentsCollection();

                // list of the sales
                ArrayList saleIds = new ArrayList();

                PaymenTech paymenTech = new PaymenTech();
                TransactionController trans = new TransactionController();

                // try to credit the amount at paymentech
                
                
                //name
                    int pos = TextBoxName.Text.IndexOf(" ", 0);
                    string firstName = TextBoxName.Text.Substring(0, pos);
                    string lastname = TextBoxName.Text.Substring(pos + 1, TextBoxName.Text.Length - pos - 1);
                //amount
                    string amnt = TextBoxAmount.ToString();
                    string cents = "";
                    pos = amnt.IndexOf(".");
                    if (pos > 0)
                    {
                        cents = amnt.Substring(pos + 1, 2);
                    }
                    else
                    {
                        amnt = amnt + "00";

                    }
                              int amount = Convert.ToInt32(amnt);
                //credit card
                    EfundraisingCRM.com.qsp.dev_wsi.CardType ccType;
                    switch (DropDownListCCType.SelectedItem.Text.ToUpper())
                    {
                        case "VISA":
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.VISA;
                            break;
                        case "MASTERCARD":
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.MASTERCARD;
                            break;
                        case "AMEX":
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.AMEX;
                            break;
                        case "DISCOVER":
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.DISCOVER;
                            break;
                        default:
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.DISCOVER;
                            break;

                    }


                EfundraisingCRM.com.qsp.dev_wsi.BatchPaymentSystemWebservice oClient =
                    new EfundraisingCRM.com.qsp.dev_wsi.BatchPaymentSystemWebservice();

                EfundraisingCRM.com.qsp.dev_wsi.BPPSTxResponse oResponse =
                    oClient.Refund(    1394, //sale screen code
                                       "95215703-E594-49E3-AF33-8FF722F00116",
                                       firstName,
                                       lastname,
                                       TextBoxStreetAddress.Text,
                                       "",
                                       TextBoxCity.Text,
                                       TextBoxState.Text,
                                       TextBoxZipCode.Text,
                                       EfundraisingCRM.com.qsp.dev_wsi.CountryCode.US,
                                       ccType,
                                       TextBoxCCNumber.Text,
                                       int.Parse(DropDownListMonth.SelectedValue),
                                       int.Parse(DropDownListYear.SelectedValue),
                                       amount, "eFundraising.com", "eFundraising.com", "1394");

                                                    
                
                
                // check if credit was successful
                if (oResponse.BPPS_Tx_Id > 0 && oResponse.ErrorMessage == "")
                {                
                   
                   int orderID = oResponse.BPPS_Tx_Id; // oRespojnse.orderid

                        // create a new negative payment for each payments of the order
                        foreach (Payment p in payments)
                        {
                            if (p.PaymentAmount > 0)
                            {
                                if (saleIds.IndexOf(p.SalesId) < 0)
                                    saleIds.Add(p.SalesId);

                                Payment newPayment = new Payment();
                                newPayment.SalesId = p.SalesId;
                                newPayment.PaymentNo = newPayment.GetNextPaymentNo();
                                newPayment.AuthorizationNumber = oResponse.authNumber;
                                newPayment.PaymentEntryDate = DateTime.Now;
                                newPayment.CashableDate = DateTime.Now;
                                newPayment.CreditCardNo = TextBoxCCNumber.Text + "Credit";
                                newPayment.ExpiryDate = DropDownListMonth.SelectedValue + "/" + DropDownListYear.SelectedValue;
                                newPayment.NameOnCard = TextBoxName.Text;
                                newPayment.PaymentMethodId = byte.Parse(DropDownListCCType.SelectedValue);
                                newPayment.PaymentAmount = double.Parse(TextBoxAmount.Text) * -1;
                                newPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
                                newPayment.ForeignOrderId = orderID;

                                newPayments.Add(newPayment);
                            }

                        }

                        // insert the new payments in the database
                        trans.InsertPaymentsForCompletedTransactions(newPayments);

                        // Update the sales and create a comment for each
                        foreach (int i in saleIds)
                        {

                            Sale currentSale = Sale.GetSaleByID(i);
                            if ((currentSale.TotalAmount - currentSale.CalculateTotalPaymentsAndAdjustmentsAmount()) > 0)
                            {
                                //currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID;
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
                                    "CC credited through Paymentech OrderID: " + orderID);

                                comments.Add(currentComment);
                            }

                        }

                        // insert the sales and comments
                        trans.UpdateSalesAndInsertComments(sales, comments);

                        // display success message to the user
                        LabelStatus.CssClass = "BigTextBold Success";
                        LabelStatus.Text = "The amount was Successfully credited<br>"
                            + "Authorization Number: " + oResponse.authNumber + "<br>"
                            + "PaymenTech OrderID: " + orderID + "<br>";
                        LabelStatus.Visible = true;
                    
                }
                else
                {
                    LabelStatus.CssClass = "BigTextBold Alert";
                    LabelStatus.Text = "Could not credit the amount, please verify the credit card informations.<br><br>";
                    LabelStatus.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }*/
        }




		private void CreditSales(SaleCollection sales) 
		{/*
			try
			{

				// new collections to be inserted after Credit
				PaymentCollection newPayments = new PaymentCollection();
				SaleCollection salesToUpdate = new SaleCollection();
				CommentsCollection comments = new CommentsCollection();

				PaymenTech paymenTech = new PaymenTech();
				TransactionController trans = new TransactionController();
				
				// try to credit the amount at paymentech
				string response = paymenTech.Credit(double.Parse(TextBoxAmount.Text), 
					TextBoxCCNumber.Text, 
					int.Parse(DropDownListMonth.SelectedValue), 
					int.Parse(DropDownListYear.SelectedValue),
					TextBoxCVV2.Text,
					TextBoxName.Text,
					TextBoxStreetAddress.Text,
					"",
					TextBoxCity.Text,
					TextBoxState.Text,
					TextBoxZipCode.Text,
					"",
					bool.Parse(SecurityRadioButtonList.SelectedValue));
														
				string[] creditArray = response.Split("|".ToCharArray());

				// check if credit was successful
				if (creditArray[0] == "0" && creditArray[3] != "-1")
				{
					// a credit needs to be settled if successfully authorized
					response = paymenTech.Settle(int.Parse(creditArray[3]));
					string[] settleArray = response.Split("|".ToCharArray());
						
					// check if settle was successful
					if (settleArray[0] == "0")
					{
						
						// the amount per sale if just the total amount inputed by the user
						// divided by the number of sales that were previously selected
						double amountPerSale = (double.Parse(TextBoxAmount.Text) / sales.Count) * -1;
						
						// create a new negative payment for each sale of amountPerSale
						for(int i=0; i < sales.Count; i++)
						{							
							Sale currentSale = (Sale)sales[i];
							double currentSaleBalance = (currentSale.TotalAmount - currentSale.CalculateTotalPaymentsAndAdjustmentsAmount()) - amountPerSale;
							
							Payment newPayment = new Payment();
							newPayment.SalesId = currentSale.SalesId;
							newPayment.PaymentNo = newPayment.GetNextPaymentNo();
							newPayment.AuthorizationNumber = creditArray[1];
							newPayment.PaymentEntryDate = DateTime.Now;
							newPayment.CashableDate = DateTime.Now;
							newPayment.CreditCardNo = TextBoxCCNumber.Text + "Credit";
							newPayment.ExpiryDate = DropDownListMonth.SelectedValue + "/" + DropDownListYear.SelectedValue;
							newPayment.NameOnCard = TextBoxName.Text;
							newPayment.PaymentMethodId = byte.Parse(DropDownListCCType.SelectedValue);
							newPayment.PaymentAmount = amountPerSale;
							newPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
							newPayment.ForeignOrderId = int.Parse(creditArray[2]);

							newPayments.Add(newPayment);
							
							// make sure the sale is still opened (Ar_status NOT PAID and status ON HOLD)
							// if the sale is not paid in full (most likely to be the case after a credit)
							if (currentSaleBalance > 0)
							{
								currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID;
								currentSale.ConfirmedDate = DateTime.MinValue;
								currentSale.Fuelsurcharge = 0;
								currentSale.ArStatusId = ARStatus.NotPaid.ARStatusID;

								salesToUpdate.Add(currentSale);

								// create a comment for the sale
								Comments currentComment = new Comments(int.MinValue, 
									3,  // high priority
									currentSale.SalesId, 
									currentSale.ConsultantId, 
									currentSale.LeadId, 
									9, // other 
									DateTime.Now, 
									"CC credited through Paymentech OrderID: " + settleArray[2]);

								comments.Add(currentComment);
							}
						}
						
						// commit to the database
                        int createUserID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID"));
								
						trans.InsertPaymentsAndCommentsAndUpdateSales(newPayments, salesToUpdate, comments, createUserID);
						
						// display success message to the user
						LabelStatus.CssClass = "BigTextBold Success";
						LabelStatus.Text = "The amount was successfully credited<br>"
							+ "Authorization Number: " + settleArray[1] + "<br>"
							+ "PaymenTech OrderID: " + settleArray[2] + "<br>";
						LabelStatus.Visible = true;
					}
				}
				else
				{
					LabelStatus.CssClass = "BigTextBold Alert";
					LabelStatus.Text = "Could not credit the amount, please verify the credit card informations.<br><br>";
					LabelStatus.Visible = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}*/
		}

        private void CreditSalesNew(SaleCollection sales)
        {/*
            try
            {

                // new collections to be inserted after Credit
                PaymentCollection newPayments = new PaymentCollection();
                SaleCollection salesToUpdate = new SaleCollection();
                CommentsCollection comments = new CommentsCollection();

                PaymenTech paymenTech = new PaymenTech();
                TransactionController trans = new TransactionController();

                // try to credit the amount at paymentech
                 //name
                    int pos = TextBoxName.Text.IndexOf(" ", 0);
                    string firstName = TextBoxName.Text.Substring(0, pos);
                    string lastname = TextBoxName.Text.Substring(pos + 1, TextBoxName.Text.Length - pos - 1);
                //amount
                    string amnt = TextBoxAmount.Text;
                    string cents = "";
                    pos = amnt.IndexOf(".");
                    if (pos > 0)
                    {
                        cents = amnt.Substring(pos + 1, 2);
                    }
                    else
                    {
                        amnt = amnt + "00";

                    }
                              int amount = Convert.ToInt32(amnt);
                //credit card
                    EfundraisingCRM.com.qsp.dev_wsi.CardType ccType;
                    switch (DropDownListCCType.SelectedItem.Text.ToUpper())
                    {
                        case "VISA":
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.VISA;
                            break;
                        case "MASTERCARD":
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.MASTERCARD;
                            break;
                        case "AMEX":
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.AMEX;
                            break;
                        case "DISCOVER":
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.DISCOVER;
                            break;
                        default:
                            ccType = EfundraisingCRM.com.qsp.dev_wsi.CardType.DISCOVER;
                            break;

                    }


                EfundraisingCRM.com.qsp.dev_wsi.BatchPaymentSystemWebservice oClient =
                    new EfundraisingCRM.com.qsp.dev_wsi.BatchPaymentSystemWebservice();

                EfundraisingCRM.com.qsp.dev_wsi.BPPSTxResponse oResponse =
                    oClient.Refund(1394, //sale screen code
                                       "95215703-E594-49E3-AF33-8FF722F00116",
                                       firstName,
                                       lastname,
                                       TextBoxStreetAddress.Text,
                                       "",
                                       TextBoxCity.Text,
                                       TextBoxState.Text,
                                       TextBoxZipCode.Text,
                                       EfundraisingCRM.com.qsp.dev_wsi.CountryCode.US,
                                       ccType,
                                       TextBoxCCNumber.Text,
                                       int.Parse(DropDownListMonth.SelectedValue),
                                       int.Parse(DropDownListYear.SelectedValue),
                                       amount, "eFundraising.com","eFundraising.com", "1394");

                                                    
                
                
                // check if credit was successful
                if (oResponse.responseCode == "100")
                { 
               
                   
                   int orderID = oResponse.BPPS_Tx_Id; // oRespojnse.orderid
                    	// the amount per sale if just the total amount inputed by the user
						// divided by the number of sales that were previously selected
						double amountPerSale = (double.Parse(TextBoxAmount.Text) / sales.Count) * -1;
					
                        // create a new negative payment for each sale of amountPerSale
                        for (int i = 0; i < sales.Count; i++)
                        {
                            Sale currentSale = (Sale)sales[i];
                            double currentSaleBalance = (currentSale.TotalAmount - currentSale.CalculateTotalPaymentsAndAdjustmentsAmount()) - amountPerSale;

                            Payment newPayment = new Payment();
                            newPayment.SalesId = currentSale.SalesId;
                            newPayment.PaymentNo = newPayment.GetNextPaymentNo();
                            newPayment.AuthorizationNumber = oResponse.authNumber;
                            newPayment.PaymentEntryDate = DateTime.Now;
                            newPayment.CashableDate = DateTime.Now;
                            newPayment.CreditCardNo = TextBoxCCNumber.Text + "Credit";
                            newPayment.ExpiryDate = DropDownListMonth.SelectedValue + "/" + DropDownListYear.SelectedValue;
                            newPayment.NameOnCard = TextBoxName.Text;
                            newPayment.PaymentMethodId = byte.Parse(DropDownListCCType.SelectedValue);
                            newPayment.PaymentAmount = amountPerSale;
                            newPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
                            newPayment.ForeignOrderId = orderID;

                            newPayments.Add(newPayment);

                            // make sure the sale is still opened (Ar_status NOT PAID and status ON HOLD)
                            // if the sale is not paid in full (most likely to be the case after a credit)
                            if (currentSaleBalance > 0)
                            {
                                currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID;
                                currentSale.ConfirmedDate = DateTime.MinValue;
                                currentSale.Fuelsurcharge = 0;
                                currentSale.ArStatusId = ARStatus.NotPaid.ARStatusID;

                                salesToUpdate.Add(currentSale);

                                // create a comment for the sale
                                Comments currentComment = new Comments(int.MinValue,
                                    3,  // high priority
                                    currentSale.SalesId,
                                    currentSale.ConsultantId,
                                    currentSale.LeadId,
                                    9, // other 
                                    DateTime.Now,
                                    "CC credited through Paymentech OrderID: " + orderID);

                                comments.Add(currentComment);
                            }
                        }

                        // commit to the database
                        int createUserID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID"));

                        trans.InsertPaymentsAndCommentsAndUpdateSales(newPayments, salesToUpdate, comments, createUserID);

                        // display success message to the user
                        LabelStatus.CssClass = "BigTextBold Success";
                        LabelStatus.Text = "The amount was successfully credited<br>"
                            + "Authorization Number: " + oResponse.authNumber + "<br>"
                            + "PaymenTech OrderID: " + orderID + "<br>";
                        LabelStatus.Visible = true;
                    
                }
                else
                {
                    LabelStatus.CssClass = "BigTextBold Alert";
                    LabelStatus.Text = "Could not credit the amount. Error: " + oResponse.ErrorMessage  + "<br><br>";
                    LabelStatus.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }*/
        }

	}
}
