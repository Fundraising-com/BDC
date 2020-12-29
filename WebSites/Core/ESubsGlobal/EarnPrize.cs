using System;
using System.Collections.Generic;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal {

	public class EarnedPrize: DataObject {

        private int prizeItemId = int.MinValue;
        private int eventParticipationId = int.MinValue;
		private DateTime createDate;

		public EarnedPrize() : this(int.MinValue) { }
		public EarnedPrize(int prizeItemId) : this(prizeItemId, int.MinValue) { }
		public EarnedPrize(int prizeItemId, int eventParticipationId) : this(prizeItemId, eventParticipationId, DateTime.MinValue) { }
		public EarnedPrize(int prizeItemId, int eventParticipationId, DateTime createDate) {
			this.prizeItemId = prizeItemId;
			this.eventParticipationId = eventParticipationId;
			this.createDate = createDate;
		}


		#region XML Methods
		
		#region Save XML
		public override string GenerateXML() {
			return "<EarnedPrize>\r\n" +
				"	<PrizeItemId>" + prizeItemId + "</PrizeItemId>\r\n" +
				"	<EventParticipationId>" + eventParticipationId + "</EventParticipationId>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</EarnedPrize>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "prizeItemId") {
					SetXmlValue(ref prizeItemId, node.InnerText);
				}
				if(node.Name.ToLower() == "eventParticipationId") {
					SetXmlValue(ref eventParticipationId, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EarnedPrize[] GetEarnedPrizes() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEarnedPrizes();
		}

		public static EarnedPrize GetEarnedPrizeByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEarnedPrizeByID(id);
		}

		public static EarnedPrize GetEarnedPrizeByEventParticipationID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetEarnedPrizeByEventParticipationID(id);
		}

        public static List<EarnedPrize> GetEarnedPrizesByEventParticipationID(int id)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetEarnedPrizesByEventParticipationID(id);
        }

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertEarnedPrize(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateEarnedPrize(this);
		}
		#endregion

		#region Properties
		public int PrizeItemId {
			set { prizeItemId = value; }
			get { return prizeItemId; }
		}

		public int EventParticipationId {
			set { eventParticipationId = value; }
			get { return eventParticipationId; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
