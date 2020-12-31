using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// DAL file for class OnlineStoreDataAccess to access website info.
	/// </summary>
	public class OnlineStoreDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public OnlineStoreDataAccess()
		{
			QSPStoreDSN = "server=161.230.144.76;database=QSPStore;uid=qspcafulfillment; pwd=fi11m3nt;Connect Timeout=60;";
		}

		///<summary>Connection to QSP.ca website</summary>
		private string QSPStoreDSN;

		///<summary>Update an Account Reporting user record</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_AccountReportingUsers")]
		public bool Update_AcctReportingUsers(	
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int AccountID,
			[SqlParameter(SqlDbType.VarChar	 ,80,ParameterDirection.Input)]string UserName,
			[SqlParameter(SqlDbType.VarChar	 ,20,ParameterDirection.Input)]string Password,
			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool DeletedTF,
			[SqlParameter(SqlDbType.VarChar	 ,2,ParameterDirection.Input)]string Country,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int ReturnCode
			)
		{
			ReturnCode = -1;
			try
			{
				//SqlCommand sqlCmd = CreateSqlParametersCommand(null,
				SqlParameter[] aParams = CreateSqlParameters(null,
				new object[]{AccountID,UserName,Password,DeletedTF,Country,ReturnCode});

				SqlHelper.ExecuteNonQuery(QSPStoreDSN,CommandType.StoredProcedure, 
					"QSPStore.dbo.pr_upd_AccountReportingUsers", aParams);
				
				//ReturnCode = Convert.ToInt32(sqlCmd.Parameters["@ReturnCode"].Value);
				ReturnCode = (int)aParams[5].Value;
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
			if (ReturnCode == 1) { return true; } 
			else { return false; } 
		}

		///<summary>Select an Account Reporting user record</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_OnlineReportingAccounts_byAccountID")]
		public DataTable Get_AcctReportingUsers_byAccountID(
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int AccountID,
			[SqlParameter(SqlDbType.VarChar	 ,2,ParameterDirection.Input)]string Country
			)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
				new object[]{AccountID,Country});

				dt = SqlHelper.ExecuteDataTable(QSPStoreDSN,CommandType.StoredProcedure, 
					"QSPStore.dbo.pr_get_OnlineReportingAccounts_byAccountID", aParams);
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


		///<summary>Select Account Reporting user records by FMID</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_OnlineReportingAccounts_byFMID")]
		public DataTable Get_AcctReportingUsers_byFMID(
			[SqlParameter(SqlDbType.VarChar,4,ParameterDirection.Input)]string FMID,
			[SqlParameter(SqlDbType.VarChar,2,ParameterDirection.Input)]string Country
			)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
				new object[]{FMID,Country});

				dt = SqlHelper.ExecuteDataTable(QSPStoreDSN,CommandType.StoredProcedure, 
					"QSPStore.dbo.pr_get_OnlineReportingAccounts_byFMID", aParams);
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
