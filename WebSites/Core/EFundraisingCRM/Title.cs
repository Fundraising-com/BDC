using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	/*
	 * Tile or role of the client and/or lead.
	 * 
	 */

	public class Title: EFundraisingCRMDataObject {

		private int titleId;
		private int partyTypeId;
		private string titleDesc;


		public Title() : this(int.MinValue) { }
		public Title(int titleId) : this(titleId, int.MinValue) { }
		public Title(int titleId, int partyTypeId) : this(titleId, partyTypeId, null) { }
		public Title(int titleId, int partyTypeId, string titleDesc) {
			this.titleId = titleId;
			this.partyTypeId = partyTypeId;
			this.titleDesc = titleDesc;
		}

		#region Static Data
		public static Title ActivitiesDirector_Coordinator {
			get { return new Title(1, 1, "Activities Director / Coordinator"); }
		}

		public static Title AssistantCoach {
			get { return new Title(2, 1, "Assistant Coach"); }
		}

		public static Title AthleticDirector_Coordinator {
			get { return new Title(3, 1, "Athletic Director / Coordinator"); }
		}

		public static Title Coach_Trainer {
			get { return new Title(4, 1, "Coach / Trainer"); }
		}

		public static Title Parent {
			get { return new Title(5, 1, "Parent"); }
		}

		public static Title Principal_AssistantPrincipal {
			get { return new Title(6, 1, "Principal / Ass. Principal"); }
		}

		public static Title President_VicePresident {
			get { return new Title(7, 1, "President / Vice President"); }
		}

		public static Title Student {
			get { return new Title(8, 1, "Student"); }
		}

		public static Title Teacher {
			get { return new Title(9, 1, "Teacher"); }
		}

		public static Title Treasurer {
			get { return new Title(10, 1, "Treasurer"); }
		}

		public static Title FundraisingChairman {
			get { return new Title(11, 1, "Fundraising Chairman"); }
		}

		public static Title Secretary {
			get { return new Title(12, 1, "Secretary"); }
		}

		public static Title PTA_PTO {
			get { return new Title(13, 1, "PTA / PTO"); }
		}

		public static Title Director_Manager_Organizer {
			get { return new Title(14, 1, "Director / Manager / Organizer"); }
		}

		public static Title Leader_Captain {
			get { return new Title(15, 1, "Leader / Captain"); }
		}

		public static Title Pastor_YouthPastor {
			get { return new Title(16, 1, "Pastor / Youth Pastor"); }
		}

		public static Title Minister_YouthMinister {
			get { return new Title(17, 1, "Minister / Youth Minister"); }
		}

		public static Title Member_Volunteer {
			get { return new Title(18, 1, "Member / Volunteer"); }
		}

		public static Title DistrictAdministrator {
			get { return new Title(19, 1, "District Administrator"); }
		}

		public static Title InformationOfficer {
			get { return new Title(20, 1, "Information Officer"); }
		}

		public static Title Other {
			get { return new Title(99, 5, "Other"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Title>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<TitleDesc>" + System.Web.HttpUtility.HtmlEncode(titleDesc) + "</TitleDesc>\r\n" +
			"</Title>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("titleId")) {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partyTypeId")) {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("titleDesc")) {
					SetXmlValue(ref titleDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Title[] GetTitles() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTitles();
		}

		/*
		public static Title GetTitleByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTitleByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTitle(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTitle(this);
		}*/
		#endregion

		#region Properties
		public int TitleId {
			set { titleId = value; }
			get { return titleId; }
		}

		public int PartyTypeId {
			set { partyTypeId = value; }
			get { return partyTypeId; }
		}

		public string TitleDesc {
			set { titleDesc = value; }
			get { return titleDesc; }
		}

		#endregion
	}
}
