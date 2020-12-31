using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// DAL file for class AddressDataAccess to access Address info.
	/// </summary>
	public class AddressDataAccess : QDataAccess
	{
		public AddressDataAccess()
		{

		}

		///<summary>Insert an Address object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_ins_Address")]
		public bool Insert(	[SqlParameter(SqlDbType.VarChar	,50,ParameterDirection.Input)]string street1,
							[SqlParameter(SqlDbType.VarChar	,50,ParameterDirection.Input)]string street2,
							[SqlParameter(SqlDbType.VarChar	,50,ParameterDirection.Input)]string city,
							[SqlParameter(SqlDbType.VarChar	,10,ParameterDirection.Input)]string stateProvince,
							[SqlParameter(SqlDbType.VarChar	, 7,ParameterDirection.Input)]string postal_code,
							[SqlParameter(SqlDbType.VarChar	, 4,ParameterDirection.Input)]string zip4,
							[SqlParameter(SqlDbType.VarChar	,10,ParameterDirection.Input)]string country,
							[SqlParameter(SqlDbType.Int		, 4,ParameterDirection.Input)]int address_type,
							[SqlParameter(SqlDbType.Int		, 4,ParameterDirection.Input)]int AddressListID,
							[SqlParameter(SqlDbType.Int		,ParameterDirection.Output)]out int Address_ID
							)
		{
			Address_ID=-1;
			int rows;

			try
			{
				SqlCommand sqlCmd = CreateSqlParametersCommand(null,
					new object[]{street1,street2,city,stateProvince,postal_code,zip4,country,address_type,AddressListID, Address_ID});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_ins_Address",sqlCmd);
				Address_ID = Convert.ToInt32(sqlCmd.Parameters["@Address_ID"].Value);
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
			if(rows != 1)//inserted an Address row
			{
				msg = rows.ToString() + " rows were inserted, we needed exactly 1 to go in";
			}
			if(msg == "")
			{
				return true;
			}
			else
			{				
				throw new System.ApplicationException(msg);
			}
			//return false;
		}
		///<summary>Update a Address object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_Address_by_id")]
		public bool Update(	[SqlParameter(SqlDbType.Int		, 4,ParameterDirection.Input)]int address_id,
							[SqlParameter(SqlDbType.VarChar	,50,ParameterDirection.Input)]string street1,
							[SqlParameter(SqlDbType.VarChar	,50,ParameterDirection.Input)]string street2,
							[SqlParameter(SqlDbType.VarChar	,50,ParameterDirection.Input)]string city,
							[SqlParameter(SqlDbType.VarChar	,10,ParameterDirection.Input)]string stateProvince,
							[SqlParameter(SqlDbType.VarChar	, 7,ParameterDirection.Input)]string postal_code,
							[SqlParameter(SqlDbType.VarChar	, 4,ParameterDirection.Input)]string zip4,
							[SqlParameter(SqlDbType.VarChar	,10,ParameterDirection.Input)]string country,
							[SqlParameter(SqlDbType.Int		, 4,ParameterDirection.Input)]int address_type,
							[SqlParameter(SqlDbType.Int		, 4,ParameterDirection.Input)]int AddressListID
							)
		{
			
			
			int rows;
			try
			{

				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{address_id,street1,street2,city,stateProvince,postal_code,zip4,country,address_type,AddressListID});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_upd_Address_by_id",aParams);
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
		///<summary>Select an Address object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_get_Address_by_type")]
		public DataTable Exists([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int type,
								[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AddressListId)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{type,AddressListId});

				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_Address_by_type",aParams);
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
