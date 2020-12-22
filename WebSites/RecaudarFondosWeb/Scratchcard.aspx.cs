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
using GA.BDC.Core.efundraisingCore;

namespace efundraising.RecaudarFondosWeb
{
	/// <summary>
	/// Summary description for Scratchcard.
	/// </summary>
	public class Scratchcard : RecaudarFondosWebPage
	{
		protected System.Web.UI.WebControls.PlaceHolder MagFunTop = new PlaceHolder();
		protected System.Web.UI.WebControls.PlaceHolder EfunBottom = new PlaceHolder();
		protected System.Web.UI.WebControls.PlaceHolder MagBottom = new PlaceHolder();

		private void Page_Load(object sender, System.EventArgs e)
		{
			recaudarFondosOmnitureTracking.SetPageNameAndCategory("Public", "Scratchcard");

			Components.User.Partner.MagfunTop top = Page.LoadControl("Components/User/Partner/MagfunTop.ascx") as Components.User.Partner.MagfunTop;
			MagFunTop.Controls.Add(top);

			Components.User.Partner.EfunBottom efun = Page.LoadControl("Components/User/Partner/EfunBottom.ascx") as Components.User.Partner.EfunBottom;
			EfunBottom.Controls.Add(efun);

			Components.User.Partner.MagfunBottom magfun = Page.LoadControl("Components/User/Partner/MagfunBottom.ascx") as Components.User.Partner.MagfunBottom;
			MagBottom.Controls.Add(magfun);
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
