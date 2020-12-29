using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Session: eFundraisingStoreDataObject {

		private int sessionId;
		private int visitorsLogId;
		private DateTime dateCreated;


		public Session() : this(int.MinValue) { }
		public Session(int sessionId) : this(sessionId, int.MinValue) { }
		public Session(int sessionId, int visitorsLogId) : this(sessionId, visitorsLogId, DateTime.MinValue) { }
		public Session(int sessionId, int visitorsLogId, DateTime dateCreated) {
			this.sessionId = sessionId;
			this.visitorsLogId = visitorsLogId;
			this.dateCreated = dateCreated;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Session>\r\n" +
			"	<SessionId>" + sessionId + "</SessionId>\r\n" +
			"	<VisitorsLogId>" + visitorsLogId + "</VisitorsLogId>\r\n" +
			"	<DateCreated>" + dateCreated + "</DateCreated>\r\n" +
			"</Session>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "sessionId") {
					SetXmlValue(ref sessionId, node.InnerText);
				}
				if(node.Name.ToLower() == "visitorsLogId") {
					SetXmlValue(ref visitorsLogId, node.InnerText);
				}
				if(node.Name.ToLower() == "dateCreated") {
					SetXmlValue(ref dateCreated, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Session[] GetSessions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSessions();
		}

		public static Session GetSessionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSessionByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertSession(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateSession(this);
		}
		#endregion

		#region Properties
		public int SessionId {
			set { sessionId = value; }
			get { return sessionId; }
		}

		public int VisitorsLogId {
			set { visitorsLogId = value; }
			get { return visitorsLogId; }
		}

		public DateTime DateCreated {
			set { dateCreated = value; }
			get { return dateCreated; }
		}

		#endregion
	}
}
