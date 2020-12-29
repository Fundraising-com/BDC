using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CompetitorAdvertising: EFundraisingCRMDataObject {

		private int competitorAdvertisingID;
		private int competitorID;
		private string description;
		private string publicityDuration;
		private string comments;


		public CompetitorAdvertising() : this(int.MinValue) { }
		public CompetitorAdvertising(int competitorAdvertisingID) : this(competitorAdvertisingID, int.MinValue) { }
		public CompetitorAdvertising(int competitorAdvertisingID, int competitorID) : this(competitorAdvertisingID, competitorID, null) { }
		public CompetitorAdvertising(int competitorAdvertisingID, int competitorID, string description) : this(competitorAdvertisingID, competitorID, description, null) { }
		public CompetitorAdvertising(int competitorAdvertisingID, int competitorID, string description, string publicityDuration) : this(competitorAdvertisingID, competitorID, description, publicityDuration, null) { }
		public CompetitorAdvertising(int competitorAdvertisingID, int competitorID, string description, string publicityDuration, string comments) {
			this.competitorAdvertisingID = competitorAdvertisingID;
			this.competitorID = competitorID;
			this.description = description;
			this.publicityDuration = publicityDuration;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CompetitorAdvertising>\r\n" +
			"	<CompetitorAdvertisingID>" + competitorAdvertisingID + "</CompetitorAdvertisingID>\r\n" +
			"	<CompetitorID>" + competitorID + "</CompetitorID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<PublicityDuration>" + System.Web.HttpUtility.HtmlEncode(publicityDuration) + "</PublicityDuration>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</CompetitorAdvertising>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("competitorAdvertisingId")) {
					SetXmlValue(ref competitorAdvertisingID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("competitorId")) {
					SetXmlValue(ref competitorID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("publicityDuration")) {
					SetXmlValue(ref publicityDuration, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CompetitorAdvertising[] GetCompetitorAdvertisings() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCompetitorAdvertisings();
		}

		public static CompetitorAdvertising GetCompetitorAdvertisingByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCompetitorAdvertisingByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCompetitorAdvertising(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCompetitorAdvertising(this);
		}
		#endregion

		#region Properties
		public int CompetitorAdvertisingID {
			set { competitorAdvertisingID = value; }
			get { return competitorAdvertisingID; }
		}

		public int CompetitorID {
			set { competitorID = value; }
			get { return competitorID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string PublicityDuration {
			set { publicityDuration = value; }
			get { return publicityDuration; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
