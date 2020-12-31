using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;
using dataSetRef = Common.TableDef.RemitTestDataSet;

namespace DAL
{
	/// <summary>
	/// Data Access Layer object for RemitTests
	/// </summary>
	public class RemitTestData : DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ID= "@ID";
		internal const string PARAM_NAME= "@Name";
		internal const string PARAM_SCRIPT= "@Script";
		internal const string PARAM_CORRECTION_SCRIPT = "@CorrectionScript";
		internal const string PARAM_RUN_ID = "@iRunID";
		internal const string PARAM_RESULT_CODE = "@iResultCodeInstance";

		private SqlCommand insertCommand = null;

		private SqlCommand deleteCommand = null;

		private SqlCommand updateCommand = null;

		#endregion

		dataSetRef dataSet = new dataSetRef();

		public RemitTestData() : base(DataBaseName.QSPCanadaOrderManagement) { }

		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// </remarks>
		public  void SelectAll(DataSet dtsDataSet, string tableName)		
		{
			try
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = "dbo.[pr_RemitTest_GetRemitTests]";
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				Select(cmdToExecute,dtsDataSet, tableName);
			}
			catch(SqlException) 
			{
				throw new RemitException();
			}
		}

		public bool Validate(string commandName, DataSet dtResultSet, int runID)
		{
			try 
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = commandName;
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID, runID));
				Select(cmdToExecute, dtResultSet, "ProblemData");

				if (dtResultSet.Tables[0].Rows.Count > 0)
					return false;
				else
					return true;
			} 
			catch(SqlException) 
			{
				throw new RemitException();
			}
		}

		public bool Validate(string commandName, int runID)
		{
			try 
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = commandName;
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID, runID));
				return !Convert.ToBoolean(ExecuteScalar(cmdToExecute));
			} 
			catch(SqlException) 
			{
				throw new RemitException();
			}
		}

		public void Fix(string commandName, int runID)
		{
			try
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = commandName;
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID, runID));
				ExecuteCmd(cmdToExecute);
			}
			catch(SqlException) 
			{
				throw new RemitException();
			}
		}

		public void LogTest(int runID, int testID, int testResult)
		{
			try
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = "pr_RemitTest_LogRemitTests";
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID, runID));
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ID, testID));
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RESULT_CODE, testResult));
				ExecuteCmd(cmdToExecute);
			}
			catch(SqlException) 
			{
				throw new RemitException();
			}
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

