using SWCorporate.SystemEx.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace GA.BDC.Console.TaskExecutor.Tasks
{

    internal sealed class SendFundPassEmailTask : ITask<TaskFlags>
    {
 
        void ITask<TaskFlags>.Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            var exceptions = new List<Exception>();

            var className = this.GetType().Name;
            Trace.TraceInformation("Start: " + className);
        //    try
        //    {
        //        var notification = new Notification();
        //        var type = GA.BDC.Shared.Entities.NotificationType.SendFundPassPromoCode;

        //        //Get Response
        //        XMLHTTP30 http = new XMLHTTP30();
               
        //        http.open("GET", ConfigurationManager.AppSettings["fundraising.webapi.host.FundPass"], false, null, null);
        //        http.send(null);
        //        var value = http.responseText;
        //        var joResponse = JArray.Parse(value);

        //        foreach (JObject item in joResponse)
        //        {
        //            var ExternalId = item.GetValue("LeadIdUsed");

        //            //calling webapi SendFundPassPromoCode with the notification object.
        //            //ServerXMLHTTP emailProcessing = new ServerXMLHTTP();
        //            XMLHTTP30 emailProcessing = new XMLHTTP30();
        //            var strJSONToSend2 = "{ \"Type\":\"" + 19 +
        //                      "\",\"ExternalId\":\"" + ExternalId +
        //                           "\"}";

        //            //var serverNotificationUrl = ConfigurationManager.AppSettings["fundraising.webapi.host.Notification"];
        //            var postDataBytes2 = Encoding.Default.GetBytes(strJSONToSend2);

        //            emailProcessing.open("POST", ConfigurationManager.AppSettings["fundraising.webapi.host.Notification"], true);
        //            emailProcessing.setRequestHeader("Content-Type", "application/json");
        //            emailProcessing.setRequestHeader("Content-Length", postDataBytes2.Length.ToString());
        //            emailProcessing.send(postDataBytes2);
        //            //var text = emailProcessing.responseText + " -- " + emailProcessing.status;


        //        }

               
        //        Trace.TraceInformation("End: " + className);

        //    }
        //    catch (Exception exception)
        //    {
        //        Trace.TraceError(exception.Message);
        //        InstrumentationProvider.SendExceptionNotification(exception, null);
        //        exceptions.Add(exception);
        //    }
        //    if (exceptions.Any())
        //    {
        //        throw new AggregateException(exceptions);
               
        //    }
       }

    }
}
