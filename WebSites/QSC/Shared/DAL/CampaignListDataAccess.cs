using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>DAL file for class CampaignListDataAccess to access list of Campaign info.</summary>
	///<remarks>No Insert or Updates, only SELECT</remarks>
	public class CampaignListDataAccess : QDataAccess
	{
		///<summary>Default Constructor</summary>
		public CampaignListDataAccess(){}

		///<summary>Grabs a list of campaigns for a given account</summary>
		///<param name="AccountID">int: Which Account to look campaigns up for.</param>
		///<param name="FMID">*Optional* string to filter results by</param>
		///<returns>a DataTable of results</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_CampaignList")]
		public DataTable GetCampaignList(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountID,
			[SqlParameter(SqlDbType.VarChar,4,ParameterDirection.Input)]string FMID
			)
		{
			//fmid can be null here but not blank "" is bad.
			if(FMID.Trim() == "") { FMID = null; } 
			//string msg = "AccountID: |" + AccountID.ToString() + "| FMID: |" + FMID + "|";
			//throw new System.ArgumentException(msg);
			
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountID,FMID});
			DataTable dt = null;
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
								"QSPCanadaCommon.dbo.pr_get_CampaignList",aParams);
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

		///<summary>Grabs the header for a campaign list</summary>
		///<param name="AccountID">int: Which Account to look for.</param>
		///<returns>a DataTable of results</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_CampaignList_Header")]
		public DataTable GetCampaignListHeader([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountID)
		{
			
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountID});
			DataTable dt = null;
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_get_CampaignList_Header",aParams);
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
