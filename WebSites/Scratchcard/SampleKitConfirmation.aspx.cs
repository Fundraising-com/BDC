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
	/// Summary description for SampleKitConfirmation.
	/// </summary>
	public class SampleKitConfirmation : ScratchcardWebBase
	{
		protected GA.BDC.Core.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
		protected GA.BDC.Core.Web.UI.UIControls.PagePanelControl PagePanelControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScratchcardOmnitureTracking.SetPageNameAndCategory("Public", "Free Info Kit Confirmation");
			ScratchcardOmnitureTracking.AddEvent_Custom (5);
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
