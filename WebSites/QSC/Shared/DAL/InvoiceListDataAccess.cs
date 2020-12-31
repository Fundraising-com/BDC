using System;
using System.Reflection;
using System.EnterpriseServices;
using System.Data;
using System.Data.SqlClient;
using Debug = System.Diagnostics.Debug;
using System.Text.RegularExpressions;
using System.Configuration;


namespace DAL
{

	public class InvoiceListDataAccess : QDataAccess
	{
		public InvoiceListDataAccess()
		{}
			
		public string connString = ConfigurationSettings.AppSettings["DSN"];		

		public DataSet GetAllInvoicesByDate(string strFromDate, string strToDate, string AccountName, int AccountID, int OrderID, int InvoiceID, int CampaignID)
		{
            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetAllInvoicesByDate",
                new SqlParameter("@FromDate", strFromDate),
                new SqlParameter("@ToDate", strToDate),
                new SqlParameter("@AccountName", AccountName),
                new SqlParameter("@AccountID", AccountID),
                new SqlParameter("@OrderID", OrderID),
                new SqlParameter("@InvoiceID", InvoiceID),
                new SqlParameter("@CampaignID", CampaignID));
			return ds;
		}

		public DataSet GetPaymentDetails(int nID, int invOrderId)
		{
            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetAllInvoicePaymentDetails", new SqlParameter("@InvoiceId", nID), new SqlParameter("@OrderID", invOrderId));	
			return ds;
		}

        public DataSet GetAllInvoiceAdjustmentDetails(int nID, int invOrderId)
		{
            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetAllInvoiceAdjustmentDetails", new SqlParameter("@InvoiceID", nID), new SqlParameter("@OrderID", invOrderId));
			return ds;
		}

        public DataSet GetAllInvoiceProductDetails(int nID, int invOrderId)
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetAllInvoiceProductDetails", new SqlParameter("@InvoiceID", nID), new SqlParameter("@OrderID", invOrderId));
			return ds;
		}

		public int AddInvoiceAdjustment(int nActID, int nOrderID, string zInternalComment, decimal dAmount, int nCampaignID, int nAdjustmentType, /*int nAdjustmentAccountType,*/ int nChangedBy)
		{
			SqlParameter [] Parms	= new SqlParameter[8];
			Parms[0]			= new SqlParameter("@AccountID", SqlDbType.Int);
			Parms[0].Value		= nActID;
			
			Parms[1]			= new SqlParameter("@OrderID", SqlDbType.Int);
			Parms[1].Value		= nOrderID;

			Parms[2]			= new SqlParameter("@InternalComment", SqlDbType.VarChar);
			Parms[2].Value		= zInternalComment;

			Parms[3]			= new SqlParameter("@Amount", SqlDbType.Decimal);
			Parms[3].Value		= dAmount;

			Parms[4]			= new SqlParameter("@CampaignID", SqlDbType.Int);
			Parms[4].Value		= nCampaignID;

			Parms[5]			= new SqlParameter("@AdjustmentType", SqlDbType.Int);
			Parms[5].Value		= nAdjustmentType;
			
			//Parms[6]			= new SqlParameter("@AdjustmentAccountType", SqlDbType.Int);
			//Parms[6].Value		= nAdjustmentAccountType;

			Parms[6]			= new SqlParameter("@ChangedBy", SqlDbType.Int);
			Parms[6].Value		= nChangedBy;

			Parms[7]			= new SqlParameter("@Value", SqlDbType.Int);
			Parms[7].Direction  = ParameterDirection.Output;

			int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..AddInvoiceAdjustment",Parms);
			return (int)Parms[7].Value;


			/*
				int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..AddInvoiceAdjustment", 
				new SqlParameter("@AccountID", nActID),
				new SqlParameter("@OrderID", nOrderID),
				new SqlParameter("@InternalComment", zInternalComment),
				new SqlParameter("@Amount", dAmount),
				new SqlParameter("@CampaignID", nCampaignID),
				new SqlParameter("@AdjustmentType", nAdjustmentType),
				new SqlParameter("@AdjustmentAccountType", nAdjustmentAccountType),
				new SqlParameter("@ChangedBy", nChangedBy),
				new SqlParameter("@Value", nChangedBy));	
			return x;
			*/
		}

		
		public int AddInvoicePayment(int nActID, int nOrderID, int nCampaignID, 
			/*int nPaymentAccountType,*/ int nPaymentMethod, 
			string zCheckNumber, DateTime dtCheckDate, 
			string zCheckPayer, string zCreditCardOwner, string zCreditCardAuthNumber,
			decimal dAmount, int nChangedBy)
		{
			SqlParameter [] Parms	= new SqlParameter[12];
			Parms[0]		= new SqlParameter("@AccountID", SqlDbType.Int);
			Parms[0].Value	= nActID;
			
			Parms[1]		= new SqlParameter("@OrderID", SqlDbType.Int);
			Parms[1].Value	= nOrderID;

			Parms[2]		= new SqlParameter("@CampaignID", SqlDbType.Int);
			Parms[2].Value	= nCampaignID;

			//Parms[3]		= new SqlParameter("@PaymentAccountType", SqlDbType.Int);
			//Parms[3].Value	= nPaymentAccountType;

			Parms[3]		= new SqlParameter("@PaymentMethod", SqlDbType.Int);
			Parms[3].Value	= nPaymentMethod;

			Parms[4]		= new SqlParameter("@CheckNumber", SqlDbType.VarChar);
			Parms[4].Value	= zCheckNumber;
			
			Parms[5]		= new SqlParameter("@CheckDate", SqlDbType.DateTime);
			Parms[5].Value	= dtCheckDate;

			Parms[6]		= new SqlParameter("@CheckPayer",SqlDbType.VarChar);
			Parms[6].Value	= zCheckPayer;

			Parms[7]		= new SqlParameter("@CreditCardOwner",SqlDbType.VarChar);
			Parms[7].Value	= zCreditCardOwner;

			Parms[8]		= new SqlParameter("@CreditCardAuthNumber",SqlDbType.VarChar);
			Parms[8].Value	= zCreditCardAuthNumber;

			Parms[9]		= new SqlParameter("@Amount",SqlDbType.Decimal);
			Parms[9].Value	= dAmount;

			Parms[10]		= new SqlParameter("@ChangedBy", SqlDbType.Int);
			Parms[10].Value	= nChangedBy;

			Parms[11]				= new SqlParameter("@Value", SqlDbType.Int);
			Parms[11].Direction 	= ParameterDirection.Output;

			int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..AddInvoicePayment",Parms);
			return (int)Parms[11].Value;

			/*
			   int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..AddInvoicePayment", 
				new SqlParameter("@AccountID", nActID),
				new SqlParameter("@OrderID", nOrderID),
				new SqlParameter("@CampaignID", nCampaignID),
				new SqlParameter("@PaymentAccountType", nPaymentAccountType),
				new SqlParameter("@PaymentMethod", nPaymentMethod),
				new SqlParameter("@CheckNumber", nCheckNumber),
				new SqlParameter("@CheckDate", dtCheckDate),	
				new SqlParameter("@CheckPayer", zCheckPayer),
				new SqlParameter("@CreditCardOwner", zCreditCardOwner),
				new SqlParameter("@CreditCardAuthNumber", zCreditCardAuthNumber),
				new SqlParameter("@Amount", dAmount),
				new SqlParameter("@ChangedBy", nChangedBy));	
			return x;
			*/
		}


		//public int AddInvoiceAllocation(int nID, int nAllocationID, string zSourceType, decimal dAmount)
		//{
		/*  Not using allocations.  Use invoice amount, payments and adjustments.
			 * 
			int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..AddInvoiceAllocation", 
				new SqlParameter("@InvoiceID", nID),
				new SqlParameter("@AllocationID", nAllocationID),
				new SqlParameter("@SourceType", zSourceType),
				new SqlParameter("@Amount", dAmount));			
			return x;
			*/
		//}

		public DateTime GetFiscalStartAndEndDates(DateTime dtStartDate, DateTime dtEndDate)
		{
			SqlParameter [] dateParms	= new SqlParameter[2];
			dateParms[0]				= new SqlParameter("@StartDate", SqlDbType.DateTime);
			dateParms[0].Value			= dtStartDate;
			dateParms[0].Direction		= ParameterDirection.Output;
			dateParms[1]				= new SqlParameter("@EndDate", SqlDbType.DateTime);
			dateParms[1].Value			= dtEndDate;
			dateParms[1].Direction		= ParameterDirection.Output;

			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaCommon..GetCurrentFiscalStartAndEnd", dateParms);
			return (DateTime)dateParms[0].Value;
		}

		public DataSet GetInvoiceAdjustmentTypes()
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetInvoiceAdjustmentTypes");
			return ds;
		}

		public DataSet GetInvoiceAdjustmentStatus()
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetInvoiceAdjustmentStatus");
			return ds;
		}

		public DataSet GetInvoicePaymentMethods()
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetInvoicePaymentMethods");
			return ds;
		}

		public DataSet GetInvoiceAccountTypes()
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetAccountTypes");
			return ds;
		}

		public DataSet GetGLEntriesByAdjustment(int nInvoiceID, int nAdjustmentID)
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetGLEntriesByAdjustment",
				new SqlParameter("@InvoiceID", nInvoiceID),
				new SqlParameter("@AdjustmentID", nAdjustmentID));	
			return ds;
		}

		public DataSet GetGLEntriesByPayment(int nInvoiceID, int nPaymentID)
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetGLEntriesByPayment",
				new SqlParameter("@InvoiceID", nInvoiceID),
				new SqlParameter("@PaymentID", nPaymentID));	
			return ds;
		}

		public DataSet GetGLAccounts()
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetGLAccountNumbers");
			return ds;
		}

        public string GetGLAccountNumber(int GLAccountID)
        {
            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetGLAccountNumbers");

            DataView dv = new DataView(ds.Tables[0], "GLAccountID = " + GLAccountID.ToString(), "", DataViewRowState.CurrentRows);

            return dv[0]["GLAccountNumber"].ToString();
        }

		public int AddGLEntriesForAdjustment(int nInvoiceID, int nAdjustmentID, string zDescription, string zGlAcctNumber,
			string zDebitCredit, decimal dAmount, int nChangedBy)
		{
			int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..AddGLEntriesForAdjustment", 
				new SqlParameter("@InvoiceID", nInvoiceID),
				new SqlParameter("@AdjustmentID", nAdjustmentID),
				new SqlParameter("@Description", zDescription),
				new SqlParameter("@GlAcctNumber", zGlAcctNumber),
				new SqlParameter("@DebitCredit", zDebitCredit),
				new SqlParameter("@Amount", dAmount),
				new SqlParameter("@ChangedBy", nChangedBy));	
			return x;
		}
		public int AddGLEntriesForPayment(int nInvoiceID, int nPaymentID, string zDescription, string zGlAcctNumber,
			string zDebitCredit, decimal dAmount, int nChangedBy)
		{
			int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..AddGLEntriesForPayment", 
				new SqlParameter("@InvoiceID", nInvoiceID),
				new SqlParameter("@PaymentID", nPaymentID),
				new SqlParameter("@Description", zDescription),
				new SqlParameter("@GlAcctNumber", zGlAcctNumber),
				new SqlParameter("@DebitCredit", zDebitCredit),
				new SqlParameter("@Amount", dAmount),
				new SqlParameter("@ChangedBy", nChangedBy));	
			return x;
		}
		public int GetGLEntryAdjustmentBalance(int nAdjustmentID)
		{
			SqlParameter [] Parms	= new SqlParameter[2];
			Parms[0]		= new SqlParameter("@AdjustmentID", SqlDbType.Int);
			Parms[0].Value	= nAdjustmentID;

			Parms[1]			= new SqlParameter("@Value", SqlDbType.Int);
			Parms[1].Direction 	= ParameterDirection.Output;

			int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetGLEntryAdjustmentBalance",Parms);
			return (int)Parms[1].Value;
		}

		public int AddGLTransactionForAdjustment(int nGLEntryID, int GLAccountID, string zDebitCredit, decimal dAmount)
		{
			int x =  SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "QSPCanadaFinance..AddGLTransactionForAdjustment", 
				new SqlParameter("@GLEntryID", nGLEntryID),
				new SqlParameter("@GLAccountID", GLAccountID),
				new SqlParameter("@DebitCredit", zDebitCredit),
				new SqlParameter("@Amount", dAmount));	
			return x;
		}

		public DataSet GetAllOrdersNotInvoiced(string strFromDate, string strToDate)
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetAllOrdersNotInvoiced", 
				new SqlParameter("@FromDate", strFromDate),
				new SqlParameter("@ToDate", strToDate));
			return ds;
		}

		public DataSet GetAllOrdersNotInvoicedAdjustmentDetails(int nOrderID)
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetAllOrdersNotInvoicedAdjustmentDetails", new SqlParameter("@OrderID", nOrderID));	
			return ds;
		}

		public DataSet GetAllOrdersNotInvoicedPaymentDetails(int nOrderID)
		{
			DataSet ds =  SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "QSPCanadaFinance..GetAllOrdersNotInvoicedPaymentDetails", new SqlParameter("@OrderID", nOrderID));	
			return ds;
		}
	
	}
		
}
