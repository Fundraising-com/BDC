using System;
using System.Collections;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for MagazineItemCollection.
	/// </summary>
	[Serializable]
	public class ProductItemCollection : CollectionBase
	{
		private OrderHeader parentOrderHeader;

		public OrderHeader ParentOrderHeader 
		{
			get 
			{
				return parentOrderHeader;
			}
			set 
			{
				parentOrderHeader = value;
			}
		}

		public void Add(ProductItem item) 
		{
			item.ParentCollection = this;
			this.List.Add(item);
		}

		public void Add(ProductItem item, int isDeletedValue) 
		{
			item.ParentCollection = this;
			item.IsDeleted = isDeletedValue;
			this.List.Add(item);
		}
		
		public bool Contains(ProductItem item) 
		{
			return this.List.Contains(item);
		}

		public void Remove(ProductItem item) 
		{
			item.ParentCollection = null;
			this.List.Remove(item);
		}

		public int GetTotalQuantityofProducts()
		{
			int quantity = 0;
			foreach(ProductItem item in this.List)
			{
                quantity += item.Quantity;
			}
			return quantity;
		}

		public int IndexOf(ProductItem item) 
		{
			return this.List.IndexOf(item);
		}

		public void Update(int index, ProductItem item) 
		{
			this.List[index] = item;
		}			

		public ProductItem this[int index] 
		{
			get 
			{
				return (ProductItem) this.List[index];
			}
		}
	}
}
