using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for BillShipAccountData.
	/// </summary>
	public class GiftOrderReturnedData : QDataAccess
	{
		///<summary>default constructor</summary>
		public GiftOrderReturnedData(){}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.GetGiftOrderDetail")]
		public DataTable Exists([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int OrderId
			)
		{
			DataTable dt = null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, new object[]{OrderId});

				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.GetGiftOrderDetail" , aParams);
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
//
	public class BatchOrderHeaderData : QDataAccess
	{
      
		public BatchOrderHeaderData()
		{
        
		}
		#region CRUD Commands
		/// <summary>
		/// Insert Batch and order header
		/// </summary>
		/// 	
		
		
		
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.CreateBatchAndOrderHeader")]
		public bool InsertBatchOrderHeader( [SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)] DateTime batchdate,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int billtoacctid,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int shiptoacctid,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int campaignid,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int status,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int ordertypecode,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int orderqualifierid,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int customerinstance,
			[SqlParameter(SqlDbType.VarChar,ParameterDirection.Input)]string ChangeUserId,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Output)]out int orderid,
		    [SqlParameter(SqlDbType.Int,ParameterDirection.Output)]out int coh
			)
		{
			orderid =-1;
			coh = -1;
			try
			{                
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{batchdate,billtoacctid,shiptoacctid,campaignid,status,ordertypecode,orderqualifierid,customerinstance,ChangeUserId,orderid,coh});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.CreateBatchAndOrderHeader",aParams);

               
				orderid = (int)aParams[9].Value;
				coh	=	  (int)aParams[10].Value;
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
			return true;
		}
	


		#endregion
	}

	//Order  Detail 

	public class OrderDetailData : QDataAccess
	{
      
		public OrderDetailData()
		{
        
		}
		#region CRUD Commands
		/// <summary>
		/// Insert  order detail
		/// </summary>
		/// 	
		
		
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.CreateDetailItemForReturn")]
		public bool InsertOrderItemDetail( 
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int coh,
			[SqlParameter(SqlDbType.VarChar,ParameterDirection.Input)]string productcode,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int quantity,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] double price,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] double catalogprice,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int producttype,
			[SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int status

			)
		{
			
			try
			{                
				SqlParameter[] aParams = CreateSqlParameters(null, 
					new object[]{coh,productcode,quantity,price,catalogprice,producttype,status});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.CreateDetailItemForReturn",aParams);

				//orderid = (int)aParams[9].Value;
				//coh	=	  (int)aParams[10].Value;
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
			return true;
		}
	


		#endregion
	}

	public class CustomerAccData : QDataAccess
	{

		public CustomerAccData(){}

		#region CRUD Commands
		/// <summary>
		/// Insert a Bank Deposit Item object
		/// </summary>	 
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.CreateCustomerAccount")]
		public bool InsertCustomer( [SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int accountID,
			[SqlParameter(SqlDbType.VarChar,ParameterDirection.Input)]string ChangeUserId,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Output)]out int customerinstance
			)
		{
			customerinstance =-1;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{accountID,ChangeUserId,customerinstance});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.CreateCustomerAccount",aParams);

				customerinstance = (int)aParams[2].Value;
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
			return true;
		}
	


		#endregion
	}
}
