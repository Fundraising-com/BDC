using System;

namespace QSPForm.Common
{
	
	public enum QSPFormExceptionType
	{
		Unknown = 0,
		RequiredFields = 1,
		MaxLength = 2,
		Integrity = 3,
		Unicity = 4,
		OtherBusinessRules = 5,
		MaxLimitReached = 6,
		OrderBizRule = 7,
		RecordIsDeleted = 8,
		RecordIsModified = 9,
		
	}
	
	
	/// <summary>
	/// Summary description for QSPFormException.
	/// This is the Base Exception for the Application
	/// </summary>
	public class QSPFormException: ApplicationException
	{
		private string htmlMessage = "";		
		
		public QSPFormException()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public QSPFormException(string message): base(message)
		{
		}
		public QSPFormException(string message, string HTMLMessage): base(message)
		{
			htmlMessage = HTMLMessage;
		}
		public QSPFormException(string message, Exception inner): base(message, inner)
		{
		}
		public QSPFormException(string message, string HTMLMessage, Exception inner): base(message, inner)
		{
			htmlMessage = HTMLMessage;			
		}
		public QSPFormException(QSPFormMessage messageManager, Exception inner): base(messageManager.ErrorMessage, inner)
		{
			this.HTMLMessage = messageManager.ErrorHTMLMessage;			
		}
	
		public string HTMLMessage
		{
			get
			{
				return htmlMessage;
			}
			set
			{
				htmlMessage = value;
			}
		
		}
		
	}

	/// <summary>
	/// Summary description for QSPFormException.
	/// </summary>
	public class QSPFormValidationException: QSPFormException
	{
		
		private QSPFormExceptionType exType = QSPFormExceptionType.Unknown;

		public QSPFormValidationException()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public QSPFormValidationException(string message): base(message)
		{
		}
		public QSPFormValidationException(string message, string HTMLMessage): base(message, HTMLMessage)
		{
		}
		public QSPFormValidationException(string message, string HTMLMessage, QSPFormExceptionType ValidationExceptionType): base(message, HTMLMessage)
		{
			exType = ValidationExceptionType;
		}
		public QSPFormValidationException(string message, Exception inner): base(message, inner)
		{
		}
		public QSPFormValidationException(string message, string HTMLMessage, Exception inner): base(message, HTMLMessage, inner)
		{
			
		}
	
		public QSPFormValidationException(string message, string HTMLMessage, Exception inner, QSPFormExceptionType ValidationExceptionType): base(message, HTMLMessage, inner)
		{
			exType = ValidationExceptionType;			
		}

		public QSPFormValidationException(QSPFormMessage messageManager): base(messageManager.ErrorMessage)
		{
			this.HTMLMessage = messageManager.ErrorHTMLMessage;
			exType = messageManager.ValidationExceptionType;			
		}

		public QSPFormExceptionType ValidationExceptionType
		{
			get
			{
				return exType;
			}
			set
			{
				exType = value;
			}
		
		}

	}
}
