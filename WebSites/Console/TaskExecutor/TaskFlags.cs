using System;

namespace GA.BDC.Console.TaskExecutor
{
   [Flags]
   internal enum TaskFlags
   {
      /// <summary>
      ///   Equivalent to specifying no arguments
      /// </summary>
      NothingToDo = 0,

      SendOrder = 1,

      SendPayment = 2,

      HandleMessageQueues = 4,

      DispatchQueuedMail = 16,
      /// <summary>
      /// Loads a batch of emails into a specific MGP event
      /// </summary>
      BulkLoadMembers = 32,
      /// <summary>
      /// Sends the leads to public trac
      /// </summary>
      SendPubliTracLead = 64,
      /// <summary>
      /// Creates Sponsors for MGP
      /// </summary>
      CreateSponsors = 128,
      /// <summary>
      /// Relaunches the campaigns for the parameters given
      /// </summary>
      RelaunchCampaigns = 256,
      /// <summary>
      /// Send follow up notification for sale
      /// </summary>
      NotificationService = 512,
      /// <summary>
      /// Send a Notification when the Sale is Paid
      /// </summary>
      SendSalesPaidNotification = 1024,
      /// <summary>
      /// Send a EZFUND order idoc
      /// </summary>
      EZSendOrder = 2048,
      /// <summary>
      /// Send a EZFUND payment idoc
      /// </summary>
      EZSendPayment = 4096,
      /// <summary>
      /// Process Contacts Uploaded on MGP and creates Event Participation, Members and Touches
      /// </summary>
      TouchEmailProcessQueue = 8192,
        /// <summary>
        /// Send fund pass email to clients 
        /// </summary>
        SendFundPassEmailTask = 16384
    }
}
