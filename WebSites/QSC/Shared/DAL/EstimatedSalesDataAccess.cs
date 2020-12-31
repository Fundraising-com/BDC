using System;
//using System.Reflection;
//using System.EnterpriseServices;
using System.Data;
using System.Data.SqlClient;
//using Debug = System.Diagnostics.Debug;

namespace DAL
{
	///<summary>EstimatedOrderViewDataAccess</summary>
	public class EstimatedSalesDataAccess : QDataAccess
	{
		public EstimatedSalesDataAccess(){}

//Optimized this function only to find out it is not being used in the codebehind
//and is being used in a resource intensive manner in the aspx
//going to re-code the solution
//
//		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QspCanadaCommon.dbo.GetProgramsbyCampaign")]
//		public string FetchPrograms([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int p_campaignid)
//		{
//
//			string v_program_name;
//
//			SqlParameter p_program_name = new SqlParameter("p_program_name",SqlDbType.VarChar,500);
//			p_program_name.Direction = ParameterDirection.Output;
//
//			try
//			{
//				SqlCommand sqlCmd = CreateSqlParametersCommand(null,
//					new object[]{p_campaignid,p_program_name});
//				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
//					"QSPCanadaCommon.dbo.GetProgramsbyCampaign",sqlCmd);
//			
//				v_program_name = Convert.ToString(sqlCmd.Parameters["@p_program_name"].Value);
//			}
//			catch( NullReferenceException eNull)
//			{
//				//cleanup stuff
//
//				//re-throw
//				throw eNull;
//			}
//			catch(InvalidOperationException eIO)
//			{
//				//cleanup stuff
//
//				//re-throw
//				throw eIO;
//			}
//			catch (SqlException eSQL)
//			{
//				//cleanup stuff
//
//				//re-throw
//				throw eSQL;
//			}
//			catch(Exception eGeneric)
//			{
//				//cleanup stuff
//
//				//re-throw
//				throw eGeneric;
//			}
//			return v_program_name;  
//		}
//
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QspCanadaCommon.dbo.GetEstimatedSalesView")]
		public DataTable fetch_batch_info(
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int p_program_id, 
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int p_status_instance, 
			[SqlParameter(SqlDbType.VarChar,4,ParameterDirection.Input)]string p_fm_id, 
			[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]string  p_from_date, 
			[SqlParameter(SqlDbType.DateTime,ParameterDirection.Input)]string p_to_date)
		{
			DataTable dt=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{p_program_id,p_status_instance,p_fm_id,p_from_date,p_to_date});

				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QspCanadaCommon.dbo.GetEstimatedSalesView" , aParams);

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
