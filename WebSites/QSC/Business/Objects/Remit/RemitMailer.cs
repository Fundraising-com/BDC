using System;
using System.Text;
using System.Web.Mail;

namespace Business.Objects
{
	/// <summary>
	/// Mailer class for Remit Automation
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class RemitMailer : MailMessage
	{
		private const string MESSAGE_FROM = "RemitAutomation_Email_From";

		private const string REMIT_AUTOMATION_EMAIL_CONSTANT = "RemitAutomationValidationEmail";
		private const string MESSAGE_SUBJECT = "Remit Results";

		private const string GIFT_CARDS_EMAIL = "GiftCardsEmail";
		private const string GIFT_CARDS_MESSAGE_SUBJECT = "QSP Gift Cards";

		private StringBuilder stringBuilder = null;

		public RemitMailer(string MessageFrom, string MessageTo, string MessageSubject) : base ()
		{
			this.BodyFormat = MailFormat.Html;

			this.From = MessageFrom;
			this.To = MessageTo;
			this.Bcc = QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ErrorWebTo;
			this.Subject = MessageSubject;
		}

		public StringBuilder BodyBuilder 
		{
			get 
			{
				if(stringBuilder == null) 
				{
					stringBuilder = new StringBuilder();
				}

				return stringBuilder;
			}
		}

		public virtual void Send() 
		{
			this.Body = BodyBuilder.ToString();

			SmtpMail.SmtpServer = QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ErrorWebSmtp;

			System.Web.Mail.SmtpMail.Send(this);
		}

		public void AddAttachment(string MessageAttachmentPath)
		{
			// Create and add the attachment
			MailAttachment attachment = new MailAttachment(MessageAttachmentPath);
			this.Attachments.Add(attachment);
		}
	}
}
