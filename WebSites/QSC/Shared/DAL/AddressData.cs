///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'Address'
// Generated by GenerationClass v1.2.1949.28361 Final
// on: 10 mai, 2005, 22:13:20
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;
using dataSetRef = Common.TableDef.AddressDataSet;


namespace DAL
{
	/// <summary>
	/// Purpose: Data Access class for the table 'Address'.
	/// </summary>
	public class AddressData : DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ADDRESS_ID= "@iaddress_id";
		internal const string PARAM_STREET1= "@sstreet1";
		internal const string PARAM_STREET2= "@sstreet2";
		internal const string PARAM_CITY= "@scity";
		internal const string PARAM_STATEPROVINCE= "@sstateProvince";
		internal const string PARAM_POSTAL_CODE= "@spostal_code";
		internal const string PARAM_ZIP4= "@szip4";
		internal const string PARAM_COUNTRY= "@scountry";
		internal const string PARAM_ADDRESS_TYPE= "@iaddress_type";
		internal const string PARAM_ADDRESSLISTID= "@iAddressListID";
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
		public AddressData() : base(DataBaseName.QSPCanadaCommon) { }

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
				insertCommand = new SqlCommand("dbo.[pr_Address_Insert]");
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS_ID,SqlDbType.Int));
				sqlParams[PARAM_ADDRESS_ID].Direction = ParameterDirection.Output;
				sqlParams[PARAM_ADDRESS_ID].SourceColumn = dataSet.Address.address_idColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STREET1,SqlDbType.VarChar));
				sqlParams[PARAM_STREET1].SourceColumn = dataSet.Address.street1Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STREET2,SqlDbType.VarChar));
				sqlParams[PARAM_STREET2].SourceColumn = dataSet.Address.street2Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar));
				sqlParams[PARAM_CITY].SourceColumn = dataSet.Address.cityColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATEPROVINCE,SqlDbType.VarChar));
				sqlParams[PARAM_STATEPROVINCE].SourceColumn = dataSet.Address.stateProvinceColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_POSTAL_CODE,SqlDbType.VarChar));
				sqlParams[PARAM_POSTAL_CODE].SourceColumn = dataSet.Address.postal_codeColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ZIP4,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP4].SourceColumn = dataSet.Address.zip4Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COUNTRY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTRY].SourceColumn = dataSet.Address.countryColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS_TYPE,SqlDbType.Int));
				sqlParams[PARAM_ADDRESS_TYPE].SourceColumn = dataSet.Address.address_typeColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESSLISTID,SqlDbType.Int));
				sqlParams[PARAM_ADDRESSLISTID].SourceColumn = dataSet.Address.AddressListIDColumn.ColumnName;
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
				deleteCommand = new SqlCommand("dbo.[pr_Address_Delete]");
				deleteCommand.CommandType = CommandType.StoredProcedure;
				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS_ID,SqlDbType.Int));
				sqlParams[PARAM_ADDRESS_ID].SourceColumn = dataSet.Address.address_idColumn.ColumnName;
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
				updateCommand = new SqlCommand("dbo.[pr_Address_Update]");
				updateCommand.CommandType = CommandType.StoredProcedure;
				updateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS_ID,SqlDbType.Int));
				sqlParams[PARAM_ADDRESS_ID].SourceColumn = dataSet.Address.address_idColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STREET1,SqlDbType.VarChar));
				sqlParams[PARAM_STREET1].SourceColumn = dataSet.Address.street1Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STREET2,SqlDbType.VarChar));
				sqlParams[PARAM_STREET2].SourceColumn = dataSet.Address.street2Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar));
				sqlParams[PARAM_CITY].SourceColumn = dataSet.Address.cityColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATEPROVINCE,SqlDbType.VarChar));
				sqlParams[PARAM_STATEPROVINCE].SourceColumn = dataSet.Address.stateProvinceColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_POSTAL_CODE,SqlDbType.VarChar));
				sqlParams[PARAM_POSTAL_CODE].SourceColumn = dataSet.Address.postal_codeColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ZIP4,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP4].SourceColumn = dataSet.Address.zip4Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COUNTRY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTRY].SourceColumn = dataSet.Address.countryColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS_TYPE,SqlDbType.Int));
				sqlParams[PARAM_ADDRESS_TYPE].SourceColumn = dataSet.Address.address_typeColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESSLISTID,SqlDbType.Int));
				sqlParams[PARAM_ADDRESSLISTID].SourceColumn = dataSet.Address.AddressListIDColumn.ColumnName;
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
		///		 <LI>Address_id</LI>
		/// </UL>
		///		 <LI>Address_id</LI>
		///		 <LI>Street1</LI>
		///		 <LI>Street2</LI>
		///		 <LI>City</LI>
		///		 <LI>StateProvince</LI>
		///		 <LI>Postal_code</LI>
		///		 <LI>Zip4</LI>
		///		 <LI>Country</LI>
		///		 <LI>Address_type</LI>
		///		 <LI>AddressListID</LI>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public  void SelectOne(DataSet dtsDataSet, string tableName, Int32 address_id)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Address_SelectOne]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ADDRESS_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, address_id));
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
			cmdToExecute.CommandText = "dbo.[pr_Address_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}


		/// <summary>
		/// Purpose: Select method for a foreign key. This method will Select one or more rows from the database, based on the Foreign Key 'AddressListID'
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>AddressListID. May be SqlInt32.Null</LI>
		/// </UL>
		/// </remarks>
		public void SelectAllByAddressListID(DataSet dtsDataSet, string tableName, Int32 AddressListID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Address_SelectAllByAddressListID]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ADDRESSLISTID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, AddressListID));
			Select(cmdToExecute,dtsDataSet, tableName);
		}
	}
}