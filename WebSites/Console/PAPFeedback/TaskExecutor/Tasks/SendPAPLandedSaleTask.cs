using System;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using log4net;
using SWCorporate.SystemEx.Console;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.PAP.Data.Utilities;
using GA.BDC.Console.PAPFeedback.TaskExecutor.Properties;

namespace GA.BDC.Console.PAPFeedback.TaskExecutor.Tasks
{
    // ReSharper disable once InconsistentNaming
    internal sealed class SendPAPLandedSaleTask : ITask<TaskFlags>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SendPAPLandedSaleTask));

        #region ITask<TaskFlags> Members

        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: SendPAPLandedSaleTask");

            try
            {
                using (var eFundraisingProdDataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                {
                    eFundraisingProdDataContext.CommandTimeout = 60*10;
                    var saleToProcess = Orders.GetPapSalesToProcess(eFundraisingProdDataContext);
                    if (saleToProcess != null)
                    {
                        var results = saleToProcess.ToList();
                        Log.InfoFormat("{0} PAP Sales to process found.", results.Count);
                        foreach (var saleTransaction in results)
                        {
                            int? papProductCategoryId = null;
                            var transactionId = string.Empty;
                            var error = false;
                            try
                            {
                                Log.DebugFormat("Sale Id {0}", saleTransaction.sales_id);
                                using (var fundraisingProdDataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                                {
                                    var pTransaction = new PAPTransaction();
                                    var affiliateDateInserted = pTransaction.GetAffiliateDateInserted(saleTransaction.a_aid);
                                    if (!String.IsNullOrEmpty(affiliateDateInserted))
                                    {
                                        if (!isSaleBeforeAffilaitedSigned(affiliateDateInserted, saleTransaction.sale_date))
                                        {
                                            transactionId = pTransaction.PostSaleAPI(fundraisingProdDataContext, saleTransaction);
                                        }
                                        else
                                        {
                                            error = true;
                                            Log.WarnFormat("Sale {0} occured before affiliate {1} was signed.", saleTransaction.sales_id, saleTransaction.a_aid);
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        error = true;
                                        Log.WarnFormat("Could not retrieve inserted date for Affiliate: {0} on Sale Id {1}.", saleTransaction.a_aid, saleTransaction.sales_id);
                                        continue;
                                    }

                                }
                                error = string.IsNullOrEmpty(transactionId);

                                // update EFR database / transaction table
                                var papProductCategory = Orders.GetProductCategoryByCategoryCode(eFundraisingProdDataContext,saleTransaction.product_category_desc);
                                if (papProductCategory != null)
                                {
                                    papProductCategoryId = papProductCategory.pap_product_category_id;
                                }
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
                                    order_id = saleTransaction.sales_id,
                                    pap_product_category_id = papProductCategoryId,
                                    total_cost = saleTransaction.total_amount,
                                    ext_transaction_id = transactionId,
                                    ext_status_id =
                                        error
                                            ? Settings.Default.ErrorStatus + Settings.Default.PendingStatus
                                            : Settings.Default.PendingStatus,
                                    campaign_id = saleTransaction.client_id,
                                    lead_id = saleTransaction.lead_id,
                                    create_date = DateTime.Now,
                                    application_id = Settings.Default.CRMAppID
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
                    else
                    {
                        Log.InfoFormat("0 PAP Sales to process found.");
                    }
                }

            }

            catch (Exception exception)
            {
                Log.Error(exception);
                throw;
            }

            Log.Info("End: SendPAPLandedSaleTask");
        }
        /// <summary>
        /// Returns True if the Sale occured before the Affiliate was signed
        /// </summary>
        /// <param name="affiliateDateInserted"></param>
        /// <param name="saleDate"></param>
        /// <returns></returns>
        private static bool isSaleBeforeAffilaitedSigned(string affiliateDateInserted, DateTime saleDate)
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
