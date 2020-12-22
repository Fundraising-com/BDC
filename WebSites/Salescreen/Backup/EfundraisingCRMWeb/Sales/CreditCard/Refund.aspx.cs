using System;
using System.Text;
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
using EFundraisingCRMWeb.Components.Server;

//TEMP FOR DEV
//using EfundraisingCRM.com.qsp.dev_wsi;
using EfundraisingCRM.ePay.WebReference;

//using efundraising.EFundraisingCRMWeb.localhost;

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


       /*    BatchPaymentSystemWebservice oClient =
                         new BatchPaymentSystemWebservice();

            //iterate sales to update status
            //foreach (CreditCardRefundRequest ccr in cc)
            // {
            BPPSTxResponse oResponse =
          oClient.FetchResponse(1394, "95215703-E594-49E3-AF33-8FF722F00117", 270440); //7 prod
      
           */

			if (!IsPostBack)
			{

                DateTime dt = new DateTime(2009,10,20);

                
				/*it is important to use the technique rather that assigning the readonly property of the webcontrol so that the 
				value set on the client via javascript will be available on the server side afterwards*/
				//txtFromDate.Attributes.Add("readonly", string.Empty);
				txtFromDate.Style.Add("cursor", "hand");
                txtFromDate.Text = DateTime.Now.AddDays(-6).ToString();
				//txtTodate.Attributes.Add("readonly", string.Empty);
				txtTodate.Style.Add("cursor", "hand");
				txtTodate.Text = DateTime.Now.ToString();

				/*ClientSequence[] clientSequenceCodes = ClientSequence.GetClientSequences();
					for (int i=0; i<clientSequenceCodes.Length; i++)
					{
						if (clientSequenceCodes[i].IsActive == 1)
						{
							DropDownListClientSequenceCode.Items.Add(clientSequenceCodes[i].ClientSequenceCode);
						}
					}*/
				LoadYearDropDown();

				dgSales.Attributes.Add("onclick", "DisplayConfirmationDialog()");
				dgSales.DataSource = null;
				dgSales.DataBind();
                
                
                RegisterClientScript();
                RefreshGrid();
			}

			
		}

		private void RegisterClientScript()
		{
			ClientScript.RegisterClientScriptBlock(this.GetType(), "DisplayConfirmationDialog", "function OpenDatePickerWindow(){var textInput; if(window.event.srcElement.tagName == 'INPUT')textInput = window.event.srcElement;else textInput = window.event.srcElement.previousSibling;var url = '../DatePicker.aspx';if(textInput.value.length > 0)url += '?dateValue=' + textInput.value;var returnValue = window.showModalDialog(url, '', 'dialogHeight:200px;dialogWidth:250px;help:no;resizable:no;status:no;unadorned:yes');if(typeof(returnValue) != 'undefined')textInput.value = returnValue;}", true);
			ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenDatePickerWindow", "function DisplayConfirmationDialog(){if(Page_IsValid){if(window.event.srcElement.tagName == 'INPUT' && window.event.srcElement.type == 'submit' && !window.event.srcElement.disabled){var confirmMessage;if(window.event.srcElement.parentElement.cellIndex == 4)confirmMessage = 'Are you sure you want to cancel this payment';else confirmMessage = 'Are you sure you want to cancel this request, cancelling it means it will be excluded from the automated process as well';if(!confirm(confirmMessage + '.\\nYou won\\'t be able to undo those changes.'))window.event.returnValue = false;}}}", true);
		}

		private void LoadYearDropDown()
		{
			ListItem li;
			DateTime dt = DateTime.Now.Date;
			String year;

			for (int index = 0; index < 11; index++)
			{
				year = dt.Year.ToString();

				li = new ListItem(year, year.Substring(2, 2));
				ddlYear.Items.Add(li);

				dt = dt.AddYears(1);
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
					dr["sale_id"] = sales[i].SalesId;
					// dr["product_class"] = sales[i].GetProductClass().Description;
					dr["sale_status"] = SalesStatus.GetSalesStatusByID(sales[i].SalesStatusId).Description;
					double amountToDisplay = sales[i].TotalAmount - sales[i].CalculateTotalPaymentsAndAdjustmentsAmount();
					dr["amount"] = amountToDisplay.ToString();
					dr["amount_display"] = "$" + amountToDisplay.ToString("N", new CultureInfo("en-US", false).NumberFormat);

					dt.Rows.Add(dr);

				}

				dgSales.DataSource = dt;
				dgSales.DataBind();
			}
		}

		// checks all sales and updates the Amount
		protected void LinkButtonAll_Click(object sender, System.EventArgs e)
		{
			bool check = true;

			for (int i = 0; i < dgSales.Items.Count; i++)
			{
				CheckBox chk = (CheckBox)dgSales.Items[i].FindControl("CheckBoxInclude");
				chk.Checked = check;

			}
		}

		// unchecks all sales and updates the Amount to 0
		protected void LinkButtonNone_Click(object sender, System.EventArgs e)
		{
			bool check = false;

			for (int i = 0; i < dgSales.Items.Count; i++)
			{
				CheckBox chk = (CheckBox)dgSales.Items[i].FindControl("CheckBoxInclude");
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

		protected void ButtonUpdatePayments_Click(object sender, System.EventArgs e)
		{
			string saleIds = "";
			Client client = (Client)Session[_CLIENT_];

			// check which sales are checked for processing
			for (int i = 0; i < dgSales.Items.Count; i++)
			{
				CheckBox chk = (CheckBox)dgSales.Items[i].FindControl("CheckBoxInclude");
				// if sale is selected, add its saleId 
				if (chk.Checked)
					saleIds += dgSales.Items[i].Cells[1].Text + "|";
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

		//private void RefundPaymentsByForeignOrderId(int orderId)
		//{
		//    try
		//    {
		//        // the collection of payments previously created for the orderId
		//        PaymentCollection payments = Payment.GetPaymentsByForeignOrderId(orderId);

		//        // new collections to be inserted after Refund
		//        PaymentCollection newPayments = new PaymentCollection();
		//        SaleCollection sales = new SaleCollection();
		//        CommentsCollection comments = new CommentsCollection();

		//        // list of the sales
		//        ArrayList saleIds = new ArrayList();

		//        if (payments.Count > 0)
		//        {
		//            PaymenTech paymenTech = new PaymenTech();
		//            TransactionController trans = new TransactionController();

		//            // try to refund the order at paymentech
		//            string response = paymenTech.Refund(orderId);
		//            string[] refundArray = response.Split("|".ToCharArray());

		//            // check if refund was successful
		//            if (refundArray[0] == "0")
		//            {

		//                // create a new negative payment for each payments of the order
		//                foreach (Payment p in payments)
		//                {
		//                    if (p.PaymentAmount > 0)
		//                    {
		//                        if (saleIds.IndexOf(p.SalesId) < 0)
		//                            saleIds.Add(p.SalesId);

		//                        Payment newPayment = new Payment();
		//                        newPayment.SalesId = p.SalesId;
		//                        newPayment.PaymentNo = newPayment.GetNextPaymentNo();
		//                        newPayment.AuthorizationNumber = refundArray[1];
		//                        newPayment.PaymentEntryDate = DateTime.Now;
		//                        newPayment.CashableDate = DateTime.Now;
		//                        newPayment.CreditCardNo = p.CreditCardNo;
		//                        newPayment.ExpiryDate = p.ExpiryDate;
		//                        newPayment.NameOnCard = p.NameOnCard;
		//                        newPayment.PaymentMethodId = p.PaymentMethodId;
		//                        newPayment.PaymentAmount = p.PaymentAmount * -1;
		//                        newPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
		//                        newPayment.ForeignOrderId = orderId;

		//                        newPayments.Add(newPayment);
		//                    }

		//                }

		//                // insert the new payments in the database
		//                trans.InsertPaymentsForCompletedTransactions(newPayments);

		//                // Update the sales and create a comment for each
		//                foreach (int i in saleIds)
		//                {

		//                    Sale currentSale = Sale.GetSaleByID(i);
		//                    if ((currentSale.TotalAmount - currentSale.CalculateTotalPaymentsAndAdjustmentsAmount()) > 0)
		//                    {
		//                        // currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID; 
		//                        currentSale.ConfirmedDate = DateTime.MinValue;
		//                        currentSale.Fuelsurcharge = 0;
		//                        currentSale.ArStatusId = ARStatus.NotPaid.ARStatusID;

		//                        sales.Add(currentSale);

		//                        // create a comment for the sale
		//                        Comments currentComment = new Comments(int.MinValue,
		//                            3,  // high priority
		//                            currentSale.SalesId,
		//                            currentSale.ConsultantId,
		//                            currentSale.LeadId,
		//                            9, // other 
		//                            DateTime.Now,
		//                            "CC transaction voided through Paymentech OrderID: " + orderId);

		//                        comments.Add(currentComment);
		//                    }

		//                }

		//                // insert the sales and comments
		//                trans.UpdateSalesAndInsertComments(sales, comments);

		//                Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('Refund Successful');</script>");
		//            }
		//            else
		//            {
		//                Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('Order not cancellable');</script>");
		//            }
		//        }
		//        else
		//        {
		//            Page.RegisterClientScriptBlock(DateTime.Now.Millisecond.ToString(), "<script language='javascript'>alert('No payments found for OrderId : " + orderId + "');</script>");
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//}

		protected void ButtonGetAmount_Click(object sender, EventArgs e)
		{

		}

		protected void AmountTextBox_DataBinding(object sender, EventArgs e)
		{

		}

		protected void ButtonSendRefund_Click(object sender, EventArgs e)
		{
			//int pos = TextBoxName.Text.IndexOf(" ", 0);
			//string firstName = TextBoxName.Text.Substring(0, pos);
			//string lastname = TextBoxName.Text.Substring(pos + 1, TextBoxName.Text.Length - pos - 1);
			//amount


            //this linw will make .net ignore the out of data web service certificate
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };

                

			string amnt = txtAmount.Text.ToString();
			string cents = "";
			int pos = amnt.IndexOf(".");

			if (pos > 0)
			{

				cents = amnt.Substring(pos + 1, amnt.Length - pos - 1);
                //check for at least 2 decimals after the dot
                if (cents.Length == 0)
                {
                    cents = "00";
                }
                else if (cents.Length == 1)
                {
                    cents = cents + "0";
                }
                else
                {
                    cents = cents.Substring(0, 2);
                }

				amnt = amnt.Substring(0, pos) + cents;
			}
			else
			{
				amnt = amnt + "00";

			}
			int amount = Convert.ToInt32(amnt);

			byte creditCardTypeId;

			//credit card
			CardType ccType;
			switch (ddlCCType.SelectedItem.Text.ToUpper())
			{
				case "VISA":
					ccType = CardType.VISA;
					creditCardTypeId = 1;
					break;
				case "MASTERCARD":
					ccType = CardType.MASTERCARD;
					creditCardTypeId = 2;
					break;
				case "AMEX":
					ccType = CardType.AMEX;
					creditCardTypeId = 3;
					break;
				case "DISCOVER":
					ccType = CardType.DISCOVER;
					creditCardTypeId = 4;
					break;
				default:
					ccType = CardType.DISCOVER;
					creditCardTypeId = 4;
					break;
			}

			BatchPaymentSystemWebservice oClient = new BatchPaymentSystemWebservice();

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
            else if (!isProd && !canada)
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


			BPPSTxResponse oResponse =
		    oClient.Refund(salesScreenCode,
						   appNo,
						   txtFirstName.Text,
						   txtLastName.Text,
						   "1","1","1","ca","11111",
						   CountryCode.US,
						   ccType,
						   txtCCNumber.Text,
						   int.Parse(ddlMonth.SelectedValue),
						   int.Parse(ddlYear.SelectedValue),
						   amount, "eFundraising.com", "eFundraising.com", salesScreenCode.ToString());

			// check if credit was successful
			if (oResponse.BPPS_Tx_Id > 0 && oResponse.ErrorMessage == "")
			{
				CreditCardRefundRequest cc = new CreditCardRefundRequest();
                cc.BppsID = oResponse.BPPS_Tx_Id;
                cc.RefundAmount = Convert.ToDouble(txtAmount.Text);
				cc.SaleID = Convert.ToInt32(txtSaleId.Text);
				cc.StatusCode = "905";
				cc.CreditCardTypeId = creditCardTypeId;
				cc.Processed = false;
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
			DateTime fromDate = DateTime.Parse(txtFromDate.Text);
			DateTime toDate = DateTime.Parse(txtTodate.Text);
			StringBuilder errorMessage = new StringBuilder();


            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };

			int applicationId = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("BPPSTxResponse.ApplicationID", "value"));
			string appGuid = ManageSaleScreen.GetValueFromWebConfig("BPPSTxResponse.AppGuid", "value");
            
            Trace.Write("Evaluate condition: " + fromDate + "<=" + toDate);
            if (fromDate <= toDate)
			{
                Trace.Write("Condition succeeded");

				CreditCardRefundRequest[] ccrrs = CreditCardRefundRequest.GetCreditCardRefundRequestByRequestDate(fromDate, toDate, chkDisplayCancelled.Checked);
				BatchPaymentSystemWebservice oClient = new BatchPaymentSystemWebservice();

				//iterate sales to update status
				foreach (CreditCardRefundRequest ccrr in ccrrs)
				{
                    Trace.Write("Evaluate condition: processed=" + !ccrr.Processed + " and statuscode=" + ccrr.StatusCode);
					if (!ccrr.Processed && ccrr.StatusCode != "100")
					{
						BPPSTxResponse oResponse = oClient.FetchResponse(applicationId, appGuid, ccrr.BppsID); //7 prod

                        Trace.Write("Got Response for bppsID:" + ccrr.BppsID + " with response code =" + oResponse.responseCode);
						if (String.IsNullOrEmpty(oResponse.responseCode) || !String.IsNullOrEmpty(oResponse.ErrorMessage))//No response code recieved: Error
						{
                            Trace.Write("Condition Failed " + oResponse.ErrorMessage);
							//if (String.IsNullOrEmpty(oResponse.responseCode))
							//	errorMessage.AppendFormat("<li>BPPSTxResponse Webservice call did not produce a response code for CreditCardRefundRequestID: {0}, SaleId:{1}, ReferringBPPSTxID:{2}", ccrr.CreditCardRefundRequestID, ccrr.SaleID, ccrr.BppsID);

							//if (!String.IsNullOrEmpty(oResponse.ErrorMessage))
							//	errorMessage.AppendFormat("<li>BPPSTxResponse Webservice call failed for CreditCardRefundRequestID: {0}, SaleId:{1}, ReferringBPPSTxID:{2}, Error:{3}", ccrr.CreditCardRefundRequestID, ccrr.SaleID, ccrr.BppsID, oResponse.ErrorMessage);
						}
						else
						{
							ccrr.StatusCode = oResponse.responseCode;
                            Trace.Write("Ready to update");
							ccrr.Update();
						}
					}
				}

                //refresh
                ccrrs = CreditCardRefundRequest.GetCreditCardRefundRequestByRequestDate(fromDate, toDate, chkDisplayCancelled.Checked);
				//

				dgSales.DataSource = ccrrs;
				dgSales.DataBind();
				pSalesDataGridHeader.Visible = false;

				if (errorMessage.Length > 0)
				{
					lblError.Text = "<ul>" + errorMessage.ToString() + "</ul>";
					efundraising.Diagnostics.Logger.LogError(errorMessage.ToString().Replace("<li>", "\n"));
				}
				else
				{
					lblError.Text = string.Empty;
				}
			}
		}

		private enum SalesDataGridColumns : byte
		{
			CreditCardRefundRequestID,
			SaleID,
			StatusCode,
			RefundAmount,
			StatusDescription,
			RequestDate,
			Cancelled,
			Processed,
			CancelPaymentButton,
			CancelRequestButton
		}

		protected void SalesDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			string doubleMinValue = double.MinValue.ToString("$0.00");

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if (e.Item.Cells[(byte)SalesDataGridColumns.RefundAmount].Text == doubleMinValue
					|| e.Item.Cells[(byte)SalesDataGridColumns.StatusCode].Text != "100"
					|| e.Item.Cells[(byte)SalesDataGridColumns.Processed].Text.ToUpper() == "TRUE")
				{
					((Button)e.Item.Cells[(byte)SalesDataGridColumns.CancelPaymentButton].Controls[0]).Enabled = false;

					if (e.Item.Cells[(byte)SalesDataGridColumns.RefundAmount].Text == doubleMinValue)
						e.Item.Cells[(byte)SalesDataGridColumns.RefundAmount].Text = string.Empty;
				}
			}

			if (e.Item.Cells[(byte)SalesDataGridColumns.Cancelled].Text.ToUpper() == "TRUE")
			{
				((Button)e.Item.Cells[(byte)SalesDataGridColumns.CancelRequestButton].Controls[0]).Enabled = false;
			}
		}

		protected void SalesDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			int saleId;
			double refundAmount;

			if (e.CommandSource == e.Item.Cells[(byte)SalesDataGridColumns.CancelPaymentButton].Controls[0])
			{
				if (double.TryParse(e.Item.Cells[(byte)SalesDataGridColumns.RefundAmount].Text.TrimStart("$".ToCharArray()), out refundAmount)
					&& int.TryParse(e.Item.Cells[(byte)SalesDataGridColumns.SaleID].Text, out saleId))
				{
					RefundPayment(int.Parse(e.Item.Cells[(byte)SalesDataGridColumns.CreditCardRefundRequestID].Text));
					RefreshGrid();
				}
			}
			else
			{
				CancelRequest(int.Parse(e.Item.Cells[(byte)SalesDataGridColumns.CreditCardRefundRequestID].Text));
				RefreshGrid();
			}
		}

		private void RefundPayment(int creditCardRefundRequestID)
		{
			try
			{
         

				TransactionController trans = new TransactionController();

				CreditCardRefundRequest request = CreditCardRefundRequest.GetCreditCardRefundRequestByID(creditCardRefundRequestID);

                //get payment type id
                byte paymentMethodID = 2;
                switch (request.CreditCardTypeId)
                {
                    case 1:
                        paymentMethodID = 2;
                        break;
                    case 2:
                        
                        paymentMethodID = 3;
                        break;
                    case 3:
                        
                        paymentMethodID = 8;
                        break;
                    case 4:
                        paymentMethodID = 9;
                        break;
                    
                }




				Payment newPayment = new Payment();
				newPayment.SalesId = request.SaleID;
				newPayment.PaymentNo = newPayment.GetNextPaymentNo();
				newPayment.PaymentEntryDate = DateTime.Now;
				newPayment.CashableDate = DateTime.Now;
				newPayment.PaymentMethodId = paymentMethodID;
				newPayment.PaymentAmount = request.RefundAmount * -1;
				newPayment.CollectionStatusId = CollectionStatus.FinalSettlementOffer.CollectionStatusID;
				newPayment.CommissionPaid = false;

                

                Sale currentSale = Sale.GetSaleByID(request.SaleID);
                if (currentSale != null)
                {

				    currentSale.ConfirmedDate = DateTime.MinValue;
				    currentSale.Fuelsurcharge = 0;
				    currentSale.ArStatusId = ARStatus.NotPaid.ARStatusID;

				    Comments currentComment = new Comments(int.MinValue,
					    3,  // high priority
					    currentSale.SalesId,
					    currentSale.ConsultantId,
					    currentSale.LeadId,
					    9, // other 
					    DateTime.Now,
					    "Credit card payment refunded");//get new comment from JF

				    int createUserID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID"));
                    
                 
                    trans.InsertPaymentAndCommentsAndUpdateSale(newPayment, currentSale, currentComment, createUserID);//over here

                    

				    //request.StatusCode = 
				    request.Processed = true;
				    request.Update();
                }
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private void CancelRequest(int creditCardRefundRequestID)
		{
			CreditCardRefundRequest request = CreditCardRefundRequest.GetCreditCardRefundRequestByID(creditCardRefundRequestID);
			request.Cancelled = true;
			request.Update();
		}

		protected void chkDisplayCancelled_CheckedChanged(object sender, EventArgs e)
		{
			RefreshGrid();
		}

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {

        }
	}
}