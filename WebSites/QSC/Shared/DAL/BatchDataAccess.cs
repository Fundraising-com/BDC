using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// DAL file for class BatchDataAccess to access Batch info.
	/// </summary>
	public class BatchDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public BatchDataAccess(){}

		///<summary>Select an Order object</summary>
		///<param name="Id">int: what Order</param>
		///<returns>DataTable full of Order info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_get_Batch_ByOrderId")]
        public DataTable GetByOrderId([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int BatchOrderId,
            [SqlParameter(SqlDbType.VarChar	,4,ParameterDirection.Input)]string FMID)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{BatchOrderId, FMID});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_get_Batch_ByOrderId",aParams);
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

        [DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_get_Batch_ByAccountId")]
        public DataTable GetBatchesByAccountId([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountID,
            [SqlParameter(SqlDbType.VarChar	,4,ParameterDirection.Input)]string FMID)
        {
            DataTable dt=null;
            try
            {
                SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountID, FMID});
                dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
                    "QSPCanadaOrderManagement.dbo.pr_get_Batch_ByAccountId",aParams);
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

        
        [DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_get_Batch_ByAccountName")]
        public DataTable GetBatchesByAccountName([SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string name,
            [SqlParameter(SqlDbType.VarChar	,4,ParameterDirection.Input)]string FMID)
        {
            DataTable dt=null;
            try
            {
                SqlParameter[] aParams = CreateSqlParameters(null,new object[]{name, FMID});
                dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
                    "QSPCanadaOrderManagement.dbo.pr_get_Batch_ByAccountName",aParams);
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
		///<summary>Select all info on an Order object</summary>
		///<param name="Id">int: what Order</param>
		///<returns>DataTable full of Order info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_get_BatchDetails_ByOrderId")]
		public DataTable GetDetailsByOrderId([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int BatchOrderId)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{BatchOrderId});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_get_BatchDetails_ByOrderId",aParams);
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

		///<summary>Select all COH records for a batch</summary>
		///<param name="BatchOrderId">int: what batch</param>
		///<returns>DataTable full of account info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Get_COH_By_BatchOrderId")]
		public DataTable GetCOHByOrderId([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int BatchOrderId)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{BatchOrderId});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_COH_By_BatchOrderId",aParams);
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

		///<summary>Select a specific COH record for a specific batch</summary>
		///<param name="CustomerOrderHeaderId">int: what COH</param>
		///<returns>DataTable full of COH info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Get_COH_By_COHId")]
		public DataTable GetCOHByCOHId([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CustomerOrderHeaderId)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{CustomerOrderHeaderId});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_COH_By_COHId",aParams);
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

		///<summary>Select all COD records for a COH</summary>
		///<param name="COHId">int: what COH</param>
		///<returns>DataTable full of COD info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Get_CODs_By_COHId")]
		public DataTable GetCODsByCOHId([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int COHId)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{COHId});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_CODs_By_COHId",aParams);
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

		///<summary>Select a specific COD record for a specific batch Order</summary>
		///<param name="COHId">int: what COH</param>
		///<param name="TransId">int: what Record</param>
		///<returns>DataTable full of COD info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Get_COD_By_COHId_TransId")]
		public DataTable GetCODByCOHIdTransId(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int COHId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int TransId)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{COHId, TransId});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_COD_By_COHId_TransId",aParams);
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




		
		///<summary>Select Order object(s) by campaign</summary>
		///<param name="CampaignID">int: what Campaign to get orders from</param>
		///<param name="FMID">string: who is making this request</param>
		///<returns>DataTable full of Order info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Get_Batch_ByCampaignID")]
		public DataTable GetBatchesByCampaignID(
			[SqlParameter(SqlDbType.Int     ,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.VarChar	,4,ParameterDirection.Input)]string FMID
			)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{CampaignID, FMID});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_Batch_ByCampaignID",aParams);
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
