using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Story: eFundraisingStoreDataObject {

		private int storyId;
		private int storyTypeId;
		private int groupTypeId;
		private string storyText;


		public Story() : this(int.MinValue) { }
		public Story(int storyId) : this(storyId, int.MinValue) { }
		public Story(int storyId, int storyTypeId) : this(storyId, storyTypeId, int.MinValue) { }
		public Story(int storyId, int storyTypeId, int groupTypeId) : this(storyId, storyTypeId, groupTypeId, null) { }
		public Story(int storyId, int storyTypeId, int groupTypeId, string storyText) {
			this.storyId = storyId;
			this.storyTypeId = storyTypeId;
			this.groupTypeId = groupTypeId;
			this.storyText = storyText;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Story>\r\n" +
			"	<StoryId>" + storyId + "</StoryId>\r\n" +
			"	<StoryTypeId>" + storyTypeId + "</StoryTypeId>\r\n" +
			"	<GroupTypeId>" + groupTypeId + "</GroupTypeId>\r\n" +
			"	<StoryText>" + System.Web.HttpUtility.HtmlEncode(storyText) + "</StoryText>\r\n" +
			"</Story>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "storyId") {
					SetXmlValue(ref storyId, node.InnerText);
				}
				if(node.Name.ToLower() == "storyTypeId") {
					SetXmlValue(ref storyTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "groupTypeId") {
					SetXmlValue(ref groupTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "storyText") {
					SetXmlValue(ref storyText, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Story[] GetStorys() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetStorys();
		}

		public static Story GetStoryByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetStoryByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertStory(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateStory(this);
		}
		#endregion

		#region Properties
		public int StoryId {
			set { storyId = value; }
			get { return storyId; }
		}

		public int StoryTypeId {
			set { storyTypeId = value; }
			get { return storyTypeId; }
		}

		public int GroupTypeId {
			set { groupTypeId = value; }
			get { return groupTypeId; }
		}

		public string StoryText {
			set { storyText = value; }
			get { return storyText; }
		}

		#endregion
	}
}
