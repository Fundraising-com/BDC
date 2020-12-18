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

namespace efundraising.RecaudarFondosWeb
{
	/// <summary>
	/// Summary description for ContactUs.
	/// </summary>
	public class ContactUs : RecaudarFondosWebPage
	{
		protected System.Web.UI.HtmlControls.HtmlImage Img1;
		protected System.Web.UI.HtmlControls.HtmlImage Img2;
		protected System.Web.UI.HtmlControls.HtmlImage Img3;
		protected System.Web.UI.HtmlControls.HtmlImage Img4;
		protected System.Web.UI.HtmlControls.HtmlImage Img5;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			recaudarFondosOmnitureTracking.SetPageNameAndCategory("Public", "Contact Us");
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
