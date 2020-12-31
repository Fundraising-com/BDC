using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	
	public class OrderHistoryDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public OrderHistoryDataAccess(){}

		///<summary>Select order history</summary>
		///<returns>DataTable of printed orders</returns>
		///
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_GetOrderHistory")]
		public DataTable GetOrderHistory   ([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pOrderID,
											[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pCampaignID,
											[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pAccountID,
											[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pOrderStatus,
											[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pOrderQualifier,
											[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pWareHouse,
											[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]string  @pFromDateReceived,
											[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]string  @pToDateReceived,
											[SqlParameter(SqlDbType.VarChar,4,ParameterDirection.Input)]string  @FMID
			
										)
		{
			DataTable dt=null;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@pOrderID,@pCampaignID,@pAccountID,
																			   @pOrderStatus, @pOrderQualifier, @pWareHouse,
																			   @pFromDateReceived,@pToDateReceived,@FMID });


				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_GetOrderHistory", aParams);
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
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_UpdateOrderPrintStatus")]
		public void UpdateOrderPrintStatus([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int @pOrderID)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@pOrderID});
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_UpdateOrderPrintStatus", aParams);
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

		///<summary>Force an order to be closed.</summary>
		///<param name="OrderID">int: OrderID</param>
		///<returns>Success or failure.</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_ForceCloseOrder")]
		public bool ForceCloseOrder([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int @iOrderID)
		{
			bool isValid = false;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@iOrderID});
				isValid = Convert.ToBoolean(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_ForceCloseOrder", aParams));
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

			return isValid;
		}

		///<summary>Close an order.</summary>
		///<param name="OrderID">int: OrderID</param>
		///<returns>Success or failure.</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_CloseOrder")]
		public bool CloseOrder([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int @OrderID)
		{
			bool isValid = false;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@OrderID});
				isValid = Convert.ToBoolean(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_CloseOrder", aParams));
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

			return isValid;
		}
		///<summary>Cancel an order.</summary>
		///<param name="OrderID">int: OrderID</param>
		///<returns>Success or failure.</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.DoCancelOrder")]
		public void CancelOrder([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int @OrderID)
		{
			//bool isValid = false;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@OrderID});
				SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.DoCancelOrder", aParams);
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

			//return isValid;
		}
	}
}
