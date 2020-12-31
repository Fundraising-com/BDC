using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>UserProfileDataAccess</summary>
	public class UserProfileDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public UserProfileDataAccess(){}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.LogIn")]
		public DataTable LogIn(	[SqlParameter(SqlDbType.VarChar ,50,ParameterDirection.Input)] string UserName,
			[SqlParameter(SqlDbType.VarChar ,50,ParameterDirection.Input)] string Password)
		{

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{UserName,Password});
				return SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure,
						"QSPCanadaCommon.dbo.LogIn", aParams);
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
		}

	}
}
