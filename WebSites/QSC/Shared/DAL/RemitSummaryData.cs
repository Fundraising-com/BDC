using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;

namespace DAL
{
	/// <summary>
	/// Summary description for RemitSummaryData.
	/// </summary>
	public class RemitSummaryData : DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_RUN_ID = "@iRunID";

		private SqlCommand insertCommand = null;

		private SqlCommand deleteCommand = null;

		private SqlCommand updateCommand = null;

		#endregion

		public RemitSummaryData() : base(DataBaseName.QSPCanadaOrderManagement) { }

		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <remarks>
		/// </remarks>
		public  void SelectAll(DataSet dtsDataSet, int runID, string tableName)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Remit_GetRemitSummary]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID, runID));
			Select(cmdToExecute, dtsDataSet, tableName);
		}

		protected override SqlCommand GetDeleteCommand()
		{
			return deleteCommand;
		}

		protected override SqlCommand GetInsertCommand()
		{
			return insertCommand;
		}

		protected override SqlCommand GetUpdateCommand()
		{
			return updateCommand;
		}
	}
}
