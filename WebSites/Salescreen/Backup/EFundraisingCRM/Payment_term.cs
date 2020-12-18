using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PaymentTerm: EFundraisingCRMDataObject {

		private short paymentTermId;
		private string description;
		private float discountPercent;
		private int leadDays;
		private int defaultArStatus;
		private int hideFromConsultants;


		public PaymentTerm() : this(short.MinValue) { }
		public PaymentTerm(short paymentTermId) : this(paymentTermId, null) { }
		public PaymentTerm(short paymentTermId, string description) : this(paymentTermId, description, float.MinValue) { }
		public PaymentTerm(short paymentTermId, string description, float discountPercent) : this(paymentTermId, description, discountPercent, int.MinValue) { }
		public PaymentTerm(short paymentTermId, string description, float discountPercent, int leadDays) : this(paymentTermId, description, discountPercent, leadDays, int.MinValue) { }
		public PaymentTerm(short paymentTermId, string description, float discountPercent, int leadDays, int defaultArStatus) : this(paymentTermId, description, discountPercent, leadDays, defaultArStatus, int.MinValue) { }
		public PaymentTerm(short paymentTermId, string description, float discountPercent, int leadDays, int defaultArStatus, int hideFromConsultants) {
			this.paymentTermId = paymentTermId;
			this.description = description;
			this.discountPercent = discountPercent;
			this.leadDays = leadDays;
			this.defaultArStatus = defaultArStatus;
			this.hideFromConsultants = hideFromConsultants;
		}

		#region Static Data
		public static PaymentTerm COD_30Days {
			get { return new PaymentTerm(1, "COD - 30 Days", 0, 30, 2, 0); }
		}

		public static PaymentTerm Prepaid_CheckPickUp {
			get { return new PaymentTerm(2, "Prepaid - check pick-up", 0, int.MinValue, int.MinValue, 0); }
		}

		public static PaymentTerm COD {
			get { return new PaymentTerm(8, "COD", 0, 30, 2, 0); }
		}

		public static PaymentTerm ThirtyDaysNet {
			get { return new PaymentTerm(10, "30 days net", 0, 30, 3, 0); }
		}

		public static PaymentTerm Prepaid {
			get { return new PaymentTerm(12, "Prepaid", 0, 0, int.MinValue, 0); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PaymentTerm>\r\n" +
			"	<PaymentTermId>" + paymentTermId + "</PaymentTermId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<DiscountPercent>" + discountPercent + "</DiscountPercent>\r\n" +
			"	<LeadDays>" + leadDays + "</LeadDays>\r\n" +
			"	<DefaultArStatus>" + defaultArStatus + "</DefaultArStatus>\r\n" +
			"	<HideFromConsultants>" + hideFromConsultants + "</HideFromConsultants>\r\n" +
			"</PaymentTerm>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("paymentTermId")) {
					SetXmlValue(ref paymentTermId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("discountPercent")) {
					SetXmlValue(ref discountPercent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadDays")) {
					SetXmlValue(ref leadDays, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("defaultArStatus")) {
					SetXmlValue(ref defaultArStatus, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("hideFromConsultants")) {
					SetXmlValue(ref hideFromConsultants, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PaymentTerm[] GetPaymentTerms() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentTerms();
		}

		
		public static PaymentTerm GetPaymentTermByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentTermByID(id);
		}
/*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPaymentTerm(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePaymentTerm(this);
		}*/
		#endregion

		#region Properties
		public short PaymentTermId {
			set { paymentTermId = value; }
			get { return paymentTermId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public float DiscountPercent {
			set { discountPercent = value; }
			get { return discountPercent; }
		}

		public int LeadDays {
			set { leadDays = value; }
			get { return leadDays; }
		}

		public int DefaultArStatus {
			set { defaultArStatus = value; }
			get { return defaultArStatus; }
		}

		public int HideFromConsultants {
			set { hideFromConsultants = value; }
			get { return hideFromConsultants; }
		}

		#endregion
	}
}
