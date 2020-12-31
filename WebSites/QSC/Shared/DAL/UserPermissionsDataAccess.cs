using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>UserPermissionsDataAccess</summary>
	public class UserPermissionsDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public UserPermissionsDataAccess(){}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_get_UserPermissions")]
		public DataTable GetUserRoles([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ProfileID)
		{
			/*
			//By setting this as null
			//we get all permissions belonging to a particular user
			SqlParameter PName = new SqlParameter("@PName", SqlDbType.VarChar,30);
			PName.Direction = ParameterDirection.Input;
			//PName.set_IsNullable(true);
			PName.Value = null;
			*/

			DataTable dt=null;
			try
			{
				//SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ProfileID,PName});
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ProfileID});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_get_UserPermissions", aParams);
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

