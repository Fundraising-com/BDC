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
	/// Base class for all java objects
	/// </summary>
	public abstract class JavaScript {
		public JavaScript() {

		}

		public static string BeginJavascript() {
			return	"<script language=\"Javascript\">\n" +
					"<!-- \n\n";

		}

		public static string EndJavascript() {
			return	"//--> \n" +
					"</script>\n";
		}
		
	}
}
