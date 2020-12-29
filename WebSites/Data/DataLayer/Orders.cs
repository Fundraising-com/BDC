using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Transactions;
using System.Configuration;


namespace GA.BDC.Data.DataLayer
{

    public class Orders
    {
        public static List<sale> GetFROrdersToProcess(eFundraisingProdDataContext dc, int saleConfirmed, int saleIdCutOver, int productClassOmit, int maxOrdersInBatch, int? inHouseSaleStatus, int? saleInProcessSAP)
        {
            List<sale> salesToProcess = new List<sale>();
            salesToProcess = (from s in dc.sales where s.sales_status_id == saleConfirmed && s.ext_order_id == 0 && s.sales_id > saleIdCutOver && s.ext_sales_status_id == null orderby s.sales_id select s).Take(maxOrdersInBatch).ToList<sale>();
            if (salesToProcess.Count > 0)
            {
                var ommitted = new List<sale>();
                foreach (sale s in salesToProcess)
                {
                    if ((from si in dc.sales_items where si.sales_id == s.sales_id && si.product_class_id == productClassOmit select si).Count() == (from si1 in dc.sales_items where si1.sales_id == s.sales_id select si1).Count()
                       || (from si in dc.sales_items join sb in dc.scratch_books on si.scratch_book_id equals sb.scratch_book_id where sb.InHouse == true && si.sales_id == s.sales_id select si).Count() == (from si1 in dc.sales_items where si1.sales_id == s.sales_id select si1).Count()
                       || ((from si in dc.sales_items where si.sales_id == s.sales_id select (int)si.quantity_sold).Sum() == 0 && (from si in dc.sales_items where si.sales_id == s.sales_id select (int)si.quantity_free).Sum() == 0))
                    {
                        ommitted.Add(s);
                    }

                }
                if (ommitted.Count > 0)
                {
                    ommitted.ForEach(x => { salesToProcess.Remove(x); });
                    ommitted.ForEach(x => x.ext_sales_status_id = inHouseSaleStatus);
                    try
                    {
                        dc.SubmitChanges();
                    }
                    catch (ChangeConflictException)
                    {
                        dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                    }
                }

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
            }

            return salesToProcess;

        }

        public static List<Adjustment> GetAdjustmentsPerSale(int saleId)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from a in dc.Adjustments where a.Sales_ID == saleId select a).ToList<Adjustment>();

            }
        }


        public static List<Adjustment> GetAdjustmentsPerSale(int saleId, int sapAjustment)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from a in dc.Adjustments where a.Sales_ID == saleId && a.Reason_ID != sapAjustment select a).ToList<Adjustment>();

            }
        }


        public static client GetClient(int clientId, string clientSequenceCode)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from c in dc.clients where c.client_id == clientId && c.client_sequence_code.ToUpper() == clientSequenceCode.ToUpper() orderby c.client_id descending select c).FirstOrDefault();

            }
        }

        public static client_address GetClientBillingAddress(int clientId, string clientSequenceCode)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from ca in dc.client_addresses where ca.client_id == clientId && ca.address_type.ToUpper() == "BT".ToString() && ca.client_sequence_code.ToUpper() == clientSequenceCode.ToUpper() orderby ca.address_id descending select ca).FirstOrDefault();

            }
        }

        public static client_address GetClientShippingAddress(int clientId, string clientSequenceCode)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from ca in dc.client_addresses where ca.client_id == clientId && ca.address_type.ToUpper() == "ST".ToString() && ca.client_sequence_code.ToUpper() == clientSequenceCode.ToUpper() orderby ca.address_id descending select ca).FirstOrDefault();

            }
        }

        public static Country GetCountry(string countryCode)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from c in dc.Countries where c.Country_Code == countryCode select c).FirstOrDefault();

            }
        }

        public static State GetState(string countryCode, string stateCode)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from s in dc.States where s.Country_Code == countryCode && s.State_Code == stateCode select s).FirstOrDefault();

            }
        }

        public static product_class GetProductClass(int productClassId)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from pc in dc.product_classes where pc.product_class_id == productClassId select pc).FirstOrDefault();

            }
        }

        public static scratch_book GetScratchBook(int scratchBookId)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from sb in dc.scratch_books where sb.scratch_book_id == scratchBookId select sb).FirstOrDefault();

            }
        }

        public static List<sales_item> GetSaleItems(int saleId)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from si in dc.sales_items where si.sales_id == saleId select si).ToList<sales_item>();

            }
        }

        public static sale GetSale(int saleId)
        {
            using (var eFundraisingProdDataContext = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from s in eFundraisingProdDataContext.sales where s.sales_id == saleId select s).FirstOrDefault();

            }
        }

        /// <summary>
        /// Returns the sale found
        /// </summary>
        /// <param name="eFundraisingProdDataContext"></param>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public static sale GetSale(eFundraisingProdDataContext eFundraisingProdDataContext, int saleId)
        {
            return (from s in eFundraisingProdDataContext.sales 
                    where s.sales_id == saleId 
                    select s).FirstOrDefault();
        }

        public static bool SaleIsPaid(eFundraisingProdDataContext dc, int orderId)
        {
            decimal debit = 0.0m, credit = 0.0m;
            sale _sale = (from s in dc.sales where s.sales_id == orderId select s).FirstOrDefault();
            if (_sale != null)
            {
                debit += _sale.total_amount ?? 0.0M;

            }

            List<payment> _payment = (from p in dc.payments where p.sales_id == orderId && (p.collection_status_id == 8 || p.collection_status_id == 12) select p).ToList<payment>();
            if (_payment != null && _payment.Count > 0)
            {
                credit += _payment.Sum(p => p.payment_amount);
            }

            List<Adjustment> _adjustment = (from a in dc.Adjustments where a.Sales_ID == orderId select a).ToList<Adjustment>();

            if (_adjustment != null && _adjustment.Where(x => x.Adjustment_No == 1).Count() > 0)
                credit += _adjustment.Where(x => x.Adjustment_No == 1).Sum(x => x.Adjustment_Amount);

            if (_adjustment != null && _adjustment.Where(x => x.Adjustment_No == 2).Count() > 0)
                debit += _adjustment.Where(x => x.Adjustment_No == 2).Sum(x => x.Adjustment_Amount);

            var saleIsPaid = credit >= debit;
            return saleIsPaid;
        }

        public static profit_range GetPrfofiteRangeBySchratchbookIdAndQuantity(int scratchbookId, int qua)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {

                return (from sb in dc.scratch_books
                        join pbr in dc.product_business_rules on (int)sb.product_class_id equals pbr.product_class_id
                        join pbrpr in dc.product_business_rule_profit_ranges on pbr.product_business_rule_id equals pbrpr.product_business_rule_id
                        join pr in dc.profit_ranges on pbrpr.profit_range_id equals pr.profit_range_id
                        where sb.scratch_book_id == scratchbookId && pr.item_nbr_min <= qua && pr.item_nbr_max >= qua
                        select pr).FirstOrDefault();
            }
        }

        public static void ConfirmSale(sale input, int? shipped, int? saleInSAPWithPay, int? shippedsalescreen)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                sale sale = (from s in dc.sales where s.sales_id == input.sales_id select s).SingleOrDefault();
                if (sale != null)
                {

                    if (input.actual_ship_date != null)
                    {
                        sale.actual_ship_date = input.actual_ship_date;
                        sale.ext_sales_status_id = shipped;
                        sale.production_status_id = shippedsalescreen;
                    }


                    if (input.ext_order_id != null)
                    {
                        sale.ext_order_id = input.ext_order_id;
                        if (sale.ext_sales_status_id != shipped)
                        {
                            sale.ext_sales_status_id = saleInSAPWithPay;
                        }
                    }

                    if (!string.IsNullOrEmpty(input.waybill_no))
                    {
                        sale.waybill_no = input.waybill_no;
                    }

                    if (input.carrier_id != null)
                    {
                        sale.carrier_id = input.carrier_id;
                    }
                    if (input.ext_billing_account_id != null)
                    {
                        sale.ext_billing_account_id = input.ext_billing_account_id;
                    }
                    if (input.ext_shipping_account_id != null)
                    {
                        sale.ext_shipping_account_id = input.ext_shipping_account_id;
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
        }


        public static carrier GetCarrierBySCAC(string input)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from c in dc.carriers where c.SCAC.ToUpper().Trim() == input.ToUpper().Trim() select c).FirstOrDefault();
            }
        }

        public static int GetSumOfItemsOfSameProductClassWithinSale(int salesId, int scratchBookId)
        {
            using (eFundraisingProdDataContext dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from si in dc.sales_items
                        join sb in dc.scratch_books on si.scratch_book_id equals sb.scratch_book_id
                        join t1 in
                            (from sb1 in dc.scratch_books
                             join si1 in dc.sales_items on sb1.scratch_book_id equals si1.scratch_book_id
                             where si1.sales_id == salesId
                                && sb1.scratch_book_id == scratchBookId
                             select new { sb1.product_class_id }) on sb.product_class_id equals t1.product_class_id
                        where si.sales_id == salesId
                        select si.quantity_sold).Cast<int>().Sum();

            }
        }


        public static consultant GetConsultantByID(int id)
        {
            using (var dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
            {
                return (from c in dc.consultants where c.consultant_id == id select c).FirstOrDefault();
            }
        }

       public static lead GetLeadById(int id)
       {
          if (id < 0)
          {
             return null;
          }
          using (var dc = new eFundraisingProdDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.eFundraisingProdConnectionString"].ConnectionString))
          {
             return (from l in dc.leads where l.lead_id == id select l).FirstOrDefault();
          }
       }

        public static ISingleResult<pap_get_sales_to_be_processed_v2Result> GetPapSalesToProcess(eFundraisingProdDataContext efundraisingProdDataContext)
        {
            return efundraisingProdDataContext.pap_get_sales_to_be_processed_v2();
        }
        /// <summary>
        /// Returns the Product Class for the Product sold in the Sale
        /// </summary>
        /// <param name="efundraisingProdDataContext"></param>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public static product_class GetProductClassBySaleId(eFundraisingProdDataContext efundraisingProdDataContext, int saleId)
        {

            return (from si in efundraisingProdDataContext.sales_items
                    join sb in efundraisingProdDataContext.scratch_books on si.scratch_book_id equals sb.scratch_book_id
                    join pc in efundraisingProdDataContext.product_classes on (int)sb.product_class_id equals pc.product_class_id
                    where si.sales_id == saleId
                    select pc).FirstOrDefault();

        }

        public static pap_product_category GetProductCategoryByCategoryCode(eFundraisingProdDataContext efundraisingProdDC, string papProductCategoryCode)
        {

            return (from pc in efundraisingProdDC.pap_product_categories where pc.product_category_code == papProductCategoryCode select pc).FirstOrDefault();


        }
        /// <summary>
        /// Returns a suppressed product type
        /// </summary>
        /// <param name="efundraisingProdDataContext"></param>
        /// <param name="externalProductType"></param>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public static pap_suppressed_product_type GetSuppressedProductTypeByExtProductTypeIdAndAppId(eFundraisingProdDataContext efundraisingProdDataContext, string externalProductType, int applicationId)
        {
            return (from pspt in efundraisingProdDataContext.pap_suppressed_product_types 
                    where pspt.application_id == applicationId 
                    && pspt.ext_product_type_id == externalProductType 
                    select pspt).FirstOrDefault();
        }
        /// <summary>
        /// Returns the Product Category
        /// </summary>
        /// <param name="efundraisingProdDataContext"></param>
        /// <param name="externalProductType"></param>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public static pap_product_category GetPAPProductCategoryByProductTypeAndAppId(eFundraisingProdDataContext efundraisingProdDataContext, string externalProductType, int applicationId)
        {
            return (from pdt in efundraisingProdDataContext.pap_product_types 
                    join ppc in efundraisingProdDataContext.pap_product_categories on pdt.pap_product_category_id equals ppc.pap_product_category_id 
                    where pdt.ext_product_type_id == externalProductType 
                    && pdt.application_id == applicationId 
                    select ppc).FirstOrDefault();
        }
        /// <summary>
        /// Returns the default Product Category
        /// </summary>
        /// <param name="efundraisingProdDataContext"></param>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public static pap_product_category GetPAPDefaultProductCategoryByAppId(eFundraisingProdDataContext efundraisingProdDataContext, int applicationId)
        {
            return (from ppc in efundraisingProdDataContext.pap_product_categories 
                    join pct in efundraisingProdDataContext.pap_client_types on ppc.pap_product_category_id equals pct.pap_product_category_id 
                    where pct.application_id == applicationId 
                    && (bool)ppc.is_default select ppc).FirstOrDefault();
        }

        public static pap_client_type GetPapClientTypeByProductCategoryDesc(eFundraisingProdDataContext efundraisingProdDC, string input)
        {
            return (
                from ppc in efundraisingProdDC.pap_product_categories 
                join pct in efundraisingProdDC.pap_client_types on ppc.pap_product_category_id equals pct.pap_product_category_id 
                where ppc.product_category_desc == input.Trim() 
                select pct).FirstOrDefault();

        }
        public static bool IsOrderInPAPTransactionTable(eFundraisingProdDataContext efundraisingProdDC, int orderId, int AppId)
        {

            return (from pt in efundraisingProdDC.pap_transactions where pt.order_id == orderId && pt.application_id == AppId select pt).Count() > 0;
        }
        /// <summary>
        /// Indicates if the Order is still in a transaction with PAP
        /// </summary>
        /// <param name="efundraisingProdDataContext"></param>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        public static bool IsTransactionInPAPTransactionTable(eFundraisingProdDataContext efundraisingProdDataContext, int orderId, string status, int AppId)
        {
            return (from pt in efundraisingProdDataContext.pap_transactions
                    where pt.order_id == orderId
                    && pt.application_id == AppId
                    && pt.ext_status_id == status
                    select pt).Any();
        }


    }
}
