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
	/// This class generates the java script string used to open a customized popup.
	/// </summary>
	/// <example>
	/// <code>
	///		Response.Write(Javascript.GeneratePopupWindow("http://yahoo.ca");
	/// </code>
	/// </example>
	public class Popup : JavaScript	{

		public Popup() {

		}

		#region Generate Popup Window
		public static string GeneratePopupWindow(string url) {
			return GeneratePopupWindow(url, "newWindow", true, -1, -1, true, true, true, true, true);
		}

		public static string GeneratePopupWindowString(string pUrl) {
			return GeneratePopupWindowString(pUrl, "newWindow", true, -1, -1, true, true, true, true, true);
		}

		public static string GeneratePopupWindow(string url, string windowName) {
			return GeneratePopupWindow(url, windowName, true, -1, -1, true, true, true, true, true);
		}

		public static string GeneratePopupWindow(string url, string windowName, bool titleBar) {
			return GeneratePopupWindow(url, "newWindow", titleBar, -1, -1, true, true, true, true, true);
		}

		public static string GeneratePopupWindow(string url, string windowName, bool titleBar, int weight, int heigh) {
			return GeneratePopupWindow(url, "newWindow", titleBar, weight, heigh, true, true, true, true, true);
		}

		public static string GeneratePopupWindow(string url, string windowName, bool titleBar, int weight, int heigh,
			bool menuBar) {
			return GeneratePopupWindow(url, "newWindow", titleBar, weight, heigh, menuBar, true, true, true, true);
		}

		public static string GeneratePopupWindow(string url, string windowName, bool titleBar, int weight, int heigh,
			bool menuBar, bool resizable) {
			return GeneratePopupWindow(url, "newWindow", titleBar, weight, heigh, menuBar, resizable, true, true, true);
		}

		public static string GeneratePopupWindow(string url, string windowName, bool titleBar, int weight, int heigh,
			bool menuBar, bool resizable, bool scrollBars) {
			return GeneratePopupWindow(url, "newWindow", titleBar, weight, heigh, menuBar, resizable, scrollBars, true, true);
		}

		public static string GeneratePopupWindow(string url, string windowName, bool titleBar, int weight, int heigh,
			bool menuBar, bool resizable, bool scrollBars, bool statusBar) {
			return GeneratePopupWindow(url, "newWindow", titleBar, weight, heigh, menuBar, resizable, scrollBars, statusBar, true);
		}

		public static string GeneratePopupWindowString(string url, string windowName, bool titleBar, int weight, int heigh,
			bool menuBar, bool resizable, bool scrollBars, bool statusBar, bool toolBar) {
			string popup = "window.open(\"" + url + "\",\"" + windowName + "\", \"";
			string[] features = {"titlebar=", "width=", "height=", "menubar=", "resizable=", "scrollbars=", "status=", "toolbar=" };

			if(titleBar) 
			{
				popup += features[0] + "yes, ";
			} 
			else 
			{
				popup += features[0] + "no, ";
			}

			if(weight > 0) 
			{
				popup += features[1] + weight + ", ";
			}

			if(heigh > 0) 
			{
				popup += features[2] + heigh + ", ";
			}

			if(menuBar) 
			{
				popup += features[3] + "yes, ";
			} 
			else 
			{
				popup += features[3] + "no, ";
			}

			if(resizable) 
			{
				popup += features[4] + "yes, ";
			} 
			else 
			{
				popup += features[4] + "no, ";
			}

			if(scrollBars) 
			{
				popup += features[5] + "yes, ";
			} 
			else 
			{
				popup += features[5] + "no, ";
			}

			if(toolBar) 
			{
				popup += features[6] + "yes";
			} 
			else 
			{
				popup += features[6] + "no";
			}

			popup += "\");return false;";

			return popup;
		}

		public static string GeneratePopupWindow(string url, string windowName, bool titleBar, int weight, int heigh,
			bool menuBar, bool resizable, bool scrollBars, bool statusBar, bool toolBar) {
			string popup = "<script language=\"javascript\">newWindow=window.open(\"" + url + "\",\"" + windowName + "\", \"";
			string[] features = {"titlebar=", "width=", "height=", "menubar=", "resizable=", "scrollbars=", "status=", "toolbar=" };

			if(titleBar) {
				popup += features[0] + "yes, ";
			} else {
				popup += features[0] + "no, ";
			}

			if(weight > 0) {
				popup += features[1] + weight + ", ";
			}

			if(heigh > 0) {
				popup += features[2] + heigh + ", ";
			}

			if(menuBar) {
				popup += features[3] + "yes, ";
			} else {
				popup += features[3] + "no, ";
			}

			if(resizable) {
				popup += features[4] + "yes, ";
			} else {
				popup += features[4] + "no, ";
			}

			if(scrollBars) {
				popup += features[5] + "yes, ";
			} else {
				popup += features[5] + "no, ";
			}

			if(toolBar) {
				popup += features[6] + "yes";
			} else {
				popup += features[6] + "no";
			}

			popup += "\");</script>";

			return popup;
		}

        // Added by Jiro Hidaka (August 22, 2008)
        public static string GeneratePopupWindowOnPopUpBlockerEnabledBrowsers(string url, string windowName, bool titleBar, int weight, int heigh,
            bool menuBar, bool resizable, bool scrollBars, bool statusBar, bool toolBar, string popupAlertMessage)
        {
            string popup = "<script language=\"javascript\">var newWindow=window.open(\"" + url + "\",\"" + windowName + "\", \"";
            string[] features = { "titlebar=", "width=", "height=", "menubar=", "resizable=", "scrollbars=", "status=", "toolbar=" };

            if (titleBar)
            {
                popup += features[0] + "yes, ";
            }
            else
            {
                popup += features[0] + "no, ";
            }

            if (weight > 0)
            {
                popup += features[1] + weight + ", ";
            }

            if (heigh > 0)
            {
                popup += features[2] + heigh + ", ";
            }

            if (menuBar)
            {
                popup += features[3] + "yes, ";
            }
            else
            {
                popup += features[3] + "no, ";
            }

            if (resizable)
            {
                popup += features[4] + "yes, ";
            }
            else
            {
                popup += features[4] + "no, ";
            }

            if (scrollBars)
            {
                popup += features[5] + "yes, ";
            }
            else
            {
                popup += features[5] + "no, ";
            }

            if (toolBar)
            {
                popup += features[6] + "yes";
            }
            else
            {
                popup += features[6] + "no";
            }

            popup += "\");if(newWindow == null){alert(\""+popupAlertMessage+"\");}</script>";

            return popup;
        }

        public static string GeneratePopupWindowOnPopUpBlockerEnabledBrowsers(string url, string windowName, string popupAlertMessage)
        {
            string popup = "<script language=\"javascript\">var newWindow=window.open(\"" + url + "\",\"" + windowName + "\");";
            popup += "if(newWindow == null){alert(\"" + popupAlertMessage + "\");}</script>";
            return popup;
        }

		#endregion 

	}
}
