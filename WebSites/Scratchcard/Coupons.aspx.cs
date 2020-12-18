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
	/// Summary description for Coupons.
	/// </summary>
	public class Coupons : ScratchcardWebBase
	{
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected System.Web.UI.WebControls.Image Image1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl6;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl2;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScratchcardOmnitureTracking.SetPageNameAndCategory("Public", "Coupons");
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
			this.ButtonPanelControl2.Click += new TrackingButtonEventHandler(this.ButtonPanelControl2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ButtonPanelControl2_Click(object sender, System.EventArgs e)
		{
         string url = System.Configuration.ConfigurationSettings.AppSettings["efundraisingStorePartnerURL"];
			Response.Redirect(url);
		}
	}
}
