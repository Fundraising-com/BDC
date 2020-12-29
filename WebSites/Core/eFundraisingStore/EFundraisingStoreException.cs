using System;

namespace GA.BDC.Core.eFundraisingStore
{
	/// <summary>
	/// Summary description for eFundraisingStoreException.
	/// </summary>
	public class eFundraisingStoreException : Exception
	{
		public eFundraisingStoreException()
		{
		}
		
		public eFundraisingStoreException(string message) : base(message) 
		{		

		}
		
		public eFundraisingStoreException(string message, eFundraisingStoreObject CRMObject) : base(message) 
		{
		 
		}
		
		public eFundraisingStoreException(string message, System.Exception innerException) : base(message, innerException) 
		{
		 
		}
		
        public eFundraisingStoreException(string message, System.Exception innerException, eFundraisingStoreObject CRMObject) : base(message + Environment.NewLine + CRMObject.ToXmlString(), innerException) 
		{

		}
		
	}
}
