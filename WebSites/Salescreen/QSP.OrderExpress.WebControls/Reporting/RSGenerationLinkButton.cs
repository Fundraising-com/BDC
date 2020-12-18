using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSP.WebControl.Reporting
{
	/// <summary>
	/// LinkButton that allows to get an RS report in the window or in a pop-up.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:RSGenerationLinkButton runat=server></{0}:RSGenerationLinkButton>")]
	public class RSGenerationLinkButton : System.Web.UI.WebControls.LinkButton, INamingContainer
	{
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

				if (ViewState["ReportName"] != null) 
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

                if (ViewState["ReportParameterDictionary"] != null)
                {
                    reportParameterDictionary = (Dictionary<string, string>)ViewState["ReportParameterDictionary"];
                }

                return reportParameterDictionary;
            }
            set
            {
                ViewState["ReportParameterDictionary"] = value;
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
                int reportTimeOut = -1;

                if (Convert.ToInt32(ViewState["ReportTimeOut"]) != 0)
                {
                    reportTimeOut = Convert.ToInt32(ViewState["ReportTimeOut"]);
                }

                return reportTimeOut;
            }
            set
            {
                ViewState["ReportTimeOut"] = value;
            }
        }

        /// <summary>
        /// Report output behavior when the button is clicked.
        /// </summary>
        [Bindable(true),
        Category("Data"),
        DefaultValue("")]
        public FilePageMode ReportMode
        {
            get
            {
                FilePageMode reportMode = FilePageMode.PopUp;

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

		/// <summary>
		/// Calls the report rendering.
		/// </summary>
		protected override void OnClick(EventArgs e)
		{
			string text = this.Text;

			RSGeneration rsGenerationControl = new RSGeneration();
			this.Controls.Add(rsGenerationControl);

			rsGenerationControl.ReportMode = ReportMode;
            rsGenerationControl.ReportWebPage = ReportWebPage;
            rsGenerationControl.Generate(ReportName, ReportParameterDictionary, ReportTimeOut);

			this.Text = text;
			base.OnClick (e);
		}
	}
}
