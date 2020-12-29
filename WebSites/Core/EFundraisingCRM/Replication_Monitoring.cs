using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ReplicationMonitoring: EFundraisingCRMDataObject {

		private int replicationID;
		private string msg;


		public ReplicationMonitoring() : this(int.MinValue) { }
		public ReplicationMonitoring(int replicationID) : this(replicationID, null) { }
		public ReplicationMonitoring(int replicationID, string msg) {
			this.replicationID = replicationID;
			this.msg = msg;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ReplicationMonitoring>\r\n" +
			"	<ReplicationID>" + replicationID + "</ReplicationID>\r\n" +
			"	<Msg>" + System.Web.HttpUtility.HtmlEncode(msg) + "</Msg>\r\n" +
			"</ReplicationMonitoring>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("replicationId")) {
					SetXmlValue(ref replicationID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("msg")) {
					SetXmlValue(ref msg, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ReplicationMonitoring[] GetReplicationMonitorings() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReplicationMonitorings();
		}

		public static ReplicationMonitoring GetReplicationMonitoringByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReplicationMonitoringByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReplicationMonitoring(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReplicationMonitoring(this);
		}
		#endregion

		#region Properties
		public int ReplicationID {
			set { replicationID = value; }
			get { return replicationID; }
		}

		public string Msg {
			set { msg = value; }
			get { return msg; }
		}

		#endregion
	}
}
