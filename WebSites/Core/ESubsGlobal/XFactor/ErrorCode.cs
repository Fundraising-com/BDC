using System;

namespace GA.BDC.Core.ESubsGlobal.XFactor
{
	public enum ErrorCodes: int {Unknown = -1 , Successful = 0, InvalidParameter= 2, GroupExisted = 3, 
		ExternalGroupIDExisted=4, InvalidExternalGroupID = 5,
		ParticipantExists = 6, ParticipantDoesNotExists = 7, 
		LoginFailed = 8, GroupNotExisted = 9, TerminateEventFailed = 10, SessionNotSupport= 99 ,Others = 100};


	public class ErrorMessage
	{
		public static string GetErrorMessage(ErrorCodes eCode)
		{
			switch (eCode)
			{
				case ErrorCodes.Successful:
					return eCode.ToString ();

				case ErrorCodes.GroupExisted:
					return "Group Url already exists";
				case ErrorCodes.ExternalGroupIDExisted:
					return "External GroupID already exists";
				case ErrorCodes.InvalidExternalGroupID :
					return "External Group ID is invalid";

				case ErrorCodes.GroupNotExisted:
					return "External Group has not been created";

				case ErrorCodes.ParticipantExists:
					return "Participant already exists";
				case ErrorCodes.ParticipantDoesNotExists:
					return "Participant does not Exists";
				
				case ErrorCodes.SessionNotSupport:
					return "Session does not support. It might be cookies not available";
				case ErrorCodes.TerminateEventFailed:
					return "Unknown: Terminate event failed";

				case ErrorCodes.Unknown: 
				default:
					return ErrorCodes.Unknown.ToString(); 
			}
		}
	}
	public abstract class BaseServiceStatus
	{
		private string _errorMessage;
		private string _passwordGenerated = string.Empty;

		public string errorMessage
		{
			get
			{
				return _errorMessage;
			}

			set
			{
				_errorMessage = value;
			}
		}

		public string passwordGenerated
		{
			get
			{
				return _passwordGenerated;
			}

			set
			{
				_passwordGenerated = value;
			}
		}

		private bool _isUserTest = false;
		public bool IsTestUser
		{
			get
			{
				return _isUserTest;
			}
			set
			{
				_isUserTest = value;
			}
		}
	}

	public class ServiceStatus : BaseServiceStatus
	{

	}
}
