using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Data access for the Problem Solver Report</summary>
	public class ProblemSolverReportDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public ProblemSolverReportDataAccess(){}

		///<summary>Get Problem Solver info</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaFinance.dbo.GetProblemSolverReportToPrint")]
		public DataTable GetProblemSolverReportToPrint(	
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime FromDate,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime ToDate
			)
		{
			DataTable dt = null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{FromDate,ToDate});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaFinance.dbo.GetProblemSolverReportToPrint",aParams);
			}
			catch(InvalidOperationException eIO)
			{
				//cleanup stuff

				//re-throw
				throw eIO;
			}
			catch (SqlException eSQL)
			{
				//cleanup stuff

				//re-throw
				throw eSQL;
			}
			catch(Exception eGeneric)
			{
				//cleanup stuff

				//re-throw
				throw eGeneric;
			}
			return dt;
		}
	}
}
