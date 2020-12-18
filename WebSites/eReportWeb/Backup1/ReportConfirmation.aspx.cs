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

using efundraising.eReport;

namespace efundraising.eReportWeb
{
	/// <summary>
	/// Summary description for ReportConfirmation.
	/// </summary>
	public partial class ReportConfirmation : eReportWebBasePage
	{
		// LabelLogin should be removed when created by VS IDE

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				Report report = Report.Create(Session);

				if(report == null)
					Response.Redirect("ReportSelection.aspx");

				// This title has the particularity that its link needs to know the id of the report, that's why it is in code behind
				LiteralReportSummaryLink.Text = "<a href=\"ReportSummary.aspx?rid=" + report.ReportID.ToString() + "\">Summary</a>";

				// Fill labels
				LabelReport.Text = report.Label;
				LabelReportDescription.Text = report.Desc;

				// Get data set that represents the results of the report
				// The data set will be a multiple of 2 tables, a header an the results
				DataSet dataSet = report.RetreiveDataSet();

				// Validates and get number of tables that must be a multiple of 2
				int nbTable = dataSet.Tables.Count;
				if(nbTable%2 == 1)
					throw new Exception("Number of tables returned in data set must be a multiple of 2");

				// Show list of parameters:
				int j = 0;
				TableRow theTableRow = null;
				Table theTable = new Table();
				foreach(ReportParameter parameter in report.ParameterCollection)
				{
					Label label = new Label();
					if (j%2 == 0)
					{
						theTableRow = new TableRow();
					}
					label.CssClass = "NormalText";
					label.Text = string.Format("<span class='NormalTextBold Passive'>{0}</span>: {1}", parameter.Label, parameter.PassingValueDesc);
					TableCell tableCell1 = new TableCell();
					tableCell1.Width = 300;
					tableCell1.Controls.Add(label);
					theTableRow.Controls.Add(tableCell1);
					
					if (j%2 == 1)
					{
						theTable.Controls.Add(theTableRow);
					}
					j++;
				}

				if (j > 0 && j%2 == 1)
					theTable.Controls.Add(theTableRow);


				PlaceholderParameters.Controls.Add(theTable);

				Reporting.Artefact.DataSetToHtmlReport ds2html = new efundraising.Reporting.Artefact.DataSetToHtmlReport();

				for(int i=0; i<nbTable/2; i++)
				{
					// Creates a single data set 
					DataSet dataSetSingle = new DataSet();
					dataSetSingle.Tables.Add(dataSet.Tables[2*i].Copy());
					dataSetSingle.Tables.Add(dataSet.Tables[2*i+1].Copy());

					// Calls artefact to transform data set in html results
					Table table = ds2html.Transform(dataSetSingle, 0, "#FFFFFF", 0, 3, "NormalText", "#EBEAEA", true, "#EDECEC", "#F7F7F7", "#EBEAEA", true);

					TableCell tableCell = new TableCell();
					tableCell.Height = 20;
					TableRow tableRow = new TableRow();
					tableRow.Cells.Add(tableCell);
					table.Rows.Add(tableRow);

					// Adds result in the web page
					PlaceHolderResult.Controls.Add(table);
				}
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

		}
		#endregion
	}
}

