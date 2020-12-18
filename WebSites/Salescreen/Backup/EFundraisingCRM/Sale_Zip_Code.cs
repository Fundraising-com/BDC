using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SaleZipCode: EFundraisingCRMDataObject {

		private string zipCode;
		private int salesID;


		public SaleZipCode() : this(null) { }
		public SaleZipCode(string zipCode) : this(zipCode, int.MinValue) { }
		public SaleZipCode(string zipCode, int salesID) {
			this.zipCode = zipCode;
			this.salesID = salesID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SaleZipCode>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"</SaleZipCode>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SaleZipCode[] GetSaleZipCodes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleZipCodes();
		}

		/*
		public static SaleZipCode GetSaleZipCodeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleZipCodeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSaleZipCode(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSaleZipCode(this);
		}*/
		#endregion

		#region Properties
		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		#endregion
	}
}
