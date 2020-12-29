using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PartnerContacts: EFundraisingCRMDataObject {

		private int partnerContactId;
		private int partnerId;
		private short languageId;
		private string sectionName;
		private string sectionValue;
		private short displayOrder;


		public PartnerContacts() : this(int.MinValue) { }
		public PartnerContacts(int partnerContactId) : this(partnerContactId, int.MinValue) { }
		public PartnerContacts(int partnerContactId, int partnerId) : this(partnerContactId, partnerId, short.MinValue) { }
		public PartnerContacts(int partnerContactId, int partnerId, short languageId) : this(partnerContactId, partnerId, languageId, null) { }
		public PartnerContacts(int partnerContactId, int partnerId, short languageId, string sectionName) : this(partnerContactId, partnerId, languageId, sectionName, null) { }
		public PartnerContacts(int partnerContactId, int partnerId, short languageId, string sectionName, string sectionValue) : this(partnerContactId, partnerId, languageId, sectionName, sectionValue, short.MinValue) { }
		public PartnerContacts(int partnerContactId, int partnerId, short languageId, string sectionName, string sectionValue, short displayOrder) {
			this.partnerContactId = partnerContactId;
			this.partnerId = partnerId;
			this.languageId = languageId;
			this.sectionName = sectionName;
			this.sectionValue = sectionValue;
			this.displayOrder = displayOrder;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerContacts>\r\n" +
			"	<PartnerContactId>" + partnerContactId + "</PartnerContactId>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<SectionName>" + System.Web.HttpUtility.HtmlEncode(sectionName) + "</SectionName>\r\n" +
			"	<SectionValue>" + System.Web.HttpUtility.HtmlEncode(sectionValue) + "</SectionValue>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"</PartnerContacts>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partnerContactId")) {
					SetXmlValue(ref partnerContactId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sectionName")) {
					SetXmlValue(ref sectionName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sectionValue")) {
					SetXmlValue(ref sectionValue, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayOrder")) {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerContacts[] GetPartnerContactss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerContactss();
		}

		public static PartnerContacts GetPartnerContactsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerContactsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPartnerContacts(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePartnerContacts(this);
		}
		#endregion

		#region Properties
		public int PartnerContactId {
			set { partnerContactId = value; }
			get { return partnerContactId; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
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
