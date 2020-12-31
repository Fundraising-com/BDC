using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>DAL file for class PhoneDataAccess to access Phone info.</summary>
	public class PhoneDataAccess : QDataAccess
	{
		public PhoneDataAccess()
		{

		}

		///<summary>Insert a Phone object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_ins_Phone")]
		public bool Insert(	[SqlParameter(SqlDbType.Int		,   4,ParameterDirection.Input)]int Type,
							[SqlParameter(SqlDbType.Int		,   4,ParameterDirection.Input)]int PhoneListID,
							[SqlParameter(SqlDbType.VarChar	,  50,ParameterDirection.Input)]string PhoneNumber,
							[SqlParameter(SqlDbType.VarChar	,2000,ParameterDirection.Input)]string BestTimeToCall,
							[SqlParameter(SqlDbType.Int,ParameterDirection.Output)]out int Phone_ID
							)
		{
			Phone_ID=-2;
			int rows;

			try
			{
				SqlCommand sqlCmd = CreateSqlParametersCommand(null,
					new object[]{Type,PhoneListID,PhoneNumber,BestTimeToCall,Phone_ID});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_ins_Phone",sqlCmd);
					
				Phone_ID = Convert.ToInt32(sqlCmd.Parameters["@Phone_ID"].Value);
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

			string msg = "";
			if(rows == 1)
			{
				return true;
			}
			else
			{
				msg = rows.ToString() + " rows were inserted, when only 1 was expected";
				throw new System.ApplicationException(msg);
			}
		}


		///<summary>Update a Phone object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_Phone_by_id")]
		public bool Update(	[SqlParameter(SqlDbType.Int		,   4,ParameterDirection.Input)]int ID,
							[SqlParameter(SqlDbType.Int		,   4,ParameterDirection.Input)]int Type,
							[SqlParameter(SqlDbType.Int		,   4,ParameterDirection.Input)]int PhoneListID,
							[SqlParameter(SqlDbType.VarChar	,  50,ParameterDirection.Input)]string PhoneNumber,
							[SqlParameter(SqlDbType.VarChar	,2000,ParameterDirection.Input)]string BestTimeToCall
							)
		{

			int rows;
			
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{ID,Type,PhoneListID,PhoneNumber,BestTimeToCall});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_upd_Phone_by_id",aParams);
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
			
			string msg = "";
			if(rows != 1)
			{
				msg = rows.ToString() + " rows were updated, when only 1 was expected";
			}
			if(msg == "")
			{
				return true;
			}
			else
			{				
				throw new System.ApplicationException(msg);
			}
		}


		///<summary>Select a Phone object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_Phone_by_type")]
		public DataTable Exists([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int type,
								[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int PhoneListId)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{type,PhoneListId});

				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "pr_get_Phone_by_type",aParams);
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