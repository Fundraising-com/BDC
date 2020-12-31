using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for OrderStageTrackingDataAccess.
	/// </summary>
	public class OrderStageTrackingDataAccess : QDataAccess
	{
		public OrderStageTrackingDataAccess()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.GetTrackingOrderDetail")]
		//public DataTable GetTrackingOrderDetail([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId)
		public DataTable GetTrackingOrderDetail(		
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountId ,
			[SqlParameter(SqlDbType.VarChar,50 ,ParameterDirection.Input)]string AccountName,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignId,
			[SqlParameter(SqlDbType.VarChar,4 ,ParameterDirection.Input)]string FMId,
			[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]DateTime DateFrom ,
			[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]DateTime DateTo,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId ,
			[SqlParameter(SqlDbType.VarChar,5,ParameterDirection.Input)]string OrderStatus,
         [SqlParameter(SqlDbType.Bit, 1, ParameterDirection.Input)]bool ShowOrdersPastStage,
         [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderQualifierID,
         [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int ProductType
         )
		{
			DataTable dt ;
					
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{AccountId,AccountName,CampaignId,FMId,DateFrom,DateTo,OrderId,OrderStatus, ShowOrdersPastStage, OrderQualifierID, ProductType});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.GetTrackingOrderDetail",aParams);

				
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

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.GetAllOrderTrackingFiles")]
		public DataTable GetAllOrderTrackingFiles(
									[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountId ,
									[SqlParameter(SqlDbType.VarChar,50 ,ParameterDirection.Input)]string AccountName,
									[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignId,
									[SqlParameter(SqlDbType.VarChar,4 ,ParameterDirection.Input)]string FMId,
									[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]DateTime DateFrom ,
									[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]DateTime DateTo,
									[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId ,
									[SqlParameter(SqlDbType.VarChar,5,ParameterDirection.Input)]string OrderStatus,
			                  [SqlParameter(SqlDbType.Bit,1,ParameterDirection.Input)]bool ShowOrdersPastStage,
                           [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderQualifierID,
                           [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int ProductType
									)
		{
			DataTable dt ;
			
			
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{AccountId,AccountName,CampaignId,FMId,DateFrom,DateTo,OrderId,OrderStatus,ShowOrdersPastStage,OrderQualifierID,ProductType});
							
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.GetAllOrderTrackingFiles",aParams);

				
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
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.GetTrackingFileDetail")]
		public DataTable GetTrackingFileDetail(		
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountId ,
			[SqlParameter(SqlDbType.VarChar,50 ,ParameterDirection.Input)]string AccountName,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignId,
			[SqlParameter(SqlDbType.VarChar,4 ,ParameterDirection.Input)]string FMId,
			[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]DateTime DateFrom ,
			[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]DateTime DateTo,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId ,
			[SqlParameter(SqlDbType.VarChar,5,ParameterDirection.Input)]string OrderStatus,
         [SqlParameter(SqlDbType.Bit, 1, ParameterDirection.Input)]bool ShowOrdersPastStage,
         [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int OrderQualifierID,
         [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int ProductType
			 )
		{
			DataTable dt;
			
			
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{AccountId,AccountName,CampaignId,FMId,DateFrom,DateTo,OrderId,OrderStatus,ShowOrdersPastStage,OrderQualifierID,ProductType});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.GetTrackingFileDetail",aParams);

				
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
