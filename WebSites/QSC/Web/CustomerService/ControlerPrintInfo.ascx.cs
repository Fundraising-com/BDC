namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;

	/// <summary>
	///		Summary description for AddressInfo.
	/// </summary>
	public partial class ControlerPrintInfo : CustomerServiceControl
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected ControlerAddress ctrlControlerAddressRecipient;
		protected ControlerSubscriptionStatusHistory ctrlControlerSubscriptionStatusHistory;
		private CustomerOrderDetailRemitHistoryTable Table;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			DataBind();
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
			try
			{
				Table = new CustomerOrderDetailRemitHistoryTable();
				this.DataSource = Table;
				this.Page.BusCustomerOrderDetailRemitHistory.SelectOneLastTransaction(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
			}
	
			catch(QSPFulfillment.DataAccess.Common.ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		public override void DataBind()
		{
			LoadData(this.Page.OrderInfo.CampaignID);

			ctrlControlerAddressRecipient.DataBind(QSPFulfillment.DataAccess.Business.AddressType.ShipTo);
			SetValueElementLastTransaction();
			ctrlControlerSubscriptionStatusHistory.DataBind();
		}

		private void SetValueElementLastTransaction()
		{
			if(DataSource.Rows.Count!=0)
			{
				DataRow row=DataSource.Rows[0];
				lblTitleCode.Text = row[CustomerOrderDetailRemitHistoryTable.FLD_TITLECODE].ToString();
				lblTitleCode.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_TITLECODE].ToString();
				lblMagazineTitle.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_MAGAZINETITLE].ToString();
				lblMagazineStatus.Text= row["MagazineStatus"].ToString();
				lblFulfillmentHouseName.Text= row["Ful_Name"].ToString();
				lblFullContact.Text= row["Ful_Tel"].ToString();
				lblPriceEntered.Text= row["PriceEntered"].ToString();
				lblCatalogPrice.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_CATALOGPRICE].ToString();
				lblPrice.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_BASEPRICE].ToString();
				
			}
	
		}
	
		
		
	}
}
