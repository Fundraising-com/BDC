using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AccountingPeriod: EFundraisingCRMDataObject {

		private DateTime closingDate;


		public AccountingPeriod() : this(DateTime.MinValue) { }
		public AccountingPeriod(DateTime closingDate) {
			this.closingDate = closingDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AccountingPeriod>\r\n" +
			"	<ClosingDate>" + closingDate + "</ClosingDate>\r\n" +
			"</AccountingPeriod>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("closingDate")) {
					SetXmlValue(ref closingDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static AccountingPeriod[] GetAccountingPeriods() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAccountingPeriods();
		}

		public static AccountingPeriod GetAccountingPeriodByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAccountingPeriodByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAccountingPeriod(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAccountingPeriod(this);
		}*/
		#endregion

		#region Properties
		public DateTime ClosingDate {
			set { closingDate = value; }
			get { return closingDate; }
		}

		#endregion
	}
}
