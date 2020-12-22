using System;
using System.Collections;
using System.ComponentModel;
using System.Web;

using System.Web.SessionState;
using GA.BDC.Core.Diagnostics;
using GA.BDC.Core.efundraisingCore;


namespace efundraising.RecaudarFondosWeb
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

		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			// check to make sure that the host is always :
			// www.recaudar-fondos.com/_PATH_?_QUERYSTRING_
			#if !DEBUG
			if (Request.Url.Host.ToLower().IndexOf("www.") < 0)
			{	
				Response.Status = "301 Moved Permanently";
				Response.RedirectLocation = "http";
				if (Request.IsSecureConnection)
					Response.RedirectLocation += "s";
				Response.RedirectLocation += "://www.recaudar-fondos.com" + Request.Url.PathAndQuery;
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

