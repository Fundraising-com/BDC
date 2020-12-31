using System;
using QSPFulfillment.DataAccess.Common.ActionObject;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for CurrentInfoSession.
	/// </summary>
	[Serializable]
	public class CurrentOrderInfo
	{
		private int iOrderID = 0;
		private int iOrderItemID = 0;
		private int iShipmentID = 0;
		private int iTransID = 0;
		private int iCustomerOrderHeaderInstance = 0;
		private int iCampaignID = 0;
		private System.Collections.ArrayList alTransID;
		private string sStatus = "";
		private string sQualifierName = "";
		private int iAccoutID = 0;
		private string sAccountName = String.Empty;

		public CurrentOrderInfo() {	}

		public int OrderID
		{
			get{return iOrderID;}
			set{iOrderID=value;}
		}
		
		public int OrderItemID
		{
			get{return iOrderItemID;}
			set{iOrderItemID=value;}
		}
		public int TransID
		{
			get{return iTransID;}
			set{iTransID=value;}
		}
		public System.Collections.ArrayList ListTransID
		{
			get{return alTransID;}
			set{alTransID = value;}
		}
		public int CampaignID
		{
			get{return iCampaignID;}
			set{iCampaignID = value;}
		}
		
		public int CustomerOrderHeaderInstance
		{
			get{return iCustomerOrderHeaderInstance;}
			set{iCustomerOrderHeaderInstance=value;}
		}
		public string Status
		{
			get{return this.sStatus;}
			set{sStatus = value;}
		}
		public string QualifierName
		 {
			 get{return this.sQualifierName;}
			 set{sQualifierName = value;}
		 }
		public int ShipmentID
		{
			get{return this.iShipmentID;}
				set{iShipmentID = value;}
		}
		public int AccountID 
		{
			get{return this.iAccoutID;}
			set{iAccoutID = value;}
		}
	
		public string AccountName 
		{
			get 
			{
				return this.sAccountName;
			}
			set 
			{
				this.sAccountName = value;
			}
		}
	}
}