using System;

namespace GA.BDC.Core.eFundraisingStore 
{
	
	/*
	 * Object represents a collection of Products.  
	 * One client can have multiple Products.
	 * 
	 */
	public class ProductCollection : eFundraisingStoreCollectionBase
	{
		public ProductCollection() 
		{
			
		}

		#region Methods
		public void LoadAllProducts() {
	        List.Clear();

			Product[] products = Product.GetProducts();
			foreach(Product product in products) {
                List.Add(product);
			}
		}

	

		/*public void LoadProductsByPackageID(int id) 
		{
			List.Clear();

			Product[] products = Product.GetProductsByPackageID(id);
			foreach(Product product in products) 
			{
				List.Add(product);
			}
		}*/

		/* public void LoadProductsByDescription(string description) 
		 {
			 List.Clear();

			 Product[] products = Product.GetProductsByPackageID(id);
			 foreach(Product product in products) 
			 {
				 List.Add(product);
			 }
		
		 }*/

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
			ProductCollection copy =
				(ProductCollection)eFundraisingStoreCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}
		
		#endregion
       
		#region Operators
		public static ProductCollection operator +(ProductCollection collection1, ProductCollection collection2) 
		{
			return (ProductCollection)eFundraisingStoreCollectionBase.AddCollection(collection1, collection2);
		}

		public static ProductCollection operator +(ProductCollection collection, Product item) 
		{
			return (ProductCollection)eFundraisingStoreCollectionBase.AddItem(collection, item);
		}

		public static ProductCollection operator -(ProductCollection collection1, ProductCollection collection2) 
		{
			return (ProductCollection)eFundraisingStoreCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static ProductCollection operator -(ProductCollection collection, Product item) 
		{
			return (ProductCollection)eFundraisingStoreCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}
