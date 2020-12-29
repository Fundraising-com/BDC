using System;

namespace GA.BDC.Core.EFundraisingCRM 
{
	/*
	 * Object represents a collection of sales.  
	 * One client can have multiple sales.
	 * 
	 */
	public class PromotionalKitCollection : EFundraisingCRMCollectionBase 
	{

		public PromotionalKitCollection() 
		{
	
		}
		
		public void GetAllPromotionalKits() 
		{
			foreach(PromotionalKit pk in PromotionalKit.GetPromotionalKits()) 
			{
				List.Add(pk);
			}

		}
		public void GetPromotionalKitsByLeadID(int leadID)
		{
			foreach(PromotionalKit p in PromotionalKit.GetPromotionalKitsByLeadID(leadID))
			{
				List.Add(p);
			}
		}

		public PromotionalKitCollection GetPromotionKitsByKitType(int kitTypeId)
		{
			PromotionalKitCollection pkc = new PromotionalKitCollection();
			foreach (PromotionalKit pk in this)
			{
				if (pk.KitTypeId == kitTypeId)
					pkc.Add(pk);
			}
			return pkc;
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
			PromotionalKitCollection copy =
				(PromotionalKitCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}

		// sort the collection list using the specified sort argument
		public void Sort(PromotionalKitComparable sortBy) 
		{
			// set the sort by option
			SetSortBy(sortBy);

			// sort the collection
			SortProcess();
		}


		// apply the sort by to a complete list in the collection
		public void SetSortBy(PromotionalKitComparable comparable) 
		{
			foreach(PromotionalKit pk in List) 
			{
				pk.SortBy = comparable;
			}
		}
		
		#endregion
       
		#region Operators
		public static PromotionalKitCollection operator +(PromotionalKitCollection collection1, PromotionalKitCollection collection2) 
		{
			return (PromotionalKitCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static PromotionalKitCollection operator +(PromotionalKitCollection collection, Sale item) 
		{
			return (PromotionalKitCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static PromotionalKitCollection operator -(PromotionalKitCollection collection1, PromotionalKitCollection collection2) 
		{
			return (PromotionalKitCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static PromotionalKitCollection operator -(PromotionalKitCollection collection, Sale item) 
		{
			return (PromotionalKitCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}
