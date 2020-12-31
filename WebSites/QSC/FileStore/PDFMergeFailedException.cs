using System;

namespace FileStore
{
	/// <summary>
	/// Summary description for ConfigurationFailedException.
	/// </summary>
	public class PDFMergeFailedException : Exception
	{
		private const string EXCEPTION_MESSAGE = "The PDF merge tool failed.";
        private string err;
        public PDFMergeFailedException() : base() { }

		public override string Message
		{
			get
			{
                return this.err;
			}
		}
	}
}
