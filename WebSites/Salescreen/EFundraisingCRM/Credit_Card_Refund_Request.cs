using System;
using System.Collections.Generic;
using System.Text;
using efundraising.Core;
using efundraising.Data.Sql;

namespace efundraising.EFundraisingCRM
{
	public class CreditCardRefundRequest : EFundraisingCRMDataObject
	{
		private int creditCardRefundRequestID;
		private int saleID;
		private int bppsID;
		private string statusCode;
		private string statusDescription;
		private DateTime requestDate;
		private double refundAmount;
		private Boolean processed;
		private byte creditCardTypeId;
		private string creditCardTypeName;
		private bool cancelled;

		public CreditCardRefundRequest() : this(int.MinValue, int.MinValue, int.MinValue, DateTime.MinValue, string.Empty, string.Empty, double.MinValue, byte.MinValue, string.Empty, true) { }

		public CreditCardRefundRequest(int creditCardRefundRequestID, int salesID, int bppsID, DateTime requestDate, string statusCode, string statusDescription, double refundAmount, byte creditCardTypeId, string creditCardTypeName, bool cancelled)
		{
			this.creditCardRefundRequestID = creditCardRefundRequestID;
			this.SaleID = salesID;
			this.BppsID = bppsID;
			this.StatusCode = statusCode;
			this.statusDescription = statusDescription;
			this.RequestDate = requestDate;
			this.RefundAmount = refundAmount;
			this.CreditCardTypeId = creditCardTypeId;
			this.CreditCardTypeName = creditCardTypeName;
			this.cancelled = cancelled;
		}

		public static CreditCardRefundRequest[] GetCreditCardRefundRequestLastDays(int days, bool cancelled)
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return db.GetCreditCardRefundRequestLastDays(days, cancelled);
		}

		public static CreditCardRefundRequest GetCreditCardRefundRequestByID(int id)
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return db.GetCreditCardRefundRequestByID(id);
		}

		public static CreditCardRefundRequest[] GetCreditCardRefundRequestByRequestDate(DateTime fromDate, DateTime toDate, bool cancelled)
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return db.GetCreditCardRefundRequestByRequestDate(fromDate, toDate, cancelled);
		}

		public static CreditCardRefundRequest[] GetCreditCardRefundRequestUnapproved()
		{
			DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
			return db.GetCreditCardRefundRequestUnapproved();
		}

		public int Insert()
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCreditCardRefundRequest(this);
		}

		public int Update()
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCreditCardRefundRequest(this);
		}

		public int CreditCardRefundRequestID
		{
			set { creditCardRefundRequestID = value; }
			get { return creditCardRefundRequestID; }
		}

		public int SaleID
		{
			set { saleID = value; }
			get { return saleID; }
		}

		public int BppsID
		{
			set { bppsID = value; }
			get { return bppsID; }
		}

		public string StatusCode
		{
			set { statusCode = value; }
			get { return statusCode; }
		}

		public string StatusDescription
		{
			set { statusDescription = value; }
			get { return statusDescription; }
		}

		public DateTime RequestDate
		{
			set { requestDate = value; }
			get { return requestDate; }
		}
		public double RefundAmount
		{
			set { refundAmount = value; }
			get { return refundAmount; }
		}

		public Boolean Processed
		{
			set { processed = value; }
			get { return processed; }
		}

		public byte CreditCardTypeId
		{
			set { creditCardTypeId = value; }
			get { return creditCardTypeId; }
		}

		public string CreditCardTypeName
		{
			set { creditCardTypeName = value; }
			get { return creditCardTypeName; }
		}

		public bool Cancelled
		{
			set { cancelled = value; }
			get { return cancelled; }
		}
	}
}