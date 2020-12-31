using System;
using System.Reflection;
using System.EnterpriseServices;
using System.Data;
using System.Data.SqlClient;
using Debug = System.Diagnostics.Debug;

namespace DAL
{
	/// <summary>
	/// BankDepositItemData
	/// </summary>	 

	public class BankDepositItemData : QDataAccess
	{
      
		public BankDepositItemData()
		{
        
		}

		#region CRUD Commands
		/// <summary>
		/// Insert a Bank Deposit Item object
		/// </summary>	 
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaFinance..AddBankDepositItem")]
		public bool Insert( [SqlParameter(SqlDbType.Int ,ParameterDirection.Input)] int BankDepositID,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int PaymentId,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Output)]out int DepositItemID
			)
		{
			DepositItemID=-1;
			try
			{                
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{BankDepositID,PaymentId,DepositItemID});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaFinance..AddBankDepositItem",aParams);

               
				DepositItemID = (int)aParams[2].Value;
			}
			catch(InvalidOperationException )
			{              			
               
			}
			catch( SqlException e)
			{
				Debug.Assert(false, e.Message);
			}
			catch( Exception )
			{
                
			}
			return true;
		}
	


		#endregion

		//[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaFinance..GetBankDepositItem")]
		//public DataSet Exists([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int DepositItemID )
		//{
		//	DataSet ds=null;
		//	try
		//	{
		//		SqlParameter[] aParams = CreateSqlParameters(null,
		//			new object[]{DepositItemID});

				
		//		ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, 	"QSPCanadaFinance..GetBankDepositItem" , aParams);

		//	}
		//	catch(InvalidOperationException io)
		//	{              			
		//		string x = io.Message;
		//	}
		//	catch( SqlException e)
		//	{
		//	}
		//	catch( Exception e)
		//	{
		//	}
		//	return ds;
		//}
		public bool Delete(int ID)
		{
			bool bOk = true;
			return bOk;
		}
	}
}
