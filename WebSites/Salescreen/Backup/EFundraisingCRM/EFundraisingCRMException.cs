using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for EFundraisingCRMException.
	/// </summary>
	public class EFundraisingCRMException : Exception
	{
		public EFundraisingCRMException()
		{
		}
		
		public EFundraisingCRMException(string message) : base(message) 
		{		

		}
		
		public EFundraisingCRMException(string message, EFundraisingCRMObject CRMObject) : base(message) 
		{
		 
		}
		
		public EFundraisingCRMException(string message, System.Exception innerException) : base(message, innerException) 
		{
		 
		}
		
        public EFundraisingCRMException(string message, System.Exception innerException, EFundraisingCRMObject CRMObject) : base(message + Environment.NewLine + CRMObject.ToXmlString(), innerException) 
		{

		}

		
		public EFundraisingCRMException(string message, System.Exception innerException, efundraising.Core.BusinessBase theObject) : base(message + Environment.NewLine + theObject.ToXmlString(), innerException) 
		{

		}
		
	}
}
