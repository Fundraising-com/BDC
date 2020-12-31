using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for BillShipAccountData.
	/// </summary>
	public class BillShipAccountData : QDataAccess
	{
		///<summary>default constructor</summary>
		public BillShipAccountData(){}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.GetAccountInfo")]
		public DataTable Exists([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int CampaignId
			)
		{
			DataTable dt = null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, new object[]{CampaignId});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure,
					"QSPCanadaOrderManagement.dbo.GetAccountInfo" , aParams);
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

		public bool Delete(int ID)
		{
			bool bOk = false;
			return bOk;
		}

	}
}
