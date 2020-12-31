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

namespace QSPFulfillment.CommonWeb
{
	///<summary>Base page for QSPFulfillment App - no menu</summary>
	public class QSPPageNoMenu : System.Web.UI.Page
	{
		protected Business.UserProfile aUserProfile;

		public QSPPageNoMenu()
		{
			this.Load += new System.EventHandler(this.Page_Load_INBASE);
		}
		private void Page_Load_INBASE(object sender, System.EventArgs e)
		{
			LoadUserProfile();
		}

		protected void LoadUserProfile()
		{
			aUserProfile = new Business.UserProfile();
			aUserProfile.FMID     = Convert.ToString(Session["FMID"]);
			aUserProfile.Instance = Convert.ToInt32(Session["Instance"]);
			aUserProfile.UserName = Convert.ToString(Session["UserName"]);
			//aUserProfile.Password = Convert.ToString(Session["Password"]);
		}

		#region	GetCheckBox - GetInt - GetString
		protected bool GetCheckBox(string name)
		{
			try
			{
				if(Request.Form[name].ToLower() == "on")
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch(NullReferenceException)
			{
				//if a check box is unchecked, it wont be in the request.form at all
				return false;
			}
		}

		protected int GetInt(ref bool ValidState, ref string Error, string name)
		{
			try
			{
				return Convert.ToInt32(Request.Form[name].Trim());
			}
			catch
			{
				ValidState = false;
				Error += ":" + name; //indicate that an error occured here, to be stripped out later
				return -99999;
			}
		}

		protected string GetString(ref bool ValidState, ref string Error, string name, int maxlen)
		{
			return GetString(ref ValidState, ref Error, name, maxlen, false);
		}
		protected string GetString(ref bool ValidState, ref string Error, string name, int maxlen, bool Mandatory)
		{
			string returnStr = "";
			try   {returnStr = Request.Form[name].Trim() ; }
			catch (NullReferenceException){}

			if((Mandatory == true)&&(returnStr.Length < 1))
			{
				ValidState = false;
				Error += ":MAND" + name; //indicate that an error occured here, to be stripped out later
				return "";
			}
			if(returnStr.Length > maxlen)
			{
				ValidState = false;
				Error += ":MAXLEN" + name; //indicate that an error occured here, to be stripped out later
				return "";
			}
			else
			{
				return returnStr;
			}
		}

		#endregion	GetCheckBox - GetInt - GetString
	}
}
