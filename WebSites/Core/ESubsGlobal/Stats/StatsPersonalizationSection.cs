using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Stats {

	
	public class StatsPersonalizationSection: DataObject {

		private int statsPersonalizationSectionId;
		private string description;
		private DateTime createDate;


		public StatsPersonalizationSection() : this(int.MinValue) { }
		public StatsPersonalizationSection(int statsPersonalizationSectionId) : this(statsPersonalizationSectionId, null) { }
		public StatsPersonalizationSection(int statsPersonalizationSectionId, string description) : this(statsPersonalizationSectionId, description, DateTime.MinValue) { }
		public StatsPersonalizationSection(int statsPersonalizationSectionId, string description, DateTime createDate) {
			this.statsPersonalizationSectionId = statsPersonalizationSectionId;
			this.description = description;
			this.createDate = createDate;
		}

		#region Static Data

		public static StatsPersonalizationSection SponsorWizardPersonalization {
			get { return new StatsPersonalizationSection(1, "Sponsor Wizard Personalization", DateTime.MinValue); }
		}

		public static StatsPersonalizationSection SponsorEditPersonalization {
			get { return new StatsPersonalizationSection(2, "Sponsor Edit Personalization", DateTime.MinValue); }
		}

		public static StatsPersonalizationSection ParticipantEditPersonalization {
			get { return new StatsPersonalizationSection(3, "Participant Edit Personalization", DateTime.MinValue); }
		}

		public static StatsPersonalizationSection XFactorSponsorEditPersonalization {
			get { return new StatsPersonalizationSection(4, "XFactor Sponsor Edit Personalization", DateTime.MinValue); }
		}

		public static StatsPersonalizationSection SponsorWizardEmailPersonalization {
			get { return new StatsPersonalizationSection(5, "Sponsor Wizard Email Personalization", DateTime.MinValue); }
		}

        public static StatsPersonalizationSection ParticipantWizardEmailPersonalization
        {
            get { return new StatsPersonalizationSection(15, "Participant Wizard Email Personalization", DateTime.MinValue); }
        }

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<StatsPersonalizationSection>\r\n" +
				"	<StatPersonalizationSectionId>" + statsPersonalizationSectionId + "</StatPersonalizationSectionId>\r\n" +
				"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</StatsPersonalizationSection>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "statsPersonalizationSectionId") {
					SetXmlValue(ref statsPersonalizationSectionId, node.InnerText);
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
		public static StatsPersonalizationSection[] GetStatsPersonalizationSections() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStatsPersonalizationSections();
		}

		public static StatsPersonalizationSection GetStatsPersonalizationSectionByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStatsPersonalizationSectionByID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertStatsPersonalizationSection(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateStatsPersonalizationSection(this);
		}
		#endregion

		#region Properties
		public int StatsPersonalizationSectionId {
			set { statsPersonalizationSectionId = value; }
			get { return statsPersonalizationSectionId; }
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
