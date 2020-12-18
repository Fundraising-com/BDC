using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LocalSponsorActivity: EFundraisingCRMDataObject {

		private int localSponsorActivityID;
		private int localSponsorActivityTypeID;
		private int salesID;
		private int sponsorConsultantID;
		private DateTime localSponsorActivityDate;
		private DateTime completedDate;
		private string comments;
		private int brandID;
		private int localSponsorID;


		public LocalSponsorActivity() : this(int.MinValue) { }
		public LocalSponsorActivity(int localSponsorActivityID) : this(localSponsorActivityID, int.MinValue) { }
		public LocalSponsorActivity(int localSponsorActivityID, int localSponsorActivityTypeID) : this(localSponsorActivityID, localSponsorActivityTypeID, int.MinValue) { }
		public LocalSponsorActivity(int localSponsorActivityID, int localSponsorActivityTypeID, int salesID) : this(localSponsorActivityID, localSponsorActivityTypeID, salesID, int.MinValue) { }
		public LocalSponsorActivity(int localSponsorActivityID, int localSponsorActivityTypeID, int salesID, int sponsorConsultantID) : this(localSponsorActivityID, localSponsorActivityTypeID, salesID, sponsorConsultantID, DateTime.MinValue) { }
		public LocalSponsorActivity(int localSponsorActivityID, int localSponsorActivityTypeID, int salesID, int sponsorConsultantID, DateTime localSponsorActivityDate) : this(localSponsorActivityID, localSponsorActivityTypeID, salesID, sponsorConsultantID, localSponsorActivityDate, DateTime.MinValue) { }
		public LocalSponsorActivity(int localSponsorActivityID, int localSponsorActivityTypeID, int salesID, int sponsorConsultantID, DateTime localSponsorActivityDate, DateTime completedDate) : this(localSponsorActivityID, localSponsorActivityTypeID, salesID, sponsorConsultantID, localSponsorActivityDate, completedDate, null) { }
		public LocalSponsorActivity(int localSponsorActivityID, int localSponsorActivityTypeID, int salesID, int sponsorConsultantID, DateTime localSponsorActivityDate, DateTime completedDate, string comments) : this(localSponsorActivityID, localSponsorActivityTypeID, salesID, sponsorConsultantID, localSponsorActivityDate, completedDate, comments, int.MinValue) { }
		public LocalSponsorActivity(int localSponsorActivityID, int localSponsorActivityTypeID, int salesID, int sponsorConsultantID, DateTime localSponsorActivityDate, DateTime completedDate, string comments, int brandID) : this(localSponsorActivityID, localSponsorActivityTypeID, salesID, sponsorConsultantID, localSponsorActivityDate, completedDate, comments, brandID, int.MinValue) { }
		public LocalSponsorActivity(int localSponsorActivityID, int localSponsorActivityTypeID, int salesID, int sponsorConsultantID, DateTime localSponsorActivityDate, DateTime completedDate, string comments, int brandID, int localSponsorID) {
			this.localSponsorActivityID = localSponsorActivityID;
			this.localSponsorActivityTypeID = localSponsorActivityTypeID;
			this.salesID = salesID;
			this.sponsorConsultantID = sponsorConsultantID;
			this.localSponsorActivityDate = localSponsorActivityDate;
			this.completedDate = completedDate;
			this.comments = comments;
			this.brandID = brandID;
			this.localSponsorID = localSponsorID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LocalSponsorActivity>\r\n" +
			"	<LocalSponsorActivityID>" + localSponsorActivityID + "</LocalSponsorActivityID>\r\n" +
			"	<LocalSponsorActivityTypeID>" + localSponsorActivityTypeID + "</LocalSponsorActivityTypeID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<SponsorConsultantID>" + sponsorConsultantID + "</SponsorConsultantID>\r\n" +
			"	<LocalSponsorActivityDate>" + localSponsorActivityDate + "</LocalSponsorActivityDate>\r\n" +
			"	<CompletedDate>" + completedDate + "</CompletedDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<BrandID>" + brandID + "</BrandID>\r\n" +
			"	<LocalSponsorID>" + localSponsorID + "</LocalSponsorID>\r\n" +
			"</LocalSponsorActivity>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorActivityId")) {
					SetXmlValue(ref localSponsorActivityID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorActivityTypeId")) {
					SetXmlValue(ref localSponsorActivityTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sponsorConsultantId")) {
					SetXmlValue(ref sponsorConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorActivityDate")) {
					SetXmlValue(ref localSponsorActivityDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("completedDate")) {
					SetXmlValue(ref completedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("brandId")) {
					SetXmlValue(ref brandID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorId")) {
					SetXmlValue(ref localSponsorID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LocalSponsorActivity[] GetLocalSponsorActivitys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorActivitys();
		}

		public static LocalSponsorActivity GetLocalSponsorActivityByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorActivityByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLocalSponsorActivity(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLocalSponsorActivity(this);
		}
		#endregion

		#region Properties
		public int LocalSponsorActivityID {
			set { localSponsorActivityID = value; }
			get { return localSponsorActivityID; }
		}

		public int LocalSponsorActivityTypeID {
			set { localSponsorActivityTypeID = value; }
			get { return localSponsorActivityTypeID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int SponsorConsultantID {
			set { sponsorConsultantID = value; }
			get { return sponsorConsultantID; }
		}

		public DateTime LocalSponsorActivityDate {
			set { localSponsorActivityDate = value; }
			get { return localSponsorActivityDate; }
		}

		public DateTime CompletedDate {
			set { completedDate = value; }
			get { return completedDate; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public int BrandID {
			set { brandID = value; }
			get { return brandID; }
		}

		public int LocalSponsorID {
			set { localSponsorID = value; }
			get { return localSponsorID; }
		}

		#endregion
	}
}
