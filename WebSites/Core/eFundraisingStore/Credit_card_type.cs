using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CreditCardType: eFundraisingStoreDataObject {

		private short creditCardTypeId;
		private short paymentMethodId;
		private string creditCardTypeName;
		private string creditCardImage;
		private short displayOrder;
		private short displayable;


		public CreditCardType() : this(short.MinValue) { }
		public CreditCardType(short creditCardTypeId) : this(creditCardTypeId, short.MinValue) { }
		public CreditCardType(short creditCardTypeId, short paymentMethodId) : this(creditCardTypeId, paymentMethodId, null) { }
		public CreditCardType(short creditCardTypeId, short paymentMethodId, string creditCardTypeName) : this(creditCardTypeId, paymentMethodId, creditCardTypeName, null) { }
		public CreditCardType(short creditCardTypeId, short paymentMethodId, string creditCardTypeName, string creditCardImage) : this(creditCardTypeId, paymentMethodId, creditCardTypeName, creditCardImage, short.MinValue) { }
		public CreditCardType(short creditCardTypeId, short paymentMethodId, string creditCardTypeName, string creditCardImage, short displayOrder) : this(creditCardTypeId, paymentMethodId, creditCardTypeName, creditCardImage, displayOrder, short.MinValue) { }
		public CreditCardType(short creditCardTypeId, short paymentMethodId, string creditCardTypeName, string creditCardImage, short displayOrder, short displayable) {
			this.creditCardTypeId = creditCardTypeId;
			this.paymentMethodId = paymentMethodId;
			this.creditCardTypeName = creditCardTypeName;
			this.creditCardImage = creditCardImage;
			this.displayOrder = displayOrder;
			this.displayable = displayable;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CreditCardType>\r\n" +
			"	<CreditCardTypeId>" + creditCardTypeId + "</CreditCardTypeId>\r\n" +
			"	<PaymentMethodId>" + paymentMethodId + "</PaymentMethodId>\r\n" +
			"	<CreditCardTypeName>" + System.Web.HttpUtility.HtmlEncode(creditCardTypeName) + "</CreditCardTypeName>\r\n" +
			"	<CreditCardImage>" + System.Web.HttpUtility.HtmlEncode(creditCardImage) + "</CreditCardImage>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"	<Displayable>" + displayable + "</Displayable>\r\n" +
			"</CreditCardType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "creditCardTypeId") {
					SetXmlValue(ref creditCardTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentMethodId") {
					SetXmlValue(ref paymentMethodId, node.InnerText);
				}
				if(node.Name.ToLower() == "creditCardTypeName") {
					SetXmlValue(ref creditCardTypeName, node.InnerText);
				}
				if(node.Name.ToLower() == "creditCardImage") {
					SetXmlValue(ref creditCardImage, node.InnerText);
				}
				if(node.Name.ToLower() == "displayOrder") {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
				if(node.Name.ToLower() == "displayable") {
					SetXmlValue(ref displayable, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CreditCardType[] GetCreditCardTypes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCreditCardTypes();
		}

		public static CreditCardType GetCreditCardTypeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCreditCardTypeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCreditCardType(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCreditCardType(this);
		}
		#endregion

		#region Properties
		public short CreditCardTypeId {
			set { creditCardTypeId = value; }
			get { return creditCardTypeId; }
		}

		public short PaymentMethodId {
			set { paymentMethodId = value; }
			get { return paymentMethodId; }
		}

		public string CreditCardTypeName {
			set { creditCardTypeName = value; }
			get { return creditCardTypeName; }
		}

		public string CreditCardImage {
			set { creditCardImage = value; }
			get { return creditCardImage; }
		}

		public short DisplayOrder {
			set { displayOrder = value; }
			get { return displayOrder; }
		}

		public short Displayable {
			set { displayable = value; }
			get { return displayable; }
		}

		#endregion
	}
}
