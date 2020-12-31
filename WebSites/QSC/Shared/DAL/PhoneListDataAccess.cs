using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>DAL file for class PhoneListDataAccess to access Phone List info.</summary>
	public class PhoneListDataAccess : QDataAccess
	{
		public PhoneListDataAccess()
		{

		}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_get_PhoneList")]
		public DataTable GetPhoneListByID([SqlParameter(SqlDbType.Int ,4,ParameterDirection.Input)] int ListId)
		{
			DataTable PhoneList = null;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ListId});
				PhoneList = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_PhoneList",aParams);
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
			return PhoneList;
		}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_get_PhoneListNextID")]
		public int GetPhoneListNextID()
		{
			Object result = null;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{});
				result = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_PhoneListNextID",aParams);
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
			return Convert.ToInt32(result);
		}
	}
}
