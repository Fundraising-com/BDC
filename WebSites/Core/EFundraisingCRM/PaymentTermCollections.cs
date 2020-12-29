using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for PaymentTermCollections.
	/// </summary>
	public class PaymentTermCollections: EFundraisingCRMCollectionBase
	{
		public PaymentTermCollections()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		public void GetAllPaymentTerms() 
		{
			PaymentTerm[] pyt = PaymentTerm.GetPaymentTerms();
			for (int i=0; i< pyt.Length; i++)
			{
				List.Add(pyt[i]);
			}
		}
	}
}
