using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for CodeDetailDataAccess.
	/// </summary>
	public class BankAccountData : QDataAccess
	{
		public BankAccountData()
		{
			
		}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaFinance..GetBankAccount")]
		public DataSet GetAllBankAccount([SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string  FieldName ,
										 [SqlParameter(SqlDbType.VarChar,20 ,ParameterDirection.Input)]string Value)
		{
			DataSet ds=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{FieldName,Value});

			
				
				ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "QSPCanadaFinance..GetBankAccount" , aParams);

			}
			catch(InvalidOperationException io)
			{              			
				string x = io.Message;
			}
			catch(SqlException)
			{
				//string ax =e.Message;
			}
			catch( Exception)
			{
			}
			return ds;
		}
	}
}
