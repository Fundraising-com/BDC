namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;
    using System.Collections.Generic;

	/// <summary>
	///		Summary description for refundsub.
	/// </summary>
	public partial class CancelCustomerRefund : CustomerServiceActionControl
	{
		/*protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.Label lblSubscription;
		protected QSP.WebControl.Currency Currency1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblCreatedBy;
		protected System.Web.UI.WebControls.Label lblDateCreated;
		protected System.Web.UI.WebControls.Label lblRefundSubscription;*/
		protected const string MSG_HEADER = "Customer Refund";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadData();
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
		}
		#endregion

		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}

        private void LoadData()
		{
            LoadRefunds();
        }

        private void LoadRefunds()
		{
            try
            {
                this.ctrlControlerRefund.DataBindRefundsAvailableToCancel();
            }
            catch (ExceptionFulf ex)
            {
                this.Page.SetPageError(ex);
            }
		}

        protected override void DoAction()
		{
            foreach (int refundID in ctrlControlerRefund.GetSelectedRefundIDs())
            {
                this.Page.BusPayment.CancelCheque(refundID);
            }
		}
	}
}
