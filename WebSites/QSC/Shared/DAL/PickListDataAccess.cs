using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>PickListDataAccess</summary>
	public class PickListDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public PickListDataAccess(){}

		///<summary>Select unpicked orders object</summary>
		///<param name="DistributionCenterId">int: what Distribution Center</param>
		///<returns>DataTable of unpicked orders</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_get_UnpickedOrders")]
		public DataTable GetUnpickedOrders([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int DistributionCenterId)
		{
			DataTable dt=null;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{DistributionCenterId});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_Get_UnpickedOrders", aParams);
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

		///<summary>Select unpicked orders from list object</summary>
		///<param name="DistributionCenterId">int: what Distribution Center</param>
		///<returns>DataTable of unpicked orders</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_get_UnpickedOrders_ByList")]
		public DataTable GetUnpickedOrdersByList([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int DistributionCenterId, [SqlParameter(SqlDbType.VarChar,255,ParameterDirection.Input)]string OrderIdList)
		{
			DataTable dt=null;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{DistributionCenterId, OrderIdList});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_Get_UnpickedOrders_ByList", aParams);
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

		///<summary>Select order dc breakdown object</summary>
		///<param name="DistributionCenterId">int: what Distribution Center</param>
		///<param name="OrderId">int: what OrderId or Batch</param>
		///<returns>DataTable of breakdown information for order by distribution center</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Get_Order_DC_Breakdown")]
		public DataTable GetOrderBreakdown([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int DistributionCenterId, [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{DistributionCenterId, OrderId});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_Get_Order_DC_Breakdown", aParams);
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

		///<summary>Insert BatchDistributionCenter object</summary>
		///<param name="DistributionCenterId">int: what Distribution Center</param>
		///<param name="OrderId">int: what OrderId or Batch</param>
		///<returns>Nada</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Insert_BatchDistributionCenter")]
		public void InsertBatchDistributionCenter([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int DistributionCenterId, [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{DistributionCenterId, OrderId});
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_Insert_BatchDistributionCenter", aParams);
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

		///<summary>Process Reserve Quantities and update status</summary>
		///<param name="DistributionCenterId">int: what Distribution Center</param>
		///<param name="OrderId">int: what OrderId or Batch</param>
		///<returns>Nada</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_ProcessReserveQuantities_ByOrderId")]
		public void ReserveQuantitiesAndUpdateStatus([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int DistributionCenterId, [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId)
		{

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{DistributionCenterId, OrderId});
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_ProcessReserveQuantities_ByOrderId", aParams);
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

		///<summary>Select mag queue object</summary>
		///<returns>DataTable of mag queue orders</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Get_OrdersInMagQueue")]
		public DataTable GetMagQueueOrders()
		{
			DataTable dt=null;
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_Get_OrdersInMagQueue");
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

		///<summary>Select mag queue orders from list object</summary>
		///<param name="OrderIdList">string: what Order Ids (comma seperated)</param>
		///<returns>DataTable of unpicked orders</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_get_MagQueueOrders_ByList")]
		public DataTable GetMagQueueOrdersByList([SqlParameter(SqlDbType.VarChar,255,ParameterDirection.Input)]string OrderIdList)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{OrderIdList});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_Get_MagQueueOrders_ByList", aParams);
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

		///<summary>update magazine queue status</summary>
		///<param name="OrderId">int: what OrderId or Batch</param>
		///<returns>Nada</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_ProcessMagQueue_ByBatchId")]
		public void ProcessMagQueue([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{OrderId});
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_ProcessMagQueue_ByBatchId", aParams);
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
