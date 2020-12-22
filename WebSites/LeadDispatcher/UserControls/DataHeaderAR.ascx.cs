namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for DataHeader.
	/// </summary>
	public partial class DataHeaderAR : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            Classes.ClientInfo ci = (Classes.ClientInfo) Session[Global.SessionVariables.CLIENT_INFO];
			Classes.SaleInfo si = (Classes.SaleInfo) Session[Global.SessionVariables.SALE_INFO];

			if (si != null) {
				lblSalesID.Text = si.SALE_ID.ToString();
				lblOrg.Text = ci.ORGANIZATION;
				lblPerson.Text = ci.FIRST_NAME + " " + ci.LAST_NAME;
				lblClientNo.Text = ci.CLIENT_NO;
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
