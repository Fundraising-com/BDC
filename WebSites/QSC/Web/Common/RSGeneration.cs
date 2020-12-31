// Created by:	Benoit Nadon
// Date:		2005-10-03

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Business.ReportExecution;
using QSP.WebControl;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// Allows to call a report and show the PDF as the client response.
	/// Solution to eliminate security issues with RSDirect.aspx.
	/// Eventually the reports will need to be strong typed to avoid report parameters
	/// inconsistencies.
	/// NOTE: THE PAGE NEEDS TO INHERIT QSP.WebControl.PageSwitchStatePage
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:RSGeneration runat=server></{0}:RSGeneration>")]
	public class RSGeneration : System.Web.UI.WebControls.WebControl, IPageSwitchStateControl
	{
		private const int NO_TIMEOUT_VALUE = -1;
		private const string RSPAGE_URL = "/QSPFulfillment/Common/RSPage.aspx";

		private new PageSwitchStatePage Page 
		{
			get 
			{
				return (PageSwitchStatePage) base.Page;
			}
		}

		/// <summary>
		/// Name of the report to be called.
		/// Does not manage language, this has to be managed by the page using the control.
		/// </summary>
		[Bindable(true), 
		Category("Data"), 
		DefaultValue("")] 
		public string ReportName 
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
			set 
			{
				ViewState["ReportName"] = value;
			}
		}

		private ParameterValueCollection ParameterValues 
		{
			get 
			{
				return ViewState["ParameterValueCollection"] as ParameterValueCollection;
			}
			set 
			{
				ViewState["ParameterValueCollection"] = value;
			}
		}

		private int TimeOut 
		{
			get 
			{
				if(ViewState["TimeOut"] == null) 
				{
					ViewState["TimeOut"] = NO_TIMEOUT_VALUE;
				}

				return Convert.ToInt32(ViewState["TimeOut"]);
			}
			set 
			{
				ViewState["TimeOut"] = value;
			}
		}

        private string OutputFormat
        {
            get
            {
                if (ViewState["OutputFormat"] == null)
                {
                    ViewState["OutputFormat"] = "PDF";
                }

                return Convert.ToString(ViewState["OutputFormat"]);
            }
            set
            {
                ViewState["OutputFormat"] = value;
            }
        }

		/// <summary>
		/// Report output behavior when the generation is started.
		/// </summary>
		[Bindable(true), 
		Category("Data"), 
		DefaultValue("")]
		public FilePageMode Mode 
		{
			get 
			{
				FilePageMode mode = FilePageMode.Internal;

				try 
				{
					mode = (FilePageMode) ViewState["Mode"];
				}
				catch { }

				return mode;
			}
			set 
			{
				ViewState["Mode"] = value;
			}
		}

		/// <summary>
		/// Generates the report and outputs the PDF in the response.
		/// </summary>
		/// <param name="parameterValueCollection">Report parameters</param>
		public virtual void Generate(ParameterValueCollection parameterValueCollection) 
		{
			Generate(ReportName, parameterValueCollection);
		}

		/// <summary>
		/// Generates the report and outputs the PDF in the response.
		/// </summary>
		/// <param name="parameterValueCollection">Report parameters</param>
		/// <param name="timeOut">Time allowed before timing out (ms)</param>
		public virtual void Generate(ParameterValueCollection parameterValueCollection, int timeOut) 
		{
			Generate(ReportName, parameterValueCollection, timeOut);
		}

        public virtual void Generate(ParameterValueCollection parameterValueCollection, int timeOut, string outputFormat)
        {
            Generate(ReportName, parameterValueCollection, timeOut, outputFormat);
        }

		/// <summary>
		/// Generates the report and outputs the PDF in the response.
		/// </summary>
		/// <param name="reportName">Report Name</param>
		/// <param name="parameterValueCollection">Report Parameters</param>
		public virtual void Generate(string reportName, ParameterValueCollection parameterValueCollection) 
		{
			Generate(reportName, parameterValueCollection, NO_TIMEOUT_VALUE);
		}

        public virtual void Generate(string reportName, ParameterValueCollection parameterValueCollection, int timeOut)
        {
            Generate(reportName, parameterValueCollection, timeOut, "PDF");
        }

		/// <summary>
		/// Generates the report and outputs the PDF in the response.
		/// </summary>
		/// <param name="reportName">Report Name</param>
		/// <param name="parameterValueCollection">Report Parameters</param>
		/// <param name="timeOut">Time allowed before timing out (ms)</param>
		public virtual void Generate(string reportName, ParameterValueCollection parameterValueCollection, int timeOut, string outputFormat) 
		{
			int pageSwitchStateID;

			ReportName = reportName;
			ParameterValues = parameterValueCollection;
			TimeOut = timeOut;
            OutputFormat = outputFormat;

			pageSwitchStateID = Page.SavePageSwitchState();

			if(Mode == FilePageMode.Internal) 
			{
				Context.Response.Redirect(RSPAGE_URL + "?PageSwitchStateID=" + pageSwitchStateID.ToString(), false);
			} 
			else if(Mode == FilePageMode.PopUp) 
			{
				Context.Response.Write("<script>window.open('" + RSPAGE_URL + "?IsNewWindow=true&PageSwitchStateID=" + pageSwitchStateID.ToString() + "','',\"toolbar = yes,status=yes,scrollbars=yes,resizable=yes, width=800, height=550\");</script>");
			}
		}

		/// <summary>
		/// Removes the default span tag rendered by a web control.
		/// </summary>
		protected override void Render(HtmlTextWriter writer) { }

		public void SavePageSwitchState(int pageSwitchStateID)
		{
			Page.PageSwitchState[pageSwitchStateID]["ReportName"] = ReportName;
			Page.PageSwitchState[pageSwitchStateID]["ParameterValueCollection"] = ParameterValues;
			Page.PageSwitchState[pageSwitchStateID]["TimeOut"] = TimeOut;
            Page.PageSwitchState[pageSwitchStateID]["OutputFormat"] = OutputFormat;
        }
	}
}
