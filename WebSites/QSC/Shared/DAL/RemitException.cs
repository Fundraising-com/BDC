using System;

namespace DAL
{
	/// <summary>
	/// Summary description for RemitException.
	/// </summary>
	public class RemitException : Exception
	{
		private const string EXCEPTION_MESSAGE = "A technical issue was encountered and the remit processing will stop. The IT department has been notified and will address the issue.";

		public override string Message
		{
			get
			{
				return EXCEPTION_MESSAGE;
			}
		}

	}
}
