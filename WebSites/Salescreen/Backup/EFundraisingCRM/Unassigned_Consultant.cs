using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class UnassignedConsultant: EFundraisingCRMDataObject {

		private int leadID;
		private int oldConsultantID;
		private int newConsultantID;
		private DateTime unassignedDate;
		private int unassignationID;


		public UnassignedConsultant() : this(int.MinValue) { }
		public UnassignedConsultant(int leadID) : this(leadID, int.MinValue) { }
		public UnassignedConsultant(int leadID, int oldConsultantID) : this(leadID, oldConsultantID, int.MinValue) { }
		public UnassignedConsultant(int leadID, int oldConsultantID, int newConsultantID) : this(leadID, oldConsultantID, newConsultantID, DateTime.MinValue) { }
		public UnassignedConsultant(int leadID, int oldConsultantID, int newConsultantID, DateTime unassignedDate) : this(leadID, oldConsultantID, newConsultantID, unassignedDate, int.MinValue) { }
		public UnassignedConsultant(int leadID, int oldConsultantID, int newConsultantID, DateTime unassignedDate, int unassignationID) {
			this.leadID = leadID;
			this.oldConsultantID = oldConsultantID;
			this.newConsultantID = newConsultantID;
			this.unassignedDate = unassignedDate;
			this.unassignationID = unassignationID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<UnassignedConsultant>\r\n" +
			"	<LeadID>" + leadID + "</LeadID>\r\n" +
			"	<OldConsultantID>" + oldConsultantID + "</OldConsultantID>\r\n" +
			"	<NewConsultantID>" + newConsultantID + "</NewConsultantID>\r\n" +
			"	<UnassignedDate>" + unassignedDate + "</UnassignedDate>\r\n" +
			"	<UnassignationID>" + unassignationID + "</UnassignationID>\r\n" +
			"</UnassignedConsultant>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("oldConsultantId")) {
					SetXmlValue(ref oldConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("newConsultantId")) {
					SetXmlValue(ref newConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unassignedDate")) {
					SetXmlValue(ref unassignedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unassignationId")) {
					SetXmlValue(ref unassignationID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static UnassignedConsultant[] GetUnassignedConsultants() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetUnassignedConsultants();
		}

		public static UnassignedConsultant GetUnassignedConsultantByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetUnassignedConsultantByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertUnassignedConsultant(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateUnassignedConsultant(this);
		}
		#endregion

		#region Properties
		public int LeadID {
			set { leadID = value; }
			get { return leadID; }
		}

		public int OldConsultantID {
			set { oldConsultantID = value; }
			get { return oldConsultantID; }
		}

		public int NewConsultantID {
			set { newConsultantID = value; }
			get { return newConsultantID; }
		}

		public DateTime UnassignedDate {
			set { unassignedDate = value; }
			get { return unassignedDate; }
		}

		public int UnassignationID {
			set { unassignationID = value; }
			get { return unassignationID; }
		}

		#endregion
	}
}
