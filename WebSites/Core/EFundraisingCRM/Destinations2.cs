using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Destinations2: EFundraisingCRMDataObject {

		private int destinationID;
		private int webSiteId;
		private string uRL;


		public Destinations2() : this(int.MinValue) { }
		public Destinations2(int destinationID) : this(destinationID, int.MinValue) { }
		public Destinations2(int destinationID, int webSiteId) : this(destinationID, webSiteId, null) { }
		public Destinations2(int destinationID, int webSiteId, string uRL) {
			this.destinationID = destinationID;
			this.webSiteId = webSiteId;
			this.uRL = uRL;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Destinations2>\r\n" +
			"	<DestinationID>" + destinationID + "</DestinationID>\r\n" +
			"	<WebSiteId>" + webSiteId + "</WebSiteId>\r\n" +
			"	<URL>" + System.Web.HttpUtility.HtmlEncode(uRL) + "</URL>\r\n" +
			"</Destinations2>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("destinationId")) {
					SetXmlValue(ref destinationID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("webSiteId")) {
					SetXmlValue(ref webSiteId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("url")) {
					SetXmlValue(ref uRL, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Destinations2[] GetDestinations2s() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDestinations2s();
		}

		public static Destinations2 GetDestinations2ByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDestinations2ByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDestinations2(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDestinations2(this);
		}
		#endregion

		#region Properties
		public int DestinationID {
			set { destinationID = value; }
			get { return destinationID; }
		}

		public int WebSiteId {
			set { webSiteId = value; }
			get { return webSiteId; }
		}

		public string URL {
			set { uRL = value; }
			get { return uRL; }
		}

		#endregion
	}
}
