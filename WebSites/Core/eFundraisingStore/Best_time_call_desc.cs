using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class BestTimeCallDesc: eFundraisingStoreDataObject {

		private short bestTimeCallId;
		private string cultureCode;
		private string description;


		public BestTimeCallDesc() : this(short.MinValue) { }
		public BestTimeCallDesc(short bestTimeCallId) : this(bestTimeCallId, null) { }
		public BestTimeCallDesc(short bestTimeCallId, string cultureCode) : this(bestTimeCallId, cultureCode, null) { }
		public BestTimeCallDesc(short bestTimeCallId, string cultureCode, string description) {
			this.bestTimeCallId = bestTimeCallId;
			this.cultureCode = cultureCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BestTimeCallDesc>\r\n" +
			"	<BestTimeCallId>" + bestTimeCallId + "</BestTimeCallId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</BestTimeCallDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "bestTimeCallId") {
					SetXmlValue(ref bestTimeCallId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BestTimeCallDesc[] GetBestTimeCallDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetBestTimeCallDescs();
		}

		public static BestTimeCallDesc GetBestTimeCallDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetBestTimeCallDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertBestTimeCallDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateBestTimeCallDesc(this);
		}
		#endregion

		#region Properties
		public short BestTimeCallId {
			set { bestTimeCallId = value; }
			get { return bestTimeCallId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
