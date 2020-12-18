using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LocalSponsorSalesItem: EFundraisingCRMDataObject {

		private int brandID;
		private int localSponsorID;
		private int salesID;
		private int salesItemNo;


		public LocalSponsorSalesItem() : this(int.MinValue) { }
		public LocalSponsorSalesItem(int brandID) : this(brandID, int.MinValue) { }
		public LocalSponsorSalesItem(int brandID, int localSponsorID) : this(brandID, localSponsorID, int.MinValue) { }
		public LocalSponsorSalesItem(int brandID, int localSponsorID, int salesID) : this(brandID, localSponsorID, salesID, int.MinValue) { }
		public LocalSponsorSalesItem(int brandID, int localSponsorID, int salesID, int salesItemNo) {
			this.brandID = brandID;
			this.localSponsorID = localSponsorID;
			this.salesID = salesID;
			this.salesItemNo = salesItemNo;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LocalSponsorSalesItem>\r\n" +
			"	<BrandID>" + brandID + "</BrandID>\r\n" +
			"	<LocalSponsorID>" + localSponsorID + "</LocalSponsorID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<SalesItemNo>" + salesItemNo + "</SalesItemNo>\r\n" +
			"</LocalSponsorSalesItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("brandId")) {
					SetXmlValue(ref brandID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorId")) {
					SetXmlValue(ref localSponsorID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesItemNo")) {
					SetXmlValue(ref salesItemNo, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LocalSponsorSalesItem[] GetLocalSponsorSalesItems() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorSalesItems();
		}

		public static LocalSponsorSalesItem GetLocalSponsorSalesItemByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorSalesItemByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLocalSponsorSalesItem(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLocalSponsorSalesItem(this);
		}
		#endregion

		#region Properties
		public int BrandID {
			set { brandID = value; }
			get { return brandID; }
		}

		public int LocalSponsorID {
			set { localSponsorID = value; }
			get { return localSponsorID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int SalesItemNo {
			set { salesItemNo = value; }
			get { return salesItemNo; }
		}

		#endregion
	}
}
