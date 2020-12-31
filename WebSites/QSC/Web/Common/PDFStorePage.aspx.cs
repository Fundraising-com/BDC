// Created by:	Benoit Nadon
// Date:		2006-01-13

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
using FileStore;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Generates a report using session variables.
	/// </summary>
	public partial class PDFStorePage : QSPPage
	{
		protected void Page_Load(object s, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				Merge();
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
		/// PDFStore containing files to be merged.
		/// </summary>
		private PDFStore Store 
		{
			get 
			{
				PDFStore pdfStore = null;

				try
				{
					pdfStore = (PDFStore) ViewState["PDFStore"];
				} 
				catch { }

				return pdfStore;
			}
		}

		#endregion

		/// <summary>
		/// Merges and streams the file.
		/// </summary>
		private void Merge() 
		{
			try 
			{
				if(Store != null) 
				{
					Merge(Store);
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
		public virtual void Merge(PDFStore pdfStore) 
		{
			try 
			{
				byte[] pdfMergedReportFile = pdfStore.Get(pdfStore.Merge());
				pdfStore.Close();

				// Clear HTTP header and content
				Response.ClearHeaders();
				Response.ClearContent();

				// Stream the file to the client
				Response.ContentType = "application/pdf";
				Response.BinaryWrite(pdfMergedReportFile);
				Response.Flush();
				Response.Close();
			} 
			catch(Exception ex) 
			{
				if(pdfStore != null) 
				{
					pdfStore.Close();
				}

				throw ex;
			}
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
