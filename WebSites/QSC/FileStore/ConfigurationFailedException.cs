using System;

namespace FileStore
{
	/// <summary>
	/// Summary description for ConfigurationFailedException.
	/// </summary>
	public class ConfigurationFailedException : Exception
	{
		private const string EXCEPTION_MESSAGE = "The store configuration could not be loaded.";

		public ConfigurationFailedException() : base() { }

		public override string Message
		{
			get
			{
				return EXCEPTION_MESSAGE;
			}
		}
	}
}
