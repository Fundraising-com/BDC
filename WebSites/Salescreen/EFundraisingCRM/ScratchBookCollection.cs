using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ScratchBookCollection.
	/// </summary>
	public class ScratchBookCollection : EFundraisingCRMCollectionBase {
		public ScratchBookCollection()
		{
			
		}

		#region Comparable Methods
		
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() {
			// sort the collection
			SortProcess();
		}


		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) {
			SaleCollection copy =
				(SaleCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}

		#endregion
       
		#region Operators
		public static ScratchBookCollection operator +(ScratchBookCollection collection1, ScratchBookCollection collection2) {
			return (ScratchBookCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static ScratchBookCollection operator +(ScratchBookCollection collection, ScratchBook item) {
			return (ScratchBookCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static ScratchBookCollection operator -(ScratchBookCollection collection1, ScratchBookCollection collection2) {
			return (ScratchBookCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static ScratchBookCollection operator -(ScratchBookCollection collection, ScratchBook item) {
			return (ScratchBookCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion

	}
}
