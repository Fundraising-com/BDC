using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AddressZone: EFundraisingCRMDataObject {

		private int addressZoneId;
		private string description;


		public AddressZone() : this(int.MinValue) { }
		public AddressZone(int addressZoneId) : this(addressZoneId, null) { }
		public AddressZone(int addressZoneId, string description) {
			this.addressZoneId = addressZoneId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AddressZone>\r\n" +
				"	<AddressZoneId>" + addressZoneId + "</AddressZoneId>\r\n" +
				"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
				"</AddressZone>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "addressZoneId") {
					SetXmlValue(ref addressZoneId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AddressZone[] GetAddressZones() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAddressZones();
		}

		public static AddressZone GetAddressZoneByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAddressZoneByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAddressZone(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAddressZone(this);
		}
		#endregion

		#region Properties
		public int AddressZoneId {
			set { addressZoneId = value; }
			get { return addressZoneId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
