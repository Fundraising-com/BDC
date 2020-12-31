using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;
using dataSetRef = Common.TableDef.LetterTemplateDataSet;


namespace DAL
{
	/// <summary>
	/// Purpose: Data Access class for the table 'LetterTemplate'.
	/// </summary>
	public class LetterTemplateData : DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ID = "@iID";

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
		public LetterTemplateData() : base(DataBaseName.QSPCanadaOrderManagement) { }
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
			cmdToExecute.CommandText = "dbo.[pr_LetterTemplate_SelectOne]";
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
			cmdToExecute.CommandText = "dbo.[pr_LetterTemplate_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}
	}
}
