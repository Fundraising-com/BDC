using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl.Reporting
{
	/// <summary>
	/// Allows to call a report and show the PDF as the client response.
	/// Solution to eliminate security issues with RSDirect.aspx.
	/// Eventually the reports will need to be strong typed to avoid report parameters
	/// inconsistencies.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:RSGeneration runat=server></{0}:RSGeneration>")]
	public class RSGeneration : System.Web.UI.WebControls.WebControl
    {
        #region Properties

        /// <summary>
		/// Name of the report to be called.
		/// </summary>
		[Bindable(true), 
		Category("Data"), 
		DefaultValue("")] 
		public string ReportName 
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
			set 
			{
				Context.Session["ReportName"] = value;
			}
		}

        /// <summary>
        /// Dictionary of parameters for the report
        /// </summary>
        [Bindable(true),
        Category("Data"),
        DefaultValue("")] 
        public Dictionary<string, string> ReportParameterDictionary
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
			set 
			{
                Context.Session["ReportParameterDictionary"] = value;
			}
		}

        /// <summary>
        /// Time allowed before timing out when the button is clicked (ms).
        /// </summary>
        [Bindable(true),
        Category("Data"),
        DefaultValue("60000")]
        public int ReportTimeOut
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
			set 
			{
				Context.Session["ReportTimeOut"] = value;
			}
        }

		/// <summary>
		/// Report output behavior when the generation is started.
		/// </summary>
		[Bindable(true), 
		Category("Data"), 
		DefaultValue("")]
		public FilePageMode ReportMode 
		{
			get 
			{
				FilePageMode reportMode = FilePageMode.Internal;

                if (ViewState["ReportMode"] != null)
                {
                    reportMode = (FilePageMode)ViewState["ReportMode"];
                }

                return reportMode;
			}
			set 
			{
				ViewState["ReportMode"] = value;
			}
		}

        /// <summary>
        /// Name of the webpage to display the report.
        /// </summary>
        [Bindable(true),
        Category("Data"),
        DefaultValue("Common/Reporting/RSPage.aspx")]
        public string ReportWebPage
        {
            get
            {
                string reportWebPage = String.Empty;

                if (ViewState["ReportWebPage"] != null)
                {
                    reportWebPage = (string)ViewState["ReportWebPage"];
                }

                return reportWebPage;
            }
            set
            {
                ViewState["ReportWebPage"] = value;
            }
        }

        #endregion

        #region Methods

		/// <summary>
		/// Generates the report and outputs the PDF in the response.
		/// </summary>
		/// <param name="reportName">Report Name</param>
        /// <param name="reportParameterDictionary">Report Parameters</param>
        /// <param name="reportTimeOut">Time allowed before timing out (ms)</param>
        public virtual void Generate(string reportName, Dictionary<string, string> reportParameterDictionary, int reportTimeOut) 
		{
			ReportName = reportName;
            ReportParameterDictionary = reportParameterDictionary;
            ReportTimeOut = reportTimeOut;

			if (ReportMode == FilePageMode.Internal) 
			{
				Context.Response.Redirect(ReportWebPage, false);
			} 
			else if (ReportMode == FilePageMode.PopUp) 
			{
				Context.Response.Write("<script>window.open('" + ReportWebPage + "?IsNewWindow=true','',\"toolbar = yes,status=yes,scrollbars=yes,resizable=yes, width=800, height=550\");</script>");
			}
		}

		/// <summary>
		/// Removes the default span tag rendered by a web control.
		/// </summary>
		protected override void Render(HtmlTextWriter writer) { }

        #endregion
	}
}
