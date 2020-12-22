///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'product'
// Generated by Jas on: Monday, November 03, 2003, 4:18:07 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.Promo_LogoSubdivisionTable;

namespace QSPForm.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'product'.
	/// </summary>
	public class Promo_logoSubdivision : DBTableOperation
	{

		#region Parameter
		//Stored procedure parameter names
		public const string PARAM_PKID = "@iPromo_logo_subdivision_id";
		public const string PARAM_SUBDIVISION_CODE = "@ssubdivision_code";
		public const string PARAM_Promo_logo_ID = "@iPromo_logo_id";
		
		//
		// Stored procedure names for each operation
		private const string SQL_PROC_INSERT = "pr_QSPForm_Promo_logo_Subdivision_Insert";
		private const string SQL_PROC_UPDATE = "pr_QSPForm_Promo_logo_Subdivision_Update";
		private const string SQL_PROC_DELETE = "pr_QSPForm_Promo_logo_Subdivision_Delete";
		private const string SQL_PROC_SELECT_ONE = "pr_QSPForm_Promo_logo_Subdivision_SelectOne";
		private const string SQL_PROC_SELECT_ALL = "pr_QSPForm_Promo_logo_Subdivision_SelectAll";
		private const string SQL_PROC_SELECT_ALL_BY_Promo_logo_ID = "pr_QspForm_Promo_logo_Subdivision_SelectAllByPromo_logoID";

		#endregion

		//
		// DataSetCommand object
		//
		
		private SqlCommand insertCommand;
		private SqlCommand updateCommand;
		private SqlCommand deleteCommand;

		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public Promo_logoSubdivision()
		{
			// Nothing for now.
		}

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
				insertCommand = new SqlCommand(SQL_PROC_INSERT);
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
            
				SqlParameterCollection sqlParams = insertCommand.Parameters;
            
				//Fill the SqlParameterCollection
				FillParams(sqlParams);				
								
				//Adjust paramters direction
				sqlParams[PARAM_PKID].Direction = ParameterDirection.Output;				
				
				//Map the source column
				MapColumn(sqlParams);	
				
				//Adjust for insert context
				sqlParams.Add(new SqlParameter(PARAM_CREATE_USER_ID, SqlDbType.Int));
				sqlParams[PARAM_CREATE_USER_ID].SourceColumn = dataDef.FLD_CREATE_USER_ID;

			}
            
			return insertCommand;
		}

		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
			if ( updateCommand == null )
			{
				
				updateCommand = new SqlCommand(SQL_PROC_UPDATE);
				updateCommand.CommandType = CommandType.StoredProcedure;
				
            
				SqlParameterCollection sqlParams = updateCommand.Parameters;
            
				//Fill the SqlParameterCollection
				FillParams(sqlParams);

				//Map the source column
				MapColumn(sqlParams);

				
				//Adjust for update context
				sqlParams.Add(new SqlParameter(PARAM_UPDATE_USER_ID, SqlDbType.Int));
				sqlParams[PARAM_UPDATE_USER_ID].SourceColumn = dataDef.FLD_UPDATE_USER_ID;

				sqlParams.Add(new SqlParameter(PARAM_DELETED, SqlDbType.Bit));
				sqlParams[PARAM_DELETED].SourceColumn = dataDef.FLD_DELETED;


			}
            
			return updateCommand;
		}
		//-----------------------
		// Set Mapping and Params in 2 differents method
		//-----------------------

		private void FillParams(SqlParameterCollection param)
		{
			param.Add(new SqlParameter(PARAM_PKID,SqlDbType.Int));
			param.Add(new SqlParameter(PARAM_Promo_logo_ID,SqlDbType.Int));
			param.Add(new SqlParameter(PARAM_SUBDIVISION_CODE,SqlDbType.VarChar));
		}

		private void MapColumn(SqlParameterCollection param)
		{
			param[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
			param[PARAM_Promo_logo_ID].SourceColumn = dataDef.FLD_PROMO_LOGO_ID;
			param[PARAM_SUBDIVISION_CODE].SourceColumn = dataDef.FLD_SUBDIVISION_CODE;
		}

		protected override string TableName
		{
			get{return dataDef.TBL_Promo_logo_SUBDIVISION;}
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
				deleteCommand = new SqlCommand(SQL_PROC_DELETE);
				deleteCommand.CommandType = CommandType.StoredProcedure;
				
				SqlParameterCollection sqlParams = deleteCommand.Parameters;
            
				sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_UPDATE_USER_ID, SqlDbType.Int));
				//
				// Define the parameter mappings from the data tabl
				//
				sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;	
				sqlParams[PARAM_UPDATE_USER_ID].SourceColumn = dataDef.FLD_UPDATE_USER_ID;
				//Only Mapped DataColumn are imply...
			}
            
			return deleteCommand;
		}

		//public dataDef SelectAll_Search(int SearchType, String Criteria, int SubdivisionID, int NationalStatus, int DisplayStatus, string StartDate, string EndDate, string FM_ID, bool IncludeFMReportedTo)
		public dataDef SelectAll_Search(int SearchType, String Criteria, int SubdivisionID, int NationalStatus, int DisplayStatus, DateTime StartDate, DateTime EndDate, string FM_ID, bool IncludeFMReportedTo)
		{
			return null;
		}

		public dataDef SelectAll_Search(string Criteria, string FSM_ID)
		{
			/*
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_QSPForm_Promo_logo_SelectAll_Search";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FSM_ID, SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FSM_ID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SEARCH_CRITERIA, SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Criteria));
			Select(cmdToExecute,Table);
			return Table;
			*/
			return null;
		}
		public new dataDef SelectAll()
		{
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,Table);
			return Table;
		}
		public new dataDef SelectOne(int Promo_logoSubdivisionID)
		{
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ONE;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Promo_logoSubdivisionID));
			Select(cmdToExecute,Table);
			return Table;
		}
		public dataDef SelectAllByPromo_logoID(int Promo_logoID)
		{
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL_BY_Promo_logo_ID;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_Promo_logo_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Promo_logoID));
			Select(cmdToExecute,Table);
			return Table;
		}

		/*
		
		//We create a new method when we want to the return type variable
		public new dataDef SelectOne(int ProductID)
		{
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ONE;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ProductID));
			
			Select(cmdToExecute,Table);

			return Table;
				
		}

		public new dataDef SelectAll()
		{
			dataDef Table = new dataDef();

			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			
					
			Select(cmdToExecute,Table);
			return  Table;
			
		}

		public dataDef SelectAllWproduct_type_idLogic(int ProductType)
		{
			dataDef Table = new dataDef();

			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_product_SelectAllWproduct_type_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PRODUCT_TYPE_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ProductType));
			
					
			Select(cmdToExecute,Table);
			
			return  Table;
			
		}
		*/
	
	}
}
