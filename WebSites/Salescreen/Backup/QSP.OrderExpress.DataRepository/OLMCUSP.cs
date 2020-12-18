///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'OLMCUSP'
// Based on code originally generated by LLBLGen v1.2.1402.29234 Final .
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using tableRef = QSPForm.Common.DataDef.OLMCUSPTable;
using QSPForm.Common;
using System.Data.SqlClient;

namespace QSPForm.DataRepository
{
	///<summary>Data Access class for the table 'OLMCUSP'.</summary>
	public class OLMCUSP : DBInteractionBase
	{
		#region Parameters
		//Stored procedure parameter names
		public const string PARAM_PKID = "OLCUST";
		public const string PARAM_ACCOUNT_ID = "OL#INT";
		//
		// Stored procedure names for each operation
//		private const string SQL_PROC_SELECT_ONE = "QDSDATA.dbo.pr_OLMCUSP_SelectOne";
        private const string SQL_PROC_SELECT_ALL = "QDSDATA.dbo.pr_OLMCUSP_SelectAll";
//		private const string SQL_PROC_SELECT_ALL_WSCHOOLNAME = "QDSDATA.dbo.pr_OLMCUSP_SelectAllWSchoolNameLogic";
//		private const string SQL_PROC_SELECT_ALL_SCHOOLSEARCH = "QDSDATA.dbo.pr_OLMCUSP_SelectAllSchool_Search";
		#endregion Parameters

		#region Constructors
		///<summary>default constructor</summary>
		public OLMCUSP()
		{
			// Nothing for now.
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>iInstance</LI>
		/// </UL>
		/// Properties set after a succesful call of this method: 
		/// <UL>
		///		 <LI>iErrorCode</LI>
		///		 <LI>sUserName</LI>
		///		 <LI>sPassword</LI>
		/// </UL>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public new tableRef SelectOne(int OLCUST)
		{
			tableRef Table = new tableRef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "SELECT * FROM QDSDATA.dbo.OLMCUSP WHERE OLCUST = ?";
			cmdToExecute.CommandType = CommandType.Text;

			cmdToExecute.Parameters.Add(
				new SqlParameter("PAR_OLCUST", 
												SqlDbType.Decimal, 
												0, 
												System.Data.ParameterDirection.Input, 
												true, 
												((System.Byte)(16)), 
												((System.Byte)(0)), 
												"", 
												System.Data.DataRowVersion.Current, 
												OLCUST));

			Select(cmdToExecute,Table);

			return Table;
		}

		public tableRef SelectOneWAccount_idLogic(int AccountID)
		{
			tableRef Table = new tableRef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "SELECT * FROM QDSDATA.dbo.OLMCUSP WHERE OL#INT = ?";
			cmdToExecute.CommandType = CommandType.Text;

			cmdToExecute.Parameters.Add(
				new SqlParameter("PAR_OL#INT", 
				SqlDbType.Decimal, 
				0, 
				System.Data.ParameterDirection.Input, 
				true, 
				((System.Byte)(16)), 
				((System.Byte)(0)), 
				"", 
				System.Data.DataRowVersion.Current, 
				AccountID));

			Select(cmdToExecute,Table);

			return Table;
		}

		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties set after a succesful call of this method: 
		/// <UL>
		///		 <LI>iErrorCode</LI>
		/// </UL>
		/// </remarks>
		public new tableRef SelectAll()
		{
			tableRef Table = new tableRef();

			SqlCommand	cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "SELECT * FROM QDSDATA.dbo.OLMCUSP";
            cmdToExecute.CommandType = CommandType.Text;

			Select(cmdToExecute,Table);
			//AssignInnerProperty(Table);
			return  Table;

		}

		
		#endregion Methods

	}
}
