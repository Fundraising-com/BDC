using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// DAL file for class AddressListDataAccess to access AddressList info.
	/// </summary>
	public class AddressListDataAccess : QDataAccess
	{
		public AddressListDataAccess()
		{

		}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_get_AddressList")]
		public DataTable GetAddressListByID([SqlParameter(SqlDbType.Int ,4,ParameterDirection.Input)] int ListId)
		{
			DataTable AddressList = null;
			
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ListId});
				AddressList = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_AddressList",aParams);
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
			return AddressList;
		}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_get_AddressListNextID")]
		public int GetAddressListNextID()
		{
			Object result = null;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{});
				result = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_AddressListNextID",aParams);
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
