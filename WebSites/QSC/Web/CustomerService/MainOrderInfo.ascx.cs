namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess;

	/// <summary>
	///		Summary description for OrderInfo.
	/// </summary>
	public partial class MainOrderInfo : CustomerServiceControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected ControlerFM ctrlControlerFM;
		protected ControlerCampaignProgram ctrlControlerCampaignProgram;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label lblTotalItems;
		protected System.Web.UI.WebControls.Label lbltotalAmount;
		protected ControlerInvoice ctrlControlerInvoice;
		protected ControlerAddress ctrlControlerAddressBillToGroup;
		protected ControlerAddress	ctrlControlerAddressShipToGroup;
		protected ControlerOrderItemsTotal ctrlControlerOrderItemsTotal;
		
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
		private void SetInformation()
		{
		
		}
		public override void DataBind()
		{
			ctrlControlerAddressBillToGroup.DataBind(this.Page.OrderInfo.CampaignID,QSPFulfillment.DataAccess.Business.AddressType.BillTo);
			ctrlControlerAddressShipToGroup.DataBind(this.Page.OrderInfo.CampaignID,QSPFulfillment.DataAccess.Business.AddressType.ShipTo);
			SetValue();
			ctrlControlerInvoice.DataBind();
			ctrlControlerCampaignProgram.DataBind();
			ctrlControlerFM.DataBind();
			ctrlControlerOrderItemsTotal.DataBind();

			
		}
		private void SetValue()
		{
			this.lblStatus.Text = this.Page.OrderInfo.Status;
			this.lblQualifierName.Text = this.Page.OrderInfo.QualifierName;
			this.lblCampaignID.Text =this.Page.OrderInfo.CampaignID.ToString();
			
			this.lblStudentName.Text = this.Page.StudentName;
			this.lblCustomerName.Text = this.Page.RecipientName;

			lblAccoutNumber.Text = this.Page.OrderInfo.AccountID.ToString();
			lblOrderID.Text = this.Page.OrderInfo.OrderID.ToString();
		}
	}
}
