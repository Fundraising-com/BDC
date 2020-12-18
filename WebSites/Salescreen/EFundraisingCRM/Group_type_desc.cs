using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class GroupTypeDesc: EFundraisingCRMDataObject {

		private short groupTypeId;
		private short languageId;
		private string description;


		public GroupTypeDesc() : this(short.MinValue) { }
		public GroupTypeDesc(short groupTypeId) : this(groupTypeId, short.MinValue) { }
		public GroupTypeDesc(short groupTypeId, short languageId) : this(groupTypeId, languageId, null) { }
		public GroupTypeDesc(short groupTypeId, short languageId, string description) {
			this.groupTypeId = groupTypeId;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<GroupTypeDesc>\r\n" +
			"	<GroupTypeId>" + groupTypeId + "</GroupTypeId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</GroupTypeDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("groupTypeId")) {
					SetXmlValue(ref groupTypeId, node.InnerText);
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
		public static GroupTypeDesc[] GetGroupTypeDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGroupTypeDescs();
		}

		/*
		public static GroupTypeDesc GetGroupTypeDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGroupTypeDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertGroupTypeDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateGroupTypeDesc(this);
		}*/
		#endregion

		#region Properties
		public short GroupTypeId {
			set { groupTypeId = value; }
			get { return groupTypeId; }
		}

		public short LanguageId {
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
