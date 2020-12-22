using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

namespace efundraising.eReportWeb 
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

		#region Static Members and Methods

		private static readonly string WebConfigGroupAccessAllReports = "Group.Access.AllReports";

		public static string GroupAccessAllReports()
		{
			return System.Configuration.ConfigurationSettings.AppSettings[WebConfigGroupAccessAllReports];
		}

		#endregion

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

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			// Retreive and log last error
			Exception ex = Server.GetLastError();
			HttpContext context = HttpContext.Current;
			if (context.Request.Url.Host.ToLower().IndexOf("localhost") == -1
				&& ex.StackTrace.IndexOf("get_aspx_ver.aspx") == -1
				)
			efundraising.Diagnostics.Logger.LogError(ex);
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

