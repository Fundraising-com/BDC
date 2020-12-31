///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'Phone'
// Generated by GenerationClass v1.2.1949.28361 Final
// on: 11 mai, 2005, 15:38:38
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;
using dataSetRef = Common.TableDef.PhoneDataSet;


namespace DAL
{
	/// <summary>
	/// Purpose: Data Access class for the table 'Phone'.
	/// </summary>
	public class PhoneData : DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ID= "@iID";
		internal const string PARAM_TYPE= "@iType";
		internal const string PARAM_PHONELISTID= "@iPhoneListID";
		internal const string PARAM_PHONENUMBER= "@sPhoneNumber";
		internal const string PARAM_BESTTIMETOCALL= "@sBestTimeToCall";
		//

		// DataSetCommand object

		//

		private SqlCommand insertCommand;

		private SqlCommand deleteCommand;

		private SqlCommand updateCommand;

		#endregion

		dataSetRef dataSet = new dataSetRef();



		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public PhoneData() : base(DataBaseName.QSPCanadaCommon) { }

		//----------------------------------------------------------------
		// Sub GetInsertCommand:
		//   Initialize the parameterized Insert command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetInsertCommand()
		{
			if ( insertCommand == null )
			{
				//
				// Construct the command since we don't have it already
				// 
				insertCommand = new SqlCommand("dbo.[pr_Phone_Insert]");
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].Direction = ParameterDirection.Output;
				sqlParams[PARAM_ID].SourceColumn = dataSet.Phone.IDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_TYPE,SqlDbType.Int));
				sqlParams[PARAM_TYPE].SourceColumn = dataSet.Phone.TypeColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_PHONELISTID,SqlDbType.Int));
				sqlParams[PARAM_PHONELISTID].SourceColumn = dataSet.Phone.PhoneListIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_PHONENUMBER,SqlDbType.VarChar));
				sqlParams[PARAM_PHONENUMBER].SourceColumn = dataSet.Phone.PhoneNumberColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_BESTTIMETOCALL,SqlDbType.VarChar));
				sqlParams[PARAM_BESTTIMETOCALL].SourceColumn = dataSet.Phone.BestTimeToCallColumn.ColumnName;
			}
			return insertCommand;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetDeleteCommand()
		{
			if ( deleteCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				deleteCommand = new SqlCommand("dbo.[pr_Phone_Delete]");
				deleteCommand.CommandType = CommandType.StoredProcedure;
				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = dataSet.Phone.IDColumn.ColumnName;
			}
			return deleteCommand;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
			if ( updateCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				updateCommand = new SqlCommand("dbo.[pr_Phone_Update]");
				updateCommand.CommandType = CommandType.StoredProcedure;
				updateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = dataSet.Phone.IDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_TYPE,SqlDbType.Int));
				sqlParams[PARAM_TYPE].SourceColumn = dataSet.Phone.TypeColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_PHONELISTID,SqlDbType.Int));
				sqlParams[PARAM_PHONELISTID].SourceColumn = dataSet.Phone.PhoneListIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_PHONENUMBER,SqlDbType.VarChar));
				sqlParams[PARAM_PHONENUMBER].SourceColumn = dataSet.Phone.PhoneNumberColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_BESTTIMETOCALL,SqlDbType.VarChar));
				sqlParams[PARAM_BESTTIMETOCALL].SourceColumn = dataSet.Phone.BestTimeToCallColumn.ColumnName;
			}
			return updateCommand;
		}


		/// <summary>
		/// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>ID</LI>
		/// </UL>
		///		 <LI>ID</LI>
		///		 <LI>Type</LI>
		///		 <LI>PhoneListID</LI>
		///		 <LI>PhoneNumber</LI>
		///		 <LI>BestTimeToCall</LI>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public  void SelectOne(DataSet dtsDataSet, string tableName, Int32 ID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Phone_SelectOne]";
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
			cmdToExecute.CommandText = "dbo.[pr_Phone_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}


		/// <summary>
		/// Purpose: Select method for a foreign key. This method will Select one or more rows from the database, based on the Foreign Key 'PhoneListID'
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>PhoneListID. May be SqlInt32.Null</LI>
		/// </UL>
		/// </remarks>
		public void SelectAllByPhoneListID(DataSet dtsDataSet, string tableName, Int32 PhoneListID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Phone_SelectAllByPhoneListID]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PHONELISTID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PhoneListID));
			Select(cmdToExecute,dtsDataSet, tableName);
		}
	}
}