using System;

namespace GA.BDC.Core.eFundraisingStore
{
	/// <summary>
	/// Summary description for Product_desc_collection.
	/// </summary>
	public class ProductDescCollection : eFundraisingStoreCollectionBase
	{
		public ProductDescCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region public Methods
		public void LoadAllProductDescs() 
		{
			List.Clear();

			ProductDesc[] productDescs = ProductDesc.GetProductDescs();
			foreach(ProductDesc productDesc in productDescs) 
			{
				List.Add(productDesc);
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
			ProductDescCollection copy =
				(ProductDescCollection)eFundraisingStoreCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}
		
		#endregion
       
		#region Operators
		public static ProductDescCollection operator +(ProductDescCollection collection1, ProductDescCollection collection2) 
		{
			return (ProductDescCollection)eFundraisingStoreCollectionBase.AddCollection(collection1, collection2);
		}

		public static ProductDescCollection operator +(ProductDescCollection collection, ProductDesc item) 
		{
			return (ProductDescCollection)eFundraisingStoreCollectionBase.AddItem(collection, item);
		}

		public static ProductDescCollection operator -(ProductDescCollection collection1, ProductDescCollection collection2) 
		{
			return (ProductDescCollection)eFundraisingStoreCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static ProductDescCollection operator -(ProductDescCollection collection, Product item) 
		{
			return (ProductDescCollection)eFundraisingStoreCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}
