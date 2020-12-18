using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for CarrierShippingOptionCollection.
	/// </summary>
	
	public class CarrierShippingOptionCollection:EFundraisingCRMCollectionBase
	{
		public CarrierShippingOptionCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Methods
		public void GetAllCarrierShippingOptions() 
		{
			foreach(CarrierShippingOption cso in CarrierShippingOption.GetCarrierShippingOptions()) 
			{
				List.Add(cso);
			}

		}

		
		#endregion

		
	}
}
