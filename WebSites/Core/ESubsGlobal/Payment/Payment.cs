using System;
using System.Xml;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Payment {

	public enum _PaymentStatus
	{
		Error,
		Ok
	}

	public class CheckSystemPayment: DataObject
	{
		private PaymentStatus _paymentStatus;
		private PaymentInfo  _paymentInfo;
		private PaymentPeriod _paymentPeriod;
		private ESubsGlobal.Partner _partner;
		private ESubsGlobal.Group _group;
		private ESubsGlobal.Payment.PaymentType _paymentType;
		private GroupStatus _groupStatus;
        private int paymentTo;
		private Payment _payment;

		public Payment payment
		{
			get
			{
				return _payment;
			}

			set
			{
				_payment = value;
			}
		}

        public int PaymentTo
        {
            get { return paymentTo; }
            set { paymentTo = value; }
        }

		public PaymentStatus paymentStatus
		{
			get 
			{
				return _paymentStatus;
			}
			set
			{
				_paymentStatus = value;
			}
		}

		public PaymentInfo  paymentInfo
		{
			get
			{
				return _paymentInfo;
			}
			set
			{
				_paymentInfo = value;
			}
		}

		public PaymentPeriod checkSystemPaymentPeriod
		{
			get { return _paymentPeriod;}
			set { _paymentPeriod = value;}
		}

		public ESubsGlobal.Partner partner
		{
			get { return _partner;}
			set {_partner = value;}
		}

		public ESubsGlobal.Group group
		{
			get { return _group;}
			set {_group = value;}
		}

		public ESubsGlobal.Payment.PaymentType paymentType
		{
			get { return _paymentType;}
			set {_paymentType = value;}
		}

        public GroupStatus groupStatus
		{
			get { return _groupStatus;}
			set {_groupStatus = value;}
		}

        public int PaymentID
        {
            get { return payment.PaymentId; }
          }

        public string PartnerName
        {
            get
            {
                if (partner.Name != null && partner.Name != string.Empty)
                {
                    return partner.Name;
                }
                else
                {
                    return payment.Name;
                }
            }
        }
		#region XML Methods

		#region Save XML
		public override string GenerateXML() 
		{
			return _payment.GenerateXML();
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) 
		{
			_payment.Load(childNodes);
		}
		#endregion

		#endregion
		
		
	}
	public class Payment: DataObject {

		private int paymentId;
		private int paymentTypeId;
		private int paymentInfoId;
		private int paymentPeriodId;
		private int chequeNumber;
		private DateTime chequeDate;
		private Decimal paidAmount;
		private string name;
		private string phoneNumber;
		private string address1;
		private string address2;
		private string city;
		private string zipCode;
		private string countryCode;
		private string subdivisionCode;
		private DateTime createDate;
        private int paymentBatchID;
        private bool isValidated;
        private bool isProcessed;


		public Payment() : this(int.MinValue) { }
		public Payment(int paymentId) : this(paymentId, int.MinValue) { }
		public Payment(int paymentId, int paymentTypeId) : this(paymentId, paymentTypeId, int.MinValue) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId) : this(paymentId, paymentTypeId, paymentInfoId, int.MinValue) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, int.MinValue) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, DateTime.MinValue) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, Decimal.MinValue) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, null) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, null) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, null) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, null) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, address2, null) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2, string city) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, address2, city, null) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2, string city, string zipCode) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, address2, city, zipCode, null) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2, string city, string zipCode, string countryCode) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, address2, city, zipCode, countryCode, null) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2, string city, string zipCode, string countryCode, string subdivisionCode) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, address2, city, zipCode, countryCode, subdivisionCode, DateTime.MinValue) { }
        public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2, string city, string zipCode, string countryCode, string subdivisionCode, DateTime createDate) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, address2, city, zipCode, countryCode, subdivisionCode, createDate, int.MinValue) { }
        public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2, string city, string zipCode, string countryCode, string subdivisionCode, DateTime createDate, int paymentBatchID) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, address2, city, zipCode, countryCode, subdivisionCode, createDate, paymentBatchID, false) { }
        public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2, string city, string zipCode, string countryCode, string subdivisionCode, DateTime createDate, int paymentBatchID, bool isValidated) : this(paymentId, paymentTypeId, paymentInfoId, paymentPeriodId, chequeNumber, chequeDate, paidAmount, name, phoneNumber, address1, address2, city, zipCode, countryCode, subdivisionCode, createDate, paymentBatchID,isValidated, false) { }
		public Payment(int paymentId, int paymentTypeId, int paymentInfoId, int paymentPeriodId, int chequeNumber, DateTime chequeDate, Decimal paidAmount, string name, string phoneNumber, string address1, string address2, string city, string zipCode, string countryCode, string subdivisionCode, DateTime createDate, int paymentBatchID, bool isValidated,  bool isProcessed) {
			this.paymentId = paymentId;
			this.paymentTypeId = paymentTypeId;
			this.paymentInfoId = paymentInfoId;
			this.paymentPeriodId = paymentPeriodId;
			this.chequeNumber = chequeNumber;
			this.chequeDate = chequeDate;
			this.paidAmount = paidAmount;
			this.name = name;
			this.phoneNumber = phoneNumber;
			this.address1 = address1;
			this.address2 = address2;
			this.city = city;
			this.zipCode = zipCode;
			this.countryCode = countryCode;
			this.subdivisionCode = subdivisionCode;
			this.createDate = createDate;
            this.paymentBatchID = paymentBatchID;
            this.isValidated = isValidated;
            this.isProcessed = isProcessed;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Payment>\r\n" +
			"	<PaymentId>" + paymentId + "</PaymentId>\r\n" +
			"	<PaymentTypeId>" + paymentTypeId + "</PaymentTypeId>\r\n" +
			"	<PaymentInfoId>" + paymentInfoId + "</PaymentInfoId>\r\n" +
			"	<PaymentPeriodId>" + paymentPeriodId + "</PaymentPeriodId>\r\n" +
			"	<ChequeNumber>" + chequeNumber + "</ChequeNumber>\r\n" +
			"	<ChequeDate>" + chequeDate + "</ChequeDate>\r\n" +
			"	<PaidAmount>" + paidAmount + "</PaidAmount>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<PhoneNumber>" + System.Web.HttpUtility.HtmlEncode(phoneNumber) + "</PhoneNumber>\r\n" +
			"	<Address1>" + System.Web.HttpUtility.HtmlEncode(address1) + "</Address1>\r\n" +
			"	<Address2>" + System.Web.HttpUtility.HtmlEncode(address2) + "</Address2>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<CountryCode>" + countryCode + "</CountryCode>\r\n" +
			"	<SubdivisionCode>" + subdivisionCode + "</SubdivisionCode>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</Payment>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "paymentId") {
					SetXmlValue(ref paymentId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentTypeId") {
					SetXmlValue(ref paymentTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentInfoId") {
					SetXmlValue(ref paymentInfoId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentPeriodId") {
					SetXmlValue(ref paymentPeriodId, node.InnerText);
				}
				if(node.Name.ToLower() == "chequeNumber") {
					SetXmlValue(ref chequeNumber, node.InnerText);
				}
				if(node.Name.ToLower() == "chequeDate") {
					SetXmlValue(ref chequeDate, node.InnerText);
				}
				if(node.Name.ToLower() == "paidAmount") {
					SetXmlValue(ref paidAmount, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "phoneNumber") {
					SetXmlValue(ref phoneNumber, node.InnerText);
				}
				if(node.Name.ToLower() == "address1") {
					SetXmlValue(ref address1, node.InnerText);
				}
				if(node.Name.ToLower() == "address2") {
					SetXmlValue(ref address2, node.InnerText);
				}
				if(node.Name.ToLower() == "city") {
					SetXmlValue(ref city, node.InnerText);
				}
				if(node.Name.ToLower() == "zipCode") {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(node.Name.ToLower() == "countryCode") {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "subdivisionCode") {
					SetXmlValue(ref subdivisionCode, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Payment[] GetPayments() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPayments();
		}
		public static Payment[] GetPaymentsWithoutExceptions()
		{
			DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentsWithoutExceptions();
		}
		
		public static Payment[] GetPaymentsInProcessWithoutExceptions()
		{
			DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentsInProcessWithoutExceptions();
		}

		public static List<CheckSystemPayment> GetCheckSystemPaymentsInProcessWithoutExceptions()
		{
			DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
			return dbo.GetCheckSystemPaymentsInProcessWithoutExceptions();
		}

        public static List<CheckSystemPayment> GetCheckSystemPaymentsByCountryCode(string countryCode)
        {
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dbo.GetCheckSystemPaymentsByCountryCode(countryCode);
        }


        public static List<CheckSystemPayment> GetCheckSystemPaymentsWithoutExceptionByCountryCode(string countryCode)
        {
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dbo.GetCheckSystemPaymentsWithoutExceptionByCountryCode(countryCode);
        }

        public static List<CheckSystemPayment> GetCheckSystemPaymentsAlreadyProcessed()
        {
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dbo.GetCheckSystemPaymentsAlreadyProcessed();
        }

        public static List<Payment> GetPaymentsByPaymentBatchID(int id)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetPaymentsByPaymentBatchID(id);
        }

		public static Payment GetPaymentByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByID(id);
		}
		public static Payment[] GetPaymentsCashedByGroupID(int id)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentsCashedByGroupID(id);
         }
        
		public static Payment[] GetPaymentByEventID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByEventID(id);
		}
		  
		public static Payment[] GetPaymentByEventIDProfitLess20(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByEventIDAndException(id, 
				GA.BDC.Core.ESubsGlobal.Payment.ExceptionTypeCategory.ProfitLessThan20,
				GA.BDC.Core.ESubsGlobal.Payment.PaymentStatusCategory.InProcess);
		}
		  
		public static Payment[] GetPaymentByEventIDPaymentAmountIsNegative(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByEventIDAndException(id, 
				GA.BDC.Core.ESubsGlobal.Payment.ExceptionTypeCategory.PaymentAmountLessThanAllowance,
				GA.BDC.Core.ESubsGlobal.Payment.PaymentStatusCategory.InProcess);
		}

		 
		public static Payment[] GetPaymentByPartnerIDProfitLess20(int partnerId) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByPartnerIDAndException(partnerId, 
				GA.BDC.Core.ESubsGlobal.Payment.ExceptionTypeCategory.ProfitLessThan20,
				GA.BDC.Core.ESubsGlobal.Payment.PaymentStatusCategory.InProcess);
		}

		 
		public static Payment[] GetPaymentByPartnerIDNegativeAmount(int partnerId) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByPartnerIDAndException(partnerId, 
				GA.BDC.Core.ESubsGlobal.Payment.ExceptionTypeCategory.PaymentAmountLessThanAllowance,
				GA.BDC.Core.ESubsGlobal.Payment.PaymentStatusCategory.InProcess);
		}

		
		public static Payment[] GetPaymentsForDPCC(DateTime upToThisDay) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentsForDPCC(upToThisDay);
		}


	

		public static Payment[] GetPaymentByEventIDBetween(int eventId, DateTime startDate, DateTime endDate)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByEventIDBetween(eventId, startDate, endDate);
		}

		public static Payment[] GetPaymentByPartnerIDBetween(int PartnerId, DateTime startDate, DateTime endDate)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByPartnerIDBetween(PartnerId, startDate, endDate);
		}

        public static Payment[] GetPaymentToBeValidated(string countryCode)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetPaymentToBeValidated(countryCode);
        }

		public static Decimal GetTotalPayment(int paymentID, int eventId,DateTime startDate, DateTime endDate, PartnerPaymentConfig ppConfig)
		{
			if (ppConfig == null)
			{
				ppConfig = ESubsGlobal.Payment.PartnerPaymentConfig.GetPartnerPaymentConfigByPaymentID(paymentID);
			}
			Decimal totalPayment = Decimal.Zero;
			//PartnerPaymentConfig ppConfig = PartnerPaymentConfig.GetPartnerPaymentConfigByPaymentID(paymentID);
			if (ppConfig != null && ppConfig.IsPayToPartner())
			{
				Payment[] payments = GetPaymentByPartnerIDBetween(ppConfig.PartnerId, startDate, endDate);
				if (payments != null && payments.Length > 0)
					for (int i= 0; i < payments.Length; i++)
						totalPayment += Convert.ToDecimal(payments[i].PaidAmount);
			}
			else
			{
				Payment[] payments = GetPaymentByEventIDBetween(eventId, startDate, endDate);
				if (payments != null && payments.Length > 0)
					for (int i= 0; i < payments.Length; i++)
						totalPayment += Convert.ToDecimal(payments[i].PaidAmount);
			}
			return totalPayment;
		}

		public static Payment[] GetPaymentByEventIDInProcess(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByEventIDInProcess(id);
		}

		public static Payment[] GetPaymentByGroupID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentByGroupID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPayment(this);
		}
        public static int UpdateValidations(string countryCode)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.UpdatePaymentToValidated(countryCode);
        }

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePayment(this);
		}

  


		#endregion

		#region Properties
		public int PaymentId {
			set { paymentId = value; }
			get { return paymentId; }
		}

		public int PaymentTypeId {
			set { paymentTypeId = value; }
			get { return paymentTypeId; }
		}

		public int PaymentInfoId {
			set { paymentInfoId = value; }
			get { return paymentInfoId; }
		}

		public int PaymentPeriodId {
			set { paymentPeriodId = value; }
			get { return paymentPeriodId; }
		}

		public int ChequeNumber {
			set { chequeNumber = value; }
			get { return chequeNumber; }
		}

		public DateTime ChequeDate {
			set { chequeDate = value; }
			get { return chequeDate; }
		}

		public Decimal PaidAmount {
			set { paidAmount = value; }
			get { return paidAmount; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string PhoneNumber {
			set { phoneNumber = value; }
			get { return phoneNumber; }
		}

		public string Address1 {
			set { address1 = value; }
			get { return address1; }
		}

		public string Address2 {
			set { address2 = value; }
			get { return address2; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string SubdivisionCode {
			set { subdivisionCode = value; }
			get { return subdivisionCode; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

        public int PaymentBatchID
        {
            get { return paymentBatchID; }
            set { paymentBatchID = value; }
        }

        public bool IsValidated
        {
            get { return isValidated; }
            set { isValidated = value; }
        }

        public bool IsProcessed
        {
            get { return isProcessed; }
            set { isProcessed = value; }
        }

       
		#endregion
	}
}
