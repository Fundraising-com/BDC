//
// 2005-08-01 - Stephen Lim - New class.
//

using System;
using System.Collections;

namespace GA.BDC.Core.LinkShare
{
	/// <summary>
	/// Summary description for LsTransEntryCollection.
	/// </summary>
	[Serializable]
	public class LsTransEntryCollection : CollectionBase
	{
		/// <summary>
		/// Create a collection of LsTransEntrys.
		/// </summary>
		public LsTransEntryCollection()
		{

		}

		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public LsTransEntry this[int index]
		{
			get 
			{ 
				if (Count > index)
					return (LsTransEntry) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="obj">LsTransEntry object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(LsTransEntry obj) 
		{
			return List.Add(obj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="obj">LsTransEntry object.</param>
		public void Remove(LsTransEntry obj) 
		{
			List.Remove(obj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="obj">LsTransEntry object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(LsTransEntry obj) 
		{
			return List.Contains(obj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="obj">LsTransEntry object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(LsTransEntry obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">LsTransEntry object.</param>
		public void Insert(int index, LsTransEntry obj) 
		{
			List.Insert(index, obj);
		}
	}
}
