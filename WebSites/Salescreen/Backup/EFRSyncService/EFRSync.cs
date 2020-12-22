using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
using efundraising.EFundraisingCRM;
using efundraising.EFundraisingCRM.Linq;
using System.Threading;
using efundraising.Diagnostics;

using System.Net.Mail;

namespace EFRSyncService
{
    class EFRSync
    {
        DateTime lastSyncDate;
        bool serviceIsActive = false;
        int syncId;
        
        private void Log(string info)
        {
            LogSimple.Log(info, syncId);
        }

        public void Start()
        {
            try
            {

                serviceIsActive = true;
                
                while (serviceIsActive)
                {
                    //check time to run at 7 am only
                    bool timetoRun = false;
                    int hour = DateTime.Now.Hour;
                    int start_hour;

                    try
                    {
                        start_hour = Convert.ToInt32(ConfigurationManager.AppSettings["StartTime"]);
                    }
                    catch (Exception ex)
                    {start_hour = 7;}
                        
                        if (hour == start_hour)
                        {
                            //get new id
                            Random rand = new Random();
                            syncId = rand.Next(0, 9999);

                            string v = ConfigurationManager.AppSettings["version"];
                            DateTime currentSyncTime = DateTime.Now;
                            Log("<EFRSync Start_Time = '" + currentSyncTime + " V" + v + " '>");


                            lastSyncDate = DateTime.MaxValue;
                            string msg = efundraising.EFundraisingCRM.SyncServiceLog.GetLatestSyncDate(ref lastSyncDate);
                            //
                            //  lastSyncDate = new DateTime(2010,8,1);

                            if (msg != "")
                            {
                                Log("Error getting Latest Sync Date " + msg);
                                Log("</EFRSync>");
                                Stop(msg);
                            }

                            msg = efundraising.EFundraisingCRM.SyncServiceLog.InsertSyncLog(ref currentSyncTime);


                            if (msg == "")
                            {
                                //
                                Log("<Sale_Values>");
                                msg = SyncSaleValues();
                                Log("</Sale_Values>");

                                if (msg == "")
                                {
                                    Log("<Payment_Values>");
                                    msg = SyncPaymentValues();
                                    Log("</Payment_Values>");
                                }

                                if (msg == "")
                                {
                                    Log("<Adjustment_Values>");
                                    msg = SyncAdjustmentValues();
                                    Log("</Adjustment_Values>");
                                }

                                /*  if (msg == "")
                                  {
                                      Log("<Invoice_Values>");
                                      msg = SyncInvoiceValues();
                                      Log("</Invoice_Values>");
                                  }*/

                                if (msg == "")
                                {
                                    //SET SYNC AS Successful
                                    msg = efundraising.EFundraisingCRM.SyncServiceLog.SetSyncLogByDate(true, currentSyncTime);
                                    if (msg != "")
                                    {

                                        Log("Error setting sync as successful " + msg);

                                        Log("</EFRSync>");
                                        Stop(msg);
                                    }
                                    //
                                }
                                else
                                {

                                    Log("Error Syncing values : " + msg);

                                    Log("</EFRSync>");
                                    Stop(msg);
                                }

                                Log("</EFRSync>");


                            }
                            else
                            {
                                Log("Error inserting sync time " + msg);
                                Log("</EFRSync>");
                                Stop(msg);
                            }







                        }

                       
                        //7 oclock
                        // wait for 59 min
                        int min = Convert.ToInt32(ConfigurationManager.AppSettings["SleepTime"]);
                        TimeSpan span = new TimeSpan(0, min, 0);
                        Thread.Sleep(span);
                        
                   
                }//while active
            }

            catch (Exception x)
            {
                SendMail(x.Message,0,"", "");
            }
        }

        public void Stop(string msg)
        {
            serviceIsActive = false;
            if (msg != "")
            {
                throw new Exception();
                //SEND ERROR EMAIL
            }
        }

        private string SyncAdjustmentValues()
        {
            string msg = "";
            
            try
            {
                //get latest adjustments
                List<efundraising.EFundraisingCRM.Fulfillment.AdjustmentInvoiceResult> adjustments = efundraising.EFundraisingCRM.Fulfillment.AdjustmentInvoice.GetNewAdjustments(lastSyncDate);
                int adjFound = adjustments.Count;
                foreach (efundraising.EFundraisingCRM.Fulfillment.AdjustmentInvoiceResult adj in adjustments)
                {
                    //get sale id based on the order in the invocie
                    int orderId = adj.OrderId;
                    efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo =
                             new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase(true);
                    sale s = Sale.GetSaleByOEID(orderId, dbo);
                    if (s != null)
                    {

                        Log("<Sale_Found SaleID = '" + s.sales_id + "'>");

                    //    if (s.sales_id == 134124)
                      //  {

                            //get next adj No
                            int adjNo = efundraising.EFundraisingCRM.Adjustment.GetAdjustmentLatestNo(s.sales_id, dbo);
                            adjNo++;
                         
                            //get reason that need to become a negative payment
                            string[] reasonIds = ConfigurationManager.AppSettings["AdjustmentToBecomePayment"].ToString().Split(',');
                            bool isPayment = false;
                            foreach (string id in reasonIds)
                            {
                                if (adj.AdjustmentTypeId.ToString() == id)
                                {
                                    isPayment = true;
                                }
                            }

                            if (isPayment)
                            {
                                InsertPaymentFromAdjustment(s, adj, dbo);
                            }
                            else
                            {
                                //get adj reason
                                int adjTypeId = efundraising.EFundraisingCRM.Reason.GetReasonIdByAdjustmentTypeId(adj.AdjustmentTypeId ?? 0);
                            

                               efundraising.EFundraisingCRM.Linq.Adjustment a = new efundraising.EFundraisingCRM.Linq.Adjustment();
                                a.Sales_ID = s.sales_id;
                                a.Adjustment_No = adjNo;
                                a.Reason_ID = Convert.ToByte(adjTypeId);
                                a.Adjustment_Date = adj.AdjustmentDate ?? DateTime.MaxValue;
                                a.Adjustment_Amount = -adj.AdjustmentAmount;
                                a.Adjustment_On_Shipping = 0;
                                a.Adjustment_On_Taxes = 0;
                                a.Adjustment_On_Sale_Amount = -adj.AdjustmentAmount;
                                int extId = 0;
                                string acctId = adj.AccountId.ToString();
                                if (adj.SequenceNo > 9)
                                {
                                    string id = acctId.Substring(1, acctId.Length - 1);
                                    extId = Convert.ToInt32(id + adj.SequenceNo.ToString());

                                }
                                else
                                {
                                    extId = Convert.ToInt32(adj.AccountId.ToString() + adj.SequenceNo.ToString());
                                }

                                //a.Ext_Adjustment_Id = extId;
                                a.Ext_Adjustment_Id = adj.AdjustmentId;
                                a.Create_Date = DateTime.Now;
                                a.Create_User_ID = Convert.ToInt32(ConfigurationManager.AppSettings["createUserId"]);


                                int newSalesId = 0;
                                int newAdjNo = 0;


                                string d = a.Adjustment_Date.ToString();
                                int pos = d.IndexOf(" ");
                                string temp = d.Substring(0, pos);
                                DateTime newDate = Convert.ToDateTime(temp);

                                //before inserting double check if adjustment was not already created 
                                bool duplicate = efundraising.EFundraisingCRM.Adjustment.GetAdjustmentDuplicate(extId);
                                efundraising.EFundraisingCRM.Linq.Adjustment AdjustmentsAlreadyInserted = efundraising.EFundraisingCRM.Adjustment.GetAdjustmentDoubleEntry(s.sales_id, adj.AdjustmentAmount, dbo);

                                if (duplicate)
                                {
                                    Log(" Duplicate Found. Ext Id = " + extId);
                                }
                                else if (AdjustmentsAlreadyInserted != null)
                                {
                                    //UPDATE PAYMENT 
                                    Log(" Record already inserted from Sale Screen. Update ext id to " + extId.ToString());
                                    Log(" Update Adj Date from " + AdjustmentsAlreadyInserted.Adjustment_Date.ToString() + " to " + d);
                                    AdjustmentsAlreadyInserted.Ext_Adjustment_Id = extId;
                                    AdjustmentsAlreadyInserted.Adjustment_Date = a.Adjustment_Date;
                                    string result = dbo.SubmitChanges();
                                }
                                else
                                {

                                    string result = dbo.InsertAdjustment(a, ref newSalesId, ref newAdjNo);
                                    if (result != "")
                                    {
                                        msg = " Insert failed on Adjustment. SaleId = " + s.sales_id + " - " + adjNo + " Error: " + result;
                                        Log(msg);
                                    }
                                    else
                                    {
                                        Log(" Success. SaleId = " + newSalesId + " - " + newAdjNo + " Ext ADj ID=" + adj.AdjustmentId.ToString());
                                    }
                                }
                            }

                            Log("</Sale_Found>");
                        }
                        else
                        {
                            Log("<Sale_Not_Found OrderID = '" + orderId + "'/>");
                        }

                  //  }///

                }
           }catch(Exception x){
                msg = x.Message;
            }
            
            if (msg != ""){
               return "Error in SyncAdjustmentValues" + msg;
            }else{
               return msg;
            }
        }


        private string SyncInvoiceValues()
        {
            string msg = "";

            try
            {
                //get latest adjustments
                List<efundraising.EFundraisingCRM.Fulfillment.InvoiceResult> invoices = efundraising.EFundraisingCRM.Fulfillment.PaymentInvoice.GetNewNegativeInvoice(lastSyncDate);
                int invoiceFound =  invoices.Count;
                foreach (efundraising.EFundraisingCRM.Fulfillment.InvoiceResult invoice in invoices)
                {
                    //get sale id based on the order in the invocie
                    int orderId = invoice.OrderId;
                    efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo =
                             new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase(true);
                    sale s = Sale.GetSaleByOEID(orderId, dbo);
                    if (s != null)
                    {
                        Log("<Sale_Found SaleID = '" + s.sales_id + "'>");
                        
                        decimal saleAmount = Convert.ToDecimal(s.total_amount);
                        decimal adjAmount = efundraising.EFundraisingCRM.Adjustment.GetTotalAdjustmentBySaleID(s.sales_id);
                        decimal total = saleAmount - adjAmount;

                        decimal invoiceAmt = invoice.InvoiceAmount ?? 0;
                        decimal diff = invoiceAmt + total;

                        Log(" Difference between total sale and negative invoice is " + diff.ToString());

                        //create adjustment if diff is not 0, else sale is fully returned
                        if (diff > 1 || diff < -1)
                        {
                            //CREATE ADJUSTMENT
                            //get next adj No
                            int adjNo = efundraising.EFundraisingCRM.Adjustment.GetAdjustmentLatestNo(s.sales_id, dbo);
                            adjNo++;
                            //get adj reason
                            int adjTypeId = Convert.ToInt32(ConfigurationManager.AppSettings["ReasonIdForReturn"]);

                            efundraising.EFundraisingCRM.Linq.Adjustment a = new efundraising.EFundraisingCRM.Linq.Adjustment();
                            a.Sales_ID = s.sales_id;
                            a.Adjustment_No = adjNo;
                            a.Reason_ID = Convert.ToByte(adjTypeId);
                            a.Adjustment_Date = DateTime.Now;
                            a.Adjustment_Amount = invoiceAmt; //negative invoice on qsp become a negative adjustment for efr
                            a.Adjustment_On_Shipping = 0;
                            a.Adjustment_On_Taxes = 0;
                            a.Adjustment_On_Sale_Amount = invoiceAmt;
                            a.Ext_Adjustment_Id = invoice.InvocieId; 
                            a.Create_Date = DateTime.Now;
                            a.Create_User_ID = Convert.ToInt32(ConfigurationManager.AppSettings["createUserId"]);



                            int newSalesId = 0;
                            int newAdjNo = 0;
                            //before inserting double check if adjustment was not already created
                            bool duplicate = efundraising.EFundraisingCRM.Adjustment.GetAdjustmentDuplicate(invoice.InvocieId);
                            if (duplicate)
                            {
                                Log(" Duplicate Found. Ext Id = " + invoice.InvocieId);
                            }
                            else
                            {
                                string result = dbo.InsertAdjustment(a, ref newSalesId, ref newAdjNo);
                                if (result != "")
                                {
                                    msg = " Insert failed on Return Adjustment. SaleId = " + s.sales_id + " - " + adjNo + " Error: " + result;
                                    Log(msg);
                                }
                                else
                                {
                                    Log(" Success on inserting Return Adjustment. SaleId = " + newSalesId + " - " + newAdjNo);
                                }
                            }

                        }
                        else
                        {
                            s.box_return_date = DateTime.Now;
                            s.ar_status_id = Convert.ToInt32(ConfigurationManager.AppSettings["NegativeInvoiceStatus"]);

                            string result = dbo.SubmitChanges();
                            if (result != "")
                            {
                                msg = " Update failed on Sale's box return date. Error: " + result;
                                Log(msg);
                            }
                            else
                            {
                                Log(" Success: Updated sale with box return date = " + invoice.InvoiceDate.ToString());
                            }
                        }

                        Log("</Sale_Found>");
                    }
                    else
                    {
                        Log("</Sale_Not_Found OrderID = '" + orderId + "'>");
                    }



                }
            }
            catch (Exception x)
            {
                msg = x.Message;
            }

            if (msg != "")
            {
                return "Error in SyncInvoiceValues" + msg;
            }
            else
            {
                return msg;
            }
        }



        private string SyncPaymentValues()
        {
            
            string msg = "";

            try
            {   

                //get latest payments
                List<efundraising.EFundraisingCRM.Fulfillment.PaymentInvoiceResult> payments = efundraising.EFundraisingCRM.Fulfillment.PaymentInvoice.GetNewPayments(lastSyncDate);
                int paymentFound = payments.Count;
                foreach (efundraising.EFundraisingCRM.Fulfillment.PaymentInvoiceResult payment in payments)
                {

                    //get sale id based on the order in the invocie
                    int orderId = payment.OrderId;
                
                    efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase(true);
                    sale s = Sale.GetSaleByOEID(orderId, dbo);
                    if (s != null)
                    {
                        Log("<Sale_Found SaleID = '" + s.sales_id + "'>");

          
                        //get next paymetn No
                        int paymentNo = efundraising.EFundraisingCRM.Payment.GetPaymentLatestNo(s.sales_id, dbo);
                        paymentNo++;
                        //get payment method
                        int paymentMethodId = efundraising.EFundraisingCRM.Payment.GetPaymentMethodIdByPaymentTypeId(payment.PaymentTypeId ?? 0);


                        efundraising.EFundraisingCRM.Linq.payment p = new efundraising.EFundraisingCRM.Linq.payment();
                        p.sales_id = s.sales_id;
                        p.payment_no = paymentNo;
                        p.payment_method_id = Convert.ToByte(paymentMethodId);
                        p.collection_status_id = Convert.ToInt32(ConfigurationManager.AppSettings["CollectionStatus"]); //set collection status to check in house
                        p.payment_entry_date = payment.PaymentDate ?? DateTime.MaxValue;
                        p.payment_amount = -payment.PaymentAmount;
                        p.cashable_date = payment.PaymentDate ?? DateTime.MaxValue;
                        int extId = 0;
                        string acctId = payment.AccountId.ToString();
                        if (payment.SequenceNo > 9)
                        {
                            string id = acctId.Substring(1, acctId.Length - 1);
                            extId = Convert.ToInt32(id + payment.SequenceNo.ToString());

                        }
                        else
                        {
                            extId = Convert.ToInt32(payment.AccountId.ToString() + payment.SequenceNo.ToString());
                        }
                        p.ext_payment_id = extId;
                        p.create_date = DateTime.Now;
                        p.create_user_id = Convert.ToInt32(ConfigurationManager.AppSettings["createUserId"]);

                        int newSalesId = 0;
                        int newPaymentNo = 0;

                        string d = p.payment_entry_date.ToString();
                        int pos = d.IndexOf(" ");
                        string temp = d.Substring(0, pos);
                        DateTime newDate = Convert.ToDateTime(temp);

                        //before inserting double check if payment was not already created
                        bool duplicate = efundraising.EFundraisingCRM.Payment.GetPaymentDuplicate(extId);
                        efundraising.EFundraisingCRM.Linq.payment PaymentAlreadyInserted = efundraising.EFundraisingCRM.Payment.GetPaymentDoubleEntry(s.sales_id, payment.PaymentAmount, dbo);
                        if (duplicate)
                        {
                            Log(" Duplicate Found. Ext Id = " + extId);
                        }
                        else if (PaymentAlreadyInserted != null)
                        {
                            //UPDATE PAYMENT 
                            Log(" Record already inserted from Sale Screen. Update ext id to " + extId.ToString());
                            Log(" Update Payment Date from " + PaymentAlreadyInserted.payment_entry_date.ToString() + " to " + d);
                            PaymentAlreadyInserted.ext_payment_id = extId;
                            PaymentAlreadyInserted.payment_entry_date = p.payment_entry_date;
                            string result = dbo.SubmitChanges();
                        }
                        else
                        {

                            string result = dbo.InsertPayment(p, ref newSalesId, ref newPaymentNo);
                            if (result != "")
                            {
                                msg = " Update failed on Payment. SaleId = " + s.sales_id + " - " + paymentNo + " Error: " + result;
                                Log(msg);
                            }
                            else
                            {
                                Log(" Success. SaleId = " + newSalesId + " - " + newPaymentNo + " Ext ID = " + extId.ToString());
                            }
                        }

               

                        Log("</Sale_Found>");
                    }
                    else
                    {
                        Log("<Sale_Not_Found OrderID = '" + orderId + "'/>");
                    }



                }//for each payment
                 
            }catch(Exception x){
                msg = x.Message;
            }
            
            if (msg != ""){
               return "Error in SyncPaymentValues" + msg;
            }else{
                return msg;
            }

        }

        private string InsertPaymentFromAdjustment(sale s, efundraising.EFundraisingCRM.Fulfillment.AdjustmentInvoiceResult adj, efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo)
        {
            string msg = "";

            //get next paymetn No
            int paymentNo = efundraising.EFundraisingCRM.Payment.GetPaymentLatestNo(s.sales_id, dbo);
            paymentNo++;
            

            efundraising.EFundraisingCRM.Linq.payment p = new efundraising.EFundraisingCRM.Linq.payment();
            p.sales_id = s.sales_id;
            p.payment_no = paymentNo;
            p.payment_method_id = 1;
            p.collection_status_id = Convert.ToInt32(ConfigurationManager.AppSettings["CollectionStatus"]); //set collection status to check in house
            p.payment_entry_date = adj.AdjustmentDate ?? DateTime.MaxValue;
            p.payment_amount = -adj.AdjustmentAmount;
            p.cashable_date = adj.AdjustmentDate ?? DateTime.MaxValue;
            int extId = 0;
            string acctId = adj.AccountId.ToString();
            if (adj.SequenceNo > 9)
            {
                string id = acctId.Substring(1, acctId.Length - 1);
                extId = Convert.ToInt32(id + adj.SequenceNo.ToString());

            }
            else
            {
                extId = Convert.ToInt32(adj.AccountId.ToString() + adj.SequenceNo.ToString());
            }
            p.ext_payment_id = extId;
            p.create_date = DateTime.Now;
            p.create_user_id = Convert.ToInt32(ConfigurationManager.AppSettings["createUserId"]);

            int newSalesId = 0;
            int newPaymentNo = 0;

            string d = p.payment_entry_date.ToString();
            int pos = d.IndexOf(" ");
            string temp = d.Substring(0, pos);
            DateTime newDate = Convert.ToDateTime(temp);

            //before inserting double check if payment was not already created
            bool duplicate = efundraising.EFundraisingCRM.Payment.GetPaymentDuplicate(extId);
            efundraising.EFundraisingCRM.Linq.payment PaymentAlreadyInserted = efundraising.EFundraisingCRM.Payment.GetPaymentDoubleEntry(s.sales_id, adj.AdjustmentAmount, dbo);
            if (duplicate)
            {
                Log(" (Adj2Payment) Duplicate Found. Ext Id = " + extId);
            }
            else if (PaymentAlreadyInserted != null)
            {
                //UPDATE PAYMENT 
                Log(" (Adj2Payment) Record already inserted from Sale Screen. Update ext id to " + extId.ToString());
                Log(" (Adj2Payment) Update Payment Date from " + PaymentAlreadyInserted.payment_entry_date.ToString() + " to " + d);
                PaymentAlreadyInserted.ext_payment_id = extId;
                PaymentAlreadyInserted.payment_entry_date = p.payment_entry_date;
                string result = dbo.SubmitChanges();
            }
            else
            {

                string result = dbo.InsertPayment(p, ref newSalesId, ref newPaymentNo);
                if (result != "")
                {
                    msg = " (Adj2Payment) Update failed on Payment. SaleId = " + s.sales_id + " - " + paymentNo + " Error: " + result;
                    Log(msg);
                }
                else
                {
                    Log(" (Adj2Payment) Success. SaleId = " + newSalesId + " - " + newPaymentNo + " Ext ID = " + extId.ToString());
                }
            }
            
            return msg;

        }

        //sync ship date and tracking no
        private string SyncSaleValues()
        {
            string msg = "";
            try
            {
                //****Update SALE TABLE/ 
                /*1- Update Actual Ship date and waybull in SALE
                     get all shipment groups that were updated since last update was run
                      inner jouin oprder detail with source 20 (need info for first item only)
        */

                //this forces the datacontect to push the changes even if it thinks nothing has chnaged
                //ctx.DataContext.InvoiceItems.Attach(data, true);


                //get all shipment groups that were updated since last update was run
                // inner jouin oprder detail with source 20 (need info for first item only)
                List<efundraising.EFundraisingCRM.Fulfillment.ShipmentGroupsResult> shipmentGroups = efundraising.EFundraisingCRM.Fulfillment.ShipmentGroup.GetShipmentGroupsUpdated(lastSyncDate);

                foreach (efundraising.EFundraisingCRM.Fulfillment.ShipmentGroupsResult ship in shipmentGroups)
                {
                    DateTime? shipDate = ship.ShipDate;
                    int orderId = ship.OrderId;
                    string trackingNo = ship.TrackingNo.Trim() + "-";
                    
                    //if last char is a -, remove it
                    int len = trackingNo.Length;
                    int pos = trackingNo.LastIndexOf('-');
                    if (len == (pos + 1))
                    {
                        trackingNo = trackingNo.Substring(0, len - 1);
                    }

                    //get sale
                    efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase(true);
                    efundraising.EFundraisingCRM.Linq.sale s = efundraising.EFundraisingCRM.Sale.GetSaleByOEID(orderId, dbo);
                    
                    if (s != null)
                    {
                        bool isShipped = false;
                        Log("<Sale_Found SaleID = '" + s.sales_id + "'>");
                        if (s.actual_ship_date != shipDate)
                        {
                            isShipped = true;
                            Log("Update Ship Date: " + s.actual_ship_date + " to " + shipDate);
                        }
                        if (s.waybill_no != trackingNo)
                        {
                            Log("Update Tracking #: " + s.waybill_no + " to " + trackingNo);
                        }

                        //update sale
                        s.actual_ship_date = shipDate;
                        s.waybill_no = trackingNo;
                        if (isShipped)
                        {
                            s.production_status_id = 4; //stock shipped
                        }

                        string result = dbo.SubmitChanges();
                        if (result != "")
                        {
                            msg = "-Update failed on Sale. SaleId = " + orderId + " Error: " + result;
                            Log(msg);
                        }
                        else
                        {
                            Log("-Success");
                        }
                        Log("</Sale_Found>");
                    }
                    else
                    {
                        Log("<Sale_Not_Found OrderID = '" + orderId + "'/>");
                    }
                }//for each

            }
            catch (Exception x)
            {
                msg = x.Message;
            }

            if (msg != "")
            {
                return "Error in SyncSaleValues" + msg;
            }
            else
            {
                return msg;
            }
        }

        public static void SendMail(string body, int saleID, string host, string dept)
        {

            string to = "";
 

            SmtpClient smtpClient = new SmtpClient();
            MailMessage Mail = new MailMessage();

            Mail.From = new MailAddress("EFRSync@qsp.com", "EFR Sync");

            to = ConfigurationManager.AppSettings["errorEmail"].ToString();
                Mail.Subject = "AR Sync Error";
       

            Mail.To.Add(to);

            Mail.Body = body;
            Mail.IsBodyHtml = true;

            smtpClient.Host = "outgoingsmtp";
            smtpClient.Send(Mail);

        }

/*
        private void DeletePaymentsClearedOnOE(int saleId)
        {
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase(true);

            List<efundraising.EFundraisingCRM.Linq.payment> payments =
                        efundraising.EFundraisingCRM.Payment.GetExternalPayments(saleId , dbo);

            foreach (efundraising.EFundraisingCRM.Linq.payment p in payments){
                int extId = p.ext_payment_id ?? 0;
                if (extId > 0)
                {
                    //check if still existe on OE
                    efundraising.EFundraisingCRM.Fulfillment.PaymentInvoiceResult payment = efundraising.EFundraisingCRM.Fulfillment.PaymentInvoice.GetPayment(extId);

                    if (payment == null)
                    {
                        p.payment_amount = 0;
                        dbo.SubmitChanges();
                    }
                }
            }
            

        }
    */


    }
}
