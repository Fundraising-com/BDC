using System;

namespace GA.BDC.Core.Utilities.Javascript
{
	/// <summary>
	/// Summary description for ASP_Utilities.
	/// </summary>
	public class ASP_Utilities
	{
		private ASP_Utilities()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void RestoreFocus(System.Web.UI.Page page) {
			page.RegisterStartupScript("restoreFocus", "<script>window.__smartNav.restoreFocus = function() {scroll(0,0);}</script>");
		}
	}
}
