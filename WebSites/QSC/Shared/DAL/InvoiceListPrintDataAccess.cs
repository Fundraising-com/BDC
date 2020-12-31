using System;
//using System.Reflection;
//using System.EnterpriseServices;
using System.Data;
using System.Data.SqlClient;
//using Debug = System.Diagnostics.Debug;
//using System.Text.RegularExpressions;
//using System.Configuration;


namespace DAL
{
	/// <summary>
	/// InvoiceListPrintDataAcess
	/// This is the data access layer for the InvoiceListPrint.aspx page
	/// </summary>
	/// <remarks>
	/// Updated by Jeff Miles
	/// October, 2006
	/// Updated to return results for one FM only
	/// </remarks>
	public class InvoiceListPrintDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public InvoiceListPrintDataAccess(){}

			
		public DataTable GetAllInvoicesToPrint(int? fiscalYear, string FMID, bool showOnlyAccountsInOwing, bool showNonPrinted)
		{
			DataTable dt = null;
			try
			{
				dt = SqlHelper.ExecuteDataTable(
                    connection, 
                    CommandType.StoredProcedure, 
                    "QSPCanadaFinance.dbo.GetAllInvoicesToPrint",
                    new SqlParameter("@FiscalYear", fiscalYear),
					     new SqlParameter("@FMID", FMID),
                    new SqlParameter("@ShowInOwingOnly", showOnlyAccountsInOwing ? 1 : 0),
                    new SqlParameter("@ShowNonPrinted", showNonPrinted ? 1 : 0)
                    );
			}
			catch(InvalidOperationException eIO)
			{
				//cleanup stuff

				//re-throw
				throw eIO;
			}
			catch (SqlException eSQL)
			{
				//cleanup stuff

				//re-throw
				throw eSQL;
			}
			catch(Exception eGeneric)
			{
				//cleanup stuff

				//re-throw
				throw eGeneric;
			}
			return dt;
		}

		public int UpdateInvoicePrintedStatus(string strInvoiceID, string strChangedBy, string isPrinted)
		{
			int x = 0;
			try
			{
				x =  SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaFinance.dbo.UpdateInvoicePrintedStatus", 
					new SqlParameter("@InvoiceID", strInvoiceID),
					new SqlParameter("@ChangedBy", strChangedBy),
					new SqlParameter("@IsPrinted", isPrinted));
			}
			catch(InvalidOperationException eIO)
			{
				//cleanup stuff

				//re-throw
				throw eIO;
			}
			catch (SqlException eSQL)
			{
				//cleanup stuff

				//re-throw
				throw eSQL;
			}
			catch(Exception eGeneric)
			{
				//cleanup stuff

				//re-throw
				throw eGeneric;
			}
			return x;
		}

	}
		
}
