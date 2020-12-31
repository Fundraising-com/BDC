///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'Season'
// Generated by GenerationClass v1.2.1949.28361 Final
// on: 5 mai, 2005, 19:57:33
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;
using dataSetRef = Common.TableDef.SeasonDataSet;


namespace DAL
{
	/// <summary>
	/// Purpose: Data Access class for the table 'Season'.
	/// </summary>
	public class SeasonData : DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ID= "@iID";
		internal const string PARAM_COUNTRY= "@sCountry";
		internal const string PARAM_NAME= "@sName";
		internal const string PARAM_FISCALYEAR= "@iFiscalYear";
		internal const string PARAM_SEASON= "@sSeason";
		internal const string PARAM_STARTDATE= "@daStartDate";
		internal const string PARAM_ENDDATE= "@daEndDate";
		internal const string PARAM_USERIDCHANGED= "@iUserIDChanged";
		internal const string PARAM_DEFAULTCONVERSIONRATE = "@nDefaultConversionRate";
		internal const string PARAM_DATE = "@dDate";
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
		public SeasonData() : base(DataBaseName.QSPCanadaCommon) { }
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
				insertCommand = new SqlCommand("dbo.[pr_Season_Insert]");
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_COUNTRY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTRY].SourceColumn = dataSet.Season.CountryColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_NAME,SqlDbType.VarChar));
				sqlParams[PARAM_NAME].SourceColumn = dataSet.Season.NameColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_FISCALYEAR,SqlDbType.Int));
				sqlParams[PARAM_FISCALYEAR].SourceColumn = dataSet.Season.FiscalYearColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SEASON,SqlDbType.Char));
				sqlParams[PARAM_SEASON].SourceColumn = dataSet.Season.SeasonColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STARTDATE,SqlDbType.DateTime));
				sqlParams[PARAM_STARTDATE].SourceColumn = dataSet.Season.StartDateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ENDDATE,SqlDbType.DateTime));
				sqlParams[PARAM_ENDDATE].SourceColumn = dataSet.Season.EndDateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_USERIDCHANGED,SqlDbType.Int));
				sqlParams[PARAM_USERIDCHANGED].SourceColumn = dataSet.Season.UserIDChangedColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_DEFAULTCONVERSIONRATE,SqlDbType.Decimal));
				sqlParams[PARAM_DEFAULTCONVERSIONRATE].SourceColumn = dataSet.Season.DefaultConversionRateColumn.ColumnName;
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
				deleteCommand = new SqlCommand("dbo.[pr_Season_Delete]");
				deleteCommand.CommandType = CommandType.StoredProcedure;
				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = dataSet.Season.IDColumn.ColumnName;
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
				updateCommand = new SqlCommand("dbo.[pr_Season_Update]");
				updateCommand.CommandType = CommandType.StoredProcedure;
				updateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = dataSet.Season.IDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COUNTRY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTRY].SourceColumn = dataSet.Season.CountryColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_NAME,SqlDbType.VarChar));
				sqlParams[PARAM_NAME].SourceColumn = dataSet.Season.NameColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_FISCALYEAR,SqlDbType.Int));
				sqlParams[PARAM_FISCALYEAR].SourceColumn = dataSet.Season.FiscalYearColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SEASON,SqlDbType.Char));
				sqlParams[PARAM_SEASON].SourceColumn = dataSet.Season.SeasonColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STARTDATE,SqlDbType.DateTime));
				sqlParams[PARAM_STARTDATE].SourceColumn = dataSet.Season.StartDateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ENDDATE,SqlDbType.DateTime));
				sqlParams[PARAM_ENDDATE].SourceColumn = dataSet.Season.EndDateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_USERIDCHANGED,SqlDbType.Int));
				sqlParams[PARAM_USERIDCHANGED].SourceColumn = dataSet.Season.UserIDChangedColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_DEFAULTCONVERSIONRATE,SqlDbType.Decimal));
				sqlParams[PARAM_DEFAULTCONVERSIONRATE].SourceColumn = dataSet.Season.DefaultConversionRateColumn.ColumnName;
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
		///		 <LI>Country</LI>
		///		 <LI>Name</LI>
		///		 <LI>FiscalYear</LI>
		///		 <LI>Season</LI>
		///		 <LI>StartDate</LI>
		///		 <LI>EndDate</LI>
		///		 <LI>DateChanged</LI>
		///		 <LI>UserIDChanged</LI>
		///		 <LI>DefaultConversionRate</LI>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public  void SelectOne(DataSet dtsDataSet, string tableName, Int32 ID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Season_SelectOne]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ID));
			Select(cmdToExecute, dtsDataSet, tableName);
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
			cmdToExecute.CommandText = "dbo.[pr_Season_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public  void SelectAllFiscalYears(DataSet dtsDataSet, string tableName)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_FiscalYear_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public  void SelectOneByDate(DataSet dtsDataSet, string tableName, DateTime date)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Season_SelectOneByDate]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, date));
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		/// <summary>
		/// get the list of season letters
		/// </summary>
		public void SelectSeasonLetters(DataSet dtsDataSet, string tableName)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Season_SelectAllSeasonLetters]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		/// <summary>
		/// get the count of seasons that have given year and season in
		/// the Season table
		/// </summary>
		public int SelectCountByYearSeason(int id, int fiscalYear, string season)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Season_SelectOneByYearSeason]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ID, id));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FISCALYEAR, fiscalYear));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SEASON, season));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}

		/// <summary>
		/// get the last Fiscal Year and it's maximum Default Conversion Rate 
		/// from Seasons table
		/// </summary>
		public void SelectLastYearAndRate(DataSet ds, string tableName)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Season_SelectLastYearAndRate]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute, ds, tableName);
		}

		/// <summary>
		/// check if given season is referenced in the database
		/// </summary>
		/// <param name="seasonID">Season ID to check</param>
		/// <returns>1=referenced, 0=not</returns>
		public int IsSeasonRefrenced(int seasonID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_Season_ValidateDelete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ID, seasonID));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}
		public  void SelectCurrentSeasonStartAndEnd(DataSet dtsDataSet, string tableName)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[GetCurrentSeasonStartAndEndDate]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,dtsDataSet, tableName);
		}
	}
}