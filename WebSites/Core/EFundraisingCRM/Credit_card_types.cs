using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CreditCardTypes: EFundraisingCRMDataObject {

		private short creditCardTypeId;
		private short paymentMethodId;
		private string creditCardTypeName;
		private string creditCardImage;
		private short displayOrder;
		private int displayable;


		public CreditCardTypes() : this(short.MinValue) { }
		public CreditCardTypes(short creditCardTypeId) : this(creditCardTypeId, short.MinValue) { }
		public CreditCardTypes(short creditCardTypeId, short paymentMethodId) : this(creditCardTypeId, paymentMethodId, null) { }
		public CreditCardTypes(short creditCardTypeId, short paymentMethodId, string creditCardTypeName) : this(creditCardTypeId, paymentMethodId, creditCardTypeName, null) { }
		public CreditCardTypes(short creditCardTypeId, short paymentMethodId, string creditCardTypeName, string creditCardImage) : this(creditCardTypeId, paymentMethodId, creditCardTypeName, creditCardImage, short.MinValue) { }
		public CreditCardTypes(short creditCardTypeId, short paymentMethodId, string creditCardTypeName, string creditCardImage, short displayOrder) : this(creditCardTypeId, paymentMethodId, creditCardTypeName, creditCardImage, displayOrder, int.MinValue) { }
		public CreditCardTypes(short creditCardTypeId, short paymentMethodId, string creditCardTypeName, string creditCardImage, short displayOrder, int displayable) {
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
			return "<CreditCardTypes>\r\n" +
			"	<CreditCardTypeId>" + creditCardTypeId + "</CreditCardTypeId>\r\n" +
			"	<PaymentMethodId>" + paymentMethodId + "</PaymentMethodId>\r\n" +
			"	<CreditCardTypeName>" + System.Web.HttpUtility.HtmlEncode(creditCardTypeName) + "</CreditCardTypeName>\r\n" +
			"	<CreditCardImage>" + System.Web.HttpUtility.HtmlEncode(creditCardImage) + "</CreditCardImage>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"	<Displayable>" + displayable + "</Displayable>\r\n" +
			"</CreditCardTypes>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("creditCardTypeId")) {
					SetXmlValue(ref creditCardTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentMethodId")) {
					SetXmlValue(ref paymentMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("creditCardTypeName")) {
					SetXmlValue(ref creditCardTypeName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("creditCardImage")) {
					SetXmlValue(ref creditCardImage, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayOrder")) {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayable")) {
					SetXmlValue(ref displayable, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CreditCardTypes[] GetCreditCardTypess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCreditCardTypess();
		}

		/*
		public static CreditCardTypes GetCreditCardTypesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCreditCardTypesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCreditCardTypes(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCreditCardTypes(this);
		}*/
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

		public int Displayable {
			set { displayable = value; }
			get { return displayable; }
		}

		#endregion
	}
}
