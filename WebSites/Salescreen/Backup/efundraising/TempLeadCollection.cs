//
// 2006-07-05 : Maxime Normand - New Class.
//

using System;
using efundraising.Collections;

namespace efundraising.efundraisingCore
{
	/// <summary>
	/// Summary description for TempLeadCollection.
	/// </summary>
	public class TempLeadCollection : BusinessCollectionBase
	{
		public TempLeadCollection()
		{
		}
		
		/// <summary>Get or set the object at specified index.</summary>
		/// 
		public TempLead this[int index]
		{
			get { return (TempLead) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>Add object to collection.</summary>
		/// <param name="obj">TempLead object.</param>
		/// <returns>Index of the newly added object.</returns>
		/// 
		public int Add(TempLead obj)
		{
			return List.Add(obj);
		}

		/// <summary>Add collection to collection of objects.</summary>
		/// <param name="obj">TempLeadCollection object.</param>
		/// 
		public void Add(TempLeadCollection obj)
		{
			if (obj != null)
			{
				for (int i = 0; i < obj.Count; i++)
				{
					List.Add(obj[i]);
				}
			}
		}

		/// <summary>Remove object from collection.</summary>
		/// <param name="obj">TempLead object.</param>
		/// 
		public void Remove(TempLead obj)
		{
			List.Remove(obj);
		}

		/// <summary>Check if object is in collection.</summary>
		/// <param name="obj">TempLead object</param>
		/// <returns>True if object is in collection, else false.</returns>
		/// 
		public bool Contains(TempLead obj)
		{
			return List.Contains(obj);
		}

		/// <summary>Get the index associated with the object in collection.</summary>
		/// <param name="obj">TempLead object.</param>
		/// <returns>The index of the object.</returns>
		/// 
		public int IndexOf(TempLead obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>Insert object into collection at the specified index.</summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">TempLead object.</param>
		/// 
		public void Insert(int index, TempLead obj) 
		{
			List.Insert(index, obj);
		}
	}
}

