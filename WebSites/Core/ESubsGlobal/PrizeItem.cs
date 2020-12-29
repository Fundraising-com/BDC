using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal {

	public class PrizeItem: DataObject {

		private int prizeItemId;
		private int prizeId;
		private string prizeItemCode;
		private DateTime expirationDate;
		private DateTime createDate;


		public PrizeItem() : this(int.MinValue) { }
		public PrizeItem(int prizeItemId) : this(prizeItemId, int.MinValue) { }
		public PrizeItem(int prizeItemId, int prizeId) : this(prizeItemId, prizeId, null) { }
		public PrizeItem(int prizeItemId, int prizeId, string prizeItemCode) : this(prizeItemId, prizeId, prizeItemCode, DateTime.MinValue) { }
		public PrizeItem(int prizeItemId, int prizeId, string prizeItemCode, DateTime expirationDate) : this(prizeItemId, prizeId, prizeItemCode, expirationDate, DateTime.MinValue) { }
		public PrizeItem(int prizeItemId, int prizeId, string prizeItemCode, DateTime expirationDate, DateTime createDate) {
			this.prizeItemId = prizeItemId;
			this.prizeId = prizeId;
			this.prizeItemCode = prizeItemCode;
			this.expirationDate = expirationDate;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PrizeItem>\r\n" +
				"	<PrizeItemId>" + prizeItemId + "</PrizeItemId>\r\n" +
				"	<PrizeId>" + prizeId + "</PrizeId>\r\n" +
				"	<PrizeItemCode>" + System.Web.HttpUtility.HtmlEncode(prizeItemCode) + "</PrizeItemCode>\r\n" +
				"	<ExpirationDate>" + expirationDate + "</ExpirationDate>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</PrizeItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "prizeItemId") {
					SetXmlValue(ref prizeItemId, node.InnerText);
				}
				if(node.Name.ToLower() == "prizeId") {
					SetXmlValue(ref prizeId, node.InnerText);
				}
				if(node.Name.ToLower() == "prizeItemCode") {
					SetXmlValue(ref prizeItemCode, node.InnerText);
				}
				if(node.Name.ToLower() == "expirationDate") {
					SetXmlValue(ref expirationDate, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PrizeItem[] GetPrizeItems() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizeItems();
		}

		public static PrizeItem GetPrizeItemNext(int prizeType) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizeItemNext(prizeType);
		}

		public static PrizeItem GetPrizeItemByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizeItemByID(id);
		}

		public static PrizeItem[] GetPrizeItemByPrizeID (int id)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizeItemByPrizeID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPrizeItem(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePrizeItem(this);
		}
		#endregion

		#region Properties
		public int PrizeItemId {
			set { prizeItemId = value; }
			get { return prizeItemId; }
		}

		public int PrizeId {
			set { prizeId = value; }
			get { return prizeId; }
		}

		public string PrizeItemCode {
			set { prizeItemCode = value; }
			get { return prizeItemCode; }
		}

		public DateTime ExpirationDate {
			set { expirationDate = value; }
			get { return expirationDate; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
