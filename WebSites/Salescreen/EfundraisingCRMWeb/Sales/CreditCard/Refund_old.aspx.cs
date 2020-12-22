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
using efundraising.EFundraisingCRM;

//using efundraising.EFundraisingCRMWeb.localhost;
using efundraising.EFundraisingCRM;
using EfundraisingCRM.WebReference;
//using EfundraisingCRM.com.efundraising.webservices;

namespace efundraising.EFundraisingCRMWeb.Sales.CreditCard
{
	/// <summary>
	/// Summary description for Refund.
	/// </summary>
	public partial class Refund : System.Web.UI.Page
	{

		private Sale[] sales = null;
		private const string _CLIENT_ = "ClientObject";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
			/*	ClientSequence[] clientSequenceCodes = ClientSequence.GetClientSequences();
				for (int i=0; i<clientSequenceCodes.Length; i++)
				{
					if (clientSequenceCodes[i].IsActive == 1)
					{
						DropDownListClientSequenceCode.Items.Add(clientSequenceCodes[i].ClientSequenceCode);
					}
				}*/
			}
			
		}
		
		private void FillSalesDataGrid(int clientId, string clientSequenceCode)
		{
					
			// get the Client Object filled from the database
			Client client = Client.GetClientByID(clientId, clientSequenceCode);
			
			if (client != null)
			{
				Session[_CLIENT_] = client;
				// get the client's billing address and use it to fill the fields
				ClientAddress billingAddress = ClientAddress.GetClientAddressByIdSequenceAddressType(client.ClientId, client.ClientSequenceCode, ClientAddressType.BillingAddress.AddressType);
				
				if (billingAddress != null)
				{
					if (billingAddress.CountryCode == Country.UnitedStates.CountryCode)
					{
						sales = Sale.GetSalesByClient(client);
					}
					else
					{
						Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('Invalid country, the billing address of the client must be in the US.');</script>");
					}
				}
				
			}
			
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
						
					DataRow dr = dt.NewRow();
					dr["sale_id"] =	 sales[i].SalesId;
					// dr["product_class"] = sales[i].GetProductClass().Description;
					dr["sale_status"] = SalesStatus.GetSalesStatusByID(sales[i].SalesStatusId).Description;
					double amountToDisplay = sales[i].TotalAmount - sales[i].CalculateTotalPaymentsAndAdjustmentsAmount();
					dr["amount"] = amountToDisplay.ToString();
					dr["amount_display"] = "$" + amountToDisplay.ToString("N", new CultureInfo("en-US", false).NumberFormat);
						
					dt.Rows.Add(dr);
					
				}
					
				SalesDataGrid.DataSource = dt;
				SalesDataGrid.DataBind();
			}
		}

		// checks all sales and updates the Amount
		protected void LinkButtonAll_Click(object sender, System.EventArgs e)
		{
			bool check = true;
			
			for (int i=0; i < SalesDataGrid.Items.Count; i++) 
			{
				CheckBox chk = (CheckBox) SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
				chk.Checked = check;
				
			}
		}

		// unchecks all sales and updates the Amount to 0
		protected void LinkButtonNone_Click(object sender, System.EventArgs e)
		{
			bool check = false;
			
			for (int i=0; i < SalesDataGrid.Items.Count; i++) 
			{
				CheckBox chk = (CheckBox) SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
				chk.Checked = check;
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void ButtonNext_Click(object sender, System.EventArgs e)
		{
			string saleIds = "";
			Client client = (Client)Session[_CLIENT_];

			// check which sales are checked for processing
			for (int i=0; i < SalesDataGrid.Items.Count; i++) 
			{
				CheckBox chk = (CheckBox) SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
				// if sale is selected, add its saleId 
				if (chk.Checked)
					saleIds += SalesDataGrid.Items[i].Cells[1].Text + "|";
			}
			
			if (saleIds.Length > 0)
			{
				// remove the last | from the string
				saleIds = saleIds.Remove(saleIds.Length - 1, 1);

				// redirect to the Refund page
				Response.Redirect("Credit.aspx?clientId=" + client.ClientId + "&clientSequenceCode=" + client.ClientSequenceCode + "&saleIds=" + saleIds, false);
			}
			else
			{
				Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('You must select at least one sale.');</script>");
			}

			
		}

		


		private void RefundPaymentsByForeignOrderId(int orderId)
		{
			try
			{
				// the collection of payments previously created for the orderId
				PaymentCollection payments = Payment.GetPaymentsByForeignOrderId(orderId);

				// new collections to be inserted after Refund
				PaymentCollection newPayments = new PaymentCollection();
				SaleCollection sales = new SaleCollection();
				CommentsCollection comments = new CommentsCollection();

				// list of the sales
				ArrayList saleIds = new ArrayList();

				if (payments.Count > 0)
				{
					PaymenTech paymenTech = new PaymenTech();
					TransactionController trans = new TransactionController();
					
					// try to refund the order at paymentech
					string response = paymenTech.Refund(orderId);
					string[] refundArray = response.Split("|".ToCharArray());

					// check if refund was successful
					if (refundArray[0] == "0")
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
								newPayment.AuthorizationNumber = refundArray[1];
								newPayment.PaymentEntryDate = DateTime.Now;
								newPayment.CashableDate = DateTime.Now;
								newPayment.CreditCardNo = p.CreditCardNo;
								newPayment.ExpiryDate = p.ExpiryDate;
								newPayment.NameOnCard = p.NameOnCard;
								newPayment.PaymentMethodId = p.PaymentMethodId;
								newPayment.PaymentAmount = p.PaymentAmount * -1;
								newPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
								newPayment.ForeignOrderId = orderId;

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
								// currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID; 
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
									"CC transaction voided through Paymentech OrderID: " + orderId);

								comments.Add(currentComment);
							}

						}
                        
						// insert the sales and comments
						trans.UpdateSalesAndInsertComments(sales, comments);
						
						Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('Refund Successful');</script>");
					}
					else
					{
						Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('Order not cancellable');</script>");
					}
				}
				else
				{
					Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('No payments found for OrderId : " + orderId + "');</script>");					
				}
			}
			catch (Exception ex)
			{
				throw ex;	
			}
		}

	

        protected void ButtonGetAmount_Click(object sender, EventArgs e)
        {

        }

        protected void AmountTextBox_DataBinding(object sender, EventArgs e)
        {

        }

        protected void ButtonSendRefund_Click(object sender, EventArgs e)
        {
             // try to credit the amount at ePAY
                                
                //name
                    int pos = TextBoxName.Text.IndexOf(" ", 0);
                    string firstName = TextBoxName.Text.Substring(0, pos);
                    string lastname = TextBoxName.Text.Substring(pos + 1, TextBoxName.Text.Length - pos - 1);
                //amount
                    string amnt = TextBoxAmount.Text.ToString();
                    string cents = "";
                    pos = amnt.IndexOf(".");
                    if (pos > 0)
                    {
                        cents = amnt.Substring(pos + 1, 2);
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

  
               BatchPaymentSystemWebservice oClient = new BatchPaymentSystemWebservice();

               string transactionID = "1394";
                BPPSTxResponse oResponse =
                    oClient.Refund(    1394, //sale screen code
                                       "95215703-E594-49E3-AF33-8FF722F00117",  //prod ...7 dev ....6
                                       firstName,
                                       lastname,
                                       TextBoxStreetAddress.Text,
                                       "",
                                       TextBoxCity.Text,
                                       TextBoxState.Text,
                                       TextBoxZipCode.Text,
                                       CountryCode.US,
                                       ccType,
                                       TextBoxCCNumber.Text,
                                       int.Parse(DropDownListMonth.SelectedValue),
                                       int.Parse(DropDownListYear.SelectedValue),
                                       amount, "eFundraising.com", "eFundraising.com", transactionID);

                                                    
                
                
                // check if credit was successful
                if (oResponse.BPPS_Tx_Id > 0 && oResponse.ErrorMessage == "")
                {   
                    CreditCardRefundRequest cc = new CreditCardRefundRequest();
                    cc.BppsID = oResponse.BPPS_Tx_Id;
                    cc.RefundAmount = amount;
                    cc.SaleID = Convert.ToInt32(SaleIdTextBox.Text);
                    cc.StatusCode = "";
                    cc.Insert();

                    RefreshGrid();

                }
        }

        protected void TextBoxCity0_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
           //CreditCardRefundRequest[] cc = CreditCardRefundRequest.GetCreditCardRefundRequestLastDays(-7);
        

            BatchPaymentSystemWebservice oClient =
                   new BatchPaymentSystemWebservice();

            //iterate sales to update status
            //foreach (CreditCardRefundRequest ccr in cc)
           // {
                BPPSTxResponse oResponse =
              oClient.FetchResponse(1394, "95215703-E594-49E3-AF33-8FF722F00117", 219551); //7 prod


            /*    CreditCardRefundRequest request = CreditCardRefundRequest.GetCreditCardRefundRequestByID(ccr.BppsID);
                if (request != null)
                {
                    request.StatusCode = oResponse.responseCode;
                    request.Update();
                }*/
            //} 
            
         //   SalesDataGrid.DataSource = cc;
          //  SalesDataGrid.DataBind();
        }

	}
}
