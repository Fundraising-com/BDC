using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Destinations: EFundraisingCRMDataObject {

		private int destinationID;
		private int webSiteID;
		private string uRL;


		public Destinations() : this(int.MinValue) { }
		public Destinations(int destinationID) : this(destinationID, int.MinValue) { }
		public Destinations(int destinationID, int webSiteID) : this(destinationID, webSiteID, null) { }
		public Destinations(int destinationID, int webSiteID, string uRL) {
			this.destinationID = destinationID;
			this.webSiteID = webSiteID;
			this.uRL = uRL;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Destinations>\r\n" +
			"	<DestinationID>" + destinationID + "</DestinationID>\r\n" +
			"	<WebSiteID>" + webSiteID + "</WebSiteID>\r\n" +
			"	<URL>" + System.Web.HttpUtility.HtmlEncode(uRL) + "</URL>\r\n" +
			"</Destinations>\r\n";
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
					SetXmlValue(ref webSiteID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("url")) {
					SetXmlValue(ref uRL, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Destinations[] GetDestinationss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDestinationss();
		}

		public static Destinations GetDestinationsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDestinationsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDestinations(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDestinations(this);
		}
		#endregion

		#region Properties
		public int DestinationID {
			set { destinationID = value; }
			get { return destinationID; }
		}

		public int WebSiteID {
			set { webSiteID = value; }
			get { return webSiteID; }
		}

		public string URL {
			set { uRL = value; }
			get { return uRL; }
		}

		#endregion
	}
}
