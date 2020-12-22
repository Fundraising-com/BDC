using System;

namespace efundraising.EFundraisingCRM {
	/*
	 * Object represents a collection of sales.  
	 * One client can have multiple sales.
	 * 
	 */
	public class SaleCollection : EFundraisingCRMCollectionBase {

		public SaleCollection() {
	
		}

		#region Methods
		// get the grand total of sales
		public double GetSalesTotal() {
			double total = double.MinValue;
			foreach(Sale sale in List) {
				total += sale.TotalAmount;
			}
			return total;
		}

		#endregion

		#region Comparable Methods
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() {
			// sort the collection
			SortProcess();
		}

		// sort the collection list using the specified sort argument
		public void Sort(SaleComparable sortBy) {
			// set the sort by option
			SetSortBy(sortBy);

			// sort the collection
			SortProcess();
		}

		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) {
			SaleCollection copy =
				(SaleCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}

		// apply the sort by to a complete list in the collection
		public void SetSortBy(SaleComparable comparable) {
			foreach(Sale sale in List) {
				sale.SortBy = comparable;
			}
		}
		#endregion
       
		#region Operators
		public static SaleCollection operator +(SaleCollection collection1, SaleCollection collection2) {
			return (SaleCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static SaleCollection operator +(SaleCollection collection, Sale item) {
			return (SaleCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static SaleCollection operator -(SaleCollection collection1, SaleCollection collection2) {
			return (SaleCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static SaleCollection operator -(SaleCollection collection, Sale item) {
			return (SaleCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}
