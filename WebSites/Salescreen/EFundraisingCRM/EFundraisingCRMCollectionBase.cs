using System;
using System.Collections;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for EFundraisingCRMCollectionBase.
	/// </summary>
	public abstract class EFundraisingCRMCollectionBase : efundraising.Collections.BusinessCollectionBase {

		public EFundraisingCRMCollectionBase() {
			//
			// base class for all collection objects of efundraising.EFundraisingCRM
			// this class contains mainly help methods for faster implementations
			// of collection classes.
			//
		}

		#region Comparer Helper Methods
		// create a copy of the items in the collection with
		// the result of the sort.  Then apply this result
		// to the current items.
		protected void SortProcess() {
			EFundraisingCRMCollectionBase copy =
				EFundraisingCRMCollectionBase.Sort(this);
			ReplaceByCollection(copy);
		}

		// this method replace the items of a collection to the items
		// of the current instance.
		protected void ReplaceByCollection(EFundraisingCRMCollectionBase copy) {
			ArrayList arrayCopy = new ArrayList();
			foreach(EFundraisingCRMObject obj in copy) {
				arrayCopy.Add(obj);
			}
			this.Clear();
			foreach(EFundraisingCRMObject obj in arrayCopy) {
				Add(obj);
			}
		}

		// create a normal array list based on items of the current collection instance
		private static ArrayList CreateArrayList(EFundraisingCRMCollectionBase collection) {
			ArrayList ar = new ArrayList();
			foreach(EFundraisingCRMObject obj in collection.List) {
				ar.Add(obj);
			}
			return ar;
		}

		// sort the collection items based on default or current sort option
		public static EFundraisingCRMCollectionBase Sort(EFundraisingCRMCollectionBase collection) {
			ArrayList ar = CreateArrayList(collection);
			collection.Clear();
			ar.Sort();
			foreach(EFundraisingCRMObject obj in ar) {
				collection.Add(obj);
			}			
			return collection;
		}

		// sort the collection items based on custom icomparer
		public static EFundraisingCRMCollectionBase SortWithComparable(EFundraisingCRMCollectionBase collection, IComparer comparer) {
			ArrayList ar = CreateArrayList(collection);
			collection.Clear();
			ar.Sort(comparer);
			foreach(EFundraisingCRMObject obj in ar) {
				collection.Add(obj);
			}			
			return collection;
		}

		#endregion

		#region Operation Helper Methods
		public static EFundraisingCRMCollectionBase AddCollection(EFundraisingCRMCollectionBase collection1, EFundraisingCRMCollectionBase collection2) {
			foreach(EFundraisingCRMObject obj in collection2) {
				collection1.Add(obj);
			}
			return collection1;
		}

		public static EFundraisingCRMCollectionBase RemoveCollection(EFundraisingCRMCollectionBase collection1, EFundraisingCRMCollectionBase collection2) {
			foreach(EFundraisingCRMObject obj in collection2) {
				collection1.Remove(obj);
			}
			return collection1;
		}

		public static EFundraisingCRMCollectionBase AddItem(EFundraisingCRMCollectionBase collection, EFundraisingCRMObject item) {
			collection.Add(item);
			return collection;
		}

		public static EFundraisingCRMCollectionBase RemoveItem(EFundraisingCRMCollectionBase collection, EFundraisingCRMObject item) {
			collection.Remove(item);
			return collection;
		}

		#endregion

		#region Common Collection Methods and Properties

		/// <summary>Get or set the object at specified index.</summary> 
		public EFundraisingCRMObject this[int index] {
			get { return (EFundraisingCRMObject) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>Add object to collection.</summary>
		/// <param name="obj">User object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(EFundraisingCRMObject obj) {
			return List.Add(obj);
		}

		/// <summary>Remove object from collection.</summary>
		/// <param name="obj">User object.</param>
		public void Remove(EFundraisingCRMObject obj) {
			List.Remove(obj);
		}

		/// <summary>Check if object is in collection.</summary>
		/// <param name="obj">User object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(EFundraisingCRMObject obj) {
			return List.Contains(obj);
		}

		/// <summary>Get the index associated with the object in collection.</summary>
		/// <param name="obj">User object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(EFundraisingCRMObject obj) {
			return List.IndexOf(obj);
		}

		/// <summary>Insert object into collection at the specified index.</summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">User object.</param>
		public void Insert(int index, EFundraisingCRMObject obj) {
			List.Insert(index, obj);
		}

		#endregion

		
		public virtual void SortByProperty(string propertyName)
		{
			for (int i = 0; i < this.List.Count; i++)
			{
				(this.List[i] as efundraising.Core.BusinessBase).sortByPropertyName = propertyName;
			}
			Sort();
		
		}

		
		public virtual void SortByProperty(string propertyName, bool inAscending)
		{
			for (int i = 0; i < this.List.Count; i++)
			{
				(this.List[i] as efundraising.Core.BusinessBase).sortByPropertyName = propertyName;
				(this.List[i] as efundraising.Core.BusinessBase).sortByAscending = inAscending;
			}
			Sort();
		}


	}
}
