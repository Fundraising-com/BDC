namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for AddressInfo.
	/// </summary>
	public partial class MainAddressInfo : CustomerServiceControl
	{
		protected ControlerAddress ctrlControlerAddressCustomer;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected ControlerRefund ctrlControlerRefund;
		protected ControlerAddressHistory ctrlControlerAddressHistoryBillTo;
		protected ControlerAddressHistory ctrlControlerAddressHistoryRecipient;
		protected ControlerAddress ctrlControlerAddressRecipient;
		protected void Page_Load(object sender, System.EventArgs e)
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

		}
		#endregion
		
		private void LoadData(int CampaignID)
		{
			ctrlControlerAddressCustomer.DataBind(QSPFulfillment.DataAccess.Business.AddressType.BillTo);
			ctrlControlerAddressHistoryBillTo.DataBind(QSPFulfillment.DataAccess.Business.AddressType.BillTo);
			ctrlControlerRefund.DataBindRefunds();
			ctrlControlerAddressHistoryRecipient.DataBind(QSPFulfillment.DataAccess.Business.AddressType.ShipTo);
			ctrlControlerAddressRecipient.DataBind(QSPFulfillment.DataAccess.Business.AddressType.ShipTo);
		}

        public override void DataBind()
        {
            LoadData(this.Page.OrderInfo.CampaignID);
        }

	}
}
