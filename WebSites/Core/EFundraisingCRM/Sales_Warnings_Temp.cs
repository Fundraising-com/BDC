using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SalesWarningsTemp: EFundraisingCRMDataObject {

		private int saleToAddID;
		private int salesItemNo;
		private int salesConstraintID;


		public SalesWarningsTemp() : this(int.MinValue) { }
		public SalesWarningsTemp(int saleToAddID) : this(saleToAddID, int.MinValue) { }
		public SalesWarningsTemp(int saleToAddID, int salesItemNo) : this(saleToAddID, salesItemNo, int.MinValue) { }
		public SalesWarningsTemp(int saleToAddID, int salesItemNo, int salesConstraintID) {
			this.saleToAddID = saleToAddID;
			this.salesItemNo = salesItemNo;
			this.salesConstraintID = salesConstraintID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalesWarningsTemp>\r\n" +
			"	<SaleToAddID>" + saleToAddID + "</SaleToAddID>\r\n" +
			"	<SalesItemNo>" + salesItemNo + "</SalesItemNo>\r\n" +
			"	<SalesConstraintID>" + salesConstraintID + "</SalesConstraintID>\r\n" +
			"</SalesWarningsTemp>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("saleToAddId")) {
					SetXmlValue(ref saleToAddID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesItemNo")) {
					SetXmlValue(ref salesItemNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesConstraintId")) {
					SetXmlValue(ref salesConstraintID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SalesWarningsTemp[] GetSalesWarningsTemps() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesWarningsTemps();
		}

		public static SalesWarningsTemp GetSalesWarningsTempByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesWarningsTempByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalesWarningsTemp(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesWarningsTemp(this);
		}
		#endregion

		#region Properties
		public int SaleToAddID {
			set { saleToAddID = value; }
			get { return saleToAddID; }
		}

		public int SalesItemNo {
			set { salesItemNo = value; }
			get { return salesItemNo; }
		}

		public int SalesConstraintID {
			set { salesConstraintID = value; }
			get { return salesConstraintID; }
		}

		#endregion
	}
}
