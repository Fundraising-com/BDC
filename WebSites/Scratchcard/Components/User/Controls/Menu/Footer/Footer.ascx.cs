namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Menu.Footer
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Footer.
	/// </summary>
	public class Footer : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected System.Web.UI.WebControls.Label Label4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl7;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl8;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl9;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl11;
		protected System.Web.UI.WebControls.Label Label1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl12;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl10;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// GLOBALIZATION
			GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage gbp =
				(GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage)this.Page;

			GA.BDC.Core.efundraisingCore.Partner partner = GA.BDC.Core.efundraisingCore.Partner.Create(Session);
			gbp.DynTags["PartnerPath"] = partner.PartnerPath + "test";

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
