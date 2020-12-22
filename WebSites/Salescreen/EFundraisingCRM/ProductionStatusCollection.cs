using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ProductionStatusCollection.
	/// </summary>
	
	public class ProductionStatusCollection:EFundraisingCRMCollectionBase
	{
		public ProductionStatusCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Methods
		public void GetAllProductionStatuss() 
		{
			foreach(ProductionStatus ps in ProductionStatus.GetProductionStatuss()) 
			{
				List.Add(ps);
			}

		}

		
		#endregion

		
	}
}
