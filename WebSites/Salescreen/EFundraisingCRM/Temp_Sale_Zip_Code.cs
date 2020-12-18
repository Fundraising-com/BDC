using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class TempSaleZipCode: EFundraisingCRMDataObject {

		private string zipCode;
		private int saleToAddID;


		public TempSaleZipCode() : this(null) { }
		public TempSaleZipCode(string zipCode) : this(zipCode, int.MinValue) { }
		public TempSaleZipCode(string zipCode, int saleToAddID) {
			this.zipCode = zipCode;
			this.saleToAddID = saleToAddID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TempSaleZipCode>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<SaleToAddID>" + saleToAddID + "</SaleToAddID>\r\n" +
			"</TempSaleZipCode>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("saleToAddId")) {
					SetXmlValue(ref saleToAddID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TempSaleZipCode[] GetTempSaleZipCodes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTempSaleZipCodes();
		}

		/*
		public static TempSaleZipCode GetTempSaleZipCodeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTempSaleZipCodeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTempSaleZipCode(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTempSaleZipCode(this);
		}*/
		#endregion

		#region Properties
		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public int SaleToAddID {
			set { saleToAddID = value; }
			get { return saleToAddID; }
		}

		#endregion
	}
}
