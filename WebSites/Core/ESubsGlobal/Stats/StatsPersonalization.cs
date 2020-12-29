using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Stats {

	public class StatsPersonalization: DataObject {

		private int statsPersonalizationId;
		private int eventParticipationId;
		private int statsPersonalizationItemId;
		private int statsPersonalizationSectionId;
		private DateTime createDate;


		public StatsPersonalization() : this(int.MinValue) { }
		public StatsPersonalization(int statsPersonalizationId) : this(statsPersonalizationId, int.MinValue) { }
		public StatsPersonalization(int statsPersonalizationId, int eventParticipationId) : this(statsPersonalizationId, eventParticipationId, int.MinValue) { }
		public StatsPersonalization(int statsPersonalizationId, int eventParticipationId, int statsPersonalizationItemId) : this(statsPersonalizationId, eventParticipationId, statsPersonalizationItemId, int.MinValue) { }
		public StatsPersonalization(int statsPersonalizationId, int eventParticipationId, int statsPersonalizationItemId, int statsPersonalizationSectionId) : this(statsPersonalizationId, eventParticipationId, statsPersonalizationItemId, statsPersonalizationSectionId, DateTime.MinValue) { }
		public StatsPersonalization(int statsPersonalizationId, int eventParticipationId, int statsPersonalizationItemId, int statsPersonalizationSectionId, DateTime createDate) {
			this.statsPersonalizationId = statsPersonalizationId;
			this.eventParticipationId = eventParticipationId;
			this.statsPersonalizationItemId = statsPersonalizationItemId;
			this.statsPersonalizationSectionId = statsPersonalizationSectionId;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<StatsPersonalization>\r\n" +
				"	<StatsPersonalizationId>" + statsPersonalizationId + "</StatsPersonalizationId>\r\n" +
				"	<EventParticipationId>" + eventParticipationId + "</EventParticipationId>\r\n" +
				"	<StatsPersonalizationItemId>" + statsPersonalizationItemId + "</StatsPersonalizationItemId>\r\n" +
				"	<StatsPersonalizationSectionId>" + statsPersonalizationSectionId + "</StatsPersonalizationSectionId>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</StatsPersonalization>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "statsPersonalizationId") {
					SetXmlValue(ref statsPersonalizationId, node.InnerText);
				}
				if(node.Name.ToLower() == "eventParticipationId") {
					SetXmlValue(ref eventParticipationId, node.InnerText);
				}
				if(node.Name.ToLower() == "statsPersonalizationItemId") {
					SetXmlValue(ref statsPersonalizationItemId, node.InnerText);
				}
				if(node.Name.ToLower() == "statsPersonalizationSectionId") {
					SetXmlValue(ref statsPersonalizationSectionId, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static StatsPersonalization[] GetStatsPersonalizations() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStatsPersonalizations();
		}

		public static StatsPersonalization GetStatsPersonalizationByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStatsPersonalizationByID(id);
		}

		public static int InsertPersonalizations(StatsPersonalization[] statsPers)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertStatsPersonalization(statsPers);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertStatsPersonalization(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateStatsPersonalization(this);
		}
		#endregion

		#region Properties
		public int StatsPersonalizationId {
			set { statsPersonalizationId = value; }
			get { return statsPersonalizationId; }
		}

		public int EventParticipationId {
			set { eventParticipationId = value; }
			get { return eventParticipationId; }
		}

		public int StatsPersonalizationItemId {
			set { statsPersonalizationItemId = value; }
			get { return statsPersonalizationItemId; }
		}

		public int StatsPersonalizationSectionId {
			set { statsPersonalizationSectionId = value; }
			get { return statsPersonalizationSectionId; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
