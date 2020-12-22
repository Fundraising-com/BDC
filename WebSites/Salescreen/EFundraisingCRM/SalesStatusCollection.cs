using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for SalesStatusCollection.
	/// </summary>
	
	public class SalesStatusCollection:EFundraisingCRMCollectionBase
	{
		public SalesStatusCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Methods
		public void GetAllSalesStatuss() 
		{
			foreach(SalesStatus ss in SalesStatus.GetSalesStatuss()) 
			{
				List.Add(ss);
			}

		}

		
		#endregion

		
	}
}
