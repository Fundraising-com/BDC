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
using GA.BDC.Core.Diagnostics;
using GA.BDC.Core.eFundraisingStore;


namespace GA.BDC.WEB.ScratchcardWeb
{

	/// <summary>
	/// Summary description for _Default.
	/// </summary>

	public class Error404 : System.Web.UI.Page
	{
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected System.Web.UI.WebControls.Label Label1;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
	
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				Response.Status = "301 Moved Permanently";
				Response.AddHeader("Location", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"]+Get301RedirectURL());
			}
			catch(Exception ex){
			    Logger.LogError("Error in page load of Error404",ex);
			}
		}

		private string Get301RedirectURL()
		{			
			string requestedURL = "";
			string redirectURL = "";

            try
			{
				requestedURL ="";

				//Set the requested URL
				if(Request["aspxerrorpath"] != null)
				{
					//get link the user requested
					string requestPage = Request.ServerVariables["QUERY_STRING"].ToString();
		
					//extract the page only
					requestedURL = "/" + requestPage.Replace("aspxerrorpath=/","");
					string vd = GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.VirtualDirectory", "host"];
					if (vd != "")
					{
						requestedURL = requestedURL.Replace(vd,"");
					}
				
				}
				else 
				{
					requestedURL = Request.Url.AbsoluteUri.Substring(Request.Url.AbsoluteUri.IndexOf("?404;")+5);
					requestedURL = requestedURL.Substring(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"].Length);
					Response.Write(requestedURL);
				}

				//Find the appropriate redirect
				Components.Server.SEO.Webpage301Redirect webpage301Redirect = new Components.Server.SEO.Webpage301Redirect(requestedURL, Server.MapPath("~/Resources/xml/Webpage301Redirect.xml"));
				redirectURL = webpage301Redirect.RedirectURL;
			
				
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Get301Redirect",ex);
			}

            return redirectURL;

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
