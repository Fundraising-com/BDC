///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'Product'
// Generated by LLBLGen v1.2.1655.16789 Final
// on: Tuesday, July 13, 2004, 10:23:38 AM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
//using tableRef =QSPFulfillment.DataAccess.Common.TableDef.ProductTable;


namespace QSPFulfillment.DataAccess.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'Product'.
	/// </summary>
	public class CatalogSectionData : QSPFulfillment.DataAccess.Data.DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ID = "@iID";
		internal const string PARAM_PROGRAMSECTIONID = "@iProgramSectionID";

		//

		// DataSetCommand object

		//

		private SqlCommand insertCommand;

		private SqlCommand deleteCommand;

		private SqlCommand updateCommand;

		#endregion


		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public CatalogSectionData()
		{
			// Nothing for now.
		}
		//----------------------------------------------------------------
		// Sub GetInsertCommand:
		//   Initialize the parameterized Insert command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetInsertCommand()
		{
			insertCommand = null;

			return insertCommand;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetDeleteCommand()
		{
			deleteCommand = null;

			return deleteCommand;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
			updateCommand = null;

			return updateCommand;
		}
		protected override string TableName
		{
			get
			{
				return "QSPCanadaProduct.dbo.ProgramSection";
			}
		}

		public void SelectSearch(DataTable table, int catalogID) 
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "QSPCanadaProduct.dbo.pr_SelectAllCatalogSections";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@iCatalogID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, catalogID));
			Select(cmdToExecute, table);
		}

		public int Insert(int catalogID, string catalogCode, int type, string name, int fsProgramID, string userID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "QSPCanadaProduct.dbo.pr_InsertCatalogSection";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@iCatalogID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, catalogID));
			cmdToExecute.Parameters.Add(new SqlParameter("@zCatalogCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, catalogCode));
			cmdToExecute.Parameters.Add(new SqlParameter("@iType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, type));
			cmdToExecute.Parameters.Add(new SqlParameter("@zName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, name));
			cmdToExecute.Parameters.Add(new SqlParameter("@iFSProgramID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fsProgramID));
			cmdToExecute.Parameters.Add(new SqlParameter("@zUserID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}

		public int Update(int catalogSectionID, string catalogCode, int type, string name, int fsProgramID, string userID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "QSPCanadaProduct.dbo.pr_UpdateCatalogSection";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@iCatalogSectionID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, catalogSectionID));
			cmdToExecute.Parameters.Add(new SqlParameter("@zCatalogCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, catalogCode));
			cmdToExecute.Parameters.Add(new SqlParameter("@iType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, type));
			cmdToExecute.Parameters.Add(new SqlParameter("@zName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, name));
			cmdToExecute.Parameters.Add(new SqlParameter("@iFSProgramID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fsProgramID));
			cmdToExecute.Parameters.Add(new SqlParameter("@zUserID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));
			return ExecuteCmd(cmdToExecute);
		}

		public int Delete(int catalogSectionID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "QSPCanadaProduct.dbo.pr_DeleteCatalogSection";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@iCatalogSectionID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, catalogSectionID));
			return ExecuteCmd(cmdToExecute);
		}

		public void SelectAllCatalogSectionTypes(DataTable table)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "QSPCanadaProduct.dbo.pr_SelectAllCatalogSectionTypes";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute, table);
		}

		public int SelectDefaultProductTypeByCatalogSection(int catalogSectionID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "QSPCanadaProduct.dbo.pr_SelectDefaultProductTypeByProgramSection";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PROGRAMSECTIONID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, catalogSectionID));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}

		public int SelectCustomerOrderDetailCount(int catalogSectionID) 
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "QSPCanadaProduct.dbo.pr_SelectCustomerOrderDetailCountByCatalogSectionID";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@iCatalogSectionID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, catalogSectionID));
			return Convert.ToInt32(ExecuteScalar(cmdToExecute));
		}
	}
}
