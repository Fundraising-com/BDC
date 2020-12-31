namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///		Summary description for cancelsub.
	/// </summary>
	public partial class CancelSub : CustomerServiceActionControl
	{
		protected const string MSG_HEADER = "Cancel subscription";
		protected ControlerConfirmationPage ctrlControlerConfirmationPage;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			/* TEMP - WARNING */
			if(this.Page.UserID == "463") 
			{
				if(ViewState["ConfirmationNumber"] == null)
					ViewState["ConfirmationNumber"] = 0;
			}
			
			if(!IsPostBack)
			{
			
				if(GetCancelAction() == QSPFulfillment.DataAccess.Business.Action.CancelSubBeforeRemit)
				{
                    Response.Redirect("default.aspx?Action=" + ((int)QSPFulfillment.DataAccess.Business.Action.CancelSubBeforeRemit).ToString());
				}
			}

			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ctrlControlerConfirmationPage.Confirmed += new ConfirmEventHandler(ctrlControlerConfirmationPage_Confirmed);
		}
		#endregion

		
		
		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
		protected override void DoAction()
		{
			/* TEMP - WARNING */
			if(this.Page.UserID == "463" && Convert.ToInt32(ViewState["ConfirmationNumber"]) == 0) 
			{
				this.ctrlControlerConfirmationPage.Message = "Are you sure you wish to cancel this subscription?";
				this.ctrlControlerConfirmationPage.ShowConfirmationWindow();

				ViewState["ConfirmationNumber"] = Convert.ToInt32(ViewState["ConfirmationNumber"]) + 1;
			} 
			else 
			{
				CancelSubscription Cancel = new CancelSubscription(this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID,this.Page.UserID);
		
				this.Page.BusCustomerOrderDetail.CancelSub(Cancel);
			}
		}

		private QSPFulfillment.DataAccess.Business.Action GetCancelAction()
		{
			return this.Page.BusCustomerOrderDetail.GetCancelAction(this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID); 
		}

		/* TEMP - WARNING */
		private void ctrlControlerConfirmationPage_Confirmed(object sender, EventArgs e)
		{
			if(this.Page.UserID == "463") 
			{
				if(Convert.ToInt32(ViewState["ConfirmationNumber"]) == 1)
				{
					this.ctrlControlerConfirmationPage.Message = "Mario, are you REALLY sure you wish to cancel this subscription?";
					this.ctrlControlerConfirmationPage.ShowConfirmationWindow();
				} 
				else if(Convert.ToInt32(ViewState["ConfirmationNumber"]) == 2) 
				{
					this.ctrlControlerConfirmationPage.Message = "Mario, this is not a change of address, are you REALLY REALLY sure you wish to cancel this subscription?";
					this.ctrlControlerConfirmationPage.ShowConfirmationWindow();
				} 
				else 
				{
					CancelSubscription Cancel = new CancelSubscription(this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID,this.Page.UserID);
		
					this.Page.BusCustomerOrderDetail.CancelSub(Cancel);
					this.AddScriptRelaodClose();
				}

				ViewState["ConfirmationNumber"] = Convert.ToInt32(ViewState["ConfirmationNumber"]) + 1;
			}
		}
	}
}
