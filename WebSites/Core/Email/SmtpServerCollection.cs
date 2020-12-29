//
// 2005-06-16 - Stephen Lim - New class.
//

using System;
using System.Collections;

namespace GA.BDC.Core.Email
{
	/// <summary>
	/// Summary description for EmailCollection.
	/// </summary>
	[Serializable]
	public class SmtpServerCollection : CollectionBase
	{
		/// <summary>
		/// Create a collection of SmtpServers.
		/// </summary>
		public SmtpServerCollection()
		{

		}

		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public SmtpServer this[int index]
		{
			get { return (SmtpServer) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="s">SmtpServer object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(SmtpServer s) 
		{
			return List.Add(s);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="s">SmtpServer object.</param>
		public void Remove(SmtpServer s) 
		{
			List.Remove(s);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="s">SmtpServer object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(SmtpServer s) 
		{
			return List.Contains(s);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="s">SmtpServer object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(SmtpServer s)
		{
			return List.IndexOf(s);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="s">SmtpServer object.</param>
		public void Insert(int index, SmtpServer s) 
		{
			List.Insert(index, s);
		}
	}
}
