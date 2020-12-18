using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace QSPForm.Data
{
	/// <summary>
	/// Summary description for DataAdmin.
	/// </summary>
	public class DataAdmin : DBInteractionBase
	{
		
		#region Parameter
		//Stored procedure parameter names
		public const string PARAM_CAMP_ID = "@icampaign_id";
		//
		// Stored procedure names for each operation
		private const string SQL_PROC_COUNT_ALL = "pr_Admin_CountAllDataWcampaign_idLogic";
		
		#endregion
		
		
		public DataAdmin()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataTable CountAllDataWcampaign_idLogic(int CampaignID)
		{
			DataTable Table = new DataTable();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_COUNT_ALL;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CAMP_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CampaignID));
			//cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ERROR, SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

			Select(cmdToExecute,Table);
			//AssignInnerProperty(Table);

			return Table;
				
		}

	}
}
