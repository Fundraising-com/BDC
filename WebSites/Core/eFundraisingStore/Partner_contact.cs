using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PartnerContact: eFundraisingStoreDataObject {

		private short partnerContactId;
		private int partnerId;
		private string cultureCode;
		private string sectionName;
		private string sectionValue;
		private short displayOrder;


		public PartnerContact() : this(short.MinValue) { }
		public PartnerContact(short partnerContactId) : this(partnerContactId, int.MinValue) { }
		public PartnerContact(short partnerContactId, int partnerId) : this(partnerContactId, partnerId, null) { }
		public PartnerContact(short partnerContactId, int partnerId, string cultureCode) : this(partnerContactId, partnerId, cultureCode, null) { }
		public PartnerContact(short partnerContactId, int partnerId, string cultureCode, string sectionName) : this(partnerContactId, partnerId, cultureCode, sectionName, null) { }
		public PartnerContact(short partnerContactId, int partnerId, string cultureCode, string sectionName, string sectionValue) : this(partnerContactId, partnerId, cultureCode, sectionName, sectionValue, short.MinValue) { }
		public PartnerContact(short partnerContactId, int partnerId, string cultureCode, string sectionName, string sectionValue, short displayOrder) {
			this.partnerContactId = partnerContactId;
			this.partnerId = partnerId;
			this.cultureCode = cultureCode;
			this.sectionName = sectionName;
			this.sectionValue = sectionValue;
			this.displayOrder = displayOrder;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerContact>\r\n" +
			"	<PartnerContactId>" + partnerContactId + "</PartnerContactId>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<SectionName>" + System.Web.HttpUtility.HtmlEncode(sectionName) + "</SectionName>\r\n" +
			"	<SectionValue>" + System.Web.HttpUtility.HtmlEncode(sectionValue) + "</SectionValue>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"</PartnerContact>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerContactId") {
					SetXmlValue(ref partnerContactId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "sectionName") {
					SetXmlValue(ref sectionName, node.InnerText);
				}
				if(node.Name.ToLower() == "sectionValue") {
					SetXmlValue(ref sectionValue, node.InnerText);
				}
				if(node.Name.ToLower() == "displayOrder") {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerContact[] GetPartnerContacts() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerContacts();
		}

		public static PartnerContact GetPartnerContactByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerContactByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartnerContact(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartnerContact(this);
		}
		#endregion

		#region Properties
		public short PartnerContactId {
			set { partnerContactId = value; }
			get { return partnerContactId; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string SectionName {
			set { sectionName = value; }
			get { return sectionName; }
		}

		public string SectionValue {
			set { sectionValue = value; }
			get { return sectionValue; }
		}

		public short DisplayOrder {
			set { displayOrder = value; }
			get { return displayOrder; }
		}

		#endregion
	}
}
