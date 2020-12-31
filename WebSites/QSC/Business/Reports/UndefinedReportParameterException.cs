using System;

namespace Business.Reports
{
	/// <summary>
	/// Summary description for ConfigurationFailedException.
	/// </summary>
	public class UndefinedReportParameterException : Exception
	{
		private string sExceptionMessage = "The report parameter [var1] was not defined.";

		public UndefinedReportParameterException(string reportParameterName) : base()
		{
			sExceptionMessage = sExceptionMessage.Replace("[var1]", reportParameterName);
		}

		public override string Message
		{
			get
			{
				return sExceptionMessage;
			}
		}
	}
}
