using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Menu.Left
{


	/// <summary>
	///		Summary description for LeftMenu.
	/// </summary>
	public class LeftMenu : System.Web.UI.UserControl
	{
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl5;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl6;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl8;
		protected GA.BDC.Core.Web.UI.UIControls.ButtonPanelControl ButtonPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;

		private void Page_Load(object sender, System.EventArgs e)
		{
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ButtonPanelControl2_Click(object sender, System.EventArgs e)
		{
			string url = System.Configuration.ConfigurationSettings.AppSettings["efundraisingStoreURL"];
			string redirect = url;

			//change url if canada
			GA.BDC.Core.efundraisingCore.eFundEnv oEnv = GA.BDC.Core.efundraisingCore.eFundEnv.Create();
			if (oEnv.CultureName == "en-CA")
			{
               url = "SampleKit.aspx?tc=imgTOPSampleKit";
			}

			Response.Redirect(url);


		}
	}
}
