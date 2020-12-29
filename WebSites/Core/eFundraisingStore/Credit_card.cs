using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CreditCard: eFundraisingStoreDataObject {

		private int creditCardId;
		private int onlineUserId;
		private short creditCardTypeId;
		private string creditCard;
		private string lastDigits;
		private short yearExpire;
		private short monthExpire;
		private DateTime dateCreated;
		private short removed;


		public CreditCard() : this(int.MinValue) { }
		public CreditCard(int creditCardId) : this(creditCardId, int.MinValue) { }
		public CreditCard(int creditCardId, int onlineUserId) : this(creditCardId, onlineUserId, short.MinValue) { }
		public CreditCard(int creditCardId, int onlineUserId, short creditCardTypeId) : this(creditCardId, onlineUserId, creditCardTypeId, null) { }
		public CreditCard(int creditCardId, int onlineUserId, short creditCardTypeId, string creditCard) : this(creditCardId, onlineUserId, creditCardTypeId, creditCard, null) { }
		public CreditCard(int creditCardId, int onlineUserId, short creditCardTypeId, string creditCard, string lastDigits) : this(creditCardId, onlineUserId, creditCardTypeId, creditCard, lastDigits, short.MinValue) { }
		public CreditCard(int creditCardId, int onlineUserId, short creditCardTypeId, string creditCard, string lastDigits, short yearExpire) : this(creditCardId, onlineUserId, creditCardTypeId, creditCard, lastDigits, yearExpire, short.MinValue) { }
		public CreditCard(int creditCardId, int onlineUserId, short creditCardTypeId, string creditCard, string lastDigits, short yearExpire, short monthExpire) : this(creditCardId, onlineUserId, creditCardTypeId, creditCard, lastDigits, yearExpire, monthExpire, DateTime.MinValue) { }
		public CreditCard(int creditCardId, int onlineUserId, short creditCardTypeId, string creditCard, string lastDigits, short yearExpire, short monthExpire, DateTime dateCreated) : this(creditCardId, onlineUserId, creditCardTypeId, creditCard, lastDigits, yearExpire, monthExpire, dateCreated, short.MinValue) { }
		public CreditCard(int creditCardId, int onlineUserId, short creditCardTypeId, string creditCard, string lastDigits, short yearExpire, short monthExpire, DateTime dateCreated, short removed) {
			this.creditCardId = creditCardId;
			this.onlineUserId = onlineUserId;
			this.creditCardTypeId = creditCardTypeId;
			this.creditCard = creditCard;
			this.lastDigits = lastDigits;
			this.yearExpire = yearExpire;
			this.monthExpire = monthExpire;
			this.dateCreated = dateCreated;
			this.removed = removed;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CreditCard>\r\n" +
			"	<CreditCardId>" + creditCardId + "</CreditCardId>\r\n" +
			"	<OnlineUserId>" + onlineUserId + "</OnlineUserId>\r\n" +
			"	<CreditCardTypeId>" + creditCardTypeId + "</CreditCardTypeId>\r\n" +
			"	<CreditCard>" + creditCard + "</CreditCard>\r\n" +
			"	<LastDigits>" + System.Web.HttpUtility.HtmlEncode(lastDigits) + "</LastDigits>\r\n" +
			"	<YearExpire>" + yearExpire + "</YearExpire>\r\n" +
			"	<MonthExpire>" + monthExpire + "</MonthExpire>\r\n" +
			"	<DateCreated>" + dateCreated + "</DateCreated>\r\n" +
			"	<Removed>" + removed + "</Removed>\r\n" +
			"</CreditCard>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "creditCardId") {
					SetXmlValue(ref creditCardId, node.InnerText);
				}
				if(node.Name.ToLower() == "onlineUserId") {
					SetXmlValue(ref onlineUserId, node.InnerText);
				}
				if(node.Name.ToLower() == "creditCardTypeId") {
					SetXmlValue(ref creditCardTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "creditCard") {
					SetXmlValue(ref creditCard, node.InnerText);
				}
				if(node.Name.ToLower() == "lastDigits") {
					SetXmlValue(ref lastDigits, node.InnerText);
				}
				if(node.Name.ToLower() == "yearExpire") {
					SetXmlValue(ref yearExpire, node.InnerText);
				}
				if(node.Name.ToLower() == "monthExpire") {
					SetXmlValue(ref monthExpire, node.InnerText);
				}
				if(node.Name.ToLower() == "dateCreated") {
					SetXmlValue(ref dateCreated, node.InnerText);
				}
				if(node.Name.ToLower() == "removed") {
					SetXmlValue(ref removed, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CreditCard[] GetCreditCards() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCreditCards();
		}

		public static CreditCard GetCreditCardByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCreditCardByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCreditCard(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCreditCard(this);
		}
		#endregion

		#region Properties
		public int CreditCardId {
			set { creditCardId = value; }
			get { return creditCardId; }
		}

		public int OnlineUserId {
			set { onlineUserId = value; }
			get { return onlineUserId; }
		}

		public short CreditCardTypeId {
			set { creditCardTypeId = value; }
			get { return creditCardTypeId; }
		}

		public string CreditCardNumber {
			set { creditCard = value; }
			get { return creditCard; }
		}

		public string LastDigits {
			set { lastDigits = value; }
			get { return lastDigits; }
		}

		public short YearExpire {
			set { yearExpire = value; }
			get { return yearExpire; }
		}

		public short MonthExpire {
			set { monthExpire = value; }
			get { return monthExpire; }
		}

		public DateTime DateCreated {
			set { dateCreated = value; }
			get { return dateCreated; }
		}

		public short Removed {
			set { removed = value; }
			get { return removed; }
		}

		#endregion
	}
}
