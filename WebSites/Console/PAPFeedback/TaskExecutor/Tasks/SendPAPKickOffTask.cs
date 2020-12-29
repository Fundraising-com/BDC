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
    internal sealed class SendPAPKickOffTask : ITask<TaskFlags>
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(SendPAPKickOffTask));
        #region ITask<TaskFlags> Members

        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: SendPAPKickOffTask");
            try
            {
                using (var efrCommonDataContext = new EFRCommonDataContext(Settings.Default.EFRCommonConnectionString))
                {
                    var papParnerAttributeValues = Partner.GetPapParnerAttributeValues(efrCommonDataContext);
                    if (papParnerAttributeValues.Any())
                    {
                        using (var esubsGlobalV2DataContext = new EsubsGlobalV2DataContext(Settings.Default.EsubsConnectionString))
                        {
                            foreach (var partnerAttributeValue in papParnerAttributeValues)
                            {
                                Log.DebugFormat("Partner Id: {0}", partnerAttributeValue.partner_id);
                                var papTransaction = new PAPTransaction();
                                var dateInserted = papTransaction.GetAffiliateDateInserted(partnerAttributeValue.value);
                                if (!String.IsNullOrEmpty(dateInserted))
                                {
                                    // logic for activations
                                    var startDate = DateTime.Now.AddDays(-1);
                                    var kickoffs = OnlineOrders.GetKickoffsByPartnerId(esubsGlobalV2DataContext, partnerAttributeValue.partner_id, startDate).ToList();
                                    if (kickoffs.Any())
                                    {
                                        using (var efundProd = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                                        {
                                            foreach (var kickoff in kickoffs)
                                            {
                                                Log.DebugFormat("Event Participation Id {0}", kickoff.event_participation_id);
                                                if (IsSaleBeforeAffilaitedSigned(dateInserted, kickoff.create_date))
                                                {
                                                    Log.Debug("Skipped because sale was before the affiliate signed.");
                                                    continue;
                                                }

                                                if (kickoff.event_participation_id != null && Orders.IsTransactionInPAPTransactionTable(efundProd, (int)kickoff.event_participation_id, Settings.Default.KickoffStatus, Settings.Default.EsubsAppID))
                                                {
                                                    Log.Debug("Skipped because sale is in Transaction PAP table.");
                                                    continue;
                                                }

                                                var transactionId = string.Empty;
                                                var error = false;
                                                try
                                                {
                                                    transactionId = papTransaction.PostKickoffAPI(efundProd, partnerAttributeValue, kickoff, Settings.Default.KickoffCampaign, Settings.Default.ApprovedStatus);
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
                                                        order_id = kickoff.event_participation_id,
                                                        total_cost = Decimal.Zero,
                                                        ext_transaction_id = transactionId,
                                                        ext_status_id = error ? Settings.Default.ErrorStatus + Settings.Default.KickoffStatus : Settings.Default.KickoffStatus,
                                                        campaign_id = kickoff.event_id,
                                                        create_date = DateTime.Now,
                                                        application_id = Settings.Default.EsubsAppID
                                                    };


                                                    efundProd.pap_transactions.InsertOnSubmit(pt);
                                                }
                                            }
                                            try
                                            {
                                                efundProd.SubmitChanges();
                                            }
                                            catch (ChangeConflictException)
                                            {
                                                efundProd.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
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

            Log.Info("End: SendPAPKickOffTask");
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
