using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for RefundCustomer.
	/// </summary>
	public class RefundCustomer:ActionObjectCommon
	{

		private Customer cCustomer;
		private float fRegularPrice = 0;
		private float fRefundAmount =0;
		private string sRefundReason = "";
		private int iIncidentID =0;
		public RefundCustomer(int CustomerOrderHeaderInstance,int TransID,Customer CustomerInfo, float RegularPrice, float RefundAmount,string RefundReason,int IncidentID,string UserID)
		{
			base.iCOHInstance = CustomerOrderHeaderInstance;
			base.iTransID = TransID;
			this.cCustomer = CustomerInfo;
			this.fRegularPrice = RegularPrice;
			this.fRefundAmount = RefundAmount;
			this.sRefundReason = RefundReason;
			this.iIncidentID = IncidentID;
			base.sUserID = UserID;

			
		}
		public Customer CustomerInfo
		{
			get {return cCustomer;}
		}
		public float RegularPrice 
		{
			get {return fRegularPrice;}
		}
		public float RefundAmount
		{
			get {return fRefundAmount;}
		}
		public string RefundReason
		{
			get {return sRefundReason;}
		}
		public int IncidentID
		{
			get {return iIncidentID;}
		}
	}
}
