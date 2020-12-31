namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for ControlerShipementDetail.
	/// </summary>
	public class ControlerOrderItemsTotal : CustomerServiceControlDataGrid
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected float iTotal =0f;
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
			base.OnInit(e,dtgMain,lblMessage);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dtgMain.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgMain_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		protected override void LoadData()
		{
			DataSource = new DataTable("OrderTotlal");
			
			this.Page.BusCustomerOrderHeader.SelectOrderTotals(DataSource,this.Page.OrderInfo.OrderID);

		}

		private void dtgMain_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				iTotal += Convert.ToSingle(((Label)e.Item.FindControl("lblTotalAmount")).Text);
			}
		}
	}
}
