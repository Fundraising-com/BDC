namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for temp.
	/// </summary>
	public partial class temp : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//if (!IsPostBack){
				// Put user code to initialize the page here
		//		DataTable dt = DatabaseObjects.GetPayments(1000);
			//	dg.DataSource = dt;
			//	dg.DataBind();
			//}
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

		private void dg_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
			int a = 1;
		}

	
	}
}

