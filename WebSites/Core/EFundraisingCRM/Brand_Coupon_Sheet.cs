using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class BrandCouponSheet: EFundraisingCRMDataObject {

		private int brandID;
		private int couponSheetID;
		private int couponPerSheet;


		public BrandCouponSheet() : this(int.MinValue) { }
		public BrandCouponSheet(int brandID) : this(brandID, int.MinValue) { }
		public BrandCouponSheet(int brandID, int couponSheetID) : this(brandID, couponSheetID, int.MinValue) { }
		public BrandCouponSheet(int brandID, int couponSheetID, int couponPerSheet) {
			this.brandID = brandID;
			this.couponSheetID = couponSheetID;
			this.couponPerSheet = couponPerSheet;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BrandCouponSheet>\r\n" +
			"	<BrandID>" + brandID + "</BrandID>\r\n" +
			"	<CouponSheetID>" + couponSheetID + "</CouponSheetID>\r\n" +
			"	<CouponPerSheet>" + couponPerSheet + "</CouponPerSheet>\r\n" +
			"</BrandCouponSheet>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("brandId")) {
					SetXmlValue(ref brandID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("couponSheetId")) {
					SetXmlValue(ref couponSheetID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("couponPerSheet")) {
					SetXmlValue(ref couponPerSheet, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BrandCouponSheet[] GetBrandCouponSheets() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBrandCouponSheets();
		}

		public static BrandCouponSheet GetBrandCouponSheetByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBrandCouponSheetByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBrandCouponSheet(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBrandCouponSheet(this);
		}
		#endregion

		#region Properties
		public int BrandID {
			set { brandID = value; }
			get { return brandID; }
		}

		public int CouponSheetID {
			set { couponSheetID = value; }
			get { return couponSheetID; }
		}

		public int CouponPerSheet {
			set { couponPerSheet = value; }
			get { return couponPerSheet; }
		}

		#endregion
	}
}
