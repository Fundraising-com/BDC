using System;
using System.Collections.Generic;


namespace GA.BDC.WebApi.Fundraising.Core.Helpers
{
   public static class EmailManager
   {
      public static void Send(KeyValuePair<string, string> from, IDictionary<string, string> to, KeyValuePair<string, string> replyTo, string subject, string bodyText, string bodyHtml, IDictionary<string, string> cc = null, IDictionary<string, string> bcc = null, int delay = 0)
      {
         //using (var dispatchClient = new DispatchClient("BDC MassMailer"))
         //{
         //   var mail = new MailEntity(String.Format("{0} <{1}>", from.Value, from.Key), String.Format("{0} <{1}>", replyTo.Value, replyTo.Key), subject, bodyHtml, true)
         //   {
         //      AlternateView = bodyText
         //   };
         //   foreach (var email in to)
         //   {
         //      mail.MailRecipients.Add(new MailRecipientEntity(MailRecipientType.To, email.Key, email.Value));
         //   }
         //   if (cc != null)
         //   {
         //      foreach (var email in cc)
         //      {
         //         mail.MailRecipients.Add(new MailRecipientEntity(MailRecipientType.Cc, email.Key, email.Value));
         //      }
         //   }
         //   if (bcc != null)
         //   {
         //      foreach (var email in bcc)
         //      {
         //         mail.MailRecipients.Add(new MailRecipientEntity(MailRecipientType.Bcc, email.Key, email.Value));
         //      }
         //   }
         //   if (delay > 0)
         //   {
         //      mail.DispatchDelay = delay;
         //   }
         //   dispatchClient.QueueMailForDispatch(mail);
         //}
      }
   }
}