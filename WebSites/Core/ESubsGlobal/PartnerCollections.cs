using System;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for PartnerCollections.
	/// </summary>
	public class PartnerCollections : GA.BDC.Core.Collections.BusinessCollectionBase
	{
		public PartnerCollections()
		{
			
		}

		#region Basic Methods For Collection
		/// <summary>Get or set the object at specified index.</summary>
		/// 
		public Partner this[int index]
		{
			get { return (Partner) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>Add object to collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>Index of the newly added object.</returns>
		/// 
		public int Add(Partner obj)
		{
			return List.Add(obj);
		}

		/// <summary>Add collection to collection of objects.</summary>
		/// <param name="obj">LeadCollection object.</param>
		/// 
		public void Add(PartnerCollections obj)
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
		public void Remove(Partner obj)
		{
			List.Remove(obj);
		}

		/// <summary>Check if object is in collection.</summary>
		/// <param name="obj">Lead object</param>
		/// <returns>True if object is in collection, else false.</returns>
		/// 
		public bool Contains(Partner obj)
		{
			return List.Contains(obj);
		}

		/// <summary>Get the index associated with the object in collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>The index of the object.</returns>
		/// 
		public int IndexOf(Partner obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>Insert object into collection at the specified index.</summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">Lead object.</param>
		/// 
		public void Insert(int index, Partner obj) 
		{
			List.Insert(index, obj);
		}

		#endregion

		
		public void SortByType(PartnerComparable srtBy)
		{
			for (int i=0; i < this.Count; i++)
			{
				Partner obj = this[i] as Partner;
				if (obj != null)
					obj.SortBy = srtBy;
			}
			Sort();
		}
	}
}
