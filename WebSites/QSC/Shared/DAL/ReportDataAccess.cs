using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Access warehouse shipment info.</summary>
	public class ReportDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public ReportDataAccess(){}
  

  
		///<summary>Select all printers object</summary>
		///<returns>DataTable of printers</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement..pr_GetAllPrinters")]
		public DataTable GetAllPrinters()
		{
			DataTable dt=null;
   
			dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement..pr_GetAllPrinters", null);
			//   try
			//   {
			//   }
			//   catch(InvalidOperationException)
			//   {
			//   }
			//   catch(SqlException)
			//   {
			//   }
			//   catch(Exception)
			//   {
			//   }
			return dt;
		}

		///<summary>Insert A Report Request Detail </summary>
		///<param name="ReportRequestId">int: what ReportRequestId</param>
		///<param name="ReportId">int: what Report Id</param>
		///<param name="ModifiedBy">string: whose doing the insert</param>  
		///<returns>Nothing</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Insert_ReportRequestDetail")]
		public void InsertReportRequestDetail(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ReportRequestId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ReportId, 
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string ModifiedBy
			)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ReportRequestId, ReportId, ModifiedBy});
   
			try
			{
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Insert_ReportRequestDetail",aParams);
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

		///<summary>Insert A Report Combination </summary>
		///<param name="ReportRequestId">int: what ReportRequestId</param>
		///<param name="CombinedReportRequestId">int: what Report The Source Reports are being combined into</param>
		///<returns>Nothing</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Insert_ReportCombination")]
		public void InsertReportCombination(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ReportRequestId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CombinedReportRequestId 
			)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ReportRequestId, CombinedReportRequestId});
   
			try
			{
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Insert_ReportCombination",aParams);
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




		///<summary>Insert A Report Request </summary>
		///<param name="BatchOrderId">int: The Order Id of the Batch</param>
		///<param name="ReportTypeId">int: what Report Type Id</param>
		///<param name="RSSubscriptionId">int: The RS Subscripiton Id.  Just enter 0.  This will be updated after the call to RS.</param>
		///<param name="ModifiedBy">string: who is doing the insert</param>
		///<param name="ReportRequestId">int: not used by call, just enter 0.</param>
		///<returns>Int with ReportRequestId</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_ShipBatch")]
		public int InsertReportRequest(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int BatchOrderId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ReportTypeId, 
			[SqlParameter(SqlDbType.VarChar,100,ParameterDirection.Input)]string RSSubscriptionId,
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string ModifiedBy,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Output)]int ReportRequestId
			)
		{
   
   
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{BatchOrderId, ReportTypeId, RSSubscriptionId, ModifiedBy, ReportRequestId});
   
			try
			{
				DataTable oDT = new DataTable();

				oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Insert_ReportRequest",aParams);
    
				return (int)aParams[4].Value;
    
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

		///<summary>Insert A Print Request </summary>
		///<param name="ReportRequestId">int: The Report Request Id</param>
		///<param name="PrinterId">int: what Printer to print to</param>
		///<param name="PrintRequestStatudId">int: The initial status of the print job.</param>
		///<param name="ModifiedBy">string: who is doing the insert</param>
		///<param name="PrintRequestId">int: not used by call, just enter 0.</param>
		///<returns>Int with PrintRequestId</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Insert_Print_Request")]
		public int InsertPrintRequest(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ReportRequestId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int PrinterId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int PrintRequestStatusId, 
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string ModifiedBy,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Output)]int PrintRequestId
			)
		{
   
   
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ReportRequestId, PrinterId, PrintRequestStatusId, ModifiedBy, PrintRequestId});
   
			try
			{
				DataTable oDT = new DataTable();

				oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Insert_Print_Request",aParams);
    
				return (int)aParams[4].Value;
    
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



		///<summary>Update SubscriptionId for Report Request</summary>
		///<param name="ReportRequestId">int: what ReportRequestId</param>
		///<param name="RSSubscriptionId">string: what RS Subscription Id</param>
		///<param name="ModifiedBy">string: whose doing the insert</param>  
		///<returns>Nothing</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Update_ReportRequest_SubscriptionId")]
		public void UpdateSubscriptionId(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ReportRequestId, 
			[SqlParameter(SqlDbType.VarChar,100, ParameterDirection.Input)]string RSSubscriptionId
			)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ReportRequestId, RSSubscriptionId});
   
			try
			{
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Update_ReportRequest_SubscriptionId",aParams);
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

		///<summary>Batch Order Report Status</summary>
		///<param name="BatchOrderId">int: The Order Id of the Batch</param>
		///<returns>DT full of Report History for given order</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Select_BatchReportStatus")]
		public DataTable BatchOrderReportStatus(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int BatchOrderId,
			[SqlParameter(SqlDbType.VarChar ,4,ParameterDirection.Input)]string FMID
			)
		{  
   
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{BatchOrderId, FMID});
   
			try
			{
				DataTable oDT = new DataTable();

				oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Select_BatchReportStatus",aParams);
    
				return oDT;
    
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


		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Select_BatchReportsInfo")]
		public DataTable BatchOrderReportsInfo(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int BatchOrderId,
            [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int? ShipmentGroupID,
            [SqlParameter(SqlDbType.VarChar ,4,ParameterDirection.Input)]string FMID
			)
		{  
   
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{BatchOrderId, ShipmentGroupID, FMID});
   
			try
			{
				DataTable oDT = new DataTable();

				oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Select_BatchReportsInfo",aParams);
    
				return oDT;
    
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








		///<summary>Batch Order Report Status</summary>
		///<param name="BatchOrderId">int: The Order Id of the Batch</param>
		///<returns>DT full of Report History for given order</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_GetQSPCAReportByAccountID")]
		public DataTable GetQSPCAReportByAccountID(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountID,
			[SqlParameter(SqlDbType.VarChar ,4,ParameterDirection.Input)]string FMID
			)
		{  
   
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountID, FMID});
   
			try
			{
				DataTable oDT = new DataTable();

				oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_GetQSPCAReportByAccountID",aParams);
    
				return oDT;
    
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
        
		///<summary>Batch Order Report Status</summary>
		///<param name="BatchOrderId">int: The Order Id of the Batch</param>
		///<returns>DT full of Report History for given order</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_GetQSPCAReportByCampaignID")]
		public DataTable GetQSPCAReportByCampaignID(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.VarChar ,4,ParameterDirection.Input)]string FMID
			)
		{  
   
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{CampaignID, FMID});
   
			try
			{
				DataTable oDT = new DataTable();

				oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_GetQSPCAReportByCampaignID",aParams);
    
				return oDT;
    
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
        
		///<summary>Batch Order Report Status</summary>
		///<param name="BatchOrderId">int: The Order Id of the Batch</param>
		///<returns>DT full of Report History for given order</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_GetQSPCAReportByAccountName")]
		public DataTable GetQSPCAReportByAccountName(
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string AccountName,
			[SqlParameter(SqlDbType.VarChar ,4,ParameterDirection.Input)]string FMID
			)
		{  
   
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountName, FMID});
   
			try
			{
				DataTable oDT = new DataTable();

				oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_GetQSPCAReportByAccountName",aParams);
    
				return oDT;
    
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
