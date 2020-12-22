using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Text;
using GA.BDC.Web.MGP.Properties;
using GA.BDC.Web.MGP.Models.Views;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Data.MGP.esubs_global_v2.Models;

namespace GA.BDC.Web.MGP.Helpers.EmailTemplate
{
    public static class TagParsor
    {
        #region Public Static Methods

        public static void ParseEmail(int eventId, string userTypeFrom, Partner partner, User user, EmailTemplateBase ko, bool isTestEnvironment)
        {
            var eventRedirect = GetEventRedirectById(eventId) ?? " ";
            var eventName = GetEventNameById(eventId);
            switch (userTypeFrom.ToLower())
            {
                case "sponsor":
                    var spImagePath = GetCoverAlbumImageUrlById(eventId);
                    if (spImagePath.IsEmpty())
                        spImagePath = string.Format("{0}/{1}",
                                            Settings.Default.PersonalizationImageDirectory,
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
                                            Settings.Default.PersonalizationImageDirectory,
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
                        ko.Message = ko.Message.Replace("[++redirect++]", string.Concat(eventRedirect.Trim(),"/",participantRedirect));
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
                    break;
                default:
                    break;
            }

            /* Change the domain links:
             *  OLD: http://my.fundraising.com
             *  NEW: http://efundraising.com   
             */
            ko.Message = ko.Message.Replace(Constants.LEGACY_DOMAIN_HOST, 
                                            (isTestEnvironment ? "test." : string.Empty) + Constants.NEW_DOMAIN_HOST);
            ko.TextMessage = ko.TextMessage.Replace(Constants.LEGACY_DOMAIN_HOST,
                                                    (isTestEnvironment ? "test." : string.Empty) + Constants.NEW_DOMAIN_HOST);

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

        public static void ParseEmailForTouch(int eventId, EventTypeInfo eventType, string userTypeFrom, string userTypeTo, Partner partner, User user, EmailTemplateBase ko, string customMessage = "")
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
                                            Settings.Default.PersonalizationImageDirectory,
                                            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);

                    if (!string.IsNullOrEmpty(eventRedirect) && eventRedirect.Trim().Length > 0)
                    {
                        ko.Message = ko.Message.Replace(eventRedirect, "[++redirect++]");
                        ko.TextMessage = ko.TextMessage.Replace(eventRedirect, "[++redirect++]");
                    }
                    
                    ko.Message = ko.Message.Replace(spImagePath, "[++image_path++]");
                    ko.Message += "<br/><img src='http://www.efundraising.com/pixel?touchId=[++source_id++]' alt='pixel'/>";
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
                    var participantRedirect = GetParticipantRedirectById(user.EventParticipationId);
                    var parImagePath = GetParticipantImageUrlById(user.EventParticipationId);
                    if (parImagePath.IsEmpty())
                        parImagePath = string.Format("{0}/{1}",
                                            Settings.Default.PersonalizationImageDirectory,
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
                    ko.Message += "<br/><img src='http://www.efundraising.com/pixel?touchId=[++source_id++]' alt='pixel'/>";
                    ko.TextMessage = ko.TextMessage.Replace("Member Name", "[++supporter++]");
                    break;
            }
        }

        public static void ParseEmailForPreview(Models.Views.Email email, int eventId, int memberTypeId, Partner partner)
        {
            var eventRedirect = GetEventRedirectById(eventId) ?? " ";
            var eventName = GetEventNameById(eventId);
            var user = GetUserFromTouchId(email.Id);
            email.Body = email.Body.Replace("[++partner_name++]", partner.Name);            
            switch (memberTypeId)
            {
                case 1:
                    var spImagePath = GetCoverAlbumImageUrlById(eventId);
                    if (spImagePath.IsEmpty())
                        spImagePath = string.Format("{0}/{1}",
                                            Settings.Default.PersonalizationImageDirectory,
                                            Constants.GROUP_PHOTO_PLACEHOLDER_FILENAME);

                    email.Subject = email.Subject.Replace("[++sponsor_first_name++]", user.FirstName);
                    email.Subject = email.Subject.Replace("[++sponsor_last_name++]", user.LastName);
                    email.Subject = email.Subject.Replace("[++participant_first_name++]", "Member First Name");
                    email.Subject = email.Subject.Replace("[++participant_last_name++]", "Member Last Name");
                    email.Body = email.Body.Replace("[++sponsor_name++]", user.Email);
                    email.Body = email.Body.Replace("[++sponsor_email++]", user.Email);
                    email.Body = email.Body.Replace("[++Sponsor_Name++]", user.CompleteName);
                    email.Body = email.Body.Replace("[++Group_name++]", eventName);
                    email.Body = email.Body.Replace("[++Group_Name++]", eventName);
                    email.Body = email.Body.Replace("[++campaign++]", eventName);
                    email.Body = email.Body.Replace("[++redirect++]", eventRedirect);
                    email.Body = email.Body.Replace("[++group_name++]", eventName);
                    email.Body = email.Body.Replace("[++event_name++]", eventName);
                    email.Body = email.Body.Replace("[++participant++]", "Member Name");
                    email.Body = email.Body.Replace("[++participant_first_name++]", "Member First Name");
                    email.Body = email.Body.Replace("[++participant_last_name++]", "Member Last Name");
                    email.Body = email.Body.Replace("[++supporter++]", "Member Name");
                    email.Body = email.Body.Replace("[++image_path++]", spImagePath);
                    break;
                case 2:
                case 3:
                    var participantRedirect = GetParticipantRedirectById(user.EventParticipationId);
                    var parImagePath = GetParticipantImageUrlById(user.EventParticipationId);
                    if (parImagePath.IsEmpty())
                        parImagePath = string.Format("{0}/{1}",
                                            Settings.Default.PersonalizationImageDirectory,
                                            Constants.PARTICIPANT_PHOTO_PLACEHOLDER_FILENAME);

                    email.Subject = email.Subject.Replace("[++participant_first_name++]", user.FirstName);
                    email.Subject = email.Subject.Replace("[++participant_last_name++]", user.LastName);
                    email.Body = email.Body.Replace("[++sponsor_name++]", user.Email);
                    email.Body = email.Body.Replace("[++Sponsor_name++]", user.Email);
                    email.Body = email.Body.Replace("[++sponsor_email++]", user.Email);
                    email.Body = email.Body.Replace("[++participant_name++]", user.Email);
                    email.Body = email.Body.Replace("[++Participant_name++]", user.CompleteName);
                    email.Body = email.Body.Replace("[++participant_email++]", user.CompleteName);
                    email.Body = email.Body.Replace("[++participant++]", user.CompleteName);
                    email.Body = email.Body.Replace("[++participant_first_name++]", user.FirstName);
                    email.Body = email.Body.Replace("[++participant_last_name++]", user.LastName);
                    email.Body = email.Body.Replace("[++supporter++]", "Member Name");
                    email.Body = email.Body.Replace("[++Group_name++]", eventName);
                    email.Body = email.Body.Replace("[++campaign++]", eventName);
                    email.Body = email.Body.Replace("[++group_name++]", eventName);
                    email.Body = email.Body.Replace("[++event_name++]", eventName);
                    email.Body = email.Body.Replace("[++student_name++]", user.CompleteName);
                    email.Body = email.Body.Replace("[++parent_name++]", user.CompleteName);
                    email.Body = email.Body.Replace("[++image_path++]", parImagePath);
                    email.Body = email.Body.Replace("[++participant_password++]", user.Password);
                    if (participantRedirect != null && participantRedirect.Trim().Length > 0)
                    {
                        email.Body = email.Body.Replace("[++redirect++]", string.Concat(eventRedirect.Trim(), "/", participantRedirect));
                    }
                    else
                    {
                        email.Body = email.Body.Replace("[++redirect++]", eventRedirect);
                    }
                    break;
                default:
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
                if (participantPerso != null && participantPerso.redirect.IsNotEmpty())
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
}