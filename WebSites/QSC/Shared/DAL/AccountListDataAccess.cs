using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Gets Account List Info</summary>
	public class AccountListDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public AccountListDataAccess(){}

		///<summary>Select an AccountListDataAccess object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_AccountList")]
		public DataTable GetAccountList(	
			[SqlParameter(SqlDbType.VarChar	 ,4,ParameterDirection.Input)]string fmid,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int sAccountID,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string sAccountName,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string sCity,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string sState,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string sPostal,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string sFMFirstName,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string sFMLastName,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int fiscalYear,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int orderby
			)
		{
			DataTable dt = null;
			try
			{
				if((fmid != null)&&(fmid.Trim() == "")) { fmid = null; }
				if((sAccountName != null)&&(sAccountName.Trim() == "")) { sAccountName = null; }
				if((sCity != null)&&(sCity.Trim() == "")) { sCity = null; }
				if((sState != null)&&(sState.Trim() == "")) { sState = null; }
				if((sPostal != null)&&(sPostal.Trim() == "")) { sPostal = null; }

				if((sFMFirstName != null)&&(sFMFirstName.Trim() == "")) { sFMFirstName = null; }
				if((sFMLastName != null)&&(sFMLastName.Trim() == "")) { sFMLastName = null; }
				
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{fmid,sAccountID,sAccountName,sCity,sState,sPostal,sFMFirstName,sFMLastName,fiscalYear, orderby});

				dt = SqlHelper.ExecuteDataTable(connection, 
					CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_AccountList",aParams);
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
	