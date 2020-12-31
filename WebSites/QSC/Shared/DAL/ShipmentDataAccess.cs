using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Access warehouse shipment info.</summary>
	public class ShipmentDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public ShipmentDataAccess(){}

		///<summary>Select a Shipment object</summary>
		///<param name="Id">int: what shipment</param>
		///<returns>DataTable full of Shipment info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_JLC_test_GetShipment")]
		public DataTable GetShipmentByID([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int Id)
		{
			DataTable dt=null;
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{Id});
			
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_JLC_test_GetShipment",aParams);
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


		///<summary>Select Shipment objects</summary>
		///<returns>DataTable full of Shipment info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_JLC_test_GetShipments")]
		public DataTable GetShipments()
		{
			DataTable dt=null;
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{});
			
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_JLC_test_GetShipments",aParams);
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
		

		///<summary>Select all Carriers object</summary>
		///<returns>DataTable full of Carriers</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Get_Carriers")]
		public DataTable GetCarriers()
		{
			DataTable dt=null;
			
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_Carriers");
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

		///<summary>Select all Shippable Orders object</summary>
		///<returns>DataTable full of Orders</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Get_Orders_ReadyForShipment_V2")]
		public DataTable GetShippableOrders([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int Top,
                         [SqlParameter(SqlDbType.VarChar,512,ParameterDirection.Input)]string ProdLine,
                         [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int IndividualOrders,
						 [SqlParameter(SqlDbType.VarChar,500,ParameterDirection.Input)]string OrderList,
						 [SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int PrintStatus,
						 [SqlParameter(SqlDbType.VarChar,500,ParameterDirection.Input)]string ProductCode,
						 [SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int BackOrderOnly,
                         [SqlParameter(SqlDbType.Int, ParameterDirection.Input)]int ShipmentGroupID
                         )
        {
			DataTable dt=null;
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{Top, ProdLine, IndividualOrders,OrderList,PrintStatus,ProductCode,BackOrderOnly,ShipmentGroupID});
			
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_Orders_ReadyForShipment_V2", aParams);
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

		///<summary>Select a Order with Shipment information object</summary>
		///<param name="Id">int: what Batch OrderId</param>
		///<returns>DataTable full of Shipment info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Get_Order_ReadyForShipment")]
		public DataTable GetShipmentInfoByOrderID([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId)
		{
			DataTable dt=null;
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{OrderId});
			
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_Order_ReadyForShipment",aParams);
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

		///<summary>Select a Order with Shipment Variation information object</summary>
		///<param name="OrderId">int: what Batch OrderId</param>
		///<param name="SessionId">int: what Session Id</param>
		///<returns>DataTable full of Shipment Variation info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Get_Order_ShipmentDetailVariations")]
		public DataTable GetShipmentVariationInfo([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int OrderId, [SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string SessionId,
            [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int? ShipmentGroupID)
		{
			DataTable dt=null;
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{OrderId, SessionId, ShipmentGroupID});
			
			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Get_Order_ShipmentDetailVariations",aParams);
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


		///<summary>Insert A Single Shipment Variation </summary>
		///<param name="SessionId">string: what Session Id</param>
		///<param name="CustomerOrderHeaderInstance">int: what CustomerOrderHeaderId</param>
		///<param name="TransId">int: what Trans Id</param>
		///<param name="QuantityShipped">int: Quantity Shipped</param>
		///<param name="QuantityReplaced">int: Quantity To Replace</param>
		///<param name="ReplacementItemId">int: what Item To Replace Item With</param>
		///<param name="ShipTF">boolean: To Ship or not To Ship, that is the question</param>
		///<param name="Comment">string: Comment</param>
		///<param name="CustomerComment">string: Customer Comment</param>
		///<param name="ModifiedBy">string: whose doing the insert</param>		
		///<returns>Nothing</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Insert_Shipment_Variation")]
		public void InsertShipmentVariation(
			[SqlParameter(SqlDbType.VarChar,100,ParameterDirection.Input)]string SessionId,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CustomerOrderHeaderInstance, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int TransId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int QuantityShipped, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int QuantityReplaced, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ReplacementItemId, 
			[SqlParameter(SqlDbType.Bit,4,ParameterDirection.Input)]bool ShipTF, 
			[SqlParameter(SqlDbType.VarChar,255,ParameterDirection.Input)]string Comment,
			[SqlParameter(SqlDbType.VarChar,255,ParameterDirection.Input)]string CustomerComment,
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string ModifiedBy
			
			)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{SessionId, CustomerOrderHeaderInstance, TransId, QuantityShipped, QuantityReplaced, ReplacementItemId, ShipTF, Comment, CustomerComment, ModifiedBy});
			
			try
			{
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Insert_Shipment_Variation",aParams);
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


		///<summary>Delete All Shipment Variations For A Certain Session </summary>
		///<param name="SessionId">string: what Session Id</param>
		///<returns>Nothing</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Delete_ShipmentVariations")]
		public void DeleteShipmentVariations(
			[SqlParameter(SqlDbType.VarChar,100,ParameterDirection.Input)]string SessionId)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{SessionId});
			
			try
			{
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Delete_ShipmentVariations",aParams);
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


		///<summary>Ship a Batch Order or group of batches </summary>
		///<param name="OrderIds">string: what batchs to ship</param>
		///<param name="DistCenterId">int: what Distribution Center Id</param>
		///<param name="CarrierId">int: what carrier</param>
		///<param name="ShipDate">string: Datetime that shipment was made</param>
		///<param name="ExpectedDeliveryDate">string: Date of expected delivery</param>
		///<param name="NumberOfBoxes">int: # of boxes in shipment</param>
		///<param name="Weight">numeric: Weight of shipment</param>
		///<param name="NumberOfSkids">int: number of skids</param>
		///<param name="WeightUnitOfMeasure">string: Weight Unit</param>
		///<param name="Comment">string: comment on shipment</param>
		///<param name="UserId">int: userid of shipper</param>
		///<param name="SessionId">string: ASP.NET SessionId</param>
		///<returns>DataTable with ShipmentId</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_ShipBatch")]
		public int ShipBatches(
			[SqlParameter(SqlDbType.VarChar,512,ParameterDirection.Input)]string OrderIds,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int DistCenterId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CarrierId, 
			[SqlParameter(SqlDbType.DateTime,8,ParameterDirection.Input)]string ShipDate, 
			[SqlParameter(SqlDbType.DateTime,8,ParameterDirection.Input)]string ExpectedDeliveryDate, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int NumberOfBoxes, 
			[SqlParameter(SqlDbType.Money,8,ParameterDirection.Input)]float Weight, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int NumberOfSkids, 
			[SqlParameter(SqlDbType.VarChar,100,ParameterDirection.Input)]string WeightUnitOfMeasure,
			[SqlParameter(SqlDbType.VarChar,255,ParameterDirection.Input)]string Comment,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int UserId,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Output)]int ShipmentId,
            [SqlParameter(SqlDbType.VarChar,100,ParameterDirection.Input)]string SessionId,
            [SqlParameter(SqlDbType.VarChar,512,ParameterDirection.Input)]string ProdLine,
            [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int? ShipmentGroupID

            )
        {
			
			SqlParameter[] aParams = CreateSqlParameters(null,
                new object[]{OrderIds, DistCenterId, CarrierId, ShipDate, ExpectedDeliveryDate, NumberOfBoxes, 
                                Weight, NumberOfSkids, WeightUnitOfMeasure, Comment,  UserId, ShipmentId,SessionId,
                                ProdLine, ShipmentGroupID});
			
			try
			{
				DataTable oDT = new DataTable();

				oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_ShipBatch",aParams);
				
				return (int)aParams[11].Value;
				
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

        [DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_ShipOrderItem")]
        public int ShipOrderItem(
            [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CustomerOrderHeaderInstance,
            [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int TransID,
            [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int DistCenterId, 
            [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CarrierId, 
            [SqlParameter(SqlDbType.DateTime,8,ParameterDirection.Input)]string ShipDate, 
            [SqlParameter(SqlDbType.DateTime,8,ParameterDirection.Input)]string ExpectedDeliveryDate, 
            [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int NumberOfBoxes, 
            [SqlParameter(SqlDbType.Money,8,ParameterDirection.Input)]float Weight, 
            [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int NumberOfSkids, 
            [SqlParameter(SqlDbType.VarChar,100,ParameterDirection.Input)]string WeightUnitOfMeasure,
            [SqlParameter(SqlDbType.VarChar,255,ParameterDirection.Input)]string Comment,
            [SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int UserId,
            [SqlParameter(SqlDbType.Int,4,ParameterDirection.Output)]int ShipmentId,
            [SqlParameter(SqlDbType.VarChar,100,ParameterDirection.Input)]string SessionId
            
            )
        {
			
            SqlParameter[] aParams = CreateSqlParameters(null,
                new object[]{CustomerOrderHeaderInstance, TransID, DistCenterId, CarrierId, ShipDate, ExpectedDeliveryDate, NumberOfBoxes, 
                                Weight, NumberOfSkids, WeightUnitOfMeasure, Comment,  UserId, ShipmentId,SessionId});
			
            try
            {
                DataTable oDT = new DataTable();

                oDT = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
                    "QSPCanadaOrderManagement.dbo.pr_ShipOrderItem",aParams);
				
                return (int)aParams[12].Value;
				
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



		///<summary>Split a Customer Order Detail </summary>
		///<param name="CustomerOrderHeaderInstance">int: what CustomerOrderHeaderId</param>
		///<param name="TransId">int: what Trans Id</param>
		///<param name="SplitQuantity">int: Quantity to Split the new detail item to</param>
		///<param name="ModifiedBy">string: whose doing the insert</param>		
		///<returns>Nothing</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Split_COD")]
		public void SplitCOD(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CustomerOrderHeaderInstance, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int TransId, 
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int SplitQuantity, 
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string ModifiedBy
			
			)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{CustomerOrderHeaderInstance, TransId, SplitQuantity, ModifiedBy});
			
			try
			{
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Split_COD",aParams);
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


		///<summary>Insert A Shipment Waybill </summary>
		///<param name="ShipmentId">int: what Shipment Id</param>
		///<param name="WaybillNumber">string: WayBill Number</param>
		///<returns>Nothing</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Insert_Shipment_Waybill")]
		public void InsertShipmentWaybill(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ShipmentId, 
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string WaybillNumber			
			)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ShipmentId, WaybillNumber});
			
			try
			{
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.pr_Insert_Shipment_Waybill",aParams);
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
