//
// 2005-08-22 - Stephen Lim - New class.
//


using System;
using System.Configuration;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for TellAFriend.
	/// </summary>
	public class TellAFriend : EnvironmentBase
	{
		#region Fields
		private int _id = int.MinValue;
		private string _senderName = null;
		private string _senderEmail = null;
		private string _subject = null;
		private string _textBody = null;
		private string _htmlBody = null;
		private string _recipientName = null;
		private string _recipientEmail = null;
		#endregion

		#region Constructors
		public TellAFriend()
		{
			
		}

		public TellAFriend(string senderName, string senderEmail, string subject, 
			string textBody, string htmlBody, string recipientName, string recipientEmail)
		{
			_senderName = senderName;
			_senderEmail = senderEmail;
			_subject = subject;
			_textBody = textBody;
			_htmlBody = htmlBody;
			_recipientName = recipientName;
			_recipientEmail = recipientEmail;
		}
		#endregion


		#region Methods
		public void Send()
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.InsertTellAFriend(this);

                string smtp = ConfigurationManager.AppSettings["Common.Email.SmtpServer"];

				// send a copy
				GA.BDC.Core.Email.SendMail.AsyncSend(smtp, 
					this.SenderEmail, "jfbuist@rd.com", "", "", this.SenderEmail,
					this.SenderEmail, this.Subject, this.TextBody, this.HtmlBody);

			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException("Unable to insert tell a friend.", ex, this);
			}
		}
		#endregion


		#region Properties
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public string SenderName
		{
			get { return _senderName; }
			set { _senderName = value; }
		}

		public string SenderEmail
		{
			get { return _senderEmail; }
			set { _senderEmail = value; }
		}

		public string Subject
		{
			get { return _subject; }
			set { _subject = value; }
		}

		public string TextBody
		{
			get { return _textBody; }
			set { _textBody = value; }
		}

		public string HtmlBody
		{
			get { return _htmlBody; }
			set { _htmlBody = value; }
		}

		public string RecipientName
		{
			get { return _recipientName; }
			set { _recipientName = value; }
		}

		public string RecipientEmail
		{
			get { return _recipientEmail; }
			set { _recipientEmail = value; }
		}
		#endregion

	}
}
