using System;
using System.Linq;
using System.Data.Linq;
using log4net;
using SWCorporate.SystemEx.Console;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.PAP.Data.SearchFilters;
using GA.BDC.PAP.Data.Utilities;
using GA.BDC.Console.PAPFeedback.TaskExecutor.Properties;

namespace GA.BDC.Console.PAPFeedback.TaskExecutor.Tasks
{
    // ReSharper disable once InconsistentNaming
    internal sealed class CancelPAPSaleTask : ITask<TaskFlags>
    {
        #region ITask<TaskFlags> Members
        private static readonly ILog Log = LogManager.GetLogger(typeof(CancelPAPSaleTask));
        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: CancelPAPSaleTask");
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

                        foreach (var sale in respond)
                        {
                            var error = false;
                            pap_client_type papClientType = null;

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
                                papClientType = Orders.GetPapClientTypeByProductCategoryDesc(eFundraisingProdDataContext,
                                    sale[categoryNamePosition]);
                                // Case Landed Orders (CRM)
                                if (papClientType != null && papClientType.application_id == Settings.Default.CRMAppID)
                                {
                                    var saleFound = Orders.GetSale(eFundraisingProdDataContext, orderId);

                                    if (saleFound != null && saleFound.sales_status_id == 2)
                                    {
                                        Log.Debug("Skipped because it's a CRM sale with Status: 2.");
                                        skip = true;
                                        continue;
                                    }
                                }
                                else if (papClientType != null && papClientType.application_id == Settings.Default.EsubsAppID)
                                {
                                    using (var esubsGlobalV2DataContext = new EsubsGlobalV2DataContext(Settings.Default.EsubsConnectionString))
                                    {
                                        esubsGlobalV2DataContext.CommandTimeout = Settings.Default.EsubsCommandExec;
                                        if (OnlineOrders.GetOnlineOrdersByOrderItemId(esubsGlobalV2DataContext, orderId).Any())
                                        {
                                            Log.Debug("Skipped because it has valid order items.");
                                            skip = true;
                                            continue;
                                        }
                                    }

                                }
                                else
                                {
                                    error = true;
                                    Log.ErrorFormat("Cannot find PAP Client Type for Commision Type {0} for Order ID {1}.", sale[categoryNamePosition], orderId);
                                }


                                var posId = Array.IndexOf(respond[0], (new CampaignFilter()).FilterResult);
                                extId = sale[posId];
                                var totalCostId = Array.IndexOf(respond[0], (new TotalCostFilter()).FilterResult);

                                try
                                {
                                    totalCost = Convert.ToDecimal(sale[totalCostId]);
                                }
                                catch (Exception exception)
                                {
                                    Log.Warn(string.Format("Error while trying to retrieve the Total Cost."), exception);
                                    totalCost = 0.0M;
                                }

                                var leadIdPos = Array.IndexOf(respond[0], (new Data1Filter()).FilterResult) +
                                                (papClientType != null && papClientType.application_id == Settings.Default.EsubsAppID && papClientType.application_id != null
                                                    ? (int) papClientType.application_id
                                                    : 0);
                                try
                                {
                                    leadId = Convert.ToInt32(sale[leadIdPos]);
                                }
                                catch (Exception exception)
                                {
                                    Log.Warn(string.Format("Error while trying to retrieve the Lead Id."), exception);
                                    leadId = 0;
                                }

                                if (!error)
                                {
                                    papTransaction.UpdateTransaction(sale[posId], orderId, Settings.Default.DeclinedStatus,
                                        String.Format("Due to cancellation of order {0} by CancelPapSaleTask {1}",
                                            orderId, DateTime.Now));
                                }
                            }
                            catch (Exception exception)
                            {
                                Log.Error(exception);
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
                                                ? Settings.Default.ErrorStatus + Settings.Default.DeclinedStatus
                                                : Settings.Default.DeclinedStatus,
                                        lead_id = leadId,
                                        create_date = DateTime.Now,
                                        application_id = papClientType == null || papClientType.application_id == null ? 2 : (int) papClientType.application_id
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

            Log.Info("End: CancelPAPSaleTask");
        }

        #endregion
    }

}