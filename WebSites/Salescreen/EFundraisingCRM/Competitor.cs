using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Competitor: EFundraisingCRMDataObject {

		private int competitorID;
		private string businessName;
		private string comments;


		public Competitor() : this(int.MinValue) { }
		public Competitor(int competitorID) : this(competitorID, null) { }
		public Competitor(int competitorID, string businessName) : this(competitorID, businessName, null) { }
		public Competitor(int competitorID, string businessName, string comments) {
			this.competitorID = competitorID;
			this.businessName = businessName;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Competitor>\r\n" +
			"	<CompetitorID>" + competitorID + "</CompetitorID>\r\n" +
			"	<BusinessName>" + System.Web.HttpUtility.HtmlEncode(businessName) + "</BusinessName>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</Competitor>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("competitorId")) {
					SetXmlValue(ref competitorID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("businessName")) {
					SetXmlValue(ref businessName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Competitor[] GetCompetitors() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCompetitors();
		}

		public static Competitor GetCompetitorByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCompetitorByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCompetitor(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCompetitor(this);
		}
		#endregion

		#region Properties
		public int CompetitorID {
			set { competitorID = value; }
			get { return competitorID; }
		}

		public string BusinessName {
			set { businessName = value; }
			get { return businessName; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
