using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ARStatusCollection.
	/// </summary>
	
	
	public class ARStatusCollection:EFundraisingCRMCollectionBase
	{
		public ARStatusCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Methods
		public void GetAllARStatuss() 
		{
			foreach(ARStatus ars in ARStatus.GetARStatuss()) 
			{
				List.Add(ars);
			}

		}

		
		#endregion

		
	}
}
