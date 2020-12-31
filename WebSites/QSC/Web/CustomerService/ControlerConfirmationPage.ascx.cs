namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public delegate void ConfirmEventHandler(object sender, EventArgs e);
	/// <summary>
	///		Summary description for ControlerConfirmationPage.
	/// </summary>
	public partial class ControlerConfirmationPage : System.Web.UI.UserControl
	{
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidConfirmed;
		public event ConfirmEventHandler Confirmed;

		string strMessage = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(hidConfirmed.Value == "1") 
			{
				hidConfirmed.Value = "0";
				Confirmed(this, EventArgs.Empty);
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		public string Message 
		{
			get 
			{
				return strMessage;
			}
			set 
			{
				strMessage = value;
			}
		}

		public void ShowConfirmationWindow() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  window.onload = new Function(\"openErrorWindow('/QSPFulfillment/customerservice/showconfirmationpage.aspx?Message=" + strMessage + "&ParentHidden=" + this.hidConfirmed.ClientID + "');\");\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("confirmation", script);
		}
	}
}
