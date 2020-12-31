using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>DAL file for class CampaignDataAccess to access Campaign info.</summary>
	public class CampaignDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public CampaignDataAccess(){}

		///<summary>Insert a Campaign object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon..pr_ins_Campaign")]
		public bool Insert(	[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int CampaignID,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int Status,
							//[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Country,
							[SqlParameter(SqlDbType.VarChar	 ,4,ParameterDirection.Input)]string FMID,
							[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string DateChanged,
							[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Lang,
							[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime StartDate,
							[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime EndDate,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int IncentivesBillToID,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int BillToAccountID,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int ShipToAccountID,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int EstimatedGross,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int NumberOfParticipants,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int NumberOfClassroooms,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int NumberOfStaff,
							//[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime SuppliesDeliveryDate,
							[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string SpecialInstructions,
							[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool IsStaffOrder,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int StaffOrderDiscount,
							[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool IsTestCampaign,
							[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime DateModified,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int UserIDModified,
							[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool IsPayLater,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int IncentivesDistributionID,
							[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime MagnetStatementDate,
							//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagazine,
							//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagazineExpress,
							//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagnet,
							//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagazineCombo,
							//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagazineStaff,
							//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool RewardsProgramCumulative,
							//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool RewardsProgramChart,
							//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool RewardsProgramDraw,
							[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string ContactName,
							[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string ContactPhone
							)
		{
			CampaignID = -1;

			//how many rows should be inserted ?
			System.Int16 rowsToDo = 3; //the campaign, the PhoneList, the ContactPhone #
			//if (ProgramMagazine			== true) rowsToDo += 1;
			//if (ProgramMagazineExpress	== true) rowsToDo += 1;
			//if (ProgramMagnet			== true) rowsToDo += 1;
			//if (ProgramMagazineCombo	== true) rowsToDo += 1;
			//if (ProgramMagazineStaff	== true) rowsToDo += 1;

			SqlCommand sqlCmd = CreateSqlParametersCommand(null,
			new object[]{CampaignID,Status,FMID,DateChanged,Lang,StartDate,EndDate,IncentivesBillToID,BillToAccountID,ShipToAccountID,EstimatedGross,NumberOfParticipants,NumberOfClassroooms,NumberOfStaff,
				/*SuppliesDeliveryDate,*/
				SpecialInstructions,IsStaffOrder,StaffOrderDiscount,IsTestCampaign,DateModified,UserIDModified,
				IsPayLater,IncentivesDistributionID,MagnetStatementDate,
				/*ProgramMagazine,ProgramMagazineExpress,ProgramMagnet,ProgramMagazineCombo,ProgramMagazineStaff,RewardsProgramCumulative,RewardsProgramChart,RewardsProgramDraw,*/
				ContactName,ContactPhone});

			int rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_ins_Campaign",sqlCmd);
			CampaignID	= Convert.ToInt32(sqlCmd.Parameters["@CampaignID"].Value);
			Lang		= Convert.ToString(sqlCmd.Parameters["@Lang"].Value);

			string msg = "";
			if(rows != rowsToDo)//inserted 1 Campaign row + 1 PhoneList row + 1 ContactPhone # + rowsToDo-3 programs
			{
				msg = rows.ToString() + " rows were inserted, we needed exactly " + rowsToDo.ToString();
				msg += " : 1 campaign " + ((int)(rowsToDo -3)).ToString() + " programs";
				msg += " 1 Phone List 1 Contact PhoneNumber.";
				throw new System.ApplicationException(msg);
			}
			return true;
		}
		///<summary>Update a Campaign object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon..pr_upd_Campaign")]
		public bool Update(	[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int Status,
			//[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Country,
			[SqlParameter(SqlDbType.VarChar	 ,4,ParameterDirection.Input)]string FMID,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string DateChanged,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Lang,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime StartDate,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime EndDate,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int IncentivesBillToID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int BillToAccountID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int ShipToAccountID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int EstimatedGross,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int NumberOfParticipants,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int NumberOfClassroooms,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int NumberOfStaff,
			//[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime SuppliesDeliveryDate,
			[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string SpecialInstructions,
			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool IsStaffOrder,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int StaffOrderDiscount,
			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool IsTestCampaign,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime DateModified,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int UserIDModified,
			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool IsPayLater,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int IncentivesDistributionID,
			//[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool FSOrderRecCreated,
			[SqlParameter(SqlDbType.DateTime ,ParameterDirection.Input)]DateTime MagnetStatementDate,
//			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagazine,
//			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagazineExpress,
//			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagnet,
//			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagazineCombo,
//			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool ProgramMagazineStaff,
//			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool RewardsProgramCumulative,
//			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool RewardsProgramChart,
//			[SqlParameter(SqlDbType.Bit		 ,1,ParameterDirection.Input)]bool RewardsProgramDraw,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string ContactName,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string ContactPhone

			)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,
				new object[]{CampaignID,Status,FMID,DateChanged,Lang,StartDate,EndDate,IncentivesBillToID,
				BillToAccountID,ShipToAccountID,EstimatedGross,NumberOfParticipants,NumberOfClassroooms,NumberOfStaff,
				/*SuppliesDeliveryDate,*/
				SpecialInstructions,IsStaffOrder,StaffOrderDiscount,IsTestCampaign,DateModified,UserIDModified,
				IsPayLater,IncentivesDistributionID,/*FSOrderRecCreated,*/MagnetStatementDate,
				/*ProgramMagazine,ProgramMagazineExpress,ProgramMagnet,ProgramMagazineCombo,ProgramMagazineStaff,RewardsProgramCumulative,RewardsProgramChart,RewardsProgramDraw,*/
				ContactName,ContactPhone});

			object msg = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_upd_Campaign",aParams);
			if(msg.ToString() != "")
			{
				throw new System.ArgumentException("The following programs could not be removed due to existing sales", msg.ToString());
			}
			return true;

//			int rows = SqlHelper.ExecuteNonQuery(...........);
//			string msg = "";
//			if(rows != 1)//updated 1 Campaign row
//			{
//				msg = rows.ToString() + " rows were updated, we needed exactly 1 to go in";
//				throw new System.ApplicationException(msg);
//			}
//			return true;
		}
		///<summary>Select a Campaign object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_get_Campaign")]
		public DataTable Exists([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID)
		{
			//DataTable dt=null;
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{CampaignID});
			DataTable dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_Campaign",aParams);
			return dt;
		}

		///<summary>Select an Account object to start a new campaign</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_get_Account_4new_Campaign")]
		public DataTable AccountForNewCampaign([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountID)
		{
			//DataTable dt=null;
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountID});
			DataTable dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_Account_4new_Campaign",aParams);
			return dt;
		}

		///<summary>Perform a campaign lookup for a Canadian Account</summary>
		///<param name="AccountID">int:What Account/Group to check</param>
		///<returns>datatable</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_CampaignInfo_ForLinks")]
		public DataTable GetCampaignInfoForLinksCA([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountID)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountID});

				dt = SqlHelper.ExecuteDataTable(connection,CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_get_CampaignInfo_ForLinks", aParams);
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

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_GetCAOrderCount")]
        public DataTable GetCAOrderCount([SqlParameter(SqlDbType.Int, 4, ParameterDirection.Input)]int CampaignID)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{CampaignID});

				dt = SqlHelper.ExecuteDataTable(connection,CommandType.StoredProcedure,
                    "QSPCanadaCommon.dbo.pr_GetCAOrderCount", aParams);
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
