using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PartnerAttributeValue: eFundraisingStoreDataObject {

		private int partnerId;
		private int partnerAttributeId;
		private string cultureCode;
		private string value;
		private DateTime createDate;


		public PartnerAttributeValue() : this(int.MinValue) { }
		public PartnerAttributeValue(int partnerId) : this(partnerId, int.MinValue) { }
		public PartnerAttributeValue(int partnerId, int partnerAttributeId) : this(partnerId, partnerAttributeId, null) { }
		public PartnerAttributeValue(int partnerId, int partnerAttributeId, string cultureCode) : this(partnerId, partnerAttributeId, cultureCode, null) { }
		public PartnerAttributeValue(int partnerId, int partnerAttributeId, string cultureCode, string value) : this(partnerId, partnerAttributeId, cultureCode, value, DateTime.MinValue) { }
		public PartnerAttributeValue(int partnerId, int partnerAttributeId, string cultureCode, string value, DateTime createDate) {
			this.partnerId = partnerId;
			this.partnerAttributeId = partnerAttributeId;
			this.cultureCode = cultureCode;
			this.value = value;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerAttributeValue>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<PartnerAttributeId>" + partnerAttributeId + "</PartnerAttributeId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Value>" + System.Web.HttpUtility.HtmlEncode(value) + "</Value>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PartnerAttributeValue>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerAttributeId") {
					SetXmlValue(ref partnerAttributeId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "value") {
					SetXmlValue(ref value, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerAttributeValue[] GetPartnerAttributeValues() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerAttributeValues();
		}

		public static PartnerAttributeValue GetPartnerAttributeValueByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerAttributeValueByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartnerAttributeValue(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartnerAttributeValue(this);
		}
		#endregion

		#region Properties
		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public int PartnerAttributeId {
			set { partnerAttributeId = value; }
			get { return partnerAttributeId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string Value {
			set { value = value; }
			get { return value; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
