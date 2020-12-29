using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Console.PAPFeedback.TaskExecutor.Properties;
using GA.BDC.Data;
using GA.BDC.Data.DataLayer;
using GA.BDC.PAP.Data.Utilities;
using log4net;
using SWCorporate.SystemEx.Console;

namespace GA.BDC.Console.PAPFeedback.TaskExecutor.Tasks
{
    /// <summary>
    /// Verifies that the Campaigns in MONQSPMVA2.EfundraisingProd.pap_product_category have a valid match in PAP
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal sealed class VerifyPAPAffiliates : ITask<TaskFlags>
    {
        #region ITask<TaskFlags> Members

        private static readonly ILog Log = LogManager.GetLogger(typeof(SendPAPEsubsSaleTask));

        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Log.Info("Start: VerifyPAPAffiliates");
            try
            {
                using (var efrCommon = new EFRCommonDataContext(Settings.Default.EFRCommonConnectionString))
                {
                    var papPartners = Partner.GetPapParnerAttributeValues(efrCommon);
                    Log.InfoFormat("{0} PAP Partners found.", papPartners.Count);
                    if (papPartners.Count > 0)
                    {
                        foreach (var partnerAttributeValue in papPartners)
                        {
                            var papTransaction = new PAPTransaction();
                            var dateInserted = papTransaction.GetAffiliateDateInserted(partnerAttributeValue.value);
                            if (String.IsNullOrEmpty(dateInserted))
                            {
                                var partner =
                                    efrCommon.partners.Single(p => p.partner_id == partnerAttributeValue.partner_id);
                                Log.WarnFormat("Partner: {0}. Reference Id: {1} not found.",
                                    partner.partner_name, partnerAttributeValue.value);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }

            Log.Info("End: VerifyPAPAffiliates");
        }

        #endregion
    }
}
