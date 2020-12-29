using System;
using SWCorporate.SystemEx.Console;
using SWCorporate.AppServices.Shared;
using SWCorporate.AppServices.Shared.Contracts;
using GA.BDC.Data.DataLayer;
using GA.BDC.Console.TaskExecutor.Properties;

namespace GA.BDC.Console.TaskExecutor.Tasks
{
    internal sealed class DispatchQueuedMailTask : ITask<TaskFlags>
    {
        #region constructor

        #endregion

        #region explicit implementation of the SWCorporate.SystemEx.Console.ITask<TaskFlags> interface

        void ITask<TaskFlags>.Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            using (var dispatchClient = new DispatchClient(Settings.Default.MassMailerAppName))
            {
                var emails = Mailer.GetBDCEmailsToSend();
                if (emails != null && emails.Count > 0)
                {
                    foreach (var email in emails)
                    {
                        var from = email.from_email.ToLower() != email.from_name.ToLower() ? String.Format("{0} <{1}>", email.from_name, email.from_email) : email.from_email;
                        var reply = email.reply_to_email.ToLower() != email.reply_to_name.ToLower() ? String.Format("{0} <{1}>", email.reply_to_name, email.reply_to_email) : email.reply_to_email;
                        var mail = new MailEntity(from, reply, email.subject, email.bodyhtml, true)
                        {
                            AlternateView = email.bodytxt
                        };
                        mail.MailRecipients.Add(new MailRecipientEntity(MailRecipientType.To, email.to_email, email.to_name));
                        mail = dispatchClient.QueueMailForDispatch(mail);
                        Mailer.UpdateExternalMailID(email.queue_id, mail.Id, Settings.Default.MassMailerSentStatus);
                    }
                }
                if (dispatchClient.Header.Mails.Count > 0)
                {
                    dispatchClient.DispatchQueuedMail();
                }
            }
        }
        #endregion
    }
}
