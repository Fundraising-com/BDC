//
// 2005-06-29 - Stephen Lim - New class.
//


using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
	/// <summary>
	/// TouchEmailCollection.
	/// </summary>
	public class TouchEmailCollection : EnvironmentCollectionBase
    {

        #region Private Members
   
        #endregion

        #region Constructors
        /// <summary>
		/// Create a new instance of TouchEmailCollection.
		/// </summary>
		public TouchEmailCollection()
		{

		}
		#endregion

		#region Properties
		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public TouchEmail this[int index]
		{
			get 
			{ 
				if (Count > index)
					return (TouchEmail) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}
		#endregion

		#region Methods

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="emailObj">TouchEmail object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(TouchEmail emailObj) 
		{
			return List.Add(emailObj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="emailObj">TouchEmail object.</param>
		public void Remove(TouchEmail emailObj) 
		{
			List.Remove(emailObj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="emailObj">TouchEmail object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(TouchEmail emailObj) 
		{
			return List.Contains(emailObj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="emailObj">TouchEmail object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(TouchEmail emailObj)
		{
			return List.IndexOf(emailObj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="emailObj">TouchEmail object.</param>
		public void Insert(int index, TouchEmail emailObj) 
		{
			List.Insert(index, emailObj);
		}
		#endregion
	}
}
