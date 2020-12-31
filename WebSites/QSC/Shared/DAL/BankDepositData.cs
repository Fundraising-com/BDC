using System;
using System.Reflection;
using System.EnterpriseServices;
using System.Data;
using System.Data.SqlClient;
using Debug = System.Diagnostics.Debug;

namespace DAL
{
	/// <summary>
	/// BankDepositData
	/// </summary>	 

	public class BankDepositData : QDataAccess
	{
      
		public BankDepositData()
		{
        
		}

		#region CRUD Commands
		/// <summary>
		/// Insert a Bank Deposit object
		/// </summary>	 
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QspCanadaFinance..AddBankDeposit")]
		public bool Insert( [SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string DepositDate,
							[SqlParameter(SqlDbType.VarChar,5,ParameterDirection.Input)]string ItemCount,
							[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string DepositAmount,
							[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string DepositStatusID,
							[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string DepositAccountId,
							[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Output)]out string BankDepositID
						  )
		{
			BankDepositID= Convert.ToString("-1");
			try
			{                
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{DepositDate,ItemCount,DepositAmount,DepositStatusID,DepositAccountId,BankDepositID});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaFinance..AddBankDeposit",aParams);

               
				BankDepositID = (string) aParams[5].Value;
			}
			catch(InvalidOperationException)
			{              			
               
			}
			catch( SqlException e)
			{
				Debug.Assert(false, e.Message);
			}
			catch( Exception)
			{
                
			}
			return true;
		}


		#endregion

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QspCanadaFinance..GetBankDeposit")]
		public DataSet Exists([SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)]string BankDepositId ,
							  [SqlParameter(SqlDbType.VarChar,10 ,ParameterDirection.Input)]string DepositStatusId,
							  [SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string DepositDateFrom ,
				              [SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string DepositDateTo,
			                  [SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string DepositAmount,
                              [SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string DepositAccountNo,
                              [SqlParameter(SqlDbType.VarChar,5,ParameterDirection.Input)] string ItemDeposited
				           	 )
		{
			DataSet ds=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{BankDepositId,DepositStatusId,DepositDateFrom,DepositDateTo,DepositAmount,DepositAccountNo,ItemDeposited});

				
				ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, 	"QspCanadaFinance..GetBankDeposit" , aParams);

			}
			catch(InvalidOperationException io)
			{              			
				string x = io.Message;
			}
			catch( SqlException e)
			{
				string x = e.Message;
			}
			catch( Exception e)
			{
				string x = e.Message;
			}
			return ds;
		}



		public bool Delete(int ID)
		{
			bool bOk = true;
			return bOk;
		}
	}
}
