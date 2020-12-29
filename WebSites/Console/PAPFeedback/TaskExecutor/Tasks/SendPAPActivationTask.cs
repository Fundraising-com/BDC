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
    internal sealed class SendPAPActivationTask : ITask<TaskFlags>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SendPAPActivationTask));
        #region ITask<TaskFlags> Members

        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: SendPAPActivationTask");
            try
            {
                using (var efrCommonDataContext = new EFRCommonDataContext(Settings.Default.EFRCommonConnectionString))
                {
                    var partnerAttributeValues = Partner.GetPapParnerAttributeValues(efrCommonDataContext);
                    if (partnerAttributeValues.Any())
                    {
                        using (var esubsGlobalV2DataContext = new EsubsGlobalV2DataContext(Settings.Default.EsubsConnectionString))
                        {
                            foreach (var partnerAttributeValue in partnerAttributeValues)
                            {
                                var papTransaction = new PAPTransaction();
                                var dateInserted = papTransaction.GetAffiliateDateInserted(partnerAttributeValue.value);
                                if (!String.IsNullOrEmpty(dateInserted))
                                {
                                    var fromDate = DateTime.Now.AddDays(Settings.Default.DaysToSubtractForFromDate);
                                    var toDate = DateTime.Now.AddDays(1);
                                    // logic for activations
                                    var orders = OnlineOrders.GetOnlineOrdersByPartnerIdAndDateRange(esubsGlobalV2DataContext, partnerAttributeValue.partner_id, fromDate, toDate).Where(o => o.act == 1);
                                    if (orders.Any())
                                    {
                                        using (var eFundraisingProdDataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                                        {
                                            foreach (var order in orders)
                                            {
                                                Log.DebugFormat("Order Id {0}", order.order_id);
                                                if (IsSaleBeforeAffilaitedSigned(dateInserted, order.create_date))
                                                {
                                                    Log.WarnFormat("Order {0} occured before Partner {1} was signed.", order.order_id, partnerAttributeValue.partner_id);
                                                    continue;
                                                }
                                                // REDO (smth like Activationsin PAP transaction table Orders.IsOrderInPAPTransactionTable(efundProd, (int)o.order_item_id, Settings.Default.ActivationStatus)
                                                if (order.order_item_id != null && Orders.IsTransactionInPAPTransactionTable(eFundraisingProdDataContext, (int)order.order_item_id, Settings.Default.ActivationStatus, Settings.Default.EsubsAppID))
                                                {
                                                    Log.Debug("Order is in Transaction in PAP Transaction table.");
                                                    continue;
                                                }

                                                var transactionId = string.Empty;
                                                var error = false;
                                                try
                                                {
                                                    transactionId = papTransaction.PostActivationAPI(eFundraisingProdDataContext, partnerAttributeValue, order, Settings.Default.ActivationCampaign, Settings.Default.ApprovedStatus);
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
                                                        total_cost = order.sub_total,
                                                        ext_transaction_id = transactionId,
                                                        ext_status_id = error ? Settings.Default.ErrorStatus + Settings.Default.ActivationStatus : Settings.Default.ActivationStatus,
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
            }
            catch (Exception exception)
            {
                Log.Error(exception);

                throw;
            }

            Log.Info("End: SendPAPActivationTask");
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

        #endregion
    }
}
