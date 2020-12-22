using System;

namespace EFundraisingCRMWeb.Components.Server {
	/// <summary>
	/// Summary description for UrlParam.
	/// </summary>
	public class UrlParam {
		// key names
		public const string UrlKeySaleId = "clientId";
		public const string UrlKeySequenceCodeId = "sequenceCodeId";
		public const string UrlKeyPartnerId = "partnerId";
		public const string UrlKeySearchQuery = "q";
		public const string UrlKeyEmailTemplateID = "emailTemplateId";
		public const string UrlKeyLeadId ="leadId";
		public const string UrlKeyKitTypeId ="kitTypeId";
		public const string UrlKeyItemType = "itemType";
		public const string UrlKeyProductClassId = "productClassId";
		public const string UrlKeyOnlineEventID = "onlineEventId";
		public const string UrlKeyPaymentID ="paymentId";

		private int saleId = int.MinValue;
		private string sequenceCodeId = null;
		private int partnerId = int.MinValue;
		private string searchQuery = null;
		private int emailTemplateID = int.MinValue;
		private int leadId = int.MinValue;
		private int kitTypeId = int.MinValue;
		private string itemType = null;
		private int productClassId = int.MinValue;
		private int onlineEventID = int.MinValue;
		private int paymentID = int.MinValue;

		public UrlParam() : this (null) { }
		public UrlParam(System.Web.HttpRequest request) {
			if(request != null) {
				saleId = ToInt(request[UrlKeySaleId]);
				sequenceCodeId = ToString(request[UrlKeySequenceCodeId]);
				partnerId = ToInt(request[UrlKeyPartnerId]);
				searchQuery = ToString(request[UrlKeySearchQuery]);
				emailTemplateID = ToInt(request[UrlKeyEmailTemplateID]);
				leadId = ToInt(request[UrlKeyLeadId]);
				kitTypeId = ToInt(request[UrlKeyKitTypeId]);
				itemType = ToString(request[UrlKeyItemType]);
				productClassId = ToInt(request[UrlKeyProductClassId]);
				onlineEventID = ToInt(request[UrlKeyOnlineEventID]);
				paymentID = ToInt(request[UrlKeyPaymentID]);
			}
		}

		private int ToInt(object o) {
			if(o != null) {
				int i = int.MinValue;
				try {
					i = int.Parse(o.ToString()); 
				} catch { }
				return i;
			} else {
				return int.MinValue;
			}
		}

		private string ToString(object o) {
			if(o != null) {
				return o.ToString();
			} else {
				return null;
			}
		}

		#region Properties
		public int SaleId {
			get { return saleId; }
			set { saleId = value; }
		}

		public string SequenceCodeId {
			get { return sequenceCodeId; }
			set { sequenceCodeId = value; }
		}

		public int PartnerID {
			get { return partnerId; }
			set { partnerId = value; }
		}

		public string SearchQuery {
			get { return searchQuery; }
			set { searchQuery = value; }
		}

		public int EmailTemplateID {
			get { return emailTemplateID; }
			set { emailTemplateID = value; }
		}
		public int LeadID
		{
			get {return leadId;}
			set { leadId = value;}
		}
		public int KitTypeID
		{
			get {return kitTypeId;}
			set {kitTypeId = value;}
		}

		public int ProductClassId 
		{
			get { return productClassId; }
			set { productClassId = value; }
		}

		public string ItemType
		{
			get { return itemType; }
			set { itemType = value; }
		}

		public int OnlineEventID {
			get { return onlineEventID; }
			set { onlineEventID = value; }
		}
		public int PaymentID
		{
			get {return paymentID;}
			set {paymentID = value;}
		}
		#endregion
	}
}
