using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Globalization;
using System.Transactions;
using System.Configuration;



namespace GA.BDC.Data.DataLayer
{
    public class EZPayments
    {

        public static List<AR_TRNS_TBL> GetEZPaymentsToProcess(EZMainCloudDataContext dc, int saleInProcessSAP)
        {
            return (from p in dc.AR_TRNS_TBLs
                    join s in dc.ORDR_INVOIC_TO_PROCESS_TBLs on p.ORDR_ID equals s.ORDR_ID
                    where s.ext_sales_status_id != saleInProcessSAP
                    && s.ext_sales_status_id != null
                    && s.ext_order_id > 0
                    && p.PAYMENT_STATUS_ID == null
                    && p.TRNS_TYPE_CDE == "PMT"
                    && p.PMT_METH_TYPE_CDE != null
                    && p.PMT_METH_TYPE_CDE != "CK"
                    orderby p.ORDR_ID descending
                    select p).ToList();
        }

        public static AR_TRNS_TBL GetEZPaymentMethod(string paymentMethodId)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from pm in dc.AR_TRNS_TBLs where pm.PMT_METH_TYPE_CDE == paymentMethodId select pm).FirstOrDefault();

            }
        }

        //public static AR_TRNS_TBL GetEZPayment(int orderId, int paymentNo)
        //{
        //    using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
        //    {
        //        return (from p in dc.AR_TRNS_TBLs where p.ORDR_ID == orderId select p).FirstOrDefault();

        //    }
        //}

        public static AR_TRNS_TBL GetEZPayment(int orderId)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from p in dc.AR_TRNS_TBLs where p.ORDR_ID == orderId && p.TRNS_TYPE_CDE == "PMT" select p).FirstOrDefault();

            }
        }


        public static List<AR_TRNS_TBL> GetEZPayments(int orderId)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from p in dc.AR_TRNS_TBLs where p.ORDR_ID == orderId && p.TRNS_TYPE_CDE == "PMT" select p).ToList();
            }
        }

      //public static Payment_status GetPaymentStatusByDesc(string input)
      //{
      //    using (EZMainDataContext dc = new EZMainDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
      //    {
      //        return (from ps in dc.Payment_status where ps.Description.ToLower() == input.ToLower() select ps).FirstOrDefault();
      //    }
      //}

      public static void UpdatePayment(AR_TRNS_TBL input)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                AR_TRNS_TBL _payment = (from p in dc.AR_TRNS_TBLs where p.ORDR_ID == input.ORDR_ID && p.TRNS_TYPE_CDE == "PMT" select p).FirstOrDefault();
                if (_payment != null)
                {
                    _payment.EXT_PAYMENT_ID = input.EXT_PAYMENT_ID;
                    _payment.LAST_MODF_DTE = input.TRNS_DTE;
                    _payment.PAYMENT_STATUS_ID = input.PAYMENT_STATUS_ID;

                }

                try
                {
                    dc.SubmitChanges();

                }
                catch (ChangeConflictException)
                {
                    dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                }
            }
        }

      //public static AR_TRNS_TBL GetPaymentMethodByDesc(string desc)
      //{


      //    using (EZMainDataContext dc = new EZMainDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
      //    {
      //        return (from pm in dc.AR_TRNS_TBLs where pm.description.ToLower().Trim() == desc.ToLower().Trim() select pm).FirstOrDefault();

      //    }


      //}




      public static void InsertPayment(AR_TRNS_TBL input)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                using (var transaction = new TransactionScope())
                {
                    


                    dc.AR_TRNS_TBLs.InsertOnSubmit(input);
                    
                    try
                    {
                        dc.SubmitChanges();
                    }
                    catch (ChangeConflictException)
                    {
                        dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                    }
                    transaction.Complete();
                }
            }
        }

      //public static void InsertAdjustment(ORDR_DISC_TBL input)
      //{
      //    using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
      //    {

      //        dc.ORDR_DISC_TBLs.InsertOnSubmit(input);
      //        try
      //        {
      //            dc.SubmitChanges();
      //        }
      //        catch (ChangeConflictException)
      //        {
      //            dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
      //        }

      //    }
      //}


      public static void UpdateAdjustment(ORDR_DISC_TBL input)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                ORDR_DISC_TBL _adj = (from a in dc.ORDR_DISC_TBLs where a.ORDR_ID == input.ORDR_ID && a.DISC_SEQ_NBR == input.DISC_SEQ_NBR select a).FirstOrDefault();
                if (_adj != null)
                {
                    if (input.Ext_Adjustment_Id > 0)
                    {
                        _adj.Ext_Adjustment_Id = input.Ext_Adjustment_Id;
                    }

                    //_adj.Reason_ID = input.Reason_ID;
                    _adj.DISC_AMT = input.DISC_AMT;
                   // _adj.Adjustment_Amount = input.Adjustment_Amount;
                   // _adj.Create_Date = input.Create_Date;
                }

                try
                {
                    dc.SubmitChanges();

                }
                catch (ChangeConflictException)
                {
                    dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                }

            }
        }

      //public static double? GetProfit(int saleId, int scrathBookId)
      //{
      //    using (EZMainDataContext dc = new EZMainDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
      //    {
      //        return dc.fct_sale_item_profit(saleId, scrathBookId);
      //    }
      //}


   }
}
