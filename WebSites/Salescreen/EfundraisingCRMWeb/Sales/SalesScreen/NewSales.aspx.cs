using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI;
using EFundraisingCRMWeb.Components.Server;
using efundraising.EnterpriseComponents;
using efundraising.EFundraisingCRM;
//using QSP.Business.Fulfillment;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;
using MSXML2;
using System.Text;
//using GA.BDC.Data.Fundraising.EFundStore;


//passphrase line 919
//CalaculateTotals line 544
namespace EFundraisingCRMWeb
{

    public enum DropDownListProfitStatus
    {
        Default = 0,
        AddNewItem = 1,
        Recalculate,
        AfterProductSelected,
        SaveSale
    }

}

namespace EFundraisingCRMWeb.Sales.SalesScreen
{
    public partial class NewSales : EFundraisingCRMSalesBasePage, IPage, INoQuickCreate, INoPageInformation, INoQuickSearch
    {
       
        
        
        
        
        protected Components.User.ClientHeader ClientHeader1;
        protected Components.User.PaymentInformation.PaymentOptions PaymentOptions1;

        protected Components.User.Sales.Items Items1;
        protected Components.User.Sales.SaleInfo SaleInfo1;
        protected System.Web.UI.WebControls.Button SaveSaleButton;
        protected Components.User.Sales.Status SaleStatus1;
        protected System.Web.UI.WebControls.Label messageLabel;
        protected Components.User.Sales.SaleDates SaleDates1;
        protected System.Web.UI.WebControls.Label Label4;


        private int SaleID;


        #region Private Properties and Methods

        private int clientId
        {
            get
            {
                try
                {
                    /*if (Request["clid"] == null)
                         return int.MinValue;
                     return System.Convert.ToInt32(Request["clid"]);*/
                    if (ViewState[Global.SessionVariables.CLIENT_ID] == null)
                        return int.MinValue;
                    return System.Convert.ToInt32(ViewState[Global.SessionVariables.CLIENT_ID]);
                }
                catch (Exception)
                {
                    return int.MinValue;
                }
            }
            set
            {
                ViewState[Global.SessionVariables.CLIENT_ID] = value;
            }

        }

        private string clientSeq
        {
            get
            {
                var seq = ViewState[Global.SessionVariables.CLIENT_SEQUENCE_CODE];
                return seq != null ? seq.ToString() : string.Empty;
            }
            set
            {
                ViewState[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = value;
            }
        }

        private int saleId
        {
            get
            {
                try
                {
                    if (Request["sid"] == null)
                    {
                        return int.MinValue;
                    }
                    else
                    {
                        return System.Convert.ToInt32(Request["sid"]);
                    }

                }
                catch (Exception)
                {
                    return int.MinValue;
                }
            }
            set { SaleID = value; }
        }

        private Client clientInfoInMemory
        {
            get
            {
                return (Client)ViewState["clientInfoInMemory"];
            }
            set
            {
                ViewState["clientInfoInMemory"] = value;
            }
        }


        #endregion


        private void SetViewStateInfo()
        {
            //fill client header
            ClientHeader1.SetClientInfo(clientId, clientSeq);
            //set viewstate variables'
            Items1.clientId = clientId;
            Items1.clientSeq = clientSeq;
            PaymentOptions1.clientId = clientId;
            PaymentOptions1.clientSeq = clientSeq;

        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 60)) + "; url=../../SessionExpired.aspx");


            System.Web.HttpContext.Current.Session["ClickOnce"] = 0;
            System.Web.HttpContext.Current.Session["x"] = 0;
            ///////////////////////////////////////////////
            //VERSION
            versionError.Text = "v2.02";

            ///////////////////////////////////////////////

            // Put user code to initialize the page here
            int debugRowLine = 0;
            string referer = "No";
            int leadID = 0;
            errorMainLabel.Visible = false;

            try
            {


               // ModalLabel.Text = "";
                errorLabel.Visible = false;
                if (!IsPostBack)
                {




                    if (Request["msg"] != null)
                    {
                        string msg = Request["msg"].ToString();
                        if (msg.Length > 1)
                        {
                            //ModalLabel.Text = msg;
                            //pnlModal_ModalPopupExtender.Show();


                        }
                    }
                    debugRowLine = 1;
                    SaleIsValidated = false;
                    SalesItemCollection sCol = null;


                    //NEW SALE
                    if (saleId < 1)
                    {

                        bool error = true;

                        //need to reset seesion from sales object to store shipping and sales ID for new sale
                        System.Web.HttpContext.Current.Session[Global.SessionVariables.SALES_SHIPPING_FEE] = null;
                        System.Web.HttpContext.Current.Session[Global.SessionVariables.SALES_ID] = null; 


                        Logger.Debug("outside - Request[clID] != null && Helper.IsNumeric(Request[clID]");
                        if (Request["clID"] != null && Helper.IsNumeric(Request["clID"]))
                        {
                            Logger.Debug("inside - Request[clID] != null && Helper.IsNumeric(Request[clID]");
                            clientId = Convert.ToInt32(Request["clID"]);
                            //Session[Global.SessionVariables.CLIENT_ID] = Convert.ToInt32(Request["clID"]);
                            if (Request["clseq"] != null && Request["clseq"].ToString().Length == 2 && !(Helper.IsNumeric(Request["clseq"])))
                            {
                                Logger.Debug("inside - Request[clseq] != null && Request[clseq].ToString().Length == 2 && !(Helper.IsNumeric(Request[clseq)))");
                                debugRowLine = 4;

                                //Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE] = Request["clseq"].ToString();
                                clientSeq = Request["clseq"].ToString();

                                //set the lead ID
                                var c = Client.GetClientByID(clientId, clientSeq);
                                Logger.Debug("after -  Client.GetClientByID(clientId, clientSeq); -- " + c.LeadId);
                                leadID = c.LeadId;
                                Session[Global.SessionVariables.LEAD_ID] = leadID;
                                Logger.Debug("line 221");

                                error = false;
                            }
                            SetViewStateInfo();
                        }




                        if (error)
                        {
                            Logger.Debug("line 232");
                            if (Request.UrlReferrer != null)
                            {
                                referer = Request.UrlReferrer.AbsolutePath.ToString();
                            }

                            if (Request["clid"] == null || Request["clseq"] == null)
                            {
                                //dont log the error
                                //pnlModal_ModalPopupExtender.Show();
                                //ModalLabel.Text = "No valid sale or client was specified in the request";
                                return;

                            }
                            string msg = "Error finding the client specified: " + Request["clseq"].ToString() + Request["clid"].ToString() + " --Referer: " + referer;
                            Logger.Error("Sales Screen: NewSales Page Load. " + msg);
                            Items1.Visible = false;
                            errorMainLabel.Visible = true;
                            errorMainLabel.Text = "Error finding the client";


                        }
                        else
                        {

                            Logger.Debug("line 257");
                            Items1.SetOneEmptyRow();
                            //set consultant name , sale date

                            if (leadID > 0)
                            {
                                Logger.Debug("line 264");
                                Lead l = Lead.GetLeadByID(leadID);
                                Logger.Debug("line 266");
                                int consultantID = Convert.ToInt32(l.ConsultantId);
                                SaleInfo1.ConsulantID = consultantID;
                            }

                            Logger.Debug("line 268");
                            SaleDates1.SaleDate = DateTime.Now;
                            SaleDates1.ScheduledShipDate = ProductBusinessRules.GetNextBusinessDay(DateTime.Now, 1);
                            SaleDates1.PaymentDueDate = DateTime.Now.AddDays(30);
                        }
                    }
                    else //IF SALE EXIST
                    {



                        // START - GET ext billing id from sale and populate sap account id on salescreen
                        string conn = "";
                        efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext();
                        if (EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsProd())
                        {
                            conn = ConfigurationManager.ConnectionStrings["EFRProdConnectionString"].ConnectionString;
                            db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext(conn);
                        }
                        else
                        {
                            conn = ConfigurationManager.ConnectionStrings["EFRProdConnectionStringDEV"].ConnectionString;
                            db = new efundraising.EFundraisingCRM.Linq.eFundraisingProdDataContext(conn);
                        }

                        // get ext billing id
                        var salesbillingID = from d in db.sales
                                             where d.sales_id == saleId && d.ext_billing_account_id != null
                                             select d.ext_billing_account_id;


                        if (salesbillingID != null)
                        {
                            AccountIDTextBox.Text = salesbillingID.FirstOrDefault();
                        }
                        else
                        {
                            AccountIDTextBox.Text = "N/A";

                        }

                        //                       •First() - throws if empty/not found, does not throw if duplicate
                        //•FirstOrDefault() - returns default if empty/not found, does not throw if duplicate
                        //•Single() - throws if empty/not found, throws if duplicate exists
                        //•SingleOrDefault() - returns default if empty/not found, throws if duplicate exists


                        // END - GET ext billing id from sale and populate sap account id on salescreen












                        //set comment
                        Comments comment = Comments.GetCommentBySaleIDAndLeadID(saleId);
                        if (comment != null)
                        {
                            CommentTextBox.Text = comment.Comment;
                        }


                        Sale s = Sale.GetSaleByID(saleId);


                        


                        //need to create seesion from sales object to store shipping and sales ID
                        System.Web.HttpContext.Current.Session[Global.SessionVariables.SALES_SHIPPING_FEE] = s.ShippingFees;
                        System.Web.HttpContext.Current.Session[Global.SessionVariables.SALES_ID] = s.SalesId; 
                        
                        OEIdTextBox.Text = s.ExtOrderID.ToString();

                        int sapstatus = s.SAPOrderStatusID;

                        if (sapstatus != int.MinValue)
                        {
                            SalesSAPStatus sap = SalesSAPStatus.GetSAPSalesStatusByID(sapstatus);
                            StatusTextbox.Text = sap.Description.ToString();
                        }



                        if (s == null)
                        {
                            //dont log the error
                            //pnlModal_ModalPopupExtender.Show();
                            //ModalLabel.Text = "The sale requested does not exist";
                            return;
                        }
                        clientId = s.ClientId;
                        clientSeq = s.ClientSequenceCode;

                        if (Session["Country"] == null)
                        {
                            efundraising.EFundraisingCRM.ClientAddress c = efundraising.EFundraisingCRM.Client.GetBillingClientAddressByID(s.ClientId, s.ClientSequenceCode);
                            if (c != null)
                            {
                                Session["Country"] = c.CountryCode;
                            }
                        }



                        SetViewStateInfo();

                        if (s != null)
                        {

                            //set External ids

                            bool isProd = Convert.ToBoolean(ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.Production", "isProduction"));


                            if (isProd)
                            {

                                //set adjustments
                                //decimal discount = 0;
                                //decimal surcharge = 0;
                                //int discountReasonID = 0;
                                //int surchargeReasonID = 0;

                                efundraising.EFundraisingCRM.Adjustment[] adj = efundraising.EFundraisingCRM.Adjustment.GetLatestAdjustmentsBySaleID(s.SalesId);
                                foreach (efundraising.EFundraisingCRM.Adjustment a in adj)
                                {
                                    if (a.AdjustmentNo == 1)
                                    {
                                        Items1.Discount = Convert.ToDecimal(a.AdjustmentAmount);
                                        Items1.DiscountReasonId = a.ReasonID;
                                        Items1.DiscountBox.Text = a.AdjustmentAmount.ToString("C");

                                    }
                                    else if (a.AdjustmentNo == 2)
                                    {
                                        Items1.Surcharge = Convert.ToDecimal(a.AdjustmentAmount);
                                        Items1.SurchargeReasonId = a.ReasonID;
                                        Items1.SurchargeBox.Text = a.AdjustmentAmount.ToString("C");
                                    }
                                   

                                }
                            }
                            Items1.AdjTotalBox.Text = (Items1.Surcharge - Items1.Discount).ToString("C");

                            
                            SaleInfo1.ConsulantID = s.ConsultantId;
                            SaleInfo1.SetInfo(s, clientInfoInMemory);

                            sCol = SalesItem.GetSalesItemsBySaleID_Collection(saleId);


                            Client c = Client.GetClientByID(s.ClientId, s.ClientSequenceCode);
                            leadID = c.LeadId;
                            Session[Global.SessionVariables.LEAD_ID] = leadID;

                            SaleStatus1.SetInfo(s, leadID);
                            Items1.SetData(sCol);
                            SaleDates1.SetInfo(s);
                            PaymentOptions1.SetInfo(s);

                            //calculate totals if sale is not confimred, else get totals from db
                            debugRowLine = 18;
                            SalesStatus ss = SalesStatus.GetSalesStatusByID(SaleStatus1.SaleStatusID);
                            if (ss.Description == "Confirmed" || ss.Description == "Cancelled")
                            {
                                Items1.GetTotals(saleId);
                            }
                            else
                            {
                                Recalculate(false);

                            }
                        }
                        else  //error sale doesnt exists
                        {
                            if (Request.UrlReferrer != null)
                            {
                                referer = Request.UrlReferrer.AbsolutePath.ToString();
                            }

                            Logger.Error("Sales Screen: NewSales Page Load Sale doesnt exists: " + saleId + " --Referer:" + referer);
                            errorMainLabel.Visible = true;
                            errorMainLabel.Text = "The sale you requested does not exist!";

                        }



                    }
                }

                debugRowLine = 21;
                if (clientId == null)
                {
                    //pnlModal_ModalPopupExtender.Show();
                    //ModalLabel.Text = "Your session has expired. Please reopen your browser";
                    return;
                    debugRowLine = 29;
                }

                Logger.Debug("line 474");
                clientInfoInMemory = Components.Server.ManageClient.GetClient(clientId, clientSeq);





                //Set Rights
                debugRowLine = 22;

                SetRights();

                //disable consultant drop down list there are payments on sale
                Logger.Debug("line 487");
                PaymentCollection payments = efundraising.EFundraisingCRM.Payment.GetPaymentsBySaleId(saleId);
                if (payments.Count > 0)
                {
                    bool isMIS = Components.Server.ManageSaleScreen.IsMIS();
                    if (!isMIS)
                    {
                        SaleInfo1.DisableConsultant();
                    }
                }




                // }

                debugRowLine = 24;
                Logger.Debug("line 504");
                if (string.Compare(this.PostBackTarget, "productselected", true) == 0)
                {
                    debugRowLine = 25;
                    AfterSelectProduct(this.PostBackArgument);
                }

                Logger.Debug("line 511" + this.PostBackTarget);
                debugRowLine = 27;
                if (string.Compare(this.PostBackTarget, "allproductselected", true) == 0)
                {
                    debugRowLine = 26;
                    Logger.Debug("line 516");
                    AfterSelectAllProduct(this.PostBackArgument);
                }
                Logger.Debug("line 512");

            }
            catch (Exception ex)
            {
                Logger.Error("Sales Screen: NewSales Page Load line:" + debugRowLine, ex);
            }
            Logger.Debug("line 513");
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

        #region IPage Members

        public string PageInformation
        {
            get
            {
                return "Main Sales Form";
            }
        }

        public string PageDescription
        {
            get
            {
                return "Sale Form";
            }
        }

        public override void Search(string searchQuery)
        {
            //get leadid coprrspondiong to sale id
            Sale s = Sale.GetSaleByID(Convert.ToInt32(searchQuery));
            if (s != null)
            {
                Redirect("../../Sales/SalesScreen/Default.aspx?clid=" + s.ClientId + "&seq=" + s.ClientSequenceCode);
            }
        }

        public override void Create(string redirection)
        {
            base.Create(redirection);
        }

        #endregion

        private void AfterSelectAllProduct(string prodIdAndRowNo)
        {

            string debug = "0";
            try
            {
                Logger.Debug("line 594");
                string[] Split = prodIdAndRowNo.Split(';');
                int rn = 0;
                if (Split[1] != "")
                {
                    debug = "1";
                    rn = Convert.ToInt32(Split[1]);
                }

                string allIds = Split[0];
                string[] ids = allIds.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    AfterSelectProductProcess(Convert.ToInt32(ids[i]), rn);
                    rn++;
                }
                Recalculate(false);
            }
            catch (Exception ex)
            {
                Logger.Error("Sales Screen: AfterSelectProduct. debug:" + debug, ex);

            }
        }

        private void AfterSelectProduct(string prodIdAndRowNo)
        {
            int scratchBookId = -1;
            int rn = -1;

            SplitIds(prodIdAndRowNo, ref scratchBookId, ref rn);
            AfterSelectProductProcess(scratchBookId, rn);
            Recalculate(false);
        }

        private void SplitIds(string prodIdAndRowNo, ref int scratchBookId, ref int rn)
        {
            string[] theSplits = prodIdAndRowNo.Split(';');
            if (theSplits.Length > 1)
            {
                try
                {
                    rn = Convert.ToInt32(theSplits[1]);
                }
                catch (Exception exception)
                {
                    Logger.Error("Sales Screen: Split Ids", exception);
                }

                int productID = -1;

                try
                {
                    scratchBookId = Convert.ToInt32(theSplits[0]);
                }
                catch (Exception exception)
                {
                    Logger.Error("Sales Screen: Split Ids", exception);
                }
            }
        }

        //if comes from selectAll, we wont calculate every time
        private void AfterSelectProductProcess(int scratchBookID, int rn)
        {
            try
            {

                SaleIsValidated = false;

                DataTable dt = this.Items1.GetDataFromDatagrid();

                if (rn > -1 && scratchBookID > -1)
                {
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(dt.NewRow());
                    }
                    Logger.Debug("line 669");
                    ScratchBook sc = ScratchBook.GetScratchBookByID(scratchBookID);
                    //bool isFixedProfit = true;
                    if (sc != null)
                    {
                        if (rn + 1 > dt.Rows.Count)
                        {
                            dt.Rows.Add(dt.NewRow());
                        }
                        dt.Rows[rn]["Product"] = sc.Description;
                        dt.Rows[rn]["ScratchbookID"] = scratchBookID;
                        dt.Rows[rn]["ProductCode"] = sc.ProductCode;
                        //dt.Rows[rn]["SalesItemID"] = sc.ProductCode;
                        if (sc.FixedProfit != int.MinValue && sc.ProductClassId == 53)
                        {
                            dt.Rows[rn]["Profit"] = sc.FixedProfit;
                        }
                        else
                        {
                            dt.Rows[rn]["Profit"] = ""; //will set to default value
                        }
                    }

                    Items1.SetData(dt, new DropDownListProfitStatus[] { DropDownListProfitStatus.AfterProductSelected });
                    Items1.RowNumberItemAdded = rn;
                    Items1.RebindDatagrid(dt);
                }

                //Items1.SetOneEmptyRow();
            }
            catch (Exception exception)
            {
                Logger.Error("Sales Screen: New Sales. After select product", exception);
            }


        }


        private bool Recalculate(bool validate)
        {

            int nbProductClass = -1;
            bool error = Recalculate(validate, ref nbProductClass);
            return error;
        }

        private bool Recalculate(bool validate, ref int nbProductClass)
        {
            bool error = false;
            string debug = "1";
            try
            {


                //ArrayList productList = null;
                DataTable productList = new DataTable();
                DataColumn col = new DataColumn("ScratchbookID");
                productList.Columns.Add(col);
                col = new DataColumn("NbItems");
                productList.Columns.Add(col);

                int statusID = SaleStatus1.SaleStatusID;
                SalesStatus ss = SalesStatus.GetSalesStatusByID(statusID);

              

                debug = "2";


                //validate some data
                if (Items1.Discount == -1)
                {
                    //ModalLabel.Text = "The Discount amount is invalid.";
                    //pnlModal_ModalPopupExtender.Show();
                    return true;
                }
                else if (Items1.Surcharge == -1)
                {
                    //ModalLabel.Text = "The Surcharge amount is invalid.";
                    //pnlModal_ModalPopupExtender.Show();
                    return true;
                }


                debug = "3";

                error = Items1.CalculateTotals(validate, ref nbProductClass, ref productList);

                //GET THE TOTAL qty of frozen food for the   COOKIE DOUGH RULE
                int frozenFoodID = Convert.ToInt32(Components.Server.ManageSaleScreen.GetValueFromWebConfig("FrozenFoodCaseShortAmount", "productClassID"));
                int pineValleyID = Convert.ToInt32(Components.Server.ManageSaleScreen.GetValueFromWebConfig("PineValleyShortAmount", "productClassID"));
                DataTable classTotals = (DataTable)Session[Global.SessionVariables.CLASS_TOTALS];
                DataTable packageTotals = (DataTable)Session[Global.SessionVariables.PACKAGE_TOTALS];
                int FFqty = Convert.ToInt32(Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", frozenFoodID.ToString()));
                int PVqty = Convert.ToInt32(Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", pineValleyID.ToString()));
                string setProfit = "";


                //SET PROFIT % ACCORDING THE BUSINESS RULES
                //ONLY FOR CONSULANTS, Sales support can choose in the list


                debug = "4";

                //if consultant, for every product in grid
                //get array of product ids from the grid

                bool isConsultant = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.IsConsultant();
                
                //if (isConsultant)
                //{
                    foreach (DataRow row in productList.Rows)
                    {

                        int productId = Convert.ToInt32(row["ScratchbookID"]);
                        ScratchBook sb = ScratchBook.GetScratchBookByID(productId);
                        int productQty = Convert.ToInt32(row["NbItems"]);
                        debug = "6";
                        //get row from productclassTotal for current productclass
                        DataRow[] rows = classTotals.Select("ProductClassID=" + sb.ProductClassId);
                        debug = "61" + sb.ProductClassId.ToString();
                        string debugMsg = rows[0].ToString();
                        int classQty = Convert.ToInt32(rows[0]["Quantity"]);
                        debug = "62";
                        //get package id for product
                        int packageQTy = Convert.ToInt32(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(packageTotals, "PackageID", "Quantity", sb.PackageId.ToString()));
                        //sent qty to get profit rate

                        string ruleBaseLevel = "";
                        setProfit = ManageSaleScreen.GetProfitRate(productId, productQty, classQty, packageQTy, ref ruleBaseLevel);

                        if (ruleBaseLevel == "Product")
                        {
                            Items1.ResetProfitPercentage(productId, setProfit);
                        }
                        else if (ruleBaseLevel == "Package")
                        {
                            Items1.ResetAllPackageProfitPercentage(sb.PackageId, setProfit);
                        }
                        else //product class
                        {
                            Items1.ResetAllProfitPercentage(sb.ProductClassId, setProfit);
                        }
                        //calculate a 2nd time with new profits
                        nbProductClass = 0;

                    }
                    error = Items1.CalculateTotals(validate, ref nbProductClass, ref productList);
                //}


                DateTime schDelDate = SaleDates1.ScheduledDeliveryDate;
                DateTime paymentDueDate = SaleDates1.PaymentDueDate;
                DateTime schShipDate = SaleDates1.ScheduledShipDate;
                //initialize shipDate for FC 
                if (ManageSaleScreen.IsOnlyConsultant())
                {
                    schShipDate = DateTime.Now;
                }


                
                Comments comment = Comments.GetCommentBySaleIDAndLeadID(saleId);
                if (comment != null)
                {
                    CommentTextBox.Text = comment.Comment;
                }
                //set calculated schedule ship date except for confirmed sales
                //based on date entered

                int paymentTermID = PaymentOptions1.PaymentTermID;

                //dates cannot change for a confimred sale (for anybody)
                if (ss.Description != "Confirmed") // || !(ManageSaleScreen.IsOnlyConsultant))
                {
                    //want to know what date came in, to know if we display a messaghhe stating the date has been changed
                    //not used
                    DateTime originalSchShip = schShipDate;
                    DateTime originalSchDel = schDelDate;


                    //Figure out the dates
                    if (productList.Rows.Count > 0)
                    {
                        string errorMsg = ManageSaleScreen.CalculateNewDatesForProductList(ref schShipDate, ref schDelDate, ref paymentDueDate, paymentTermID, productList, SaleInfo1.SaleID, clientId, clientSeq);
                        if (errorMsg != "")
                        {
                            error = true;
                            //ModalLabel.Text = errorMsg;
                            //pnlModal_ModalPopupExtender.Show();
                            errorLabel.Text = errorMsg;
                            errorLabel.Visible = true;
                        }
                        else
                        {
                            SaleDates1.ScheduledDeliveryDate = schDelDate;
                            SaleDates1.ScheduledShipDate = schShipDate;
                            SaleDates1.PaymentDueDate = paymentDueDate;
                        }
                    }
                }
                //set confirmed date
                if (SaleStatus1.SaleStatusID == 2 && SaleDates1.ConfirmedDate == DateTime.MinValue)//confirmed
                {
                    SaleDates1.ConfirmedDate = DateTime.Now;
                }
                /*else
                {
                    SaleDates1.ConfirmedDate = DateTime.MinValue;
                }*/




            }
            catch (Exception exception)
            {
                Logger.Error("Sales Screen: Recalculate. Debug=" + debug, exception);
            }


            return error;

        }

        //This function will split a sale for new products only
        private bool SaveCurrentSale()
        {
            bool error = false;
            try
            {

                //get sale

                SaleCollection saleCol = GetSaleCollectionFromDataGrid(this.Items1.GetDataFromDatagrid(), true);
                //check if more than one sale (product class)
                if (saleCol.Count > 1)
                {
                    //ModalLabel.Text = "You cannot modify a sale with products from different product class";
                    //pnlModal_ModalPopupExtender.Show();
                    error = true;
                    //RegisterStartupScript("myAlert", "<script>alert('You cannot modify a sale with products from different product class.')</script>");
                }
                else
                {

                    foreach (Sale sale in saleCol)
                    {

                        //verify if actually delivery date has been changes
                        if (SaleDates1.ActualDeliveryDate != null)
                        {
                            //procceed with update actually delivery date
                            sale.ScheduledDeliveryDate = SaleDates1.ScheduledDeliveryDate;
                            sale.Update();
                            //LogSimple.LogInfo("Sales Screen: Sale Updated With Actual Delivery Date" + sale);
                        }
                        else { }

                        //IF SALE IS ALREADY PROCESSED, CAN ONLY MODIFY THE CANCEL STATUS
                        if ((QSPIdTextBox.Text != "" && QSPIdTextBox.Text != "0"))
                        {
                            if (SaleStatus1.SaleStatusID == 4)
                            {
                                //procceed with update
                                sale.Update();
                                Logger.Debug("Sales Screen: Sale Updated " + sale);

                            }

                        }
                        else
                        {





                            //create array of sales with tax, shipping and comment (for OE order entry)
                            DataTable dtSaleTaxAndShip = new DataTable();
                            DataRow dr = dtSaleTaxAndShip.NewRow();
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("SaleID"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("Shipping"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("GST"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("GSTRate"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("PST"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("PSTRate"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("HST"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("HSTRate"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("Comment"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("Discount"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("DiscountReasonID"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("Surcharge"));
                            dtSaleTaxAndShip.Columns.Add(new DataColumn("SurchargeReasonID"));

                            decimal discount = Items1.Discount;
                            decimal surcharge = Items1.Surcharge;

                            dr["SaleID"] = sale.SalesId;
                            dr["Shipping"] = sale.ShippingFees;
                            dr["GST"] = 0;
                            dr["PST"] = 0;
                            dr["HST"] = 0;
                            dr["Discount"] = discount;
                            dr["Surcharge"] = surcharge;
                            dr["DiscountReasonID"] = Items1.DiscountReasonId;
                            dr["SurchargeReasonID"] = Items1.SurchargeReasonId;
                            dr["GSTRate"] = ManageSaleScreen.GetGST(clientId, clientSeq);
                            dr["PSTRate"] = ManageSaleScreen.GetPST(clientId, clientSeq);
                            dr["HSTRate"] = ManageSaleScreen.GetHST(clientId, clientSeq);



                            //TODO transaction
                            System.Collections.ArrayList saleItems = sale.SalesItems;

                            //insert taxes
                            System.Collections.ArrayList taxes = sale.ApplicableTaxes;

                            //PUT EACH tac back to 0
                            if (taxes.Count > 0)
                            {
                                ApplicableTax[] t = ApplicableTax.GetApplicableTaxByID(sale.SalesId);
                                foreach (ApplicableTax tax in t)
                                {
                                    tax.TaxAmount = 0;
                                    tax.Update();
                                }
                            }



                            //CODE TO INSERT OR UPDATE ADJUSTMENT
                            decimal pst = 0;
                            decimal gst = 0;
                            decimal hst = 0;

                            System.Collections.ArrayList items = sale.SalesItems;

                            efundraising.EFundraisingCRM.Linq.Adjustment adjustDIS = new efundraising.EFundraisingCRM.Linq.Adjustment();

                            if (discount != 0)
                            {

                                SalesItem item = (SalesItem)items[0];
                                if (!ManageSaleScreen.IsProductTaxExempt(item.ScratchBookId))
                                {
                                    ManageSaleScreen.CalculateTaxes(discount, sale.ClientId, sale.ClientSequenceCode, ref hst, ref pst, ref gst);
                                }

                                adjustDIS.Adjustment_On_Taxes = Convert.ToDecimal(pst + gst + hst);
                                adjustDIS.Adjustment_On_Sale_Amount = discount;
                                adjustDIS.Adjustment_Amount = Convert.ToDecimal(adjustDIS.Adjustment_On_Sale_Amount);
                                adjustDIS.Adjustment_Date = DateTime.Now;
                                adjustDIS.charge_id = 13; //Compensation
                                adjustDIS.Sales_ID = sale.SalesId;
                                adjustDIS.Adjustment_No = 1;
                                adjustDIS.Reason_ID = 9;
                                ManageSaleScreen.SubmitAdjustmentToDb(adjustDIS);
                            }

                            if (surcharge != 0)
                            {
                                SalesItem item = (SalesItem)items[0];
                                if (!ManageSaleScreen.IsProductTaxExempt(item.ScratchBookId))
                                {
                                    ManageSaleScreen.CalculateTaxes(surcharge, sale.ClientId, sale.ClientSequenceCode, ref hst, ref pst, ref gst);
                                }

                                adjustDIS.Adjustment_On_Taxes = Convert.ToDecimal(pst + gst + hst);
                                adjustDIS.Adjustment_On_Sale_Amount = surcharge;
                                adjustDIS.Adjustment_Amount = Convert.ToDecimal(adjustDIS.Adjustment_On_Sale_Amount);
                                adjustDIS.Adjustment_Date = DateTime.Now;
                                adjustDIS.charge_id = 18; //Surcharge Price Adjustment
                                adjustDIS.Sales_ID = sale.SalesId;
                                adjustDIS.Adjustment_No = 2;
                                adjustDIS.Reason_ID = 33;
                                ManageSaleScreen.SubmitAdjustmentToDb(adjustDIS);
                            }






                            foreach (ApplicableTax tax in taxes)
                            {
                                tax.SalesID = sale.SalesId;
                                if (tax.TaxAmount > 0)
                                {


                                    if (tax.TaxCode == "PST" || tax.TaxCode == "HST" || tax.TaxCode == "GST")
                                    {
                                        //tax.TaxAmount += adjPst;  //+ is really a minus                          
                                        dr["PST"] = tax.TaxAmount;
                                    }
                                    else if (tax.TaxCode == "HST")
                                    {
                                        //tax.TaxAmount += adjGst;                               
                                        dr["HST"] = tax.TaxAmount;
                                    }
                                    else
                                    {
                                        dr["GST"] = tax.TaxAmount;
                                    }

                                    //check if tax exists
                                    try //QUICK FIX need to create proc
                                    {
                                        tax.Update();
                                    }
                                    catch (Exception x)
                                    {
                                        tax.Insert();
                                    }
                                }
                            }




                            //update comment
                            Comments comment = Comments.GetCommentBySaleIDAndLeadID(sale.SalesId);
                            if (comment != null)
                            {
                                comment.SalesID = sale.SalesId;
                                comment.LeadID = 0;  //0 indicates a salescreen unique comment
                                comment.Comment = CommentTextBox.Text;
                                comment.ConsultantID = sale.ConsultantId;
                                comment.Update();

                            }
                            else if (CommentTextBox.Text.Trim() != "") //Insert
                            {
                                comment = new Comments();
                                comment.SalesID = sale.SalesId;
                                comment.EntryDate = DateTime.Now;
                                comment.Comment = CommentTextBox.Text;
                                comment.LeadID = 0;  //0 indicates a salescreen unique comment
                                comment.ConsultantID = sale.ConsultantId;
                                comment.Insert();
                            }

                            if (comment == null)
                            {
                                dr["Comment"] = "";
                            }
                            else
                            {
                                dr["Comment"] = comment.Comment;
                            }


                            foreach (SalesItem saleItem in sale.SalesItems)
                            {

                                int saleItemNo = saleItem.SalesItemNo;
                                if (saleItemNo > 0)
                                {
                                    saleItem.Update();
                                }
                                else
                                {
                                    //doublecheck if product already exists in the sale
                                    SalesItem[] sItems = SalesItem.GetSalesItemsBySaleID(sale.SalesId);
                                    bool found = false;
                                    foreach (SalesItem si in sItems)
                                    {
                                        if (si.ScratchBookId == saleItem.ScratchBookId)
                                        {
                                            found = true;
                                        }

                                    }
                                    if (!(found))
                                    {
                                        saleItem.Insert();
                                    }
                                    else
                                    {
                                        //product already exists
                                        Logger.Error("Sales Screen: Tries to insert duplicate product while saving sale " + sale.SalesId);
                                    }
                                }

                            }


                            dtSaleTaxAndShip.Rows.Add(dr);
                            Session[Global.SessionVariables.SALES_TAX_AND_SHIP] = dtSaleTaxAndShip;

                            sale.POConComm = SaleInfo1.POConComm;
                            sale.Update();
                            Logger.Debug("Sales Screen: Sale Updated " + sale);
                        }


                    }//for each sale
                } //if

            }
            catch (Exception exception)
            {
                Logger.Error("Sales Screen: Save current Sale", exception);
                //ModalLabel.Text = "An error occured while saving the sale.";
                //pnlModal_ModalPopupExtender.Show();
                //errorLabel.Text = "An error occured while saving the sale.";
                //errorLabel.Visible = true;
                error = true;
            }


            return error;
            //this.PrintPOButton.Text = string.Format("No of Sale: {0}", saleCol.Count);

        }

        private bool CreateNewSales(ref int saleID)
        {
            var itemCount = 0;
            bool error = false;
            try
            {
                SaleCollection saleCol = GetSaleCollectionFromDataGrid(this.Items1.GetDataFromDatagrid(), false);
                DataTable dtSaleTaxAndShip = new DataTable();

                dtSaleTaxAndShip.Columns.Add(new DataColumn("SaleID"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("Shipping"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("GST"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("GSTRate"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("PST"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("PSTRate"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("HST"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("HSTRate"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("Comment"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("Discount"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("DiscountReasonID"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("Surcharge"));
                dtSaleTaxAndShip.Columns.Add(new DataColumn("SurchargeReasonID"));

                //Sale sale = new Sale();

                foreach (Sale sale in saleCol)
                {
                    itemCount++; 

                sale.POConComm = SaleInfo1.POConComm;
                    var pclass = Session["pcIDForSurchage"].ToString();
                    //AUTO confirmation temp / except for t-shirts
                    if (pclass == "8")
                    {
                        sale.SalesStatusId = 2;
                        sale.ConfirmedDate = DateTime.Now;
                        Logger.Error("CreateNewSales pclass sale.SalesStatusId = 2 - " + pclass);
                    }
                    //AUTO confirmation temp / except for t - shirts end

                    if (itemCount <= 1)
                    {
                        saleID = sale.Insert();
                    }
                   
                    
                    DataRow dr = dtSaleTaxAndShip.NewRow();


                    decimal discount = Items1.Discount;
                    decimal surcharge = Items1.Surcharge;

                    dr["SaleID"] = sale.SalesId;
                    dr["Shipping"] = sale.ShippingFees;
                    dr["GST"] = 0;
                    dr["PST"] = 0;
                    dr["HST"] = 0;
                    dr["Comment"] = "";
                    dr["Discount"] = discount;
                    dr["Surcharge"] = surcharge;
                    dr["DiscountReasonID"] = Items1.DiscountReasonId;
                    dr["SurchargeReasonID"] = Items1.SurchargeReasonId;

                    /*  int clientID = Convert.ToInt32(Session[Global.SessionVariables.CLIENT_ID]);
                      string seq = Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();
                      dr["GSTRate"] = ManageSaleScreen.GetGST(clientId, seq);
                      dr["PSTRate"] = ManageSaleScreen.GetPST(clientId, seq);
                      */
                    dr["GSTRate"] = ManageSaleScreen.GetGST(clientId, clientSeq);
                    dr["PSTRate"] = ManageSaleScreen.GetPST(clientId, clientSeq);
                    dr["HSTRate"] = ManageSaleScreen.GetHST(clientId, clientSeq);



                    Logger.Debug("Sales Screen: Sale Updated" + sale);


                    SaleInfo1.SaleID = saleID;

                    //insert comment
                    if (CommentTextBox.Text.Trim() != "")
                    {
                        Comments comment = new Comments();
                        comment.SalesID = sale.SalesId;
                        comment.LeadID = 0;  //0 indicates a salescreen unique comment
                        comment.Comment = CommentTextBox.Text;
                        comment.EntryDate = DateTime.Now;
                        comment.ConsultantID = Convert.ToInt32(Session[Global.SessionVariables.CONSULTANT_ID]);
                        comment.Insert();
                        dr["Comment"] = comment.Comment;

                    }



                    //insert discount for sale
                    efundraising.EFundraisingCRM.Linq.Adjustment adjustDIS = new efundraising.EFundraisingCRM.Linq.Adjustment();
                    if (discount != 0)
                    {

                        decimal pst = 0;
                        decimal gst = 0;
                        decimal hst = 0;

                        System.Collections.ArrayList items = sale.SalesItems;
                        SalesItem item = (SalesItem)items[0];
                        if (!ManageSaleScreen.IsProductTaxExempt(item.ScratchBookId))
                        {
                            ManageSaleScreen.CalculateTaxes(discount, sale.ClientId, sale.ClientSequenceCode, ref hst, ref pst, ref gst);
                        }

                        ManageSaleScreen.SendMail("A Discount adjustment was created for this sale", sale.SalesId, Request.Url.Authority, "Xavier");

                        adjustDIS.Adjustment_On_Taxes = Convert.ToDecimal(pst + gst + hst);
                        adjustDIS.Adjustment_On_Sale_Amount = discount;
                        adjustDIS.Adjustment_Amount = Convert.ToDecimal(adjustDIS.Adjustment_On_Sale_Amount);
                        adjustDIS.Adjustment_Date = DateTime.Now;
                        adjustDIS.charge_id = Items1.DiscountReasonId;
                        adjustDIS.Sales_ID = sale.SalesId;
                        adjustDIS.Adjustment_No = 1;
                        adjustDIS.Reason_ID = 9;
                        ManageSaleScreen.SubmitAdjustmentToDb(adjustDIS);
                    }

                    //insert surcharge for sale
                    if (surcharge != 0)
                    {
                        decimal pst = 0;
                        decimal gst = 0;
                        decimal hst = 0;
                        System.Collections.ArrayList items = sale.SalesItems;
                        SalesItem item = (SalesItem)items[0];
                        if (!ManageSaleScreen.IsProductTaxExempt(item.ScratchBookId))
                        {
                            ManageSaleScreen.CalculateTaxes(surcharge, sale.ClientId, sale.ClientSequenceCode, ref hst, ref pst, ref gst);
                        }

                        ManageSaleScreen.SendMail("A Surcharge adjustment was created for this sale", sale.SalesId, Request.Url.Authority, "Xavier");

                        adjustDIS.Adjustment_On_Taxes = Convert.ToDecimal(pst + gst + hst);
                        adjustDIS.Adjustment_On_Sale_Amount = surcharge;
                        adjustDIS.Adjustment_Amount = Convert.ToDecimal(adjustDIS.Adjustment_On_Sale_Amount);
                        adjustDIS.Adjustment_Date = DateTime.Now;
                        adjustDIS.charge_id = Items1.SurchargeReasonId;
                        adjustDIS.Sales_ID = sale.SalesId;
                        adjustDIS.Adjustment_No = 2;
                        adjustDIS.Reason_ID = 33;
                        ManageSaleScreen.SubmitAdjustmentToDb(adjustDIS);
                    }



                    //insert taxes
                    System.Collections.ArrayList taxes = sale.ApplicableTaxes;
                    foreach (ApplicableTax tax in taxes)
                    {
                        tax.SalesID = saleID;
                        if (tax.TaxAmount > 0)
                        {
                            tax.Insert();
                            if (tax.TaxCode == "PST")
                            {
                                dr["PST"] = tax.TaxAmount;
                            }
                            else if (tax.TaxCode == "GST")
                            {
                                dr["GST"] = tax.TaxAmount;
                            }
                            else 
                            { 
                          
                                dr["HST"] = tax.TaxAmount;
                           
                            }
                        }
                    }

                    dtSaleTaxAndShip.Rows.Add(dr);
                    Session[Global.SessionVariables.SALES_TAX_AND_SHIP] = dtSaleTaxAndShip;

                    //TODO transaction
                    System.Collections.ArrayList saleItems = sale.SalesItems;


                    // insert sale items and store product class id to be used later for efundcard and OE processing
                    Session["PClassID"] = null;

                    foreach (SalesItem saleItem in saleItems)
                    {
                        saleItem.SalesId = saleID;
                        int saleItemNo = saleItem.SalesItemNo;
                        saleItem.Insert();
                        //Logger.Error("Inside SendNotificationEmailRules: " + saleID);
                        //if (saleItem.ProductClassId == 36)
                        //{
                        //    //send email notification for t-shirts order on hold
                        //    Logger.Error("Before SendNotificationEmailRules for T-shirts: " + saleID);
                        //    ServerXMLHTTP emailProcessing = new ServerXMLHTTP();
                        //        var strJSONToSend2 = "{ \"Type\":\"" + 20 +
                        //            "\",\"ExternalId\":\"" + sale.ClientId +
                        //            "\",\"Email\":\"fake@fake.com" +
                        //            "\",\"ExtraData\":\"1|" + sale.SalesId + "|" + sale.ClientSequenceCode +
                        //            "\"}";

                        //        var serverNotificationUrl = (ManageSaleScreen.GetValueFromWebConfig("core.webapi.host.notification", "value"));
                        //        var postDataBytes2 = Encoding.Default.GetBytes(strJSONToSend2);

                        //        emailProcessing.open("POST", serverNotificationUrl);
                        //        emailProcessing.setRequestHeader("Content-Type", "application/json");
                        //        emailProcessing.setRequestHeader("Content-Length", postDataBytes2.Length.ToString());
                        //        emailProcessing.send(postDataBytes2);
                        //    Logger.Error("END SendNotificationEmailRules for T-shirts: " + saleID);
                        //    //end

                        //}

                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error("Sales Screen: Create New Sale", ex);
                //ModalLabel.Text = "An error occured while saving the sale.";
                //pnlModal_ModalPopupExtender.Show();
                //errorLabel.Text = "An error occured while saving the sale.";
                //errorLabel.Visible = true;
                error = true;
            }
            return error;


            //this.PrintPOButton.Text = string.Format("No of Sale: {0}", saleCol.Count);
        }



        public SaleCollection GetSaleCollectionFromDataGrid(DataTable dtSaleItems, bool update)
        {

            SaleCollection saleCol = new SaleCollection();
            System.Collections.Hashtable hProductClass = new Hashtable();

            int productClassID_wfc = 0;
            int productClassID = 0;

            try
            {
                DataTable classTotals = (DataTable)Session[Global.SessionVariables.CLASS_TOTALS];
                if (dtSaleItems == null)
                    return null;

                SalesItemCollection dbSaleItems = null;
                //get items from databse if not new sale
                if (update)
                {
                    dbSaleItems = SalesItem.GetSalesItemsBySaleID_Collection(saleId);
                }

                System.Collections.Hashtable dbSalesID;

                foreach (DataRow row in dtSaleItems.Rows)
                {

                    Sale ss = new Sale();
                    int scratchBookId = System.Convert.ToInt32(row["ScratchbookID"]);

                    ScratchBook scrB = Components.Server.ManageProduct.GetScratchBookByID(scratchBookId, Session);
                    if (scrB != null)
                    {
                        //set the product class
                        productClassID = scrB.ProductClassId; //will beused for tax calculation for OER

                        //set the new ship date
                        string hour = SaleDates1.ScheduledShipTimeHour;
                        string min = SaleDates1.ScheduledShipTimeMin;
                        string pm = SaleDates1.ScheduledShipTimePM;
                        string sch = SaleDates1.ScheduledDeliveryDate.ToShortDateString() + " " + hour + ":" + min + " " + pm;

                        DateTime schShipDate = SaleDates1.ScheduledShipDate;
                        DateTime schDelDate = DateTime.Parse(sch);
                        DateTime paymentDueDate = DateTime.MinValue;


                        //split sale
                        if (hProductClass[scrB.ProductClassId] == null) //create a new sale with same info but remove items
                        {

                            //TAXES - Must Remove the taxes on the adjusmtent amount from the sale tax
                            double discount = Convert.ToDouble(Items1.Discount);
                            double surcharge = Convert.ToDouble(Items1.Surcharge);
                            /*decimal pstAdjustment = 0;
                            decimal gstAdjustment = 0;
                            if (Components.Server.ManageSaleScreen.IsSaleTaxExempt())
                            {
                                ManageSaleScreen.CalculateTaxes(Items1.Surcharge - Items1.Discount, clientId, clientSeq, ref pstAdjustment, ref gstAdjustment);
                            }*/

                           
                            ss = GetSale(update, "j7@3bv!009");

                            //GET SHIPPINGFEE for product class (hack to get shipping fee if there is a shipping fee stored in global session 
                            ss.ShippingFees = Items1.ShippingFees;
                               
                            
                         
                            //ss.ShippingFees = Convert.ToDecimal(Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalShipping", scrB.ProductClassId.ToString()));
                            //set taxes
                            decimal totalTax = 0;
                            ApplicableTax tax = new ApplicableTax();
                            tax.TaxAmount = Convert.ToDecimal(Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalGST", scrB.ProductClassId.ToString()));
                            // tax.TaxAmount -= gstAdjustment;
                            tax.TaxCode = "GST";
                            ss.ApplicableTaxes.Add(tax);
                            totalTax += tax.TaxAmount;
                            ApplicableTax tax2 = new ApplicableTax();
                            tax2.TaxAmount = Convert.ToDecimal(Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalPST", scrB.ProductClassId.ToString()));
                            // tax2.TaxAmount -= pstAdjustment;
                            tax2.TaxCode = "PST";
                            ss.ApplicableTaxes.Add(tax2);
                            totalTax += tax2.TaxAmount;
                            //tax3
                            ApplicableTax tax3 = new ApplicableTax();
                            tax3.TaxAmount = Convert.ToDecimal(Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalHST", scrB.ProductClassId.ToString()));
                            tax3.TaxCode = "HST";
                            ss.ApplicableTaxes.Add(tax3);
                            totalTax += tax3.TaxAmount;




                            ss.TotalAmount = surcharge - discount + Convert.ToDouble(totalTax) + Convert.ToDouble(ss.ShippingFees) + Convert.ToDouble(Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", scrB.ProductClassId.ToString()));
                            //ss.TotalAmount = Convert.ToDouble(totalTax)  + Convert.ToDouble(ss.ShippingFees) + Convert.ToDouble(Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", scrB.ProductClassId.ToString()));

                            ManageSaleScreen.CalculateNewDatesByProduct(ref schShipDate, ref schDelDate, ref paymentDueDate, short.MinValue, scratchBookId, ss.SalesId, clientId, clientSeq);

                            //set the totals for the product class
                            hProductClass[scrB.ProductClassId] = ss;
                        }
                        else
                        {
                            ss = new Sale();
                            ss = (Sale)hProductClass[scrB.ProductClassId];

                            //set new ship date if necessary
                            ManageSaleScreen.CalculateNewDatesByProduct(ref schShipDate, ref schDelDate, ref paymentDueDate, short.MinValue, scratchBookId, int.MinValue, clientId, clientSeq);


                        }

                        //newly calculated dates
                        ss.ScheduledShipDate = schShipDate;
                        ss.ScheduledDeliveryDate = schDelDate;
                        //add time
                        sch = schDelDate.ToShortDateString() + " " + hour + ":" + min + " " + pm;
                        ss.ScheduledDeliveryDate = DateTime.Parse(sch);



                        //if new item
                        string x = row["SalesItemId"].ToString();
                        if (x != "")
                        {

                            int saleItemNoCart = Convert.ToInt32(row["SalesItemId"]);
                            //for each items in db, look for same item in cart
                            foreach (SalesItem saleItem in dbSaleItems)
                            {
                                int saleItemNo = saleItem.SalesItemNo;
                                //use the list of db sale to compare with sales in cart, for deletion
                                //when delete, use sb id and intem no
                                dbSalesID = new System.Collections.Hashtable();
                                dbSalesID.Add(saleItem.ScratchBookId, saleItem.SalesItemNo);

                                if (saleItemNo == saleItemNoCart)
                                {
                                    saleItem.ProductClassId = Convert.ToInt16(productClassID);
                                    saleItem.ScratchBookId = Convert.ToInt32(row["ScratchbookID"]);
                                    saleItem.QuantitySold = Convert.ToInt32(row["Quantity"]);
                                    saleItem.QuantityFree = Convert.ToInt32(row["QuantityFree"]);
                                    saleItem.UnitPriceSold = Convert.ToDecimal(row["Price"].ToString().Remove(0, 1));
                                    saleItem.SalesAmount = Convert.ToDecimal(row["TotalAmount"].ToString().Remove(0, 1));
                                    saleItem.GroupName = row["GroupName"].ToString();
                                    //add item to sale
                                    ss.SalesItems.Add(saleItem);
                                }
                            }


                        }
                        else
                        {
                            SalesItem saleItem = new SalesItem();
                            saleItem.SalesId = SaleInfo1.SaleID;
                            saleItem.ProductClassId = Convert.ToInt16(productClassID);
                            saleItem.ScratchBookId = Convert.ToInt32(row["ScratchbookID"]);
                            saleItem.QuantitySold = Convert.ToInt32(row["Quantity"]);
                            saleItem.QuantityFree = Convert.ToInt32(row["QuantityFree"]);
                            saleItem.UnitPriceSold = Convert.ToDecimal(row["Price"].ToString().Remove(0, 1));
                            saleItem.SalesAmount = Convert.ToDecimal(row["TotalAmount"].ToString().Remove(0, 1));
                            saleItem.SponsorCommissionAmount = 0;
                            saleItem.SalesCommissionAmount = 0;
                            saleItem.AdjustedAmount = 0;
                            saleItem.PaidAmount = 0;
                            saleItem.GroupName = row["GroupName"].ToString();

                            //add item to sale
                            ss.SalesItems.Add(saleItem);

                        }

                    }
                }


                ProductClass[] productClasses = ProductClass.GetProductClasss();
                int wfcID = 0;
                int wfcStockID = 0;
                int wfcPEID = 0;
                int wfcCAID = 0;
                bool process = false;




                foreach (object prdKey in hProductClass.Keys)
                {
                    Sale ssss = hProductClass[prdKey] as Sale;
                    saleCol.Add(ssss);
                }
                Session[Global.SessionVariables.SALES_IN_CART] = hProductClass;


            }
            catch (Exception ex)
            {
                Logger.Error("Sales Screen: GetSaleCollectionFromDataGrid", ex);
            }


            return saleCol;
        }

        private Sale GetSale(bool update)
        {
            return GetSale(update, "");
        }

        private Sale GetSale(bool update, string passphrase)
        {
            Sale s = new Sale();

            try
            {
                //for an update get the sale from db
                if (update)
                {
                    s = Sale.GetSaleByID(SaleInfo1.SaleID, passphrase);
                }
                else
                {
                    int leadID = Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);
                    /*  int clientID = Convert.ToInt32(Session[Global.SessionVariables.CLIENT_ID]);
                      string clientSeqCode = Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();
                   
                      s = Components.Server.ManageSaleScreen.BuildDefaultSaleScreen(leadID, clientID, clientSeqCode);
                     */
                    s = Components.Server.ManageSaleScreen.BuildDefaultSaleScreen(leadID, clientId, clientSeq);
                }

                //update sale info with sale screen info
                s.ConsultantId = SaleInfo1.ConsulantID;
                s.PoStatusId = Convert.ToInt16(SaleInfo1.POStatusID);

                s.POConComm = SaleInfo1.POConComm;
              



                s.CarrierId = Convert.ToInt16(SaleInfo1.CarrierID);
                s.ShippingOptionId = Convert.ToInt16(SaleInfo1.ShipppingOptionID);
                s.WaybillNo = SaleInfo1.WaybillNo;
                s.SalesStatusId = SaleStatus1.SaleStatusID;


                s.ProductionStatusId = SaleStatus1.ProductionStatusID;
                s.ArStatusId = SaleStatus1.ArStatusID;

                s.SalesDate = SaleDates1.SaleDate;
                s.PaymentDueDate = SaleDates1.PaymentDueDate;
                s.ScheduledDeliveryDate = SaleDates1.ScheduledDeliveryDate;
                s.ScheduledShipDate = SaleDates1.ScheduledShipDate;
                s.ActualDeliveryDate = SaleDates1.ActualDeliveryDate;
                s.ActualShipDate = SaleDates1.ActualShipDate;
                s.BoxReturnDate = SaleDates1.BoxReturnDate;
                s.ReshipDate = SaleDates1.BoxReshipDate;
                s.ConfirmedDate = SaleDates1.ConfirmedDate;

                s.PaymentMethodId = PaymentOptions1.PaymentMethodID;
                s.PaymentTermId = PaymentOptions1.PaymentTermID;
                s.UpfrontPaymentMethodId = PaymentOptions1.DepositMethodID;
                s.UpfrontPaymentRequired = PaymentOptions1.SaleDepositPayment;





            }
            catch (Exception ex)
            {
                Logger.Error("Sales Screen: GetSale", ex);
            }


            return s;
        }


        protected void SaleButton_Click(object sender, System.EventArgs e)
        {



            errorLabel.Visible = false;
            try
            {

                //CHECK IF SALE IS DISABLED, IF SO, ONLY SAVE PAYMENT METHOD INFO

                /*    if (!ValidateSaleButton.Enabled)
                    {
                        efundraising.EFundraisingCRM.Sale s = efundraising.EFundraisingCRM.Sale.GetSaleByID(SaleInfo1.SaleID);
                        if (s != null)
                        {
                            s.PaymentMethodId = PaymentOptions1.PaymentMethodID;
                            s.PaymentTermId = PaymentOptions1.PaymentTermID;
                            s.UpfrontPaymentMethodId = PaymentOptions1.DepositMethodID;
                            s.UpfrontPaymentRequired = PaymentOptions1.SaleDepositPayment;
                            s.Update();
                        }
                    }
                    else
                    {*/



                string errorMsg = "";
                //  int clientID = Convert.ToInt32(Session[Global.SessionVariables.CLIENT_ID]);
                // string clientSeqCode = Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();
                int saleId = SaleInfo1.SaleID;

                //check if canadian sale (no order express)
                //  ClientAddress address = ClientAddress.GetClientAddressByIdSequenceAddressType(clientID, clientSeqCode, "BT");
                ClientAddress address = ClientAddress.GetClientAddressByIdSequenceAddressType(clientId, clientSeq, "BT");

                if (SaleIsValidated || !ValidateSaleButton.Enabled)
                {
                    Items1.Refresh();


                    bool error = Recalculate(true);
                    Logger.Debug("Recalculating...");

                    if (!(error))
                    {
                        if (saleId > 0)
                        {
                            bool wasSaleConfirmed = WasSaleConfirmed();
                            Logger.Info("Sale :" + saleId + " Saving existing sale");
                            error = SaveCurrentSale();

                            if (!error)
                            {

                                //checking stored session id for product class to be used  for efundcard and intiate OE processing
                                if (Session["PClassID"] != null)
                                {
                                    int pclassid = Convert.ToInt32(Session["PClassID"]);
                                    if (pclassid == 39)
                                    {
                                        Logger.Info("Sale :" + saleId + " Calling Order Express for efundscard order");
                                        bool warning = false;
                                        error = OrderExpress(ref warning);

                                    }

                                }


                               



                                //update production status of sale
                                Sale s = Sale.GetSaleByID(saleId);


                                SaleStatus1.ProductionStatusID = s.ProductionStatusId;
                                if (!(error))
                                {
                                    Logger.Info("Sale :" + saleId + " Sale Updated");
                                    //string msg = ModalLabel.Text;


                                    //send client confirmation sale email if sale confirmed
                                    //if (s.SalesStatusId == 2)
                                    //{
                                    //    //send confirmation email after sale is insert and sale is confirmed
                                    //    var client = new HttpClient();
                                    //    var notification = new { Id = s.ClientId, Type = 5, Email = "fake@fake.com", ExtraData = "1" };

                                    //    try
                                    //    {
                                    //        var reponse = client.PostAsJsonAsync("https://test-core.fundraising.com/notifications", notification).Result;
                                    //        Logger.Error("api call response status in save sale -  " + reponse.IsSuccessStatusCode + " - " + reponse.ReasonPhrase);
                                    //        {
                                    //            //
                                    //        }
                                    //    }
                                    //    catch (Exception ex)
                                    //    {
                                    //        throw ex;
                                    //    }

                                    //}


                                    Redirect("~/Sales/SalesScreen/NewSales.aspx?sid=" + saleId);
                                    //Redirect("~/Sales/SalesScreen/NewSales.aspx?sid=" + saleId + "&msg=" + msg);

                                }
                                else
                                {
                                    //error already displayed from ORderExpress()
                                    //OE                                            // ModalLabel.Text = "Error editing the sale";
                                    // pnlModal_ModalPopupExtender.Show();
                                    //RegisterStartupScript("myAlert", "<script>alert('Error editing the sale.')</script>");
                                    Logger.Info("Sale :" + saleId + " Saving existing sale FAILED");

                                }


                                






                            }
                            else
                            {
                                Logger.Info("Sale :" + saleId + " Error saving existing sale");

                            }
                        }
                        else
                        {
                            // New Sales
                            Logger.Debug("Creating new sale");
                            error = CreateNewSales(ref saleId);
                            if (!error)
                            {
                                //checking stored session id for product class to be used  for efundcard and intiate OE processing
                                if (Session["PClassID"] != null)
                                {
                                    int pclassid = Convert.ToInt32(Session["PClassID"]);
                                    if (pclassid == 39)
                                    {
                                        Logger.Debug("Sale :" + saleId + " Calling Order Express for efundscard order");
                                        bool warning = false;
                                        error = OrderExpress(ref warning);

                                    }

                                }

                                //create activity
                                ClientActivity ca = new ClientActivity();
                                //get default activity from webconfig
                                short clientActivityID = Convert.ToInt16(efundraising.Configuration.ApplicationSettings.GetConfig()["NewClientActivityIDAfterSale", "default"]);
                                //  ca.ClientId = clientID;
                                // ca.ClientSequenceCode = clientSeqCode;
                                ca.ClientId = clientId;
                                ca.ClientSequenceCode = clientSeq;
                                ca.ClientActivityTypeId = clientActivityID;
                                ca.ClientActivityDate = DateTime.Now.AddMonths(9); //next fundraiser
                                ca.Insert();

                                //refresh items
                                if (!error)
                                {
                                    Logger.Debug("Sale :" + saleId + " Sale created successfully");
                                    //ModalLabel.Text = "Sale created successfully";
                                    //pnlModal_ModalPopupExtender.Show();
                                    //RegisterStartupScript("myAlert", "<script>alert('Sale created successfully.')</script>");
                                    if (!error) //(!warning)
                                    {
                                        Redirect("~/Sales/SalesScreen/NewSales.aspx?sid=" + saleId);
                                    }
                                }
                                else
                                {
                                    //OE                                            //error already displayed from ORderExpress()
                                    //LogSimple.LogInfo("Saving new sale FAILED");
                                    //pnlModal_ModalPopupExtender.Show();
                                    //RegisterStartupScript("myAlert", "<script>alert('Error creating sale.')</script>");


                                }

                            }
                            else
                            {
                                Logger.Debug("Error creating sale");

                            }
                        }
                    }

                }
                else
                {
                    //ModalLabel.Text = "Please click Validate before Saving";
                    //pnlModal_ModalPopupExtender.Show();
                    //RegisterStartupScript("myAlert", "<script>alert('Please click Validate before Saving.')</script>");
                }
                // }//Sale not enabled
            }
            catch (Exception ex)
            {
                Logger.Error("Sales Screen: SaleButtonClick", ex);
                //ModalLabel.Text = "Error saving the sale";
                //pnlModal_ModalPopupExtender.Show();
            }
        }


        private bool OrderExpress(ref bool warning)
        {


            bool error = false;
            try
            {


                string errorMsg = "";
                // int clientID = Convert.ToInt32(Session[Global.SessionVariables.CLIENT_ID]);
                // string clientSeqCode = Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();

                bool canConfirm = false;
                if (ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_SaleSupport) ||
                    ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_IT))
                {
                    canConfirm = true;
                }

                //NO MORE --> check is sale is confirmed for the first time, never process by web service, production status TODO and is Sale Support
                if (Session[Global.SessionVariables.SALES_IN_CART] != null)
                {
                    Hashtable salesInCart = (Hashtable)Session[Global.SessionVariables.SALES_IN_CART];

                    foreach (object key in salesInCart.Keys)
                    {
                        Sale saleInCart = salesInCart[key] as Sale;

                        //set web config values
                        Hashtable hWebConfig = new Hashtable();
                        hWebConfig["Form"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "form");
                        hWebConfig["ChargeTo"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "chargeTo");
                        hWebConfig["CatalogID"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "catalogID");
                        hWebConfig["businessDivisionID"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "businessDivisionID");
                        hWebConfig["createUserID"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "createUserID");
                        hWebConfig["orderTypeID"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "orderTypeID");
                        hWebConfig["startDate"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "startDate");
                        hWebConfig["endDate"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "endDate");
                        hWebConfig["fiscalYear"] = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "fiscalYear");
                        bool isProd = Convert.ToBoolean(ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.Production", "isProduction"));
                        if (isProd)
                        {
                            hWebConfig["isProd"] = "1";
                            hWebConfig["qspConnStr"] = ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString;
                        }
                        else
                        {
                            hWebConfig["isProd"] = "0";
                            hWebConfig["qspConnStr"] = ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString;
                        }

                        //OrderExpressClient.OEPlaceOrderEntryPoint oe = new OrderExpressClient.OEPlaceOrderEntryPoint();

                        //to be safe block access if in dev
                        //if (isProd)
                        //{
                        DataTable dt = (DataTable)Session[Global.SessionVariables.SALES_TAX_AND_SHIP];

                        Logger.Debug("Sale :" + saleInCart.SalesId + " Calling Place Order");
                        int cid = Convert.ToInt32(Session[Global.SessionVariables.CONSULTANT_ID]);

                        int as400ID = 0;
                        //if (QSPIdTextBox.Text != "")
                        //{
                        //    as400ID = Convert.ToInt32(QSPIdTextBox.Text);
                        //}

                        ////if IsProcessed, the status must be To be cancelled to call OE
                        //if (as400ID == 0 || SaleStatus1.SaleStatusID == 4)
                        //{
                        //    errorMsg = oe.PlaceOrder(saleInCart, clientId, clientSeq, hWebConfig, dt, cid, as400ID);
                        //}

                        //display warning if sale is on as400 aleady
                        /**    try
                            {
                                if (as400ID > 0)
                                {
                                    ModalLabel.Text = "Please be aware that only the Status of this order will be transmitted to as400";
                                }
                            }catch(Exception x){
                                string msg = x.Message;
                            }*/


                        if (errorMsg == "No OE - WFC" || errorMsg == "No OE - Sample")
                        {
                            if (IsSaleConfirmed())
                            {
                                warning = true;
                                //errorLabel.Visible = true;
                                if (errorMsg == "No OE - Sample")
                                {
                                    //ModalLabel.Text = "This sale contains sample items that will have to be processed by Production";
                                }
                                else
                                {
                                    //ModalLabel.Text = "This sale contains WFC items that will have to be processed by Production";
                                }

                            }
                        }
                        else if (errorMsg != "")
                        {
                            error = true;
                            //errorLabel.Visible = true;
                            //pnlModal_ModalPopupExtender.Show();
                            Logger.Error(errorMsg);


                            int warningPos = errorMsg.ToUpper().IndexOf("WARNING");
                            if (warningPos == -1)
                            {
                                ManageSaleScreen.SendMail(errorMsg, saleInCart.SalesId, Request.Url.Authority, "SaleSupport");
                                Logger.Error(errorMsg);
                            }


                            if (errorMsg.Length > 80)
                            {
                                //ModalLabel.Text = errorLabel.Text + errorMsg.Substring(0, 60);
                                //errorLabel.Text = errorLabel.Text + errorMsg.Substring(0, 80);
                            }
                            else
                            {
                                //ModalLabel.Text = errorLabel.Text + errorMsg;
                                //errorLabel.Text = errorLabel.Text + errorMsg;
                            }

                        }




                    }
                    ///////////////////////////
                    ///
                }

            }



            catch (Exception ex)
            {
                Logger.Error("Sales Screen: OrderExpress()", ex);
            }
            return error;
        }


        protected void ValidateSaleButton_Click(object sender, System.EventArgs e)
        {
            int debug = 1;
            try
            {

                bool error = false;
                int nbProductClass = 0;
                Items1.Refresh();


                DateTime actualShipDate = SaleDates1.ActualShipDate;

                //added code for PVF prepack -- select appropriate surcharge from dropdown
                if (Session["AddSurcharge"] == "active")
                {
                    Items1.SurchargeReasonId = 4;

                }

                if (Items1.DiscountReasonId == int.MinValue && Items1.Discount > 0)
                {
                    error = true;
                    //ModalLabel.Text = "This sale has a discount with no reason attached to it";
                    //pnlModal_ModalPopupExtender.Show();
                    //RegisterStartupScript("myAlert", "<script>alert('This sale has a discount with no reason attached to it.')</script>");
                }



                if (Items1.SurchargeReasonId == int.MinValue && Items1.Surcharge > 0)
                {
                    error = true;
                    //ModalLabel.Text = "This sale has a surcharge with no reason attached to it";
                    //pnlModal_ModalPopupExtender.Show();
                    //RegisterStartupScript("myAlert", "<script>alert('This sale has a surcharge with no reason attached to it.')</script>");
                }

                if (!error)
                {
                    debug = 2;
                    error = Recalculate(true, ref nbProductClass);
                    debug = 3;
                }

                if (nbProductClass > 1)
                {
                    //ModalLabel.Text = "This sale contains products from different classes. The sale will be split";
                    //pnlModal_ModalPopupExtender.Show();
                    //RegisterStartupScript("myAlert", "<script>alert('This sale contains products from different classes. The sale will be split.')</script>");
                }

                if (!(error) && nbProductClass > 0)
                {
                    SaveButton.Enabled = true;
                }
                else
                {
                    SaveButton.Enabled = false;
                }

                SaleIsValidated = true;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Index was outside the bounds of the array.")
                {
                    Logger.Error("Sales Screen: ValidateSaleButton. Debug=" + debug.ToString(), ex);
                }
                else
                {
                    Logger.Error("Sales Screen: ValidateSaleButton", ex);
                }

            }


        }

        protected void BackButton_Click(object sender, System.EventArgs e)
        {
            if (clientId != int.MinValue && clientSeq != null)
            {
                Redirect("~/Sales/SalesScreen/Default.aspx?clid=" + clientId + "&seq=" + clientSeq);
            }
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            SaleStatus1.ProductionStatusID = 1;
            RegisterStartupScript("myAlert", "<script>alert('Sale updated.')</script>");

        }

        protected void CreditCheckButton_Click(object sender, System.EventArgs e)
        {
            //calculate toal so that we can use ClassTotal
            Recalculate(false);

            string host = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "Host");

            if (Request.Url.Host.StartsWith("localhost"))
            {
                host = Request.Url.Host + ":" + Request.Url.Port;

            }

            int leadID = Convert.ToInt32(Session[Global.SessionVariables.LEAD_ID]);
            string credit = "http://" + host + "/Sales/SalesScreen/CreditCheckSA.aspx?lid=" + leadID + "&sid=" + SaleInfo1.SaleID + "&pmid=" + PaymentOptions1.PaymentMethodID;
            string script = "<script language='javascript'>window.open('" + credit + "','Streaming', 'width=700, height=500, location=no, menubar=no, status=no, toolbar=no, scrollbars=no, resizable=yes')</script>";
            Page.RegisterClientScriptBlock("Open", script);
            //Server.Transfer(script);			

            //Redirect("~/Sales/SalesScreen/CreditCheck.aspx?sid=" + SaleInfo1.SaleID + "&pmid=" + PaymentOptions1.PaymentMethodID + "&lid=" + leadID);
        }




        private bool SaleIsValidated
        {
            get
            {
                return Convert.ToBoolean(ViewState["saleIsValidated"]);
            }
            set
            {
                ViewState["saleIsValidated"] = value;
            }

        }

        private bool IsPOConfirmed()
        {
            bool confirmed = false;

            //check if po is confirmed
            int poStatusID = SaleInfo1.POStatusID;
            PoStatus ps = PoStatus.GetPoStatusByID(poStatusID);
            if (ps.Description == "Confirmed")
            {
                confirmed = true;
            }


            return confirmed;

        }

        private bool IsSaleConfirmed()
        {
            bool confirmed = false;
            try
            {
                //check if sale is confirmed
                int saleStatusID = SaleStatus1.SaleStatusID;
                if (saleStatusID != int.MinValue)
                {
                    SalesStatus ss = SalesStatus.GetSalesStatusByID(saleStatusID);
                    if (ss.Description == "Confirmed")
                    {
                        confirmed = true;
                    }
                }

            }
            catch (Exception x)
            {
                int cid = Convert.ToInt32(Session[Global.SessionVariables.CONSULTANT_ID]);
                Consultant c = Consultant.GetConsultantByID(cid);
                Logger.Error(c.Name + ": Sales screen: IsSaleConfirmed(). saleStatusID=" + SaleStatus1.SaleStatusID.ToString(), x);
            }


            return confirmed;

        }

        private bool WasSaleConfirmed()
        {
            bool confirmed = false;


            if (saleId != int.MinValue)
            {
                Sale s = Sale.GetSaleByID(saleId);
                SalesStatus ss = SalesStatus.GetSalesStatusByID(s.SalesStatusId);
                if (ss.Description == "Confirmed")
                {
                    confirmed = true;
                }
            }

            return confirmed;

        }

        private bool WasPOConfirmed()
        {
            bool confirmed = false;

            if (saleId != int.MinValue)
            {
                Sale s = Sale.GetSaleByID(saleId);
                if (s.PoStatusId != short.MinValue)
                {
                    PoStatus ps = PoStatus.GetPoStatusByID(s.PoStatusId);
                    if (ps.Description == "Confirmed")
                    {
                        confirmed = true;
                    }
                }
            }

            return confirmed;

        }

        private void SetRights()
        {
            try
            {
                if (SaleIsValidated)
                {
                    SaveButton.Enabled = true;
                }
                else
                {
                    SaveButton.Enabled = false;
                }

                string status = "";
                if (saleId > 0)
                {
                    Sale s = Sale.GetSaleByID(saleId);
                    //set External ids
                    ////OE                    if (s.ExtOrderID != null && s.ExtOrderID != int.MinValue)
                    //                    {
                    //                        OEIdTextBox.Text = s.ExtOrderID.ToString();
                    //                        status = GetOEStatus(s.ExtOrderID);
                    //                    }
                }


                bool isProcessed = true;
                bool confirmed = IsSaleConfirmed();
                // if (status == "Wait Appr." || status == "In process" || status == ""){
                if (QSPIdTextBox.Text == "" || QSPIdTextBox.Text == "0")
                {
                    isProcessed = false;
                }


                bool isAdmin = ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_SaleScreenAdmin);

                int cid = Convert.ToInt32(Session[Global.SessionVariables.CONSULTANT_ID]);

                bool isConsultant = ManageSaleScreen.IsConsultant();
                bool isSaleSupport = ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_SaleSupport);
                bool isMIS = Components.Server.ManageSaleScreen.IsMIS();

                if (ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_Accounting) ||
                   ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_IT))
                {
                    RefundButton.Visible = true;
                }


                //SALE IS PICK UP BY AS400--> FCs cannot do anything
                /*if (confirmed && isProcessed && !(isMIS) && !(isSaleSupport))
                {
                    SaleDates1.DisableEverything();
                    SaleStatus1.DisableEverything();
                    SaleInfo1.DisableEverything();
                    PaymentOptions1.DisableEverything();
                    Items1.DisableEverything();
                    CreditCheckButton.Enabled = false;
                    ValidateSaleButton.Enabled = false;
                    SaveButton.Enabled = false;

                } //SALE IS CONFIRMED, Fcs CANNOT TOUCH IT
                else */
                if ((confirmed || isProcessed) && !(isMIS) && !(isSaleSupport))
                {
                    SaleDates1.DisableEverything();
                    SaleStatus1.DisableEverything();
                    SaleInfo1.DisableEverything();
                    SaleInfo1.DisableForConsultants2();
                    //PaymentOptions1.DisableEverything();
                    Items1.DisableEverything();
                    CreditCheckButton.Enabled = false;
                    ValidateSaleButton.Enabled = false;
                    SaveButton.Enabled = true; //
                    SaleDates1.DisableForConsultants();


                }
                else if (ManageSaleScreen.IsOnlyConsultant())
                {

                    SaleDates1.DisableForConsultants();
                    SaleStatus1.DisableForConsultants();
                    SaleInfo1.DisableForConsultants();
                    Items1.DisableForConsultants();

                }
                //sale support can only change the status To cancel and Payments
                //the save button will make sure to update only if cancel was chosen
                else if (isProcessed) //&& isSaleSupport)
                {



                    SaleDates1.DisableEverything();
                    SaleStatus1.DisableEverything();
                    SaleInfo1.DisableEverything();
                    //PaymentOptions1.DisableEverything();
                    Items1.DisableEverything();
                    CreditCheckButton.Enabled = false;
                    ValidateSaleButton.Enabled = false;
                    SaveButton.Enabled = true;

                    DateTime ship = SaleDates1.ActualShipDate;
                    if (ship == null || ship == DateTime.MinValue)
                    {
                        SaleStatus1.EnableCancelStatus();
                    }

                }


                //ALLOW Admins TO Change Dssxcount and Surcharge
                if (!isProcessed && isAdmin)
                {
                    Items1.EnableDiscounts();

                }

            }
            catch (Exception ex)
            {
                Logger.Error("Sales screen: SetRights", ex);
            }


        }

        protected void PrintPOButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                string language = "";
                if (SaleInfo1 != null && SaleInfo1.SaleID != int.MinValue)
                {
                    var sale = Sale.GetSaleByID(SaleInfo1.SaleID);
                    if (sale == null)
                    {
                        throw new Exception(string.Format("Sale Id {0} not found", SaleInfo1.SaleID));
                    }
                    var clientAddress = ClientAddress.GetClientAddressByIdSequenceAddressType(sale.ClientId,
                        sale.ClientSequenceCode, "BT");
                    if (clientAddress == null)
                    {
                        throw new Exception(string.Format("Client Address not found. ClientId = {0}. Sequence Code={1}", SaleInfo1.SaleID, sale.ClientSequenceCode));
                    }
                    var invoice = "http://efrcrm.efrprod.com/Reports/Invoices/Invoice.aspx?sids=";

                    if (!string.IsNullOrEmpty(invoice))
                    {
                        if (clientAddress.CountryCode == "CA")
                        {
                            invoice = invoice + sale.SalesId + "&lg=EN&rn=InvoiceCA";
                        }
                        else
                        {
                            invoice = invoice + sale.SalesId + "&lg=EN";
                        }
                        //opens new window
                        Logger.Debug("About to Register Client Script Block to print PO.");
                        string script = "<script language='javascript'>window.open('" + invoice +
                                        "','Streaming', 'width=500, height=700, location=no, menubar=no, status=no, toolbar=no, scrollbars=no, resizable=yes')</script>";
                        //Page.RegisterClientScriptBlock("Open", script);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Open", script);
                    }
                    else
                    {
                        Logger.Error("Error In processing price quote from salesscreen");
                    }
                    
                }
                else
                {
                    throw new Exception("Saleinfo1 is null");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Sales Screen: Print PO", ex);
            }


        }

        //private string GetOEStatus(int extOrderID)
        //{
        //    int a = 0;
        //    return GetOEStatus(extOrderID, ref a);
        //}
        //private string GetOEStatus(int extOrderID, ref int fulfID)
        //{
        //    string status = "";

        //    //get status
        //    //
        //    ICriteria criteria = QSP.Business.Fulfillment.Order.CreateCriteria2();
        //    criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Order.OrderIdProperty, extOrderID));
        //    List<QSP.Business.Fulfillment.Order> orderList = QSP.Business.Fulfillment.Order.GetOrderList(criteria);
        //    if (orderList != null && orderList.Count > 0)
        //    {
        //        int statusID = orderList[0].OrderStatusId;
        //        fulfID = Convert.ToInt32(orderList[0].FulfOrderId);

        //        criteria = QSP.Business.Fulfillment.OrderStatus.CreateCriteria2();
        //        criteria.Add(Expression.Eq(QSP.Business.Fulfillment.OrderStatus.OrderStatusIdProperty, statusID));
        //        List<QSP.Business.Fulfillment.OrderStatus> orderStatusList = QSP.Business.Fulfillment.OrderStatus.GetOrderStatusList(criteria);
        //        if (orderStatusList != null && orderStatusList.Count > 0)
        //        {
        //            status = orderStatusList[0].OrderStatusName;

        //        }

        //    }

        //    return status;

        //}


        //private DateTime GetShipDate(int extOrderID)
        //{
        //    DateTime date = DateTime.MaxValue;

        //    //get status
        //    //
        //    ICriteria criteria = QSP.Business.Fulfillment.OrderDetail.CreateCriteria2();
        //    criteria.Add(Expression.Eq(QSP.Business.Fulfillment.OrderDetail.OrderIdProperty, extOrderID));
        //    List<QSP.Business.Fulfillment.OrderDetail> orderDetailList = QSP.Business.Fulfillment.OrderDetail.GetOrderDetailList(criteria);
        //    if (orderDetailList != null && orderDetailList.Count > 0)
        //    {
        //        //take ship date of first item only
        //        int shipmentGroupID = orderDetailList[0].ShipmentGroupId;

        //        criteria = QSP.Business.Fulfillment.ShipmentGroup.CreateCriteria2();
        //        criteria.Add(Expression.Eq(QSP.Business.Fulfillment.ShipmentGroup.ShipmentGroupIdProperty, shipmentGroupID));
        //        List<QSP.Business.Fulfillment.ShipmentGroup> shipmentGroupList = QSP.Business.Fulfillment.ShipmentGroup.GetShipmentGroupList(criteria);
        //        if (shipmentGroupList != null && shipmentGroupList.Count > 0)
        //        {
        //            date = Convert.ToDateTime(shipmentGroupList[0].ShipmentDate);

        //        }

        //    }

        //    return date;

        //}

        //OE
        //private string GetWaybillNo(int extOrderID)
        //{
        //    string waybill = "";

        //    //get status
        //    //
        //    ICriteria criteria = QSP.Business.Fulfillment.OrderDetail.CreateCriteria2();
        //    criteria.Add(Expression.Eq(QSP.Business.Fulfillment.OrderDetail.OrderIdProperty, extOrderID));
        //    List<QSP.Business.Fulfillment.OrderDetail> orderDetailList = QSP.Business.Fulfillment.OrderDetail.GetOrderDetailList(criteria);
        //    if (orderDetailList != null && orderDetailList.Count > 0)
        //    {
        //        //take waybill of first item only
        //        int shipmentGroupID = orderDetailList[0].ShipmentGroupId;

        //        criteria = QSP.Business.Fulfillment.ShipmentGroup.CreateCriteria2();
        //        criteria.Add(Expression.Eq(QSP.Business.Fulfillment.ShipmentGroup.ShipmentGroupIdProperty, shipmentGroupID));
        //        List<QSP.Business.Fulfillment.ShipmentGroup> shipmentGroupList = QSP.Business.Fulfillment.ShipmentGroup.GetShipmentGroupList(criteria);
        //        if (shipmentGroupList != null && shipmentGroupList.Count > 0)
        //        {
        //            if (shipmentGroupList[0].FulfShipmentTracking != null)
        //            {
        //                waybill = shipmentGroupList[0].FulfShipmentTracking.ToString();
        //            }

        //        }

        //    }

        //    return waybill;

        //}

        //private void GetDiscount(int orderID, ref decimal discount, ref decimal surcharge, ref int discountReasonID, ref int surchargeReasonID)
        //{

        //    //get status
        //    //
        //    ICriteria criteria = QSP.Business.Fulfillment.OrderCharge.CreateCriteria2();
        //    criteria.Add(Expression.Eq(QSP.Business.Fulfillment.OrderCharge.OrderIdProperty, orderID));
        //    List<QSP.Business.Fulfillment.OrderCharge> orderCharges = QSP.Business.Fulfillment.OrderCharge.GetOrderChargeList(criteria);
        //    foreach (OrderCharge orderCharge in orderCharges)
        //    {
        //        int chargeID = orderCharge.ChargeId;
        //        criteria = QSP.Business.Fulfillment.Charge.CreateCriteria2();
        //        criteria.Add(Expression.Eq(QSP.Business.Fulfillment.Charge.ChargeIdProperty, chargeID));
        //        List<QSP.Business.Fulfillment.Charge> charges = QSP.Business.Fulfillment.Charge.GetChargeList(criteria);
        //        if (charges != null && charges.Count > 0)
        //        {
        //            if (charges[0].IsCredit)
        //            {
        //                discount = -(Convert.ToDecimal(orderCharge.Amount));
        //                discountReasonID = chargeID;
        //            }
        //            else
        //            {
        //                surcharge = Convert.ToDecimal(orderCharge.Amount);
        //                surchargeReasonID = chargeID;
        //            }

        //        }


        //    }


        //}


        protected void RefundButton_Click(object sender, EventArgs e)
        {
            string host = ManageSaleScreen.GetValueFromWebConfig("OrderExpress", "Host");

            if (Request.Url.Host.StartsWith("localhost"))
            {
                host = Request.Url.Host + ":" + Request.Url.Port;

            }

            // int clientID = Convert.ToInt32(Session[Global.SessionVariables.CLIENT_ID]);
            ///string seq = Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();


            //  string credit = "http://" + host + "/Sales/CreditCard/Refund.aspx?clientID=" + clientID + "&clientSequenceCode=" + seq;
            string credit = "http://" + host + "/Sales/CreditCard/Refund.aspx?clientID=" + clientId + "&clientSequenceCode=" + clientSeq;
            string script = "<script language='javascript'>window.open('" + credit + "','Streaming', 'width=900, height=700, location=no, menubar=no, status=no, toolbar=no, scrollbars=yes, resizable=yes')</script>";
            Page.RegisterClientScriptBlock("Open", script);
        }

    }
}
