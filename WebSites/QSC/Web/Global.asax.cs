using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using QSPFulfillment.DataAccess.Common;
using System.Web.Security;
using System.Security.Principal;
using QSP.WebControl;

namespace QSPFulfillment
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
			object o = System.Configuration.ConfigurationSettings.GetConfig("ApplicationConfiguration");
			//ApplicationConfiguration.OnApplicationStart(Context.Server.MapPath( Context.Request.ApplicationPath ));
			QSP.WebControl.DataAccess.Business.Connection.ConnnectionString = DataAccess.Common.ApplicationConfiguration.ConnectionString;

         System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
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
			if (!(HttpContext.Current.User == null))
			{
				if (HttpContext.Current.User.Identity.AuthenticationType == "Forms" && HttpContext.Current.User.Identity.IsAuthenticated)
				{
					
					FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
					FormsAuthenticationTicket ticket = id.Ticket;

					// Get the stored user-data, in this case, our roles
					string userData = ticket.UserData;
					string[] roles = userData.Split(',');
					HttpContext.Current.User = new GenericPrincipal(id, roles);

					
				}
						
			}
		}
		protected void Application_AuthorizeRequest(Object sender, EventArgs e)
		{
			
		}
		protected void Application_ReleaseRequestState(Object sender, EventArgs e)
		{
			
		}
		protected void Application_Error(Object sender, EventArgs e)
		{
			try
			{
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(System.Web.HttpContext.Current.Server.GetLastError().GetBaseException());
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

