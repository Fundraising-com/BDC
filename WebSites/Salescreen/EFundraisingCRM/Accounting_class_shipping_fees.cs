using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AccountingClassShippingFees: EFundraisingCRMDataObject {
		private short accountingClassId;
		private float minAmount;
		private float maxAmount;
		private short shippingFee;


		public AccountingClassShippingFees() : this(short.MinValue) { }
		public AccountingClassShippingFees(short accountingClassId) : this(accountingClassId, float.MinValue) { }
		public AccountingClassShippingFees(short accountingClassId, float minAmount) : this(accountingClassId, minAmount, float.MinValue) { }
		public AccountingClassShippingFees(short accountingClassId, float minAmount, float maxAmount) : this(accountingClassId, minAmount, maxAmount, short.MinValue) { }
		public AccountingClassShippingFees(short accountingClassId, float minAmount, float maxAmount, short shippingFee) {
			this.accountingClassId = accountingClassId;
			this.minAmount = minAmount;
			this.maxAmount = maxAmount;
			this.shippingFee = shippingFee;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AccountingClassShippingFees>\r\n" +
			"	<AccountingClassId>" + accountingClassId + "</AccountingClassId>\r\n" +
			"	<MinAmount>" + minAmount + "</MinAmount>\r\n" +
			"	<MaxAmount>" + maxAmount + "</MaxAmount>\r\n" +
			"	<ShippingFee>" + shippingFee + "</ShippingFee>\r\n" +
			"</AccountingClassShippingFees>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("accountingClassId")) {
					SetXmlValue(ref accountingClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("minAmount")) {
					SetXmlValue(ref minAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("maxAmount")) {
					SetXmlValue(ref maxAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippingFee")) {
					SetXmlValue(ref shippingFee, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AccountingClassShippingFees[] GetAccountingClassShippingFeess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAccountingClassShippingFeess();
		}

		/*
		public static AccountingClassShippingFees GetAccountingClassShippingFeesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAccountingClassShippingFeesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAccountingClassShippingFees(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAccountingClassShippingFees(this);
		}*/
		#endregion

		#region Properties
		public short AccountingClassId {
			set { accountingClassId = value; }
			get { return accountingClassId; }
		}

		public float MinAmount {
			set { minAmount = value; }
			get { return minAmount; }
		}

		public float MaxAmount {
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
