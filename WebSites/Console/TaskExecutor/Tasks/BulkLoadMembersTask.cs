using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using SWCorporate.SystemEx;
using SWCorporate.SystemEx.Console;
using Microsoft.VisualBasic.FileIO;
namespace GA.BDC.Console.TaskExecutor.Tasks
{
    internal sealed class BulkLoadMembersTask : ITask<TaskFlags>
    {
        #region explicit implementation of the SWCorporate.SystemEx.Console.ITask<TaskFlags> interface

        void ITask<TaskFlags>.Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            var className = GetType().Name;
            var batchSize = 5;
            var smallerParentsList = new List<SageParent>();
            var currentListPosition = 0;
            Trace.TraceInformation("Start: " + className);

            try
            {
                var filePath = ConfigurationManager.AppSettings["CSVFilePath"];
                var parents = GetDataFromCSVFile(filePath);
                var businessRuleId = int.Parse(ConfigurationManager.AppSettings["Bulk_BusinessRuleId"]);
                var eventId = int.Parse(ConfigurationManager.AppSettings["Bulk_EventId"]);
                var eventType = int.Parse(ConfigurationManager.AppSettings["Bulk_EventTypeId"]);
                var partnerId = int.Parse(ConfigurationManager.AppSettings["Bulk_PartnerId"]);
                var senderHierachyId = int.Parse(ConfigurationManager.AppSettings["Bulk_SenderHiearchyId"]);
                var creationChannelId = int.Parse(ConfigurationManager.AppSettings["Bulk_CreationChannelId"]);
                while (parents.Count() > currentListPosition + batchSize)
                {
                    smallerParentsList = parents.Skip(currentListPosition).Take(batchSize).ToList();
                    using (var dataProvider = new DataProvider())
                    {
                        using (var transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 0, 10, 0)))
                        {
                            var templates = (from ets in dataProvider.email_template_selector
                                             from ef in dataProvider.email_flows
                                             from eft in dataProvider.email_flow_template
                                             from et in dataProvider.email_template
                                             from etc in dataProvider.email_template_culture
                                             where ets.email_template_selector_id == ef.email_template_selector_id
                                                 && ef.email_flow_id == 15
                                                 && ef.email_flow_id == eft.email_flow_id
                                                 && eft.event_type_id == eventType
                                                 && eft.email_template_id == et.email_template_id
                                                 && etc.email_template_id == et.email_template_id
                                                 && etc.culture_code == "en-US"
                                                 && ets.partner_id == partnerId
                                                 && (et.email_template_id == 518 || et.email_template_id == 519)
                                             orderby etc.email_template_id ascending
                                             select new { TemplateId = et.email_template_id, From = et.from_name, Subject = etc.subject, BodyHtml = etc.body_html, BodyText = etc.body_text, CreationChannelManual = ef.manual_creation_channel_id, CreationChannelManualOverride = eft.override_manual_creation_channel_id, CreationChannelImport = ef.import_creation_channel_id, CreationChannelImportOverride = eft.override_import_creation_channel_id, BusinessRuleId = eft.business_rule_id, UserTypeFrom = ef.user_type_from, UserTypeTo = ef.user_type_to }).ToList();
                            var firstEmail = templates.First();
                            var reminder = templates.Skip(1).First();
                            var firstEmailMessageText = ParseEmailForTouch(eventId, firstEmail.BodyHtml);
                            var reminderMessageText = ParseEmailForTouch(eventId, reminder.BodyHtml);
                            var touchInfos = new List<touch_info>();
                            var touchInfoFirstEmail = new touch_info
                            {
                                create_date = DateTime.Now,
                                business_rule_id = businessRuleId,
                                launch_date = DateTime.Now,
                            };
                            var touchInfoReminder = new touch_info
                            {
                                create_date = DateTime.Now,
                                business_rule_id = businessRuleId + 1,
                                launch_date = DateTime.Now.AddDays(5),
                            };
                            dataProvider.touch_info.Add(touchInfoFirstEmail);
                            dataProvider.touch_info.Add(touchInfoReminder);
                            dataProvider.SaveChanges();
                            var customEmailTemplateMainMail = new custom_email_template { create_date = DateTime.Now, body_html = firstEmailMessageText, subject = firstEmail.Subject, touch_info_id = touchInfoFirstEmail.touch_info_id, body_txt = firstEmail.BodyText };
                            dataProvider.custom_email_template.Add(customEmailTemplateMainMail);
                            var customEmailTemplateReminder = new custom_email_template { create_date = DateTime.Now, body_html = reminderMessageText, subject = reminder.Subject, touch_info_id = touchInfoReminder.touch_info_id, body_txt = reminder.BodyText };
                            dataProvider.custom_email_template.Add(customEmailTemplateReminder);
                            dataProvider.SaveChanges();
                            touchInfos.Add(touchInfoFirstEmail);
                            touchInfos.Add(touchInfoReminder);
                            foreach (var recipient in smallerParentsList)
                            {

                                var member = new member
                                {
                                    create_date = DateTime.Now,
                                    comments = "Member created by sending him an email at the registration for SAGE",
                                    bounced = false,
                                    culture_code = "en-US",
                                    deleted = false,
                                    email_address = recipient.Email,
                                    email_validated = false,
                                    first_name = string.IsNullOrEmpty(recipient.FirstName) ? " " : recipient.FirstName,
                                    last_name = string.IsNullOrEmpty(recipient.LastName) ? " " : recipient.LastName,
                                    partner_id = partnerId,
                                    opt_status_id = 1,
                                    unsubscribe = false
                                };
                                dataProvider.members.Add(member);
                                dataProvider.SaveChanges();
                                var memberHierarchy = new member_hierarchy
                                {
                                    active = true,
                                    create_date = DateTime.Now,
                                    parent_member_hierarchy_id = senderHierachyId,
                                    unsubscribe = false,
                                    member_id = member.member_id,
                                    creation_channel_id = creationChannelId
                                };
                                dataProvider.member_hierarchy.Add(memberHierarchy);
                                dataProvider.SaveChanges();
                                var eventParticipation = new event_participation
                                {
                                    event_id = eventId,
                                    member_hierarchy_id = memberHierarchy.member_hierarchy_id,
                                    participation_channel_id = 1,
                                    create_date = DateTime.Now,
                                    salutation = member.first_name + " " + member.last_name,
                                    agree_term_services = true,
                                    holiday_reminders = false
                                };
                                dataProvider.event_participation.Add(eventParticipation);
                                dataProvider.SaveChanges();

                                foreach (var touchInfo in touchInfos)
                                {
                                    var touch = new touch
                                    {
                                        create_date = DateTime.Now,
                                        event_participation_id = eventParticipation.event_participation_id,
                                        touch_info_id = touchInfo.touch_info_id,
                                        processed = 0,
                                    };
                                    dataProvider.touches.Add(touch);
                                }
                                dataProvider.SaveChanges();
                            }
                            transactionScope.Complete();
                        }
                    }
                    currentListPosition += batchSize;
                }
                
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message);
                InstrumentationProvider.SendExceptionNotification(exception, null);
                throw;
            }
            Trace.TraceInformation("End: " + className);
        }
        #endregion
        /// <summary>
        /// Reads the data from the CSV file and creates a SageParent class
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static IEnumerable<SageParent> GetDataFromCSVFile(string filePath)
        {
            var result = new List<SageParent>();
            try
            {
                using (var csvReader = new TextFieldParser(filePath))
                {
                    csvReader.SetDelimiters(new [] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;

                    while (!csvReader.EndOfData)
                    {
                        var fieldData = csvReader.ReadFields();
                        if (fieldData != null)
                        {
                            result.Add(new SageParent { Unknown = fieldData[0], SageCode = fieldData[1], FirstName = fieldData[2], LastName = fieldData[3], Email = fieldData[4], Points = Double.Parse(fieldData[5]) });    
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
            return result;
        }
        /// <summary>
        /// Replaces the special tags for the correct values
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ParseEmailForTouch(int eventId, string message)
        {
            var eventRedirect = GetEventRedirectById(eventId) ?? " ";
            message = message.Replace(eventRedirect, "[++redirect++]");
            message = message.Replace("Member Name", "[++participant++]").Replace("Member First Name", "[++participant_first_name++]").Replace("Member Last Name", "[++participant_last_name++]");
            return message;
        }
        /// <summary>
        /// Returns the redirection event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        private static string GetEventRedirectById(int eventId)
        {
            var sp = GetSponsorPersonalizationById(eventId);
            return sp != null ? sp.redirect : "Group?e=" + eventId;
        }
        /// <summary>
        /// Returns the sponsor personalization
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        private static personalization GetSponsorPersonalizationById(int eventId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @eventParticipation = dataProvider.event_participation.Single(
                    p => p.event_id == eventId && p.participation_channel_id == 3);
                var sponsorPersonalization =
                    dataProvider.personalizations.SingleOrDefault(
                        p => p.event_participation_id == @eventParticipation.event_participation_id);
                return sponsorPersonalization;
            }
        }
    }

    class SageParent
    {
        public string Unknown { get; set; }
        public string SageCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Points { get; set; }
    }
}
