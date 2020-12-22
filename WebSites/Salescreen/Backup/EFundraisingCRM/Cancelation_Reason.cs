using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CancelationReason: EFundraisingCRMDataObject {

		private int cancelationReasonId;
		private string description;


		public CancelationReason() : this(int.MinValue) { }
		public CancelationReason(int cancelationReasonId) : this(cancelationReasonId, null) { }
		public CancelationReason(int cancelationReasonId, string description) {
			this.cancelationReasonId = cancelationReasonId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CancelationReason>\r\n" +
			"	<CancelationReasonId>" + cancelationReasonId + "</CancelationReasonId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</CancelationReason>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("cancelationReasonId")) {
					SetXmlValue(ref cancelationReasonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CancelationReason[] GetCancelationReasons() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCancelationReasons();
		}

		public static CancelationReason GetCancelationReasonByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCancelationReasonByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCancelationReason(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCancelationReason(this);
		}
		#endregion

		#region Properties
		public int CancelationReasonId {
			set { cancelationReasonId = value; }
			get { return cancelationReasonId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
