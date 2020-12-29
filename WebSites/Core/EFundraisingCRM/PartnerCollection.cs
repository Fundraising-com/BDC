using System;

namespace GA.BDC.Core.EFundraisingCRM {

	/// <summary>
	/// Summary description for PartnerCollection.
	/// </summary>
	public class PartnerCollection : EFundraisingCRMCollectionBase {
		private bool sortAssending = true;

		public PartnerCollection() {
			//
			// TODO: Add constructor logic here
			//
		}

		#region Sorting Methods
		public void Sort(PartnerSort partnerSort) {
			for(int i=0;i<List.Count;i++) {
				Partner partner = (Partner)List[i];
				partner.SortType = partnerSort;
				partner.SortAssending = sortAssending;
			}
			Sort();
		}
		#endregion

		#region Methods

		public void LoadAllPartners() {
			Partner[] partners = Partner.GetPartners();
			foreach(Partner partner in partners) {
				List.Add(partner);
			}
		}

		public Partner GetPartnerByPartnerID(int partnerID) {
			for(int i=0;i<List.Count;i++) {
				Partner partner = (Partner)List[i];
				if(partner.PartnerId == partnerID) {
					return partner;
				}
			}
			return null;
		}

		public void LoadAllPartnersByPartnerName(string name) {
			Partner[] partners = Partner.GetPartnersByName(name);
			foreach(Partner partner in partners) {
				List.Add(partner);
			}

		}

		/*
		// get the grand total of sales
		public float GetSalesTotal() {
			float total = 0.0f;
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
		}*/
		#endregion
       
		#region Operators
		public static PartnerCollection operator +(PartnerCollection collection1, PartnerCollection collection2) {
			return (PartnerCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static PartnerCollection operator +(PartnerCollection collection, Partner item) {
			return (PartnerCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static PartnerCollection operator -(PartnerCollection collection1, PartnerCollection collection2) {
			return (PartnerCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static PartnerCollection operator -(PartnerCollection collection, Partner item) {
			return (PartnerCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion

		#region Properties
		public bool SortAssending {
			get { return sortAssending; }
			set { sortAssending = value; }
		}
		#endregion
	}
}

