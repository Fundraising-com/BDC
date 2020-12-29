using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class TargetedMarket: EFundraisingCRMDataObject {

		private int targetedMarketID;
		private int targetedMarketTypeID;
		private int advertisingSupportID;
		private int targetMarketTypeID;
		private int seasoner;
		private string ageRange;
		private string educationLevel;
		private string description;
		private string comments;


		public TargetedMarket() : this(int.MinValue) { }
		public TargetedMarket(int targetedMarketID) : this(targetedMarketID, int.MinValue) { }
		public TargetedMarket(int targetedMarketID, int targetedMarketTypeID) : this(targetedMarketID, targetedMarketTypeID, int.MinValue) { }
		public TargetedMarket(int targetedMarketID, int targetedMarketTypeID, int advertisingSupportID) : this(targetedMarketID, targetedMarketTypeID, advertisingSupportID, int.MinValue) { }
		public TargetedMarket(int targetedMarketID, int targetedMarketTypeID, int advertisingSupportID, int targetMarketTypeID) : this(targetedMarketID, targetedMarketTypeID, advertisingSupportID, targetMarketTypeID, int.MinValue) { }
		public TargetedMarket(int targetedMarketID, int targetedMarketTypeID, int advertisingSupportID, int targetMarketTypeID, int seasoner) : this(targetedMarketID, targetedMarketTypeID, advertisingSupportID, targetMarketTypeID, seasoner, null) { }
		public TargetedMarket(int targetedMarketID, int targetedMarketTypeID, int advertisingSupportID, int targetMarketTypeID, int seasoner, string ageRange) : this(targetedMarketID, targetedMarketTypeID, advertisingSupportID, targetMarketTypeID, seasoner, ageRange, null) { }
		public TargetedMarket(int targetedMarketID, int targetedMarketTypeID, int advertisingSupportID, int targetMarketTypeID, int seasoner, string ageRange, string educationLevel) : this(targetedMarketID, targetedMarketTypeID, advertisingSupportID, targetMarketTypeID, seasoner, ageRange, educationLevel, null) { }
		public TargetedMarket(int targetedMarketID, int targetedMarketTypeID, int advertisingSupportID, int targetMarketTypeID, int seasoner, string ageRange, string educationLevel, string description) : this(targetedMarketID, targetedMarketTypeID, advertisingSupportID, targetMarketTypeID, seasoner, ageRange, educationLevel, description, null) { }
		public TargetedMarket(int targetedMarketID, int targetedMarketTypeID, int advertisingSupportID, int targetMarketTypeID, int seasoner, string ageRange, string educationLevel, string description, string comments) {
			this.targetedMarketID = targetedMarketID;
			this.targetedMarketTypeID = targetedMarketTypeID;
			this.advertisingSupportID = advertisingSupportID;
			this.targetMarketTypeID = targetMarketTypeID;
			this.seasoner = seasoner;
			this.ageRange = ageRange;
			this.educationLevel = educationLevel;
			this.description = description;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TargetedMarket>\r\n" +
			"	<TargetedMarketID>" + targetedMarketID + "</TargetedMarketID>\r\n" +
			"	<TargetedMarketTypeID>" + targetedMarketTypeID + "</TargetedMarketTypeID>\r\n" +
			"	<AdvertisingSupportID>" + advertisingSupportID + "</AdvertisingSupportID>\r\n" +
			"	<TargetMarketTypeID>" + targetMarketTypeID + "</TargetMarketTypeID>\r\n" +
			"	<Seasoner>" + seasoner + "</Seasoner>\r\n" +
			"	<AgeRange>" + System.Web.HttpUtility.HtmlEncode(ageRange) + "</AgeRange>\r\n" +
			"	<EducationLevel>" + System.Web.HttpUtility.HtmlEncode(educationLevel) + "</EducationLevel>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</TargetedMarket>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("targetedMarketId")) {
					SetXmlValue(ref targetedMarketID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("targetedMarketTypeId")) {
					SetXmlValue(ref targetedMarketTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportId")) {
					SetXmlValue(ref advertisingSupportID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("targetMarketTypeId")) {
					SetXmlValue(ref targetMarketTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("seasoner")) {
					SetXmlValue(ref seasoner, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ageRange")) {
					SetXmlValue(ref ageRange, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("educationLevel")) {
					SetXmlValue(ref educationLevel, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TargetedMarket[] GetTargetedMarkets() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTargetedMarkets();
		}

		public static TargetedMarket GetTargetedMarketByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTargetedMarketByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTargetedMarket(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTargetedMarket(this);
		}
		#endregion

		#region Properties
		public int TargetedMarketID {
			set { targetedMarketID = value; }
			get { return targetedMarketID; }
		}

		public int TargetedMarketTypeID {
			set { targetedMarketTypeID = value; }
			get { return targetedMarketTypeID; }
		}

		public int AdvertisingSupportID {
			set { advertisingSupportID = value; }
			get { return advertisingSupportID; }
		}

		public int TargetMarketTypeID {
			set { targetMarketTypeID = value; }
			get { return targetMarketTypeID; }
		}

		public int Seasoner {
			set { seasoner = value; }
			get { return seasoner; }
		}

		public string AgeRange {
			set { ageRange = value; }
			get { return ageRange; }
		}

		public string EducationLevel {
			set { educationLevel = value; }
			get { return educationLevel; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
