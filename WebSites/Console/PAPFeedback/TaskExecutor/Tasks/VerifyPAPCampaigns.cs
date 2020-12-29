using System.Linq;
using GA.BDC.Console.PAPFeedback.TaskExecutor.Properties;
using GA.BDC.Data;
using GA.BDC.PAP.Data.Utilities;
using log4net;
using SWCorporate.SystemEx.Console;

namespace GA.BDC.Console.PAPFeedback.TaskExecutor.Tasks
{
    /// <summary>
    /// Verifies that the Campaigns in MONQSPMVA2.EfundraisingProd.pap_product_category have a valid match in PAP
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal sealed class VerifyPAPCampaigns : ITask<TaskFlags>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VerifyPAPCampaigns));

        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: VerifyPAPCampaigns");

            using (var eFundraisingProdDataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
            {
                var papProductCategories = eFundraisingProdDataContext.pap_product_categories.ToList();
                var papTransaction = new PAPTransaction();
                foreach (var papProductCategory in papProductCategories)
                {
                    var campaignId = papTransaction.SearchCampaignIdByName(papProductCategory.product_category_desc);
                    if (string.IsNullOrEmpty(campaignId))
                    {
                        Log.ErrorFormat("Campaign {0} invalid. It doesn't have a match in PAP.", papProductCategory.product_category_desc);
                    }
                    else
                    {
                        Log.DebugFormat("Campagin {0} valid. PAP Campaign Id: {1}.", papProductCategory.product_category_desc, campaignId);
                    }
                }
            }

            Log.Info("End: VerifyPAPCampaigns");
        }
    }
}
