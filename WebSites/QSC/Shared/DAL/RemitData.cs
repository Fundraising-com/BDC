using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;


namespace DAL
{
	/// <summary>
	/// Data Access Layer for Remit (global remit) object
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class RemitData : DBTableOperation
	{
		#region Class Member Declarations

		private const string PARAM_RUN_ID = "@tempRBid";
		private const string PARAM_RUN_ID_2 = "@runid";
		private const string PARAM_RUN_ID_3 = "@RUNID";
		private const string PARAM_RUN_ID_4 = "@iRunID";
        private const string PARAM_RUN_ID_5 = "@RemitBatchID";
        private const string RETURN_CODE = "@ReturnCode";

		private SqlCommand insertCommand = null;

		private SqlCommand deleteCommand = null;

		private SqlCommand updateCommand = null;

		#endregion

		public RemitData() : base(DataBaseName.QSPCanadaOrderManagement) { }

		public int CreateNewRemit()
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Remit_CreateNewBatch]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}

		public bool ProcessRemit(int runID)
		{
			try
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = "dbo.[ProcessRemitBatch]";
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID, runID));
				cmdToExecute.Parameters.Add(new SqlParameter(RETURN_CODE, SqlDbType.Bit, 1, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

				ExecuteCmd(cmdToExecute);

				return !Convert.ToBoolean(cmdToExecute.Parameters[RETURN_CODE].Value);
			}
			catch(SqlException) 
			{

				throw new RemitException();
			}
		}

		public bool ReProcessRemit(int runID)
		{
			try
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = "dbo.[ReprocessRemitBatchByRunID]";
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID_2, runID));
				cmdToExecute.Parameters.Add(new SqlParameter(RETURN_CODE, SqlDbType.Bit, 1, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

				ExecuteCmd(cmdToExecute);

				return !Convert.ToBoolean(cmdToExecute.Parameters[RETURN_CODE].Value);
			}
			catch(SqlException) 
			{
				throw new RemitException();
			}
		}

		public bool CalculateTaxes(int runID)
		{
			try
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = "dbo.[Pr_CalcTaxesForRemit]";
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID_3, runID));
				return !Convert.ToBoolean(ExecuteScalar(cmdToExecute));
			}
			catch(SqlException) 
			{
				throw new RemitException();
			}
		}

        public bool SendAP(int runID)
        {
            try
            {
                SqlCommand cmdToExecute = new SqlCommand();
                cmdToExecute.CommandText = "QSPCanadaFinance.dbo.[AP_Remit_SendAP]";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUN_ID_5, runID));
                cmdToExecute.Parameters.Add(new SqlParameter("@GenerateChequeFile", true));
                cmdToExecute.Parameters.Add(new SqlParameter("@RemitCode", null)); 
                ExecuteScalar(cmdToExecute);
                return true;
            }
            catch (SqlException)
            {
                throw new RemitException();
            }
        }

		public bool GenerateGiftCards(string fileName)
		{
			try
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = "dbo.[sp_GenerateGiftCardFile]";
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter("@fileName", fileName));
				ExecuteScalar(cmdToExecute);
				return true;
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
