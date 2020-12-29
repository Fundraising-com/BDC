using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PartnerAttribute: eFundraisingStoreDataObject {

		private int partnerAttributeId;
		private string name;
		private DateTime createDate;


		public PartnerAttribute() : this(int.MinValue) { }
		public PartnerAttribute(int partnerAttributeId) : this(partnerAttributeId, null) { }
		public PartnerAttribute(int partnerAttributeId, string name) : this(partnerAttributeId, name, DateTime.MinValue) { }
		public PartnerAttribute(int partnerAttributeId, string name, DateTime createDate) {
			this.partnerAttributeId = partnerAttributeId;
			this.name = name;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerAttribute>\r\n" +
			"	<PartnerAttributeId>" + partnerAttributeId + "</PartnerAttributeId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PartnerAttribute>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerAttributeId") {
					SetXmlValue(ref partnerAttributeId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerAttribute[] GetPartnerAttributes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerAttributes();
		}

		public static PartnerAttribute GetPartnerAttributeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerAttributeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartnerAttribute(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartnerAttribute(this);
		}
		#endregion

		#region Properties
		public int PartnerAttributeId {
			set { partnerAttributeId = value; }
			get { return partnerAttributeId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
