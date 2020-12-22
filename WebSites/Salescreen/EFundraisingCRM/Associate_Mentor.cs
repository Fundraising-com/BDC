using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AssociateMentor: EFundraisingCRMDataObject {

		private int associateID;
		private int mentorID;
		private DateTime startDate;
		private DateTime endDate;


		public AssociateMentor() : this(int.MinValue) { }
		public AssociateMentor(int associateID) : this(associateID, int.MinValue) { }
		public AssociateMentor(int associateID, int mentorID) : this(associateID, mentorID, DateTime.MinValue) { }
		public AssociateMentor(int associateID, int mentorID, DateTime startDate) : this(associateID, mentorID, startDate, DateTime.MinValue) { }
		public AssociateMentor(int associateID, int mentorID, DateTime startDate, DateTime endDate) {
			this.associateID = associateID;
			this.mentorID = mentorID;
			this.startDate = startDate;
			this.endDate = endDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AssociateMentor>\r\n" +
			"	<AssociateID>" + associateID + "</AssociateID>\r\n" +
			"	<MentorID>" + mentorID + "</MentorID>\r\n" +
			"	<StartDate>" + startDate + "</StartDate>\r\n" +
			"	<EndDate>" + endDate + "</EndDate>\r\n" +
			"</AssociateMentor>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("associateId")) {
					SetXmlValue(ref associateID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("mentorId")) {
					SetXmlValue(ref mentorID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("startDate")) {
					SetXmlValue(ref startDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("endDate")) {
					SetXmlValue(ref endDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AssociateMentor[] GetAssociateMentors() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAssociateMentors();
		}

		public static AssociateMentor GetAssociateMentorByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAssociateMentorByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAssociateMentor(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAssociateMentor(this);
		}
		#endregion

		#region Properties
		public int AssociateID {
			set { associateID = value; }
			get { return associateID; }
		}

		public int MentorID {
			set { mentorID = value; }
			get { return mentorID; }
		}

		public DateTime StartDate {
			set { startDate = value; }
			get { return startDate; }
		}

		public DateTime EndDate {
			set { endDate = value; }
			get { return endDate; }
		}

		#endregion
	}
}
