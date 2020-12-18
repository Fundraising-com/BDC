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
	///		Summary description for SuccessStory.
	/// </summary>
	public class SuccessStory : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblStory;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// GLOBALIZATION
			GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage gbp =
				(GA.BDC.Core.Web.UI.UIControls.GlobalizerBasePage)this.Page;
			gbp.Globalize(PagePanelControl1, this);
			
			
			// get a random success story from the database
			Story successStory = new Story(1, 1);
			lblStory.Text = successStory.StoryString;
			
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
