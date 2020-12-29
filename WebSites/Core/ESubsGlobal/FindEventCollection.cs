//
// 2005-08-21 - Stephen Lim - New class.
//


using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for FindEventCollection.
	/// </summary>
	public class FindEventCollection : EnvironmentCollectionBase
	{
		public FindEventCollection()
		{
		}

		#region Methods
		/// <summary>
		/// Get or set the object at specified index.
		/// </summary>
		public FindEvent this[int index]
		{
			get 
			{ 
				if (Count > index)
					return (FindEvent) List[index]; 
				else
					return null;
			}
			set { List[index] = value; }
		}

		/// <summary>
		/// Add object to collection.
		/// </summary>
		/// <param name="obj">FindEvent object.</param>
		/// <returns>Index of the newly added object.</returns>
		public int Add(FindEvent obj) 
		{
			return List.Add(obj);
		}

		/// <summary>
		/// Remove object from collection.
		/// </summary>
		/// <param name="obj">FindEvent object.</param>
		public void Remove(FindEvent obj) 
		{
			List.Remove(obj);
		}

		/// <summary>
		/// Check if object is in collection.
		/// </summary>
		/// <param name="obj">FindEvent object</param>
		/// <returns>True if object is in collection, else false.</returns>
		public bool Contains(FindEvent obj) 
		{
			return List.Contains(obj);
		}

		/// <summary>
		/// Get the index associated with the object in collection.
		/// </summary>
		/// <param name="obj">FindEvent object.</param>
		/// <returns>The index of the object.</returns>
		public int IndexOf(FindEvent obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>
		/// Insert object into collection at the specified index.
		/// </summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">FindEvent object.</param>
		public void Insert(int index, FindEvent obj) 
		{
			List.Insert(index, obj);
		}

		public static FindEventCollection FindEvent(string FindEventName, string city, string countryCode, string subDivisionCode, int partnerID) {
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			FindEventCollection fevCol = dbo.FindEvent(FindEventName, city, countryCode, subDivisionCode, partnerID);
			return fevCol;
		}

		public static FindEventCollection FindEvent(string FindEventName, string countryCode, string subDivisionCode, int partnerID)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			FindEventCollection fevCol = dbo.FindEvent(FindEventName, countryCode, subDivisionCode, partnerID);
			return fevCol;
		}

		public static FindEventCollection SuggestEvent(string FindEventName, string countryCode, string subDivisionCode, int partnerID)
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			FindEventCollection fevCol = dbo.SuggestEvent(FindEventName, countryCode, subDivisionCode, partnerID);
			return fevCol;
		}

		public void Sort()
		{
			try 
			{
				this.InnerList.Sort();
			}
			catch (Exception ex) {Console.WriteLine(ex.ToString());}
		}
		#endregion
	}
}
