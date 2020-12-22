using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AccountingPeriodResult: EFundraisingCRMDataObject {

		private int accountingPeriodResultId;
		private short accountingClassId;
		private DateTime period;
		private float amount;
		private float budgetedAmount;


		public AccountingPeriodResult() : this(int.MinValue) { }
		public AccountingPeriodResult(int accountingPeriodResultId) : this(accountingPeriodResultId, short.MinValue) { }
		public AccountingPeriodResult(int accountingPeriodResultId, short accountingClassId) : this(accountingPeriodResultId, accountingClassId, DateTime.MinValue) { }
		public AccountingPeriodResult(int accountingPeriodResultId, short accountingClassId, DateTime period) : this(accountingPeriodResultId, accountingClassId, period, float.MinValue) { }
		public AccountingPeriodResult(int accountingPeriodResultId, short accountingClassId, DateTime period, float amount) : this(accountingPeriodResultId, accountingClassId, period, amount, float.MinValue) { }
		public AccountingPeriodResult(int accountingPeriodResultId, short accountingClassId, DateTime period, float amount, float budgetedAmount) {
			this.accountingPeriodResultId = accountingPeriodResultId;
			this.accountingClassId = accountingClassId;
			this.period = period;
			this.amount = amount;
			this.budgetedAmount = budgetedAmount;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AccountingPeriodResult>\r\n" +
			"	<AccountingPeriodResultId>" + accountingPeriodResultId + "</AccountingPeriodResultId>\r\n" +
			"	<AccountingClassId>" + accountingClassId + "</AccountingClassId>\r\n" +
			"	<Period>" + period + "</Period>\r\n" +
			"	<Amount>" + amount + "</Amount>\r\n" +
			"	<BudgetedAmount>" + budgetedAmount + "</BudgetedAmount>\r\n" +
			"</AccountingPeriodResult>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("accountingPeriodResultId")) {
					SetXmlValue(ref accountingPeriodResultId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accountingClassId")) {
					SetXmlValue(ref accountingClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("period")) {
					SetXmlValue(ref period, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("amount")) {
					SetXmlValue(ref amount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("budgetedAmount")) {
					SetXmlValue(ref budgetedAmount, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AccountingPeriodResult[] GetAccountingPeriodResults() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAccountingPeriodResults();
		}

		public static AccountingPeriodResult GetAccountingPeriodResultByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAccountingPeriodResultByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAccountingPeriodResult(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAccountingPeriodResult(this);
		}
		#endregion

		#region Properties
		public int AccountingPeriodResultId {
			set { accountingPeriodResultId = value; }
			get { return accountingPeriodResultId; }
		}

		public short AccountingClassId {
			set { accountingClassId = value; }
			get { return accountingClassId; }
		}

		public DateTime Period {
			set { period = value; }
			get { return period; }
		}

		public float Amount {
			set { amount = value; }
			get { return amount; }
		}

		public float BudgetedAmount {
			set { budgetedAmount = value; }
			get { return budgetedAmount; }
		}

		#endregion
	}
}
