using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class AccountingClass: eFundraisingStoreDataObject {

		private short accountingClassId;
		private short carrierId;
		private short shippingOptionId;
		private string description;
		private int rank;
		private short deliveryDays;
		private short shippingFees;
		private int freeShippingAmount;


		public AccountingClass() : this(short.MinValue) { }
		public AccountingClass(short accountingClassId) : this(accountingClassId, short.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId) : this(accountingClassId, carrierId, short.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId) : this(accountingClassId, carrierId, shippingOptionId, null) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId, string description) : this(accountingClassId, carrierId, shippingOptionId, description, int.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId, string description, int rank) : this(accountingClassId, carrierId, shippingOptionId, description, rank, short.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId, string description, int rank, short deliveryDays) : this(accountingClassId, carrierId, shippingOptionId, description, rank, deliveryDays, short.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId, string description, int rank, short deliveryDays, short shippingFees) : this(accountingClassId, carrierId, shippingOptionId, description, rank, deliveryDays, shippingFees, int.MinValue) { }
		public AccountingClass(short accountingClassId, short carrierId, short shippingOptionId, string description, int rank, short deliveryDays, short shippingFees, int freeShippingAmount) {
			this.accountingClassId = accountingClassId;
			this.carrierId = carrierId;
			this.shippingOptionId = shippingOptionId;
			this.description = description;
			this.rank = rank;
			this.deliveryDays = deliveryDays;
			this.shippingFees = shippingFees;
			this.freeShippingAmount = freeShippingAmount;
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
			"	<ShippingFees>" + shippingFees + "</ShippingFees>\r\n" +
			"	<FreeShippingAmount>" + freeShippingAmount + "</FreeShippingAmount>\r\n" +
			"</AccountingClass>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "accountingClassId") {
					SetXmlValue(ref accountingClassId, node.InnerText);
				}
				if(node.Name.ToLower() == "carrierId") {
					SetXmlValue(ref carrierId, node.InnerText);
				}
				if(node.Name.ToLower() == "shippingOptionId") {
					SetXmlValue(ref shippingOptionId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
				if(node.Name.ToLower() == "rank") {
					SetXmlValue(ref rank, node.InnerText);
				}
				if(node.Name.ToLower() == "deliveryDays") {
					SetXmlValue(ref deliveryDays, node.InnerText);
				}
				if(node.Name.ToLower() == "shippingFees") {
					SetXmlValue(ref shippingFees, node.InnerText);
				}
				if(node.Name.ToLower() == "freeShippingAmount") {
					SetXmlValue(ref freeShippingAmount, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AccountingClass[] GetAccountingClasss() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetAccountingClasss();
		}

		public static AccountingClass GetAccountingClassByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetAccountingClassByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertAccountingClass(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateAccountingClass(this);
		}
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

		public int Rank {
			set { rank = value; }
			get { return rank; }
		}

		public short DeliveryDays {
			set { deliveryDays = value; }
			get { return deliveryDays; }
		}

		public short ShippingFees {
			set { shippingFees = value; }
			get { return shippingFees; }
		}

		public int FreeShippingAmount {
			set { freeShippingAmount = value; }
			get { return freeShippingAmount; }
		}

		#endregion
	}
}
