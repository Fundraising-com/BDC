using System;

namespace GA.BDC.Core.EFundraisingCRM 
{

	public class LeadInterestProductClassCollection : EFundraisingCRMCollectionBase 
	{

		public LeadInterestProductClassCollection() 
		{
	
		}



		#region Comparable Methods
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() 
		{
			// sort the collection
			SortProcess();
		}


		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) 
		{
			LeadInterestProductClassCollection copy =
				(LeadInterestProductClassCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}

		#endregion
       
		#region Operators
		public static LeadInterestProductClassCollection operator +(LeadInterestProductClassCollection collection1, LeadInterestProductClassCollection collection2) 
		{
			return (LeadInterestProductClassCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static LeadInterestProductClassCollection operator +(LeadInterestProductClassCollection collection, LeadInterestProductClass item) 
		{
			return (LeadInterestProductClassCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static LeadInterestProductClassCollection operator -(LeadInterestProductClassCollection collection1, LeadInterestProductClassCollection collection2) 
		{
			return (LeadInterestProductClassCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static LeadInterestProductClassCollection operator -(LeadInterestProductClassCollection collection, LeadInterestProductClass item) 
		{
			return (LeadInterestProductClassCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}
