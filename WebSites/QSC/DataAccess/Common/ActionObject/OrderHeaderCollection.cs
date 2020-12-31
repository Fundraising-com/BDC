using System;
using System.Collections;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for MagazineItemCollection.
	/// </summary>
	[Serializable]
	public class OrderHeaderCollection : CollectionBase
	{
		private Batch parentBatch = null;

		public Batch ParentBatch 
		{
			get 
			{
				return parentBatch;
			}
			set 
			{
				parentBatch = value;
			}
		}

		public void Add(OrderHeader item) 
		{
			item.ParentCollection = this;
			item.CollectionID = this.Count;
			this.List.Add(item);
		}

		public bool Contains(OrderHeader item) 
		{
			return this.List.Contains(item);
		}

		public int IndexOf(OrderHeader item) 
		{
			return this.List.IndexOf(item);
		}

		public void Remove(OrderHeader item) 
		{
			item.ParentCollection = null;
			this.List.Remove(item);
		}

		public int GetTotalQuantityofProducts()
		{
			int quantity = 0;
			foreach(OrderHeader item in this.List)
			{
				quantity += item.ProductItems.GetTotalQuantityofProducts();
			}
			return quantity;
		}

		public OrderHeader this[int index] 
		{
			get 
			{
				return (OrderHeader) this.List[index];
			}
			set 
			{
				this.List[index] = value;
			}
		}
	}
}
