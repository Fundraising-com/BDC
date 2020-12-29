using System;

namespace GA.BDC.Core.Utilities.Javascript
{
	/*
	 * Created by:	Jean-Francois Buist.
	 * Date:		Novembre 2004.
	 * Version:		0.0
	 * 
	 */

	/// <summary>
	/// When you generate a form with textboxes, combo boxes, or whatever,
	/// some times you need to validate data on the server side.
	/// This class creates hrefs that points the the incorect control.
	/// </summary>
	public class ValidationAnchor : JavaScript {
		public ValidationAnchor() {
			
		}

		public static string SetFocusFunction() {
			return	"function SetFocusToControl(control) {\n" +
				"	return control.focus();\n" +
				"}\n";
		}

		public static string GenerateLinkToControlFocus(System.Web.UI.Page page, System.Web.UI.Control control, string text, string cssClass, bool isUserControl) {
			string controlName = page.ToString().Replace("ASP.", "").Replace("_aspx", "");
			if(isUserControl) {
				controlName += "_" + control.ID;
			} else {
				controlName = control.ID;
			}

			return GenerateLinkToControlFocus(controlName, text, cssClass);
		}

		public static string GenerateLinkToControlFocus(string controlName, string linkName, string cssClass) {
			return "<a class=\"" + cssClass + "\" onclick=\"javascript:SetFocusToControl(" + controlName + ")\" href=\"#\">" + linkName + "</a><br>";
		}

		
	}
}
