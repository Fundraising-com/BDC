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
        private DateTime requestDate;
        private double refundAmount;


        public CreditCardRefundRequest() : this(int.MinValue, int.MinValue, int.MinValue, DateTime.MinValue, "", double.MinValue) { }
        
        public CreditCardRefundRequest(int creditCardRefundRequestID, int salesID, int bppsID, DateTime requestDate, string statusCode, double refundAmount) 
		{
            this.creditCardRefundRequestID = creditCardRefundRequestID;
			this.SaleID = salesID;
			this.BppsID = bppsID;
            this.StatusCode = statusCode;
            this.RequestDate = requestDate;
			this.RefundAmount = refundAmount;

		}


        public static CreditCardRefundRequest[] GetCreditCardRefundRequestLastDays(int days)
        {
            DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
            return db.GetCreditCardRefundRequestLastDays(days);
        }

        public static CreditCardRefundRequest GetCreditCardRefundRequestByID(int id)
        {
            DataAccess.EFundraisingCRMDatabase db = new DataAccess.EFundraisingCRMDatabase();
            return db.GetCreditCardRefundRequestByID(id);
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
    }
}
