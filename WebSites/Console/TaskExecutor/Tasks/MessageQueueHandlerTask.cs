using System;
using System.Diagnostics;
using System.Globalization;
using SWCorporate.SystemEx.Console;
using SWCorporate.SystemEx.Messaging;
using System.Messaging;
using SWCorporate.SystemEx;

namespace GA.BDC.Console.TaskExecutor.Tasks
{
   internal class MessageQueueHandlerTask : ITask<TaskFlags>
   {
      #region explicit implementation of the SWCorporate.SystemEx.Console.ITask<TaskFlags> interface

      void ITask<TaskFlags>.Execute(TaskFlags taskFlags, params string[] taskArgs)
      {
         var className = this.GetType().Name;
         Trace.TraceInformation("Start: " + className);
         try
         {
            //// Enumerate message queues based on configuration settings...
            using (var queueConfiguredEnumeratorInstance = new MessageQueueConfiguredEnumerator())
            {
               // Iterate through the enumerated message queues...
               while (queueConfiguredEnumeratorInstance.MoveNext())
               {
                  var messageQueue = queueConfiguredEnumeratorInstance.Current;
                  Trace.TraceInformation("Queue: " + messageQueue.Label);
                  var batchReceiver = new MessageQueueBatchReceiver(messageQueue);
                  batchReceiver.ReceiveQueuedMessages();
               }
            }
         }
         catch (MessageQueueException msmqException)
         {
            var instrumentationMessage = string.Concat(msmqException.ErrorCode.ToString(CultureInfo.InvariantCulture), ": ", msmqException.MessageQueueErrorCode.ToString());
            Trace.TraceError(instrumentationMessage);
            InstrumentationProvider.SendExceptionNotification(new ApplicationException(instrumentationMessage, msmqException), null);
            throw;
         }
         catch (Exception exception)
         {
            Trace.TraceError(exception.Message);
            InstrumentationProvider.SendExceptionNotification(exception, null);
            throw;
         }
         finally
         {
            Trace.TraceInformation("End: " + className);
         }
      }

      #endregion
   }
}
