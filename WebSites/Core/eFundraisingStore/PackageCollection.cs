using System;

namespace GA.BDC.Core.eFundraisingStore 
{
	/*
	 * Object represents a collection of Packages.  
	 * One client can have multiple Packages.
	 * 
	 */
	public class PackageCollection : eFundraisingStoreCollectionBase
	{

		public PackageCollection() 
		{
	
		}

		#region public Methods
		public void LoadAllPackages() 
		{
			List.Clear();

			Package[] packages = Package.GetPackages();
			foreach(Package package in packages) 
			{
				List.Add(package);
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
			PackageCollection copy =
				(PackageCollection)eFundraisingStoreCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}
		
		#endregion
       
		#region Operators
		public static PackageCollection operator +(PackageCollection collection1, PackageCollection collection2) 
		{
			return (PackageCollection)eFundraisingStoreCollectionBase.AddCollection(collection1, collection2);
		}

		public static PackageCollection operator +(PackageCollection collection, Package item) 
		{
			return (PackageCollection)eFundraisingStoreCollectionBase.AddItem(collection, item);
		}

		public static PackageCollection operator -(PackageCollection collection1, PackageCollection collection2) 
		{
			return (PackageCollection)eFundraisingStoreCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static PackageCollection operator -(PackageCollection collection, Package item) 
		{
			return (PackageCollection)eFundraisingStoreCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}
