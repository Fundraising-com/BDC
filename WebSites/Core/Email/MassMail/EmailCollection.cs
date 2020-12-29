//
// 2005-06-16 - Stephen Lim - New class.
//

using System;
using System.Collections;

namespace GA.BDC.Core.Email.MassMail
{
	/// <summary>
	/// Summary description for EmailCollection.
	/// </summary>
	[Serializable]
	public class EmailCollection : CollectionBase
	{
		/// <summary>
		/// Create a collection of emails.
		/// </summary>
		public EmailCollection()
		{

		}

		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public Email this[int index]
		{
			get { 
				if (Count > index)
					return (Email) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="emailObj">Email object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(Email emailObj) 
		{
			return List.Add(emailObj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="emailObj">Email object.</param>
		public void Remove(Email emailObj) 
		{
			List.Remove(emailObj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="emailObj">Email object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(Email emailObj) 
		{
			return List.Contains(emailObj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="emailObj">Email object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(Email emailObj)
		{
			return List.IndexOf(emailObj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="emailObj">Email object.</param>
		public void Insert(int index, Email emailObj) 
		{
			List.Insert(index, emailObj);
		}
	}
}
