using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Deposit: EFundraisingCRMDataObject {

		private int depositId;
		private short paymentMethodId;
		private int bankId;
		private string bankAccountNo;
		private DateTime depositDate;


		public Deposit() : this(int.MinValue) { }
		public Deposit(int depositId) : this(depositId, short.MinValue) { }
		public Deposit(int depositId, short paymentMethodId) : this(depositId, paymentMethodId, int.MinValue) { }
		public Deposit(int depositId, short paymentMethodId, int bankId) : this(depositId, paymentMethodId, bankId, null) { }
		public Deposit(int depositId, short paymentMethodId, int bankId, string bankAccountNo) : this(depositId, paymentMethodId, bankId, bankAccountNo, DateTime.MinValue) { }
		public Deposit(int depositId, short paymentMethodId, int bankId, string bankAccountNo, DateTime depositDate) {
			this.depositId = depositId;
			this.paymentMethodId = paymentMethodId;
			this.bankId = bankId;
			this.bankAccountNo = bankAccountNo;
			this.depositDate = depositDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Deposit>\r\n" +
			"	<DepositId>" + depositId + "</DepositId>\r\n" +
			"	<PaymentMethodId>" + paymentMethodId + "</PaymentMethodId>\r\n" +
			"	<BankId>" + bankId + "</BankId>\r\n" +
			"	<BankAccountNo>" + System.Web.HttpUtility.HtmlEncode(bankAccountNo) + "</BankAccountNo>\r\n" +
			"	<DepositDate>" + depositDate + "</DepositDate>\r\n" +
			"</Deposit>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("depositId")) {
					SetXmlValue(ref depositId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentMethodId")) {
					SetXmlValue(ref paymentMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("bankId")) {
					SetXmlValue(ref bankId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("bankAccountNo")) {
					SetXmlValue(ref bankAccountNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("depositDate")) {
					SetXmlValue(ref depositDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Deposit[] GetDeposits() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDeposits();
		}

		public static Deposit GetDepositByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDepositByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDeposit(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDeposit(this);
		}
		#endregion

		#region Properties
		public int DepositId {
			set { depositId = value; }
			get { return depositId; }
		}

		public short PaymentMethodId {
			set { paymentMethodId = value; }
			get { return paymentMethodId; }
		}

		public int BankId {
			set { bankId = value; }
			get { return bankId; }
		}

		public string BankAccountNo {
			set { bankAccountNo = value; }
			get { return bankAccountNo; }
		}

		public DateTime DepositDate {
			set { depositDate = value; }
			get { return depositDate; }
		}

		#endregion
	}
}
