using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class PartnerGroupType: eFundraisingStoreDataObject {

		private short partnerGroupTypeId;
		private string description;


		public PartnerGroupType() : this(short.MinValue) { }
		public PartnerGroupType(short partnerGroupTypeId) : this(partnerGroupTypeId, null) { }
		public PartnerGroupType(short partnerGroupTypeId, string description) {
			this.partnerGroupTypeId = partnerGroupTypeId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerGroupType>\r\n" +
			"	<PartnerGroupTypeId>" + partnerGroupTypeId + "</PartnerGroupTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</PartnerGroupType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "partnerGroupTypeId") {
					SetXmlValue(ref partnerGroupTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerGroupType[] GetPartnerGroupTypes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerGroupTypes();
		}

		public static PartnerGroupType GetPartnerGroupTypeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPartnerGroupTypeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPartnerGroupType(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePartnerGroupType(this);
		}
		#endregion

		#region Properties
		public short PartnerGroupTypeId {
			set { partnerGroupTypeId = value; }
			get { return partnerGroupTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
