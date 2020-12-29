using System;
using GA.BDC.Data.DataLayer;
using SWCorporate.AppServices.Shared.Contracts;
using SWCorporate.SystemEx.Data.Definitions.Entities;
namespace GA.BDC.Integration.MessageParsers
{
   // ReSharper disable once InconsistentNaming
   internal sealed class FRMailFeedbackMessageParser : EntityGraphMessageParserBase<MailEntity>
   {
      public FRMailFeedbackMessageParser(byte[] message)
         : base(message)
      {

      }

      protected override void ReconcileAndIntegrate(MailEntity graphRootEntity)
      {
         try
         {
            var emailStatus = (CommunicationStatus)graphRootEntity.CommunicationStatusCode;
            var touchId = TouchEmailTemplate.GetByExternalId(graphRootEntity.Id);
            TouchEmailTemplate.UpdateStatus(touchId, ConvertCommunicationStatusToRDMailerStatus(emailStatus));
            // var sourceId = Mailer.UpdateEmailStatus(graphRootEntity.Id, ConvertCommunicationStatusToRDMailerStatus(emailStatus));
            switch (emailStatus)
            {
               case CommunicationStatus.NDR:
               case CommunicationStatus.Faulted:
                  TouchEmailTemplate.UpdateNDR(touchId);
                  break;
               case CommunicationStatus.Complaint:
               case CommunicationStatus.OptOut:
                  TouchEmailTemplate.UpdateComplaint(touchId);
                  break;
               case CommunicationStatus.DeliveryConfirmed:
                  TouchEmailTemplate.UpdateDelivery(touchId);
                  break;
            }
         }
         catch (Exception exception)
         {
            throw new Exception(string.Format("Exception while trying to reconcile the Email [{0}]", graphRootEntity.Id), exception);
         }

      }
      /// <summary>
      /// Converts the Amazon status into RD Mailer status equivalent
      /// </summary>
      /// <param name="communicationStatus">Amazon email status</param>
      /// <returns>RD Mailer status</returns>
      private static int ConvertCommunicationStatusToRDMailerStatus(CommunicationStatus communicationStatus)
      {
         var result = 0;
         switch (communicationStatus)
         {
            case CommunicationStatus.Sent:
               result = 0;
               break;
            case CommunicationStatus.DeliveryConfirmed:
               result = 2;
               break;
            case CommunicationStatus.NDR:
               result = 101;
               break;
            case CommunicationStatus.Complaint:
               result = 7;
               break;
         }
         return result;
      }
   }
}
