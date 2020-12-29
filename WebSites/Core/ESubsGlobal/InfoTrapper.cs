/* Title:	InfoTrapper
 * Author:	Jean-Francois Buist
 * Summary:	Act like the event viewer of MS Windows but for eSubs application.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// InfoTrapper is used as informational activity log from the site.
	/// When an event occure, we can log it to the database for daily reports.
	/// This acts like the microsoft windows event viewer.
	/// </summary>
	public class InfoTrapper {
		public InfoTrapper() {

		}

		public static void TrapInfo(string message) {
			
		}

		public static void TrapWarning(string message) {
			
		}

		public static void TrapError(string message) {
			
		}
	}
}
