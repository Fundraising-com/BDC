using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Configuration;
using System.Transactions;

using GA.BDC.Data;

namespace GA.BDC.Data.DataLayer
{
    public struct TouchChangeLogInfo
    {
        public int? TouchChangeLogID;
        public int? EmailTemplateID;
        public string CultureCode;
        public DateTime? ModifiedDate;
        public string ModifiedBy;
        public int? FieldID;
        public string FieldName;
        public string Value;
        public bool? ProdRefreshed;
        public string RefreshedBy;
        public DateTime? RefreshedDate;
    }

    public class TouchChangeLogger
    {
        public static List<email_template_field> GetEmailTemplateFields()
        {
            using (EsubsGlobalV2DataContext dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
            {
                return (from field in dc.email_template_fields
                        select field).ToList<email_template_field>();
            }
        }

        public static List<TouchChangeLogInfo> GetTouchChangeLogInfo()
        {
            using (EsubsGlobalV2DataContext dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
            {
                return (from change in dc.es_get_touch_change_logs()
                        select new TouchChangeLogInfo { TouchChangeLogID = change.touch_change_log_id,
                                                        EmailTemplateID = change.email_template_id, 
                                                        CultureCode = change.culture_code,
                                                        ModifiedDate = change.modified_date,
                                                        ModifiedBy = change.modified_by,
                                                        FieldID = change.email_template_field_id,
                                                        FieldName = change.field_name,
                                                        Value = change.value,
                                                        ProdRefreshed = change.prod_refreshed,
                                                        RefreshedBy = change.refreshed_by,
                                                        RefreshedDate = change.refreshed_date }).ToList<TouchChangeLogInfo>();
            }
        }

        public static void LogTouchInsert(touch_change_log change_log)
        {
            using (EsubsGlobalV2DataContext dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
            {
                dc.touch_change_logs.InsertOnSubmit(change_log);

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

        public static void LogTouchChanges(touch_change_log change_log, List<touch_change_log_detail> change_log_details)
        {
            if (change_log_details.Count > 0)
            {
                using (EsubsGlobalV2DataContext dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
                {
                    dc.touch_change_logs.InsertOnSubmit(change_log);

                    try
                    {
                        dc.SubmitChanges();
                    }
                    catch (ChangeConflictException)
                    {
                        dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                    }

                    using (TransactionScope ts = new TransactionScope())
                    {
                        change_log_details.ForEach(x => x.touch_change_log_id = change_log.touch_change_log_id);
                        dc.touch_change_log_details.InsertAllOnSubmit(change_log_details);
                        try
                        {
                            dc.SubmitChanges();
                        }
                        catch (ChangeConflictException)
                        {
                            dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                        }
                        ts.Complete();
                    }
                }
            }
        }

        public static void SaveProdRefreshed(int touch_change_log_id, string refreshed_by)
        {
            using (EsubsGlobalV2DataContext dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
            {
                var change_logs = dc.touch_change_log_details.Where(x => x.touch_change_log_id == touch_change_log_id).ToList();
                if (change_logs != null)
                {
                    try
                    {
                        change_logs.ForEach(x => x.prod_refreshed = true);
                        change_logs.ForEach(x => x.refreshed_date = System.DateTime.Now);
                        change_logs.ForEach(x => x.refreshed_by = refreshed_by);

                        dc.SubmitChanges();
                    }
                    catch (ChangeConflictException)
                    {
                        dc.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                    }
                }
            }
        }
    }
}
