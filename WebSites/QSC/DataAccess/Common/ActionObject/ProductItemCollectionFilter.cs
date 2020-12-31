using System.Collections;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	public class ProductItemCollectionFilter
	{
		public ProductItemCollectionFilter(){}

		public ProductItemCollection Filter(ProductItemCollection collection, int isDeletedValue) 
		{ 
			ProductItemCollection FilteredCollection = new ProductItemCollection(); 

			foreach (ProductItem item in collection) 
			{ 
				if (item.IsDeleted != isDeletedValue) 
				{ 
					FilteredCollection.Add(item); 
				} 
			} 
			return FilteredCollection; 
		}
	}    
}