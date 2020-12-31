using System;

namespace FileStore
{
	/// <summary>
	/// Summary description for ConfigurationFailedException.
	/// </summary>
	public class StoreAccessFailedException : Exception
	{
		private const string EXCEPTION_MESSAGE = "The file store could not be accessed.";

		public StoreAccessFailedException() : base() { }

		public override string Message
		{
			get
			{
				return EXCEPTION_MESSAGE;
			}
		}
	}
}
