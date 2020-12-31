namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for SubscriptionInfoSearch.
	/// </summary>
	public class SubscriptionInfoSearch : CustomerServiceControl
	{
		protected System.Web.UI.WebControls.Label lblFromDate;
		protected System.Web.UI.WebControls.Label lbltitle;
		protected System.Web.UI.WebControls.Label lblInfoMissing;
		protected System.Web.UI.WebControls.Label lblToDate;
		protected System.Web.UI.WebControls.Label lblChadd;
		protected System.Web.UI.WebControls.Label lblCancel;
		protected System.Web.UI.WebControls.Label lblSwitch;
		protected System.Web.UI.WebControls.Label lblRemitID;
		protected System.Web.UI.WebControls.Label lblRemitDate;
		protected QSP.WebControl.TextBoxSearch tbxTitleCode;
		protected QSP.WebControl.TextBoxSearch TextBox1;
		protected QSP.WebControl.TextBoxSearch TextBox2;
		protected System.Web.UI.WebControls.Label lblTitleCode;

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
			base.OnInit(e);
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
	}
}
