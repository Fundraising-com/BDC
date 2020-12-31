namespace QSPFulfillment.Fulfillment.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public delegate void ConfirmYesNoEventHandler(object sender, EventArgs e);
	/// <summary>
	///		Confirmation window, which postbacks and sends events both on "Yes"
	///		and "No" answers (based on CustomerService\ControlerConfirmationPage)
	/// </summary>
	public partial class ControlerYesNoConfirmation : System.Web.UI.UserControl
	{
		public event ConfirmYesNoEventHandler ConfirmedYes;
		public event ConfirmYesNoEventHandler ConfirmedNo;

		string strMessage = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			switch (hidConfirmed.Value)
			{
				case "2"://Yes
					hidConfirmed.Value = "0";
					ConfirmedYes(this, EventArgs.Empty);
					break;

				case "1"://No
					hidConfirmed.Value = "0";
					ConfirmedNo(this, EventArgs.Empty);
					break;

				default:
					break;
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
			script += "  window.onload = new Function(\"openErrorWindow('/QSPFulfillment/Fulfillment/showYesNoconfirmationpage.aspx?Message=" + strMessage + "&ParentHidden=" + this.hidConfirmed.ClientID + "');\");\n";
			script += "</script>\n";

			this.Page.RegisterClientScriptBlock("confirmation", script);
		}
	}
}
