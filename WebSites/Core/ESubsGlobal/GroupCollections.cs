using System;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for GroupCollections.
	/// </summary>
	public class GroupCollections : GA.BDC.Core.Collections.BusinessCollectionBase
	{
		public GroupCollections()
		{
			
		}

		#region Basic Methods For Collection
		/// <summary>Get or set the object at specified index.</summary>
		/// 
		public Group this[int index]
		{
			get { return (Group) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>Add object to collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>Index of the newly added object.</returns>
		/// 
		public int Add(Group obj)
		{
			return List.Add(obj);
		}

		/// <summary>Add collection to collection of objects.</summary>
		/// <param name="obj">LeadCollection object.</param>
		/// 
		public void Add(GroupCollections obj)
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
		/// <param name="obj">Lead object.</param>
		/// 
		public void Remove(Group obj)
		{
			List.Remove(obj);
		}

		/// <summary>Check if object is in collection.</summary>
		/// <param name="obj">Lead object</param>
		/// <returns>True if object is in collection, else false.</returns>
		/// 
		public bool Contains(Group obj)
		{
			return List.Contains(obj);
		}

		/// <summary>Get the index associated with the object in collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>The index of the object.</returns>
		/// 
		public int IndexOf(Group obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>Insert object into collection at the specified index.</summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">Lead object.</param>
		/// 
		public void Insert(int index, Group obj) 
		{
			List.Insert(index, obj);
		}

		#endregion

	}
}
