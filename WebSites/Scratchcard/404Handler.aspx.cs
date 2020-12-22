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
using System.Xml;
using System.Diagnostics;

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for _404Handler.
	/// </summary>
	public class _404Handler : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
    		//get website name
    		string host = Request.Url.Host;
	
			//get link the user requested
			string requestPage = Request.ServerVariables["QUERY_STRING"].ToString();
		
			//extract the page only
			requestPage = requestPage.Replace("aspxerrorpath=/","");
			requestPage = requestPage.Replace("404;http://" + host + "/","");
			          			
			////GET NEW URL
			bool hostFound = false;
			bool pageFound = false;
			bool exit = false;
			string defaultRedirect = "";
            string newPage = "";

			try
			{
				XmlTextReader reader =  new XmlTextReader(Server.MapPath("301Redirect.xml"));
		
				// ignore the whitespace in the document
				reader.WhitespaceHandling = WhitespaceHandling.None;
 
				while (reader.Read() && !exit)
				{
					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "HostName") //host is found
					{
						reader.Read(); //falls on the value
						if (reader.Value == host)
						{
							hostFound = true;
						}
					}

					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "DefaultRedirect" && hostFound)  //gets the default redirect value of the host
					{
						reader.Read(); //falls on the value
						defaultRedirect = reader.Value;
					
					}
				
					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "PageName" && hostFound)  //reads each page for our Host
					{
						reader.Read(); //falls on the value
						if (reader.Value == requestPage)       //check if page is the requested one
						{
							pageFound = true;
						}
					}
				
					if (reader.NodeType == XmlNodeType.Element &&
						reader.Name == "Redirect" && pageFound)  //gets the newPage value
					{
						reader.Read();
						newPage = reader.Value;
						exit = true;
					}
				}
				reader.Close();
		 
				string newURL = defaultRedirect;
				if (newPage != "")
				{
					newURL = "/" + newPage;
				}
			
				Response.Status = "301 Moved Permanently";
				Response.AddHeader("Location", newURL);
			}
			catch(Exception ex)
			{
				Response.Status = "301 Moved Permanently";
				Response.AddHeader("Location", "");
			}

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
