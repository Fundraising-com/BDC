using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PartnerGroupTypes: EFundraisingCRMDataObject {

		private short partnerGroupTypeId;
		private string partnerGroupTypeDesc;


		public PartnerGroupTypes() : this(short.MinValue) { }
		public PartnerGroupTypes(short partnerGroupTypeId) : this(partnerGroupTypeId, null) { }
		public PartnerGroupTypes(short partnerGroupTypeId, string partnerGroupTypeDesc) {
			this.partnerGroupTypeId = partnerGroupTypeId;
			this.partnerGroupTypeDesc = partnerGroupTypeDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerGroupTypes>\r\n" +
			"	<PartnerGroupTypeId>" + partnerGroupTypeId + "</PartnerGroupTypeId>\r\n" +
			"	<PartnerGroupTypeDesc>" + System.Web.HttpUtility.HtmlEncode(partnerGroupTypeDesc) + "</PartnerGroupTypeDesc>\r\n" +
			"</PartnerGroupTypes>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partnerGroupTypeId")) {
					SetXmlValue(ref partnerGroupTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerGroupTypeDesc")) {
					SetXmlValue(ref partnerGroupTypeDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerGroupTypes[] GetPartnerGroupTypess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerGroupTypess();
		}

		/*
		public static PartnerGroupTypes GetPartnerGroupTypesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerGroupTypesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPartnerGroupTypes(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePartnerGroupTypes(this);
		}*/
		#endregion

		#region Properties
		public short PartnerGroupTypeId {
			set { partnerGroupTypeId = value; }
			get { return partnerGroupTypeId; }
		}

		public string PartnerGroupTypeDesc {
			set { partnerGroupTypeDesc = value; }
			get { return partnerGroupTypeDesc; }
		}

		#endregion
	}
}
