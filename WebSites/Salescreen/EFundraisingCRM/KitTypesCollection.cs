using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for KitTypesCollection.
	/// </summary>
	public class KitTypesCollection:EFundraisingCRMCollectionBase
	{
		public KitTypesCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Methods
		public void GetAllKitTypes() 
		{
			foreach(KitType k in KitType.GetKitTypes()) 
			{
				List.Add(k);
			}

		}
		public void GetAllActiveKitTypes()
		{
			
			foreach(KitType k in KitType.GetKitTypesActive()) 
			{
				List.Add(k);
			}
		}
		#endregion
	}
}
