using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using QSPForm.Common.DataDef;
using QSPForm.Common;
using dataDef = QSPForm.Common.DataDef.ProgramAgreementCatalogTable;

namespace QSPForm.Data
{
    public class ProgramAgreementCatalog :  DBTableOperation
    {
        #region Parameter
        //Stored procedure parameter names
        public const string PARAM_PKID = "@iprogram_agreement_catalog_id";
        public const String PARAM_PROGRAM_AGREEMENT_ID = "@iprogram_agreement_id";
        public const String PARAM_CATALOG_ID = "@icatalog_id";
        public const String PARAM_CATALOG_NAME = "@icatalog_name";
        public const string PARAM_ENTITY_TYPE_ID = "@ientity_type_id";
        public const string PARAM_ENTITY_ID = "@ientity_id";	

        //
        // Stored procedure names for each operation
        private const string SQL_PROC_DELETE = "pr_program_agreement_catalog_Delete";
        private const string SQL_PROC_INSERT = "pr_program_agreement_catalog_Insert";
        private const string SQL_PROC_UPDATE = "pr_program_agreement_catalog_Update";
       
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
		public ProgramAgreementCatalog()
		{
			// Nothing for now.
		}

        //----------------------------------------------------------------
        // Sub GetInsertCommand:
        //   Initialize the parameterized Insert command for the DataAdapter
        //----------------------------------------------------------------
        protected override SqlCommand GetInsertCommand()
        {
            if (insertCommand == null)
            {
                //
                // Construct the command since we don't have it already
                // 
                insertCommand = new SqlCommand(SQL_PROC_INSERT);
                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                SqlParameterCollection sqlParams = insertCommand.Parameters;
                sqlParams.Add(new SqlParameter(PARAM_PROGRAM_AGREEMENT_ID, SqlDbType.Int));
                sqlParams.Add(new SqlParameter(PARAM_CATALOG_ID, SqlDbType.Int));

                sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));

                sqlParams[PARAM_PKID].Direction = ParameterDirection.Output;		
                //
                // Define the parameter mappings from the data table in the
                // dataset.
                //
                sqlParams[PARAM_PROGRAM_AGREEMENT_ID].SourceColumn = dataDef.FLD_PROGRAM_AGREEMENT_ID;
                sqlParams[PARAM_CATALOG_ID].SourceColumn = dataDef.FLD_CATALOG_ID;
                
                //Don't need to map ErrorCode cause is not imply in the insert
                //Only Mapped DataColumn are imply...
            }

            return insertCommand;
        }

        //----------------------------------------------------------------
        // Sub BuildUpdateCommand:
        //   Initialize the parameterized Update command for the DataAdapter
        //----------------------------------------------------------------
        protected override SqlCommand GetUpdateCommand()
        {
            if (updateCommand == null)
            {
                //
                // Construct the command since we don't have it already
                //
                updateCommand = new SqlCommand(SQL_PROC_UPDATE);
                updateCommand.CommandType = CommandType.StoredProcedure;


                SqlParameterCollection sqlParams = updateCommand.Parameters;

                sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
                sqlParams.Add(new SqlParameter(PARAM_PROGRAM_AGREEMENT_ID, SqlDbType.Int));
                sqlParams.Add(new SqlParameter(PARAM_CATALOG_ID, SqlDbType.Int));
               
                // Define the parameter mappings from the data tabl
                //
                sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
                sqlParams[PARAM_PROGRAM_AGREEMENT_ID].SourceColumn = dataDef.FLD_PROGRAM_AGREEMENT_ID;
                sqlParams[PARAM_CATALOG_ID].SourceColumn = dataDef.FLD_CATALOG_ID;
                //Don't need to map ErrorCode cause is not imply in the insert
                //Only Mapped DataColumn are imply...
            }

            return updateCommand;
        }

        //----------------------------------------------------------------
        // Sub BuildUpdateCommand:
        //   Initialize the parameterized Update command for the DataAdapter
        //----------------------------------------------------------------
        protected override SqlCommand GetDeleteCommand()
        {
            if (deleteCommand == null)
            {
                //
                // Construct the command since we don't have it already
                //
                deleteCommand = new SqlCommand(SQL_PROC_DELETE);
                deleteCommand.CommandType = CommandType.StoredProcedure;

                SqlParameterCollection sqlParams = deleteCommand.Parameters;

                sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
                //
                // Define the parameter mappings from the data tabl
                //
                sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
                //Only Mapped DataColumn are imply...
            }

            return deleteCommand;
        }

        protected override string TableName
        {
            get { return dataDef.TBL_PROGRAM_AGREEMENT_CATALOG; }
        }

        public dataDef SelectAllProgramAgreementCatalogs(int programAgreementId, int entityTypeId)
        {
            dataDef Table = new dataDef();

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_program_agreement_catalogs_SelectOne";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter("@iprogram_agreement_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, programAgreementId));

            Select(cmdToExecute, Table);
            
            foreach (DataRow row in Table.Rows)
            {
                row[ProgramAgreementCatalogTable.FLD_PROGRAM_AGREEMENT_ID] = programAgreementId;
                row[ProgramAgreementCatalogTable.FLD_ENTITY_TYPE_ID] = entityTypeId;
                row[ProgramAgreementCatalogTable.FLD_ENTITY_ID] = programAgreementId;
            }
            return Table;
        }


    }
}
