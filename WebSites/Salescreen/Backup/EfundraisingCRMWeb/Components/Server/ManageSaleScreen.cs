using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using efundraising.EFundraisingCRM;
using QSPForm.Business;
using System.Collections.Generic;
using System.Net.Mail;



namespace EFundraisingCRMWeb.Components.Server
{
    /// <summary>
    /// Summary description for ManageSaleScreen.
    /// </summary>
    public class ManageSaleScreen : System.Web.UI.Page
    {

        public enum Roles
        {
            gCAEFR_Intranet_IT,
            gCAEFR_Intranet_Production,
            gCAEFR_Intranet_Consultant,
            gCAEFR_Intranet_SaleSupport,
            gCAEFR_Intranet_Accounting,
            gCAEFR_Intranet_SaleScreenAdmin
        }


        private ManageSaleScreen()
        {
            //
            // TODO: Add constructor logic here
            //

        }
        public static void SendMail(string body, int saleID, string host, string dept)
        {

            string to = "";
            string cc = "";
            string bcc = "";

            SmtpClient smtpClient = new SmtpClient();
            MailMessage Mail = new MailMessage();

            Mail.From = new MailAddress("salescreen@qsp.com", "Sale Screen");

            if (dept == "Finance")
            {
                to = GetValueFromWebConfig("Common.Finance.Email.Warning", "TO");
                cc = GetValueFromWebConfig("Common.Finance.Email.Warning", "CC");
                
                Mail.Subject = "An update was made to a discount/surcharge that needs verified";
            }
            else if (dept == "Xavier")
            {
                to = GetValueFromWebConfig("Common.Finance.Email.Notify", "TO");
                Mail.Subject = "A discount/surcharge was made on a sale";
            }else
            {
                to = GetValueFromWebConfig("Common.OE.Email.Error", "TO");
                cc = GetValueFromWebConfig("Common.OE.Email.Error", "CC");
                bcc = GetValueFromWebConfig("Common.OE.Email.Error", "BCC");
                Mail.Subject = "An error occured creating sale on OE";
            }

            Mail.To.Add(to);
            if (cc != "")
            {
                Mail.CC.Add(cc);    
            }
            if (bcc != "")
            {
                Mail.CC.Add(bcc);
            }
                
            
            

            string url = "http://" + host+ "/launchPage.aspx?sid=" + saleID;
            // You can specify a plain text or HTML contents
            Mail.Body = body +
                "<br /> <br>" +
                "<a href=\'" + url + "'>Go To Sale</a>";
            
            //Mail.Body = "The FM for the sale 73227 does not exist on QSP<br /><a href=\'http://localhost:1316/Sales/SalesScreen/NewSales.aspx?sid=73227'> Go To Sale</a>";
            //Mail.Body = "B";

            Mail.IsBodyHtml = true;

            //smtpClient.Host = "outgoingsmtp";
            smtpClient.Send(Mail);

        }



        public static efundraising.EFundraisingCRM.Sale BuildDefaultSaleScreen(efundraising.EFundraisingCRM.Client cl)
        {
            if (cl == null)
                return null;

            Sale sale = null;
            Lead l = Lead.GetLeadByID(cl.LeadId);
            if (l != null)
            {
                // determine the billing company
                int billingCompany = BillingCompany.eFundraising_USA.BillingCompanyID;  // 1 is the default - efundraising USA
                if (l.PromotionId == 5953 || l.PromotionId == 5961)
                    billingCompany = BillingCompany.FR.BillingCompanyID;

                sale =
                    new Sale(int.MinValue,
                    l.ConsultantId,
                    short.MinValue,
                    1,
                    PaymentTerm.COD_30Days.PaymentTermId,
                    cl.ClientSequenceCode,
                    cl.ClientId,
                    SalesStatus.New.SalesStatusID, ManageProduct.GetPaymentMethodId("CHECK")
                    , short.Parse(PoStatus.Pending.PoStatusId.ToString()),
                    ProductionStatus.Default.ProductionStatusID,
                    int.MinValue, // Sponsor Consultant
                    int.MinValue, // AR Consultant
                    ARStatus.NotPaid.ARStatusID,
                    cl.LeadId,
                    billingCompany,
                    short.MinValue, // upfront payment 
                    int.MinValue, // confirmer id
                    CollectionStatus.Default.CollectionStatusID,
                    ConfirmationMethod.CreditCard.ConfirmationMethodID,
                    CreditApprovalMethod.CreditApprovedByAR.CreditApprovalMethodID,
                    null, // PO number
                    string.Empty, //creditInfo.creditCardNumber, 
                    string.Empty, //creditInfo.expDate,
                    DateTime.Now,	// sale date
                    0, // shipping fees
                    0, // shipping fees discounts
                    DateTime.MinValue, // payment due date
                    DateTime.MinValue, // confirmed date
                    DateTime.MinValue, // scheduled delivery date
                    DateTime.MinValue, // scheduled ship date
                    DateTime.MinValue, // actual ship date
                    null, // way bill no.
                    null, // comment
                    0, // is coupons sheet assigned
                    0, // total amount
                    DateTime.MinValue, // invoice date
                    DateTime.MinValue, // cancellation date
                    0, // is ordered
                    DateTime.MinValue,  // PO received date
                    0, // is delivered
                    0, // local sponsor found
                    DateTime.MinValue, // return date
                    DateTime.MinValue, // reship date
                    0, // upfront payment required
                    DateTime.MinValue, // upfront payment due date
                    0, // is sponsor required
                    DateTime.MinValue,  // actual delivery date
                    null,	// accounting comment
                    null,	// social security number
                    null,	// social security address
                    null,	// social security city
                    null,	// social security state
                    null,	// social security country
                    null,	// social security zip
                    0,
                    DateTime.MinValue,	// promise due date
                    0,	// general flag (always 0)
                    short.MinValue);	// fuel surcharge (always null));


                // Need to investigate in contructor
                //				sale.PaymentMethodId = GetPaymentMethodId("default");
                //				sale.SalesStatusId = SalesStatus.New.SalesStatusID;
                //				sale.PoStatusId = short.Parse(PoStatus.Pending.PoStatusId.ToString());
                //				sale.ProductionStatusId = ProductionStatus.Default.ProductionStatusID;
                //				sale.ArStatusId = ARStatus.NotPaid.ARStatusID;
                //				sale.LeadId = cl.LeadId;
                //				sale.BillingCompanyId = billingCompany;
                //				sale.CollectionStatusId = CollectionStatus.Default.CollectionStatusID;
                //				sale.ConfirmationMethodId = ConfirmationMethod.CreditCard.ConfirmationMethodID;
                //				sale.CreditApprovalMethodId = CreditApprovalMethod.CreditApprovedByAR.CreditApprovalMethodID;
                //				sale.SalesDate = DateTime.Now;
                //				sale.ClientSequenceCode = cl.ClientSequenceCode;
                //				sale.PaymentTermId = PaymentTerm.Prepaid.PaymentTermId;
                //				sale.ConsultantId = l.ConsultantId;
            }
            return sale;
        }



        public static efundraising.EFundraisingCRM.Sale BuildDefaultSaleScreen(int leadID, int clientID, string clientSeqCode)
        {
            Sale sale = null;

            Lead l = Lead.GetLeadByID(leadID);
            if (l != null)
            {
                // determine the billing company
                int billingCompany = BillingCompany.eFundraising_USA.BillingCompanyID;  // 1 is the default - efundraising USA
                if (l.PromotionId == 5953 || l.PromotionId == 5961)
                    billingCompany = BillingCompany.FR.BillingCompanyID;

                sale =
                    new Sale(int.MinValue,
                    l.ConsultantId,
                    short.MinValue,
                    1,
                    PaymentTerm.COD_30Days.PaymentTermId,
                    clientSeqCode,
                    clientID,
                    SalesStatus.New.SalesStatusID, ManageProduct.GetPaymentMethodId("CHECK")
                    , short.Parse(PoStatus.Pending.PoStatusId.ToString()),
                    ProductionStatus.Default.ProductionStatusID,
                    int.MinValue, // Sponsor Consultant
                    int.MinValue, // AR Consultant
                    ARStatus.NotPaid.ARStatusID,
                    leadID,
                    billingCompany,
                    short.MinValue, // upfront payment 
                    int.MinValue, // confirmer id
                    CollectionStatus.Default.CollectionStatusID,
                    ConfirmationMethod.CreditCard.ConfirmationMethodID,
                    CreditApprovalMethod.CreditApprovedByAR.CreditApprovalMethodID,
                    null, // PO number
                    string.Empty, //creditInfo.creditCardNumber, 
                    string.Empty, //creditInfo.expDate,
                    DateTime.Now,	// sale date
                    0, // shipping fees
                    0, // shipping fees discounts
                    DateTime.MinValue, // payment due date
                    DateTime.MinValue, // confirmed date
                    DateTime.MinValue, // scheduled delivery date
                    DateTime.MinValue, // scheduled ship date
                    DateTime.MinValue, // actual ship date
                    null, // way bill no.
                    null, // comment
                    0, // is coupons sheet assigned
                    0, // total amount
                    DateTime.MinValue, // invoice date
                    DateTime.MinValue, // cancellation date
                    0, // is ordered
                    DateTime.MinValue,  // PO received date
                    0, // is delivered
                    0, // local sponsor found
                    DateTime.MinValue, // return date
                    DateTime.MinValue, // reship date
                    0, // upfront payment required
                    DateTime.MinValue, // upfront payment due date
                    0, // is sponsor required
                    DateTime.MinValue,  // actual delivery date
                    null,	// accounting comment
                    null,	// social security number
                    null,	// social security address
                    null,	// social security city
                    null,	// social security state
                    null,	// social security country
                    null,	// social security zip
                    0,
                    DateTime.MinValue,	// promise due date
                    0,	// general flag (always 0)
                    short.MinValue);	// fuel surcharge (always null));


                // Need to investigate in contructor
                //				sale.PaymentMethodId = GetPaymentMethodId("default");
                //				sale.SalesStatusId = SalesStatus.New.SalesStatusID;
                //				sale.PoStatusId = short.Parse(PoStatus.Pending.PoStatusId.ToString());
                //				sale.ProductionStatusId = ProductionStatus.Default.ProductionStatusID;
                //				sale.ArStatusId = ARStatus.NotPaid.ARStatusID;
                //				sale.LeadId = cl.LeadId;
                //				sale.BillingCompanyId = billingCompany;
                //				sale.CollectionStatusId = CollectionStatus.Default.CollectionStatusID;
                //				sale.ConfirmationMethodId = ConfirmationMethod.CreditCard.ConfirmationMethodID;
                //				sale.CreditApprovalMethodId = CreditApprovalMethod.CreditApprovedByAR.CreditApprovalMethodID;
                //				sale.SalesDate = DateTime.Now;
                //				sale.ClientSequenceCode = cl.ClientSequenceCode;
                //				sale.PaymentTermId = PaymentTerm.Prepaid.PaymentTermId;
                //				sale.ConsultantId = l.ConsultantId;
            }
            return sale;
        }



        /*public static decimal GetItemProfit(int scratchBookID, int quantitySold, int quantityFree,  decimal profitPercentage)
        {
            decimal totalProfit;
            decimal unitPrice;
            //1- Check if item has a package with raising potential
            EFundraisingCRM.ScratchBook sc = EFundraisingCRM.ScratchBook.GetScratchBookByID(scratchBookID);
            EFundraisingCRM.Package p = EFundraisingCRM.Package.GetPackageByID(sc.PackageId);
            EFundraisingCRM.ProductBusinessRules br = EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductID(scratchBookID);

			
            if (p.ProfitDefault > 0)  //Package Profit Found
            {
                //Calculate Unit Price of item based on profit percentage
                if (profitPercentage == 0)
                {
                    //get default profit percentage
                   unitPrice = br.RaisingPotential - (br.RaisingPotential * br.DefaultProfitPercenatge);

                }else{
				
                    unitPrice = br.RaisingPotential - (br.RaisingPotential * profitPercentage);
                }

                profitPercentage = Convert.ToDecimal(((raisingPotential - salesItem.UnitPriceSold) / raisingPotential));
                totalProfit = Convert.ToDecimal(dtotalAmount*margin*decimal.Parse("0.01")).ToString("C");
            }


            decimal TotalAmount =  0; 
            return TotalAmount; //* ProfitPercentage *decimal.Parse("0.01")).ToString("C");
        }
*/
        //For exsisting sales

        
         public static decimal GetSalesItemProfitPercentage(SalesItem salesItem)
        {
            //get raising potential if packages only
            efundraising.EFundraisingCRM.ScratchBook sb = efundraising.EFundraisingCRM.ScratchBook.GetScratchBookByID(salesItem.ScratchBookId);
            efundraising.EFundraisingCRM.Package p = efundraising.EFundraisingCRM.Package.GetPackageByID(sb.PackageId);

            decimal raisingPotential = Convert.ToDecimal(sb.RaisingPotential);
            decimal profitPercentage = 0;
            bool packageFound = false;

            try
            {

                if (p != null)
                {
                    packageFound = true;
                }
                else if (p.ProfitDefault > 0)
                {
                    packageFound = true;
                }

                //profit par rapport au montant total paye
                //si je paye 1$ et ca rapporte 900$ le profit est pas 900% mais 99,9%
                if (packageFound && raisingPotential > 0)
                {
                    profitPercentage = Convert.ToDecimal((raisingPotential - salesItem.UnitPriceSold) / raisingPotential);
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetSalesItemProfitPercentage - " + salesItem, ex);
            }


            return profitPercentage;
        }

        public static decimal GetSalesItemTotalProfit(ScratchBook sb, int quantitySold, int quantityFree)
        {
            decimal totalProfit = Convert.ToInt32(sb.RaisingPotential) * (quantitySold + quantityFree);
            return totalProfit;
        }

        public static int GetQtyFree(int scratchBookID, int quantitySold)
        {
            int qtyFree = 0;
            try
            {
                efundraising.EFundraisingCRM.ProductBusinessRules br = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductID(scratchBookID);
                if (br != null)
                {
                    if (br.Free > 0)
                    {
                        double temp = Convert.ToDouble(quantitySold * br.Free);
                        if (temp >= 1)
                        {
                            qtyFree = Convert.ToInt32(Math.Ceiling(temp));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetQtyFree. sbID = " + scratchBookID + " - QtySold = " + quantitySold, ex);
            }

            return qtyFree;


        }


        //only for gst an pst
        public static void CalculateTaxes(decimal totalAmount, ref decimal gst, ref decimal pst, int clientID, string clientSeqCode)
        {
            try
            {
                DataTable dt = efundraising.EFundraisingCRM.StateTax.GetStateTaxByClientID(clientID, clientSeqCode);


                foreach (DataRow dr in dt.Rows)
                {
                    decimal rate = Convert.ToDecimal(dr["tax_rate"]);
                    string code = dr["tax_code"].ToString();
                    if (code == "GST")
                    {
                        gst = rate / 100;
                    }
                    else
                    {
                        pst = rate / 100;
                    }
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: CalculateTaxes. ClientID = " + clientSeqCode + clientID, ex);
            }


        }

        //only for gst an pst
        public static decimal GetGST(int clientID, string clientSeqCode)
        {
            decimal GST = 0;
            try
            {
                
                DataTable dt = efundraising.EFundraisingCRM.StateTax.GetStateTaxByClientID(clientID, clientSeqCode);


                foreach (DataRow dr in dt.Rows)
                {
                    decimal rate = Convert.ToInt32(dr["tax_rate"]);
                    string code = dr["tax_code"].ToString();
                    if (code == "GST")
                    {
                        GST = rate / 100;
                    }
                }

                
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetGST. ClientID = " + clientSeqCode + clientID, ex);
            }
            return GST;

        }


                //only for gst an pst
        public static decimal GetPST(int clientID, string clientSeqCode)
        {  
            decimal PST = 0;
            try
            {
              
                DataTable dt = efundraising.EFundraisingCRM.StateTax.GetStateTaxByClientID(clientID, clientSeqCode);


                foreach (DataRow dr in dt.Rows)
                {
                    decimal rate = Convert.ToDecimal(dr["tax_rate"]);
                    string code = dr["tax_code"].ToString();
                    if (code == "QST")
                    {
                        PST = rate / 100;
                    }
                }

               
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetPST. ClientID = " + clientSeqCode + clientID, ex);
            }
            return PST;

        }

     
        public static decimal GetItemPrice(ScratchBook sb, decimal profitPercentage)
        {
           


            decimal itemPrice = 0;
            try
            {
                if (profitPercentage == 0)
                {
                    itemPrice = ScratchBookPriceInfo.GetScratchBookCurrentPriceByID(sb.ScratchBookId);
                }
                else
                {
                    decimal raisingPotential = Convert.ToDecimal(sb.RaisingPotential);
                    itemPrice = Convert.ToDecimal(raisingPotential - (raisingPotential * profitPercentage));
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetItemPrice - " + sb, ex);
            }

            return itemPrice;
        }

        public static decimal GetSalesItemTotalProfit(decimal totalPaid, decimal raisingPotential, int totalQty)
        {
            decimal profit = (raisingPotential * totalQty) - totalPaid;
            return profit;
        }

        
        public static decimal GetSalesItemTotalProfit(SalesItem salesItem)
        {
            decimal profitPercentage = GetSalesItemProfitPercentage(salesItem);
            efundraising.EFundraisingCRM.ScratchBook sb = efundraising.EFundraisingCRM.ScratchBook.GetScratchBookByID(salesItem.ScratchBookId);

            decimal raisingPotential = Convert.ToDecimal(sb.RaisingPotential);
            decimal totalProfit = 0;

            try
            {
                //get raising potential
                if (profitPercentage > 0)
                {
                    totalProfit = GetSalesItemTotalProfit(salesItem.UnitPriceSold * salesItem.QuantitySold,raisingPotential,salesItem.QuantitySold + salesItem.QuantityFree);
                }
                else
                {
                    totalProfit = raisingPotential * (salesItem.QuantitySold + salesItem.QuantityFree);
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetSalesItemtotalProfit - " + salesItem, ex);
            }


            return totalProfit;
        }

        //gets the scheduled delivery date
        //the actual ship date has to be today or later
        public static DateTime GetMaxDeliveryDate(DataTable productList, DateTime schShipDate)
        {
            int maxTime = 0;
            int time = 0;
            try
            {
                foreach (DataRow row in productList.Rows)
                {
                    int scratchBookID = Convert.ToInt32(row["ScratchbookID"]);
                    efundraising.EFundraisingCRM.ProductBusinessRules br = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductID(scratchBookID);
                    if (br != null)
                    {
                        time = br.AverageDeliveryTime;
                        if (time > maxTime)
                        {
                            maxTime = time;
                        }
                    }
                    else
                    {
                        return DateTime.MinValue;
                    }

                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetMaxDeliveryDate - " + productList, ex);
            }

            if (productList.Rows.Count == 0)
            {
                return DateTime.MinValue;
            }
            else
            {
                return efundraising.EFundraisingCRM.ProductBusinessRules.GetNextBusinessDay(schShipDate, time);
            }
        }

        //gets the scheduled delivery date
        public static DateTime GetDeliveryDate(int scratchbookID, DateTime schShipDate)
        {
            int maxTime = 0;
            int time = 0;

            try
            {
                efundraising.EFundraisingCRM.ProductBusinessRules br = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductID(scratchbookID);
                if (br != null)
                {
                    time = br.AverageDeliveryTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetDeliveryDate - sbID=" + scratchbookID, ex);
            }

            return efundraising.EFundraisingCRM.ProductBusinessRules.GetNextBusinessDay(schShipDate, time);


        }

        //gets the scheduled shipping date
        public static DateTime GetShippingDate(int scratchbookID, DateTime schDelDate)
        {
            int maxTime = 0;
            int time = 0;

            try
            {
                efundraising.EFundraisingCRM.ProductBusinessRules br = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductID(scratchbookID);
                if (br != null)
                {
                    time = br.AverageDeliveryTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetShippingDate - sbID=" + scratchbookID, ex);
            }

            return efundraising.EFundraisingCRM.ProductBusinessRules.GetPreviousBusinessDay(schDelDate, time);


        }


        public static int GetMaxDeliveryDays(DataTable productList)
        {
            int maxTime = 0;
            int time = 0;
            try
            {
                foreach (DataRow row in productList.Rows)
                {
                    int scratchBookID = Convert.ToInt32(row["ScratchbookID"]);
                    efundraising.EFundraisingCRM.ProductBusinessRules br = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductID(scratchBookID);
                    if (br != null)
                    {
                        time = br.AverageDeliveryTime;
                        if (time > maxTime)
                        {
                            maxTime = time;
                        }
                    }
                    else
                    {
                        return -1;
                    }

                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetMaxDeliveryDays - " + productList, ex);
            }

            if (productList.Rows.Count == 0)
            {
                return -1;
            }
            else
            {
                return maxTime;
            }
        }

        public static int GetDeliveryDays(int scratchbookID)
        {
            int maxTime = 0;
            int time = 0;

            try
            {
                efundraising.EFundraisingCRM.ProductBusinessRules br = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductID(scratchbookID);
                if (br != null)
                {
                    time = br.AverageDeliveryTime;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetDeliveryDays - sbID=" + scratchbookID, ex);
            }

            return time;

        }



        //Get total shipping fess for the items of dsifferent product class
        //if different shipping for items in same product class, takes the highest amount
        //if more than one product class, adds them all up
        public static decimal GetTotalShippingFees(DataTable productList, ref DataTable classTotals, decimal customShipping)
        {
            decimal totalShipping = 0;
            try
            {


                    bool wfcCAProcessed = false;
                    bool PVProcessed = false;
                    bool FFProcessed = false;
                    bool FFCAProcessed = false;
                    bool tshirtProcessed = false;
                    bool giftProcessed = false;

                    foreach (DataRow row in productList.Rows)
                    {

                        int scratchBookID = Convert.ToInt32(row["ScratchbookID"]);
                        int nbItems = Convert.ToInt32(row["NbItems"]);
                        ProductBusinessRules br = ProductBusinessRules.GetProductBusinessRulesByProductID(scratchBookID);
                        int minQty = br.MinOrder;

                        //get product class of each product to get total amount
                        efundraising.EFundraisingCRM.ScratchBook sb = efundraising.EFundraisingCRM.ScratchBook.GetScratchBookByID(scratchBookID);
                        int productClassID = sb.ProductClassId;

                        if (customShipping >= 0)
                        {
                            SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), customShipping.ToString());
                            totalShipping = customShipping;
                        }
                        else
                        {

                            //check if package is frozen food
                            
                            decimal surplus = 0;
                            int frozenFoodID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("FrozenFoodCaseShortAmount", "productClassID"));
                            int frozenFoodCAID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("FrozenFoodCACaseShortAmount", "productClassID"));
                            int pineValleyID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("PineValleyShortAmount", "productClassID"));
                            int giftID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("GiftShortAmount", "productClassID"));
                            int tshirtID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("TShirtShortAmount", "productClassID"));
                            int wfcCAID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("WFCcaShortAmount", "productClassID"));


                            
                            decimal totalAmount = 0;
                            decimal totalQty = 0;
                        

                            //WFC CA EXCEPTION
                            if (productClassID == wfcCAID)
                            {
                                if (wfcCAProcessed)
                                {
                                    continue;
                                }
                                wfcCAProcessed = true;
                                totalAmount = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", productClassID.ToString()));
                                totalQty = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", productClassID.ToString()));
                                decimal cost = Convert.ToDecimal(GetValueFromWebConfig("WFCcaShortAmount", "value"));
                                int min = Convert.ToInt32(GetValueFromWebConfig("WFCcaShortAmount", "min"));

                                decimal wfcCAShipping = totalQty * cost;
                                if (totalQty < min)
                                {
                                    totalShipping += wfcCAShipping;
                                }else if (totalQty < 50)
                                {
                                    totalShipping += 40;
                                }
                       
                                SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), wfcCAShipping.ToString());
                            }  //FF EXCEPTION
                            else if (productClassID == frozenFoodID)
                            {
                                if (FFProcessed)
                                {
                                    continue;
                                }
                                FFProcessed = true;
                          
                                totalAmount = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", productClassID.ToString()));
                                totalQty = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", productClassID.ToString()));
                                decimal cost = Convert.ToDecimal(GetValueFromWebConfig("FrozenFoodCaseShortAmount", "value"));
                                int min = Convert.ToInt32(GetValueFromWebConfig("FrozenFoodCaseShortAmount", "min"));

                                decimal FFshipping = (min - totalQty) * cost;
                                if (FFshipping < 0)
                                {
                                    FFshipping = 0;
                                }
                                if (totalQty < min)
                                {
                                    totalShipping += FFshipping;
                                }
                         

                                SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), FFshipping.ToString());

                            }  //PV EXCEPTION
                            else if (productClassID == pineValleyID)
                            {
                                if (PVProcessed)
                                {
                                    continue;
                                }
                                PVProcessed = true;

                                totalAmount = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", productClassID.ToString()));
                                totalQty = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", productClassID.ToString()));
                                decimal cost = Convert.ToDecimal(GetValueFromWebConfig("PineValleyShortAmount", "value"));
                                int min = Convert.ToInt32(GetValueFromWebConfig("PineValleyShortAmount", "min"));

                                decimal PVshipping = (min - totalQty) * cost;
                                if (PVshipping < 0)
                                {
                                    PVshipping = 0;
                                }
                                if (totalQty < min)
                                {
                                    totalShipping += PVshipping;
                                }


                                SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), PVshipping.ToString());


                            }//GIFT CA EXCEPTION
                            else if (productClassID == giftID)
                            {
                                if (giftProcessed)
                                {
                                    continue;
                                }
                                giftProcessed = true;

                                totalAmount = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", productClassID.ToString()));
                                totalQty = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", productClassID.ToString()));
                                decimal cost = Convert.ToDecimal(GetValueFromWebConfig("GiftShortAmount", "value"));
                                int min = Convert.ToInt32(GetValueFromWebConfig("GiftShortAmount", "min"));

                                decimal giftShipping = totalQty;
                                if (totalQty >= min)
                                {
                                    giftShipping = 0;
                                }

                                totalShipping += giftShipping;

                                SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), giftShipping.ToString());

                                //TSHIRT EXCEPTION
                            }
                            else if (productClassID == frozenFoodCAID)
                            {
                                if (FFCAProcessed)
                                {
                                    continue;
                                }
                                FFCAProcessed = true;

                                totalAmount = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", productClassID.ToString()));
                                totalQty = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", productClassID.ToString()));
                                decimal cost = Convert.ToDecimal(GetValueFromWebConfig("FrozenFoodCACaseShortAmount", "value"));
                                int min = Convert.ToInt32(GetValueFromWebConfig("FrozenFoodCACaseShortAmount", "min"));

                                decimal FFCAshipping = (min - totalQty) * cost;
                                if (FFCAshipping < 0)
                                {
                                    FFCAshipping = 0;
                                }
                                if (totalQty < min)
                                {
                                    totalShipping += FFCAshipping;
                                }


                                SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), FFCAshipping.ToString());

                                //TSHIRT EXCEPTION
                            }/*
                            else if (productClassID == tshirtID)
                            {
                                if (tshirtProcessed)
                                {
                                    continue;
                                }
                                tshirtProcessed = true;

                                totalAmount = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", productClassID.ToString()));
                                totalQty = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", productClassID.ToString()));
                                decimal cost = Convert.ToDecimal(GetValueFromWebConfig("TShirtShortAmount", "value"));
                                int min = Convert.ToInt32(GetValueFromWebConfig("TShirtShortAmount", "min"));
                                decimal tshirtShipping = (min - totalQty) * cost;
                                if (totalQty < min)
                                {
                                    totalShipping += tshirtShipping;
                                }


                                SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), tshirtShipping.ToString());



                            }*/
                            else
                            {

                                totalAmount = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount", productClassID.ToString()));
                                totalQty = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", productClassID.ToString()));
                              

                                //go get the total for that product for the total amount of the product class
                                //(will keep the higest amount)
                                efundraising.EFundraisingCRM.ShippingFee f = efundraising.EFundraisingCRM.ShippingFee.GetShippingFeeByProductIDAndTotalAmout(scratchBookID, totalAmount);


                                decimal fee = 0;
                                if (f != null)
                                {
                                    f._ShippingFee = f._ShippingFee + surplus;
                                    fee = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString()));
                                    //replace if greater
                                    if (fee < f._ShippingFee)
                                    {
                                        SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), f._ShippingFee.ToString());
                                        totalShipping += f._ShippingFee - fee;
                                    }
                                }
                            }  //product class IF
                    } //if not custom ship

                }//foreach
                 
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetTotalShippingFees - " + productList, ex);
            }

            return totalShipping;

        }
       

        /*
                public static decimal GetShippingFees(int scratchBookID, ref DataTable classTotals)
                {		
			
                    decimal totalShipping = 0;
			
                    //get product class of each product to get total amount
                    EFundraisingCRM.ScratchBook sb = EFundraisingCRM.ScratchBook.GetScratchBookByID(scratchBookID); 
                    string productClassID = sb.ProductClassId.ToString();
                    decimal totalAmount = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalAmount",productClassID));
			
                    EFundraisingCRM.ShippingFee f = EFundraisingCRM.ShippingFee.GetShippingFeeByProductIDAndTotalAmout(scratchBookID, totalAmount);
			
                    decimal fee = Convert.ToDecimal(GetValueFromDataTable(classTotals, "ProductClassID", "TotalShipping",productClassID.ToString()));
                    //replace if greater
                    if (fee < f._ShippingFee)
                    {
                        //SetValueToDataTable(classTotals, "ProductClassID", "TotalShipping", productClassID.ToString(), f._ShippingFee.ToString());
                        fee = f._ShippingFee;
                    }			
							
                    return fee;					  
						
                }*/




        //validate the min qty by product
        //workin wuth classTotals and peroduct list
        //for each propduct if this product hes the business rules by itesf, its qty will be taken off the total class
        //qty and will be be validated on its own
        public static bool ValidateMinQty(ref DataTable classTotals, ref string message)
        {
            bool error = false;
            try
            {
                for (int i = 0; i < classTotals.Rows.Count; i++)
                {
                    int productClassID = Convert.ToInt32(classTotals.Rows[i]["ProductClassID"]);
                    int frozenFoodID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("FrozenFoodCaseShortAmount", "productClassID"));
                    int PEWFCID = Convert.ToInt32(ManageSaleScreen.GetValueFromWebConfig("PEWFC", "ID"));
                    if (productClassID != frozenFoodID)
                    {
                      

                        efundraising.EFundraisingCRM.ProductClass pc = efundraising.EFundraisingCRM.ProductClass.GetProductClassById(productClassID);
                        string productClass = pc.Description;

                        efundraising.EFundraisingCRM.ProductBusinessRules br = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByProductClass(productClassID);
                        if (br != null)
                        {
                            //get total qty
                            int qty = Convert.ToInt32(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(classTotals, "ProductClassID", "Quantity", productClassID.ToString()));
                           /* if (productClassID == PEWFCID)
                            {
                                if (qty % 25 != 0)
                                {
                                    error = true;
                                    message = "The quantity must be a multiple of 25";

                                }

                            }*/
                            if (qty < br.MinOrder && qty > 0)  //0 means that it contains a product on its own
                            {
                                error = true;
                                message = "The minimun quantity for product class " + productClass + " is " + br.MinOrder.ToString();
                            }
                        }
                        else
                        {
                            error = true;
                            message = "Business rules not set for product class " + productClass;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: VaidateMinQty - " + classTotals, ex);
            }

            return error;

        }

        //validate the min qty by product
        //workin wuth classTotals and peroduct list
        //for each propduct if this product hes the business rules by itesf, its qty will be taken off the total class
        //qty and will be be validated on its own
        public static bool ValidateMinQtyPackage(ref DataTable packageTotals, ref string message)
        {
            bool error = false;
            try
            {
                foreach (DataRow row in packageTotals.Rows)
                {
                    bool frozenFood = false;
                    int packageID = Convert.ToInt32(row["PackageID"]);
                    string packages = GetValueFromWebConfig("FrozenFoodCaseShortAmount","packageID");
                    string[] packageList = packages.Split(',');
                    foreach (string id in packageList)
                    {
                        if (Convert.ToInt32(id) == packageID)
                        {
                            frozenFood = true;
                        }
                    }

                    if (!(frozenFood))
                    {
                        efundraising.EFundraisingCRM.Package package = efundraising.EFundraisingCRM.Package.GetPackageByID(packageID);
                        string packageDesc = package.Description;

                        efundraising.EFundraisingCRM.ProductBusinessRules br = efundraising.EFundraisingCRM.ProductBusinessRules.GetProductBusinessRulesByPackageIdOnly(packageID);
                        if (br != null)
                        {
                            //get total qty
                            int qty = Convert.ToInt32(EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetValueFromDataTable(packageTotals, "PackageID", "Quantity", packageID.ToString()));
                            if (qty < br.MinOrder && qty > 0)  //0 means that it contains a product on its own
                            {
                                error = true;
                                message = "The minimun quantity for package " + packageDesc + " is " + br.MinOrder.ToString();
                            }
                            else
                            {
                                message = "Passed";
                            }
                        }
                        else
                        {
                            error = true;
                            message = "Business rules not set for package " + packageDesc;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: VaidateMinQty - " + packageTotals, ex);
            }

            return error;

        }



        public static void CalculateTaxes(decimal amount,int clientID, string clientSeqCode, ref decimal pst, ref decimal gst)
        {
    

            DataTable dt = StateTax.GetStateTaxByClientID(clientID, clientSeqCode);
            foreach (DataRow dr in dt.Rows)
            {
                decimal rate = Convert.ToDecimal(dr["tax_rate"]);
                string code = dr["tax_code"].ToString();
                if (code == "GST")
                {
                    gst = rate;
                }
                else
                {
                    pst = rate;
                }
            }

            //set gst
            gst = amount * (gst / 100);
            //set pst
            pst = (amount + gst) * (pst / 100);

           

         
                        
        }
        
        public static bool IsProductTaxExempt(int scratchBookId)
        {
            ScratchBook sb = ScratchBook.GetScratchBookByID(scratchBookId);
            int productClassId = sb.ProductClassId;
            ProductClass productClass = ProductClass.GetProductClassById(productClassId);
            return productClass.TaxExempt;
        }

        public static bool IsSaleTaxExempt(int saleId)
        {
            SalesItem[] si = SalesItem.GetSalesItemsBySaleID(saleId);
            int sbId = si[0].ScratchBookId;
            return IsProductTaxExempt(sbId);
        }

        public static void CalculateTaxesByProductClass(DataTable classTotals, int clientID, string clientSeqCode,  ref decimal totalGST, ref decimal totalPST, decimal adjustment, ref decimal pstOnAdj,ref decimal gstOnAdj)
        {
            try
            {

                foreach (DataRow row in classTotals.Rows)
                {
                    //check if product class is tax exempt
                    int pcID = Convert.ToInt32(row["ProductClassID"]);
                    ProductClass productClass = ProductClass.GetProductClassById(pcID);
                    bool taxExempt = productClass.TaxExempt;
                    decimal gst = 0;
                    decimal pst = 0;
                    decimal gstRate = 0;
                    decimal pstRate = 0;
                    decimal totalAmount = Convert.ToDecimal(row["TotalAmount"]);
                    decimal totalShipping = Convert.ToDecimal(row["TotalShipping"]);
                    
                    if (taxExempt)
                    {
                        totalAmount = 0;
                        adjustment = 0;
                    }             
                    
                    DataTable dt = StateTax.GetStateTaxByClientID(clientID, clientSeqCode);

                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal rate = Convert.ToDecimal(dr["tax_rate"]);
                        string code = dr["tax_code"].ToString();
                        if (code == "GST")
                        {
                            gstRate = rate;
                        }
                        else
                        {
                            pstRate = rate;
                        }
                    }
                        
                    gst = (totalAmount + totalShipping) * (gstRate / 100); // (totalAmount + totalShipping + charge) * (gst / 100);
                    row["TotalGST"] = gst;
                    //set pst
                    row["TotalPST"] = (totalAmount + gst + totalShipping) * (pstRate / 100); //(totalAmount + gst + totalShipping + charge) * (pst / 100);
         

                    totalGST += Convert.ToDecimal(row["TotalGST"]);
                    totalPST += Convert.ToDecimal(row["TotalPST"]);
                    
                    gstOnAdj = adjustment * (gstRate / 100);
                    pstOnAdj = (adjustment + gstOnAdj) * (pstRate / 100);

                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: CalculateTaxesByProductClass", ex);
            }

        }

        
        public static string GetProfitRate(int productId, int qty, int classQty, int packageQty, ref string  ruleBaseLevel)
        {
            string rate = "0";
            ProductBusinessRules rules = ProductBusinessRules.GetProductBusinessRulesByProductID(productId, ref ruleBaseLevel);
            if (ruleBaseLevel == "ProductClass")
            {
                qty = classQty;
            }
            else if (ruleBaseLevel == "Package")
            {
                qty = packageQty;
            }
            if (rules != null)
            {
               List<ProfitRange> ranges = ProfitRange.GetProfitRangeByProductBusinessRuleID(rules.ProductBusinessRuleID);
               foreach(ProfitRange range in ranges)
               {
                   int min = range.ItemNbrMin;
                   int max = range.ItemNbrMax;
                   if (qty >= min && (qty <= max))
                   {
                       rate = range.ProfitPercentage.ToString();
                       break;
                   }


               }
            }

            return rate;
        }

     


       /* public static void GetProfitRateMinMax(int productId,ref int min, ref int max)
        {
            min = 0;
            max = 0;
            ProductBusinessRules rules = ProductBusinessRules.GetProductBusinessRulesByProductID(productId);
            if (rules != null)
            {
                ProfitRange[] ranges = ProfitRange.GetProfitRangeByProductBusinessRuleID(rules.ProductBusinessRuleID);
                foreach (ProfitRange range in ranges)
                {
                    if (range.ItemNbrMin < min)
                    {
                        min = range.ItemNbrMin;
                    }
                    if (range.ItemNbrMax > max)
                    {
                        max = range.ItemNbrMax;
                    }
                }
            }
            
        }*/





        public static string GetValueFromDataTable(DataTable dt, string KeyColumn, string ValueColumn, string whereCriteria)
        {
            string amt = "0";
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    //if row[ProductClassID] == 2
                    if (row[KeyColumn].ToString() == whereCriteria)
                    {
                        //row[TotalAmount]
                        amt = row[ValueColumn].ToString();
                    }
                }
                if (amt == "")
                {
                    amt = "0";
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetValueFromDataTable", ex);
            }
            return amt;
        }

        public static void SetValueToDataTable(DataTable dt, string KeyColumn, string ValueColumn, string whereCriteria, string newValue)
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    //if row[ProductClassID] == 2
                    if (row[KeyColumn].ToString() == whereCriteria)
                    {
                        //row[TotalAmount]
                        row[ValueColumn] = newValue;
                    }
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: SetValueToDataTable", ex);
            }

        }

        
        //if user is MIS, we dont want to APPLY THE RULES OF CONSULTANT
        public static bool IsConsultant()
        {

            bool isConsultant = false;
            try
            {
                bool isMIS = false;
                Components.Server.User.Role[] r = new Components.Server.User.Role[1];


                bool impersonate = Convert.ToBoolean(GetValueFromWebConfig("UserGroup", "impersonate"));
                if (impersonate)
                {
                    string group = GetValueFromWebConfig("UserGroup", "value").ToString();
                    Components.Server.User.Role a = new Components.Server.User.Role();
                    a.Name = group;
                    r[0] = a;
                }
                else
                {
                    
                    Components.Server.User.Roles roles = (Components.Server.User.Roles)System.Web.HttpContext.Current.Session[Global.SessionVariables.ROLES];
                    r = roles.GetAllRoles();
                }


                foreach (Components.Server.User.Role role in r)
                {
                    if (role.Name == "gCAEFR_Intranet_Consultant")
                    {
                        isConsultant = true;
                    }
                    else if (role.Name == "gCAEFR_Intranet_IT")
                    {
                        isMIS = true;
                    }
                }

                if (isMIS)
                {
                    isConsultant = false;
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: IsConsultant", ex);
            }

            return isConsultant;

        }


        public static bool IsOnlyConsultant()
        {
            if (!(ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_IT) ||
                ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_Production) ||
                ManageSaleScreen.IsInGroup(ManageSaleScreen.Roles.gCAEFR_Intranet_SaleSupport)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsInGroup(Roles role)
        {
           
            bool found = false;
            bool impersonate = Convert.ToBoolean(GetValueFromWebConfig("UserGroup", "impersonate"));
            string group = GetValueFromWebConfig("UserGroup", "value").ToString();
                

            try{
            //if martine, then she is in sale support (bug with her windows profile)
            string userName = System.Web.HttpContext.Current.Session[Global.SessionVariables.USER_NAME].ToString();

            if (role == ManageSaleScreen.Roles.gCAEFR_Intranet_SaleSupport){
            //check if user is part of sale support is web.config
            string[] users =  GetValueFromWebConfig("SaleSupportEmployee", "default").ToString().Split('|');
            foreach(string name in users)
            {
                if (userName == name){
                    found = true;
                }
            }
            }

            if (!found)
            {
                if (impersonate)
                {
                    if (role.ToString() != group)
                    {
                        found = false;
                    }
                    else
                    {
                        found = true;
                    }

                }

                else
                {

                    Components.Server.User.Role[] r = new Components.Server.User.Role[1];

                    Components.Server.User.Roles roles = (Components.Server.User.Roles)System.Web.HttpContext.Current.Session[Global.SessionVariables.ROLES];
                    r = roles.GetAllRoles();

                    foreach (Components.Server.User.Role rrole in r)
                    {
                        if (rrole.Name == role.ToString())
                        {
                            found = true;
                        }
                    }
                }
            }
            else
            {
                if (impersonate)
                {
                    //this overrides the previous logic
                    if (role.ToString() != group)
                    {                        
                        found = false;
                    }

                }
            }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales screen: IsInGroup", ex);
            }

            return found;
        }

        public static bool IsProd()
        {
            return Convert.ToBoolean(ManageSaleScreen.GetValueFromWebConfig("EFundraisingProd.Production", "isProduction"));
        }


        //check in all the roles, if user has MIS
        public static bool IsMIS()
        {
            bool isMIS = false;
            try
            {
                Components.Server.User.Role[] r = new Components.Server.User.Role[1];

                bool impersonate = Convert.ToBoolean(GetValueFromWebConfig("UserGroup", "impersonate"));
                if (impersonate)
                {
                    isMIS = false;
                }
                else
                {
                    Components.Server.User.Roles roles = (Components.Server.User.Roles)System.Web.HttpContext.Current.Session[Global.SessionVariables.ROLES];
                    r = roles.GetAllRoles();

                    foreach (Components.Server.User.Role role in r)
                    {
                        if (role.Name == "gCAEFR_Intranet_IT")
                        {
                            isMIS = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: IsMIS", ex);
            }
            return isMIS;

        }

        public static int GetRootIDFromWebConfig()
        {
            int rootPackageID = 0;
            try
            {
                //get root package from web config
                bool prod = Convert.ToBoolean(GetValueFromWebConfig("EFundraisingStore.Production", "isProduction"));

                if (prod)
                {
                    rootPackageID = Convert.ToInt32(GetValueFromWebConfig("SaleScreen.RootPackageID", "prod"));
                }
                else
                {
                    rootPackageID = Convert.ToInt32(GetValueFromWebConfig("SaleScreen.RootPackageID", "dev"));
                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: GetRootIDFromWebConfig", ex);
            }
            return rootPackageID;
        }

        public static string GetValueFromWebConfig(string key, string keyValue)
        {
            return efundraising.Configuration.ApplicationSettings.GetConfig()[key, keyValue].ToString();

        }


        /// <summary>
        /// Cycles through all controls contained in a WebControl and sets		them
        /// to "ReadOnly" based on their Type.  "ReadOnly" changes from
        /// control to control since TextBoxes are the only type that actually			has
        /// a built-in readonly attribute.
        /// The controls still appear "white" and not "grayed out" as if they			had
        /// the enabled attribute set to false
        /// Recursively calls as well for any child controls, so such things				like
        /// datagrids with controls work.
        /// </summary>
        /// <param name="parentControl">Parent Control that contains controls				to be turned ReadOnly</param>
        public static void MakeReadOnly(Control parentControl)
        {
            try
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    switch (ctrl.GetType().Name)
                    {
                        case "TextBox":
                            ((TextBox)ctrl).ReadOnly = true;
                            ((TextBox)ctrl).TabIndex = -1;
                            break;

                        case "CheckBox":

                            CheckBox ckCtrl = (CheckBox)ctrl;
                            string sChecked = ckCtrl.Checked.ToString().ToLower();
                            ckCtrl.Attributes.Add("onClick", "javascript: this.checked=" + sChecked
                                + ";");
                            ckCtrl.TabIndex = -1;
                            break;

                        case "RadioButton":
                            RadioButton rbCtrl = (RadioButton)ctrl;
                            string sRBChecked = rbCtrl.Checked.ToString().ToLower();
                            rbCtrl.GroupName = String.Empty;	//Eliminate association with othe	dioButtons so that they don't automatically turn off
                            rbCtrl.Attributes.Add("onClick", "javascript:this.checked=" + sRBChecked + ";");
                            break;

                        case "DropDownList":
                            DropDownList dd = ((DropDownList)ctrl);
                            dd.Attributes.Add("onChange", "javascript:this.selectedIndex='" + dd.SelectedIndex.ToString() + "';");
                            break;
                    }
                    // Recursively do any child controls as well.
                    foreach (Control ctrlChild in ctrl.Controls)
                        MakeReadOnly(ctrlChild);

                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: makeReadOnly", ex);
            }
        }
        public static void MakeReadOnly(DropDownList dd)
        {
            dd.Attributes.Add("onChange", "javascript:this.selectedIndex='" + dd.SelectedIndex.ToString() + "';");
        }

        public static void RemoveReadOnly(DropDownList dd)
        {
            dd.Attributes.Remove("onChange");
        }


        public static string CalculateNewDatesByProduct(ref DateTime schShipDate, ref DateTime schDelDate, ref DateTime paymentDueDate, int paymentTermID, int scratchBookID, int saleID, int clientID, string clientSeq)
        {
            DataTable productList = new DataTable();
            DataColumn col = new DataColumn("ScratchbookID");
            productList.Columns.Add(col);
            col = new DataColumn("NbItems");
            productList.Columns.Add(col);
            
            DataRow row = productList.NewRow();
            row["ScratchbookID"] = scratchBookID;
            productList.Rows.Add(row);

            return CalculateNewDatesForProductList(ref schShipDate, ref schDelDate, ref paymentDueDate, paymentTermID, productList, saleID, clientID, clientSeq);
        }

        public static string CalculateNewDatesForProductList(ref DateTime schShipDate, ref DateTime schDelDate, ref DateTime paymentDueDate, int paymentTermID, DataTable productList, int saleID, int clientID, string clientSeq)
        {
            string error = "";
            string CHRerror = "";
            bool addShipDay = false;
            try
            {
                //get max del time
         
                string temp = ManageSaleScreen.GetValueFromWebConfig("NextDayShippingTime","default");
                string[] date = temp.Split(':');
                int H = Convert.ToInt32(date[0]);
                int M = Convert.ToInt32(date[1]);
                DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month,DateTime.Now.Day, H, M,0);
           
                
         

                bool haveChippery = false;
                bool haveWFC = false;
                bool havePineValley = false;
                bool pickUp = false;
                bool CHR = false;
                string zip = "";                       

                

                foreach (DataRow row in productList.Rows)
                {
                    int scratchBookID = Convert.ToInt32(row["ScratchbookID"]);
                    efundraising.EFundraisingCRM.ScratchBook sb = efundraising.EFundraisingCRM.ScratchBook.GetScratchBookByID(scratchBookID);
           
                    //CHR=  25-stock
                    if (sb.ProductClassId == 25)
                    {
                        CHR = true;    
                    }
                    
                    if (sb.ProductClassId == 4 ||
                        sb.ProductClassId == 25 ||
                        sb.ProductClassId == 26)
                    {
                        haveWFC = true;
                    }

                    if (sb.ProductClassId == 37)
                    {
                        havePineValley = true;
                    }
                    
                    if (sb.SupplierId == 43)
                    {
                        haveChippery = true;
                    }

                }


                if (DateTime.Now > time && havePineValley)
                {
                    addShipDay = true;
                }

                //calculate sch del days
                //check if sale is for pick up
              // int clientID = Convert.ToInt32(System.Web.HttpContext.Current.Session[Global.SessionVariables.CLIENT_ID]);
               // string clientSeq = System.Web.HttpContext.Current.Session[Global.SessionVariables.CLIENT_SEQUENCE_CODE].ToString();
                
                int scheduledDeliveryDays = 0;
                efundraising.EFundraisingCRM.ClientAddress ca = efundraising.EFundraisingCRM.ClientAddress.GetClientAddressByIdSequenceAddressType(clientID, clientSeq, "ST");
                
                if (ca != null)
                {
                    if (ca.PickUp)
                    {
                        scheduledDeliveryDays = 1;
                        pickUp = true;
                    }
                }
                if (!(pickUp))
                {
                    //check if CHR
                    //only us
                    if (CHR && ca.CountryCode == "US")
                    {
                        CHRerror = "CHR ERROR";
                        QSPForm.Business.BusinessRuleSystem brs = new QSPForm.Business.BusinessRuleSystem();
                        scheduledDeliveryDays = brs.GetLeadTime_CHR_BusinessDays(ca.ZipCode,0);

                        //IF CHR ERROR
                        if (scheduledDeliveryDays == 0)
                        {
                            scheduledDeliveryDays = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetMaxDeliveryDays(productList);
                            //feb-1-2010, now we shoud coulf from today ionstant of tomorrowe, so insteads of changin glogic everyehwrre, we will deduct 1 from the lead time in database
                            scheduledDeliveryDays--;
                        }
                    }
                    else
                    {
                        scheduledDeliveryDays = EFundraisingCRMWeb.Components.Server.ManageSaleScreen.GetMaxDeliveryDays(productList);
                        if (!addShipDay) //pvf is one day more
                        {
                            scheduledDeliveryDays--;
                        }
                    }

                }



                if (scheduledDeliveryDays < 1)
                {
                    error = "No business rules have been created for one of the products";
                }


                bool datesAdjusted = false;
                //if user select a date smaller than today, the sch del is ignored and we base ourselves on the sch ship
                if (schDelDate < DateTime.Now)
                {
                    schDelDate = DateTime.MinValue;
                }

                //calculate ship date upon chosen delivery date
                if (schDelDate != DateTime.MinValue)
                {
                    AdjustDates(ref schShipDate, ref schDelDate, haveChippery, haveWFC, scheduledDeliveryDays,CHR);
                    //if user enter a sch del date too early, the dates were reinitialized
                    if (schDelDate > DateTime.MinValue)
                    {
                        datesAdjusted = true;
                    }

                }

                //calculate sch del date based on sch ship date
                if (!(datesAdjusted))
                {
                    if (schShipDate <= DateTime.Now)
                    {
                        schShipDate = efundraising.EFundraisingCRM.ProductBusinessRules.GetNextBusinessDay(DateTime.Now, 1);
                    }

                    //if in package chippery, lead time doesnt apply
                    /*if (!(haveChippery))
                    {
                        schDelDate = EFundraisingCRM.ProductBusinessRules.GetNextBusinessDay(schShipDate,scheduledDeliveryDays);
                    }*/
                   
                     AdjustDatesUpwards(ref schShipDate, ref schDelDate, haveChippery, haveWFC, scheduledDeliveryDays);


                }


                //set payment due 
                if (paymentTermID != short.MinValue)
                {
                    efundraising.EFundraisingCRM.PaymentTerm pt = efundraising.EFundraisingCRM.PaymentTerm.GetPaymentTermByID(paymentTermID);
                    switch (pt.Description)
                    {
                        case "Prepaid": paymentDueDate = DateTime.Now;
                            break;
                        case "Prepaid - check pick-up": paymentDueDate = DateTime.Now;
                            break;
                        case "COD": paymentDueDate = schDelDate;
                            break;
                        case "COD - 30 Days": paymentDueDate = schDelDate.AddDays(30);
                            break;
                        case "30 days net": paymentDueDate = schDelDate.AddDays(30);
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: Calculate New Dates" + CHRerror, ex);
            }

            return error;

        }


        //check if wfc is in the list of products, if so, cannot be delivered on monday
        //check if has package chippery--special rule
        private static void AdjustDates(ref DateTime schShipDate, ref DateTime schDelDate, bool haveChippery, bool haveWFC, int maxBusinessDays, bool CHR)
        {

            if (haveWFC && !CHR)
            {
                if (schDelDate.DayOfWeek == DayOfWeek.Monday)
                {
                    schDelDate = ProductBusinessRules.GetNextBusinessDay(schDelDate, 1);
                }
            }


            //the haveChippery overrules haveWFC (longer del time)
            if (haveChippery)
            {
                DateTime nextSunday = DateTime.Now;
                int sundays = 0;
                //get the 3rd following sunday
                for (int i = 0; i < 21; i++)
                {
                    nextSunday = nextSunday.AddDays(1);
                    if (nextSunday.DayOfWeek == DayOfWeek.Sunday)
                    {
                        sundays++;
                        if (sundays == 3)
                        {
                            i = 21;
                        }
                    }
                }

                //check if the 3rd sunday from now is earlier then the date selected
                //if it is, we will take the following sunday from the date selected
                if (schDelDate > nextSunday)
                {
                    //get the following sunday
                    for (int i = 0; i < 7; i++)
                    {
                        nextSunday = nextSunday.AddDays(1);
                        if (nextSunday.DayOfWeek == DayOfWeek.Sunday)
                        {
                            i = 7;
                        }
                    }
                }

                //if date chosen if smaller then the 3rd sunday, it will get overriden
                schDelDate = nextSunday;

            }

            //calculate sch ship date
            //if chippery count diffenrent
            if (haveChippery)
            {
                //count previous 3 mondays
                DateTime previousMonday = schDelDate;
                int mondays = 0;
                //get the 3rd previous monday
                for (int i = 0; i < 14; i++)
                {
                    previousMonday = previousMonday.AddDays(-1);
                    if (previousMonday.DayOfWeek == DayOfWeek.Monday)
                    {
                        mondays++;
                        if (mondays == 2)
                        {
                            i = 14;
                        }
                    }
                }
                schShipDate = previousMonday;
            }
            else
            {
                schShipDate = efundraising.EFundraisingCRM.ProductBusinessRules.GetPreviousBusinessDay(schDelDate, maxBusinessDays);
                //if ship date is today or prior, we set it to tomorrow
                if (schShipDate <= DateTime.Now)
                {
                    //recalculate all by setting sch ship date to tomorrow and erasing sch del date
                    schDelDate = DateTime.MinValue;
                    schShipDate = ProductBusinessRules.GetNextBusinessDay(DateTime.Now, 1);
                }
            }

        }

        private static void AdjustDatesUpwards(ref DateTime schShipDate, ref DateTime schDelDate, bool haveChippery, bool haveWFC, int maxBusinessDays)
        {

            if (haveWFC)
            {
                if (schShipDate.DayOfWeek == DayOfWeek.Monday)
                {
                    schShipDate = ProductBusinessRules.GetNextBusinessDay(schShipDate, 1);
                }
            }


            //the haveChippery overrules haveWFC (longer del time)
            if (haveChippery)
            {
                //sch ship must be a monday
                //get the next one

                DateTime nextMonday = schShipDate;
                //get the next monday
                for (int i = 0; i < 7; i++)
                {
                    nextMonday = nextMonday.AddDays(1);
                    if (nextMonday.DayOfWeek == DayOfWeek.Monday)
                    {
                        i = 7;
                    }
                }
                schShipDate = nextMonday;

                DateTime nextSunday = DateTime.Now;
                int sundays = 0;
                //get the 3rd following sunday
                for (int i = 0; i < 21; i++)
                {
                    nextSunday = nextSunday.AddDays(1);
                    if (nextSunday.DayOfWeek == DayOfWeek.Sunday)
                    {
                        sundays++;
                        if (sundays == 3)
                        {
                            i = 21;
                        }
                    }
                }

                //if date chosen if smaller then the 3rd sunday, it will get overriden
                schDelDate = nextSunday;
            }

            else
            {
                //if ship date is today or prior, we set it to tomorrow
                if (schShipDate <= DateTime.Now)
                {
                    schShipDate = ProductBusinessRules.GetNextBusinessDay(DateTime.Now, 1);
                }
                schDelDate = ProductBusinessRules.GetNextBusinessDay(schShipDate, maxBusinessDays);
            }

        }

    }
}
