using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class AccountingClassShippingFee: eFundraisingStoreDataObject {

		private short accountingClassId;
		private double minAmount;
		private double maxAmount;
		private short shippingFee;


		public AccountingClassShippingFee() : this(short.MinValue) { }
		public AccountingClassShippingFee(short accountingClassId) : this(accountingClassId, double.MinValue) { }
		public AccountingClassShippingFee(short accountingClassId, double minAmount) : this(accountingClassId, minAmount, double.MinValue) { }
		public AccountingClassShippingFee(short accountingClassId, double minAmount, double maxAmount) : this(accountingClassId, minAmount, maxAmount, short.MinValue) { }
		public AccountingClassShippingFee(short accountingClassId, double minAmount, double maxAmount, short shippingFee) {
			this.accountingClassId = accountingClassId;
			this.minAmount = minAmount;
			this.maxAmount = maxAmount;
			this.shippingFee = shippingFee;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AccountingClassShippingFee>\r\n" +
			"	<AccountingClassId>" + accountingClassId + "</AccountingClassId>\r\n" +
			"	<MinAmount>" + minAmount + "</MinAmount>\r\n" +
			"	<MaxAmount>" + maxAmount + "</MaxAmount>\r\n" +
			"	<ShippingFee>" + shippingFee + "</ShippingFee>\r\n" +
			"</AccountingClassShippingFee>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "accountingClassId") {
					SetXmlValue(ref accountingClassId, node.InnerText);
				}
				if(node.Name.ToLower() == "minAmount") {
					SetXmlValue(ref minAmount, node.InnerText);
				}
				if(node.Name.ToLower() == "maxAmount") {
					SetXmlValue(ref maxAmount, node.InnerText);
				}
				if(node.Name.ToLower() == "shippingFee") {
					SetXmlValue(ref shippingFee, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AccountingClassShippingFee[] GetAccountingClassShippingFees() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetAccountingClassShippingFees();
		}

		public static AccountingClassShippingFee GetAccountingClassShippingFeeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetAccountingClassShippingFeeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertAccountingClassShippingFee(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateAccountingClassShippingFee(this);
		}
		#endregion

		#region Properties
		public short AccountingClassId {
			set { accountingClassId = value; }
			get { return accountingClassId; }
		}

		public double MinAmount {
			set { minAmount = value; }
			get { return minAmount; }
		}

		public double MaxAmount {
			set { maxAmount = value; }
			get { return maxAmount; }
		}

		public short ShippingFee {
			set { shippingFee = value; }
			get { return shippingFee; }
		}

		#endregion
	}
}
