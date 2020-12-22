namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using GA.BDC.Core.EnterpriseComponents;

	/// <summary>
	///		Summary description for PaymentAdjusment_Header.
	/// </summary>
	public partial class PaymentAdjusment_Header : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		
			Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];
			if (si != null){	
				lblTotalDue.Text = Helper.FormatCurrency(si.TOTAL_AMOUNT);
				lblAdjustment.Text = Helper.FormatCurrency(si.TOTAL_ADJUSTMENT.ToString());
				lblBalance.Text = Helper.FormatCurrency(si.BALANCE.ToString());
				lblTotalPaid.Text = Helper.FormatCurrency(si.TOTAL_PAID.ToString());
				lblSaleID.Text = si.SALE_ID.ToString();
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
