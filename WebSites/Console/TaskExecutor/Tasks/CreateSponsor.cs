using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Data.MGP.EFRCommon.LINQ;
using Microsoft.VisualBasic.FileIO;
using SWCorporate.SystemEx.Console;

namespace GA.BDC.Console.TaskExecutor.Tasks
{
    internal sealed class CreateSponsor : ITask<TaskFlags>
    {
        public void Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            var exceptions = new List<Exception>();
            try
            {
                //1. Read Configuration
                var partnerId = int.Parse(ConfigurationManager.AppSettings["CreateSponsor.PartnerId"]);
                var creationChannelId = int.Parse(ConfigurationManager.AppSettings["CreateSponsor.CreationChannelId"]);
                var eventTypeId = int.Parse(ConfigurationManager.AppSettings["CreateSponsor.EventType"]);
                var filePath = ConfigurationManager.AppSettings["CreateSponsor.FilePath"];
                //2. Read Excel File
                var sponsors = LoadFile(filePath);
                //3. Iterate Sponsors
                var profitPercentage = 0.0;
                var profitGroupId = 0;
                using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["GA.BDC.Console.TaskExecutor.Properties.Settings.EFRCommon"].ConnectionString))
                {
                    var profits = dataProvider.es_get_partner_profit(partnerId).ToList();
                    if (profits.Any())
                    {
                        var profit = profits.First();
                        profitPercentage = profit.Percentage;
                        profitGroupId = profit.ProfitGroupId;
                    }
                }
                using (var dataProvider = new DataProvider())
                {
                    using (var transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 0, 10, 0)))
                    {
                        
                        foreach (var sponsor in sponsors)
                        {
                            var redirect = FindSuggestedCampaignRedirect(sponsor.FirstName + sponsor.LastName);
                            var user = new user
                            {
                                agree_term_services = true,
                                create_date = DateTime.Now,
                                culture_code = "en-US",
                                email_address = sponsor.Email,
                                first_name = sponsor.FirstName,
                                is_first_login = true,
                                last_name = sponsor.LastName,
                                opt_status_id = false,
                                partner_id = partnerId,
                                password = "usafootball",
                                unsubscribe = false,
                                username = sponsor.Email                                
                            };
                            dataProvider.users.Add(user);
                            dataProvider.SaveChanges();
                            var member = new member
                            {
                                create_date = DateTime.Now,
                                comments = "Member created automatically on Task Executor",
                                bounced = false,
                                culture_code = "en-US",
                                deleted = false,
                                email_address = sponsor.Email,
                                email_validated = false,
                                first_name = sponsor.FirstName,
                                last_name = sponsor.LastName,
                                partner_id = partnerId,
                                opt_status_id = 1,
                                unsubscribe = false,
                                user_id = user.user_id
                            };
                            dataProvider.members.Add(member);
                            dataProvider.SaveChanges();
                            var memberHierarchy = new member_hierarchy
                            {
                                active = true,
                                create_date = DateTime.Now,
                                unsubscribe = false,
                                member_id = member.member_id,
                                creation_channel_id = creationChannelId
                            };
                            dataProvider.member_hierarchy.Add(memberHierarchy);
                            dataProvider.SaveChanges();
                            var group = new @group
                            {
                                comments = "Group created automatically by Task Executor",
                                create_date = DateTime.Now,
                                partner_id = partnerId,
                                group_name = sponsor.FirstName + " " + sponsor.LastName,
                                test_group = false,
                                group_url = string.Empty,
                                sponsor_id = memberHierarchy.member_hierarchy_id,
                                redirect = redirect
                            };
                            dataProvider.groups.Add(group);
                            dataProvider.SaveChanges();

                            var @event = new _event
                            {
                                event_type_id = eventTypeId,
                                active = true,
                                comments = "Event created automatically by Task Executor",
                                create_date = DateTime.Now,
                                culture_code = "en-US",
                                event_name = sponsor.FirstName + " " + sponsor.LastName,
                                start_date = DateTime.Now,
                                redirect = redirect,
                                displayable = true,
                                group_type_id = 1,
                                profit_calculated = profitPercentage,
                                event_status_id = 1,
                                profit_group_id = profitGroupId,
                                donation = false,
                                referral_application = 1
                            };
                            dataProvider.events.Add(@event);
                            dataProvider.SaveChanges();
                            var eventGroup = new event_group
                            {
                                create_date = DateTime.Now,
                                group_id = group.group_id,
                                event_id = @event.event_id
                            };
                            dataProvider.event_group.Add(eventGroup);
                            dataProvider.SaveChanges();
                            var groupPaymentInfoPhone = new phone_number
                            {
                                create_date = DateTime.Now,
                                phone_number1 = "1-877-275-8664",
                            };
                            dataProvider.phone_number.Add(groupPaymentInfoPhone);
                            dataProvider.SaveChanges();

                            var groupPostalAddress = new postal_address
                            {
                                address_1 = "156 Lawrence Paquette Ind'l Dr PMW#5",
                                city = "Champlain",
                                country_code = "US",
                                subdivision_code = "US-NY",
                                zip_code = "12919",
                                matching_code = "12919We20",
                                create_date = DateTime.Now,
                            };
                            dataProvider.postal_address.Add(groupPostalAddress);
                            dataProvider.SaveChanges();

                            var groupPaymentInfo = new payment_info
                            {
                                active = true,
                                create_date = DateTime.Now,
                                event_id = @event.event_id,
                                group_id = @group.group_id,
                                on_behalf_of_name = string.Concat(sponsor.FirstName, " ", sponsor.LastName),
                                payment_name = string.Concat(sponsor.FirstName, " ", sponsor.LastName),
                                phone_number_id = groupPaymentInfoPhone.phone_number_id,
                                postal_address_id = groupPostalAddress.postal_address_id,
                                ship_to_name = string.Empty,
                                ssn = string.Empty
                            };
                            dataProvider.payment_info.Add(groupPaymentInfo);
                            dataProvider.SaveChanges();
                            var eventParticipation = new event_participation
                            {
                                event_id = @event.event_id,
                                member_hierarchy_id = memberHierarchy.member_hierarchy_id,
                                participation_channel_id = 3,
                                create_date = DateTime.Now,
                                salutation = sponsor.FirstName + " " + sponsor.LastName,
                                agree_term_services = true,
                                holiday_reminders = false
                            };
                            dataProvider.event_participation.Add(eventParticipation);
                            dataProvider.SaveChanges();
                            var defaultPersonalization = dataProvider.default_personalizations.FirstOrDefault(p => p.PartnerId == partnerId && p.EventTypeId == @event.event_type_id && p.ParticipantTypeId == 1);
                            var personalization = new personalization
                            {
                                create_date = DateTime.Now,
                                event_participation_id = eventParticipation.event_participation_id,
                                body = defaultPersonalization != null ? defaultPersonalization.Body : string.Empty,
                                fundraising_goal = 500,
                                redirect = redirect,
                                header_title1 = defaultPersonalization != null ? defaultPersonalization.HeaderTitle1 : string.Empty,
                                header_title2 = defaultPersonalization != null ? defaultPersonalization.HeaderTitle2 : string.Empty,
                                displayGroupMessage = true,
                            };
                            dataProvider.personalizations.Add(personalization);
                            dataProvider.SaveChanges();
                            var personalizationImage = new personalization_image
                            {
                                personalization_id = personalization.personalization_id,
                                image_url = "/Content/images/personalization/usnft_gao.jpg",
                                deleted = false,
                                create_date = DateTime.Now,
                                isCoverAlbum = true,
                                image_approval_status_id = 3,
                            };
                            dataProvider.personalization_image.Add(personalizationImage);
                            dataProvider.SaveChanges();
                        }
                        transactionScope.Complete();
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
        }

        private static string FindSuggestedCampaignRedirect(string campaignName)
        {
            var incremental = 1;

            using (var dataProvider = new DataProvider())
            {
                var personalizations = dataProvider.personalizations.Select(p => p.redirect).ToList();
                if (personalizations.All(p => !String.Equals(p, campaignName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return campaignName;
                }
                while (true)
                {
                    var newName = string.Concat(campaignName, incremental);
                    var existingRedirects = personalizations.Any(p => String.Equals(p, newName, StringComparison.CurrentCultureIgnoreCase));
                    if (!existingRedirects)
                    {
                        return newName;
                    }
                    incremental++;
                }
            }
        }        

        private static IEnumerable<Sponsor> LoadFile(string filePath)
        {
            var result = new List<Sponsor>();

            using (var csvReader = new TextFieldParser(filePath))
            {
                csvReader.SetDelimiters(",");
                csvReader.HasFieldsEnclosedInQuotes = true;

                while (!csvReader.EndOfData)
                {
                    var fieldData = csvReader.ReadFields();
                    if (fieldData != null)
                    {
                        result.Add(new Sponsor { FirstName = fieldData[0], LastName = fieldData[1], Email = fieldData[2] });
                    }
                }
            }

            return result;
        }
    }

    class Sponsor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
