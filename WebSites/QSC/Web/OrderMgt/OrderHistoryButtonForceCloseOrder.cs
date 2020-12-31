using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using Common;

namespace QSPFulfillment.OrderMgt
{
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:OrderHistoryButtonForceCloseOrder runat=server></{0}:OrderHistoryButtonForceCloseOrder>")]
	public class OrderHistoryButtonForceCloseOrder : OrderHistoryButton
	{
		private const string BUTTON_TEXT = "Close";

		public event System.EventHandler OrderClosed;

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

				if(oBatch.CloseOrder(this.OrderID)) 
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
