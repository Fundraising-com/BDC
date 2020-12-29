using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class BestTimeCall: eFundraisingStoreDataObject {

		private short bestTimeCallId;
		private string description;


		public BestTimeCall() : this(short.MinValue) { }
		public BestTimeCall(short bestTimeCallId) : this(bestTimeCallId, null) { }
		public BestTimeCall(short bestTimeCallId, string description) {
			this.bestTimeCallId = bestTimeCallId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BestTimeCall>\r\n" +
			"	<BestTimeCallId>" + bestTimeCallId + "</BestTimeCallId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</BestTimeCall>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "bestTimeCallId") {
					SetXmlValue(ref bestTimeCallId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BestTimeCall[] GetBestTimeCalls() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetBestTimeCalls();
		}

		public static BestTimeCall GetBestTimeCallByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetBestTimeCallByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertBestTimeCall(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateBestTimeCall(this);
		}
		#endregion

		#region Properties
		public short BestTimeCallId {
			set { bestTimeCallId = value; }
			get { return bestTimeCallId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
