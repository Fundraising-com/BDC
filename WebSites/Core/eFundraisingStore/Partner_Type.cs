using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PartnerType: eFundraisingStoreDataObject {

		private int partnerTypeId;
		private string name;
		private DateTime createDate;


		public PartnerType() : this(int.MinValue) { }
		public PartnerType(int partnerTypeId) : this(partnerTypeId, null) { }
		public PartnerType(int partnerTypeId, string name) : this(partnerTypeId, name, DateTime.MinValue) { }
		public PartnerType(int partnerTypeId, string name, DateTime createDate) {
			this.partnerTypeId = partnerTypeId;
			this.name = name;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerType>\r\n" +
			"	<PartnerTypeId>" + partnerTypeId + "</PartnerTypeId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PartnerType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerTypeId") {
					SetXmlValue(ref partnerTypeId, node.InnerText);
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
		public static PartnerType[] GetPartnerTypes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerTypes();
		}

		public static PartnerType GetPartnerTypeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerTypeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartnerType(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartnerType(this);
		}
		#endregion

		#region Properties
		public int PartnerTypeId {
			set { partnerTypeId = value; }
			get { return partnerTypeId; }
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
