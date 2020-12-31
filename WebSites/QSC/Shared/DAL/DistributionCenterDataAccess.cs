using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for DistributionCenterDataAccess.
	/// </summary>
	public class DistributionCenterDataAccess : QDataAccess
	{
		public DistributionCenterDataAccess()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		///<summary>Select Distribution Centers object</summary>
		///<returns>DataTable of distribution centers</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement..pr_get_DistributionCenters")]
		public DataTable GetDistributionCenters()
		{
			DataTable dt=null;

			dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement..pr_get_DistributionCenters");
			//			try
			//			{
			//			}
			//			catch(InvalidOperationException)
			//			{
			//			}
			//			catch(SqlException)
			//			{
			//			}
			//			catch(Exception)
			//			{
			//			}
			return dt;
		}

		///<summary>Select Distribution Center object</summary>
		///<param name="Id">int: what distribution center</param>
		///<returns>DataTable of a single DC information</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaOrderManagement..pr_get_DistributionCenter")]
		public DataTable GetDistributionCenter([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int DistributionCenterId)
		{
			DataTable dt=null;
			
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{DistributionCenterId});

			dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaOrderManagement..pr_get_DistributionCenter", aParams);
			//			try
			//			{
			//			}
			//			catch(InvalidOperationException)
			//			{
			//			}
			//			catch(SqlException)
			//			{
			//			}
			//			catch(Exception)
			//			{
			//			}
			return dt;
		}
	}
}

