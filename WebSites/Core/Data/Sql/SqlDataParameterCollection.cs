//
// 2005-07-11 - Stephen Lim - New class.
//


using System;
using System.Collections;

namespace GA.BDC.Core.Data.Sql
{
	/// <summary>
	/// SqlDataParameterCollection.
	/// </summary>
	[Serializable]
	public class SqlDataParameterCollection : CollectionBase
	{
		/// <summary>
		/// Create a collection of SqlDataParameter.
		/// </summary>
		public SqlDataParameterCollection()
		{

		}

		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public SqlDataParameter this[int index]
		{
			get 
			{ 
				if (Count > index)
					return (SqlDataParameter) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}

		/// <summary>
		/// Get the object with the specified parameter name.
		/// </summary>
		public SqlDataParameter this[string paramName]
		{
			get 
			{ 
				for (int i = 0; i < List.Count; i++)
				{
					SqlDataParameter param = (SqlDataParameter) List[i];
					if (param.ParameterName == paramName)
						return param;
				}
				return null;
			}
		}

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="obj">SqlDataParameter object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(SqlDataParameter obj) 
		{
			return List.Add(obj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="obj">SqlDataParameter object.</param>
		public void Remove(SqlDataParameter obj) 
		{
			List.Remove(obj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="obj">SqlDataParameter object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(SqlDataParameter obj) 
		{
			return List.Contains(obj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="obj">SqlDataParameter object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(SqlDataParameter obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">SqlDataParameter object.</param>
		public void Insert(int index, SqlDataParameter obj) 
		{
			List.Insert(index, obj);
		}
	}
}
