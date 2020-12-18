///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'order_detail'
// Generated by Jas on: Monday, November 03, 2003, 4:18:07 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrderDetailTable;

namespace QSPForm.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'order_detail'.
	/// </summary>
	public class OrderDetail : DBTableOperation
	{

		#region Parameter
		//Stored procedure parameter names
		public const string PARAM_PKID = "@iorder_detail_id";
		public const string PARAM_ORDER_ID ="@iorder_id";
		public const string PARAM_CATALOG_ITEM_DETAIL_ID ="@icatalog_item_detail_id";	
		public const string PARAM_SOURCE_ID ="@isource_id";
		public const string PARAM_ORDER_STATUS_ID ="@iorder_status_id";	
		public const string PARAM_STATUS_REASON_ID ="@istatus_reason_id";	
		public const string PARAM_SHIPMENT_GROUP_ID ="@ishipment_group_id";	
		public const string PARAM_QUANTITY ="@iquantity";	
		public const string PARAM_ADJUSTMENT_QUANTITY ="@iadjustment_quantity";
		public const string PARAM_PRICE ="@dprice";
        public const string PARAM_PERSONALIZATION_ID = "@ipersonalization_id";
		
		//
		// Stored procedure names for each operation
		private const string SQL_PROC_DELETE = "pr_order_detail_Delete";
		private const string SQL_PROC_INSERT = "pr_order_detail_Insert";
		private const string SQL_PROC_UPDATE = "pr_order_detail_Update";	
		private const string SQL_PROC_SELECT_ONE = "pr_order_detail_SelectOne";
		private const string SQL_PROC_SELECT_ALL = "pr_order_detail_SelectAll";

		#endregion

		//
		// DataSetCommand object
		//
		
		private SqlCommand insertCommand;
		private SqlCommand updateCommand;
		private SqlCommand deleteCommand;

		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public OrderDetail()
		{
			// Nothing for now.
		}

		//----------------------------------------------------------------
		// Sub GetInsertCommand:
		//   Initialize the parameterized Insert command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetInsertCommand()
		{
			if ( insertCommand == null )
			{
				//
				// Construct the command since we don't have it already
				// 
				insertCommand = new SqlCommand(SQL_PROC_INSERT);
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
            
				SqlParameterCollection sqlParams = insertCommand.Parameters;
            
				sqlParams.Add(new SqlParameter(PARAM_ORDER_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_CATALOG_ITEM_DETAIL_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_SOURCE_ID, SqlDbType.Int));	
				sqlParams.Add(new SqlParameter(PARAM_ORDER_STATUS_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_STATUS_REASON_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_SHIPMENT_GROUP_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_QUANTITY, SqlDbType.Int));		
				sqlParams.Add(new SqlParameter(PARAM_ADJUSTMENT_QUANTITY, SqlDbType.Int));	
				sqlParams.Add(new SqlParameter(PARAM_PRICE, SqlDbType.Money));				
				sqlParams.Add(new SqlParameter(PARAM_CREATE_USER_ID, SqlDbType.Int));
							
				sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
								
				sqlParams[PARAM_PKID].Direction = ParameterDirection.Output;				
				//
				// Define the parameter mappings from the data table in the
				// dataset.
				//
				sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
				sqlParams[PARAM_ORDER_ID].SourceColumn = dataDef.FLD_ORDER_ID;
				sqlParams[PARAM_CATALOG_ITEM_DETAIL_ID].SourceColumn = dataDef.FLD_CATALOG_ITEM_DETAIL_ID;
				sqlParams[PARAM_SOURCE_ID].SourceColumn = dataDef.FLD_SOURCE_ID;
				sqlParams[PARAM_ORDER_STATUS_ID].SourceColumn = dataDef.FLD_ORDER_STATUS_ID;
				sqlParams[PARAM_STATUS_REASON_ID].SourceColumn = dataDef.FLD_STATUS_REASON_ID;
				sqlParams[PARAM_SHIPMENT_GROUP_ID].SourceColumn = dataDef.FLD_SHIPMENT_GROUP_ID;
				sqlParams[PARAM_QUANTITY].SourceColumn = dataDef.FLD_QUANTITY;
				sqlParams[PARAM_ADJUSTMENT_QUANTITY].SourceColumn = dataDef.FLD_ADJUSTMENT_QUANTITY;
				sqlParams[PARAM_PRICE].SourceColumn = dataDef.FLD_PRICE;
				sqlParams[PARAM_CREATE_USER_ID].SourceColumn = dataDef.FLD_CREATE_USER_ID;						
				
				//Don't need to map ErrorCode cause is not imply in the insert
				//Only Mapped DataColumn are imply...
			}
            
			return insertCommand;
		}

		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
			if ( updateCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				updateCommand = new SqlCommand(SQL_PROC_UPDATE);
				updateCommand.CommandType = CommandType.StoredProcedure;
				
            
				SqlParameterCollection sqlParams = updateCommand.Parameters;
            
				sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_ORDER_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_CATALOG_ITEM_DETAIL_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_SOURCE_ID, SqlDbType.Int));	
				sqlParams.Add(new SqlParameter(PARAM_ORDER_STATUS_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_STATUS_REASON_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_SHIPMENT_GROUP_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_QUANTITY, SqlDbType.Int));	
				sqlParams.Add(new SqlParameter(PARAM_ADJUSTMENT_QUANTITY, SqlDbType.Int));	
				sqlParams.Add(new SqlParameter(PARAM_PRICE, SqlDbType.Money));				
				sqlParams.Add(new SqlParameter(PARAM_DELETED, SqlDbType.Bit));
				sqlParams.Add(new SqlParameter(PARAM_UPDATE_USER_ID, SqlDbType.Int));
                sqlParams.Add(new SqlParameter(PARAM_PERSONALIZATION_ID, SqlDbType.Int));	

				// Define the parameter mappings from the data tabl
				//
				sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
				sqlParams[PARAM_ORDER_ID].SourceColumn = dataDef.FLD_ORDER_ID;
				sqlParams[PARAM_CATALOG_ITEM_DETAIL_ID].SourceColumn = dataDef.FLD_CATALOG_ITEM_DETAIL_ID;
				sqlParams[PARAM_SOURCE_ID].SourceColumn = dataDef.FLD_SOURCE_ID;
				sqlParams[PARAM_ORDER_STATUS_ID].SourceColumn = dataDef.FLD_ORDER_STATUS_ID;
				sqlParams[PARAM_STATUS_REASON_ID].SourceColumn = dataDef.FLD_STATUS_REASON_ID;
				sqlParams[PARAM_SHIPMENT_GROUP_ID].SourceColumn = dataDef.FLD_SHIPMENT_GROUP_ID;
				sqlParams[PARAM_QUANTITY].SourceColumn = dataDef.FLD_QUANTITY;
				sqlParams[PARAM_ADJUSTMENT_QUANTITY].SourceColumn = dataDef.FLD_ADJUSTMENT_QUANTITY;
				sqlParams[PARAM_PRICE].SourceColumn = dataDef.FLD_PRICE;
				sqlParams[PARAM_DELETED].SourceColumn = dataDef.FLD_DELETED;				
				sqlParams[PARAM_UPDATE_USER_ID].SourceColumn = dataDef.FLD_UPDATE_USER_ID;
                sqlParams[PARAM_PERSONALIZATION_ID].SourceColumn = dataDef.FLD_PERSONALIZATION_ID;				
				//Don't need to map ErrorCode cause is not imply in the insert
				//Only Mapped DataColumn are imply...
			}
            
			return updateCommand;
		}

		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetDeleteCommand()
		{
			if ( deleteCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				deleteCommand = new SqlCommand(SQL_PROC_DELETE);
				deleteCommand.CommandType = CommandType.StoredProcedure;
				
				SqlParameterCollection sqlParams = deleteCommand.Parameters;
            
				sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_UPDATE_USER_ID, SqlDbType.Int));
				//
				// Define the parameter mappings from the data tabl
				//
				sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;	
				sqlParams[PARAM_UPDATE_USER_ID].SourceColumn = dataDef.FLD_UPDATE_USER_ID;
				//Only Mapped DataColumn are imply...
			}
            
			return deleteCommand;
		}

		protected override string TableName
		{
			get{return dataDef.TBL_ORDER_DETAILS;}
		}
		
		//We create a new method when we want to the return type variable
		public new dataDef SelectOne(int ID)
		{
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ONE;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ID));
			
			Select(cmdToExecute,Table);

			return Table;
				
		}

		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties set after a succesful call of this method: 
		/// <UL>
		///		 <LI>ErrorCode</LI>
		/// </UL>
		/// </remarks>
		public override DataTable SelectAll()
		{
			dataDef Table = new dataDef();

			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			
					
			Select(cmdToExecute,Table);
			return  Table;
			
		}

		public dataDef SelectAllWorder_idLogic(int OrderID)
		{
			dataDef Table = new dataDef();

			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_order_detail_SelectAllWorder_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ORDER_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, OrderID));
					
			Select(cmdToExecute,Table);
			
			return  Table;
			
		}

		public dataDef SelectAllWform_idLogic(int FormID, int OrderID)
		{
			dataDef Table = new dataDef();
			
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_order_detail_SelectAllWform_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@iform_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FormID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ORDER_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, OrderID));
			Table.PrimaryKey = null;	
			Table.Columns[dataDef.FLD_PKID].AllowDBNull = true;
			Select(cmdToExecute,Table);
			
			return  Table;
			
		}


		public dataDef SelectAllSupplyWorder_idLogic(int OrderID)
		{
			dataDef Table = new dataDef();

			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_order_detail_SelectAllWorder_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ORDER_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, OrderID));
			cmdToExecute.Parameters.Add(new SqlParameter("@bis_supply", SqlDbType.Bit, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, true));
					
			Select(cmdToExecute,Table);
			
			return  Table;
			
		}

		public dataDef SelectAllSupplyWform_idLogic(int FormID, int OrderID)
		{
			dataDef Table = new dataDef();

			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_order_detail_SelectAllWform_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@iform_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FormID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ORDER_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, OrderID));
			cmdToExecute.Parameters.Add(new SqlParameter("@bis_supply", SqlDbType.Bit, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, true));
					
			Select(cmdToExecute,Table);
			
			return  Table;
			
		}

		public dataDef SelectAllWshipment_group_idLogic(int ShipmentGroupID)
		{
			dataDef Table = new dataDef();

			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_order_detail_SelectAllWorder_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SHIPMENT_GROUP_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ShipmentGroupID));
					
			Select(cmdToExecute,Table);
			
			return  Table;
			
		}

	

        public void SelectAllByOrderID_IncludeAll(OrderData dtsOrder, int OrderID, int FormID)
        {
            dataDef dTbl = new dataDef();

            //
            // Get the user DataTable from the DataLayer
            //
            //In this Operation we will add all product not really present in the order
            //This is based on the current Order Form

            dTbl = SelectAllWorder_idLogic(OrderID);

            CatalogItemDetail catDetailDataAccess = new CatalogItemDetail();
            CatalogItemDetailTable dTblCatDetail = catDetailDataAccess.SelectAllWform_idLogic(FormID);
            DataView dv = new DataView(dTbl);
            dv.Sort = dataDef.FLD_CATALOG_ITEM_CODE;

            foreach (DataRow detailRow in dTblCatDetail.Rows)
            {
                string CatDetailCode = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE].ToString();
                if (CatDetailCode.Length > 0)
                {
                    int iIndex = -1;
                    //iIndex = dv.Find(CatDetailCode);
                    iIndex = findRow(dTbl, CatDetailCode, detailRow[CatalogItemDetailTable.FLD_PROFIT_RATE].ToString());
                    if (iIndex == -1)
                    {
                        //Insert product offer
                        DataRow newRow = dTbl.NewRow();
                        newRow[dataDef.FLD_CATALOG_ITEM_DETAIL_ID] = detailRow[CatalogItemDetailTable.FLD_PKID];
                        newRow[dataDef.FLD_CATALOG_ITEM_CODE] = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE];
                        newRow[dataDef.FLD_CATALOG_ITEM_NAME] = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NAME];
                        newRow[dataDef.FLD_CATALOG_ITEM_DESC] = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_DESC];
                        newRow[dataDef.FLD_CATALOG_ITEM_DETAIL_PRICE] = detailRow[CatalogItemDetailTable.FLD_PRICE];
                        newRow[dataDef.FLD_PRICE] = detailRow[CatalogItemDetailTable.FLD_PRICE];
                        newRow[dataDef.FLD_CATALOG_ITEM_NB_UNITS] = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NB_UNITS];
                        newRow[dataDef.FLD_DISPLAY_ORDER] = detailRow[CatalogItemDetailTable.FLD_DISPLAY_ORDER];
                        newRow[dataDef.FLD_NB_DAY_LEAD_TIME] = detailRow[CatalogItemDetailTable.FLD_NB_DAY_LEAD_TIME];
                        newRow[dataDef.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE] = detailRow[CatalogItemDetailTable.FLD_PROFIT_RATE];
                        newRow[dataDef.FLD_PROFIT_RATE] = detailRow[CatalogItemDetailTable.FLD_PROFIT_RATE];
                        newRow[dataDef.FLD_FORM_SECTION_TYPE_ID] = detailRow[CatalogItemDetailTable.FLD_FORM_SECTION_TYPE_ID];
                        newRow[dataDef.FLD_FORM_SECTION_NUMBER] = detailRow[CatalogItemDetailTable.FLD_FORM_SECTION_NUMBER];

                        newRow[dataDef.FLD_ORDER_ID] = OrderID;
                        dTbl.Rows.Add(newRow);
                    }
                    else
                    {
                        dv[iIndex][dataDef.FLD_FORM_SECTION_TYPE_ID] = detailRow[CatalogItemDetailTable.FLD_FORM_SECTION_TYPE_ID];
                        dv[iIndex][dataDef.FLD_FORM_SECTION_NUMBER] = detailRow[CatalogItemDetailTable.FLD_FORM_SECTION_NUMBER];
                    }
                }



            }

            dv.Sort = dataDef.FLD_DISPLAY_ORDER;
            dtsOrder.Merge(dTbl);
        }




        private int findRow(DataTable dtbl, string detailCode, string profitRate)
        {
            
            int index = -1;
            if(dtbl.Rows.Count > 0)
            {
                for(int i=0;i<dtbl.Rows.Count;i++)
                {
                    if (dtbl.Rows[i][dataDef.FLD_CATALOG_ITEM_CODE].ToString() == detailCode)
                    {
                        if (dtbl.Rows[i][dataDef.FLD_PROFIT_RATE].ToString() == profitRate || dtbl.Rows[i][dataDef.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE].ToString() == profitRate)
                        {
                            index = i;
                            break;
                        }     
                    }
                }
            }
            return index;
           
        }

        //private bool findRow(DataTable dtbl, string detailCode, string profitRate)
        //{
        //    bool found = false;
        //    foreach (DataRow r in dtbl.Rows)
        //    {
        //        if (r[dataDef.FLD_CATALOG_ITEM_CODE].ToString() == detailCode)
        //        {
        //            if (r[dataDef.FLD_PROFIT_RATE].ToString() == profitRate || r[dataDef.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE].ToString() == profitRate)
        //            {
        //                found = true;
        //                break;
        //            }     
        //        }
        //    }
        //    return found;
        //}

		public void SelectAllSupplyByOrderID_IncludeAll(OrderData dtsOrder, int OrderID, int FormID)
		{			
			dataDef dTbl = new dataDef();
			
			//
			// Get the user DataTable from the DataLayer
			//
			//In this Operation we will add all product not really present in the order
			//This is based on the current Order Form

			dTbl = SelectAllWorder_idLogic(OrderID);
			
			CatalogItemDetail catDetailDataAccess = new CatalogItemDetail();
			CatalogItemDetailTable dTblCatDetail = catDetailDataAccess.SelectAllWform_idLogic(FormID, true);	
			DataView dv = new DataView(dTbl);
			dv.Sort = dataDef.FLD_CATALOG_ITEM_CODE;
			
			foreach (DataRow detailRow in dTblCatDetail.Rows)
			{
				string CatDetailCode = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE].ToString();
				if (CatDetailCode.Length > 0)
				{
					int iIndex = -1;
					iIndex = dv.Find(CatDetailCode);
					if (iIndex == -1)
					{
						//Insert product offer
						DataRow newRow = dTbl.NewRow();
						newRow[dataDef.FLD_CATALOG_ITEM_DETAIL_ID] = detailRow[CatalogItemDetailTable.FLD_PKID];
						newRow[dataDef.FLD_CATALOG_ITEM_CODE] = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE];
						newRow[dataDef.FLD_CATALOG_ITEM_NAME] = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NAME];
						newRow[dataDef.FLD_CATALOG_ITEM_DESC] = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_DESC];
						newRow[dataDef.FLD_CATALOG_ITEM_DETAIL_PRICE] = detailRow[CatalogItemDetailTable.FLD_PRICE];
						newRow[dataDef.FLD_PRICE] = detailRow[CatalogItemDetailTable.FLD_PRICE];
						newRow[dataDef.FLD_CATALOG_ITEM_NB_UNITS] = detailRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NB_UNITS];
						newRow[dataDef.FLD_DISPLAY_ORDER] = detailRow[CatalogItemDetailTable.FLD_DISPLAY_ORDER];
						newRow[dataDef.FLD_NB_DAY_LEAD_TIME] = detailRow[CatalogItemDetailTable.FLD_NB_DAY_LEAD_TIME];
                        newRow[dataDef.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE] = detailRow[CatalogItemDetailTable.FLD_PROFIT_RATE];
                        newRow[dataDef.FLD_PROFIT_RATE] = detailRow[CatalogItemDetailTable.FLD_PROFIT_RATE];
						
						newRow[dataDef.FLD_ORDER_ID] = OrderID;
						dTbl.Rows.Add(newRow);
					}
				
				}
				
			
			}

			dTbl.TableName = OrderData.TBL_SUPPLY;
			dtsOrder.Merge(dTbl);
		}
	}
}
