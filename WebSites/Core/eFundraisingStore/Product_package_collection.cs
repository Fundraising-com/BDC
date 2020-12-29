using System;

namespace GA.BDC.Core.eFundraisingStore
{
	/*
	 * Object represents a collection of Products Packages.  
	* 
	 */
	
	public class ProductPackageCollection : eFundraisingStoreCollectionBase
	{
		public ProductPackageCollection() 
		{
			
		}

		#region Methods
		public void LoadAllProductPackages() 
		{
			List.Clear();

			ProductPackage[] productPackages = ProductPackage.GetProductPackages();
			foreach(ProductPackage productPackage in productPackages) 
			{
				List.Add(productPackage);
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
			ProductPackageCollection copy =
				(ProductPackageCollection)eFundraisingStoreCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}
		
		#endregion
       
		#region Operators
		public static ProductPackageCollection operator +(ProductPackageCollection collection1, ProductPackageCollection collection2) 
		{
			return (ProductPackageCollection)eFundraisingStoreCollectionBase.AddCollection(collection1, collection2);
		}

		public static ProductPackageCollection operator +(ProductPackageCollection collection, Product item) 
		{
			return (ProductPackageCollection)eFundraisingStoreCollectionBase.AddItem(collection, item);
		}

		public static ProductPackageCollection operator -(ProductPackageCollection collection1, ProductPackageCollection collection2) 
		{
			return (ProductPackageCollection)eFundraisingStoreCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static ProductPackageCollection operator -(ProductPackageCollection collection, ProductPackage item) 
		{
			return (ProductPackageCollection)eFundraisingStoreCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}