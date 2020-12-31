namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.CustomerService;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for PayGroupMaintenanceControl.
	/// </summary>
	public class PayGroupMaintenanceControl : MarketingMgtControlDataGrid
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected DataGridObject dtgMain;
	
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
			base.OnInit(e,dtgMain,lblMessage);
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

		public override void DataBind()
		{
			LoadData();
			Bind();
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("PayGroup");

			this.Page.BusCatalog.SelectAllPayGroupLookUpCodes(DataSource);
		}
	}
}
