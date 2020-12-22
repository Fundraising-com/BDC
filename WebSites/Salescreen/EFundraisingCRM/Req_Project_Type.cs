using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ReqProjectType: EFundraisingCRMDataObject {

		private int projectTypeID;
		private int languageId;
		private string description;


		public ReqProjectType() : this(int.MinValue) { }
		public ReqProjectType(int projectTypeID) : this(projectTypeID, int.MinValue) { }
		public ReqProjectType(int projectTypeID, int languageId) : this(projectTypeID, languageId, null) { }
		public ReqProjectType(int projectTypeID, int languageId, string description) {
			this.projectTypeID = projectTypeID;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ReqProjectType>\r\n" +
			"	<ProjectTypeID>" + projectTypeID + "</ProjectTypeID>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ReqProjectType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("projectTypeId")) {
					SetXmlValue(ref projectTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ReqProjectType[] GetReqProjectTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqProjectTypes();
		}

		public static ReqProjectType GetReqProjectTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqProjectTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReqProjectType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReqProjectType(this);
		}
		#endregion

		#region Properties
		public int ProjectTypeID {
			set { projectTypeID = value; }
			get { return projectTypeID; }
		}

		public int LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
