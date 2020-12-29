using System;
using System.Linq;
using System.Globalization;
using System.Data.Linq;
using log4net;
using SWCorporate.SystemEx.Console;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.PAP.Data.Utilities;
using GA.BDC.PAP.Data.SearchFilters;
using GA.BDC.Console.PAPFeedback.TaskExecutor.Properties;

namespace GA.BDC.Console.PAPFeedback.TaskExecutor.Tasks
{
    // ReSharper disable InconsistentNaming
    internal sealed class ConfirmPAPSaleTask : ITask<TaskFlags>
    // ReSharper restore InconsistentNaming
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfirmPAPSaleTask));
        #region ITask<TaskFlags> Members

        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: ConfirmPAPSaleTask");
            try
            {
                using (var eFundraisingProdDataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                {
                    var papTransaction = new PAPTransaction();
                    var respond = papTransaction.GetAllPendingTransactions(Settings.Default.PendingStatus);
                    if (respond != null && respond.Count > 1)
                    {
                        Log.InfoFormat("{0} PAP pending Transactions found.", respond.Count);
                        var firstRun = true;
                        var error = false;
                        pap_client_type pct = null;
                        foreach (var sale in respond)
                        {
                            if (firstRun)
                            {
                                firstRun = false;
                                continue;
                            }

                            var orderId = 0;
                            var pos = Array.IndexOf(respond[0], "t_orderid");
                            var totalCost = 0.0M;
                            var leadId = 0;
                            var extId = string.Empty;
                            var skip = false;
                            try
                            {
                                orderId = Convert.ToInt32(sale[pos]);
                                Log.DebugFormat("Sale Id: {0}", orderId);
                                var categoryNamePosition = Array.IndexOf(respond[0], "name");
                                pct = Orders.GetPapClientTypeByProductCategoryDesc(eFundraisingProdDataContext,
                                    sale[categoryNamePosition]);
                                var insertDatePos = Array.IndexOf(respond[0], "dateinserted");
                                if (pct != null && pct.application_id == Settings.Default.CRMAppID)
                                {
                                    Orders.GetSale(eFundraisingProdDataContext, orderId);
                                    var isTimeConfirmed = TimeConfirmed(sale[insertDatePos]);
                                    var saleIsPaid = Orders.SaleIsPaid(eFundraisingProdDataContext, orderId);
                                    if (isTimeConfirmed || !saleIsPaid)
                                    {
                                        skip = true;
                                        Log.InfoFormat("Skipped. Reason: {0}.", isTimeConfirmed ? "isTimeConfirmed" : "saleIsNOTPaid");
                                        continue;
                                    }
                                }
                                else if (pct != null && pct.application_id == Settings.Default.EsubsAppID)
                                {
                                    using (var esubsGlobalV2DataContext = new EsubsGlobalV2DataContext(Settings.Default.EsubsConnectionString))
                                    {
                                        esubsGlobalV2DataContext.CommandTimeout = Settings.Default.EsubsCommandExec;
                                        var insertDate = sale[insertDatePos];
                                        var isTimeConfirmed = TimeConfirmed(insertDate);
                                        var onlineOrders =
                                            OnlineOrders.GetOnlineOrders(esubsGlobalV2DataContext).Where(o => o.order_item_id == orderId);
                                        if (isTimeConfirmed || onlineOrders.Any())
                                        {
                                            skip = true;
                                            Log.InfoFormat("Skipped. Reason: {0}.", isTimeConfirmed ? "isTimeConfirmed" : "saleIsNOTPaid");
                                            continue;
                                        }
                                    }

                                }
                                else
                                {
                                    error = true;
                                    Log.ErrorFormat("Cannot find PAP Client Type for commision type {0} for Order Id {1}", sale[categoryNamePosition], orderId);
                                }


                                var posId = Array.IndexOf(respond[0], (new CampaignFilter()).FilterResult);
                                var totalCostId = Array.IndexOf(respond[0], (new TotalCostFilter()).FilterResult);
                                extId = sale[posId];
                                try
                                {
                                    totalCost = Convert.ToDecimal(sale[totalCostId]);
                                }
                                catch (Exception)
                                {
                                    totalCost = 0.0M;
                                }


                                var leadIdPos = Array.IndexOf(respond[0], (new Data1Filter()).FilterResult) +
                                                (pct != null && pct.application_id == Settings.Default.EsubsAppID
                                                    ? (int)pct.application_id
                                                    : 0);
                                try
                                {
                                    leadId = Convert.ToInt32(sale[leadIdPos]);
                                }
                                catch (Exception)
                                {
                                    leadId = 0;
                                }
                                var papId = sale[posId];
                                papTransaction.UpdateTransaction(papId, orderId, Settings.Default.ApprovedStatus,
                                    string.Empty);
                                Log.InfoFormat("Updated Transaction. PAP Id: {2}, Order Id: {0}. New Status: {1}.", orderId,
                                    Settings.Default.ApprovedStatus, papId);
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex);
                                error = true;
                            }
                            finally
                            {
                                if (!skip)
                                {
                                    var pt = new pap_transaction
                                    {
                                        order_id = orderId,
                                        total_cost = totalCost,
                                        ext_transaction_id = extId,
                                        ext_status_id =
                                            error
                                                ? Settings.Default.ErrorStatus + Settings.Default.ApprovedStatus
                                                : Settings.Default.ApprovedStatus,
                                        lead_id = leadId,
                                        create_date = DateTime.Now,
                                        application_id = pct == null ? 2 : (int)pct.application_id
                                    };
                                    eFundraisingProdDataContext.pap_transactions.InsertOnSubmit(pt);
                                }
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
                    else
                    {
                        Log.Info("0 PAP pending Transactions found.");
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                throw;
            }
            Log.Info("End: ConfirmPAPSaleTask");
        }
        /// <summary>
        /// Returns true if the date plus the days before the order is confirmed is greater than today.
        /// </summary>
        /// <param name="input">sales date</param>
        /// <returns></returns>
        private static bool TimeConfirmed(string input)
        {
            try
            {
                var insertDate = DateTime.Parse(input, CultureInfo.InvariantCulture);
                return insertDate.AddDays(Settings.Default.DaysBeforeOrderConfirmed) > DateTime.Now;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return false;
            }
        }

        #endregion

    }
}
