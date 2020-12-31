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

	public class PaymentData : QDataAccess
	{
		      
		
		#region CRUD Commands
		/// <summary>
		/// 
		/// </summary>	 
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QspCanadaFinance..GetAllPaymentsNotDeposited")]
				public DataSet Exists([SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string @PaymentMethodId)
		{
			DataSet ds=null;
			try
			{                
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{@PaymentMethodId});

			    ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "QSPCanadaFinance..GetAllPaymentsNotDeposited",aParams);}
              
		
			catch(InvalidOperationException)
			{              			
             
			}
			catch( SqlException e)
			{
				Debug.Assert(false, e.Message);
			}
			catch( Exception e)
			{
               string Message =  e.Message;
			}
		return ds;
	}
//		}
		
		#endregion

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QspCanadaFinance..GetAllDepositPaymentDetails")]
		public DataSet Exists([SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string BankDepositId,
			[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string PaymentId  ,
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)] string ChequeNumber ,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)] string PaymentAmount,
			[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string OrderId,
			[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string CampaignId,
			[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string ChequeDateFrom,
			[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)] string ChequeDateTo
			)
	
		{
			DataSet ds=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{BankDepositId,PaymentId,ChequeNumber,PaymentAmount,OrderId,CampaignId,ChequeDateFrom,ChequeDateTo});

				
				ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, 	"QspCanadaFinance..GetAllDepositPaymentDetails" , aParams);

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
		
		}
		}
