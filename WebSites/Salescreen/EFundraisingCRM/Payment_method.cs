using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PaymentMethod: EFundraisingCRMDataObject {
		
		private short paymentMethodId;
		private string description;
		private int isNegative;
		private float discountPercentage;


		public PaymentMethod() : this(short.MinValue) { }
		public PaymentMethod(short paymentMethodId) : this(paymentMethodId, null) { }
		public PaymentMethod(short paymentMethodId, string description) : this(paymentMethodId, description, int.MinValue) { }
		public PaymentMethod(short paymentMethodId, string description, int isNegative) : this(paymentMethodId, description, isNegative, float.MinValue) { }
		public PaymentMethod(short paymentMethodId, string description, int isNegative, float discountPercentage) {
			this.paymentMethodId = paymentMethodId;
			this.description = description;
			this.isNegative = isNegative;
			this.discountPercentage = discountPercentage;
		}

		#region Static Data
		public static PaymentMethod Check {
			get { return new PaymentMethod(1, "Check", 0, 0); }
		}

		public static PaymentMethod VISA {
			get { return new PaymentMethod(2, "VISA", 0, 0); }
		}

		public static PaymentMethod MASTERCARD {
			get { return new PaymentMethod(3, "MASTERCARD", 0, 0); }
		}

		public static PaymentMethod AMEX {
			get { return new PaymentMethod(8, "AMEX", 0, 0); }
		}

		public static PaymentMethod Discover {
			get { return new PaymentMethod(9, "Discover", 0, 0); }
		}

		public static PaymentMethod CheckByPhone {
			get { return new PaymentMethod(10, "Check-by-phone", 0, 0); }
		}

        //added for paypal and PO processing for SAP (april-09-2015 JF)
        public static PaymentMethod Paypal
        {
            get { return new PaymentMethod(15, "Paypal", 0, 0); }
        }

        public static PaymentMethod PurchaseOrder
        {
            get { return new PaymentMethod(16, "Purchase-Order", 0, 0); }
        }

		public static string GetCreditCardName(int id)
		{
			string cardName = "";
			
			switch(id)
			{
				case 2: cardName = "VISA";
					break;
				case 3: cardName = "MASTERCARD";
					break;
				case 8: cardName = "AMEX";
					break;
				case 9: cardName = "Discover";
					break;
			}

			return cardName;
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PaymentMethod>\r\n" +
			"	<PaymentMethodId>" + paymentMethodId + "</PaymentMethodId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<IsNegative>" + isNegative + "</IsNegative>\r\n" +
			"	<DiscountPercentage>" + discountPercentage + "</DiscountPercentage>\r\n" +
			"</PaymentMethod>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("paymentMethodId")) {
					SetXmlValue(ref paymentMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isNegative")) {
					SetXmlValue(ref isNegative, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("discountPercentage")) {
					SetXmlValue(ref discountPercentage, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PaymentMethod[] GetPaymentMethods() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentMethods();
		}

		public static PaymentMethod GetPaymentMethodByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentMethodByID(id);
		}
        /*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPaymentMethod(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePaymentMethod(this);
		}*/
		#endregion

		#region Properties
		public short PaymentMethodId {
			set { paymentMethodId = value; }
			get { return paymentMethodId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int IsNegative {
			set { isNegative = value; }
			get { return isNegative; }
		}

		public float DiscountPercentage {
			set { discountPercentage = value; }
			get { return discountPercentage; }
		}

		#endregion
	}
}
