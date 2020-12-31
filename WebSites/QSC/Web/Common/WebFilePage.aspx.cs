// Created by:	Benoit Nadon
// Date:		2006-01-12

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using Business.ReportExecution;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Generates a report using session variables.
	/// </summary>
	public partial class WebFilePage : QSPPage
	{
		protected void Page_Load(object s, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				Stream();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.LoadsPageSwitchState = true;
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Streaming Parameters

		/// <summary>
		/// Name of the file to be streamed.
		/// </summary>
		private string FilePath 
		{
			get 
			{
				string filePath = String.Empty;

				if(ViewState["FilePath"] != null) 
				{
					filePath = ViewState["FilePath"].ToString();
				}

				return filePath;
			}
		}

		/// <summary>
		/// Mime-Type of the file to be streamed.
		/// </summary>
		private string MimeType
		{
			get 
			{
				string mimeType = String.Empty;

				if(ViewState["MimeType"] != null)
				{
					mimeType = ViewState["MimeType"].ToString();
				}

				return mimeType;
			}
		}

		#endregion

		/// <summary>
		/// Streams the file.
		/// </summary>
		private void Stream() 
		{
			try 
			{
				if(FilePath != String.Empty) 
				{
					Stream(FilePath, MimeType);
				}
				else 
				{
					AddScriptClose();
				}
			} 
			catch(Exception ex) 
			{
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);

				AddScriptClose();
			}
		}

		/// <summary>
		/// Streams the file and outputs it in the response.
		/// </summary>
		/// <param name="reportName">Report Name</param>
		/// <param name="parameterValueCollection">Report Parameters</param>
		/// <param name="timeOut">Time allowed before timing out (ms)</param>
		public virtual void Stream(string filePath, string mimeType) 
		{
			System.Net.WebClient webClient = new WebClient();
			byte[] file;
			
			file = webClient.DownloadData(filePath);

			// Clear HTTP header and content
			Response.ClearHeaders();
			Response.ClearContent();

			// Stream the file to the client
			Response.ContentType = mimeType;
         Response.AddHeader("Content-Length", file.Length.ToString());
         Response.BinaryWrite(file);
			Response.Flush();
			Response.Close();
		}

		/// <summary>
		/// Closes the pop-up window.
		/// </summary>
		private void AddScriptClose() 
		{
			if(IsNewWindow) 
			{
				this.RegisterStartupScript("AddScriptClose", "<script language=\"javascript\"> self.close(); </script>");
			}
		}
	}
}
