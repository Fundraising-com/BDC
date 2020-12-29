using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for CreditCheckStatusCollection.
	/// </summary>
	
		
	public class CreditCheckStatusCollection:EFundraisingCRMCollectionBase
	{
		public CreditCheckStatusCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Methods
		public void GetAllCreditCheckStatuss() 
		{
			foreach(CreditCheckStatus crcks in CreditCheckStatus.GetCreditCheckStatus()) 
			{
				List.Add(crcks);
			}

		}

		
		#endregion

		
	}
}
