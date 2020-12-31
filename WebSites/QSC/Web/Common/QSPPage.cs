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
using QSP.WebControl;

namespace QSPFulfillment.CommonWeb
{
	///<summary>Base page for QSPFulfillment App</summary>
	public class QSPPage : PageSwitchStatePage
	{

		protected skmMenu.Menu Menu1;


		private void Page_PreRender(object sender, System.EventArgs e)
		{

			if(!IsPostBack)
				if(Menu1 != null && !IsSessionNull)
				{
					AddRolesMenu();
					LoadMenu();
				}
		}
		override protected void OnInit(EventArgs e)
		{
			//SetValuePage();

			if(!((this.IsSessionNull || !this.User.Identity.IsAuthenticated) && IsNewWindow))
			{
				EnsureSessionIntegrity();
				InitialiseComponent();
				base.OnInit(e);							
			}
			else
			{
				//When the pop-up window will open and session is lost
				//Authentication will be done on the main page
				AddScriptReloadClose();
			}
		}
		private void InitialiseComponent()
		{
			this.PreRender += new System.EventHandler(this.Page_PreRender);
		}
		protected void AddScriptReloadClose()
		{
			if(!this.Page.IsStartupScriptRegistered("CloseReload"))
			{
				this.Page.RegisterStartupScript("CloseReload","<script language=\"javascript\"> window.opener.location = \"/qspfulfillment/qspfulfillmentlogin.aspx?Error=true\"; self.close(); </script>"); 
			}
		}
		protected void LoadMenu()
		{
			Menu1.DataSource = Server.MapPath("/QSPFulfillment/Menu.xml");
			Menu1.DataBind();
		}

		private void EnsureSessionIntegrity()
		{
			//validate if the session is still valide
			if(IsSessionNull && !((this is QSPFulfillmentLogin) || (!Session.IsNewSession && this is QSPFulfillmentLogin))||(this.User.Identity.IsAuthenticated && IsSessionNull && Session.IsNewSession))
			{
				string strRedirect = "/QSPFulfillment/QSPFulfillmentLogin.aspx?Error=true";
				/*strRedirect += "&ReturnUrl=" + Request.Path;
				if(Request.QueryString.Count > 0)
					strRedirect += Server.UrlEncode("?" + Request.QueryString.ToString());*/

				Response.Redirect(strRedirect);
			}


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

		public static Business.UserProfile aUserProfile
		{
			get
			{
				return (Business.UserProfile)System.Web.HttpContext.Current.Session["aUserProfile"];
			}
			set
			{
				System.Web.HttpContext.Current.Session["aUserProfile"] = value;
			}
		}
		
		protected string GetListRoles()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			int index = 0;

			foreach(string ss in aUserProfile.Roles)
			{
				sb.Append(ss);

				if(aUserProfile.Roles.Count > index)
					sb.Append(",");

				index ++;
			}
			return sb.ToString();
		}
		private void AddRolesMenu()
		{
			foreach(string ss in aUserProfile.Roles)
			{
                if (!this.Menu1.UserRoles.Contains(ss))
				    this.Menu1.UserRoles.Add(ss);
			}
		}
		private void SetValuePage()
		{
			Response.Buffer = true;
			Response.Expires = -1;
			Response.AddHeader("Pragma", "no-cache");
			Response.CacheControl = "no-cache";
		}

		protected bool IsNewWindow
		{
			get
			{
				if(Request.QueryString["IsNewWindow"]== null)
					return false;

				else
					return Convert.ToBoolean(Request.QueryString["IsNewWindow"]);
			}
		}
		protected bool IsSessionNull
		{
			get
			{
				return System.Web.HttpContext.Current.Session["aUserProfile"] == null;
			}
		}

	}
}