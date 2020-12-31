using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Data access for the Statement Report by Campaign</summary>
	public class StatementReportByCampaignPrintDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public StatementReportByCampaignPrintDataAccess(){}

		///<summary>Get a Statement Report by Campaign</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaFinance.dbo.GetAllStatementsByCampaignToPrint")]
		public DataTable GetAllStatementsByCampaignToPrint(	
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)]Int32? FiscalYear,
			[SqlParameter(SqlDbType.Bit ,ParameterDirection.Input)]bool Realtime,
 			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)]Int32? CampaignID,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)]Int32? AccountID,
			[SqlParameter(SqlDbType.VarChar ,ParameterDirection.Input)]string AccountName,
			[SqlParameter(SqlDbType.VarChar ,ParameterDirection.Input)]string FMID,
			[SqlParameter(SqlDbType.VarChar ,ParameterDirection.Input)]string FMLastName, 
            [SqlParameter(SqlDbType.Int ,ParameterDirection.Input)]Int32? StatementRunId
            )

		{
			DataTable dt=null;
			try
			{
                SqlParameter[] aParams = CreateSqlParameters(null, new object[] { FiscalYear, Realtime, CampaignID, AccountID, AccountName, FMID, FMLastName, StatementRunId });
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaFinance.dbo.GetAllStatementsByCampaignToPrint",aParams);
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
