namespace QSPFulfillment
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for login.
	/// </summary>
	public partial class login : System.Web.UI.UserControl
	{
		public event EventHandler Click;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.Page.RegisterStartupScript("focus", "<script>document.getElementById('" + this.User.ClientID + "').focus();</script>");
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			if(Click != null)
				Click(this,new EventArgs());
		}
		public string UserID
		{
			get
			{
				return this.User.Text;
			}
		}
		public string Password
		{
			get
			{
				return this.Pass.Text;
			}
		}
	}
}
