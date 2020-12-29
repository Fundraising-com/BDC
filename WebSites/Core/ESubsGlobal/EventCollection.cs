//
// 2005-08-21 - Stephen Lim - New class.
//


using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for EventCollection.
	/// </summary>
	public class EventCollection : EnvironmentCollectionBase
	{
		#region Constructors
		public EventCollection()
		{

		}
		#endregion

		#region Methods
		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public Event this[int index]
		{
			get 
			{ 
				if (Count > index)
					return (Event) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="obj">Event object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(Event obj) 
		{
			return List.Add(obj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="obj">Event object.</param>
		public void Remove(Event obj) 
		{
			List.Remove(obj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="obj">Event object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(Event obj) 
		{
			return List.Contains(obj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="obj">Event object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(Event obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">Event object.</param>
		public void Insert(int index, Event obj) 
		{
			List.Insert(index, obj);
		}
		#endregion
	}
}
