using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for PromotionalKitAssignedCollection.
	/// </summary>
	public class PromotionalKitAssignedCollection:EFundraisingCRMCollectionBase
	{
		
		public PromotionalKitAssignedCollection(int leadID)
		{
			PromotionalKitCollection temp = new PromotionalKitCollection();
			temp.GetPromotionalKitsByLeadID(leadID);
			foreach(PromotionalKit p in temp)
			{
				List.Add(new PromotionalKitAssignedItem(p.PromotionalKitId));
			}
			
		}
	}
}
