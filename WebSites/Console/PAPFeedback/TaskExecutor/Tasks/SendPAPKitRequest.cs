using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SWCorporate.SystemEx.Console;
using GA.BDC.Data;
using GA.BDC.PAP.Data.Utilities;
using GA.BDC.Console.PAPFeedback.TaskExecutor.Properties;

namespace GA.BDC.Console.PAPFeedback.TaskExecutor.Tasks
{
    // ReSharper disable once InconsistentNaming
    internal sealed class SendPAPKitRequest : ITask<TaskFlags>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SendPAPKitRequest));

        #region ITask<TaskFlags> Members

        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            var exceptions = new List<Exception>();
            Log.Info("Start: SendPAPKitRequest");

            try
            {
                using (var dataContext = new eFundraisingProdDataContext(Settings.Default.eFundraisingProdConnectionString))
                {
                    dataContext.CommandTimeout = 60*10;
                    var leads = dataContext.pap_get_leads_to_be_sent().ToList();                    
                    Log.InfoFormat("{0} Leads to process found.", leads.Count());
                    foreach (var lead in leads)
                    {
                        try
                        {
                            using (var eFrCommonDataContext = new EFRCommonDataContext(Settings.Default.EFRCommonConnectionString))
                            {
                                var partners = eFrCommonDataContext.es_get_partner_by_promotionId(lead.promotion_id).ToList();
	                            if (partners.Any())
	                            {
		                            var partnerAAid = partners.First().value;
		                            var pTransaction = new PAPTransaction();
		                            pTransaction.PostKitRequest(lead.lead_id, partnerAAid);
		                            dataContext.pap_update_lead_sent_to_pap(lead.lead_id);
	                            }
	                            else
	                            {
		                            Log.Error($"Lead {lead.lead_id} with Promotion {lead.promotion_id} does not have a Partner");
	                            }
                                
                            }
                        }
                        catch (Exception exception)
                        {
                            exceptions.Add(exception);
                        }
                        
                    }
                }

            }
            catch (Exception exception)
            {
                exceptions.Add(exception);
            }
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
            Log.Info("End: SendPAPKitRequest");
        }
        
        #endregion
    }
}
