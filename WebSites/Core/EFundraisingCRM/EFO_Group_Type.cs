using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOGroupType: EFundraisingCRMDataObject {

		private int groupTypeID;
		private string description;


		public EFOGroupType() : this(int.MinValue) { }
		public EFOGroupType(int groupTypeID) : this(groupTypeID, null) { }
		public EFOGroupType(int groupTypeID, string description) {
			this.groupTypeID = groupTypeID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOGroupType>\r\n" +
			"	<GroupTypeID>" + groupTypeID + "</GroupTypeID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</EFOGroupType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("groupTypeId")) {
					SetXmlValue(ref groupTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOGroupType[] GetEFOGroupTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOGroupTypes();
		}

		public static EFOGroupType GetEFOGroupTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOGroupTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOGroupType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOGroupType(this);
		}
		#endregion

		#region Properties
		public int GroupTypeID {
			set { groupTypeID = value; }
			get { return groupTypeID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
