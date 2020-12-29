using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CouponSheet: EFundraisingCRMDataObject {

		private int couponSheetID;
		private string productCode;
		private string description;
		private int sheetPerBooklet;
		private DateTime expirationDate;
		private int commissionPayable;
		private int isActive;


		public CouponSheet() : this(int.MinValue) { }
		public CouponSheet(int couponSheetID) : this(couponSheetID, null) { }
		public CouponSheet(int couponSheetID, string productCode) : this(couponSheetID, productCode, null) { }
		public CouponSheet(int couponSheetID, string productCode, string description) : this(couponSheetID, productCode, description, int.MinValue) { }
		public CouponSheet(int couponSheetID, string productCode, string description, int sheetPerBooklet) : this(couponSheetID, productCode, description, sheetPerBooklet, DateTime.MinValue) { }
		public CouponSheet(int couponSheetID, string productCode, string description, int sheetPerBooklet, DateTime expirationDate) : this(couponSheetID, productCode, description, sheetPerBooklet, expirationDate, int.MinValue) { }
		public CouponSheet(int couponSheetID, string productCode, string description, int sheetPerBooklet, DateTime expirationDate, int commissionPayable) : this(couponSheetID, productCode, description, sheetPerBooklet, expirationDate, commissionPayable, int.MinValue) { }
		public CouponSheet(int couponSheetID, string productCode, string description, int sheetPerBooklet, DateTime expirationDate, int commissionPayable, int isActive) {
			this.couponSheetID = couponSheetID;
			this.productCode = productCode;
			this.description = description;
			this.sheetPerBooklet = sheetPerBooklet;
			this.expirationDate = expirationDate;
			this.commissionPayable = commissionPayable;
			this.isActive = isActive;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CouponSheet>\r\n" +
			"	<CouponSheetID>" + couponSheetID + "</CouponSheetID>\r\n" +
			"	<ProductCode>" + System.Web.HttpUtility.HtmlEncode(productCode) + "</ProductCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<SheetPerBooklet>" + sheetPerBooklet + "</SheetPerBooklet>\r\n" +
			"	<ExpirationDate>" + expirationDate + "</ExpirationDate>\r\n" +
			"	<CommissionPayable>" + commissionPayable + "</CommissionPayable>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"</CouponSheet>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("couponSheetId")) {
					SetXmlValue(ref couponSheetID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productCode")) {
					SetXmlValue(ref productCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sheetPerBooklet")) {
					SetXmlValue(ref sheetPerBooklet, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("expirationDate")) {
					SetXmlValue(ref expirationDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionPayable")) {
					SetXmlValue(ref commissionPayable, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CouponSheet[] GetCouponSheets() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCouponSheets();
		}

		public static CouponSheet GetCouponSheetByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCouponSheetByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCouponSheet(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCouponSheet(this);
		}
		#endregion

		#region Properties
		public int CouponSheetID {
			set { couponSheetID = value; }
			get { return couponSheetID; }
		}

		public string ProductCode {
			set { productCode = value; }
			get { return productCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int SheetPerBooklet {
			set { sheetPerBooklet = value; }
			get { return sheetPerBooklet; }
		}

		public DateTime ExpirationDate {
			set { expirationDate = value; }
			get { return expirationDate; }
		}

		public int CommissionPayable {
			set { commissionPayable = value; }
			get { return commissionPayable; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		#endregion
	}
}
