using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace efundraising.EFundraisingCRM {

	public class Payment: EFundraisingCRMDataObject {

		private int salesId;
		private int paymentNo;
		private byte paymentMethodId;
		private int collectionStatusId;
		private DateTime paymentEntryDate;
		private DateTime cashableDate;
		private string creditCardNo;
		private string expiryDate;
		private string nameOnCard;
		private string authorizationNumber;
		private double paymentAmount;
		private bool commissionPaid;
		private int foreignOrderId;


		public Payment() : this(int.MinValue) { }
		public Payment(int salesId) : this(salesId, int.MinValue) { }
		public Payment(int salesId, int paymentNo) : this(salesId, paymentNo, byte.MinValue) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId) : this(salesId, paymentNo, paymentMethodId, int.MinValue) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, DateTime.MinValue) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, paymentEntryDate, DateTime.MinValue) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate, DateTime cashableDate) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, paymentEntryDate, cashableDate, null) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate, DateTime cashableDate, string creditCardNo) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, paymentEntryDate, cashableDate, creditCardNo, null) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate, DateTime cashableDate, string creditCardNo, string expiryDate) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, paymentEntryDate, cashableDate, creditCardNo, expiryDate, null) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate, DateTime cashableDate, string creditCardNo, string expiryDate, string nameOnCard) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, paymentEntryDate, cashableDate, creditCardNo, expiryDate, nameOnCard, null) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate, DateTime cashableDate, string creditCardNo, string expiryDate, string nameOnCard, string authorizationNumber) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, paymentEntryDate, cashableDate, creditCardNo, expiryDate, nameOnCard, authorizationNumber, double.MinValue) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate, DateTime cashableDate, string creditCardNo, string expiryDate, string nameOnCard, string authorizationNumber, double paymentAmount) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, paymentEntryDate, cashableDate, creditCardNo, expiryDate, nameOnCard, authorizationNumber, paymentAmount, false) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate, DateTime cashableDate, string creditCardNo, string expiryDate, string nameOnCard, string authorizationNumber, double paymentAmount, bool commissionPaid) : this(salesId, paymentNo, paymentMethodId, collectionStatusId, paymentEntryDate, cashableDate, creditCardNo, expiryDate, nameOnCard, authorizationNumber, paymentAmount, commissionPaid, int.MinValue) { }
		public Payment(int salesId, int paymentNo, byte paymentMethodId, int collectionStatusId, DateTime paymentEntryDate, DateTime cashableDate, string creditCardNo, string expiryDate, string nameOnCard, string authorizationNumber, double paymentAmount, bool commissionPaid, int foreignOrderId) {
			this.salesId = salesId;
			this.paymentNo = paymentNo;
			this.paymentMethodId = paymentMethodId;
			this.collectionStatusId = collectionStatusId;
			this.paymentEntryDate = paymentEntryDate;
			this.cashableDate = cashableDate;
			this.creditCardNo = creditCardNo;
			this.expiryDate = expiryDate;
			this.nameOnCard = nameOnCard;
			this.authorizationNumber = authorizationNumber;
			this.paymentAmount = paymentAmount;
			this.commissionPaid = commissionPaid;
			this.foreignOrderId = foreignOrderId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Payment>\r\n" +
			"	<SalesId>" + salesId + "</SalesId>\r\n" +
			"	<PaymentNo>" + paymentNo + "</PaymentNo>\r\n" +
			"	<PaymentMethodId>" + paymentMethodId + "</PaymentMethodId>\r\n" +
			"	<CollectionStatusId>" + collectionStatusId + "</CollectionStatusId>\r\n" +
			"	<PaymentEntryDate>" + paymentEntryDate + "</PaymentEntryDate>\r\n" +
			"	<CashableDate>" + cashableDate + "</CashableDate>\r\n" +
			"	<CreditCardNo>" + System.Web.HttpUtility.HtmlEncode(creditCardNo) + "</CreditCardNo>\r\n" +
			"	<ExpiryDate>" + System.Web.HttpUtility.HtmlEncode(expiryDate) + "</ExpiryDate>\r\n" +
			"	<NameOnCard>" + System.Web.HttpUtility.HtmlEncode(nameOnCard) + "</NameOnCard>\r\n" +
			"	<AuthorizationNumber>" + System.Web.HttpUtility.HtmlEncode(authorizationNumber) + "</AuthorizationNumber>\r\n" +
			"	<PaymentAmount>" + paymentAmount + "</PaymentAmount>\r\n" +
			"	<CommissionPaid>" + commissionPaid + "</CommissionPaid>\r\n" +
			"	<ForeignOrderId>" + foreignOrderId + "</ForeignOrderId>\r\n" +
			"</Payment>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		/*public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentNo")) {
					SetXmlValue(ref paymentNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentMethodId")) {
					SetXmlValue(ref paymentMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("collectionStatusId")) {
					SetXmlValue(ref collectionStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentEntryDate")) {
					SetXmlValue(ref paymentEntryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cashableDate")) {
					SetXmlValue(ref cashableDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("creditCardNo")) {
					SetXmlValue(ref creditCardNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("expiryDate")) {
					SetXmlValue(ref expiryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nameOnCard")) {
					SetXmlValue(ref nameOnCard, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("authorizationNumber")) {
					SetXmlValue(ref authorizationNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentAmount")) {
					SetXmlValue(ref paymentAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionPaid")) {
					SetXmlValue(ref commissionPaid, node.InnerText);
				}
			}	 
		}  */
		#endregion

		#endregion

		#region Data Source Methods
		public static Payment[] GetPayments() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPayments();
		}


        public static int GetPaymentLatestNo(int saleId, DataAccess.EFundraisingCRMDatabase dbo)
        {
            return dbo.GetPaymentLatestNo(saleId);
        }

        public static bool GetPaymentDuplicate(int paymentId)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetPaymentDuplicate(paymentId);
        }

        public static Linq.payment GetPaymentDoubleEntry(int saleID, decimal paymentAmount, DataAccess.EFundraisingCRMDatabase dbo)
        {
               return dbo.GetPaymentDoubleInsert( saleID, paymentAmount);
        }

        public static int GetPaymentMethodIdByPaymentTypeId(int paymentTypeId)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetPaymentMethodIdByPaymentTypeId(paymentTypeId);
        }

        public static int GetPaymenttermIdByPaymentTypeId(int paymentTypeId)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetPaymentTermIdByPaymentTypeId(paymentTypeId);
        }


		public static PaymentCollection GetAllPaymentsByLeadId(int leadId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAllPaymentsByLeadId(leadId);
		}

        public static PaymentCollection GetPaymentsBySaleId(int saleId)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetPaymentsBySaleId(saleId);
        }

        public static PaymentCollection GetPaymentsByForeignOrderId(int foreignOrderId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentsByForeignOrderId(foreignOrderId);
		}

		public static Payment GetPaymentBySaleIdAndPaymentNo(int saleId, int paymentNo) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetPaymentBySaleIDandPaymentNo(saleId, paymentNo);
		}

		public static Payment GetPaymentByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentByID(id);
		}
        public static List<efundraising.EFundraisingCRM.Linq.payment> GetExternalPayments(int id, DataAccess.EFundraisingCRMDatabase dbo)
        {
        
            return dbo.GetExternalPayments(id);
        }


		public static decimal GetPaymentReceivableBySaleID(int saleID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPaymentReceivableBySaleID(saleID);
		}

		public void Insert() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			dbo.InsertPayment(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePayment(this);
		}
		#endregion
		
		#region Methods
		
		public int GetNextPaymentNo()
		{
			PaymentCollection payments = GetPaymentsBySaleId(this.SalesId);
			return payments.Count + 1;
		}


    
		
		
		#endregion

		#region Properties
		public int SalesId {
			set { salesId = value; }
			get { return salesId; }
		}

		public int PaymentNo {
			set { paymentNo = value; }
			get { return paymentNo; }
		}

		public byte PaymentMethodId {
			set { paymentMethodId = value; }
			get { return paymentMethodId; }
		}

		public int CollectionStatusId {
			set { collectionStatusId = value; }
			get { return collectionStatusId; }
		}

		public DateTime PaymentEntryDate {
			set { paymentEntryDate = value; }
			get { return paymentEntryDate; }
		}

		public DateTime CashableDate {
			set { cashableDate = value; }
			get { return cashableDate; }
		}

		public string CreditCardNo {
			set { creditCardNo = value; }
			get { return creditCardNo; }
		}

		public string ExpiryDate {
			set { expiryDate = value; }
			get { return expiryDate; }
		}

		public string NameOnCard {
			set { nameOnCard = value; }
			get { return nameOnCard; }
		}

		public string AuthorizationNumber {
			set { authorizationNumber = value; }
			get { return authorizationNumber; }
		}

		public double PaymentAmount {
			set { paymentAmount = value; }
			get { return paymentAmount; }
		}

		public bool CommissionPaid {
			set { commissionPaid = value; }
			get { return commissionPaid; }
		}

		public int ForeignOrderId {
			set { foreignOrderId = value; }
			get { return foreignOrderId; }
		}
		#endregion
	}
}
