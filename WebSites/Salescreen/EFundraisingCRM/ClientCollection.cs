using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ClientCollection.
	/// </summary>
	public class ClientCollection : EFundraisingCRMCollectionBase {
		public ClientCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Methods
		public void GetAllClients() 
		{
			foreach(Client client in Client.GetClients()) 
			{
				List.Add(client);
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
		public static ClientCollection operator +(ClientCollection collection1, ClientCollection collection2) {
			return (ClientCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static ClientCollection operator +(ClientCollection collection, Client item) {
			return (ClientCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static ClientCollection operator -(ClientCollection collection1, ClientCollection collection2) {
			return (ClientCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static ClientCollection operator -(ClientCollection collection, Client item) {
			return (ClientCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion

	}
}

