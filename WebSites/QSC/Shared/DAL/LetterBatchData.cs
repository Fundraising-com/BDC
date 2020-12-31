using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;
using dataSetRef = Common.TableDef.LetterBatchDataSet;


namespace DAL
{
	/// <summary>
	/// Purpose: Data Access class for the table 'LetterTemplate'.
	/// </summary>
	public class LetterBatchData : DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ID = "@iID";
		internal const string PARAM_LETTERTEMPLATEID = "@iLetterTemplateID";
		internal const string PARAM_LETTERBATCHTYPE = "@iLetterBatchType";
		internal const string PARAM_RUNID = "@iRunID";
		internal const string PARAM_DATEFROM = "@dDateFrom";
		internal const string PARAM_DATETO = "@dDateTo";
		internal const string PARAM_CUSTOMERORDERHEADERINSTANCE = "@iCustomerOrderHeaderInstance";
		internal const string PARAM_TRANSID = "@iTransID";
		internal const string PARAM_ISPRINTED = "@bIsPrinted";
		internal const string PARAM_DATEPRINTED = "@dDatePrinted";
		internal const string PARAM_ISLOCKED = "@bIsLocked";
		internal const string PARAM_USERID = "@iUserID";
		internal const string PARAM_DATECREATEFROM = "@dDateCreatedFrom";
		internal const string PARAM_DATECREATETO = "@dDateCreatedTo";
		internal const string PARAM_RUNIDFROM = "@iRunIDFrom";
		internal const string PARAM_RUNIDTO = "@iRunIDTo";
		internal const string PARAM_LETTERBATCHID = "@iLetterBatchID";
		internal const string PARAM_PRODUCTCODE = "@sProductCode";
		internal const string PARAM_REASON = "@iReason";

		//

		// DataSetCommand object

		//

		private SqlCommand insertCommand = null;

		private SqlCommand deleteCommand = null;

		private SqlCommand updateCommand = null;

		#endregion

		dataSetRef dataSet = new dataSetRef();



		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public LetterBatchData() : base(DataBaseName.QSPCanadaOrderManagement) { }
		//----------------------------------------------------------------
		// Sub GetInsertCommand:
		//   Initialize the parameterized Insert command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetInsertCommand()
		{
			return insertCommand;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetDeleteCommand()
		{
			return deleteCommand;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
			return updateCommand;
		}


		/// <summary>
		/// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public void SelectOne(DataSet dtsDataSet, string tableName, int ID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatch_SelectOne]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ID));
			Select(cmdToExecute,dtsDataSet, tableName);
		}


		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// </remarks>
		public  void SelectAll(DataSet dtsDataSet, string tableName)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatch_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public  void SelectAll(DataSet dtsDataSet, string tableName, int letterTemplateID, DateTime dateCreatedFrom, DateTime dateCreatedTo, int letterBatchType, DateTime dateFrom, DateTime dateTo, int runIDFrom, int runIDTo, BooleanNullable isPrinted, BooleanNullable isLocked)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatch_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERTEMPLATEID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(letterTemplateID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATECREATEFROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateCreatedFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATECREATETO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateCreatedTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERBATCHTYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(letterBatchType)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATEFROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATETO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUNIDFROM, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(runIDFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUNIDTO, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(runIDTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ISPRINTED, SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isPrinted.Value));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ISLOCKED, SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isLocked.Value));
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public  void SelectByCustomerOrderDetail(DataSet dataSet, string tableName, int CustomerOrderHeaderInstance, int TransID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatch_SelectByCustomerOrderDetail]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CUSTOMERORDERHEADERINSTANCE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(CustomerOrderHeaderInstance)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TRANSID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(TransID)));			
			Select(cmdToExecute,dataSet, tableName);
		}

		public int SelectUnprocessedCount(int letterTemplateID, int runID, DateTime dateFrom, DateTime dateTo, int customerOrderHeaderInstance, int transID) 
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatch_GetUnprocessedCount]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERTEMPLATEID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, letterTemplateID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUNID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(runID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATEFROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATETO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CUSTOMERORDERHEADERINSTANCE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(customerOrderHeaderInstance)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TRANSID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(transID)));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}

		public int Generate(int letterTemplateID, int letterBatchType, int runID, DateTime dateFrom, DateTime dateTo, int customerOrderHeaderInstance, int transID, bool isLocked, int userID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatch_Generate]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERTEMPLATEID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, letterTemplateID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERBATCHTYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, letterBatchType));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUNID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(runID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATEFROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATETO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CUSTOMERORDERHEADERINSTANCE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(customerOrderHeaderInstance)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TRANSID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(transID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ISLOCKED, SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isLocked));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_USERID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}

		public int Update(int ID, bool isPrinted, DateTime datePrinted, bool isLocked) 
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatch_Update]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ISPRINTED, SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isPrinted));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATEPRINTED, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(datePrinted)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ISLOCKED, SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isLocked));
			return ExecuteCmd(cmdToExecute);
		}

		public int Delete(int ID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatch_Delete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ID));
			return ExecuteCmd(cmdToExecute);
		}

		public int DeleteLetterBatchCustomerOrderDetail(int CustomerOrderHeaderInstance, int TransID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_LetterBatchCustomerOrderDetail_Delete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CUSTOMERORDERHEADERINSTANCE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerOrderHeaderInstance));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TRANSID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, TransID));
			return ExecuteCmd(cmdToExecute);
		}

		public void InactiveMagazineSelectAll(DataSet dtsDataSet, string tableName)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_InactiveMagazineLetterBatch_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public void InactiveMagazineSelectAll(DataSet dtsDataSet, string tableName, int letterTemplateID, DateTime dateCreatedFrom, DateTime dateCreatedTo, int letterBatchType, DateTime dateFrom, DateTime dateTo, int runIDFrom, int runIDTo, BooleanNullable isPrinted, BooleanNullable isLocked, string productCode, int reason)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_InactiveMagazineLetterBatch_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERTEMPLATEID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(letterTemplateID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATECREATEFROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateCreatedFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATECREATETO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateCreatedTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERBATCHTYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(letterBatchType)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATEFROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATETO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUNIDFROM, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(runIDFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUNIDTO, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(runIDTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ISPRINTED, SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isPrinted.Value));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ISLOCKED, SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isLocked.Value));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PRODUCTCODE, SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(productCode)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_REASON, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(reason)));
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public int InactiveMagazineSelectUnprocessedCount(int letterTemplateID, int runID, DateTime dateFrom, DateTime dateTo, int customerOrderHeaderInstance, int transID, string productCode, int reason) 
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_InactiveMagazineLetterBatch_GetUnprocessedCount]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERTEMPLATEID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, letterTemplateID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUNID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(runID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATEFROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATETO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CUSTOMERORDERHEADERINSTANCE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(customerOrderHeaderInstance)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TRANSID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(transID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PRODUCTCODE, SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(productCode)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_REASON, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(reason)));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}

		public int InactiveMagazineGenerate(int letterTemplateID, int letterBatchType, int runID, DateTime dateFrom, DateTime dateTo, int customerOrderHeaderInstance, int transID, bool isLocked, int userID, string productCode, int reason)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_InactiveMagazineLetterBatch_Generate]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERTEMPLATEID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, letterTemplateID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LETTERBATCHTYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, letterBatchType));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_RUNID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(runID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATEFROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateFrom)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATETO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(dateTo)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CUSTOMERORDERHEADERINSTANCE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(customerOrderHeaderInstance)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TRANSID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(transID)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ISLOCKED, SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isLocked));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_USERID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PRODUCTCODE, SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(productCode)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_REASON, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(reason)));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}

		public void InactiveMagazineSelectByCustomerOrderDetail(DataSet dataSet, string tableName, int CustomerOrderHeaderInstance, int TransID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_InactiveMagazineLetterBatch_SelectByCustomerOrderDetail]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CUSTOMERORDERHEADERINSTANCE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(CustomerOrderHeaderInstance)));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TRANSID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandleNull(TransID)));			
			Select(cmdToExecute,dataSet, tableName);
		}
	}
}
