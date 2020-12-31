using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Data access for the Mag Items Summary Report</summary>
	public class MagItemsSummaryReportDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public MagItemsSummaryReportDataAccess(){}

		///<summary>Get a Mag Items Summary Report</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaFinance.dbo.GetMagItemsSummaryReportToPrint")]
		public DataTable GetMagItemsSummaryReportToPrint(	
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime FromDate,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime ToDate
			)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{FromDate,ToDate});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaFinance.dbo.GetMagItemsSummaryReportToPrint",aParams);
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
