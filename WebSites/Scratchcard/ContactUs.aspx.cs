using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for ContactUs.
	/// </summary>
	public class ContactUs : ScratchcardWebBase
	{
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected System.Web.UI.WebControls.Image Image1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected System.Web.UI.WebControls.Image Image2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl6;
		protected System.Web.UI.WebControls.Image Image3;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl7;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl8;
		protected System.Web.UI.WebControls.Image Image4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl9;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl10;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl11;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScratchcardOmnitureTracking.SetPageNameAndCategory("Public", "Contact Us");
			Globalize(PagePanelControl1);
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
