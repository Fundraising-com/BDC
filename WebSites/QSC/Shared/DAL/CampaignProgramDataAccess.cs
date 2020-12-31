using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Gets a list of available programs</summary>
	public class CampaignProgramDataAccess : QDataAccess
	{
		public CampaignProgramDataAccess()
		{

		}

		///<summary>Select an AccountListDataAccess object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_CampaignProgram_ContentCatalogCode")]
		public DataTable GetCampaignPrograms(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.Bit,1,ParameterDirection.Input)]bool ShowUnSelected
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, new object[]{
																				CampaignID,
																				ShowUnSelected
																			   });

				return SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
						"QSPCanadaCommon.dbo.pr_get_CampaignProgram_ContentCatalogCode",aParams);
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

		///<summary>Insert/Update a Campaign+Program Record</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_CampaignProgram")]
		public bool SaveCampaignProgram(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ProgramID,
			[SqlParameter(SqlDbType.Bit,1,ParameterDirection.Input)]bool IsPreCollect,
			[SqlParameter(SqlDbType.Decimal,9,ParameterDirection.Input)]double GroupProfit,
			[SqlParameter(SqlDbType.Bit,1,ParameterDirection.Input)]bool Choice,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int UserIDModified
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, 
					new object[]{CampaignID,ProgramID,IsPreCollect,GroupProfit,Choice,UserIDModified});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_upd_CampaignProgram",aParams);
				return true;
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


		
		///<summary>Insert/Update a Campaign+Program+Brochure Record</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_CampaignToContentCatalog")]
		public bool SaveCampaignToContentCatalog(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ProgramID,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string Content_Catalog_Code,
			[SqlParameter(SqlDbType.Bit,1,ParameterDirection.Input)]bool Choice,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int UserIDModified
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, 
					new object[]{CampaignID,ProgramID,Content_Catalog_Code,Choice,UserIDModified});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_upd_CampaignToContentCatalog",aParams);
				return true;
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

		///<summary>Get the Field Supply info for a Campaign</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_Campaign_FSinfo")]
		public DataTable Get_Campaign_FSinfo([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{CampaignID});

				return SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_get_Campaign_FSinfo",aParams);
				
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

		///<summary>Generate the Field Supply Order</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_Campaign_GenerateFSOrder")]
		public void GenerateFSOrder(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int UserID)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{CampaignID,UserID});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_Campaign_GenerateFSOrder",aParams);
				
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

		///<summary>Updates a Campaign's Field Supply Info</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_Campaign_FSinfo")]
		public bool SaveCampaignFSinfo(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.DateTime,8,ParameterDirection.Input)] System.DateTime SuppliesDeliveryDate,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int SuppliesShipToCampaignContactID,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int UserIDModified
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, 
					new object[]{CampaignID,SuppliesDeliveryDate,SuppliesShipToCampaignContactID,UserIDModified});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_upd_Campaign_FSinfo",aParams);
				return true;
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



		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_Campaign_SuppliesAddressID")]
		public bool SaveNewSuppliesAddressID(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int SuppliesAddressID
			)
		{
			SqlCommand sqlCmd = CreateSqlParametersCommand(null,
			new object[]{CampaignID,SuppliesAddressID});

			int rows = -1;
			try
			{
				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_upd_Campaign_SuppliesAddressID", sqlCmd);
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
			
 			if(rows != 1)//updated 1 campaign
			{
				string msg = rows.ToString() + " rows were updated, we needed exactly 1";
				throw new System.ApplicationException(msg);
			}
			else
			{				
				return true;
			}
		}


	}
}