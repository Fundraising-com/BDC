using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CompetitorAdvertisingSupport: EFundraisingCRMDataObject {

		private int advertisingSupportID;
		private int competitorAdvertisingID;


		public CompetitorAdvertisingSupport() : this(int.MinValue) { }
		public CompetitorAdvertisingSupport(int advertisingSupportID) : this(advertisingSupportID, int.MinValue) { }
		public CompetitorAdvertisingSupport(int advertisingSupportID, int competitorAdvertisingID) {
			this.advertisingSupportID = advertisingSupportID;
			this.competitorAdvertisingID = competitorAdvertisingID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CompetitorAdvertisingSupport>\r\n" +
			"	<AdvertisingSupportID>" + advertisingSupportID + "</AdvertisingSupportID>\r\n" +
			"	<CompetitorAdvertisingID>" + competitorAdvertisingID + "</CompetitorAdvertisingID>\r\n" +
			"</CompetitorAdvertisingSupport>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportId")) {
					SetXmlValue(ref advertisingSupportID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("competitorAdvertisingId")) {
					SetXmlValue(ref competitorAdvertisingID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CompetitorAdvertisingSupport[] GetCompetitorAdvertisingSupports() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCompetitorAdvertisingSupports();
		}

		public static CompetitorAdvertisingSupport GetCompetitorAdvertisingSupportByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCompetitorAdvertisingSupportByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCompetitorAdvertisingSupport(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCompetitorAdvertisingSupport(this);
		}
		#endregion

		#region Properties
		public int AdvertisingSupportID {
			set { advertisingSupportID = value; }
			get { return advertisingSupportID; }
		}

		public int CompetitorAdvertisingID {
			set { competitorAdvertisingID = value; }
			get { return competitorAdvertisingID; }
		}

		#endregion
	}
}
