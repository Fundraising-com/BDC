using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SalesItemCouponSheet: EFundraisingCRMDataObject {

		private int couponSheetID;
		private int salesID;
		private int salesItemNo;
		private DateTime dateAssigned;
		private int sheetPerBooklet;
		private int sponsorConsultantID;
		private int brandID;
		private int localSponsorID;


		public SalesItemCouponSheet() : this(int.MinValue) { }
		public SalesItemCouponSheet(int couponSheetID) : this(couponSheetID, int.MinValue) { }
		public SalesItemCouponSheet(int couponSheetID, int salesID) : this(couponSheetID, salesID, int.MinValue) { }
		public SalesItemCouponSheet(int couponSheetID, int salesID, int salesItemNo) : this(couponSheetID, salesID, salesItemNo, DateTime.MinValue) { }
		public SalesItemCouponSheet(int couponSheetID, int salesID, int salesItemNo, DateTime dateAssigned) : this(couponSheetID, salesID, salesItemNo, dateAssigned, int.MinValue) { }
		public SalesItemCouponSheet(int couponSheetID, int salesID, int salesItemNo, DateTime dateAssigned, int sheetPerBooklet) : this(couponSheetID, salesID, salesItemNo, dateAssigned, sheetPerBooklet, int.MinValue) { }
		public SalesItemCouponSheet(int couponSheetID, int salesID, int salesItemNo, DateTime dateAssigned, int sheetPerBooklet, int sponsorConsultantID) : this(couponSheetID, salesID, salesItemNo, dateAssigned, sheetPerBooklet, sponsorConsultantID, int.MinValue) { }
		public SalesItemCouponSheet(int couponSheetID, int salesID, int salesItemNo, DateTime dateAssigned, int sheetPerBooklet, int sponsorConsultantID, int brandID) : this(couponSheetID, salesID, salesItemNo, dateAssigned, sheetPerBooklet, sponsorConsultantID, brandID, int.MinValue) { }
		public SalesItemCouponSheet(int couponSheetID, int salesID, int salesItemNo, DateTime dateAssigned, int sheetPerBooklet, int sponsorConsultantID, int brandID, int localSponsorID) {
			this.couponSheetID = couponSheetID;
			this.salesID = salesID;
			this.salesItemNo = salesItemNo;
			this.dateAssigned = dateAssigned;
			this.sheetPerBooklet = sheetPerBooklet;
			this.sponsorConsultantID = sponsorConsultantID;
			this.brandID = brandID;
			this.localSponsorID = localSponsorID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalesItemCouponSheet>\r\n" +
			"	<CouponSheetID>" + couponSheetID + "</CouponSheetID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<SalesItemNo>" + salesItemNo + "</SalesItemNo>\r\n" +
			"	<DateAssigned>" + dateAssigned + "</DateAssigned>\r\n" +
			"	<SheetPerBooklet>" + sheetPerBooklet + "</SheetPerBooklet>\r\n" +
			"	<SponsorConsultantID>" + sponsorConsultantID + "</SponsorConsultantID>\r\n" +
			"	<BrandID>" + brandID + "</BrandID>\r\n" +
			"	<LocalSponsorID>" + localSponsorID + "</LocalSponsorID>\r\n" +
			"</SalesItemCouponSheet>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("couponSheetId")) {
					SetXmlValue(ref couponSheetID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesItemNo")) {
					SetXmlValue(ref salesItemNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dateAssigned")) {
					SetXmlValue(ref dateAssigned, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sheetPerBooklet")) {
					SetXmlValue(ref sheetPerBooklet, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sponsorConsultantId")) {
					SetXmlValue(ref sponsorConsultantID, node.InnerText);
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
		public static SalesItemCouponSheet[] GetSalesItemCouponSheets() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesItemCouponSheets();
		}

		public static SalesItemCouponSheet GetSalesItemCouponSheetByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesItemCouponSheetByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalesItemCouponSheet(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesItemCouponSheet(this);
		}
		#endregion

		#region Properties
		public int CouponSheetID {
			set { couponSheetID = value; }
			get { return couponSheetID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int SalesItemNo {
			set { salesItemNo = value; }
			get { return salesItemNo; }
		}

		public DateTime DateAssigned {
			set { dateAssigned = value; }
			get { return dateAssigned; }
		}

		public int SheetPerBooklet {
			set { sheetPerBooklet = value; }
			get { return sheetPerBooklet; }
		}

		public int SponsorConsultantID {
			set { sponsorConsultantID = value; }
			get { return sponsorConsultantID; }
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
