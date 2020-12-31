using System;

namespace QSP.WebControl.DataAccess.Common
{
	
	public enum ExceptionType
	{
		Unknown = 0,
		RequiredFields = 1,
		MaxLength = 2,
		Integrity = 3,
		Unicity = 4,
		OtherBusinessRules = 5,
		MaxLimitReached = 6,
		Select = 7
		
	}
	
	
	/// <summary>
	/// Summary description for QCAPException.
	/// This is the Base Exception for the Application
	/// </summary>
	internal class QspException: ApplicationException
	{
		private string htmlMessage = "";		
		
		public QspException()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public QspException(string message): base(message)
		{
		}
		public QspException(string message, string HTMLMessage): base(message)
		{
			htmlMessage = HTMLMessage;
		}
		public QspException(string message, Exception inner): base(message, inner)
		{
		}
		public QspException(string message, string HTMLMessage, Exception inner): base(message, inner)
		{
			htmlMessage = HTMLMessage;			
		}
		public QspException(Message messageManager, Exception inner): base(messageManager.ErrorMessage, inner)
		{
			this.HTMLMessage = messageManager.ErrorHTMLMessage;			
		}
		public QspException(Message messageManager)
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
	/// Summary description for QCAPException.
	/// </summary>
	internal class ValidationException: QspException
	{
		
		private ExceptionType exType = ExceptionType.Unknown;

		public ValidationException()
		{
			
		}

		public ValidationException(string message): base(message)
		{
		}
		public ValidationException(string message, string HTMLMessage): base(message, HTMLMessage)
		{
		}
		public ValidationException(string message, string HTMLMessage, ExceptionType ValidationExceptionType): base(message, HTMLMessage)
		{
			exType = ValidationExceptionType;
		}
		public ValidationException(string message, Exception inner): base(message, inner)
		{
		}
		public ValidationException(string message, string HTMLMessage, Exception inner): base(message, HTMLMessage, inner)
		{
			
		}
	
		public ValidationException(string message, string HTMLMessage, Exception inner, ExceptionType ValidationExceptionType): base(message, HTMLMessage, inner)
		{
			exType = ValidationExceptionType;			
		}

		public ValidationException(Message messageManager): base(messageManager.ErrorMessage)
		{
			this.HTMLMessage = messageManager.ErrorHTMLMessage;
			exType = messageManager.ValidationExceptionType;			
		}

		public ExceptionType ValidationExceptionType
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
