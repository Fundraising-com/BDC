/* Title:	Reporting Database
 * Author:	Krystian Olszanski
 * Summary:	Data access layer object to retreive/update/insert values in the database.
 * 
 * Create Date:	march 1, 2006
 * 
 */

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;
//using efundraising.ESubsGlobal.DataAccess;
//using efundraising.ESubsGlobal;
//using efundraising.ESubsGlobal.Common;
//using efundraising.ESubsGlobal.Touch;
//using efundraising.ESubsGlobal.Users;

namespace GA.BDC.Core.Reporting.Artefact.DataAccess {
	/// <summary>
	/// Summary description for DataAccess.
	/// </summary>
	public class DataAccess : GA.BDC.Core.Data.Sql.DatabaseObject {
		
		// contains a list of reportId's and reportLable's for all available reports in the DB
//		private ReportsList reportsList;

		
		public DataAccess() {
			if(Config.IsProduction) {
				SetConnectionString(Config.ConnectionStringRelease);
				SetDataProvider(Config.DataProviderRelease);
			} else {
				SetConnectionString(Config.ConnectionStringDebug);
				SetDataProvider(Config.DataProviderDebug);
			}

//			reportsList = new ReportsList();
		}
	

		// Gets a list of reportId's and reportLable's for all available reports in the DB
		public DataTable GetReportsList() 
		{

			string storedProcName = "af_get_report_list";
			DataTable dt = new DataTable();
		
			SqlInterface si = new SqlInterface(dataProvider, connectionString);
	
			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				
				si.Open();
		
				// Fetch from database.
				dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				// fill our objects
				try 
				{
			
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

			} 
			catch 
			{

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
			return dt;
		}

		public Report GetReportDetails(int reportId) 
		{

			string storedProcName = "af_get_report_details";
			Report rpt = new Report();
		
			SqlInterface si = new SqlInterface(dataProvider, connectionString);
	
			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@report_id", DbType.Int32, DBValue.ToDBInt32(reportId)));

				si.Open();
		
				// Fetch from database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				// fill our objects
				try 
				{
					rpt.ReportId = (int)DBValue.ToInt32(dt.Rows[0]["report_id"]);
					rpt.ReportLabel = DBValue.ToString(dt.Rows[0]["report_label"]);
					rpt.ReportSp = DBValue.ToString(dt.Rows[0]["report_sp"]);
					rpt.Displayable = DBValue.ToBoolean(dt.Rows[0]["displayable"]);
					rpt.DatabaseName = DBValue.ToString(dt.Rows[0]["database_name"]);
					rpt.ServerName = DBValue.ToString(dt.Rows[0]["server_name"]);

				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

			} 
			catch 
			{
				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
			return rpt;
		}


/*
		// Gets a list of reportId's and reportLable's for all available reports in the DB
		public ReportsList GetReportsList() {

			string storedProcName = "af_get_report_list";
		
			SqlInterface si = new SqlInterface(dataProvider, connectionString);
	
			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				
				si.Open();
		
				// Fetch from database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				// fill our objects
				try {
					
					foreach(DataRow row in dt.Rows) {
						reportsList.AddReport(DBValue.ToString(row["report_id"]),DBValue.ToString(row["report_label"]));
					}
				} catch(System.Exception ex) {
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

			} catch {

				// throw exception
				throw;
			} finally {
				// Always close connection.
				si.Close();
			}
			return reportsList;
		}
*/


	}

}
