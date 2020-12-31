using System;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Batch.
	/// </summary>
	[Serializable]
	public class Batch
	{
		private OrderHeaderCollection orderHeaders = null;
		private Campaign campaign = null;
		private int orderID = 0;
		private string date;
		private int id = 0;
		private OrderQualifier orderQualifierID = OrderQualifier.None;
		private DateTime orderDeliveryDate = DateTime.Now;
		private string comment = String.Empty;
		private int status = 40001;

		public OrderHeaderCollection OrderHeaders 
		{
			get 
			{
				if(orderHeaders == null) 
				{
					orderHeaders = new OrderHeaderCollection();
					orderHeaders.ParentBatch = this;
				}

				return orderHeaders;
			}
		}

		public Campaign Campaign 
		{
			get 
			{
				return campaign;
			}
			set 
			{
				campaign = value;
			}
		}
		public string Date 
		{
			get 
			{
				return date;
			}
			set 
			{
				date = value;
			}
		}
		public int ID 
		{
			get 
			{
				return id;
			}
			set 
			{
				id = value;
			}
		}
		public int OrderID 
		{
			get 
			{
				return orderID;
			}
			set 
			{
				orderID = value;
			}
		}

		public OrderQualifier OrderQualifierID 
		{
			get 
			{
				return orderQualifierID;
			}
			set 
			{
				orderQualifierID = value;
			}
		}

		public DateTime OrderDeliveryDate 
		{
			get 
			{
				return orderDeliveryDate;
			}
			set 
			{
				orderDeliveryDate = value;
			}
		}
		
		public string Comment 
		{
			get 
			{
				return comment;
			}
			set 
			{
				comment = value;
			}
		}
		public int Status 
		{
			get 
			{
				return status;
			}
			set 
			{
				status = value;
			}
		}

	}
}
