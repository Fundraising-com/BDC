using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for CarriersCollection.
	/// </summary>
	public class CarriersCollection:EFundraisingCRMCollectionBase
	{
		public CarriersCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Methods
		public void GetAllCarriers() 
		{
			foreach(Carrier c in Carrier.GetCarriers()) 
			{
				List.Add(c);
			}

		}

		
		#endregion

		
	}
}
