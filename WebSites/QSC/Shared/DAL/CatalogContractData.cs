using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;
//using dataSetRef = Common.TableDef.CatalogContractDataSet;

namespace DAL
{
	/// <summary>
	/// Data Access Layer object for CatalogContract
	/// </summary>
	public class CatalogContractData : DBTableOperation
	{
		#region Class Member Declarations

		private SqlCommand insertCommand = null;

		private SqlCommand deleteCommand = null;

		private SqlCommand updateCommand = null;

		#endregion

		//dataSetRef dataSet = new dataSetRef();

		public CatalogContractData() : base(DataBaseName.QSPCanadaOrderManagement) { }

		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// </remarks>
		public void SelectAll(DataSet dtsDataSet, string tableName, int CatalogID, int CatalogIDLastSeason)		
		{
			try
			{
				SqlCommand	cmdToExecute = new SqlCommand();
				cmdToExecute.CommandText = "QSPCanadaProduct.dbo.[pr_CatalogContractInfo_SelectAll]";
				cmdToExecute.CommandType = CommandType.StoredProcedure;
				cmdToExecute.Parameters.Add(new SqlParameter("@CatalogID", CatalogID));
				cmdToExecute.Parameters.Add(new SqlParameter("@CatalogIDLastSeason", CatalogIDLastSeason));
				Select(cmdToExecute,dtsDataSet, tableName);
			}
			catch(SqlException) 
			{
				throw new Exception();
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

