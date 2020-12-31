namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for MainPaymentInfo.
	/// </summary>
	public partial class MainPaymentInfo :CustomerServiceControl
	{

		protected ControlerSubscriptionForCOHI ctrlControlerSubscriptionForCOHI;
		protected ControlerPaymentInfo ctrlControlerPaymentInfo;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			

		}

		public override void DataBind()
		{
			
				ctrlControlerPaymentInfo.DataBind();
				if(ctrlControlerPaymentInfo.CreditCardInfo.PaymentMethodID != PaymentMethod.CheckCash && this.Page.ResultType == SearchMultiPage.CreditCard)
				{
					this.tblCreditCard.Visible = true;
					ctrlControlerSubscriptionForCOHI.CreditCardBounced = true;
					ctrlControlerSubscriptionForCOHI.DataBind();
				}
				else
				{
					this.tblCreditCard.Visible = false;
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
		
		
		
	}
}
