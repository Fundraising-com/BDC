using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using efundraising.EFundraisingCRM;
using efundraising.eFundData;
using EFundraisingCRMWeb.Components.Server;
using EfundraisingCRM.ePay.WebReference;
using MyExtensionMethods;
using log4net;
using System.Net.Http;
using MSXML2;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace efundraising.EFundraisingCRMWeb.Sales.CreditCard
{
    /// <summary>
    /// Summary description for _Default.
    /// </summary>
    public partial class _Default : System.Web.UI.Page
    {
        public ILog Logger { get; set; }

        public _Default()
	    {
            Logger = LogManager.GetLogger(GetType());
	    }

        private EFundraisingCRM.Sale[] sales = null;
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
            EFundraisingCRM.Client client = EFundraisingCRM.Client.GetClientByID(clientId, clientSequenceCode);

            if (client != null)
            {
                Session[_CLIENT_] = client;

                // get the client's billing address and use it to fill the fields
                EFundraisingCRM.ClientAddress billingAddress = EFundraisingCRM.ClientAddress.GetClientAddressByIdSequenceAddressType(client.ClientId, client.ClientSequenceCode, ClientAddressType.BillingAddress.AddressType);

                if (billingAddress != null)
                {
                    //if (billingAddress.CountryCode == efundraising.EFundraisingCRM.Country.UnitedStates.CountryCode)
                    //	{
                    sales = EFundraisingCRM.Sale.GetSalesByClient(client);

                    // get the most recent sale for the client and use it's credit card info to fill the fields
                    EFundraisingCRM.Sale latestSale = EFundraisingCRM.Sale.GetLatestSaleByLeadID(client.LeadId);
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
                                DropDownListMonth.SelectedValue = expiryDate[0];
                                DropDownListYear.SelectedValue = expiryDate[1].Substring(expiryDate[1].Length - 2, 2);
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
                
                //double totadjs = 0;

                for (int i = 0; i < sales.Length; i++)
                {

                    if (sales[i].SalesStatusId != SalesStatus.Cancelled.SalesStatusID
                        && sales[i].SalesStatusId != SalesStatus.PendingCancellation.SalesStatusID
                        && sales[i].SalesStatusId != SalesStatus.Unreachable.SalesStatusID)
                    {
                        DataRow dr = dt.NewRow();
                        dr["sale_id"] = sales[i].SalesId;
                        EFundraisingCRM.ProductClass pdCl = sales[i].GetProductClass();
                        if (pdCl != null)
                            dr["product_class"] = pdCl.Description;
                        EFundraisingCRM.SalesStatus SSt = SalesStatus.GetSalesStatusByID(sales[i].SalesStatusId);
                        if (SSt != null)
                            dr["sale_status"] = SSt.Description;

                        
                        //only want the discount/surcharge
                        double Discount = 0;
                        double Surcharge = 0;
                        efundraising.EFundraisingCRM.Adjustment[] adjs = efundraising.EFundraisingCRM.Adjustment.GetLatestAdjustmentsBySaleID(sales[i].SalesId);
                        foreach (efundraising.EFundraisingCRM.Adjustment a in adjs)
                        {
                            if (a.AdjustmentNo == 1)
                            {
                                Discount = Convert.ToDouble(a.AdjustmentAmount);
                            }
                            if (a.AdjustmentNo == 2)
                            {
                                Surcharge = Convert.ToDouble(a.AdjustmentAmount);
                            }
                        }

                            //double shippingfee = Convert.ToDouble(sales[i].ShippingFees);
                        double amountToDisplay = sales[i].TotalAmount; //- Discount + Surcharge;  //sales[i].CalculateTotalPaymentsAndAdjustmentsAmount(); //sales[i].TotalAmount; 
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
        { }

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
                        EFundraisingCRM.Sale currentSale = EFundraisingCRM.Sale.GetSaleByID(int.Parse(sale[0]));
                        currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID;
                        currentSale.ConfirmedDate = DateTime.MinValue;
                        currentSale.Fuelsurcharge = 0;
                        currentSale.ArStatusId = EFundraisingCRM.ARStatus.NotPaid.ARStatusID;

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
                        /*OE PAYMENT ENTRY*/
                        int createUserID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID"));

                        string message = "";
                           
                    bool success = trans.InsertPaymentsAndCommentsAndUpdateSales(newPayments, sales, comments, createUserID, ref message);
                   
                    


                        if (!success)
                        {
                            ManageSaleScreen.SendMail("An error occured while voiding a payment..." + message, 0, Request.Url.Authority, "SaleSupport");
                        }
                    

                }
                catch (Exception ex)
                {
                    Logger.Error("Unable to update sale after voiding.", ex);
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

            for (int i = 0; i < SalesDataGrid.Items.Count; i++)
            {
                RadioButton chk = (RadioButton)SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
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

            for (int i = 0; i < SalesDataGrid.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
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

            for (int i = 0; i < SalesDataGrid.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)SalesDataGrid.Items[i].FindControl("CheckBoxInclude");
                double currentSaleAmount = double.Parse(SalesDataGrid.Items[i].Cells[4].Text);
                chk.Checked = check;
            }

            AmountTextBox.Text = "0";
        }

        //protected void VoidTransactionLinkButton_Click(object sender, System.EventArgs e)
        //{
        //    string[] settleArray = (string[])Session[_SETTLE_ARRAY_];

        //    if (settleArray[2] != null)
        //    {
        //        VoidTransactionLinkButton.Visible = false;
        //        try
        //        {
        //            VoidTransaction(int.Parse(settleArray[2]));
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Error("Could not get orderId from session to void a transaction", ex);
        //        }
        //    }
        //}

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
            Logger.Error(message + " " + methodCall, ex);
        }
        #endregion

        protected void ProcessButtontest_Click(object sender, EventArgs e)
        {

            double amountToProcess = double.Parse(AmountTextBox.Text);
            double realTotal = 0;
            double selectedSaleTotal = 0;



            EFundraisingCRM.Client client = (EFundraisingCRM.Client)Session[_CLIENT_];
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
            string[] settleArray = authArray;


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
                        EFundraisingCRM.Sale currentSale = EFundraisingCRM.Sale.GetSaleByID(int.Parse(sale[0]));


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

                        if (selectedSaleTotal > amountPerSale + 0.01)
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
                    try
                    {
                        ProcessNew();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("ProcessNew error ", ex);

                    }


                }
            }
        }

        private void ProcessNew()
        {
            double amountToProcess = double.Parse(AmountTextBox.Text);
            double realTotal = 0;
            double selectedSaleTotal = 0;

           
            if (amountToProcess > 0)
            {
                EFundraisingCRM.Client client = (EFundraisingCRM.Client)Session[_CLIENT_];
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
                    //string amnt = amountToProcess.ToString();
                    
                    decimal amount = Convert.ToDecimal(amountToProcess);
                    
                    var ccType = int.MinValue;
                    switch (DropDownListCCType.SelectedItem.Text.ToUpper())
                    {
                        case "VISA":
                            ccType = 2;
                            break;
                        case "MASTERCARD":
                            ccType = 3;
                            break;
                        case "AMEX":
                            ccType = 8;
                            break;
                        case "DISCOVER":
                            ccType = 9;
                            break;
                        default:
                            ccType = 2;
                            break;

                    }
                   
                    var countryName = "Unites States";
                    int salesScreenCode = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("saleScreenCode", "value"));
                    CountryCode country = CountryCode.US;
                    bool canada = false;
                    if (DropDownListCountry.SelectedValue == "CA")
                    {
                        country = CountryCode.CA;
                        canada = true;
                        salesScreenCode = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("saleScreenCodeCanada", "value"));
                        countryName = "Canada";
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
                            zip = zip.Replace(" ", "");
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

                        var salesid = int.MinValue;
                        foreach (string[] sale in salesInfo)
                        {
                            if (sale[0] != null)
                            {
                                salesid = int.Parse(sale[0]);
                            }
                            else
                            {
                                salesid = 1;
                            }
                        }
                        
                        if (!badZip)
                        {
                            Logger.Error("IN PROCESS creating json string");
                            ServerXMLHTTP ccProcessing = new ServerXMLHTTP();
                            // NEW CC PROCESS USING GAPAY through API GA.BDC.WebApi.Fundraising.Core
                            var strJSONToSend = "{ \"Number\":\"" + TextBoxCCNumber.Text + 
                                "\",\"Holder\":\"" + firstName.Trim() + " " + lastname.Trim() + 
                                "\",\"ExpirationDate\":\"" + int.Parse(DropDownListMonth.SelectedValue).ToString("00") + int.Parse(DropDownListYear.SelectedValue).ToString("00") + 
                                "\",\"CVV\":\"" + TextBoxCVV2.Text + 
                                "\",\"InternalPaymentMethod\":" + ccType + 
                                ",\"Amount\":\"" + amount + 
                                "\",\"Reference\":\"" + salesid + 
                                "\",\"Address\":{ \"Address1\":\"" + address.Trim() + 
                                    "\",\"City\":\"" + TextBoxCity.Text + 
                                    "\",\"Region\":{ \"Code\":\"" + stateCode + 
                                    "\",\"Name\":null,\"CountryCode\":null},\"Country\":{ \"Code\":\"" + country.ToString() + 
                                    "\",\"Name\":\"" + countryName.ToString() + 
                                    "\"},\"PostCode\":\"" + zip + 
                                "\"} }";

                            Logger.Error("IN PROCESS5 - " + salesid + " - " + firstName.Trim() + " " + lastname.Trim() + " - " + amount + " - " + stateCode + " - " + countryName.ToString());
                            var serverUrl = (ManageSaleScreen.GetValueFromWebConfig("core.webapi.host.creditcard", "value"));
                            var postDataBytes = Encoding.Default.GetBytes(strJSONToSend);

                            ccProcessing.open("POST", serverUrl, false);
                            ccProcessing.setRequestHeader("Content-Type", "application/json");
                            ccProcessing.setRequestHeader("Content-Length", postDataBytes.Length.ToString());
                            ccProcessing.send(postDataBytes);

                            //Logger.Error("CC Request for sale: " + salesid + " Stripe Status: " + ccProcessing.statusText + " Stripe Status #: " + ccProcessing.status);
                            Logger.Debug("CC Request for sale: " + salesid + " Stripe Status: " + ccProcessing.statusText + " Stripe Status #: " + ccProcessing.status);
                            // check if authorization was successful
                           
                            
                            if (ccProcessing.status == 200)
                                {

                               
                                //var parseObjAuthNumber = (JObject)JsonConvert.DeserializeObject(ccProcessing.responseText);
                                //var ccAuthNUmber = parseObjAuthNumber["AuthNumber"].Value<string>();
                                

                                // if we have partial payments to do, split the amount equally between sales
                                double amountPerSale = 0;

                                // create the Payments and Comments, and Update the sales from salesInfo(the list of selected sales) then commit to the DB.
                                foreach (string[] sale in salesInfo)
                                {
                                    try
                                    {
                                        double processedAmountForSale = 0;
                                        EFundraisingCRM.Sale currentSale = EFundraisingCRM.Sale.GetSaleByID(int.Parse(sale[0]));


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
                                        //currentPayment.AuthorizationNumber = ccAuthNUmber;
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
                                                //UPDATE: AUTO CONFIRM 
                                                currentSale.SalesStatusId = SalesStatus.OnHold.SalesStatusID;
                                                //currentSale.ConfirmedDate = DateTime.Now;
                                           
                                            }
                                            if (currentSale.UpfrontPaymentRequired < 0)
                                                currentSale.UpfrontPaymentRequired = 0;
                                            currentSale.Fuelsurcharge = 0;
                                        }
                                        currentPayment.CommissionPaid = false;

                                        //update sale payment method
                                        currentSale.PaymentMethodId = currentPayment.PaymentMethodId;

                                        // create a comment for the sale
                                        Comments currentComment = new Comments(int.MinValue,
                                            3,  // high priority
                                            currentSale.SalesId,
                                            currentSale.ConsultantId,
                                            0,
                                            9, // other 
                                            DateTime.Now,
                                             "Stripe CC processed Successful: " + amountToProcess);
                                        //"Stripe CC processed Successful AuthID: " + ccAuthNUmber + " for $" + amountToProcess


                                        try
                                        {
                                            payments.Add(currentPayment);
                                            Logger.Debug("currentPayment");
                                            sales.Add(currentSale);
                                            Logger.Debug("currentSale");
                                            comments.Add(currentComment);
                                            Logger.Debug("currentComment");
                                        }
                                        
                                        catch (Exception ex)
                                        {
                                        PublishError("error" + ex);
                                        
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        PublishError("Error on Sales and Payments creation.", "", ex);
                                        ProcessButton.Enabled = true;
                                        Session["ClickOnce"] = 0;
                                    }
                                }//for each

                                
                                //Insert Payment ENTRY                              
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
                                            LabelStatus.Text = "Transaction Successful!<br>";

                                            //LabelStatus.CssClass = "BigTextBold Success";
                                            //LabelStatus.Text = "Transaction Successful!<br>"
                                            //    + "Stripe Authorization Number: " + ccAuthNUmber + "<br>"
                                            //    + "Affected Sale(s): ";
                                            //+salesid
                                            foreach (string[] sale in salesInfo)
                                            {
                                                LabelStatus.Text += sale[0] + "<br>";
                                                SendNotificationEmailRules(sale[0]);
                                            }

                                            Session[_SETTLE_ARRAY_] = null; //removed it
                                            Session[_AFFECTED_SALES_] = salesInfo;
                                            //VoidTransactionLinkButton.Visible = true;
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

                                       // ManageSaleScreen.SendMail("Error on payment transaction." + " ePAy ID:" + orderID, 0, Request.Url.Authority, "SaleSupport");
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

                                        //if (ccAuthNUmber != null)
                                        //{
                                        //    PublishError("Error on CC Transaction", "Auth number=" + ccAuthNUmber, ex);
                                        //}



                                    }
                                    FillSalesDataGrid();
                                }


                            }
                            else  //if code <> 100
                            {

                                // display message on authorization error
                                string message = "Error processing CC (Stripe Service) ";
                                //switch (ccProcessing.status.ToString())
                                //{
                                //    case "201": message = "Bad Credit Card Number";
                                //        break;
                                //    case "233": message = "Does Not Match MOP";
                                //        break;

                                //    case "303": message = "Declined Transaction";
                                //        break;
                                //    case "501": message = "Card should be returned";
                                //        break;
                                //    case "502": message = "Card reported lost or stolen";
                                //        break;
                                //    case "509": message = "Over limit exceeded";
                                //        break;
                                //    default: message = "General Failure Processing Card. Please verify CC Number and try again.";
                                //        break;
                                //}
                                LabelStatus.CssClass = "BigTextBold Alert";
                                LabelStatus.Text = message + "<br>" + "Message: " + ccProcessing.statusText + " Error Code:" + ccProcessing.status + "<br>"  + "CC Reponse Text: " + ccProcessing.responseText;
                                LabelStatus.Visible = true;
                                ProcessButton.Enabled = false;
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

        private void SendNotificationEmailRules(string saleID)
        {
            // SEND CLIENT EMAIL NOTIFICATION and Confirm order
            Logger.Error("Inside SendNotificationEmailRules: " + saleID);
            Logger.Error("Session[pcIDForSurchage].ToString()" + Session["pcIDForSurchage"]);
            //var pclass = Session["pcIDForSurchage"].ToString();
            var areAllSalesPaid = true;
            EFundraisingCRM.Sale currentSale = EFundraisingCRM.Sale.GetSaleByID(int.Parse(saleID));
            PaymentCollection currentPayments = Payment.GetPaymentsBySaleId(int.Parse(saleID));
            PaymentCollection allPaymentsFromLead = Payment.GetAllPaymentsByLeadId(currentSale.LeadId);
            var isCurrentSaleTotalAmountLowerThan5000 = currentSale.TotalAmount < 5000 && currentPayments.ToList().Sum(p => p.PaymentAmount) == currentSale.TotalAmount;
            //var areAllSalesPaid = allPaymentsFromLead.ToList().Any(p => p.CollectionStatusId != 8);

            
            if (allPaymentsFromLead.Count > 0)
            {
                areAllSalesPaid = false;
                Logger.Error("SALE NOT AUTOCONFIRMED PREVIOUS SALES NOT PAID / SALE-ID: " + currentSale.SalesId);
            }

           
            if (isCurrentSaleTotalAmountLowerThan5000 && areAllSalesPaid)
            {
                try
                {
                    Logger.Error("Before Json: ");

                    //if (pclass == "36")
                    //{
                    //    currentSale.SalesStatusId = 6;
                    //}
                    //else
                    //{
                    //    currentSale.SalesStatusId = 2;
                    //    currentSale.ConfirmedDate = DateTime.Now;
                    //}

                    if (currentSale.SalesStatusId != 2)
                    {
                        currentSale.SalesStatusId = 2;
                        currentSale.ConfirmedDate = DateTime.Now;
                    }

                    
                    
                    //currentSale.Update();
                    //Logger.Error("Sales Screen: CC AutoConfirmed" + currentSale.SalesId);
                    //ServerXMLHTTP emailProcessing = new ServerXMLHTTP();
                    //var strJSONToSend2 = "{ \"Type\":\"" + 17 +
                    //    "\",\"ExternalId\":\"" + currentSale.ClientId +
                    //    "\",\"Email\":\"fake@fake.com" +
                    //    "\",\"ExtraData\":\"1|" + currentSale.ClientSequenceCode + "|" + currentSale.LeadId + "|" + currentSale.SalesId +
                    //    "\"}";

                    //var serverNotificationUrl = (ManageSaleScreen.GetValueFromWebConfig("core.webapi.host.notification", "value"));
                    //Logger.Error("after Json: " + strJSONToSend2 + " - " + serverNotificationUrl);

                    //var postDataBytes2 = Encoding.Default.GetBytes(strJSONToSend2);

                    //emailProcessing.open("POST", serverNotificationUrl);
                    //emailProcessing.setRequestHeader("Content-Type", "application/json");
                    //emailProcessing.setRequestHeader("Content-Length", postDataBytes2.Length.ToString());
                    //emailProcessing.send(postDataBytes2);

                    //////Logger.Debug(emailProcessing.status + " - " + emailProcessing.statusText);
                    //Logger.Error(emailProcessing.status + " - " + emailProcessing.statusText + " - " + emailProcessing.responseText);

                    ////ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
                    ////Response.Redirect("/Sales/SalesScreen/NewSales.aspx?sid=" + currentSale.SalesId);

                }
                catch (Exception ex)
                {
                    Logger.Error("ERROR IN CC EMAIL NOTIFICATIONS SECTION " + ex + "/ SALE-ID: " + currentSale.SalesId);

                    throw ex;
                }

            }
            
       
          
                  
                }
            }
        }


    internal class CCInfoToSend
    {
        public string Address { get; set; }
        public int Amount { get; set; }
        public string CVV { get; set; }
        public int ExpirationDate { get; set; }
        public string Holder { get; set; }
        public int InternalPaymentMethod { get; set; }
        public string Number { get; set; }
        public string Reference { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Code { get; set; }

    }

