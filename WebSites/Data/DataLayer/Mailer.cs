using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Configuration;

namespace GA.BDC.Data.DataLayer
{
    public class Mailer
    {
        public static List<bdc_get_email_queueResult1> GetBDCEmailsToSend()
        {
            using (var dc = new BDCMassmailerDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.MassMailerProdConnectionString"].ConnectionString))
            {
                return dc.bdc_get_email_queue().ToList();
            }
        }

        public static void UpdateExternalMailID(int mailID, int extMailID, int emailStatus)
        {
            using (var dc = new BDCMassmailerDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.MassMailerProdConnectionString"].ConnectionString))
            {
                var e = (from em in dc.email_queues where em.queue_id == mailID select em).FirstOrDefault();
                if (e != null)
                {
                    e.ext_email_id = extMailID;
                    e.komunik_return_value_id = emailStatus;
                    e.timesent = DateTime.Now;
                    try
                    {
                        dc.SubmitChanges();

                    }
                    catch (ChangeConflictException)
                    {
                        dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                    }
                }
            }
        }

        public static Dictionary<int, int?> GetEmailsFeedbackID(int days, int project_id)
        {
            using (var dc = new BDCMassmailerDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.MassMailerProdConnectionString"].ConnectionString))
            {
                // return (from em in dc.email_queues where em.project_id == project_id && em.ext_email_id > 0 && em.timesent >= DateTime.Now.Subtract(new TimeSpan(days, 0, 0, 0, 0)) select em).ToDictionary(t => t.source_id, t => t.ext_email_id);
                var output = new Dictionary<int, int?>();
                var le = (from em in dc.email_queues where em.project_id == project_id && em.ext_email_id > 0 && em.timesent >= DateTime.Now.Subtract(new TimeSpan(days, 0, 0, 0, 0)) select em).Select(t => new { t.source_id, t.ext_email_id });
                foreach (var e in le)
                {
                    if (!output.ContainsKey(e.source_id))
                    {
                        output.Add(e.source_id, e.ext_email_id);
                    }
                }
                return output;

            }
        }

        public static List<int> GetEmailsWithStatusDelievered(short emailStatus, short projectID, short retention)
        {
            using (var dc = new BDCMassmailerDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.MassMailerProdConnectionString"].ConnectionString))
            {
                return (from em in dc.bdc_get_latest_email_status(emailStatus, projectID, retention) select em.touch_id).ToList();
            }
        }

        public static int GetLatestEmailActivity(int touchID, short projectID)
        {
            using (var dc = new BDCMassmailerDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.MassMailerProdConnectionString"].ConnectionString))
            {
                return (from em in dc.bdc_get_latest_email_activity(touchID, projectID) select em.action_id).FirstOrDefault();
            }
        }

        public static void InsertEmailActivity(email_activity input)
        {
            using (var dc = new BDCMassmailerDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.MassMailerProdConnectionString"].ConnectionString))
            {
                dc.email_activities.InsertOnSubmit(input);
                try
                {
                    dc.SubmitChanges();
                }
                catch (ChangeConflictException)
                {
                    dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                }

            }
        }
        /// <summary>
        /// Updates the email status and returns the row affected
        /// </summary>
        /// <param name="externalId">External Id</param>
        /// <param name="newStatus">New Status to be set</param>     
        public static int UpdateEmailStatus(int externalId, int newStatus)
        {
            using (var dc = new BDCMassmailerDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.MassMailerProdConnectionString"].ConnectionString))
            {
                var result = dc.bdc_update_email_status_by_external_id(externalId, newStatus).FirstOrDefault();
                if (result != null)
                {
                    return result.source_id;
                }
            }
            return -1;
        }
    }
}
