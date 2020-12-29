using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SaleToAdd: EFundraisingCRMDataObject {

		private int saleToAddId;
		private int consultantId;
		private short paymentMethodId;
		private short poStatusId;
		private int salesStatusId;
		private int leadId;
		private short paymentTermId;
		private short carrierId;
		private short shippingOptionId;
		private short upfrontPaymentMethodId;
		private string poNumber;
		private string creditCardNo;
		private string expiryDate;
		private DateTime salesDate;
		private float shippingFees;
		private float shippingFeesDiscount;
		private DateTime paymentDueDate;
		private DateTime scheduledDeliveryDate;
		private string comment;
		private float totalAmount;
		private DateTime confirmedDate;
		private float upfrontPaymentRequired;
		private DateTime upfrontPaymentDueDate;
		private int isNew;
		private int sponsorRequired;
		private string ssnNumber;
		private string ssnAddress;
		private string ssnCity;
		private string ssnStateCode;
		private string ssnCountryCode;
		private string ssnZipCode;


		public SaleToAdd() : this(int.MinValue) { }
		public SaleToAdd(int saleToAddId) : this(saleToAddId, int.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId) : this(saleToAddId, consultantId, short.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId) : this(saleToAddId, consultantId, paymentMethodId, short.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, int.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, int.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, short.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, short.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, short.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, short.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, DateTime.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, float.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, float.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, DateTime.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, DateTime.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, float.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, DateTime.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, float.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, DateTime.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, upfrontPaymentDueDate, int.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int isNew) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, upfrontPaymentDueDate, isNew, int.MinValue) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int isNew, int sponsorRequired) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, upfrontPaymentDueDate, isNew, sponsorRequired, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int isNew, int sponsorRequired, string ssnNumber) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, upfrontPaymentDueDate, isNew, sponsorRequired, ssnNumber, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int isNew, int sponsorRequired, string ssnNumber, string ssnAddress) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, upfrontPaymentDueDate, isNew, sponsorRequired, ssnNumber, ssnAddress, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int isNew, int sponsorRequired, string ssnNumber, string ssnAddress, string ssnCity) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, upfrontPaymentDueDate, isNew, sponsorRequired, ssnNumber, ssnAddress, ssnCity, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int isNew, int sponsorRequired, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, upfrontPaymentDueDate, isNew, sponsorRequired, ssnNumber, ssnAddress, ssnCity, ssnStateCode, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int isNew, int sponsorRequired, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode) : this(saleToAddId, consultantId, paymentMethodId, poStatusId, salesStatusId, leadId, paymentTermId, carrierId, shippingOptionId, upfrontPaymentMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, scheduledDeliveryDate, comment, totalAmount, confirmedDate, upfrontPaymentRequired, upfrontPaymentDueDate, isNew, sponsorRequired, ssnNumber, ssnAddress, ssnCity, ssnStateCode, ssnCountryCode, null) { }
		public SaleToAdd(int saleToAddId, int consultantId, short paymentMethodId, short poStatusId, int salesStatusId, int leadId, short paymentTermId, short carrierId, short shippingOptionId, short upfrontPaymentMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, float shippingFees, float shippingFeesDiscount, DateTime paymentDueDate, DateTime scheduledDeliveryDate, string comment, float totalAmount, DateTime confirmedDate, float upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int isNew, int sponsorRequired, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode, string ssnZipCode) {
			this.saleToAddId = saleToAddId;
			this.consultantId = consultantId;
			this.paymentMethodId = paymentMethodId;
			this.poStatusId = poStatusId;
			this.salesStatusId = salesStatusId;
			this.leadId = leadId;
			this.paymentTermId = paymentTermId;
			this.carrierId = carrierId;
			this.shippingOptionId = shippingOptionId;
			this.upfrontPaymentMethodId = upfrontPaymentMethodId;
			this.poNumber = poNumber;
			this.creditCardNo = creditCardNo;
			this.expiryDate = expiryDate;
			this.salesDate = salesDate;
			this.shippingFees = shippingFees;
			this.shippingFeesDiscount = shippingFeesDiscount;
			this.paymentDueDate = paymentDueDate;
			this.scheduledDeliveryDate = scheduledDeliveryDate;
			this.comment = comment;
			this.totalAmount = totalAmount;
			this.confirmedDate = confirmedDate;
			this.upfrontPaymentRequired = upfrontPaymentRequired;
			this.upfrontPaymentDueDate = upfrontPaymentDueDate;
			this.isNew = isNew;
			this.sponsorRequired = sponsorRequired;
			this.ssnNumber = ssnNumber;
			this.ssnAddress = ssnAddress;
			this.ssnCity = ssnCity;
			this.ssnStateCode = ssnStateCode;
			this.ssnCountryCode = ssnCountryCode;
			this.ssnZipCode = ssnZipCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SaleToAdd>\r\n" +
			"	<SaleToAddId>" + saleToAddId + "</SaleToAddId>\r\n" +
			"	<ConsultantId>" + consultantId + "</ConsultantId>\r\n" +
			"	<PaymentMethodId>" + paymentMethodId + "</PaymentMethodId>\r\n" +
			"	<PoStatusId>" + poStatusId + "</PoStatusId>\r\n" +
			"	<SalesStatusId>" + salesStatusId + "</SalesStatusId>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<PaymentTermId>" + paymentTermId + "</PaymentTermId>\r\n" +
			"	<CarrierId>" + carrierId + "</CarrierId>\r\n" +
			"	<ShippingOptionId>" + shippingOptionId + "</ShippingOptionId>\r\n" +
			"	<UpfrontPaymentMethodId>" + upfrontPaymentMethodId + "</UpfrontPaymentMethodId>\r\n" +
			"	<PoNumber>" + System.Web.HttpUtility.HtmlEncode(poNumber) + "</PoNumber>\r\n" +
			"	<CreditCardNo>" + System.Web.HttpUtility.HtmlEncode(creditCardNo) + "</CreditCardNo>\r\n" +
			"	<ExpiryDate>" + System.Web.HttpUtility.HtmlEncode(expiryDate) + "</ExpiryDate>\r\n" +
			"	<SalesDate>" + salesDate + "</SalesDate>\r\n" +
			"	<ShippingFees>" + shippingFees + "</ShippingFees>\r\n" +
			"	<ShippingFeesDiscount>" + shippingFeesDiscount + "</ShippingFeesDiscount>\r\n" +
			"	<PaymentDueDate>" + paymentDueDate + "</PaymentDueDate>\r\n" +
			"	<ScheduledDeliveryDate>" + scheduledDeliveryDate + "</ScheduledDeliveryDate>\r\n" +
			"	<Comment>" + System.Web.HttpUtility.HtmlEncode(comment) + "</Comment>\r\n" +
			"	<TotalAmount>" + totalAmount + "</TotalAmount>\r\n" +
			"	<ConfirmedDate>" + confirmedDate + "</ConfirmedDate>\r\n" +
			"	<UpfrontPaymentRequired>" + upfrontPaymentRequired + "</UpfrontPaymentRequired>\r\n" +
			"	<UpfrontPaymentDueDate>" + upfrontPaymentDueDate + "</UpfrontPaymentDueDate>\r\n" +
			"	<IsNew>" + isNew + "</IsNew>\r\n" +
			"	<SponsorRequired>" + sponsorRequired + "</SponsorRequired>\r\n" +
			"	<SsnNumber>" + System.Web.HttpUtility.HtmlEncode(ssnNumber) + "</SsnNumber>\r\n" +
			"	<SsnAddress>" + System.Web.HttpUtility.HtmlEncode(ssnAddress) + "</SsnAddress>\r\n" +
			"	<SsnCity>" + System.Web.HttpUtility.HtmlEncode(ssnCity) + "</SsnCity>\r\n" +
			"	<SsnStateCode>" + System.Web.HttpUtility.HtmlEncode(ssnStateCode) + "</SsnStateCode>\r\n" +
			"	<SsnCountryCode>" + System.Web.HttpUtility.HtmlEncode(ssnCountryCode) + "</SsnCountryCode>\r\n" +
			"	<SsnZipCode>" + System.Web.HttpUtility.HtmlEncode(ssnZipCode) + "</SsnZipCode>\r\n" +
			"</SaleToAdd>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("saleToAddId")) {
					SetXmlValue(ref saleToAddId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentMethodId")) {
					SetXmlValue(ref paymentMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("poStatusId")) {
					SetXmlValue(ref poStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesStatusId")) {
					SetXmlValue(ref salesStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentTermId")) {
					SetXmlValue(ref paymentTermId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("carrierId")) {
					SetXmlValue(ref carrierId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippingOptionId")) {
					SetXmlValue(ref shippingOptionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("upfrontPaymentMethodId")) {
					SetXmlValue(ref upfrontPaymentMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("poNumber")) {
					SetXmlValue(ref poNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("creditCardNo")) {
					SetXmlValue(ref creditCardNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("expiryDate")) {
					SetXmlValue(ref expiryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesDate")) {
					SetXmlValue(ref salesDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippingFees")) {
					SetXmlValue(ref shippingFees, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippingFeesDiscount")) {
					SetXmlValue(ref shippingFeesDiscount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentDueDate")) {
					SetXmlValue(ref paymentDueDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scheduledDeliveryDate")) {
					SetXmlValue(ref scheduledDeliveryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comment")) {
					SetXmlValue(ref comment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("totalAmount")) {
					SetXmlValue(ref totalAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("confirmedDate")) {
					SetXmlValue(ref confirmedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("upfrontPaymentRequired")) {
					SetXmlValue(ref upfrontPaymentRequired, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("upfrontPaymentDueDate")) {
					SetXmlValue(ref upfrontPaymentDueDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isNew")) {
					SetXmlValue(ref isNew, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sponsorRequired")) {
					SetXmlValue(ref sponsorRequired, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnNumber")) {
					SetXmlValue(ref ssnNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnAddress")) {
					SetXmlValue(ref ssnAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnCity")) {
					SetXmlValue(ref ssnCity, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnStateCode")) {
					SetXmlValue(ref ssnStateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnCountryCode")) {
					SetXmlValue(ref ssnCountryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnZipCode")) {
					SetXmlValue(ref ssnZipCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SaleToAdd[] GetSaleToAdds() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleToAdds();
		}

		public static SaleToAdd GetSaleToAddByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleToAddByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSaleToAdd(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSaleToAdd(this);
		}
		#endregion

		#region Properties
		public int SaleToAddId {
			set { saleToAddId = value; }
			get { return saleToAddId; }
		}

		public int ConsultantId {
			set { consultantId = value; }
			get { return consultantId; }
		}

		public short PaymentMethodId {
			set { paymentMethodId = value; }
			get { return paymentMethodId; }
		}

		public short PoStatusId {
			set { poStatusId = value; }
			get { return poStatusId; }
		}

		public int SalesStatusId {
			set { salesStatusId = value; }
			get { return salesStatusId; }
		}

		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public short PaymentTermId {
			set { paymentTermId = value; }
			get { return paymentTermId; }
		}

		public short CarrierId {
			set { carrierId = value; }
			get { return carrierId; }
		}

		public short ShippingOptionId {
			set { shippingOptionId = value; }
			get { return shippingOptionId; }
		}

		public short UpfrontPaymentMethodId {
			set { upfrontPaymentMethodId = value; }
			get { return upfrontPaymentMethodId; }
		}

		public string PoNumber {
			set { poNumber = value; }
			get { return poNumber; }
		}

		public string CreditCardNo {
			set { creditCardNo = value; }
			get { return creditCardNo; }
		}

		public string ExpiryDate {
			set { expiryDate = value; }
			get { return expiryDate; }
		}

		public DateTime SalesDate {
			set { salesDate = value; }
			get { return salesDate; }
		}

		public float ShippingFees {
			set { shippingFees = value; }
			get { return shippingFees; }
		}

		public float ShippingFeesDiscount {
			set { shippingFeesDiscount = value; }
			get { return shippingFeesDiscount; }
		}

		public DateTime PaymentDueDate {
			set { paymentDueDate = value; }
			get { return paymentDueDate; }
		}

		public DateTime ScheduledDeliveryDate {
			set { scheduledDeliveryDate = value; }
			get { return scheduledDeliveryDate; }
		}

		public string Comment {
			set { comment = value; }
			get { return comment; }
		}

		public float TotalAmount {
			set { totalAmount = value; }
			get { return totalAmount; }
		}

		public DateTime ConfirmedDate {
			set { confirmedDate = value; }
			get { return confirmedDate; }
		}

		public float UpfrontPaymentRequired {
			set { upfrontPaymentRequired = value; }
			get { return upfrontPaymentRequired; }
		}

		public DateTime UpfrontPaymentDueDate {
			set { upfrontPaymentDueDate = value; }
			get { return upfrontPaymentDueDate; }
		}

		public int IsNew {
			set { isNew = value; }
			get { return isNew; }
		}

		public int SponsorRequired {
			set { sponsorRequired = value; }
			get { return sponsorRequired; }
		}

		public string SsnNumber {
			set { ssnNumber = value; }
			get { return ssnNumber; }
		}

		public string SsnAddress {
			set { ssnAddress = value; }
			get { return ssnAddress; }
		}

		public string SsnCity {
			set { ssnCity = value; }
			get { return ssnCity; }
		}

		public string SsnStateCode {
			set { ssnStateCode = value; }
			get { return ssnStateCode; }
		}

		public string SsnCountryCode {
			set { ssnCountryCode = value; }
			get { return ssnCountryCode; }
		}

		public string SsnZipCode {
			set { ssnZipCode = value; }
			get { return ssnZipCode; }
		}

		#endregion
	}
}
