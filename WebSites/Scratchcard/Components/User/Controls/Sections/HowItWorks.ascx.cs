namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Sections
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for HowItWorks.
	/// </summary>
	public class HowItWorks : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Image Image1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected System.Web.UI.WebControls.Image Image2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl6;
		protected System.Web.UI.WebControls.Image Image3;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;

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
			this.ButtonPanelControl2.Click += new TrackingButtonEventHandler(this.ButtonPanelControl2_Click);
			this.ButtonPanelControl1.Click += new TrackingButtonEventHandler(this.ButtonPanelControl1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ButtonPanelControl1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("SampleKit.aspx");
		}

		private void ButtonPanelControl2_Click(object sender, System.EventArgs e)
		{
		   string url = System.Configuration.ConfigurationSettings.AppSettings["efundraisingStoreURL"];
		   Response.Redirect(url);
		}
	}
}
