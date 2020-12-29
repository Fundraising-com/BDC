using SWCorporate.SystemEx.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http.Headers;
using GA.BDC.Shared.Entities;
using Sale = GA.BDC.Shared.Entities.Sale;

namespace GA.BDC.Console.TaskExecutor.Tasks
{

   internal sealed class FollowUpNotificationService : ITask<TaskFlags>
   {

      void ITask<TaskFlags>.Execute(TaskFlags taskFlags, params string[] taskArgs)
      {
         var exceptions = new List<Exception>();

         var className = this.GetType().Name;
         Trace.TraceInformation("Start: " + className);
         try
         {
            List<Sale> salesResults;

            using (var getclient = new HttpClient())
            {
               //GET
               getclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["fundraising.webapi.host"]);
               getclient.DefaultRequestHeaders.Accept.Clear();
               getclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

               var getResponse = getclient.GetAsync("/sales?requiresAFollowUp=true").Result;
               salesResults = (getResponse.Content.ReadAsAsync<IEnumerable<Sale>>().Result).ToList();
            }

            foreach (var groupedSale in salesResults.GroupBy(p => p.ClientId))
            {
               // Build notification object. OrderFollowUp = 14, passing in the varialble client id in the sale.
               var notification = new Notification
               {
                  Type = GA.BDC.Shared.Entities.NotificationType.OrderFollowUp,
                  ExternalId = groupedSale.Key,
                  Email = "fake@fake.com",
                  ExtraData = null
               };

               //calling webapi SendSaleFollowUp with the notification object.
               using (var postClient = new HttpClient())
               {
                  postClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["fundraising.webapi.host"]);
                  postClient.DefaultRequestHeaders.Accept.Clear();
                  postClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                  var postResponse = postClient.PostAsJsonAsync("/Notifications", notification).Result;
                  if (!postResponse.IsSuccessStatusCode)
                  {
                     //should probably log the fail results of the call to web api so we have something to reference.
                     Trace.TraceInformation("Failed Post: " + Environment.NewLine +
                                            "StatusCode: " + postResponse.StatusCode + Environment.NewLine +
                                            "Reason: " + postResponse.ReasonPhrase + Environment.NewLine +
                                            "Request Message: " + postResponse.RequestMessage);
                  }
               }
            }
            Trace.TraceInformation("End: " + className);

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

   }
}