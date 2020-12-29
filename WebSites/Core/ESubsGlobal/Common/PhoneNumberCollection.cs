/* Title:	PhoneNumberCollection
 * Author:	Jean-Francois Buist
 * Summary:	Contains phone numbers for members.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Common {
	/// <summary>
	/// Summary description for PhoneNumberCollection.
	/// </summary>
    [Serializable]
	public class PhoneNumberCollection : EnvironmentCollectionBase {

		#region Constructors
		/// <summary>
		/// Create a new instance of PhoneNumberCollection.
		/// </summary>
		public PhoneNumberCollection()
		{

		}
		#endregion

		#region Properties
		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public PhoneNumber this[int index]
		{
			get 
			{ 
				if (Count > index)
					return (PhoneNumber) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}
		#endregion

		#region Methods

		public PhoneNumber GetDefaultPhoneNumber() 
		{	
			if(List.Count > 0) 
			{
				return (PhoneNumber) List[0];
			}
			return null;
		}

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="obj">PhoneNumber object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(PhoneNumber obj) 
		{
			return List.Add(obj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="obj">PhoneNumber object.</param>
		public void Remove(PhoneNumber obj) 
		{
			List.Remove(obj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="obj">PhoneNumber object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(PhoneNumber obj) 
		{
			return List.Contains(obj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="obj">PhoneNumber object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(PhoneNumber obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">PhoneNumber object.</param>
		public void Insert(int index, PhoneNumber obj) 
		{
			List.Insert(index, obj);
		}
		#endregion
	}
}
