using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class PhoneNumberTracking: EFundraisingCRMDataObject {

		private int phoneNumberTrackingId;
		private string phoneNumberTrackingDesc;


		public PhoneNumberTracking() : this(int.MinValue) { }
		public PhoneNumberTracking(int phoneNumberTrackingId) : this(phoneNumberTrackingId, null) { }
		public PhoneNumberTracking(int phoneNumberTrackingId, string phoneNumberTrackingDesc) {
			this.phoneNumberTrackingId = phoneNumberTrackingId;
			this.phoneNumberTrackingDesc = phoneNumberTrackingDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PhoneNumberTracking>\r\n" +
			"	<PhoneNumberTrackingId>" + phoneNumberTrackingId + "</PhoneNumberTrackingId>\r\n" +
			"	<PhoneNumberTrackingDesc>" + System.Web.HttpUtility.HtmlEncode(phoneNumberTrackingDesc) + "</PhoneNumberTrackingDesc>\r\n" +
			"</PhoneNumberTracking>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("phoneNumberTrackingId")) {
					SetXmlValue(ref phoneNumberTrackingId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneNumberTrackingDesc")) {
					SetXmlValue(ref phoneNumberTrackingDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PhoneNumberTracking[] GetPhoneNumberTrackings() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPhoneNumberTrackings();
		}

		public static PhoneNumberTracking GetPhoneNumberTrackingByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPhoneNumberTrackingByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPhoneNumberTracking(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePhoneNumberTracking(this);
		}
		#endregion

		#region Properties
		public int PhoneNumberTrackingId {
			set { phoneNumberTrackingId = value; }
			get { return phoneNumberTrackingId; }
		}

		public string PhoneNumberTrackingDesc {
			set { phoneNumberTrackingDesc = value; }
			get { return phoneNumberTrackingDesc; }
		}

		#endregion
	}
}
