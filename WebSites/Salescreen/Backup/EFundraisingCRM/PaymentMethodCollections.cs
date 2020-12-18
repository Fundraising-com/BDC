using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for PaymentMethodCollections.
	/// </summary>
	public class PaymentMethodCollections: EFundraisingCRMCollectionBase
	{
		public PaymentMethodCollections()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		

		public void GetAllPaymentMethods() 
		{
			PaymentMethod[] pms = PaymentMethod.GetPaymentMethods();
			for (int i=0; i< pms.Length; i++)
			{
				List.Add(pms[i]);
			}
		}

	}
}
