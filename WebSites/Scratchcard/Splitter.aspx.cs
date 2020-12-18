using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GA.BDC.Core.efundraisingCore;

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for Splitter.
	/// </summary>
	public class Splitter : ScratchcardWebBase
	{
		protected System.Web.UI.WebControls.Literal MetaContentLiteral;
		protected System.Web.UI.WebControls.Image Image1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl6;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl7;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl8;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl9;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Partner partner = Partner.Create(Session);
			Globalize(PagePanelControl1);
			
			
			
			// Do AB Splitting only for web browsers, non robots clients
			if (Regex.Match(Request.UserAgent, @"^mozilla/\d\.\d", RegexOptions.IgnoreCase).Success)
			{
				if (partner.PartnerID == 500)		
					Response.Redirect("http://absplitting.efundraising.com/spliting.aspx?page_code=schome");
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
