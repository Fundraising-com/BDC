using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class BestTimeCall: EFundraisingCRMDataObject {

		private short bestTimeCallId;
		private string bestTimeCallDesc;


		public BestTimeCall() : this(short.MinValue) { }
		public BestTimeCall(short bestTimeCallId) : this(bestTimeCallId, null) { }
		public BestTimeCall(short bestTimeCallId, string bestTimeCallDesc) {
			this.bestTimeCallId = bestTimeCallId;
			this.bestTimeCallDesc = bestTimeCallDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BestTimeCall>\r\n" +
			"	<BestTimeCallId>" + bestTimeCallId + "</BestTimeCallId>\r\n" +
			"	<BestTimeCallDesc>" + System.Web.HttpUtility.HtmlEncode(bestTimeCallDesc) + "</BestTimeCallDesc>\r\n" +
			"</BestTimeCall>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("bestTimeCallId")) {
					SetXmlValue(ref bestTimeCallId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("bestTimeCallDesc")) {
					SetXmlValue(ref bestTimeCallDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BestTimeCall[] GetBestTimeCalls() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBestTimeCalls();
		}

		/*
		public static BestTimeCall GetBestTimeCallByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBestTimeCallByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBestTimeCall(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBestTimeCall(this);
		}*/
		#endregion

		#region Properties
		public short BestTimeCallId {
			set { bestTimeCallId = value; }
			get { return bestTimeCallId; }
		}

		public string BestTimeCallDesc {
			set { bestTimeCallDesc = value; }
			get { return bestTimeCallDesc; }
		}

		#endregion
	}
}
