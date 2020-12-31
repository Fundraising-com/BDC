// Created by:	Benoit Nadon
// Date:		2005-10-03

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Business.ReportService;
using QSP.WebControl;

namespace QSPFulfillment.CommonWeb
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

		/// <summary>
		/// Report parameters to send when the button is clicked.
		/// </summary>
		public ParameterValueCollection ParameterValues 
		{
			get 
			{
				ParameterValueCollection parameterValues = null;

				try 
				{
					parameterValues = (ParameterValueCollection) ViewState["ParameterValues"];
				} 
				catch { }

				return parameterValues;
			}
			set 
			{
				ViewState["ParameterValues"] = value;
			}
		}

		/// <summary>
		/// Time allowed before timing out when the button is clicked (ms).
		/// </summary>
		public int TimeOut 
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
			set 
			{
				ViewState["TimeOut"] = value;
			}
		}

		/// <summary>
		/// Report output behavior when the button is clicked.
		/// </summary>
		[Bindable(true), 
		Category("Data"), 
		DefaultValue("")]
		public FilePageMode Mode 
		{
			get 
			{
				FilePageMode mode = FilePageMode.PopUp;

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
		/// Calls the report rendering.
		/// </summary>
		protected override void OnClick(EventArgs e)
		{
			string text = this.Text;

			RSGeneration rsGenerationControl = new RSGeneration();
			this.Controls.Add(rsGenerationControl);

			rsGenerationControl.Mode = Mode;
			rsGenerationControl.Generate(ReportName, ParameterValues, TimeOut);

			this.Text = text;
			base.OnClick (e);
		}
	}
}
