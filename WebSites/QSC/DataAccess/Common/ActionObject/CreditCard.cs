using System;
using System.Runtime.Serialization;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for CreditCardInformation.
	/// </summary>
	/// 
	[Serializable]
	public class CreditCard
	{
		private PaymentMethod iPaymenthMethodID;
		private string sCardHolderName;
		private string sCreditCardNumber;
		private string sExpirationMonth;
		private string sExpirationYear;
		private string sAuthorizationCode;
		private string sReturnCode;
		private int iCustomerPaymentHeaderInstance;
		private string sUserID;
      private decimal dAmount;

		public CreditCard(PaymentMethod PaymentMethodID,string CardHolderName,string CreditCardNumber,string ExpirationMonth,string ExpirationYear,string AuthorizationCode,decimal Amount)
		{
			this.iPaymenthMethodID = PaymentMethodID;
			this.sCardHolderName = CardHolderName;
			this.sCreditCardNumber = CreditCardNumber;
			this.sExpirationMonth = ExpirationMonth;
			this.sExpirationYear = ExpirationYear;
			this.sAuthorizationCode = AuthorizationCode;
         this.dAmount = Amount;
		}
		public CreditCard(PaymentMethod PaymentMethodID)
		{
			this.iPaymenthMethodID = PaymentMethodID;
			
		}
		
		public string UserID
		{
			get{return sUserID;}
			set{sUserID = value;}
			
		}
		public PaymentMethod PaymentMethodID
		{
			get
			{
				return this.iPaymenthMethodID;
			}
			set{iPaymenthMethodID = value;}
		}
		public string CardHolderName
		{
			get
			{
				return sCardHolderName;
			}
			set{sCardHolderName = value;}
		}
		public string CreditCardNumber
		{
			get
			{
				return sCreditCardNumber;
			}
			set{sCreditCardNumber = value;}
		}
		public string ExpirationMonth
		{
			get
			{
				return  sExpirationMonth;
			}
			set{sExpirationMonth = value;}
		}
		public string ExpirationYear
		{
			get
			{
				return  sExpirationYear;
			}
			set{sExpirationYear = value;}
		}
		public string AuthorizationCode
		{
			get
			{
				return sAuthorizationCode;
			}
			set{sAuthorizationCode = value;}
		}
		public string ReturnCode
		{
			get
			{
				return  sReturnCode;
			}
			set{sReturnCode = value;}
		}
		public int CustomerPaymentHeaderInstance
		{
			get
			{
				return  iCustomerPaymentHeaderInstance;
			}
			set{iCustomerPaymentHeaderInstance = value;}
		}
		public bool AsExpirationDate
		{
			get
			{
				return (sExpirationMonth != "");
			}
		}
		public string SafeOutPutCreditCardNumber
		{
			get
			{
                string safeOutPutCreditCardNumber = "********";
                if (this.CreditCardNumber.Length > 8)
                {
                    safeOutPutCreditCardNumber = this.CreditCardNumber.Substring(0, 4) + "********" + this.CreditCardNumber.Substring(this.CreditCardNumber.Length - 4, 4);
                }

                return safeOutPutCreditCardNumber;
			}
		}
		public bool IsCreditCardPayment
		{
			get
			{
				return (this.PaymentMethodID == PaymentMethod.MasterCard ||this.PaymentMethodID == PaymentMethod.Visa || this.PaymentMethodID == PaymentMethod.Error || this.PaymentMethodID == PaymentMethod.PayPal);
			}
		}

      public decimal Amount
      {
         get
         {
            return dAmount;
         }
         set { dAmount = value; }
      }

   }
}
