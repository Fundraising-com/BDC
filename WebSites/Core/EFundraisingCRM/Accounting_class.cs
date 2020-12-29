using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AccountingClass: EFundraisingCRMDataObject {

		private short accountingClassId;
		private short carrierId;
		private short shippingOptionId;
		private string description;
		private short rank;
		private short deliveryDays;


		public AccountingClass() : this(short.MinValue) { }
		public AccountingClass(short accountingClassId) : this(accountingClassId, short.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId) : this(accountingClassId, carrierId, short.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId) : this(accountingClassId, carrierId, shippingOptionId, null) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId, string description) : this(accountingClassId, carrierId, shippingOptionId, description, short.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId, string description, short rank) : this(accountingClassId, carrierId, shippingOptionId, description, rank, short.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId, string description, short rank, short deliveryDays) {
			this.accountingClassId = accountingClassId;
			this.carrierId = carrierId;
			this.shippingOptionId = shippingOptionId;
			this.description = description;
			this.rank = rank;
			this.deliveryDays = deliveryDays;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AccountingClass>\r\n" +
			"	<AccountingClassId>" + accountingClassId + "</AccountingClassId>\r\n" +
			"	<CarrierId>" + carrierId + "</CarrierId>\r\n" +
			"	<ShippingOptionId>" + shippingOptionId + "</ShippingOptionId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Rank>" + rank + "</Rank>\r\n" +
			"	<DeliveryDays>" + deliveryDays + "</DeliveryDays>\r\n" +
			"</AccountingClass>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("accountingClassId")) {
					SetXmlValue(ref accountingClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("carrierId")) {
					SetXmlValue(ref carrierId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippingOptionId")) {
					SetXmlValue(ref shippingOptionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("rank")) {
					SetXmlValue(ref rank, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("deliveryDays")) {
					SetXmlValue(ref deliveryDays, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AccountingClass[] GetAccountingClasss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAccountingClasss();
		}

		/*
		public static AccountingClass GetAccountingClassByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAccountingClassByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAccountingClass(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAccountingClass(this);
		}*/
		#endregion

		#region Properties
		public short AccountingClassId {
			set { accountingClassId = value; }
			get { return accountingClassId; }
		}

		public short CarrierId {
			set { carrierId = value; }
			get { return carrierId; }
		}

		public short ShippingOptionId {
			set { shippingOptionId = value; }
			get { return shippingOptionId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public short Rank {
			set { rank = value; }
			get { return rank; }
		}

		public short DeliveryDays {
			set { deliveryDays = value; }
			get { return deliveryDays; }
		}

		#endregion
	}
}
