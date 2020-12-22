using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace GA.BDC.WEB.ScratchcardWeb.MasterPage
{
	/// <summary>
	///		Summary description for SiteTemplate1.
	/// </summary>
	public class SiteTemplate1 : System.Web.UI.UserControl
	{
		protected GA.BDC.Core.Web.UI.MasterPages.ContentPlaceHolder cph_PageContent;
		protected GA.BDC.Core.Web.UI.MasterPages.ContentPlaceHolder cph_CornerImage;
		protected System.Web.UI.WebControls.Image Image2;
		protected GA.BDC.Core.Web.UI.MasterPages.ContentPlaceHolder cph_DidYouKnow;
		protected System.Web.UI.WebControls.Literal MetaContentLiteral;
		protected System.Web.UI.WebControls.Image eifijief;
		protected System.Web.UI.WebControls.Image hefholclkjdf;
		protected System.Web.UI.WebControls.Literal TitleLiteral;

		private void Page_Load(object sender, System.EventArgs e)
		{
			GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage oGbp =
				(GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage) Page;

			// Set title & meta content. 
			// NOTE: Meta content is already tag enclosed from Globalizer.
			TitleLiteral.Text = Server.HtmlEncode(oGbp.PageTitle);
			MetaContentLiteral.Text = oGbp.MetaContent;
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
