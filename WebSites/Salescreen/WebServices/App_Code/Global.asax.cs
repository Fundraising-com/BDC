using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
//using QSP.WebControl; 
using System.Configuration;
using QSPForm.SystemFramework;
using System.Runtime.Remoting;
using System.IO;
using System.Web.Security;

namespace QSPFormWebServices 
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
			//We use the foloowing line to be able to set the connection string and
			//to consider all other config from the Web.config file
			ApplicationConfiguration.OnApplicationStart(Context.Server.MapPath( Context.Request.ApplicationPath ));
			string configPath = Path.Combine(Context.Server.MapPath( Context.Request.ApplicationPath ),"remotingclient.cfg");
			if(File.Exists(configPath))
				RemotingConfiguration.Configure(configPath);
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
			try
			{				
				QSPForm.SystemFramework.ApplicationError.ManageError(System.Web.HttpContext.Current.Server.GetLastError().GetBaseException());
			}
			catch 
			{				
			}
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

