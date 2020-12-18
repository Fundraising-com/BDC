using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PartyType: EFundraisingCRMDataObject {

		private short partyTypeId;
		private string partyTypeDesc;


		public PartyType() : this(short.MinValue) { }
		public PartyType(short partyTypeId) : this(partyTypeId, null) { }
		public PartyType(short partyTypeId, string partyTypeDesc) {
			this.partyTypeId = partyTypeId;
			this.partyTypeDesc = partyTypeDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartyType>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<PartyTypeDesc>" + System.Web.HttpUtility.HtmlEncode(partyTypeDesc) + "</PartyTypeDesc>\r\n" +
			"</PartyType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partyTypeId")) {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partyTypeDesc")) {
					SetXmlValue(ref partyTypeDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartyType[] GetPartyTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartyTypes();
		}

		/*
		public static PartyType GetPartyTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartyTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPartyType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePartyType(this);
		}*/
		#endregion

		#region Properties
		public short PartyTypeId {
			set { partyTypeId = value; }
			get { return partyTypeId; }
		}

		public string PartyTypeDesc {
			set { partyTypeDesc = value; }
			get { return partyTypeDesc; }
		}

		#endregion
	}
}
