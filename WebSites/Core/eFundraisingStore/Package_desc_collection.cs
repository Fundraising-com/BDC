using System;

namespace GA.BDC.Core.eFundraisingStore
{
	/// <summary>
	/// Summary description for Package_desc_collection.
	/// </summary>
	public class PackageDescCollection : eFundraisingStoreCollectionBase
	{
		public PackageDescCollection()
		{
		
		}
	
		#region public Methods
		public void LoadAllPackageDescs() 
		{
			List.Clear();

			PackageDesc[] packageDescs = PackageDesc.GetPackageDescs();
			foreach(PackageDesc packageDesc in packageDescs) 
			{
				List.Add(packageDesc);
			}
		}

		#endregion
		#region Comparable Methods
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() 
		{
			// sort the collection
			SortProcess();
		}

		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) 
		{
			PackageDescCollection copy =
				(PackageDescCollection)eFundraisingStoreCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}
		
		#endregion
       
		#region Operators
		public static PackageDescCollection operator +(PackageDescCollection collection1, PackageDescCollection collection2) 
		{
			return (PackageDescCollection)eFundraisingStoreCollectionBase.AddCollection(collection1, collection2);
		}

		public static PackageDescCollection operator +(PackageDescCollection collection, PackageDesc item) 
		{
			return (PackageDescCollection)eFundraisingStoreCollectionBase.AddItem(collection, item);
		}

		public static PackageDescCollection operator -(PackageDescCollection collection1, PackageDescCollection collection2) 
		{
			return (PackageDescCollection)eFundraisingStoreCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static PackageDescCollection operator -(PackageDescCollection collection, Package item) 
		{
			return (PackageDescCollection)eFundraisingStoreCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}

	

