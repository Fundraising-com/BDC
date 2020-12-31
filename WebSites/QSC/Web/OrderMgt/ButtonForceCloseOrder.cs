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
		Picked = 40010,
		Shipped = 40011,
		SentToTPL = 40012,
		Fulfilled = 40013,
		PartiallyFulfilled = 40014,
		MagnetLoaded = 40015
	}

	/// <summary>
	/// Summary description for ButtonForceCloseOrder.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:ButtonForceCloseOrder runat=server></{0}:ButtonForceCloseOrder>")]
	public class ButtonForceCloseOrder : System.Web.UI.WebControls.Button
	{
		private const string BUTTON_TEXT = "Close";

		public event System.EventHandler OrderClosed;
		private DataTable dataSource;

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

		public override void DataBind()
		{
			DataRow row;
			int batchStatusInstance;

			try 
			{
				this.Visible = false;

				if(dataSource != null) 
				{
					row = this.DataSource.Select("OrderId = " + this.OrderID.ToString())[0];

					if(row != null) 
					{
						batchStatusInstance = Convert.ToInt32(row["StatusInstance"]);

						if(batchStatusInstance == Convert.ToInt32(BatchStatus.InProcess) ||
							batchStatusInstance == Convert.ToInt32(BatchStatus.UnderReview)) 
						{
							this.Text = BUTTON_TEXT;
							this.Visible = true;
						}
					}
				}

				base.DataBind ();
			} 
			catch(Exception ex) 
			{
				DataAccess.Common.ApplicationError.ManageError(ex);

				throw new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex);
			}
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);

			try 
			{
				Business.Batch oBatch = new Business.Batch();

				if(oBatch.ForceCloseOrder(this.OrderID)) 
				{
					if(OrderClosed != null) 
					{
						OrderClosed(this, null);
					}
				} 
				else
				{
					throw new MessageException(Message.ERRMSG_FORCE_ORDER_CLOSE_0);
				}
			} 
			catch(Exception ex) 
			{
				OrderMgtPage page = this.Page as OrderMgtPage;

				if(page != null) 
				{
					if(ex is MessageException) 
					{
						page.SetPageError((MessageException) ex);
					} 
					else 
					{
						DataAccess.Common.ApplicationError.ManageError(ex);

						page.SetPageError(new MessageException(Message.ERRMSG_SYSTEM_VAR_0, ex));
					}
				} 
				else 
				{
					throw ex;
				}
			}
		}
	}
}
