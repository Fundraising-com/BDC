using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Partner: eFundraisingStoreDataObject {

		private int partnerId;
		private int partnerTypeId;
		private string partnerName;
		private short hasCollectionSite;
		private string guid;
		private DateTime createDate;


		public Partner() : this(int.MinValue) { }
		public Partner(int partnerId) : this(partnerId, int.MinValue) { }
		public Partner(int partnerId, int partnerTypeId) : this(partnerId, partnerTypeId, null) { }
		public Partner(int partnerId, int partnerTypeId, string partnerName) : this(partnerId, partnerTypeId, partnerName, short.MinValue) { }
		public Partner(int partnerId, int partnerTypeId, string partnerName, short hasCollectionSite) : this(partnerId, partnerTypeId, partnerName, hasCollectionSite, null) { }
		public Partner(int partnerId, int partnerTypeId, string partnerName, short hasCollectionSite, string guid) : this(partnerId, partnerTypeId, partnerName, hasCollectionSite, guid, DateTime.MinValue) { }
		public Partner(int partnerId, int partnerTypeId, string partnerName, short hasCollectionSite, string guid, DateTime createDate) {
			this.partnerId = partnerId;
			this.partnerTypeId = partnerTypeId;
			this.partnerName = partnerName;
			this.hasCollectionSite = hasCollectionSite;
			this.guid = guid;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Partner>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<PartnerTypeId>" + partnerTypeId + "</PartnerTypeId>\r\n" +
			"	<PartnerName>" + System.Web.HttpUtility.HtmlEncode(partnerName) + "</PartnerName>\r\n" +
			"	<HasCollectionSite>" + hasCollectionSite + "</HasCollectionSite>\r\n" +
			"	<Guid>" + guid + "</Guid>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</Partner>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerTypeId") {
					SetXmlValue(ref partnerTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerName") {
					SetXmlValue(ref partnerName, node.InnerText);
				}
				if(node.Name.ToLower() == "hasCollectionSite") {
					SetXmlValue(ref hasCollectionSite, node.InnerText);
				}
				if(node.Name.ToLower() == "guid") {
					SetXmlValue(ref guid, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Partner[] GetPartners() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartners();
		}

		public static Partner GetPartnerByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartner(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartner(this);
		}
		#endregion

		#region Properties
		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public int PartnerTypeId {
			set { partnerTypeId = value; }
			get { return partnerTypeId; }
		}

		public string PartnerName {
			set { partnerName = value; }
			get { return partnerName; }
		}

		public short HasCollectionSite {
			set { hasCollectionSite = value; }
			get { return hasCollectionSite; }
		}

		public string Guid {
			set { guid = value; }
			get { return guid; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
