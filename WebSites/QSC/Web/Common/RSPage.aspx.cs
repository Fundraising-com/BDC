// Created by:	Benoit Nadon
// Date:		2005-10-03

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
using Business.ReportService;
using System.Net;
using System.Web.Services.Protocols;
using System.Runtime.InteropServices;
using Business.Objects;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Generates a report using session variables.
	/// </summary>
	public partial class RSPage : QSPPage
	{
		private const string DEFAULT_REPORT_FOLDER = "";
        private RSClient rs = new RSClient();

		protected void Page_Load(object s, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				Generate();
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
		
		#region Report Parameters

		/// <summary>
		/// Name of the report to be called.
		/// Does not manage language, this has to be managed by the page using the control.
		/// </summary>
		private string ReportName 
		{
			get 
			{
				string reportName = String.Empty;

				if(ViewState["ReportName"] != null) 
				{
					reportName = ViewState["ReportName"].ToString();
				}

				return reportName;
			}
		}

        /*private ParameterFieldReference[] ParameterValueCollection
        {
            get
            {
                ParameterFieldReference[] parameterValueCollection = null;

                try
                {
                    parameterValueCollection = (ParameterFieldReference[])ViewState["ParameterValueCollection"];
                }
                catch { }

                return parameterValueCollection;
            }
            set
            {
                ViewState["ParameterValueCollection"] = value;
            }
        }*/

    	private ParameterValueCollection ParameterValueCollection 
		{
			get 
			{
				ParameterValueCollection parameterValueCollection = null;

				try
				{
					parameterValueCollection = (ParameterValueCollection) ViewState["ParameterValueCollection"];
				} 
				catch { }

				return parameterValueCollection;
			}
		}
		/// <summary>
		/// Time allowed before timing out (ms).
		/// </summary>
		private int TimeOut 
		{
			get 
			{
				int timeOut = -1;

				try 
				{
					if(Convert.ToInt32(ViewState["TimeOut"]) != 0) 
					{
						timeOut = Convert.ToInt32(ViewState["TimeOut"]);
					}
				} 
				catch { }

				return timeOut;
			}
		}

        private string OutputFormat
        {
            get
            {
                string outputFormat = "PDF";

                try
                {
                    if (ViewState["OutputFormat"] != null)
                    {
                        outputFormat = Convert.ToString(ViewState["OutputFormat"]);
                    }
                }
                catch { }

                return outputFormat;
            }
        }
		#endregion

		/// <summary>
		/// Generates the report.
		/// </summary>
		private void Generate() 
		{
			try 
			{
				if(ReportName != String.Empty) 
				{
                    Business.ReportExecution.ParameterValue[] inputParams = RSConvert.ToParameterValueArray(ParameterValueCollection);
                    Generate(ReportName, inputParams, TimeOut, OutputFormat);
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
		/// Generates the report and outputs the PDF in the response.
		/// </summary>
		/// <param name="reportName">Report Name</param>
		/// <param name="parameterValueCollection">Report Parameters</param>
		/// <param name="timeOut">Time allowed before timing out (ms)</param>
        public virtual void Generate(string reportName, Business.ReportExecution.ParameterValue[] parameterValueCollection, int timeOut) 
		{
			byte[] pdfReport;

            pdfReport = rs.GenerateReportStream(reportName, "PDF", parameterValueCollection, timeOut);
			SetResponse(pdfReport, "PDF");
		}

        public virtual void Generate(string reportName, Business.ReportExecution.ParameterValue[] parameterValueCollection, int timeOut, string outputFormat)
        {
            byte[] report;

            report = rs.GenerateReportStream(reportName, outputFormat, parameterValueCollection, timeOut);
            SetResponse(report, outputFormat);
        }

		/// <summary>
		/// Manages the response to show as a PDF and outputs the file to it.
		/// </summary>
		/// <param name="pdfReport">PDF file as a byte array</param>
		/// <param name="context">HttpContext where to output the PDF</param>
		private void SetResponse(byte[] report, string outputFormat) 
		{
			Response.ClearContent();
			Response.AppendHeader("content-length", report.Length.ToString());
            switch (outputFormat)
            {
                case "EXCEL":
                    Response.ContentType = "application/vnd.ms-excel";
                    break;
                default: Response.ContentType = "application/PDF";
                    break;
            }
            Response.BinaryWrite(report);
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
