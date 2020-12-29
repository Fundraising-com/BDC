using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace GA.BDC.Data.DataLayer
{
    public struct EmailTemplate
    {
        public int TemplateId;
        public string TemplateName;
        public string Description;
        public string CultureCode;
    }

    public class TouchEmailTemplate
    {
        public static List<EmailTemplate> GetEmailTemplateBySearchTerm()
        {
            return GetEmailTemplateBySearchTerm(null, null, null, null);
        }

        public static List<EmailTemplate> GetEmailTemplateBySearchTerm(int? templateId, string cultureCode, string templateName, string description)
        {
            using (var dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
            {
                return (from template in dc.es_get_email_templates_by_search_term(templateId, cultureCode, templateName, description)
                        select new EmailTemplate
                        {
                            TemplateId = template.email_template_id,
                            TemplateName = template.email_template_name,
                            Description = template.description,
                            CultureCode = template.culture_code
                        }).ToList();
            }
        }
        /// <summary>
        /// Updates the Touch emails and member to reflect an invalid email
        /// </summary>
        /// <param name="touchId">Invalid touch id</param>
        public static void UpdateNDR(int touchId)
        {
            try
            {
                using (var dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
                {
                    dc.es_update_touch_ndr(touchId);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error while trying to update Touch [ID: {0}]", touchId), exception);
            }

        }

       public static int GetByExternalId(int externalId)
       {
          var id = 0;
          try
          {
             using (var dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
             {
                var result = dc.es_get_touch_by_external_od(externalId);
                return Convert.ToInt32(result.ReturnValue);
             }
          }
          catch
          {
             return id;
          }
       }
        /// <summary>
        /// Updates the member to reflect that he asks to be opt out
        /// </summary>
        /// <param name="touchId">Touch id</param>
        public static void UpdateComplaint(int touchId)
        {
            try
            {
                using (var dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
                {
                    dc.es_update_touch_complaint(touchId);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error while trying to update Touch [ID: {0}]", touchId), exception);
            }
        }

        public static void UpdateDelivery(int touchId)
        {
           try
           {
              using (var dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
              {
                 dc.es_update_touch_delivery(touchId);
              }
           }
           catch (Exception exception)
           {
              throw new Exception(string.Format("Error while trying to update Touch [ID: {0}]", touchId), exception);
           }
        }

        public static void UpdateStatus(int touchId, int status)
        {
           try
           {
              using (var dc = new EsubsGlobalV2DataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
              {
                 dc.es_update_touch_status(touchId, status);
              }
           }
           catch (Exception exception)
           {
              throw new Exception(string.Format("Error while trying to update Touch [ID: {0}]", touchId), exception);
           }
        }
    }
}
