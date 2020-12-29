//
// 2005-06-16 - Stephen Lim - New class.
// 2006-06-08 - KO - Updated fields and modified for Komunik
//

using System;
using System.Collections;
using GA.BDC.Core.Email;
using GA.BDC.Core.Utilities.Validation;

namespace GA.BDC.Core.Email.MassMail
{
	/// <summary>
	/// Email class.
	/// </summary>
	public class Email
	{
		#region Fields
		private int _queueId = -1;		//
		private int _sourceId = -1;		//
		private short _projectId = -1;		//
		private string _replyToName = "";	//
		private string _replyToEmail = "";	//
		private string _toName = "";		//
		private string _toEmail = "";		//
		private string _fromName = "";		//
		private string _fromEmail = "";		//
		private string _ccName = "";		//
		private string _ccEmail = "";		//
		private string _bccName = "";		//
		private string _bccEmail = "";		//
		private string _returnPathName = "";//	
		private string _returnPathEmail = "";//	
		private string _subject = "";		//
		private string _textBody = "";		//
		private string _htmlBody = "";		//
		private EmailQueue.EmailSentStatus _sentStatus = EmailQueue.EmailSentStatus.New;
		private string _message = "";
		private DateTime _datestamp = new DateTime();	//
		private short _priority = 1;
		private EmailQueue.KomunikReturnValue _komunikReturnValueId = EmailQueue.KomunikReturnValue.New;
		private ExtraInfo extraInfo = new ExtraInfo();
		#endregion

		#region Constructors
		/// <summary>
		/// Create a new Email object.
		/// </summary>
		public Email()
		{

		}
		#endregion

		#region Methods
		/// <summary>
		/// Send the email.
		/// </summary>
		/// <param name="smtpServerString">Smtp server</param>
		public void Send(string smtpServerString)
		{
			Send(new SmtpServer(smtpServerString));
		}

		/// <summary>
		/// Send the email.
		/// </summary>
		/// <param name="smtpServer">Smtp server</param>
		public void Send(SmtpServer smtpServer)
		{
			SendMail.Send(smtpServer, SendMail.FormatAddress(_fromName, _fromEmail), 
				SendMail.FormatAddress(_toName, _toEmail), 
				SendMail.FormatAddress(_ccName, _ccEmail), 
				SendMail.FormatAddress(_bccName, _bccEmail), 
				SendMail.FormatAddress(_replyToName, _replyToEmail),
				SendMail.FormatAddress(_returnPathName, _returnPathEmail),
				_subject, _textBody, _htmlBody);
		}
		#endregion

		#region Properties

		/// <summary>
		/// Get the Queue Id assigned to this resource.
		/// </summary>
		public int QueueId
		{
			get { return _queueId; }
			set { _queueId = value; }
		}

		/// <summary>
		/// Get the Source Id assigned to this resource.
		/// </summary>
		public int SourceId
		{
			get { return _sourceId; } 
			set { _sourceId = value;} 
		}

		/// <summary>
		/// Get the Project Id assigned to this resource.
		/// </summary>
		public short ProjectId
		{
			get { return _projectId; }
			set { _projectId = value; }
		}

		/// <summary>
		/// Get or set the name used for the Reply-To field.
		/// </summary>
		public string ReplyToName
		{
			get { return _replyToName; }
			set { 
				if (value == null)
					_replyToName = "";
				else
					_replyToName = value; 
			}
		}

		/// <summary>
		/// Get or set the email for the Reply-To field.
		/// </summary>
		public string ReplyToEmail
		{
			get { return _replyToEmail; }
			set { 

				// Make sure email is valid
				if (value == null)
					_replyToEmail = "";
				else if (value == "" || Utilities.Validation.Email.IsValidEmail(value))
					_replyToEmail = value.ToLower(); 
				else
				{
					throw new ArgumentException("Invalid Reply-to email.");
				}
			}
		}

		/// <summary>
		/// Get or set the name for the To field.
		/// </summary>
		public string ToName
		{
			get { return _toName; }
			set 
			{
				if (value == null)
					_toName = "";
				else
					_toName = value; 
			}
		}

		/// <summary>
		/// Get or set the email for the To field.
		/// </summary>
		public string ToEmail
		{
			get { return _toEmail; }
			set 
			{ 
				// Make sure email is valid
				if (value == null)
					_toEmail = "";
				else 
				{
					value = value.Trim();
					if (value == "" || Utilities.Validation.Email.IsValidEmail(value))
						_toEmail = value.ToLower(); 
					else
						throw new ArgumentException("Invalid To email.");
				}
			}
		}

		/// <summary>
		/// Get or set the name for the From field.
		/// </summary>
		public string FromName
		{
			get { return _fromName; }
			set 
			{
				if (value == null)
					_fromName = "";
				else
					_fromName = value; 
			}
		}

		/// <summary>
		/// Get or set the email for the From field.
		/// </summary>
		public string FromEmail
		{
			get { return _fromEmail; }
			set 
			{ 
				// Make sure email is valid
				if (value == null)
					value = "";
				else 
				{
					value = value.Trim();
					if (value == "" || Utilities.Validation.Email.IsValidEmail(value))
						_fromEmail = value.ToLower(); 
					else
						throw new ArgumentException("Invalid From email.");
				}
			}
		}

		/// <summary>
		/// Get or set the name for the CC field.
		/// </summary>
		public string CcName
		{
			get { return _ccName; }
			set 
			{ 
				if (value == null)
					_ccName = "";
				else
					_ccName = value; 
			}
		}

		/// <summary>
		/// Get or set the email for the CC field.
		/// </summary>
		public string CcEmail
		{
			get { return _ccEmail; }
			set 
			{ 
				// Make sure email is valid
				if (value == null)
					_ccEmail = "";
				else 
				{
					value = value.Trim();
					if (value == "" || Utilities.Validation.Email.IsValidEmail(value))
						_ccEmail = value.ToLower(); 
					else
						throw new ArgumentException("Invalid Cc email.");
				}
			}
		}

		/// <summary>
		/// Get or set the name for the BCC field.
		/// </summary>
		public string BccName
		{
			get { return _bccName; }
			set 
			{ 
				if (value == null)
					_bccName = "";
				else
					_bccName = value; 
			}
		}

		/// <summary>
		/// Get or set the email for the BCC field.
		/// </summary>
		public string BccEmail
		{
			get { return _bccEmail; }
			set 
			{ 
				// Make sure email is valid
				if (value == null)
					value = "";
				else 
				{
					value = value.Trim();
					if (value == "" || Utilities.Validation.Email.IsValidEmail(value))
						_bccEmail = value.ToLower(); 
					else
						throw new ArgumentException("Invalid Bcc email.");
				}
			}
		}

		/// <summary>
		/// Get or set the name for the Return-Path field.
		/// </summary>
		public string ReturnPathName
		{
			get { return _returnPathName; }
			set 
			{
				if (value == null)
					_returnPathName = "";
				else
					_returnPathName = value; }
		}

		/// <summary>
		/// Get or set the email for the Return-Path field.
		/// </summary>
		public string ReturnPathEmail
		{
			get { return _returnPathEmail; }
			set 
			{ 
				// Make sure email is valid
				if (value == null)
					_returnPathEmail = "";
				else 
				{
					value = value.Trim();
					if (value == "" || Utilities.Validation.Email.IsValidEmail(value))
						 _returnPathEmail = value.ToLower(); 
					else
						 throw new ArgumentException("Invalid Return-path email address.");
				}
			}
		}

		/// <summary>
		/// Get or set the Subject field.
		/// </summary>
		public string Subject
		{
			get { return _subject; }
			set { _subject = value; }
		}

		/// <summary>
		/// Get or set the Text Body.
		/// </summary>
		public string TextBody
		{
			get { return _textBody; }
			set { _textBody = value; }
		}

		/// <summary>
		/// Get or set the HTML Body.
		/// </summary>
		public string HtmlBody

		{
			get { return _htmlBody; }
			set { _htmlBody = value; }
		}

		/// <summary>
		/// Get or set the sent status of the email.
		/// </summary>
		public EmailQueue.EmailSentStatus SentStatus
		{
			get { return _sentStatus; }
			set { _sentStatus = value; }
		}

		/// <summary>
		/// Get or set the status message, if any.
		/// </summary>
		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

		public DateTime Datestamp
		{
			get { return _datestamp; }
			set { _datestamp = value; }
		}

		public short Priority
		{
			get { return _priority; }
			set { _priority = value; }
		}

		public EmailQueue.KomunikReturnValue KomunikReturnValueId
		{
			get { return _komunikReturnValueId; }
			set { _komunikReturnValueId = value; }
		}
		
		public ExtraInfo ExtraInfo
		{
			get { return extraInfo;}
			set { extraInfo = value;}
		}

		#endregion
	}
}
