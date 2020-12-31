using System;

namespace FileStore
{
	/// <summary>
	/// Summary description for ConfigurationFailedException.
	/// </summary>
	public class PDFMergeTimeOutException : Exception
	{
		private const string EXCEPTION_MESSAGE = "The PDF merge operation timed out.";

		public PDFMergeTimeOutException() : base() { }

		public override string Message
		{
			get
			{
				return EXCEPTION_MESSAGE;
			}
		}
	}
}
