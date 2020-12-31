using System;
using System.Runtime.Serialization;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for CreditCardTransaction.
	/// </summary>
	///
	[Serializable]
	public class CreditCardTransaction
	{
		private string sTransactionCode = "";
		private int iResultCode;
		private string sAuthCode;
		private int iOrderID;
		private int iPaymentID;
		private string sMessage;


		public CreditCardTransaction(string TransactionCode)
		{
			this.sTransactionCode = TransactionCode;
			DecodeTransactionCode();
		}

		public CreditCardTransaction(int ResultCode, string AuthorizationCode, int OrderID, int PaymentID, string Message)
		{
			this.iResultCode = ResultCode;
			this.sAuthCode = AuthorizationCode;
			this.iOrderID = OrderID;
			this.iPaymentID = PaymentID;
			this.sMessage = Message;
		}

		public string TransactionCode
		{
			get
			{
				return sTransactionCode;
			}
			set
			{
				this.sTransactionCode = value;
			}
		}

		public int ResultCode
		{
			get
			{
				return iResultCode;
			}
			set
			{
				this.iResultCode = value;
			}
		}

		public string AuthorizationCode
		{
			get
			{
				return sAuthCode;
			}
			set
			{
				this.sAuthCode = value;
			}
		}

		public int OrderID
		{
			get
			{
				return iOrderID;
			}
			set
			{
				this.iOrderID = value;
			}
		}

		public int PaymentID
		{
			get
			{
				return iPaymentID;
			}
			set
			{
				this.iPaymentID = value;
			}
		}

		public string Message
		{
			get
			{
				return sMessage;
			}
			set
			{
				this.sMessage = value;
			}
		}

		public void DecodeTransactionCode()
		{
			int cStart = 0;
			int cEnd;
			string[] decode = new string[5];

			if(sTransactionCode != String.Empty)
			{
				for(int i = 0; i <= 3; i++)
				{
					cEnd = sTransactionCode.IndexOf("|", cStart);

					decode[i] = sTransactionCode.Substring(cStart, cEnd - cStart);

					cStart = cEnd + 1;
				}

				decode[4] = sTransactionCode.Substring(cStart);

				if(decode[0] != String.Empty)
				{
					iResultCode = Convert.ToInt32(decode[0]);
				}
				else
				{
					iResultCode = -1;
				}
				sAuthCode = decode[1];
				if(decode[2] != String.Empty)
				{
					iOrderID = Convert.ToInt32(decode[2]);
				}
				else
				{
					iOrderID = 0;
				}
				if(decode[3] != String.Empty)
				{
					iPaymentID = Convert.ToInt32(decode[3]);
				}
				else
				{
					iPaymentID = 0;
				}
				sMessage = decode[4];
			}
		}
	}
}
