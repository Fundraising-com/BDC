//
// 2005-06-29 - Stephen Lim - New class.
//

using System;
using System.Collections;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
	/// <summary>
	/// TouchRuleCollection.
	/// </summary>
	public class TouchRuleCollection : EnvironmentCollectionBase
	{
		#region Constructors
		/// <summary>
		/// Create a new instance of TouchRuleCollection.
		/// </summary>
		public TouchRuleCollection()
		{

		}
		#endregion

		#region Properties
		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public TouchRule this[int index]
		{
			get 
			{ 
				if (Count > index)
					return (TouchRule) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}
		#endregion

		#region Methods
		
		public static TouchRuleCollection GetRules()
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.GetTouchRules();
		}
      
		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="obj">TouchRule object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(TouchRule obj) 
		{
			return List.Add(obj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="obj">TouchRule object.</param>
		public void Remove(TouchRule obj) 
		{
			List.Remove(obj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="obj">TouchRule object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(TouchRule obj) 
		{
			return List.Contains(obj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="obj">TouchRule object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(TouchRule obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">TouchRule object.</param>
		public void Insert(int index, TouchRule obj) 
		{
			List.Insert(index, obj);
		}
		#endregion
	}
}
