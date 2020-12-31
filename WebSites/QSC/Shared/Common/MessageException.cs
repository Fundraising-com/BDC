using System;

namespace Common
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
	public class MessageException: ApplicationException
	{
		private string htmlMessage = "";		
		
		public MessageException()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public MessageException(string message): base(message)
		{
			htmlMessage = message;
		}
		public MessageException(string message, string HTMLMessage): base(message)
		{
			htmlMessage = HTMLMessage;
		}
		public MessageException(string message, Exception inner): base(message, inner)
		{
			htmlMessage = message;
		}
		public MessageException(string message, string HTMLMessage, Exception inner): base(message, inner)
		{
			htmlMessage = HTMLMessage;			
		}
		public MessageException(Message messageManager, Exception inner): base(messageManager.ErrorMessage, inner)
		{
			this.HTMLMessage = messageManager.ErrorHTMLMessage;			
		}
		public MessageException(Message messageManager)
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
	public class ValidationException: MessageException
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
