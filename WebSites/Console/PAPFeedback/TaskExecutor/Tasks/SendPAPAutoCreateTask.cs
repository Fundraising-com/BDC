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
    internal sealed class SendPAPAutoCreateTask : ITask<TaskFlags>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SendPAPAutoCreateTask));
        #region ITask<TaskFlags> Members

        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: SendPAPAutoCreateTask");
            try
            {
                using (var efrCommonDataContext = new EFRCommonDataContext(Settings.Default.EFRCommonConnectionString))
                {
                    var parnerAttributeValues = Partner.GetPapParnerAttributeValues(efrCommonDataContext);
                    if (parnerAttributeValues.Any())
                    {
                        using (var esubsGlobalV2DataContext = new EsubsGlobalV2DataContext(Settings.Default.EsubsConnectionString))
                        {
                            foreach (var partnerAttributeValue in parnerAttributeValues)
                            {
                                Log.DebugFormat("Partner Id: {0}", partnerAttributeValue.partner_id);
                                var papTransaction = new PAPTransaction();
                                var dateInserted = papTransaction.GetAffiliateDateInserted(partnerAttributeValue.value);
                                if (!String.IsNullOrEmpty(dateInserted))
                                {
                                    var startDate = DateTime.Now.AddDays(-1);
                                    // logic for activations
                                    var kickoffs = OnlineOrders.GetAutoCreateByPartnerId(esubsGlobalV2DataContext, partnerAttributeValue.partner_id, startDate).ToList();
                                    if (kickoffs.Any())
                                    {
                                        using (var efundraisingProdDataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                                        {
                                            foreach (var kickoff in kickoffs)
                                            {
                                                Log.DebugFormat("Group Id: {0}", kickoff.group_id);
                                                if (IsSaleBeforeAffilaitedSigned(dateInserted, kickoff.create_date))
                                                {
                                                    Log.Debug("Skipped because sale was before the Affiliate signed.");
                                                    continue;
                                                }

                                                if (Orders.IsTransactionInPAPTransactionTable(efundraisingProdDataContext, kickoff.group_id, Settings.Default.AutoCreateStatus, Settings.Default.EsubsAppID))
                                                {
                                                    Log.Debug("Skipped becuase sale is in Transacion PAP Table");
                                                    continue;
                                                }

                                                var transactionId = string.Empty;
                                                var error = false;
                                                try
                                                {
                                                    transactionId = papTransaction.PostGroupAutoCreateAPI(efundraisingProdDataContext, partnerAttributeValue, kickoff, Settings.Default.AutoCreationCampain, Settings.Default.ApprovedStatus);
                                                }
                                                catch (Exception exception)
                                                {
                                                    error = true;
                                                    Log.Error(exception);
                                                }
                                                finally
                                                {
                                                    var pt = new pap_transaction
                                                    {
                                                        order_id = kickoff.group_id,
                                                        total_cost = Decimal.Zero,
                                                        ext_transaction_id = transactionId,
                                                        ext_status_id = error ? Settings.Default.ErrorStatus + Settings.Default.AutoCreateStatus : Settings.Default.AutoCreateStatus,
                                                        campaign_id = kickoff.group_id,
                                                        create_date = DateTime.Now,
                                                        application_id = Settings.Default.EsubsAppID
                                                    };


                                                    efundraisingProdDataContext.pap_transactions.InsertOnSubmit(pt);
                                                }
                                            }
                                            try
                                            {
                                                efundraisingProdDataContext.SubmitChanges();
                                            }
                                            catch (ChangeConflictException)
                                            {
                                                efundraisingProdDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
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

            Log.Info("End: SendPAPAutoCreateTask");
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
