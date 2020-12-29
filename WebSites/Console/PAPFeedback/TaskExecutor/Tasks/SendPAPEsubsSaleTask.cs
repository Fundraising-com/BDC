using System;
using System.Linq;
using System.Data.Linq;
using System.Globalization;
using log4net;
using SWCorporate.SystemEx.Console;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.PAP.Data.Utilities;
using GA.BDC.Console.PAPFeedback.TaskExecutor.Properties;


namespace GA.BDC.Console.PAPFeedback.TaskExecutor.Tasks
{
    // ReSharper disable once InconsistentNaming
    internal sealed class SendPAPEsubsSaleTask : ITask<TaskFlags>
    {
        #region ITask<TaskFlags> Members
        private static readonly ILog Log = LogManager.GetLogger(typeof(SendPAPEsubsSaleTask));
        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: SendPAPEsubsSaleTask");

            try
            {
                using (var efrCommon = new EFRCommonDataContext(Settings.Default.EFRCommonConnectionString))
                {
                    var papPartners = Partner.GetPapParnerAttributeValues(efrCommon);
                    Log.InfoFormat("{0} PAP Partners found.", papPartners.Count);
                    if (papPartners.Count > 0)
                    {
                        using (var esubsGlobalV2DataContext = new EsubsGlobalV2DataContext(Settings.Default.EsubsConnectionString))
                        {
                            foreach (var partnerAttributeValue in papPartners)
                            {
                                var papTransaction = new PAPTransaction();
                                var dateInserted = papTransaction.GetAffiliateDateInserted(partnerAttributeValue.value);
                                if (!String.IsNullOrEmpty(dateInserted))
                                {
                                    var fromDate = DateTime.Now.AddDays(Settings.Default.DaysToSubtractForFromDate);
                                    var toDate = DateTime.Now.AddDays(1);
                                    var orders = OnlineOrders.GetOnlineOrdersByPartnerIdAndDateRange(esubsGlobalV2DataContext, partnerAttributeValue.partner_id, fromDate, toDate);
                                    Log.DebugFormat("Partner: {0}", partnerAttributeValue.partner_id);
                                    Log.DebugFormat("{0} Orders found.", orders.Count());
                                    if (orders.Any())
                                    {
                                        using (var eFundraisingProdDataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                                        {
                                            foreach (var order in orders)
                                            {
                                                Log.DebugFormat("Order Id: {0}", order.order_id);
                                                if (order.order_item_id != null && Orders.IsTransactionInPAPTransactionTable(eFundraisingProdDataContext, (int)order.order_item_id, Settings.Default.PendingStatus, Settings.Default.EsubsAppID))
                                                {
                                                    Log.DebugFormat("Order is still in a Transaction.");
                                                    continue;
                                                }

                                                if (IsSaleBeforeAffilaitedSigned(dateInserted, order.create_date))
                                                {
                                                    Log.Debug("Order happened before the Affiliate was signed.");
                                                    continue;
                                                }
                                                if (Orders.GetSuppressedProductTypeByExtProductTypeIdAndAppId(eFundraisingProdDataContext, order.product_type_id.ToString(), Settings.Default.EsubsAppID) != null)
                                                {
                                                    Log.Debug("Order's product type belongs to suppressed products.");
                                                    continue;
                                                }

                                                var transactionId = string.Empty;
                                                var error = false;
                                                pap_product_category papProductCategory = null;

                                                try
                                                {
                                                    var displayProductType = OnlineOrders.GetDisplayProductTypeByExtProductIdAndStoreId(esubsGlobalV2DataContext, order.store_id, order.product_type_id);
                                                    if (displayProductType != null)
                                                    {
                                                        papProductCategory = Orders.GetPAPProductCategoryByProductTypeAndAppId(eFundraisingProdDataContext, displayProductType.display_product_type_id.ToString(CultureInfo.InvariantCulture), Settings.Default.EsubsAppID);
                                                        if (papProductCategory == null)
                                                        {
                                                            papProductCategory = Orders.GetPAPDefaultProductCategoryByAppId(eFundraisingProdDataContext, Settings.Default.EsubsAppID);
                                                            if (papProductCategory == null)
                                                            {
                                                                Log.WarnFormat("No Product Category exists for Display Product Type Id {0} .", displayProductType.display_id);
                                                                continue;
                                                            }
                                                        }

                                                        transactionId = papTransaction.PostSaleAPI(eFundraisingProdDataContext, partnerAttributeValue, order, papProductCategory);
                                                    }
                                                    else
                                                    {
                                                        Log.WarnFormat("No Diplay Product Type exists for Product Type Id: {0}. Order Id: {1}.", order.product_type_id, order.order_id);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    error = true;
                                                    Log.Error(ex);
                                                }
                                                finally
                                                {
                                                    var pt = new pap_transaction
                                                    {
                                                        order_id = order.order_item_id,
                                                        pap_product_category_id = papProductCategory == null ? (int?)null : papProductCategory.pap_product_category_id,
                                                        total_cost = order.sub_total,
                                                        ext_transaction_id = transactionId,
                                                        ext_status_id = error ? Settings.Default.ErrorStatus + Settings.Default.PendingStatus : Settings.Default.PendingStatus,
                                                        campaign_id = order.event_id,
                                                        create_date = DateTime.Now,
                                                        application_id = Settings.Default.EsubsAppID
                                                    };


                                                    eFundraisingProdDataContext.pap_transactions.InsertOnSubmit(pt);
                                                }
                                            }
                                            try
                                            {
                                                eFundraisingProdDataContext.SubmitChanges();
                                            }
                                            catch (ChangeConflictException)
                                            {
                                                eFundraisingProdDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                                            }
                                        }
                                    }
                                }                                
                            }
                        }
                    }
                }

        #endregion

            }

            catch (Exception exception)
            {
                Log.Error(exception);

                throw;
            }

            Log.Info("End: SendPAPEsubsSaleTask");
        }
        /// <summary>
        /// Returns True if the Sale occured before the Affiliate was signed
        /// </summary>
        /// <param name="affiliateDateInserted"></param>
        /// <param name="saleDate"></param>
        /// <returns></returns>
        private static bool IsSaleBeforeAffilaitedSigned(string affiliateDateInserted, DateTime? saleDate)
        {
            try
            {
                var signedDate = DateTime.Parse(affiliateDateInserted, CultureInfo.InvariantCulture);
                return saleDate < signedDate;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return true;
            }
        }

    }
}

