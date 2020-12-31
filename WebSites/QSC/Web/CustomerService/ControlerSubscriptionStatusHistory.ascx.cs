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
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for ControlerSubscriptionStatusHistory.
	/// </summary>
	public class ControlerSubscriptionStatusHistory : CustomerServiceControlDataGrid
	{
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected System.Web.UI.WebControls.Label lblOESentTitle;
		protected System.Web.UI.WebControls.Label lblOESent;
		protected System.Web.UI.WebControls.Label lblOEDateTitle;
		protected System.Web.UI.WebControls.Label lblOEDate;
		protected System.Web.UI.HtmlControls.HtmlTableRow trOEDate;

		protected Label lblMessage;

		DataTable orderEntryFollowupReportTable = new DataTable();

		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,this.dtgMain,lblMessage);
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

		protected override void LoadData()
		{
			try 
			{
				DataSource = new DataTable(CustomerOrderDetailRemitHistoryTable.TBL_CUSTOMERORDERDETAILREMITHISTORY);
				this.Page.BusCustomerOrderDetailRemitHistory.SelectByCOHInstance(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);

				this.Page.BusReportRequestBatch_OrderEntryFollowupReport.SelectOne(orderEntryFollowupReportTable, this.Page.OrderInfo.CustomerOrderHeaderInstance);

				SetValue();
			} 
			catch(Exception ex)
			{
				if(!(ex is ExceptionFulf)) 
				{
					ApplicationError.ManageError(ex);
				}

				this.Page.SetPageError(new ExceptionFulf(Common.Message.ERRMSG_SYSTEM_VAR_0));
			}
		}

		private void SetValue() 
		{
			if(orderEntryFollowupReportTable.Rows.Count == 1) 
			{
				this.lblOESent.Text = "Yes";
				this.lblOEDate.Text = orderEntryFollowupReportTable.Rows[0]["DatePrinted"].ToString();

				trOEDate.Visible = true;
			} 
			else 
			{
				this.lblOESent.Text = "No";

				trOEDate.Visible = false;
			}
		}
	}
}
