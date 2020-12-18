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
	///		Summary description for Admin_All_Accounts.
	/// </summary>
	public partial class Admin_All_Accounts : System.Web.UI.UserControl
	{

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
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);

		}
		#endregion

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			if (Helper.IsNumeric(txtLeadID.Text)){
				AllAccountsForLead acc = new AllAccountsForLead();
			    acc.GenerateControl(Convert.ToInt32(txtLeadID.Text));
			}
			

		}
	}
}
