using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for SalesItemCollection.
	/// </summary>
	public class SalesItemCollection : EFundraisingCRMCollectionBase {
		private ProductClass productClass;
		
		public SalesItemCollection(ProductClass productClass) {
			this.productClass = productClass;
		}
		
		public SalesItemCollection()
		{}

		#region Comparable Methods
		
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() {
			// sort the collection
			SortProcess();
		}

		/*// sort the collection list using the specified sort argument
		public void Sort(SaleComparable sortBy) {
			// set the sort by option
			SetSortBy(sortBy);

			// sort the collection
			SortProcess();
		}*/

		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) {
			SaleCollection copy =
				(SaleCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}

		#endregion
       
		#region Operators
		public static SalesItemCollection operator +(SalesItemCollection collection1, SalesItemCollection collection2) {
			return (SalesItemCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static SalesItemCollection operator +(SalesItemCollection collection, ScratchBook item) {
			return (SalesItemCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static SalesItemCollection operator -(SalesItemCollection collection1, SalesItemCollection collection2) {
			return (SalesItemCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static SalesItemCollection operator -(SalesItemCollection collection, ScratchBook item) {
			return (SalesItemCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion

		#region Properties
		
		public ProductClass ProductClass {
			get { return productClass; }
			set { productClass = value; }
		}
		#endregion

	}
}
