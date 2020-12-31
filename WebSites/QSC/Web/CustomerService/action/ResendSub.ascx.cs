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
	///		Summary description for Resendsub.
	/// </summary>
	public partial class ResendSub : CustomerServiceActionControl
	{
		protected const string MSG_HEADER = "Resend subscription";
		protected ControlerConfirmationPage ctrlControlerConfirmationPage;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
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
		}
		#endregion
	
		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}

		protected override void DoAction()
		{
				ResendSubscription Resend = new ResendSubscription(this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID,this.Page.UserID);
				this.Page.BusCustomerOrderDetail.ResendSub(Resend);
		}
	}
}
