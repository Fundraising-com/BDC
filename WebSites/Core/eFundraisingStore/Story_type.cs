using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class StoryType: eFundraisingStoreDataObject {

		private int storyTypeId;
		private string name;


		public StoryType() : this(int.MinValue) { }
		public StoryType(int storyTypeId) : this(storyTypeId, null) { }
		public StoryType(int storyTypeId, string name) {
			this.storyTypeId = storyTypeId;
			this.name = name;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<StoryType>\r\n" +
			"	<StoryTypeId>" + storyTypeId + "</StoryTypeId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"</StoryType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "storyTypeId") {
					SetXmlValue(ref storyTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static StoryType[] GetStoryTypes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetStoryTypes();
		}

		public static StoryType GetStoryTypeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetStoryTypeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertStoryType(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateStoryType(this);
		}
		#endregion

		#region Properties
		public int StoryTypeId {
			set { storyTypeId = value; }
			get { return storyTypeId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		#endregion
	}
}
