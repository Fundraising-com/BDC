using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QSP.OrderExpress.Web.Common.Reporting 
{
    public partial class RSPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.CreateReport();
            }
        }

        #region Report Parameters

        private string ReportName
        {
            get
            {
                string reportName = String.Empty;

                if (Context.Session["ReportName"] != null)
                {
                    reportName = Convert.ToString(Context.Session["ReportName"]);
                }

                return reportName;
            }
        }

        private Dictionary<string, string> ReportParameterDictionary
        {
            get
            {
                Dictionary<string, string> reportParameterDictionary = null;

                if (Context.Session["ReportTimeOut"] != null)
                {
                    reportParameterDictionary = (Dictionary<string, string>)Context.Session["ReportParameterDictionary"];
                }

                return reportParameterDictionary;
            }
        }

        private int ReportTimeOut
        {
            get
            {
                int reportTimeOut = 0;

                if (Context.Session["ReportTimeOut"] != null)
                {
                    reportTimeOut = Convert.ToInt32(Context.Session["ReportTimeOut"]);
                }

                return reportTimeOut;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void CreateReport()
        {
            Byte[] report = null;
            
            // Get the report
            QSP.OrderExpress.Reporting.CreateReports pof = new QSP.OrderExpress.Reporting.CreateReports();
            report = pof.CreatePdfReport(this.ReportName, this.ReportParameterDictionary, this.ReportTimeOut);

            // Send out to webpage
            Response.ClearContent();
            Response.Charset = string.Empty;
            Response.AppendHeader("content-length", report.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(report);
            Response.Flush();
            Response.Close();
        }
    } 
}