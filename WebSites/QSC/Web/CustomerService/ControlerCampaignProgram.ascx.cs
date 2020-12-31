namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;

	/// <summary>
	///		Summary description for ControlerCampaignProgram.
	/// </summary>
	public class ControlerCampaignProgram :  CustomerServiceControlDataGrid
	{
		protected DataGridObject dtgMain;
		protected Label lblMessage;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.Message = "";
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,this.dtgMain,this.lblMessage);
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
			
			DataSource = new DataTable(CampaignProgramTable.TBL_CAMPAIGNPROGRAM);
			this.Page.BusCampaignProgram.SelectByCampaignID(DataSource,this.Page.OrderInfo.CampaignID);
			

		}

	}
}
