using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Transactions;
using System.Configuration;


namespace GA.BDC.Data.DataLayer
{

    public class EZOrders
    {
        public static List<ORDR_INVOIC_TO_PROCESS_TBL> GetEZOrdersToProcess(EZMainCloudDataContext dc, string EZSaleConfirmed, int EZSaleIdCutOver, int productClassOmit, int maxOrdersInBatch, int? inHouseSaleStatus, int? saleInProcessSAP)
        {
            List<ORDR_INVOIC_TO_PROCESS_TBL> salesToProcess = new List<ORDR_INVOIC_TO_PROCESS_TBL>();
            salesToProcess = (from s in dc.ORDR_INVOIC_TO_PROCESS_TBLs where s.LAST_STAT_CDE == EZSaleConfirmed && s.ext_order_id == 0 && s.ORDR_ID > EZSaleIdCutOver && s.ext_sales_status_id == null orderby s.ORDR_ID select s).Take(maxOrdersInBatch).ToList<ORDR_INVOIC_TO_PROCESS_TBL>();
            
                if (salesToProcess.Count > 0)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        salesToProcess.ForEach(sp => sp.ext_sales_status_id = saleInProcessSAP);
                        try
                        {
                            dc.SubmitChanges();

                        }
                        catch (ChangeConflictException)
                        {
                            dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                        }
                        ts.Complete();
                    }
                }
            

            return salesToProcess;

        }

        public static List<ORDR_DISC_TBL> GetAdjustmentsPerSale(int saleId)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from a in dc.ORDR_DISC_TBLs where a.ORDR_ID == saleId select a).ToList<ORDR_DISC_TBL>();

            }
        }
        

        public static ORDR_INVOIC_TO_PROCESS_TBL GetClientOrder(int orderId)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from c in dc.ORDR_INVOIC_TO_PROCESS_TBLs where c.ORDR_ID == orderId orderby c.ORDR_ID descending select c).FirstOrDefault();

            }
        }



        public static ITEM_LKUP_TBL GetScratchBook(string itemId)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from sb in dc.ITEM_LKUP_TBLs where sb.ITEM_CDE == itemId select sb).FirstOrDefault();

            }
        }

        public static List<ORDR_ITEM_TBL> GetSaleItems(int orderId)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {

                //var orderVendor = dc.ORDR_VEND_TBLs.First(p => p.ORDR_ID == orderId);
                //var orderItems = dc.ORDR_ITEM_TBLs.Where(p => p.ORDR_SUB_ID == orderVendor.ORDR_SUB_ID);

                //return orderItems.ToList();

                return (from p in dc.ORDR_VEND_TBLs
                        from s in dc.ORDR_ITEM_TBLs
                        where p.ORDR_SUB_ID == s.ORDR_SUB_ID
                       && p.ORDR_ID == orderId && s.ITEM_PO_QTY > 0 && p.SRC_GRP != "PRZP"
                        select s).ToList<ORDR_ITEM_TBL>();

                //return (from p in dc.ORDR_VEND_TBLs
                //        join s in dc.ORDR_ITEM_TBLs on p.ORDR_SUB_ID equals s.ORDR_SUB_ID
                //        where p.ORDR_ID == orderId
                //        select s).ToList<ORDR_ITEM_TBL>();

            }
        }


        public static ORG_MSTR_TBL GetOrgInfo(int? orgId)
        {
            using (var EZMainCloudDataContext = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from s in EZMainCloudDataContext.ORG_MSTR_TBLs where s.ORG_ID == orgId select s).FirstOrDefault();

            }
        }




        public static ORDR_INVOIC_TO_PROCESS_TBL GetSale(int? orderId)
        {
            using (var EZMainCloudDataContext = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from s in EZMainCloudDataContext.ORDR_INVOIC_TO_PROCESS_TBLs where s.ORDR_ID == orderId select s).FirstOrDefault();

            }
        }



        /// <summary>
        /// Returns the sale found
        /// </summary>
        /// <param name="EZMainCloudDataContext"></param>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public static ORDR_INVOIC_TO_PROCESS_TBL GetSale(EZMainCloudDataContext EZMainCloudDataContext, int? orderID)
        {
            return (from s in EZMainCloudDataContext.ORDR_INVOIC_TO_PROCESS_TBLs
                    where s.ORDR_ID == orderID
                    select s).FirstOrDefault();
        }



        public static void ConfirmSale(ORDR_INVOIC_TO_PROCESS_TBL input, int? shipped, int? saleInSAPWithPay)
        {
            using (EZMainCloudDataContext dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                ORDR_INVOIC_TO_PROCESS_TBL sale = (from s in dc.ORDR_INVOIC_TO_PROCESS_TBLs where s.ORDR_ID == input.ORDR_ID select s).SingleOrDefault();
                if (sale != null)
                {

                    if (input.PDCT_SHIP_DTE != null)
                    {
                        sale.PDCT_SHIP_DTE = input.PDCT_SHIP_DTE;
                        sale.ext_sales_status_id = shipped;
                    }


                    if (input.ext_order_id != null)
                    {
                        sale.ext_order_id = input.ext_order_id;
                        if (sale.ext_sales_status_id != shipped)
                        {
                            sale.ext_sales_status_id = saleInSAPWithPay;
                        }
                    }

                    if (!string.IsNullOrEmpty(input.PDCT_SHIP_TRACK_NBR))
                    {
                        sale.PDCT_SHIP_TRACK_NBR = input.PDCT_SHIP_TRACK_NBR;
                    }

                    if (input.PDCT_SHIP_VIA_CDE != null)
                    {
                        sale.PDCT_SHIP_VIA_CDE = input.PDCT_SHIP_VIA_CDE;
                    }
                    //if (input.ext_billing_account_id != null)
                    //{
                    //    sale.ext_billing_account_id = input.ext_billing_account_id;
                    //}
                    //if (input.ext_shipping_account_id != null)
                    //{
                    //    sale.ext_shipping_account_id = input.ext_shipping_account_id;
                    //}
                    try
                    {
                        dc.SubmitChanges();

                    }
                    catch (Exception ex)
                    {
                        var error = ex;
                        dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                    }
                }
            }
        }


        public static ORG_CTCT_TBL GetClientID(int? id)
        {
            using (var dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from c in dc.ORG_CTCT_TBLs where c.ORG_ID == id  && c.CTCT_SEQ_NBR == 1 select c).FirstOrDefault();
            }
        }


        public static ORDR_VEND_TBL GetOrderVendorInfo(int subID)
        {

            using (var dc = new EZMainCloudDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EZMainConnectionString"].ConnectionString))
            {
                return (from c in dc.ORDR_VEND_TBLs where c.ORDR_SUB_ID == subID select c).FirstOrDefault();
            }

        }

    }
}
