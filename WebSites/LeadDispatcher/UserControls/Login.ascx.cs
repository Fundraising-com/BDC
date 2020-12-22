namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Login.
	/// </summary>
	public partial class Login : System.Web.UI.UserControl
	{
		public System.Web.UI.WebControls.ImageButton cmdLogin;
		public System.Web.UI.WebControls.Label lblError;
		public System.Web.UI.WebControls.TextBox txtUserName;
		public System.Web.UI.WebControls.TextBox txtPassword;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

		/*	public System.Web.UI.WebControls.ImageButton cmdLogin;
		public System.Web.UI.WebControls.Label lblError;
		public System.Web.UI.WebControls.TextBox txtUserName;
		public System.Web.UI.WebControls.TextBox txtPassword;
		*/
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

		protected void txtUserName_TextChanged(object sender, System.EventArgs e) {
		
		}

		

		

	

		
	}
}
