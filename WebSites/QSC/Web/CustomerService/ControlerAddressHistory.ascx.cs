namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for ControlerAddressHistory.
	/// </summary>
	public partial class ControlerAddressHistory :  CustomerServiceControl
	{
		
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

		public void DataBind(AddressType Type)
		{
			if(Type == AddressType.BillTo)
			{
				LoadData();
				
			}
			else if(Type == AddressType.ShipTo)
			{
				LoadDataShipTo();
			}

			this.dtlMain.DataSource = DataSource;
			
			this.dtlMain.DataBind();
			base.DataBind ();

		}
		public void LoadData()
		{
			try
			{
				DataSource = new DataTable("Address");
				this.Page.BusCustomerOrderHeader.SelectBillToAddressHistory(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
			
		}
		public void LoadDataShipTo()
		{
			try
			{
				DataSource = new DataTable("Address");
				this.Page.BusCustomerOrderHeader.SelectShipToAddressHistory(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
			
		}

	}
}
