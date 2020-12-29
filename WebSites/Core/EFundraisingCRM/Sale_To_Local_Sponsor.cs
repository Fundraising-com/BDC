using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SaleToLocalSponsor: EFundraisingCRMDataObject {

		private int salesID;
		private int brandID;
		private int localSponsorID;
		private DateTime assignedDate;
		private string comments;


		public SaleToLocalSponsor() : this(int.MinValue) { }
		public SaleToLocalSponsor(int salesID) : this(salesID, int.MinValue) { }
		public SaleToLocalSponsor(int salesID, int brandID) : this(salesID, brandID, int.MinValue) { }
		public SaleToLocalSponsor(int salesID, int brandID, int localSponsorID) : this(salesID, brandID, localSponsorID, DateTime.MinValue) { }
		public SaleToLocalSponsor(int salesID, int brandID, int localSponsorID, DateTime assignedDate) : this(salesID, brandID, localSponsorID, assignedDate, null) { }
		public SaleToLocalSponsor(int salesID, int brandID, int localSponsorID, DateTime assignedDate, string comments) {
			this.salesID = salesID;
			this.brandID = brandID;
			this.localSponsorID = localSponsorID;
			this.assignedDate = assignedDate;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SaleToLocalSponsor>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<BrandID>" + brandID + "</BrandID>\r\n" +
			"	<LocalSponsorID>" + localSponsorID + "</LocalSponsorID>\r\n" +
			"	<AssignedDate>" + assignedDate + "</AssignedDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</SaleToLocalSponsor>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("brandId")) {
					SetXmlValue(ref brandID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorId")) {
					SetXmlValue(ref localSponsorID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("assignedDate")) {
					SetXmlValue(ref assignedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SaleToLocalSponsor[] GetSaleToLocalSponsors() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleToLocalSponsors();
		}

		public static SaleToLocalSponsor GetSaleToLocalSponsorByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleToLocalSponsorByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSaleToLocalSponsor(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSaleToLocalSponsor(this);
		}
		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int BrandID {
			set { brandID = value; }
			get { return brandID; }
		}

		public int LocalSponsorID {
			set { localSponsorID = value; }
			get { return localSponsorID; }
		}

		public DateTime AssignedDate {
			set { assignedDate = value; }
			get { return assignedDate; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
