using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>PickListDataAccess</summary>
	public class PrintDocsDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public PrintDocsDataAccess(){}

		///<summary>Select unpicked orders object</summary>
		///<param name="DistributionCenterId">int: what Distribution Center</param>
		///<returns>DataTable of unpicked orders</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_Get_OrdersTobePrinted")]
		public DataTable Get_OrdersTobePrinted([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pOrderID,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pCampaignID,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pAccountID,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int @pOrderQualifier,
			[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]string  @pFromDateReceived,
			[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]string  @pToDateReceived,
            [SqlParameter(SqlDbType.Int, ParameterDirection.Input)]int @pShipmentGroupID,
            [SqlParameter(SqlDbType.Int, ParameterDirection.Input)]bool? @pHasShipment)
		{
			DataTable dt=null;
			
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@pOrderID,@pCampaignID,@pAccountID,
																				  @pOrderQualifier,
																				  @pFromDateReceived,@pToDateReceived,@pShipmentGroupID,@pHasShipment});

				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_Get_OrdersTobePrinted", aParams);
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
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_GetFilesForPrint")]
		public DataTable GetFilesForPrint([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int @pOrderID,
            [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int? @pShipmentGroupID)
		{
			DataTable dt=null;

			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@pOrderID,pShipmentGroupID});

                dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.pr_GetFilesForPrint", aParams);
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

		


		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement.dbo.pr_UpdateOrderPrintStatus")]
		public void UpdateOrderPrintStatus([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int @pOrderID,
            [SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int? @pShipmentGroupID)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@pOrderID,pShipmentGroupID});
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

        [DAL.SqlCommandMethod(CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.GetParticipantListingReport")]
        public DataTable GetParticipantListingParticipants([SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int @OrderID)
        {
            DataTable dt = null;
            DataTable dtView = null;

            try
            {
                SqlParameter[] aParams = CreateSqlParameters(null, new object[] { @OrderID });

                dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement.dbo.GetParticipantListingReport", aParams);

                DataView view = new DataView(dt);

                dtView = view.ToTable(true, "StudentInstance");
            }
            catch (InvalidOperationException eIO)
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
            catch (Exception eGeneric)
            {
                //cleanup stuff

                //re-throw
                throw eGeneric;
            }
            return dtView;
        }
    }
}
