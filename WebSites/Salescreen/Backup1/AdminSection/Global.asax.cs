using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using efundraising.Diagnostics;
using System.Security.Principal;
using System.Web.Security;

namespace AdminSection 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	/// 

	using System.Collections;
	//using Components.Server.SampleKit;
	//using Components.Server.Collections;

	public class Global : System.Web.HttpApplication
	{
		#region Constants
		public struct SessionKeys
		{
			public static readonly string Environment = "Environment"; 
			public static readonly string FormClicked = "Form.Clicked";
		}

		public struct QueryStringKeys
		{
			public static readonly string PromotionId = "pr_id";
			public static readonly string PromotionId2 = "promotion_id";
			public static readonly string LeadId = "lID";
			public static readonly string CreationChannelId = "CcID";
			public static readonly string PartnerId = "ptrId";
			public static readonly string VisitorLogId = "vlid";
		}

		public struct CacheKeys
		{

		}

		public struct ApplicationKeys
		{

		}

		
		#endregion


		#region Static

		//static public SampleKitCollections sampleKitItemCollection = null;
		static public readonly string SamplekitListItem = "EFundraisingWeb.Samplekit.Item";
		static public readonly string RedirectPageKey = "EFundraisingWeb.Redirect.Page";
		static public Hashtable RedirectPageHastable = new Hashtable();


		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e) {
			//sampleKitItemCollection = SamplekitItem.CreateSamplekitCollectionFromConfig();
		/*	int iCount= efundraising.Configuration.ApplicationSettings.GetConfig().GetCount(RedirectPageKey);
			for (int i= 0; i < iCount; i++)
			{
				if (efundraising.Configuration.ApplicationSettings.GetConfig()[RedirectPageKey, i, "name"] != null &&
					efundraising.Configuration.ApplicationSettings.GetConfig()[RedirectPageKey, i, "pageredirect"] != null)
				{
					string theKey = efundraising.Configuration.ApplicationSettings.GetConfig()[RedirectPageKey, i, "name"];
					string theValue  = efundraising.Configuration.ApplicationSettings.GetConfig()[RedirectPageKey, i, "pageredirect"];
					RedirectPageHastable[theKey] = theValue;
				}
			}*/
		}
 
		protected void Session_Start(Object sender, EventArgs e) {
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			
			// fix VS.NET 2003 bug with get_aspx_ver.aspx not found
			string url = Request.ServerVariables["URL"].ToString();
			if(url.ToLower().IndexOf("get_aspx_ver.aspx") >= 0)
				Response.End();
				
			// check to make sure that the host is always :
			// www.efundraising.com/_PATH_?_QUERYSTRING_
			#if !DEBUG
			if (Request.Url.Host.ToLower().IndexOf("efundraising.com") < 0)
			{	
				Response.Status = "301 Moved Permanently";
				Response.RedirectLocation = "http";
				if (Request.IsSecureConnection)
					Response.RedirectLocation += "s";
				Response.RedirectLocation += "://www.efundraising.com" + Request.Url.PathAndQuery;
				if (Request.Url.AbsolutePath.ToLower() == "/default.aspx")
					Response.RedirectLocation = Response.RedirectLocation.Replace(Request.Url.AbsolutePath, "/");
				Response.End();
			}			
			if (Request.Url.Host.ToLower().IndexOf("www.") < 0)
			{	
				Response.Status = "301 Moved Permanently";
				Response.RedirectLocation = "http";
				if (Request.IsSecureConnection)
					Response.RedirectLocation += "s";
				Response.RedirectLocation += "://www.efundraising.com" + Request.Url.PathAndQuery;
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
            string request = ((System.Web.HttpApplication)(sender)).Request.RawUrl;



            bool isAuth = false;
                        
                if (HttpContext.Current.User != null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        if (HttpContext.Current.User.Identity is FormsIdentity)
                        {
                            // Get Forms Identity From Current User
                            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                            // Get Forms Ticket From Identity object
                            FormsAuthenticationTicket ticket = id.Ticket;
                            // userdata string was retrieved from stored user-data (a roles string from db "Users" table, e.g. "Admin;Manager;User")
                            string userData = ticket.UserData;
                            string[] roles = userData.Split(',');
                            // Create a new Generic Principal Instance and assign to Current User
                            HttpContext.Current.User = new GenericPrincipal(id, roles); // (could also be a custom principal object of your design)
                            isAuth = true;

                        }
                    }
                }
                if (!isAuth)
                {
                    if (!(request.StartsWith("/Login.aspx")))
                    {
                        Response.Redirect("Login.aspx?returnURL=" + request);
                    }
                }
        

		}

		protected void Application_Error(Object sender, EventArgs e) 
		{/*
			Exception ex = Server.GetLastError();

			// Log all errors except FileNotFound errors (bad links).
			if (ex.InnerException == null || ex.InnerException.GetType() != typeof(System.IO.FileNotFoundException))
				Logger.LogError(ex);	

			// Redirects for old aspx files
			if(ex.InnerException.GetType() == typeof(System.IO.FileNotFoundException))
			{
				string badurl = Request.Url.AbsolutePath;
				badurl = badurl.Substring(badurl.LastIndexOf("/"));
				
				if (badurl.ToLower().IndexOf(".aspx") > -1)
				{
					//Find the appropriate redirect
					/*efundraising.eFundraisingWeb.Components.Server.SEO.Webpage301Redirect webpage301Redirect = new efundraising.eFundraisingWeb.Components.Server.SEO.Webpage301Redirect(badurl, Server.MapPath("~/Resources/xml/Webpage301Redirect.xml"));
					Response.Status = "301 Moved Permanently";
					Response.AddHeader("Location", efundraising.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"]+ webpage301Redirect.RedirectURL + "?" + Request.QueryString.ToString());
					Response.End();
				}

			}*/
		
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

