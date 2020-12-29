using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class GroupTypeDesc: eFundraisingStoreDataObject {

		private short groupTypeId;
		private string cultureCode;
		private string description;


		public GroupTypeDesc() : this(short.MinValue) { }
		public GroupTypeDesc(short groupTypeId) : this(groupTypeId, null) { }
		public GroupTypeDesc(short groupTypeId, string cultureCode) : this(groupTypeId, cultureCode, null) { }
		public GroupTypeDesc(short groupTypeId, string cultureCode, string description) {
			this.groupTypeId = groupTypeId;
			this.cultureCode = cultureCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<GroupTypeDesc>\r\n" +
			"	<GroupTypeId>" + groupTypeId + "</GroupTypeId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</GroupTypeDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "groupTypeId") {
					SetXmlValue(ref groupTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static GroupTypeDesc[] GetGroupTypeDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetGroupTypeDescs();
		}

		public static GroupTypeDesc GetGroupTypeDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetGroupTypeDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertGroupTypeDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateGroupTypeDesc(this);
		}
		#endregion

		#region Properties
		public short GroupTypeId {
			set { groupTypeId = value; }
			get { return groupTypeId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
