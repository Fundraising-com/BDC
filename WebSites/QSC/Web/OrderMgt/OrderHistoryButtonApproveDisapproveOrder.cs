using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using Common;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.OrderMgt
{
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:OrderHistoryButtonApproveDisapproveOrder runat=server></{0}:OrderHistoryButtonApproveDisapproveOrder>")]
	public class OrderHistoryButtonApproveDisapproveOrder : OrderHistoryButton
	{
		private const string BUTTON_APPROVE_TEXT = "Approve";
		private const string BUTTON_DISAPPROVE_TEXT = "Cancel";
		
		public event System.EventHandler OrderApproved;
		public event System.EventHandler OrderDisapproved;

		public int ButtonType 
		{
			get 
			{
				int buttonType = 0;

				try 
				{
					buttonType = Convert.ToInt32(this.ViewState["ButtonType"]);
				} 
				catch { }

				return buttonType;
			}
			set 
			{
				this.ViewState["ButtonType"] = value;
			}
		}

		public override void DataBind()
		{
			DataRow row;
			int batchStatusInstance;
         bool orderCancelAllowed;
			//string orderQualifier;

			try 
			{
				this.Visible = false;

				if(dataSource != null) 
				{
					row = this.DataSource.Select("OrderId = " + this.OrderID.ToString())[0];

					if(row != null) 
					{
						batchStatusInstance = Convert.ToInt32(row["StatusInstance"]);
                  orderCancelAllowed = Convert.ToBoolean(row["OrderCancelAllowed"]);
						//orderQualifier = Convert.ToString(row["OrderQualifier"]);

						if (this.ButtonType == 0)
						{
							if(batchStatusInstance == Convert.ToInt32(BatchStatus.PendingReview))
							{
								this.Text = BUTTON_APPROVE_TEXT;
								this.Visible = true;
							}
						}
						else
						{
							//if((batchStatusInstance == Convert.ToInt32(BatchStatus.PendingReview)
							//	|| batchStatusInstance == Convert.ToInt32(BatchStatus.InProcess)))
							if (orderCancelAllowed)
                     {
								this.Text = BUTTON_DISAPPROVE_TEXT;
								this.Visible = true;
							}
						}
					}
				}

				base.DataBind();
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

				if (this.ButtonType == 0)
				{
					if(OrderApproved != null) 
						if(oBatch.OrderApproveDisapprove(this.ButtonType, this.OrderID)) 
						{
							OrderApproved(this, null);
						}
						else
						{
							throw new MessageException(Message.ERRMSG_APPROVE_ORDER_0);
						}
					else
					{
						throw new MessageException(Message.ERRMSG_APPROVE_ORDER_0);
					}
				}
				else
				{
					if(OrderDisapproved != null) 
						if(oBatch.OrderApproveDisapprove(this.ButtonType, this.OrderID)) 
						{
							OrderDisapproved(this, null);
						}
						else
						{
							throw new MessageException(Message.ERRMSG_DISAPPROVE_ORDER_0);
						}
					else
					{
						throw new MessageException(Message.ERRMSG_DISAPPROVE_ORDER_0);
					}
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
