using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using GA.BDC.Core.Diagnostics;
using GA.BDC.Core.Configuration;

namespace ScratchcardWeb 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			Application.UnLock();
			Application.Add("WebProjectId",int.Parse(ApplicationSettings.GetConfig()["Scratchcard.WebTracking", "WebProjectId"].ToString()));
			Application.Add("WebSiteId",
				int.Parse(ApplicationSettings.GetConfig()["Scratchcard.WebTracking", "WebSiteId"]));
			Application.Add("CreationChannel",
				int.Parse(ApplicationSettings.GetConfig()["Scratchcard.General", "CreationChannel"]));
			
			Application.Lock();
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
			GA.BDC.Core.efundraisingCore.eFundEnv oEnvVars = 
				GA.BDC.Core.efundraisingCore.eFundEnv.CreateByPartnerId(0);

			oEnvVars.CultureName = "en-US";
			oEnvVars.MailConfigFile = 
				Server.MapPath("~/Resources/EmailTemplate/EmailTemplate.xml");
			//** MUST BE DYNAMIC *************************************
			oEnvVars.DefaultPromotionID = 2464;
			//********************************************************
			oEnvVars.Save();
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			// fix VS.NET 2003 bug with get_aspx_ver.aspx not found
			string url = Request.ServerVariables["URL"].ToString();
			if(url.ToLower().IndexOf("get_aspx_ver.aspx") >= 0)
				Response.End();
				
			// check to make sure that the host is always :
			// www.(scratchcard or any other host).com/_PATH_?_QUERYSTRING_
			#if !DEBUG
			if (Request.Url.Host.ToLower().IndexOf("www.") < 0)
			{	
				Response.Status = "301 Moved Permanently";
				Response.RedirectLocation = "http";
				if (Request.IsSecureConnection)
					Response.RedirectLocation += "s";
				Response.RedirectLocation += "://www." + Request.Url.Host.ToLower() + Request.Url.PathAndQuery;
				if (Request.Url.AbsolutePath.ToLower() == "/default.aspx")
					Response.RedirectLocation = Response.RedirectLocation.Replace(Request.Url.AbsolutePath, "/");
				Response.End();
			}
			#endif
		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			Exception ex = Server.GetLastError();

			// Log all errors except FileNotFound errors (bad links).
			if (ex.InnerException == null || ex.InnerException.GetType() != typeof(System.IO.FileNotFoundException))
				Logger.LogError(ex);
			else
				Console.WriteLine(ex.ToString());	
		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

