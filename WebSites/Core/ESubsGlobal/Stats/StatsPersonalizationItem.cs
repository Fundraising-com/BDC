using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Stats {


	public class StatsPersonalizationItem: DataObject {

		private int statsPersonalizationItemId;
		private string description;
		private DateTime createDate;


		public StatsPersonalizationItem() : this(int.MinValue) { }
		public StatsPersonalizationItem(int statsPersonalizationItemId) : this(statsPersonalizationItemId, null) { }
		public StatsPersonalizationItem(int statsPersonalizationItemId, string description) : this(statsPersonalizationItemId, description, DateTime.MinValue) { }
		public StatsPersonalizationItem(int statsPersonalizationItemId, string description, DateTime createDate) {
			this.statsPersonalizationItemId = statsPersonalizationItemId;
			this.description = description;
			this.createDate = createDate;
		}

		#region Static Data

		public static StatsPersonalizationItem HeaderTitle1 {
			get { return new StatsPersonalizationItem(1, "Header Title 1", DateTime.MinValue); }
		}

		public static StatsPersonalizationItem HeaderTitle2 {
			get { return new StatsPersonalizationItem(2, "Header Title 2", DateTime.MinValue); }
		}

		public static StatsPersonalizationItem MessageBody {
			get { return new StatsPersonalizationItem(3, "Message Body", DateTime.MinValue); }
		}

		public static StatsPersonalizationItem Goal {
			get { return new StatsPersonalizationItem(4, "Goal", DateTime.MinValue); }
		}

		public static StatsPersonalizationItem BackgroundColor {
			get { return new StatsPersonalizationItem(5, "Background Color", DateTime.MinValue); }
		}

		public static StatsPersonalizationItem ForegroundColor {
			get { return new StatsPersonalizationItem(6, "Foreground Color", DateTime.MinValue); }
		}

		public static StatsPersonalizationItem ImageFromLibrary {
			get { return new StatsPersonalizationItem(7, "Image From Library", DateTime.MinValue); }
		}

		public static StatsPersonalizationItem ImageUploaded {
			get { return new StatsPersonalizationItem(8, "Image Uploaded", DateTime.MinValue); }
		}

		public static StatsPersonalizationItem EmailSubject {
			get { return new StatsPersonalizationItem(9, "Email Subject", DateTime.MinValue); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<StatsPersonalizationItem>\r\n" +
				"	<StatsPersonalizationItemId>" + statsPersonalizationItemId + "</StatsPersonalizationItemId>\r\n" +
				"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</StatsPersonalizationItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "statsPersonalizationItemId") {
					SetXmlValue(ref statsPersonalizationItemId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static StatsPersonalizationItem[] GetStatsPersonalizationItems() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStatsPersonalizationItems();
		}

		public static StatsPersonalizationItem GetStatsPersonalizationItemByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStatsPersonalizationItemByID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertStatsPersonalizationItem(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateStatsPersonalizationItem(this);
		}
		#endregion

		#region Properties
		public int StatsPersonalizationItemId {
			set { statsPersonalizationItemId = value; }
			get { return statsPersonalizationItemId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
