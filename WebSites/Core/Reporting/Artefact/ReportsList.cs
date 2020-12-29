using System;
using System.Collections;
using System.Data;

namespace GA.BDC.Core.Reporting.Artefact
{
	/// <summary>
	/// Summary description for ReportsList.
	/// </summary>
	public class ReportsList
	{
		private ArrayList reportsIdList;
		private ArrayList reportsLabelList;

		public ReportsList()
		{
			reportsIdList = new ArrayList();
			reportsLabelList = new ArrayList();
		}

		public DataTable GetReportsList(){

			DataAccess.DataAccess dbo = new DataAccess.DataAccess();

			DataTable dt =  dbo.GetReportsList();

			return dt;

		}

		public void AddReport(string id, string label)
		{
			reportsIdList.Add(id);
			reportsLabelList.Add(label);
		}

		public ArrayList GetReportsIdList() {
			return reportsIdList;
		}

		public ArrayList GetReportsLabelList() {
			return reportsLabelList;
		}


	}
}
