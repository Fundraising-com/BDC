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
using QSPFulfillment.DataAccess.Business;
using System.Net;
using System.IO;
using System.Web.Services.Protocols;
using Microsoft.ReportingServices.Interfaces;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using Business.ReportExecution;
using Business.Objects;


namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Renders an SQL Server Reporting Services with the following parameters
	/// in the query string:
	///   IsNewWindow - Set to true if the report is in a new window
	///   ReportPath - Path of the report on USPVL2K20-DEV
	///   Format - Format of the rendering (PDF, HTML4.0, etc.)
	///   The next parameters are the normal report parameters, which are all
	///   needed to proceed.
	/// </summary>
	/// 

	
	public partial class ShowReport : CustomerServicePage
	{
		private const int FIRST_PARAMETERS = 3;

		private RSClient rs = new RSClient();
		private ParameterValue[] parametervalues = null;
		private System.Text.StringBuilder sb = new System.Text.StringBuilder();

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
			this.Load +=new EventHandler(Page_Load);

		}

		#endregion

		#region Events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsSessionNull)
			{

				try
				{
					Render();
				}
				catch
				{
				
				}
			}
			else if(IsSessionNull && IsNewWindow)
			{
				this.AddScriptReloadClose();
			}
		}
		#endregion
		
		#region Report Rendering

		private void GetParameters() 
		{
			if(Context.Request.QueryString.Count > FIRST_PARAMETERS) 
			{
				parametervalues = new ParameterValue[Context.Request.QueryString.Count - FIRST_PARAMETERS];

				for(int i = 0; i < parametervalues.Length; i++)
				{
					parametervalues[i] = new ParameterValue();
					parametervalues[i].Name = Context.Request.QueryString.GetKey(i + FIRST_PARAMETERS);
					parametervalues[i].Value = Context.Request.QueryString[i + FIRST_PARAMETERS];
				}
			}
		}

		private void Render()
		{
			if(Context.Request.QueryString["ReportPath"] != null) 
			{
				Byte[] b;
				b = GetReport();
				Response.Clear();
				Response.AddHeader("Content-Type", "application/pdf");
				Response.AddHeader("content-length", b.Length.ToString());
				Response.AddHeader("Title","Fulfillment Report Viewer (PDF)");
		
				Response.BinaryWrite(b);
				Response.Flush();
				Response.Close();
			} 
			else 
			{
				this.MessageManager.SetErrorMessage("Server is unable to proceed.");
			}
		}

		private Byte[] GetReport()
		{
			/*byte[] results = null;
			
			// Render arguments
			string ReportPath;
			string format;
			string historyID = null;
			string devInfo;
			DataSourceCredentials[] credentials = null;
			string showHideToggle = null;
			string encoding;
			string mimeType;
			ParameterValue[] reportHistoryParameters = null;
			Warning[] warnings = null;
			string[] streamIDs = null;

			NetworkCredential creds = new NetworkCredential(QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ReportUserID, QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ReportPassword);
			SessionHeader sh;
			bool isLogged = false;

			ReportPath = Context.Request.QueryString["ReportPath"];

			if(Context.Request.QueryString["Format"] != null) 
			{
				format = Context.Request.QueryString["Format"];
			} 
			else 
			{
				format = "PDF";
			}

			devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";
			
			rs.CookieContainer = new CookieContainer();
			rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

			GetParameters();

			try
			{
				while(!isLogged) 
				{
					try 
					{
						rs.LogonUser(creds.UserName, creds.Password, null);
						isLogged = true;
					} 
					catch
					{
						isLogged = false;
					}
				}
		
				sh = new SessionHeader();
				rs.SessionHeaderValue = sh;

				results = rs.Render(ReportPath, format, historyID, devInfo, parametervalues,
					credentials,
					showHideToggle, out encoding, out mimeType, out reportHistoryParameters,
					out warnings,
					out streamIDs);

				sh.SessionId = rs.SessionHeaderValue.SessionId;
			}
			catch (Exception ex)
			{
				throw ex;
			}
            */
            byte[] results = null;
			return results;
		}

		#endregion

	}
}
