using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using SWCorporate.SystemEx.Console;
using System.Data.Entity.Validation;

namespace GA.BDC.Console.TaskExecutor.Tasks
{
    internal sealed class TouchEmailProcessQueueTask : ITask<TaskFlags>
    {
        void ITask<TaskFlags>.Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            Trace.TraceInformation("Start: " + GetType().Name);
            var exceptions = new List<Exception>();
            using (var dataProvider = new DataProvider())
            {
                var touchesToBeProcessed = dataProvider.touch_emails_process_queue.Where(p => p.status == (int)touch_email_process_queue_status.Scheduled).Take(int.Parse(ConfigurationManager.AppSettings["TouchEmailProcessQueueThrottle"])).ToList();

                foreach (var touchEmailProcessQueue in touchesToBeProcessed.GroupBy(p => new { p.email_flow_id, p.event_type }))
                {
                    var templates = (from ef in dataProvider.email_flows
                                     from eft in dataProvider.email_flow_template
                                     from et in dataProvider.email_template
                                     from etc in dataProvider.email_template_culture
                                     from br in dataProvider.business_rule
                                     where ef.email_flow_id == touchEmailProcessQueue.Key.email_flow_id
                                           && ef.email_flow_id == eft.email_flow_id
                                           && eft.event_type_id == touchEmailProcessQueue.Key.event_type
                                           && eft.email_template_id == et.email_template_id
                                           && etc.email_template_id == et.email_template_id
                                           && etc.culture_code == "en-US"
                                           && et.email_template_id == br.email_template_id
                                           && br.active
                                     orderby etc.email_template_id ascending
                                     select new { TemplateId = et.email_template_id, From = et.from_name, Subject = etc.subject, BodyHtml = etc.body_html, BodyText = etc.body_text, CreationChannelManual = ef.manual_creation_channel_id, CreationChannelManualOverride = eft.override_manual_creation_channel_id, CreationChannelImport = ef.import_creation_channel_id, CreationChannelImportOverride = eft.override_import_creation_channel_id, BusinessRuleId = eft.business_rule_id, UserTypeFrom = ef.user_type_from, UserTypeTo = ef.user_type_to }).ToList();
                    foreach (var queueItem in touchEmailProcessQueue)
                    {
                        try
                        {
                            var partner = LoadPartner(queueItem.partner_id);

                            var model = new KickOff { ReminderRecurrency = queueItem.reminder_recurrency, MessageType = queueItem.message_type };
                            if (templates.Any())
                            {
                                var mainTemplate = templates[0];
                                model.TemplateId = mainTemplate.TemplateId;
                                model.From = mainTemplate.From;
                                model.Subject = mainTemplate.Subject;
                                model.CustomMessage = queueItem.custom_message;
                                model.Message = mainTemplate.BodyHtml;
                                model.TextMessage = mainTemplate.BodyText;
                                model.CreationChannelImport = mainTemplate.CreationChannelImportOverride ?? mainTemplate.CreationChannelImport;
                                model.CreationChannelManual = mainTemplate.CreationChannelManualOverride ?? mainTemplate.CreationChannelManual;
                                model.BusinessRuleId = mainTemplate.BusinessRuleId;
                                TagParsor.ParseEmail(queueItem.event_id, mainTemplate.UserTypeFrom, partner, new User { Email = queueItem.email_address, FirstName = queueItem.first_name, LastName = queueItem.last_name, EventParticipationId = queueItem.sponsor_event_participation_id }, model);
                                if (templates.Count > 1)
                                {
                                    var reminders = new List<Reminder>();
                                    Reminder reminder = null;
                                    for (int i = 1; i < templates.Count; i++)
                                    {
                                        reminder = new Reminder { TemplateId = templates[i].TemplateId, From = templates[i].From, Subject = templates[i].Subject, Message = templates[i].BodyHtml, TextMessage = templates[i].BodyText, CreationChannelImport = templates[i].CreationChannelImportOverride == null ? templates[i].CreationChannelImport : (int)templates[i].CreationChannelImportOverride, CreationChannelManual = templates[i].CreationChannelManualOverride == null ? templates[i].CreationChannelManual : (int)templates[i].CreationChannelManualOverride, DeleteReminder = false, BusinessRuleId = templates[i].BusinessRuleId };
                                        reminders.Add(reminder);
                                        TagParsor.ParseEmail(queueItem.event_id, mainTemplate.UserTypeFrom, partner, new User { Email = queueItem.email_address, FirstName = queueItem.first_name, LastName = queueItem.last_name, EventParticipationId = queueItem.sponsor_event_participation_id }, reminder);
                                    }
                                    model.Reminders = reminders.ToArray();
                                }
                                else
                                    model.Reminders = new List<Reminder>().ToArray();
                            }

                            var email_flow = dataProvider.email_flows.Single(p => p.email_flow_id == queueItem.email_flow_id);
                            int senderMemberHierarchyId = 0;
                            if (queueItem.is_sponsor)
                            {
                                senderMemberHierarchyId = (from mh in dataProvider.member_hierarchy
                                                           from ep in dataProvider.event_participation
                                                           where mh.member_hierarchy_id == ep.member_hierarchy_id
                                                                 && ep.event_id == queueItem.event_id
                                                                 && ep.participation_channel_id == 3
                                                           select mh.member_hierarchy_id).Single();
                            }
                            else
                            {
                                senderMemberHierarchyId = (from mh in dataProvider.member_hierarchy
                                                           from ep in dataProvider.event_participation
                                                           where mh.member_hierarchy_id == ep.member_hierarchy_id
                                                                 && ep.event_id == queueItem.event_id
                                                                 && ep.event_participation_id == queueItem.sponsor_event_participation_id
                                                           select mh.member_hierarchy_id).Single();
                            }
                            TagParsor.ParseEmailForTouch(queueItem.event_id, (EventTypeInfo)queueItem.event_type,
                               email_flow.user_type_from, email_flow.user_type_to, model, queueItem.custom_message, queueItem.sponsor_event_participation_id);

                            string html = string.Empty, text = string.Empty;
                            html = model.Message;
                            text = model.TextMessage;

                            var touchInfos = new List<touch_info>();
                            var touchInfoMainEmail = new touch_info
                            {
                                create_date = DateTime.Now,
                                business_rule_id = queueItem.bussiness_rule_id,
                                launch_date = DateTime.Now,
                            };
                            dataProvider.touch_info.Add(touchInfoMainEmail);
                            dataProvider.SaveChanges();
                            var customEmailTemplateMainMail = new custom_email_template
                            {
                                create_date = DateTime.Now,
                                body_html = html,
                                subject = queueItem.subject,
                                touch_info_id = touchInfoMainEmail.touch_info_id,
                                body_txt = text
                            };
                            dataProvider.custom_email_template.Add(customEmailTemplateMainMail);
                            dataProvider.SaveChanges();
                            touchInfos.Add(touchInfoMainEmail);
                            var days = 1;
                            if (model.Reminders != null)
                            {
                                foreach (var reminder in model.Reminders.Where(p => !p.DeleteReminder))
                                {
                                    TagParsor.ParseEmailForTouch(queueItem.event_id, (EventTypeInfo)queueItem.event_type, email_flow.user_type_from,
                                       email_flow.user_type_to, reminder, string.Empty, queueItem.sponsor_event_participation_id);

                                    html = reminder.Message;
                                    text = reminder.TextMessage;

                                    var touchInfoReminder = new touch_info
                                    {
                                        create_date = DateTime.Now,
                                        business_rule_id = reminder.BusinessRuleId,
                                        launch_date = DateTime.Now.AddDays(days * queueItem.reminder_recurrency),
                                    };

                                    dataProvider.touch_info.Add(touchInfoReminder);
                                    dataProvider.SaveChanges();
                                    var customEmailTemplateReminder = new custom_email_template
                                    {
                                        create_date = DateTime.Now,
                                        body_html = html,
                                        subject = queueItem.subject,
                                        touch_info_id = touchInfoReminder.touch_info_id,
                                        body_txt = text
                                    };
                                    dataProvider.custom_email_template.Add(customEmailTemplateReminder);
                                    dataProvider.SaveChanges();
                                    touchInfos.Add(touchInfoReminder);
                                    days++;
                                }
                            }
                            var eventParticipation = (from m in dataProvider.members
                                                      from mh in dataProvider.member_hierarchy
                                                      from ep in dataProvider.event_participation
                                                      where m.member_id == mh.member_id
                                                            && mh.parent_member_hierarchy_id == senderMemberHierarchyId
                                                            && ep.member_hierarchy_id == mh.member_hierarchy_id
                                                            && ep.event_id == queueItem.event_id
                                                            &&
                                                            m.email_address.Trim()
                                                               .Equals(queueItem.email_address.Trim(), StringComparison.InvariantCultureIgnoreCase)
                                                      select ep).FirstOrDefault();
                            if (eventParticipation == null || eventParticipation.event_participation_id == 0)
                            {
                                var member = new member
                                {
                                    create_date = DateTime.Now,
                                    comments = "Member created by sending him an email at the registration",
                                    bounced = false,
                                    culture_code = "en-US",
                                    deleted = false,
                                    email_address = queueItem.email_address,
                                    email_validated = false,
                                    first_name =
                                      string.IsNullOrEmpty(queueItem.first_name)
                                         ? " "
                                         : HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(queueItem.first_name)),
                                    last_name =
                                      string.IsNullOrEmpty(queueItem.last_name)
                                         ? " "
                                         : HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(queueItem.last_name)),
                                    partner_id = queueItem.partner_id,
                                    opt_status_id = 1,
                                    unsubscribe = false
                                };
                                dataProvider.members.Add(member);
                                dataProvider.SaveChanges();
                                var memberHierarchy = new member_hierarchy
                                {
                                    active = true,
                                    create_date = DateTime.Now,
                                    parent_member_hierarchy_id = senderMemberHierarchyId,
                                    unsubscribe = false,
                                    member_id = member.member_id,
                                    creation_channel_id = queueItem.creation_channel_id
                                };
                                dataProvider.member_hierarchy.Add(memberHierarchy);
                                dataProvider.SaveChanges();
                                eventParticipation = new event_participation
                                {
                                    event_id = queueItem.event_id,
                                    member_hierarchy_id = memberHierarchy.member_hierarchy_id,
                                    participation_channel_id = 1,
                                    create_date = DateTime.Now,
                                    salutation = member.first_name + " " + member.last_name,
                                    agree_term_services = true,
                                    holiday_reminders = false
                                };
                                dataProvider.event_participation.Add(eventParticipation);
                                dataProvider.SaveChanges();
                            }
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
                            dataProvider.touch_emails_process_queue.Remove(queueItem);
                            dataProvider.SaveChanges();
                        }
                        catch (DbEntityValidationException dbEntityValidationException)
                        {
                            foreach (var validationError in dbEntityValidationException.EntityValidationErrors.SelectMany(p => p.ValidationErrors))
                            {
                                dbEntityValidationException.Data.Add($"EntityValidationError - {validationError.PropertyName}", validationError.ErrorMessage);
                            }
                            exceptions.Add(dbEntityValidationException);
                        }
                        catch (Exception exception)
                        {
                            exceptions.Add(exception);
                        }
                        
                    }
                }

                if (exceptions.Any())
                {
                    throw new AggregateException(exceptions);
                }
            }
        }

        private Partner LoadPartner(int partnerId)
        {
            var partner = new Partner { Id = partnerId };
            using (var dataProvider2 = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Data.Properties.Settings.esubs_global_v2ConnectionString"].ConnectionString))
            {
                var partnerInformation = dataProvider2.es_get_partner_branding(partner.Id).FirstOrDefault();
                partner.ProductOffer = (ProductOffer)partnerInformation.product_offer_id;
                partner.Program = (Program)partnerInformation.program_id;
                partner.PaymentTo = (PaymentTo)partnerInformation.payment_to;
                partner.CultureCode = partnerInformation.culture_code;

            }
            using (var dataProvider = new Data.MGP.EFRCommon.LINQ.DataProviderDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EFRCommon"].ConnectionString))
            {
                var customAttributeValues = dataProvider.es_get_partner_custom_attribute_values(partner.Id).ToList();

                if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PRIZE_PROGRAM))
                {
                    var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PRIZE_PROGRAM).value;
                    partner.PrizeProgram = string.IsNullOrEmpty(value) || bool.Parse(value);
                }

                if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_REDIRECT_TO_LANDING_PAGE))
                {
                    var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_REDIRECT_TO_LANDING_PAGE).value;
                    partner.RedirectToLandingPage = !string.IsNullOrEmpty(value) && bool.Parse(value);
                }
                if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_ESUBS_URL))
                {
                    var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_ESUBS_URL).value;
                    partner.ESubsUrl = value;
                }
                if (customAttributeValues.Any(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PAP_A_AID))
                {
                    var value = customAttributeValues.First(p => p.partner_attribute_name == Constants.CUSTOM_ATTRIBUTE_PAP_A_AID).value;
                    partner.PAPAffiliateId = value;
                }
                var profits = dataProvider.es_get_partner_profit(partnerId).ToList();
                if (profits.Any())
                {
                    var profit = profits.First();
                    partner.ProfitPercentage = profit.Percentage;
                    partner.ProfitGroupId = profit.ProfitGroupId;
                    partner.ProfitDescription = profit.Description;
                    partner.Disclaimer = profit.Disclaimer;
                    partner.Name = profit.Name;
                    partner.PartnerTypeId = profit.PartnerType;
                }
            }
            return partner;
        }
    }
    #region View Models
    public class Partner
    {
        /// <summary>
        /// Partner ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// PartnerTypeId
        /// </summary>
        public int PartnerTypeId { get; set; }
        /// <summary>
        /// Culture Code
        /// </summary>
        public string CultureCode { get; set; }
        /// <summary>
        /// Profit Percentage
        /// </summary>
        public double ProfitPercentage { get; set; }
        /// <summary>
        /// Profit Group Id
        /// </summary>
        public int? ProfitGroupId { get; set; }
        /// <summary>
        /// Profit Description
        /// </summary>
        public string ProfitDescription { get; set; }
        /// <summary>
        /// Custom Disclamimer
        /// </summary>
        public string Disclaimer { get; set; }
        /// <summary>
        /// Show only the Partner Logo on the Top
        /// </summary>
        public bool ShowOnlyPartnerLogo { get; set; }
        /// <summary>
        /// Image URL for the Logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// Hide the Main Menu at the Top of the Page
        /// </summary>
        public bool HideMainMenu { get; set; }
        /// <summary>
        /// Show the Popular Items at the Group Pages
        /// </summary>
        public bool ShowPopularItems { get; set; }
        /// <summary>
        /// Prize program
        /// </summary>
        public bool PrizeProgram { get; set; }
        /// <summary>
        /// Product Type Offered
        /// </summary>
        public ProductOffer ProductOffer { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Insert leads for this partner
        /// </summary>
        public bool InsertAsLead { get; set; }
        /// <summary>
        /// Program the the Partner belongs to
        /// </summary>
        public Program Program { get; set; }
        /// <summary>
        /// Payment to Partner or Group
        /// </summary>
        public PaymentTo PaymentTo { get; set; }
        /// <summary>
        /// Hide the Sponsor Check Report
        /// </summary>
        public bool HideCheckReport { get; set; }
        /// <summary>
        /// Group Display
        /// </summary>
        public string GroupDisplay { get; set; }
        /// <summary>
        /// Redirect To Landing Page
        /// </summary>
        public bool RedirectToLandingPage { get; set; }
        /// <summary>
        /// ESubs Url
        /// </summary>
        public string ESubsUrl { get; set; }

        /// <summary>
        /// Show Receipt Link
        /// </summary>
        public bool ShowReceiptLink { get; set; }
        /// <summary>
        /// PAP Affiliate Id
        /// </summary>
        public string PAPAffiliateId { get; set; }
        /// <summary>
        /// Hides all profit related information
        /// </summary>
        public bool HideProfit { get; set; }
        /// <summary>
        /// a customized message for the goal
        /// </summary>
        public bool HideGoal { get; set; }
        /// <summary>
        /// Hides all the how it works links
        /// </summary>
        public bool HideHowItWorks { get; set; }
        /// <summary>
        /// Message for the Shop Now for our cause text
        /// </summary>
        public string ShopNowForOurCauseMessage { get; set; }
        /// <summary>
        /// Hides all the reports in the Campaign Manager
        /// </summary>
        public bool HideAllReports { get; set; }
        /// <summary>
        /// Hides the FAQ link
        /// </summary>
        public bool HideFaq { get; set; }
        /// <summary>
        /// Hides the Promote your Page menu
        /// </summary>
        public bool HidePromotionPage { get; set; }
        /// <summary>
        /// Hide the Browse Participants link
        /// </summary>
        public bool HideBrowseParticipants { get; set; }
        /// <summary>
        /// Custom message for the participant register message
        /// </summary>
        public bool CustomParticipantRegisterMessage { get; set; }
        /// <summary>
        /// Some events can hide the JOIN button
        /// </summary>
        public string EventsToHideJOINButton { get; set; }
        // <summary>
        /// Display Total Amount in Amount Raised Thermometer instead of Profit
        /// </summary>
        public bool ShowTotalAmountInThermometer { get; set; }
    }

    public enum ProductOffer
    {
        All = 1,
        MagazineOnly = 2,
        MagazineResto = 3,
        RestoMagazine = 4,
        DonationOnly = 5,
        MagazineAndMore = 6,
        BoxTops = 7
    }

    public enum Program
    {
        Undefined = 0,
        ESubs = 1,
        Mvp = 2,
        Alumni = 3,
        Traditional = 4,
        Schools = 5
    }

    public enum PaymentTo : int
    {
        Group = 0,
        Partner = 1
    }

    public class User
    {
        public int Id { get; set; }
        public int HierarchyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int EventParticipationId { get; set; }
        public bool IsLoggedIn { get; set; }
        public int MemberTypeId { get; set; }
        public string CompleteName
        {
            get
            {
                return FirstName.IsNotEmpty()
                        ? string.Concat(FirstName.Trim(), LastName.IsNotEmpty() ? " " + LastName.Trim() : string.Empty)
                        : string.Empty;
            }
        }
        public UserType UserTypeFromMemberType
        {
            get
            {
                switch (MemberTypeId)
                {
                    case (int)UserType.UNKNOWN:
                        return UserType.UNKNOWN;
                    case (int)UserType.SPONSOR:
                        return UserType.SPONSOR;
                    case (int)UserType.PARTICIPANT:
                        return UserType.PARTICIPANT;
                    case (int)UserType.SUPPORTER:
                        return UserType.SUPPORTER;
                    default:
                        return UserType.UNKNOWN;
                }
            }
        }
        public UserType UserTypeInfo { get; set; }
        public bool IsSponsor
        {
            get
            {
                return MemberTypeId > 0
                    ? (UserTypeFromMemberType == UserType.SPONSOR)
                    : (UserTypeInfo == UserType.SPONSOR);
            }
        }

        public bool IsParticipant
        {
            get
            {
                return MemberTypeId > 0
                    ? (UserTypeFromMemberType == UserType.PARTICIPANT)
                    : (UserTypeInfo == UserType.PARTICIPANT);
            }
        }

        public bool IsSupporter
        {
            get
            {
                return MemberTypeId > 0
                    ? (UserTypeFromMemberType == UserType.SUPPORTER)
                    : (UserTypeInfo == UserType.SUPPORTER);
            }
        }

        public bool IsUnknown
        {
            get
            {
                return MemberTypeId > 0
                    ? (UserTypeFromMemberType == UserType.UNKNOWN)
                    : (UserTypeInfo == UserType.UNKNOWN);
            }
        }
    }

    public enum UserType : int
    {
        UNKNOWN = 0,
        SPONSOR = 1,
        PARTICIPANT = 2,
        SUPPORTER = 3
    }

    public class EmailTemplateBase
    {
        public int TemplateId { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }

        public string Message { get; set; }
        public string TextMessage { get; set; }
        public int CreationChannelManual { get; set; }
        public int CreationChannelImport { get; set; }
        public int BusinessRuleId { get; set; }
        public int MessageType { get; set; }

        public string CustomMessage { get; set; }
    }

    public class KickOff : EmailTemplateBase
    {
        public int ReminderRecurrency { get; set; }
        public Reminder[] Reminders { get; set; }
        public bool IsDraft { get; set; }
    }

    public class Reminder : EmailTemplateBase
    {
        public bool DeleteReminder { get; set; }
    }

    public enum EventTypeInfo
    {
        GROUP_FUNDRAISER_WITH_SUBPAGE = 1,
        GROUP_FUNDRAISER_WITHOUT_SUBPAGE = 2,
        INDIVIDUAL_FUNDRAISER = 3
    }
    #endregion

    #region Helper Classes
    public static class TagParsor
    {
        #region Public Static Methods

        public static void ParseEmail(int eventId, string userTypeFrom, Partner partner, User user, EmailTemplateBase ko)
        {
            var eventRedirect = GetEventRedirectById(eventId) ?? " ";
            var eventName = GetEventNameById(eventId);
            switch (userTypeFrom.ToLower())
            {
                case "sponsor":
                    var spImagePath = GetCoverAlbumImageUrlById(eventId);
                    if (spImagePath.IsEmpty())
                        spImagePath = string.Format("{0}/{1}",
                                            "/Content/images/personalization",
                                            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);

                    ko.From = ko.From.Replace("[++sponsor_name++]", user.Email);
                    ko.From = ko.From.Replace("[++sponsor_email++]", user.Email);
                    ko.Subject = ko.Subject.Replace("[++Sponsor_Name++]", user.CompleteName);
                    ko.Subject = ko.Subject.Replace("[++sponsor_name++]", user.CompleteName);
                    ko.Subject = ko.Subject.Replace("[++sponsor_first_name++]", user.FirstName);
                    ko.Subject = ko.Subject.Replace("[++sponsor_last_name++]", user.LastName);
                    ko.Subject = ko.Subject.Replace("[++sponsor_email++]", user.Email);
                    ko.Subject = ko.Subject.Replace("[++Group_name++]", eventName);
                    ko.Subject = ko.Subject.Replace("[++Group_Name++]", eventName);
                    ko.Subject = ko.Subject.Replace("[++campaign++]", eventName);
                    ko.Subject = ko.Subject.Replace("[++participant_first_name++]", "Member First Name");
                    ko.Subject = ko.Subject.Replace("[++participant_last_name++]", "Member Last Name");
                    ko.Message = ko.Message.Replace("[++redirect++]", eventRedirect);
                    ko.Message = ko.Message.Replace("[++Sponsor_Name++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++sponsor_name++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++sponsor_first_name++]", user.FirstName);
                    ko.Message = ko.Message.Replace("[++sponsor_last_name++]", user.LastName);
                    ko.Message = ko.Message.Replace("[++sponsor_email++]", user.Email);
                    ko.Message = ko.Message.Replace("[++Group_name++]", eventName);
                    ko.Message = ko.Message.Replace("[++Group_Name++]", eventName);
                    ko.Message = ko.Message.Replace("[++campaign++]", eventName);
                    ko.Message = ko.Message.Replace("[++group_name++]", eventName);
                    ko.Message = ko.Message.Replace("[++event_name++]", eventName);
                    ko.Message = ko.Message.Replace("[++event_id++]", eventId.ToString());
                    ko.Message = ko.Message.Replace("[++Sponsor_name++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++participant++]", "Member Name");
                    ko.Message = ko.Message.Replace("[++participant_first_name++]", "Member First Name");
                    ko.Message = ko.Message.Replace("[++participant_last_name++]", "Member Last Name");
                    ko.Message = ko.Message.Replace("[++supporter++]", "Member Name");
                    ko.Message = ko.Message.Replace("[++image_path++]", spImagePath);
                    ko.TextMessage = ko.TextMessage.Replace("[++participant++]", "Member Name");
                    ko.TextMessage = ko.TextMessage.Replace("[++participant_first_name++]", "Member First Name");
                    ko.TextMessage = ko.TextMessage.Replace("[++participant_last_name++]", "Member Last Name");
                    ko.TextMessage = ko.TextMessage.Replace("[++supporter++]", "Member Name");
                    ko.TextMessage = ko.TextMessage.Replace("[++Sponsor_Name++]", user.CompleteName);
                    ko.TextMessage = ko.TextMessage.Replace("[++sponsor_name++]", user.CompleteName);
                    ko.TextMessage = ko.TextMessage.Replace("[++sponsor_first_name++]", user.FirstName);
                    ko.TextMessage = ko.TextMessage.Replace("[++sponsor_last_name++]", user.LastName);
                    ko.TextMessage = ko.TextMessage.Replace("[++sponsor_email++]", user.Email);
                    ko.TextMessage = ko.TextMessage.Replace("[++Group_name++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++Group_Name++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++campaign++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++group_name++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++event_id++]", eventId.ToString());
                    ko.TextMessage = ko.TextMessage.Replace("[++event_name++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++Sponsor_name++]", user.CompleteName);
                    ko.TextMessage = ko.TextMessage.Replace("[++redirect++]", eventRedirect);
                    break;
                case "participant":
                case "supporter":
                    var participantRedirect = GetParticipantRedirectById(user.EventParticipationId);
                    var parImagePath = GetParticipantImageUrlById(user.EventParticipationId);
                    if (parImagePath.IsEmpty())
                        parImagePath = string.Format("{0}/{1}",
                                            "/Content/images/personalization",
                                            Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);

                    ko.From = ko.From.Replace("[++sponsor_name++]", user.Email);
                    ko.From = ko.From.Replace("[++Sponsor_name++]", user.Email);
                    ko.From = ko.From.Replace("[++sponsor_email++]", user.Email);
                    ko.From = ko.From.Replace("[++participant_name++]", user.Email);
                    ko.From = ko.From.Replace("[++participant_first_name++]", user.FirstName);
                    ko.From = ko.From.Replace("[++participant_last_name++]", user.LastName);
                    ko.Subject = ko.Subject.Replace("[++sponsor_email++]", user.Email);
                    ko.Subject = ko.Subject.Replace("[++Participant_name++]", user.CompleteName);
                    ko.Subject = ko.Subject.Replace("[++participant_name++]", user.CompleteName);
                    ko.Subject = ko.Subject.Replace("[++participant_first_name++]", user.FirstName);
                    ko.Subject = ko.Subject.Replace("[++participant_last_name++]", user.LastName);
                    ko.Subject = ko.Subject.Replace("[++participant_email++]", user.CompleteName);
                    ko.Subject = ko.Subject.Replace("[++participant++]", user.CompleteName);
                    ko.Subject = ko.Subject.Replace("[++Group_name++]", eventName);
                    ko.Subject = ko.Subject.Replace("[++campaign++]", eventName);
                    ko.Message = ko.Message.Replace("[++supporter++]", "Member Name");
                    ko.Message = ko.Message.Replace("[++sponsor_email++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++Participant_name++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++participant++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++participant_name++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++event_id++]", eventId.ToString());
                    ko.Message = ko.Message.Replace("[++participant_first_name++]", user.FirstName);
                    ko.Message = ko.Message.Replace("[++participant_last_name++]", user.LastName);
                    ko.Message = ko.Message.Replace("[++participant_email++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++Group_name++]", eventName);
                    ko.Message = ko.Message.Replace("[++campaign++]", eventName);
                    ko.Message = ko.Message.Replace("[++group_name++]", eventName);
                    ko.Message = ko.Message.Replace("[++event_name++]", eventName);
                    ko.Message = ko.Message.Replace("[++student_name++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++parent_name++]", user.CompleteName);
                    ko.Message = ko.Message.Replace("[++image_path++]", parImagePath);
                    if (participantRedirect != null && participantRedirect.Trim().Length > 0)
                    {
                        ko.Message = ko.Message.Replace("[++redirect++]", string.Concat(eventRedirect.Trim(), "/", participantRedirect));
                        ko.TextMessage = ko.TextMessage.Replace("[++redirect++]", string.Concat(eventRedirect.Trim(), "/", participantRedirect));
                    }
                    else
                    {
                        ko.Message = ko.Message.Replace("[++redirect++]", eventRedirect);
                        ko.TextMessage = ko.TextMessage.Replace("[++redirect++]", eventRedirect);
                    }
                    ko.TextMessage = ko.TextMessage.Replace("[++sponsor_email++]", user.CompleteName);
                    ko.TextMessage = ko.TextMessage.Replace("[++Participant_name++]", user.CompleteName);
                    ko.TextMessage = ko.TextMessage.Replace("[++participant_first_name++]", user.FirstName);
                    ko.TextMessage = ko.TextMessage.Replace("[++participant_last_name++]", user.LastName);
                    ko.TextMessage = ko.TextMessage.Replace("[++participant++]", user.CompleteName);
                    ko.TextMessage = ko.TextMessage.Replace("[++Group_name++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++campaign++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++group_name++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++event_name++]", eventName);
                    ko.TextMessage = ko.TextMessage.Replace("[++student_name++]", user.CompleteName);
                    ko.TextMessage = ko.TextMessage.Replace("[++parent_name++]", user.CompleteName);
                    ko.TextMessage = ko.TextMessage.Replace("[++event_id++]", eventId.ToString());
                    break;
                default:
                    break;
            }

            /* Change the domain links:
             *  OLD: http://my.fundraising.com
             *  NEW: http://efundraising.com   
             */
            ko.Message = ko.Message.Replace(Constants.LEGACY_DOMAIN_HOST, string.Empty + Constants.NEW_DOMAIN_HOST);
            ko.TextMessage = ko.TextMessage.Replace(Constants.LEGACY_DOMAIN_HOST, string.Empty + Constants.NEW_DOMAIN_HOST);

            /* Email parsing by PRODUCT:
             *      Partner product offering comes from table dbo.partner_product_offer
             */
            var htmlMessage = new StringBuilder(ko.Message);
            var textMessage = new StringBuilder(ko.TextMessage);
            var tags = GetTags();
            var etts = GetEmailTemplateTagsById(ko.TemplateId, (int)partner.ProductOffer);
            if (etts != null && etts.Any())
            {
                // Replace all email template tags
                foreach (var ett in etts)
                {
                    int startTagIndex, endTagIndex, length;
                    var tag = tags.SingleOrDefault(x => x.tag_id == ett.tag_id);
                    if (tag != null)
                    {
                        var tagsToExclude = Properties.Settings.Default.Properties["EmailTag_Partner_Exclusion_" + tag.start_tag_name.Replace("[++", string.Empty).Replace("++]", string.Empty)];
                        if (tagsToExclude != null)
                        {
                            if (tagsToExclude.DefaultValue.ToString().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>().Contains(partner.Id.ToString()))
                                continue;
                        }

                        // Replace HTML Message
                        startTagIndex = ko.Message.IndexOf(tag.start_tag_name);
                        endTagIndex = ko.Message.IndexOf(tag.end_tag_name);
                        if (!(startTagIndex < 0 || endTagIndex < 0 || (endTagIndex - startTagIndex) < 0))
                        {
                            length = endTagIndex - startTagIndex + tag.end_tag_name.Length;
                            htmlMessage.Replace(ko.Message.Substring(startTagIndex, length), tag.description);
                        }

                        // Replace TEXT Message
                        startTagIndex = ko.TextMessage.IndexOf(tag.start_tag_name);
                        endTagIndex = ko.TextMessage.IndexOf(tag.end_tag_name);
                        if (!(startTagIndex < 0 || endTagIndex < 0 || (endTagIndex - startTagIndex) < 0))
                        {
                            length = endTagIndex - startTagIndex + tag.end_tag_name.Length;
                            textMessage.Replace(ko.TextMessage.Substring(startTagIndex, length), tag.description);
                        }
                    }
                }
            }
            // Remove all remaining tags
            var distinctStartTags = tags.Select(x => x.start_tag_name).Distinct().ToList();
            var distinctEndTags = tags.Select(x => x.end_tag_name).Distinct().ToList();
            foreach (string startTag in distinctStartTags)
            {
                htmlMessage.Replace(startTag, string.Empty);
                textMessage.Replace(startTag, string.Empty);
            }
            foreach (string endTag in distinctEndTags)
            {
                htmlMessage.Replace(endTag, string.Empty);
                textMessage.Replace(endTag, string.Empty);
            }

            // Remove [++BEGIN++], [++END++], [++SALUTATION++], [++HTML1++], [++HEADER++], [++TITLE++]Personal Message (can be edited)
            int dynBeginTagIndex, dynEndTagIndex, dynMsglength;
            dynBeginTagIndex = ko.Message.IndexOf("[++BEGIN++]");
            dynEndTagIndex = ko.Message.IndexOf("[++END++]");
            dynMsglength = dynEndTagIndex - dynBeginTagIndex;
            if (!(dynBeginTagIndex < 0 || dynEndTagIndex < 0 || (dynEndTagIndex - dynBeginTagIndex) < 0))
                htmlMessage.Replace(ko.Message.Substring(dynBeginTagIndex, dynMsglength), ko.Message.Substring(dynBeginTagIndex, dynMsglength).StripBR());
            dynBeginTagIndex = ko.TextMessage.IndexOf("[++BEGIN++]");
            dynEndTagIndex = ko.TextMessage.IndexOf("[++END++]");
            dynMsglength = dynEndTagIndex - dynBeginTagIndex;
            if (!(dynBeginTagIndex < 0 || dynEndTagIndex < 0 || (dynEndTagIndex - dynBeginTagIndex) < 0))
                textMessage.Replace(ko.TextMessage.Substring(dynBeginTagIndex, dynMsglength), ko.TextMessage.Substring(dynBeginTagIndex, dynMsglength).StripNewLine());
            htmlMessage.Replace("[++BEGIN++]", string.Empty).Replace("[++END++]", string.Empty).Replace("[++SALUTATION++]", string.Empty)
                       .Replace("[++HTML1++]<br />[++HEADER++]<br />", string.Empty).Replace("[++HTML1++]<br />", string.Empty).Replace("[++HTML1++]", string.Empty)
                       .Replace("[++HEADER++]", string.Empty).Replace("[++TITLE++]Personal Message (can be edited)", string.Empty)
                       .Replace("[++TITLE++] Personal Message (can be edited)", string.Empty)
                       .Replace("[++ROWS++]15", string.Empty).Replace("[++ROWS++] 15", string.Empty);
            textMessage.Replace("[++BEGIN++]", string.Empty).Replace("[++END++]", string.Empty).Replace("[++SALUTATION++]", string.Empty)
                       .Replace("[++HTML1++]", string.Empty).Replace("[++HEADER++]", string.Empty)
                       .Replace("[++TITLE++]Personal Message (can be edited)", string.Empty).Replace("[++TITLE++] Personal Message (can be edited)", string.Empty)
                       .Replace("[++ROWS++]15", string.Empty).Replace("[++ROWS++] 15", string.Empty);

            // Prize Program
            if (partner.PrizeProgram)
            {
                htmlMessage.Replace("[++PRIZE++]", string.Empty).Replace("[++ENDPRIZE++]", string.Empty);
                textMessage.Replace("[++PRIZE++]", string.Empty).Replace("[++ENDPRIZE++]", string.Empty);
            }
            else
            {
                dynBeginTagIndex = ko.Message.IndexOf("[++PRIZE++]");
                dynEndTagIndex = ko.Message.IndexOf("[++ENDPRIZE++]");
                dynMsglength = dynEndTagIndex + 14 - dynBeginTagIndex;
                if (!(dynBeginTagIndex < 0 || dynEndTagIndex < 0 || (dynEndTagIndex - dynBeginTagIndex) < 0))
                    htmlMessage.Replace(ko.Message.Substring(dynBeginTagIndex, dynMsglength), string.Empty)
                               .Replace("<br /><br /><br /><br />", "<br /><br />");
                dynBeginTagIndex = ko.TextMessage.IndexOf("[++PRIZE++]");
                dynEndTagIndex = ko.TextMessage.IndexOf("[++ENDPRIZE++]");
                dynMsglength = dynEndTagIndex + 14 - dynBeginTagIndex;
                if (!(dynBeginTagIndex < 0 || dynEndTagIndex < 0 || (dynEndTagIndex - dynBeginTagIndex) < 0))
                    textMessage.Replace(ko.TextMessage.Substring(dynBeginTagIndex, dynMsglength), string.Empty);
            }

            // Apply partner profit rate
            if (partner.ProfitPercentage != Constants.DEFAULT_PROFIT_PERCENTAGE_USA_RATE)
            {
                var profit_percent = partner.ProfitDescription;
                var eventProfitPercentage = GetEventProfitPercentage(eventId);
                if (eventProfitPercentage > partner.ProfitPercentage)
                    profit_percent = string.Concat(eventProfitPercentage.ToString(), "%");
                htmlMessage.Replace(Constants.DEFAULT_PROFIT_PERCENTAGE_USA + "%", profit_percent);
                htmlMessage.Replace(Constants.DEFAULT_PROFIT_PERCENTAGE_USA + " %", profit_percent.Insert(profit_percent.IndexOf("%"), " "));
                textMessage.Replace(Constants.DEFAULT_PROFIT_PERCENTAGE_USA + "%", profit_percent);
                textMessage.Replace(Constants.DEFAULT_PROFIT_PERCENTAGE_USA + " %", profit_percent.Insert(profit_percent.IndexOf("%"), " "));
            }

            //Remove *
            htmlMessage.Replace("*", "");
            textMessage.Replace("*", "");

            ko.Message = htmlMessage.ToString();
            ko.TextMessage = textMessage.ToString();
        }

        public static void ParseEmailForTouch(int eventId, EventTypeInfo eventType, string userTypeFrom, string userTypeTo, EmailTemplateBase ko, string customMessage, int sponsorEventParticipationId)
        {
            var eventRedirect = GetEventRedirectById(eventId) ?? " ";
            var eventName = GetEventNameById(eventId);

            string image_url = string.Empty;
            ko.Message = ko.Message.Replace("[++custom_message++]", customMessage);
            switch (userTypeFrom.ToLower())
            {
                case "sponsor":
                    var spImagePath = GetCoverAlbumImageUrlById(eventId);
                    if (spImagePath.IsEmpty())
                        spImagePath = string.Format("{0}/{1}",
                                            "/Content/images/personalization",
                                            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);

                    if (!string.IsNullOrEmpty(eventRedirect) && eventRedirect.Trim().Length > 0)
                    {
                        ko.Message = ko.Message.Replace(eventRedirect, "[++redirect++]");
                        ko.TextMessage = ko.TextMessage.Replace(eventRedirect, "[++redirect++]");
                    }

                    ko.Message = ko.Message.Replace(spImagePath, "[++image_path++]");
                    if (userTypeTo.ToLower() == "participant" && eventType != EventTypeInfo.INDIVIDUAL_FUNDRAISER)
                    {
                        ko.Subject = ko.Subject.Replace("Member Name", "[++participant++]").Replace("Member First Name", "[++participant_first_name++]").Replace("Member Last Name", "[++participant_last_name++]");
                        ko.Message = ko.Message.Replace("Member Name", "[++participant++]").Replace("Member First Name", "[++participant_first_name++]").Replace("Member Last Name", "[++participant_last_name++]");
                        ko.TextMessage = ko.TextMessage.Replace("Member Name", "[++participant++]").Replace("Member First Name", "[++participant_first_name++]").Replace("Member Last Name", "[++participant_last_name++]");
                    }
                    else
                    {
                        ko.Message = ko.Message.Replace("Member Name", "[++supporter++]");
                        ko.TextMessage = ko.TextMessage.Replace("Member Name", "[++supporter++]");
                    }
                    break;
                case "participant":
                case "supporter":
                    var participantRedirect = GetParticipantRedirectById(sponsorEventParticipationId);
                    var parImagePath = GetParticipantImageUrlById(sponsorEventParticipationId);
                    if (parImagePath.IsEmpty())
                        parImagePath = string.Format("{0}/{1}",
                                            "/Content/images/personalization",
                                            Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);

                    if (participantRedirect != null && participantRedirect.Trim().Length > 0)
                    {
                        ko.Message = ko.Message.Replace(string.Concat(eventRedirect.Trim(), "/", participantRedirect), "[++redirect++]");
                        ko.TextMessage = ko.TextMessage.Replace(string.Concat(eventRedirect.Trim(), "/", participantRedirect), "[++redirect++]");
                    }
                    else
                    {
                        ko.Message = ko.Message.Replace(eventRedirect, "[++redirect++]");
                        ko.TextMessage = ko.TextMessage.Replace(eventRedirect, "[++redirect++]");
                    }
                    ko.Message = ko.Message.Replace(parImagePath, "[++image_path++]");
                    ko.Message = ko.Message.Replace("Member Name", "[++supporter++]");
                    ko.TextMessage = ko.TextMessage.Replace("Member Name", "[++supporter++]");
                    break;
            }
        }
        #endregion

        #region Private Static Methods

        private static string GetEventNameById(int eventId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @event = dataProvider.events.Single(
                    p => p.event_id == eventId);
                return @event.event_name;
            }
        }
        private static string GetEventRedirectById(int eventId)
        {
            using (var dataProvider = new DataProvider())
            {
                var sp = GetSponsorPersonalizationById(eventId);
                return sp != null ? sp.redirect : "Group?e=" + eventId;
            }
        }
        private static string GetParticipantRedirectById(int eventParticipationId)
        {
            using (var dataProvider = new DataProvider())
            {
                var participantPerso = GetPersonalizationById(eventParticipationId);
                if (participantPerso != null && !string.IsNullOrEmpty(participantPerso.redirect))
                    return participantPerso.redirect;
                else
                    return null;
            }
        }
        private static string GetCoverAlbumImageUrlById(int eventId)
        {
            var persoImgs = GetSponsorImagesById(eventId);
            return persoImgs != null && persoImgs.Any(p => (bool)p.isCoverAlbum == true)
                                        ? persoImgs.First(p => (bool)p.isCoverAlbum == true).image_url
                                        : persoImgs != null && persoImgs.Any()
                                          ? persoImgs.First().image_url
                                          : string.Empty;
        }
        private static string GetParticipantImageUrlById(int eventParticipationId)
        {
            var persoImgs = GetParticipantImagesById(eventParticipationId);
            return persoImgs != null && persoImgs.Any() ? persoImgs.OrderByDescending(p => p.isCoverAlbum).First().image_url : string.Empty;
        }
        private static List<personalization_image> GetSponsorImagesById(int eventId)
        {
            var sponsorPerso = GetSponsorPersonalizationById(eventId);
            if (sponsorPerso != null)
                return GetPersonalizationImagesById(sponsorPerso.personalization_id);
            else
                return null;
        }
        private static List<personalization_image> GetParticipantImagesById(int eventParticipationId)
        {
            var participantPerso = GetPersonalizationById(eventParticipationId);
            if (participantPerso != null)
                return GetPersonalizationImagesById(participantPerso.personalization_id);
            else
                return null;
        }
        private static personalization GetSponsorPersonalizationById(int eventId)
        {
            using (var dataProvider = new DataProvider())
            {
                var sponsorPersonalization = (from p in dataProvider.personalizations
                                              from ep in dataProvider.event_participation
                                              where p.event_participation_id == ep.event_participation_id
                                                 && ep.event_id == eventId && ep.participation_channel_id == 3
                                              select p).SingleOrDefault();
                return sponsorPersonalization;
            }
        }
        private static personalization GetPersonalizationById(int eventParticipationId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @personalization = (from p in dataProvider.personalizations
                                        from ep in dataProvider.event_participation
                                        where p.event_participation_id == ep.event_participation_id
                                           && ep.event_participation_id == eventParticipationId
                                        select p).SingleOrDefault();
                return @personalization;
            }
        }
        private static List<personalization_image> GetPersonalizationImagesById(int personalizationId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @persoImages = (from p in dataProvider.personalizations
                                    where p.personalization_id == personalizationId
                                    select p.personalization_image).SingleOrDefault();
                if (@persoImages != null && @persoImages.Any())
                {
                    ValidatePersonalizationImage(@persoImages.Where(p => !p.deleted && p.image_approval_status_id != 4).ToList());
                    return @persoImages.Where(p => !p.deleted && p.image_approval_status_id != 4).ToList();
                }
                else
                    return null;
            }
        }
        private static void ValidatePersonalizationImage(List<personalization_image> persoImages)
        {
            if (!persoImages.Any())
                return;
            var goodImg = persoImages.FirstOrDefault(p => p.isCoverAlbum.HasValue ? p.isCoverAlbum.Value == true : false);
            if (goodImg == null)
                goodImg = persoImages.First();
            var isGoodImgCoverAlbum = goodImg.isCoverAlbum.HasValue ? goodImg.isCoverAlbum.Value : false;
            using (var dataProvider = new DataProvider())
            {
                foreach (var img in persoImages)
                {
                    if (goodImg.image_id == img.image_id)
                        continue;
                    var image = dataProvider.personalization_image.Single(p => p.image_id == img.image_id);
                    var isImgCoverAlbum = img.isCoverAlbum.HasValue ? img.isCoverAlbum.Value : false;
                    if (isGoodImgCoverAlbum && isImgCoverAlbum)
                    {
                        img.isCoverAlbum = false;
                        image.isCoverAlbum = false;
                    }
                    if (goodImg.image_url == img.image_url)
                    {
                        img.deleted = true;
                        image.deleted = true;
                    }
                }
                dataProvider.SaveChanges();
            }
        }
        private static List<tag> GetTags()
        {
            using (var dataProvider = new DataProvider())
            {
                return (from t in dataProvider.tags
                        select t).ToList();
            }
        }
        private static List<email_template_tag> GetEmailTemplateTagsById(int templateId, int productOfferId)
        {
            using (var dataProvider = new DataProvider())
            {
                return (from et in dataProvider.email_template_tag
                        where et.email_template_id == templateId
                           && et.product_offer_id == productOfferId
                        select et).ToList();
            }
        }
        private static double GetEventProfitPercentage(int eventId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @event = dataProvider.events.Single(
                    p => p.event_id == eventId);
                return @event.profit_calculated;
            }
        }
        private static User GetUserFromTouchId(int touchId)
        {
            using (var dataProvider = new DataProvider())
            {
                var @user = (from t in dataProvider.touches
                             from ep in dataProvider.event_participation
                             from mh in dataProvider.member_hierarchy
                             from m in dataProvider.members
                             where t.touch_id == touchId
                                && t.event_participation_id == ep.event_participation_id
                                && mh.member_hierarchy_id == ep.member_hierarchy_id
                                && m.member_id == mh.member_id
                             select new User
                             {
                                 Id = m.member_id,
                                 FirstName = m.first_name,
                                 LastName = m.last_name,
                                 Email = m.email_address,
                                 Password = m.password,
                                 EventParticipationId = ep.event_participation_id
                             }).Single();
                return @user;
            }
        }
        private static string ReplaceString(string text, string firstTag, string secondTag, string replaceWith)
        {
            // check if the tag exists
            bool tagFound = (text.IndexOf(firstTag) > -1);

            // loop until the tag is not found in text
            while (tagFound)
            {
                // retreive the first index of the first tag
                int start = text.IndexOf(firstTag);

                // retreive the last index of the second tag (+ second tag length)
                int end = text.IndexOf(secondTag, start);

                // get the values between these two tags (including the tags itself)
                string foundValue = text.Substring(start, (end + secondTag.Length) - start);

                // replace the found value from the text
                text = text.Replace(foundValue, replaceWith);

                // check again if the first tag exists to manage the while loop
                tagFound = (text.IndexOf(firstTag) > -1);
            }

            // return the new text without the inner tags
            return text;
        }

        #endregion
    }

    public static class ApplicationExtension
    {
        #region Public Extension Methods

        public static bool IsEmpty(this string text)
        {
            return (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text));
        }

        public static bool IsNotEmpty(this string text)
        {
            return !text.IsEmpty();
        }

        public static string TrimStart(this string target, string trimChars)
        {
            return target.TrimStart(trimChars.ToCharArray());
        }

        public static string ReturnAsDollarAmount(this decimal decimalValue)
        {
            return decimalValue.ToString("$#,##0.00");
        }

        public static string ReturnAsDollarAmountNoDollarSign(this decimal decimalValue)
        {
            return decimalValue.ToString("#,##0.00");
        }

        public static string ReturnAsDollarAmount(this double doubleValue)
        {
            return doubleValue.ToString("$#,##0.00");
        }

        public static string ReturnAsDollarAmountNoDollarSign(this double doubleValue)
        {
            return doubleValue.ToString("#,##0.00");
        }

        public static string ReturnAsDollarAmount(this int intValue)
        {
            return intValue.ToString("$#,##0.00");
        }

        public static string ReturnAsDollarAmountNoDollarSign(this int intValue)
        {
            return intValue.ToString("#,##0.00");
        }

        public static string ReturnAsAmountWithComma(this int intValue)
        {
            return intValue.ToString("#,##0");
        }

        public static string DeSpace(this string toClean)
        {
            return toClean.Replace(" ", "");
        }

        public static string FirstLetterUpper(this string toFormat)
        {
            if (toFormat.IsEmpty())
            {
                return toFormat;
            }

            var words = toFormat.ToLower().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var processedList = words.Select(word => word.Length == 1 ? word.ToUpper() : string.Format("{0}{1}", word[0].ToString(CultureInfo.InvariantCulture).ToUpper(), word.Substring(1))).ToList();

            return string.Join(" ", processedList);
        }

        public static string FirstLetterLower(this string toFormat)
        {
            if (toFormat.IsEmpty())
            {
                return toFormat;
            }

            var words = toFormat.ToLower().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var processedList = words.Select(word => word.Length == 1 ? word.ToLower() : string.Format("{0}{1}", word[0].ToString(CultureInfo.InvariantCulture).ToLower(), word.Substring(1))).ToList();

            return string.Join(" ", processedList);
        }

        public static string HtmlEncodeDecode(this string toProcess)
        {
            return toProcess.IsEmpty()
                       ? toProcess
                       : HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(toProcess));
        }

        public static string UrlEncode(this string toProcess)
        {
            return toProcess.IsEmpty()
                       ? toProcess
                       : HttpUtility.UrlEncode(toProcess);
        }

        public static string UrlDecode(this string toProcess)
        {
            return toProcess.IsEmpty()
                       ? toProcess
                       : HttpUtility.UrlDecode(toProcess);
        }

        public static string CleanupRedirect(this string redirect)
        {
            if (redirect.IsEmpty())
                return redirect;
            return Regex.Replace(redirect.Replace(" ", ""), @"[^0-9a-zA-Z]+", "");
        }

        public static string CleanupContactEntry(this string entry)
        {
            if (entry.IsEmpty())
                return entry;
            return entry.Replace(",", "").ReplaceSingleQuoteToAlternativeVersion();
        }

        public static string StripHtml(this string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        public static string StripBR(this string text)
        {
            return text.Replace("<br/>", string.Empty).Replace("<br />", string.Empty).Replace("<br>", string.Empty);
        }

        public static string StripNewLine(this string text)
        {
            return text.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        public static string EncodeDoubleQuotes(this string text)
        {
            return text.Replace("\"", "&quote;");
        }

        public static string ReplaceSingleQuoteToAlternativeVersion(this string text)
        {
            if (text.IsEmpty())
                return text;
            return text.Replace("'", "’");
        }

        public static string TransformQuotesForUI(this string text)
        {
            if (text.IsEmpty())
                return text;
            return text.ReplaceSingleQuoteToAlternativeVersion();
        }

        public static string ReplaceNewLineToBR(this string text)
        {
            if (text.IsEmpty())
                return text;
            return text.Replace("\r\n", "<br />").Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        public static string FormatGUID(this string guid)
        {
            return guid.Replace("{", "").Replace("}", "").Replace("-", "").ToLower();
        }

        #endregion
    }

    public static class Constants
    {
        /// <summary>
        /// Default USA Profit setting
        /// </summary>
        public const string DEFAULT_PROFIT_PERCENTAGE_USA = "40";
        public const double DEFAULT_PROFIT_PERCENTAGE_USA_RATE = 40D;
        /// <summary>
        /// Default CANADA Profit setting
        /// </summary>
        public const string DEFAULT_PROFIT_PERCENTAGE_CAN = "37";
        public const double DEFAULT_PROFIT_PERCENTAGE_CAN_RATE = 37D;
        /// <summary>
        /// Session key for the partner Id
        /// </summary>
        public const string SESSION_KEY_PARTNER_ID = "partner_id";
        /// <summary>
        /// Session key for the event Id
        /// </summary>
        public const string SESSION_KEY_EVENT_ID = "event_id";
        /// <summary>
        /// Session key for the event participation Id
        /// </summary>
        public const string SESSION_KEY_EVENT_PARTICIPATION_ID = "event_participation_id";
        /// <summary>
        /// Session key for the participant home Id
        /// </summary>
        public const string SESSION_KEY_PARTICIPANT_HOME_ID = "ph_id";
        /// <summary>
        /// Session key for the touch Id
        /// </summary>
        public const string SESSION_KEY_TOUCH_ID = "touch_id";
        /// <summary>
        /// Session key for the lead Id
        /// </summary>
        public const string SESSION_KEY_LEAD_ID = "lead_id";
        /// <summary>
        /// Session key for the promotion Id
        /// </summary>
        public const string SESSION_KEY_PROMOTION_ID = "promotion_id";
        /// <summary>
        /// Session key for the promotion Id
        /// </summary>
        public const string SESSION_KEY_FCEXTERNAL_ID = "fc_external_id";
        /// <summary>
        /// Session key for the participant home Id
        /// </summary>
        public const string SESSION_KEY_PERSONALIZATION_ID = "personalization_id";
        /// <summary>
        /// Session key for the creation channel Id
        /// </summary>
        public const string SESSION_KEY_CREATION_CHANNEL_ID = "creation_channel_id";
        /// <summary>
        /// Session key for the GA external supporter Id
        /// </summary>
        public const string SESSION_KEY_GA_EXTERNAL_SUPPORTER_ID = "ga_external_supporter_id";
        /// <summary>
        /// Session key for disabling header section
        /// </summary>
        public const string SESSION_KEY_DISABLE_HEADER = "disable_header";
        /// <summary>
        /// Session key for disabling footer section
        /// </summary>
        public const string SESSION_KEY_DISABLE_FOOTER = "disable_footer";
        /// <summary>
        /// Session key for user inputted password
        /// </summary>
        public const string SESSION_KEY_PASSWORD = "password";
        /// <summary>
        /// Query parameter key for the partner Id as integer
        /// </summary>
        public const string QUERY_PARAMETER_P = "p";
        /// <summary>
        /// Query parameter key for the partner Id as Guid
        /// </summary>
        public const string QUERY_PARAMETER_GID = "gid";
        /// <summary>
        /// Query parameter key for the event Id
        /// </summary>
        public const string QUERY_PARAMETER_EVENT_ID = "eventId";
        /// <summary>
        /// Query parameter key for the event Id
        /// </summary>
        public const string QUERY_PARAMETER_EVENT_ID_2 = "e";
        /// <summary>
        /// Query parameter key for the participant Id
        /// </summary>
        public const string QUERY_PARAMETER_PARTICIPANT_ID = "participantId";
        /// <summary>
        /// Query parameter key for the event participationId
        /// </summary>
        public const string QUERY_PARAMETER_EVENT_PARTICIPATION_ID = "ep";
        /// <summary>
        /// Query parameter key for the participant home Id
        /// </summary>
        public const string QUERY_PARAMETER_PARTICIPANT_HOME_ID = "ph";
        /// <summary>
        /// Query parameter key for the touchId
        /// </summary>
        public const string QUERY_PARAMETER_TOUCH_ID = "touch_id";
        /// <summary>
        /// Query parameter key for the touchId
        /// </summary>
        public const string QUERY_PARAMETER_TOUCH_ID_2 = "t";
        /// <summary>
        /// Query parameter key for the lead Id as integer
        /// </summary>
        public const string QUERY_PARAMETER_LEADID = "lID";
        /// <summary>
        /// Query parameter key for the promotion Id as integer
        /// </summary>
        public const string QUERY_PARAMETER_PROMOTIONID = "pr_id";
        /// <summary>
        /// Query parameter key for the personalization Id
        /// </summary>
        public const string QUERY_PARAMETER_PERSONALIZATION_ID = "pers";
        /// <summary>
        /// Query parameter key for the FC External Id as integer
        /// </summary>
        public const string QUERY_PARAMETER_FCEXTERNALID = "FCExtID";
        /// <summary>
        /// Query parameter key for the creation channel id override
        /// </summary>
        public const string QUERY_PARAMETER_CREATIONCHANNEL = "cc";
        /// <summary>
        /// Query parameter key for the order id (Donation)
        /// </summary>
        public const string QUERY_PARAMETER_ORDER_ID = "orId";
        /// <summary>
        /// Query parameter key for external organization id
        /// </summary>
        public const string QUERY_PARAMETER_EXTERNAL_ORGANIZATION_ID = "oID";
        /// <summary>
        /// Query parameter key for external group id
        /// </summary>
        public const string QUERY_PARAMETER_EXTERNAL_GROUP_ID = "grID";
        /// <summary>
        /// Query parameter key for Group name
        /// </summary>
        public const string QUERY_PARAMETER_GROUP_NAME = "gName";
        /// <summary>
        /// Query parameter key for organizer name
        /// </summary>
        public const string QUERY_PARAMETER_ORGANIZER_NAME = "oName";
        /// <summary>
        /// Query parameter key for organizer email
        /// </summary>
        public const string QUERY_PARAMETER_ORGANIZER_EMAIL = "oEmail";
        /// <summary>
        /// Query parameter key for GA external supporter id
        /// </summary>
        public const string QUERY_PARAMETER_GA_EXTERNAL_SUPPORTER_ID = "ext_supp";
        /// <summary>
        /// Query parameter key for AutoCreation Redirect URL
        /// </summary>
        public const string QUERY_PARAMETER_REDIRECT_URL = "rURL";
        /// <summary>
        /// Query parameter key for Header section
        /// </summary>
        public const string QUERY_PARAMETER_HEADER = "head";
        /// <summary>
        /// Query parameter key for Footer section
        /// </summary>
        public const string QUERY_PARAMETER_FOOTER = "foot";
        /// <summary>
        /// Custom attribute value to show only the partner logo
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_SHOW_ONLY_PARTNER_LOGO = "esubs_show_only_partner_logo";
        /// <summary>
        /// Custom attribute value to hide the main menu at the top of the page
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_HIDE_MAIN_MENU = "esubs_hide_main_menu";
        /// <summary>
        /// Custom attribute value to show the popular items at the group pages
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_GROUP_PAGE_SHOW_POPULAR_ITEMS = "esubs_group_page_show_popular_items";
        /// <summary>
        /// Custom attribute value to get image partner path
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_PARTNER_PATH = "partner_path";
        /// <summary>
        /// PAP Affiliate id
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_PAP_A_AID = "pap_a_aid";
        /// <summary>
        /// Custom attribute value to insert leads for the partner
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_INSERT_AS_LEAD = "esubs_insert_as_lead";
        /// <summary>
        /// Custom attribute value to allow prize program
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_PRIZE_PROGRAM = "esubs_prize_program";
        /// <summary>
        /// Custom attribute value to hide profit
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_HIDE_PROFIT = "esubs_hide_profit";
        /// <summary>
        /// Custom attribute value to hide the goal
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_HIDE_GOAL = "esubs_hide_goal";
        /// <summary>
        /// Custom attribute value to hide the how it works links
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_HIDE_HOWITWORKS = "esubs_hide_howitworks";
        /// <summary>
        /// Custom attribute value to hide the browse participants link in group page
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_HIDE_BROWSE_PARTICIPANTS = "esubs_hide_browse_participants";
        /// <summary>
        /// Custom attribute value to customize the Shop for out cause message
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_CUSTOMIZE_SHOP_NOW_FOR_OUR_CAUSE = "esubs_customize_shopnowforourcause";
        /// <summary>
        /// Custom attribute value to customize the participant register message
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_CUSTOMIZE_PARTICIPANT_REGISTER_MESSAGE = "esubs_customize_participant_register_message";
        /// <summary>
        /// Custom attribute value to hide the link to the FAQ page
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_GROUP_PAGE_HIDE_FAQ = "esubs_hide_faq";
        /// <summary>
        /// Custom attribute value to customize to Hide all the Reports
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_GROUP_PAGE_HIDE_ALL_REPORTS = "esubs_hide_all_reports";
        /// <summary>
        /// Custom attribute value to customize to Hide the Promote your Page menu
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_HIDE_PROMOTION_PAGE = "esubs_hide_promotion";
        /// <summary>
        /// Custom attribute value to hide check report
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_HIDE_CHECK_REPORT = "esubs_hide_check_report";
        /// <summary>
        /// Custom attribute value to redirect to landing page
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_REDIRECT_TO_LANDING_PAGE = "esubs_redirect_to_landing_page";
        /// <summary>
        /// Custom attribute value to redirect to landing page
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_SHOW_RECEIPT_LINK = "esubs_show_receipt_link";
        /// <summary>
        /// Custom attribute value to hide the JOIN button per events
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_HIDE_JOIN_BUTTON_BY_EVENTS = "esubs_hide_join_button_by_events";
        /// <summary>
        /// Custom attribute value to show total amount in Amount Raised Thermometer (as opposed to Total Profit)
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_SHOW_TOTAL_AMOUNT_IN_THERMOMETER = "esubs_show_total_amount_in_thermometer";
        /// <summary>
        /// Custom attribute value for esubs url
        /// </summary>
        public const string CUSTOM_ATTRIBUTE_ESUBS_URL = "esubs_url";
        /// <summary>
        /// Default group photo placeholder filename
        /// </summary>
        public const string GROUP_PHOTO_PLACEHOLDER_FILENAME = "groupphotoplaceholder.gif";
        /// <summary>
        /// Participant Registration page name
        /// </summary>
        public static string PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME = "participant_default.gif";
        /// <summary>
        /// Legacy Image Path StartsWith
        /// </summary>
        public const string LEGACY_IMAGE_PATH_STARTSWITH = "Resources/Images";
        /// <summary>
        /// Legacy Image Path sponsor
        /// </summary>
        public const string LEGACY_IMAGE_PATH_SPONSOR = "sponsor";
        /// <summary>
        /// Legacy Image Path efund classic
        /// </summary>
        public const string LEGACY_IMAGE_PATH_EFUNDCLASSIC = "_efund_/_classic_";
        /// <summary>
        /// Legacy Image Path personalized big_photos
        /// </summary>
        public const string LEGACY_IMAGE_PATH_PERSONALIZED_BIG_PHOTOS = "Personalized/big_photos";
        /// <summary>
        /// Legacy Image Path personalized folders
        /// </summary>
        public const string LEGACY_IMAGE_PATH_PERSONALIZED_FOLDERS = "sports|school|community|general";
        /// <summary>
        /// Legacy Image Path personalized folders
        /// </summary>
        public const string LEGACY_IMAGE_PARTNER_PERSONALIZATION = "Personalized/PartnerPersonalization";
        /// <summary>
        /// Legacy Domain Host
        /// </summary>
        public const string LEGACY_DOMAIN_HOST = "my.fundraising.com";
        /// <summary>
        /// New Domain Host
        /// </summary>
        public const string NEW_DOMAIN_HOST = "efundraising.com";
        /// <summary>
        /// Redirect to store page name
        /// </summary>
        public const string STORE_REDIRECT_PAGE = "RedirectToStore";
        /// <summary>
        /// Participant Registration page name
        /// </summary>
        public const string PARTICIPANT_REGISTER_PAGE = "ParticipantRegister";
        /// <summary>
        /// Default Group Size
        /// </summary>
        public const string DEFAULT_GROUP_SIZE = "15";
        /// <summary>
        /// USA Culture Code
        /// </summary>
        public const string USA_CULTURE_CODE = "en-US";
        /// <summary>
        /// USA Country Code
        /// </summary>
        public const string USA_COUNTRY_CODE = "US";
        /// <summary>
        /// CANADA Culture Code
        /// </summary>
        public const string CANADA_CULTURE_CODE = "en-CA";
        /// <summary>
        /// CANADA Country Code
        /// </summary>
        public const string CANADA_COUNTRY_CODE = "CA";
        /// <summary>
        /// Default USA Subdivision Code
        /// </summary>
        public const string DEFAULT_US_SUBDIVISIONCODE = "US-CA";
        /// <summary>
        /// Default CANADA Subdivision Code
        /// </summary>
        public const string DEFAULT_CA_SUBDIVISIONCODE = "CA-AB";
    }
    #endregion
}
