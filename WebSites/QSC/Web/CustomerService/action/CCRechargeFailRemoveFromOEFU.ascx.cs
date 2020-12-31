namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;


	/// <summary>
	///		Summary description for ProofPaymtRequired.
	/// </summary>
    public class CCRechargeFailRemoveFromOEFU : CustomerServiceActionControl
	{
        protected const string MSG_HEADER = "Remove Subscription from OEFU Rejected CC Report and School OEFU report";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
		
		
		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}

        protected override void DoAction()
        {
            this.Page.BusReportRequestBatch_OrderEntryFollowupReport.Requeue(this.Page.OrderInfo.OrderID);
        }
	}
}
