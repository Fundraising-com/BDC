using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Data access for the Participant Listing Report</summary>
	public class ParticipantListingReportDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public ParticipantListingReportDataAccess(){}

		///<summary>Get a Statement Report by Campaign</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaFinance.dbo.GetParticipantListingReportToPrint")]
		public DataTable GetParticipantListingReportToPrint(	
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime FromDate,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime ToDate
			)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{FromDate,ToDate});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaFinance.dbo.GetParticipantListingReportToPrint",aParams);
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
