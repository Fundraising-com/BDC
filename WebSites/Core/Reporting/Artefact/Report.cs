using System;

namespace GA.BDC.Core.Reporting.Artefact
{
	/// <summary>
	/// Summary description for Report.
	/// </summary>
	public class Report
	{
		private int reportId;
		private string reportLabel;
		private string reportSp;
		private bool displayable;	// FALSE by default
		private string databaseName;
		private string serverName;

		public Report()
		{
			reportId = int.MinValue;
			databaseName = String.Empty;
			reportLabel = String.Empty;
			displayable = false;	// FALSE by default
		}

		public Report(int id)
		{
			reportId = id;

			DataAccess.DataAccess dbo = new DataAccess.DataAccess();
			Report rpt = new Report();
			rpt = dbo.GetReportDetails(reportId);

			reportLabel = rpt.reportLabel;
			reportSp = rpt.reportSp;
			displayable = rpt.displayable;
			databaseName = rpt.databaseName;
			serverName = rpt.serverName;
		}


		#region Properties
		public int ReportId 
		{
            set { reportId = value; }			
			get { return reportId; }
		}
		
		public string DatabaseName{
			set { databaseName = value; }
			get { return databaseName; }
		}

		public string ServerName{
			set { serverName = value; }
			get { return serverName; }
		}

		public string ReportLabel {
			set { reportLabel = value; }
			get { return reportLabel; }
		}

		public string ReportSp {
			set { reportSp = value; }
			get { return reportSp; }
		}

		public bool Displayable {
			set { displayable = value; }
			get { return displayable; }
		}
		#endregion
	
	}
}
