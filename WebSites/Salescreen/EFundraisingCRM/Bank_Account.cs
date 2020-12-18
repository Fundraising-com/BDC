using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class BankAccount: EFundraisingCRMDataObject {

		private int bankID;
		private string bankAccountNo;
		private string currencyCode;
		private string gLAccountNo;


		public BankAccount() : this(int.MinValue) { }
		public BankAccount(int bankID) : this(bankID, null) { }
		public BankAccount(int bankID, string bankAccountNo) : this(bankID, bankAccountNo, null) { }
		public BankAccount(int bankID, string bankAccountNo, string currencyCode) : this(bankID, bankAccountNo, currencyCode, null) { }
		public BankAccount(int bankID, string bankAccountNo, string currencyCode, string gLAccountNo) {
			this.bankID = bankID;
			this.bankAccountNo = bankAccountNo;
			this.currencyCode = currencyCode;
			this.gLAccountNo = gLAccountNo;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BankAccount>\r\n" +
			"	<BankID>" + bankID + "</BankID>\r\n" +
			"	<BankAccountNo>" + System.Web.HttpUtility.HtmlEncode(bankAccountNo) + "</BankAccountNo>\r\n" +
			"	<CurrencyCode>" + System.Web.HttpUtility.HtmlEncode(currencyCode) + "</CurrencyCode>\r\n" +
			"	<GLAccountNo>" + System.Web.HttpUtility.HtmlEncode(gLAccountNo) + "</GLAccountNo>\r\n" +
			"</BankAccount>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("bankId")) {
					SetXmlValue(ref bankID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("bankAccountNo")) {
					SetXmlValue(ref bankAccountNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("currencyCode")) {
					SetXmlValue(ref currencyCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("glAccountNo")) {
					SetXmlValue(ref gLAccountNo, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BankAccount[] GetBankAccounts() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBankAccounts();
		}

		public static BankAccount GetBankAccountByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBankAccountByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBankAccount(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBankAccount(this);
		}
		#endregion

		#region Properties
		public int BankID {
			set { bankID = value; }
			get { return bankID; }
		}

		public string BankAccountNo {
			set { bankAccountNo = value; }
			get { return bankAccountNo; }
		}

		public string CurrencyCode {
			set { currencyCode = value; }
			get { return currencyCode; }
		}

		public string GLAccountNo {
			set { gLAccountNo = value; }
			get { return gLAccountNo; }
		}

		#endregion
	}
}
