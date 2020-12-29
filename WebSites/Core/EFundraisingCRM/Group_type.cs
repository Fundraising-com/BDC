using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class GroupType: EFundraisingCRMDataObject {

		private short groupTypeId;
		private short partyTypeId;
		private string description;


		public GroupType() : this(short.MinValue) { }
		public GroupType(short groupTypeId) : this(groupTypeId, short.MinValue) { }
		public GroupType(short groupTypeId, short partyTypeId) : this(groupTypeId, partyTypeId, null) { }
		public GroupType(short groupTypeId, short partyTypeId, string description) {
			this.groupTypeId = groupTypeId;
			this.partyTypeId = partyTypeId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<GroupType>\r\n" +
			"	<GroupTypeId>" + groupTypeId + "</GroupTypeId>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</GroupType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("groupTypeId")) {
					SetXmlValue(ref groupTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partyTypeId")) {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static GroupType[] GetGroupTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGroupTypes();
		}
		public static GroupType GetGroupTypeByLeadID(int leadID)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGroupTypeByLeadID(leadID);
		}
		/*
		public static GroupType GetGroupTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGroupTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertGroupType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateGroupType(this);
		}*/
		#endregion

		#region Properties
		public short GroupTypeId {
			set { groupTypeId = value; }
			get { return groupTypeId; }
		}

		public short PartyTypeId {
			set { partyTypeId = value; }
			get { return partyTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
