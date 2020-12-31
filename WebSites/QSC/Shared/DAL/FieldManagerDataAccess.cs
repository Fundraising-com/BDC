using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for FieldManagerDataAccess.
	/// </summary>
	public class FieldManagerDataAccess : QDataAccess
	{
		public FieldManagerDataAccess()
		{

		}

		///<summary>Insert a FieldManager object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_ins_FieldManager")]
		public bool Insert(	
			[SqlParameter(SqlDbType.VarChar	 ,4,ParameterDirection.Output)]out string FMID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int Status,
			[SqlParameter(SqlDbType.VarChar	 ,2,ParameterDirection.Input)]string Country,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int PhoneListID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int AddressListID,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string FirstName,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string LastName,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string MiddleInitial,
			[SqlParameter(SqlDbType.VarChar	 ,60,ParameterDirection.Input)]string Email,
			[SqlParameter(SqlDbType.VarChar	 ,4,ParameterDirection.Input)]string DMID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int UserIDModified,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime DateModified,
			[SqlParameter(SqlDbType.VarChar	 ,256,ParameterDirection.Input)]string Comment,
			[SqlParameter(SqlDbType.Bit		 ,ParameterDirection.Input)]bool DMIndicator,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Lang
			)
		{
			FMID = "";
			PhoneListID = -1;
			AddressListID = -1;

			int rows;
			
			try
			{
				SqlCommand sqlCmd = CreateSqlParametersCommand(null,
					new object[]{FMID,Status,Country,PhoneListID,AddressListID,FirstName,LastName,MiddleInitial,Email,DMID,UserIDModified,DateModified,Comment,DMIndicator,Lang});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_ins_FieldManager",sqlCmd);

				FMID    		= Convert.ToString(sqlCmd.Parameters["@FMID"].Value);
				PhoneListID		= Convert.ToInt32( sqlCmd.Parameters["@PhoneListID"].Value);
				AddressListID	= Convert.ToInt32( sqlCmd.Parameters["@AddressListID"].Value);

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
			
			if(rows != 3)//inserted a PhoneListID row, AddressListID row, and a FieldManager row
			{
				string msg = rows.ToString() + " rows were inserted, we needed exactly 3 to go in";
				throw new System.ApplicationException(msg);
			}
			else
			{				
				return true;
			}
		}

	
		///<summary>Select a FieldManager object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_FieldManager")]
		public DataTable Select([SqlParameter(SqlDbType.VarChar,4,ParameterDirection.Input)]string FMID)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{FMID});

				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_get_FieldManager",aParams);
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

	
		///<summary>Update a FieldManager object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_FieldManager")]
		public bool Update(	
			[SqlParameter(SqlDbType.VarChar	 ,4,ParameterDirection.Input)]string FMID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int Status,
			[SqlParameter(SqlDbType.VarChar	 ,2,ParameterDirection.Input)]string Country,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string FirstName,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string LastName,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string MiddleInitial,
			[SqlParameter(SqlDbType.VarChar	 ,60,ParameterDirection.Input)]string Email,
			[SqlParameter(SqlDbType.VarChar	 ,4,ParameterDirection.Input)]string DMID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int UserIDModified,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime DateModified,
			[SqlParameter(SqlDbType.VarChar	 ,256,ParameterDirection.Input)]string Comment,
			[SqlParameter(SqlDbType.Bit		 ,ParameterDirection.Input)]bool DMIndicator,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Lang
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{FMID,Status,Country,FirstName,LastName,MiddleInitial,Email,DMID,UserIDModified,DateModified,Comment,DMIndicator,Lang});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "pr_upd_FieldManager",aParams);
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
			return true;
		}


		///<summary>Get a list of FM info</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_FieldManager_List")]
		public DataTable GetFM_List([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int Mode)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{Mode});

				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_get_FieldManager_List",aParams);
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


		///<summary>Delete a FieldManager object</summary>
		///<param name="FMID"></param>
		///<returns>bool: was it done ? </returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_del_FieldManager")]
		public bool Delete([SqlParameter(SqlDbType.VarChar,4,ParameterDirection.Input)]string FMID)
		{
			int rows = -1;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{FMID});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_del_FieldManager",aParams);
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
			
			if(rows ==1)
			{
				return true;
			}
			else
			{				
				return false;
			}
		}
	}
}




