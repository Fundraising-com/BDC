using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using Common;

namespace QSPFulfillment.OrderMgt
{
	public enum BatchStatus 
	{
		New = 40001,
		InProcess = 40002,
		UnderReview = 40003,
		Approved = 40004,
		Cancelled = 40005,
		CCPending = 40006,
		Housekeeping = 40007,
		HousekeepingC = 40008,
		Pickable = 40009,
		AtWarehouse = 40010,
		Shipped = 40011,
		SentToTPL = 40012,
		Fulfilled = 40013,
		PartiallyFulfilled = 40014,
		MagnetLoaded = 40015,
		PendingReview = 40016
	}

	public class OrderHistoryButton : System.Web.UI.WebControls.Button
	{
		protected DataTable dataSource;

		public int OrderID 
		{
			get 
			{
				int orderID = 0;

				try 
				{
					orderID = Convert.ToInt32(this.ViewState["OrderID"]);
				} 
				catch { }

				return orderID;
			}
			set 
			{
				this.ViewState["OrderID"] = value;
			}
		}

		public DataTable DataSource 
		{
			get 
			{
				return dataSource;
			}
			set 
			{
				dataSource = value;
			}
		}
	}
}
