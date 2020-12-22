using System;
using System.Linq;
using System.Transactions;
using GA.BDC.Data.MGP.esubs_global_v2.Models;

namespace GA.BDC.Web.MGP.Helpers.EmailTemplate
{
    public class TouchHelper
    {
        public static void InsertEmail(int template, int businessRule, int evtParticipation, string cultureCode, int reminderIntervalDay = 0)
        {
            using (var dataProvider = new DataProvider())
            {
                using (var transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 0, 10, 0)))
                {
                    var unsubscribe = (from ep in dataProvider.event_participation
                                       from mh in dataProvider.member_hierarchy
                                       where ep.event_participation_id == evtParticipation
                                          && ep.member_hierarchy_id == mh.member_hierarchy_id
                                          && mh.unsubscribe
                                       select mh.unsubscribe).SingleOrDefault();
                    if (unsubscribe)
                    {
                        return;
                    }

                    var emailTemplate = (from et in dataProvider.email_template
                                         from etc in dataProvider.email_template_culture
                                         where et.email_template_id == etc.email_template_id
                                            && etc.culture_code == cultureCode
                                            && et.email_template_id == template
                                         select new
                                         {
                                             TemplateId = et.email_template_id,
                                             TemplateTypeId = et.email_template_type_id,
                                             CultureCode = etc.culture_code,
                                             Subject = etc.subject,
                                             TemplateName = et.email_template_name,
                                             Description = et.description,
                                             TextBody = etc.body_text,
                                             HtmlBody = etc.body_html,
                                             ProcedureCall = et.param_procedure_call,
                                             FromName = et.from_name,
                                             FromEmail = et.from_email_address,
                                             ReplyToName = et.reply_to_name,
                                             ReplyToEmail = et.reply_to_email_address,
                                             BounceName = et.bounce_name,
                                             BounceEmail = et.bounce_email_address,
                                             TextFooter = etc.footer_text,
                                             HtmlFooter = etc.footer_html
                                         }).SingleOrDefault();
                    if (emailTemplate != null)
                    {
                        var ti = new touch_info
                        {
                            visitor_log_id = -1,
                            business_rule_id = businessRule,
                            launch_date = DateTime.Now,
                            create_date = DateTime.Now,
                            reminder_interval_day = reminderIntervalDay
                        };
                        dataProvider.touch_info.Add(ti);
                        dataProvider.SaveChanges();

                        var cet = new custom_email_template
                        {
                            touch_info_id = ti.touch_info_id,
                            subject = emailTemplate.Subject,
                            body_txt = emailTemplate.TextBody,
                            body_html = emailTemplate.HtmlBody,
                            create_date = DateTime.Now
                        };
                        dataProvider.custom_email_template.Add(cet);
                        dataProvider.SaveChanges();

                        var t = new touch
                        {
                            event_participation_id = evtParticipation,
                            member_hierarchy_id = null,
                            touch_info_id = ti.touch_info_id,
                            processed = 0,
                            create_date = DateTime.Now
                        };
                        dataProvider.touches.Add(t);
                        dataProvider.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"Failed to create email template object: template={template},businessRule={businessRule},evtParticipation={evtParticipation},cultureCode={cultureCode},reminderIntervalDay={reminderIntervalDay}");
                    }

                    transactionScope.Complete();
                }
            }
        }
    }
}