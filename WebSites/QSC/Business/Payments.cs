using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	/// <summary>
	/// Summary description for CatalogSection.
	/// </summary>
	public class Payments : QBusinessObject
	{
		#region Class Members
		// Column fields

		protected string PaymentIdM;
		[DAL.DataColumn("Payment_Id")]
		public string PaymentId
		{
			get{ return this.PaymentIdM; }
			set{ this.PaymentIdM=value;}
		}

		protected string BankDepositIdM;
		[DAL.DataColumn("Bank_Deposit_Id")]
		public string BankDepositId
		{
			get{ return this.BankDepositIdM; }
			set{ this.BankDepositIdM=value;}
		}


		
	//	protected int Account_Id;
	//	[DAL.DataColumn("Account_Id")]
	//	public int AccountId
	//	{
	//		get{ return this.Account_Id; }
	//		set{ this.Account_Id = value; }
	//	}


		//protected int Account_Type_Id;
		//[DAL.DataColumn("Account_Type_Id")]
	//	public int AccountTypeId
	//	{
	//		get{ return this.Account_Type_Id; }
	//		set{ this.Account_Type_Id = value; }
	//	}

		protected string Payment_Method_Id;
		[DAL.DataColumn("Payment_Method_Id")]
		public string PaymentMethodId
		{
			get{ return this.Payment_Method_Id; }
			set{ this.Payment_Method_Id = value; }
		}


		//protected DateTime Payment_Effective_Date;
		//[DAL.DataColumn("Payment_Effective_Date")]
	//	public DateTime PaymentEffectiveDate
	//	{
	//		get{ return this.Payment_Effective_Date; }
	//		set{ this.Payment_Effective_Date = value; }
	//	}


		protected string ChequeNumberM;
		[DAL.DataColumn("Cheque_Number")]
		public string ChequeNumber
		{
			get{ return this.ChequeNumberM; }
			set{ this.ChequeNumberM = value; }
		}


		protected string ChequeDateM;
		[DAL.DataColumn("Cheque_Date")]
		public string ChequeDate
		{
			get{ return this.ChequeDateM; }
			set{ this.ChequeDateM = value; }
		}
		protected string ChequeDateToM;
		[DAL.DataColumn("Cheque_Date")]
		public string ChequeDateTo
		{
			get{ return this.ChequeDateToM; }
			set{ this.ChequeDateToM = value; }
		}

	//	protected string Cheque_Payer;
	//	[DAL.DataColumn("Cheque_Payer")]
	//	public string ChequePayer
	//	{
	//		get{ return this.Cheque_Payer; }
	//		set{ this.Cheque_Payer = value; }
	//	}



		//protected string Credit_Card_Owner;
		//[DAL.DataColumn("Credit_Card_Owner")]
	//	public string CreditCardOwner
	//	{
	//		get{ return this.Credit_Card_Owner; }
	//		set{ this.Credit_Card_Owner = value; }
	//	}

	//	protected string Credit_Card_Authorization;
	//	[DAL.DataColumn("Credit_Card_Authorization")]
	//	public string CreditCardAuthorization
	//	{
	//		get{ return this.Credit_Card_Authorization; }
	//		set{ this.Credit_Card_Authorization = value; }
	//	}
		

		protected string PaymentAmountM;
		[DAL.DataColumn("Payment_Amount")]
		public string PaymentAmount
		{
			get{ return this.PaymentAmountM; }
			set{ this.PaymentAmountM = value; }
		}

	//	protected string Note_To_Print;
	//	[DAL.DataColumn("Note_To_Print")]
	//	public string NoteToPrint
	//	{
	//		get{ return this.Note_To_Print; }
	//		set{ this.Note_To_Print = value; }
	//	}

		//protected DateTime Date_Time_Created;
		//[DAL.DataColumn("Date_Time_Created")]
		//public DateTime DateTimeCreated
	//	{
	//		get{ return this.Date_Time_Created; }
	//		set{ this.Date_Time_Created = value; }
	//	}

		//protected DateTime Date_Time_Modified;
		//[DAL.DataColumn("Date_Time_Modified")]
		//public DateTime DateTimeModified
		//{
		//	get{ return this.Date_Time_Modified; }
		//	set{ this.Date_Time_Modified = value; }
		//}

		//protected string Last_Updated_By;
		//[DAL.DataColumn("Last_Updated_By")]
		//public string LastUpdatedBy
		//{
		//	get{ return this.Last_Updated_By; }
		//	set{ this.Last_Updated_By = value; }
		//}

		protected string OrderIdM;
		[DAL.DataColumn("Order_Id")]
		public string OrderId
		{
			get{ return this.OrderIdM; }
			set{ this.OrderIdM = value; }
		}

		//protected string Country_Code;
		//[DAL.DataColumn("Country_Code")]
		//public string CountryCode
		//{
		//	get{ return this.Country_Code; }
		//	set{ this.Country_Code = value; }
		//}

		protected string CampaignIdM;
		[DAL.DataColumn("Campaign_Id")]
		public string CampaignId
		{
			get{ return this.CampaignIdM; }
			set{ this.CampaignIdM = value; }
		}

		

		#endregion

		protected PaymentData aTable;
        public string pSearchFieldType;
		public string pSearchBoxValue;

		public Payments() 
		{
			try
			{
               
				aTable = new PaymentData();

			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}
	
		
		/// <summary>
		/// Insert Payments
		/// </summary>
		override public bool ValidateAndSave()
		{
			bool bOk =true;
			// Do any validation
			if(!Validate(pSearchFieldType, pSearchBoxValue))
			{
				// Always True bOk = false;
			}
		
			return bOk;
		}

		public bool Validate( string pSearchFieldType, string pSearchBoxValue)
		{
			
			bool bValid = true;
            BankDeposit BD = new BankDeposit();
            BD.Validate(pSearchFieldType,pSearchBoxValue); 
			if (!(BD.Validate(pSearchFieldType,pSearchBoxValue)))
			{
             bValid = false;
			}
       
			return bValid;
		}
	

		public DataSet GetPaymentNotDepositedDataSet()
		{
			
			DataSet PaymentNotDepositedDataSet = aTable.Exists(Payment_Method_Id);

			return PaymentNotDepositedDataSet;
		}
		
		
		public DataSet GetPaymentDataSet()
		{
			
			DataSet PaymentDataSet = aTable.Exists(BankDepositIdM,PaymentIdM,ChequeNumberM,PaymentAmountM,OrderIdM,CampaignIdM,ChequeDate,ChequeDateToM);

			return PaymentDataSet;
		}

	}
}