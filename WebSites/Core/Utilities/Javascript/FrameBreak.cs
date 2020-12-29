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
	/// When the page will be called, it will breaks all the frames (eg. Hotmail link)
	/// </summary>
	/// <remarks>
	/// In the HTML -> Javascript tag, you musth put the data from GenerateFrameBreak()
	/// </remarks>
	public class FrameBreak : JavaScript {
		public FrameBreak() {
			//
			// TODO: Add constructor logic here
			//
		}

		public static string GenerateFrameBreak() {
			return	"if (top.location != self.location) {\n" +
				"	top.location = self.location.href\n" +
				"}\n";
		}

	}
}
