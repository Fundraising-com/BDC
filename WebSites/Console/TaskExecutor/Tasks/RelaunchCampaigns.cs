using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using SWCorporate.SystemEx.Console;
using DataProvider = GA.BDC.Data.MGP.EFRCommon.Models.DataProvider;

namespace GA.BDC.Console.TaskExecutor.Tasks
{
   internal sealed class RelaunchCampaigns : ITask<TaskFlags>
   {

      public void Execute(TaskFlags taskFlags, params string[] taskArgs)
      {
         var exceptions = new List<Exception>();
         try
         {
            //1. Read Configuration
            var partnerId = int.Parse(ConfigurationManager.AppSettings["RelaunchCampaigns.PartnerId"]);
            var @from = DateTime.Parse(ConfigurationManager.AppSettings["RelaunchCampaigns.From"]);

            var profitGroupId = 0;
            var profitCalculated = 0.0;
            //2. Retrieve Profits calculation
            using (var dataProvider = new DataProvider())
            {
               var profit = (from pp in dataProvider.partner_profit
                             from p in dataProvider.profits
                             where pp.profit_group_id == p.profit_group_id
                             && pp.partner_id == partnerId
                             select p).First();
               profitGroupId = (int)profit.profit_group_id;
               profitCalculated = (double)profit.profit_percentage;
            }
            //3. Retrieve Events with the given filter, closed all of them
            using (var dataProvider = new Data.MGP.esubs_global_v2.Models.DataProvider())
            {
               var repeatedEvents = (from g in dataProvider.groups
                                     from eg in dataProvider.event_group
                                     from e in dataProvider.events
                                     where g.group_id == eg.group_id
                                           && eg.event_id == e.event_id
                                           && g.partner_id == partnerId
                                           && e.create_date >= @from
                                     select e).ToList();
               var events = new List<_event>();
               var filteredEvents = new List<_event>();
               //4. Group Events by redirect and ignore the rest repeated
               foreach (var repeatedEvent in repeatedEvents)
               {
                  repeatedEvent.active = false;
                  repeatedEvent.end_date = DateTime.Now;
                  dataProvider.SaveChanges();

                  if (repeatedEvents.Any(p => p.event_id != repeatedEvent.event_id && p.redirect == repeatedEvent.redirect))
                  {
                     if (filteredEvents.All(p => p.event_id != repeatedEvent.event_id))
                     {
                        var eventsWithSameRedirect = repeatedEvents.Where(p => p.redirect == repeatedEvent.redirect).ToList();
                        events.Add(eventsWithSameRedirect.OrderByDescending(p => p.event_id).First());
                        filteredEvents.AddRange(eventsWithSameRedirect);
                     }

                  }
                  else
                  {
                     events.Add(repeatedEvent);
                  }
               }
               System.Console.WriteLine(string.Format("-{0} events found to be Relaunched.", events.Count));
               
               
               //5. Relaunch each event and Rejoin Participants again
               var iterator = events.Count;
               foreach (var @event in events)
               {
                  System.Console.WriteLine("--Event ID {0} and Redirect {1} to be Relaunched....", @event.event_id, @event.redirect);
                  var currentEventParticipation = dataProvider.event_participation.First(p => p.event_id == @event.event_id && p.participation_channel_id == 3);
                  var currentPersonalization = dataProvider.personalizations.First(p => p.event_participation_id == currentEventParticipation.event_participation_id);
                  var currentEventGroup = dataProvider.event_group.First(p => p.event_id == @event.event_id);
                  var currentPaymentInfo = dataProvider.payment_info.First(p => p.event_id == @event.event_id);
                  var currentPostalAddress = dataProvider.postal_address.FirstOrDefault(p => p.postal_address_id == currentPaymentInfo.postal_address_id);
                  var currentPhone = dataProvider.phone_number.FirstOrDefault(p => p.phone_number_id == currentPaymentInfo.phone_number_id);
                  var newEvent = new _event
                  {
                     create_date = DateTime.Now,
                     start_date = DateTime.Now,
                     redirect = @event.redirect,
                     active = true,
                     comments = "Campaign relaunched from the Campaign " + @event.event_id,
                     culture_code = "en-US",
                     date_of_event = DateTime.Now,
                     event_name = @event.event_name,
                     discount_site = @event.discount_site,
                     displayable = @event.displayable,
                     donation = @event.donation,
                     event_status_id = 3,
                     event_type_id = @event.event_type_id,
                     group_type_id = @event.group_type_id,
                     humeur_representative = @event.humeur_representative,
                     processing_fee = @event.processing_fee,
                     profit_calculated = profitCalculated,
                     profit_group_id = profitGroupId,
                     want_sales_rep_call = @event.want_sales_rep_call,
                     referral_application = 1
                  };
                  dataProvider.events.Add(newEvent);
                  dataProvider.SaveChanges();
                  var eventGroup = new event_group
                  {
                     create_date = DateTime.Now,
                     event_id = newEvent.event_id,
                     group_id = currentEventGroup.group_id
                  };
                  dataProvider.event_group.Add(eventGroup);
                  dataProvider.SaveChanges();
                  var eventParticipation = new event_participation
                  {
                     participation_channel_id = 3,
                     create_date = DateTime.Now,
                     event_id = newEvent.event_id,
                     member_hierarchy_id = currentEventParticipation.member_hierarchy_id,
                     agree_term_services = true,
                     holiday_reminders = false,
                     salutation = currentEventParticipation.salutation
                  };
                  dataProvider.event_participation.Add(eventParticipation);
                  dataProvider.SaveChanges();
                  var personalization = new personalization
                  {
                     create_date = DateTime.Now,
                     body = currentPersonalization.body,
                     displayGroupMessage = currentPersonalization.displayGroupMessage,
                     event_participation_id = eventParticipation.event_participation_id,
                     fundraising_goal = currentPersonalization.fundraising_goal,
                     group_url = currentPersonalization.group_url,
                     header_color = currentPersonalization.header_color,
                     header_bgcolor = currentPersonalization.header_bgcolor,
                     header_title1 = currentPersonalization.header_title1,
                     header_title2 = currentPersonalization.header_title2,
                     image_motivator = currentPersonalization.image_motivator,
                     image_url = currentPersonalization.image_url,
                     redirect = currentPersonalization.redirect,
                     remind_later = currentPersonalization.remind_later,
                     site_bgcolor = currentPersonalization.site_bgcolor,
                     skip = currentPersonalization.skip
                  };
                  dataProvider.personalizations.Add(personalization);
                  dataProvider.SaveChanges();
                  postal_address postalAddress = null;
                  if (currentPostalAddress != null)
                  {
                     postalAddress = new postal_address
                     {
                        address_1 = currentPostalAddress.address_1,
                        address_2 = currentPostalAddress.address_2,
                        city = currentPostalAddress.city,
                        country_code = currentPostalAddress.country_code,
                        create_date = DateTime.Now,
                        is_validated = currentPostalAddress.is_validated,
                        matching_code = currentPostalAddress.matching_code,
                        subdivision_code = currentPostalAddress.subdivision_code,
                        zip_code = currentPostalAddress.zip_code
                     };
                     dataProvider.postal_address.Add(postalAddress);
                     dataProvider.SaveChanges();
                  }
                  phone_number phoneNumber = null;
                  if (currentPhone != null)
                  {
                     phoneNumber = new phone_number
                     {
                        create_date = DateTime.Now,
                        phone_number1 = string.IsNullOrEmpty(currentPhone.phone_number1) ? "000000000" : currentPhone.phone_number1,
                     };
                     dataProvider.phone_number.Add(phoneNumber);
                     dataProvider.SaveChanges();
                  }
                  if (currentPaymentInfo != null)
                  {
                     var paymentInfo = new payment_info
                     {
                        postal_address_id = postalAddress != null ? postalAddress.postal_address_id : (int?)null,
                        phone_number_id = phoneNumber != null ? phoneNumber.phone_number_id : (int?)null,
                        active = true,
                        create_date = DateTime.Now,
                        event_id = newEvent.event_id,
                        group_id = eventGroup.group_id,
                        payment_name = currentPaymentInfo.payment_name,
                        on_behalf_of_name = currentPaymentInfo.on_behalf_of_name,
                        ship_to_name = currentPaymentInfo.ship_to_name,
                        ssn = currentPaymentInfo.ssn
                     };
                     dataProvider.payment_info.Add(paymentInfo);
                     dataProvider.SaveChanges();
                  }

                  //4. Rejoin Participants
                  var currentEventParticipationsWithoutSponsor = dataProvider.event_participation.Where(p => p.event_id == @event.event_id && p.participation_channel_id != 3).ToList();
                  foreach (var participation in currentEventParticipationsWithoutSponsor)
                  {
                     System.Console.WriteLine("--- Event Participation {0} to be joined.", participation.event_participation_id);
                     var currenParticipanttPersonalization = dataProvider.personalizations.FirstOrDefault(p => p.event_participation_id == participation.event_participation_id);

                     var newParticipation = new event_participation
                     {
                        participation_channel_id = 2,
                        create_date = DateTime.Now,
                        event_id = newEvent.event_id,
                        member_hierarchy_id = participation.member_hierarchy_id,
                        agree_term_services = true,
                        holiday_reminders = false,
                        salutation = participation.salutation
                     };
                     dataProvider.event_participation.Add(newParticipation);
                     dataProvider.SaveChanges();
                     if (currenParticipanttPersonalization != null)
                     {
                        var newParticipantPersonalization = new personalization
                        {
                           create_date = DateTime.Now,
                           event_participation_id = newParticipation.event_participation_id,
                           body = currenParticipanttPersonalization.body,
                           fundraising_goal = (decimal)currenParticipanttPersonalization.fundraising_goal,
                           redirect = currenParticipanttPersonalization.redirect,
                           header_title1 = currenParticipanttPersonalization.header_title1,
                           header_title2 = currenParticipanttPersonalization.header_title2,
                           displayGroupMessage = true,
                        };
                        dataProvider.personalizations.Add(newParticipantPersonalization);
                     }
                     else
                     {
                        var newParticipantPersonalization = new personalization
                        {
                           create_date = DateTime.Now,
                           event_participation_id = newParticipation.event_participation_id,
                           body = string.Empty,
                           fundraising_goal = (decimal)200,
                           redirect = FindSuggestedCampaignRedirect(participation.salutation.Replace(" ", "")),
                           header_title1 = string.Empty,
                           header_title2 = string.Empty,
                           displayGroupMessage = true,
                        };
                        dataProvider.personalizations.Add(newParticipantPersonalization);
                     }

                     dataProvider.SaveChanges();
                  }
                  System.Console.WriteLine("-- Event relaunch finished. {0} to go.", --iterator);
               }
               System.Console.WriteLine("- Task Finished");
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
      /// <summary>
      /// Finds the unique best campaign redirect
      /// </summary>
      /// <param name="campaignName"></param>
      /// <returns></returns>
      private string FindSuggestedCampaignRedirect(string campaignName)
      {
         var incremental = 1;

         using (var dataProvider = new Data.MGP.esubs_global_v2.Models.DataProvider())
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
   }
}
