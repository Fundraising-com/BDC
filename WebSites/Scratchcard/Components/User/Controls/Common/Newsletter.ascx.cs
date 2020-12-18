using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using efundraising.ScratchcardWeb;
using GA.BDC.Core.Database.Scratchcard.DataAccess;

namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common
{

	/// <summary>
	///		Summary description for Newsletter.
	/// </summary>
	public class Newsletter : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.RegularExpressionValidator EmailValidator;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl btnSubmit;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// GLOBALIZATION
			GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage gbp =
				(GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage)this.Page;
			gbp.Globalize(PagePanelControl1, this);
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
			this.btnSubmit.Click += new TrackingButtonEventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			// Insert newsletter
			NewsletterSub n = new NewsletterSub(txtName.Text, txtEmail.Text);
			n.InsertDatabase();


			Response.Redirect("~/NewsletterConfirmation.aspx");	
		}

	}
}
