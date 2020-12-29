using System;
using System.Collections;

namespace GA.BDC.Core.eFundraisingStore
{
	/// <summary>
	/// Summary description for eFundraisingStoreCollectionBase.
	/// </summary>
	public abstract class eFundraisingStoreCollectionBase : GA.BDC.Core.Collections.BusinessCollectionBase {

		public eFundraisingStoreCollectionBase() {
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
			eFundraisingStoreCollectionBase copy =
				eFundraisingStoreCollectionBase.Sort(this);
			ReplaceByCollection(copy);
		}

		// this method replace the items of a collection to the items
		// of the current instance.
		protected void ReplaceByCollection(eFundraisingStoreCollectionBase copy) {
			ArrayList arrayCopy = new ArrayList();
			foreach(eFundraisingStoreObject obj in copy) {
				arrayCopy.Add(obj);
			}
			this.Clear();
			foreach(eFundraisingStoreObject obj in arrayCopy) {
				Add(obj);
			}
		}

		// create a normal array list based on items of the current collection instance
		private static ArrayList CreateArrayList(eFundraisingStoreCollectionBase collection) {
			ArrayList ar = new ArrayList();
			foreach(eFundraisingStoreObject obj in collection.List) {
				ar.Add(obj);
			}
			return ar;
		}

		// sort the collection items based on default or current sort option
		public static eFundraisingStoreCollectionBase Sort(eFundraisingStoreCollectionBase collection) {
			ArrayList ar = CreateArrayList(collection);
			collection.Clear();
			ar.Sort();
			foreach(eFundraisingStoreObject obj in ar) {
				collection.Add(obj);
			}			
			return collection;
		}

		// sort the collection items based on custom icomparer
		public static eFundraisingStoreCollectionBase SortWithComparable(eFundraisingStoreCollectionBase collection, IComparer comparer) {
			ArrayList ar = CreateArrayList(collection);
			collection.Clear();
			ar.Sort(comparer);
			foreach(eFundraisingStoreObject obj in ar) {
				collection.Add(obj);
			}			
			return collection;
		}

		#endregion

		#region Operation Helper Methods
		public static eFundraisingStoreCollectionBase AddCollection(eFundraisingStoreCollectionBase collection1, eFundraisingStoreCollectionBase collection2) {
			foreach(eFundraisingStoreObject obj in collection2) {
				collection1.Add(obj);
			}
			return collection1;
		}

		public static eFundraisingStoreCollectionBase RemoveCollection(eFundraisingStoreCollectionBase collection1, eFundraisingStoreCollectionBase collection2) {
			foreach(eFundraisingStoreObject obj in collection2) {
				collection1.Remove(obj);
			}
			return collection1;
		}

		public static eFundraisingStoreCollectionBase AddItem(eFundraisingStoreCollectionBase collection, eFundraisingStoreObject item) {
			collection.Add(item);
			return collection;
		}

		public static eFundraisingStoreCollectionBase RemoveItem(eFundraisingStoreCollectionBase collection, eFundraisingStoreObject item) {
			collection.Remove(item);
			return collection;
		}

		#endregion

		#region Common Collection Methods and Properties

		/// <summary>Get or set the object at specified index.</summary> 
		public eFundraisingStoreObject this[int index] {
			get { return (eFundraisingStoreObject) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>Add object to collection.</summary>
		/// <param name="obj">User object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(eFundraisingStoreObject obj) {
			return List.Add(obj);
		}

		/// <summary>Remove object from collection.</summary>
		/// <param name="obj">User object.</param>
		public void Remove(eFundraisingStoreObject obj) {
			List.Remove(obj);
		}

		/// <summary>Check if object is in collection.</summary>
		/// <param name="obj">User object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(eFundraisingStoreObject obj) {
			return List.Contains(obj);
		}

		/// <summary>Get the index associated with the object in collection.</summary>
		/// <param name="obj">User object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(eFundraisingStoreObject obj) {
			return List.IndexOf(obj);
		}

		/// <summary>Insert object into collection at the specified index.</summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">User object.</param>
		public void Insert(int index, eFundraisingStoreObject obj) {
			List.Insert(index, obj);
		}

		#endregion

	}
}
