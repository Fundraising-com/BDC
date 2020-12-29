using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PriceRange: EFundraisingCRMDataObject {

		private int priceRangeID;
		private int packageID;
		private int minimumQty;
		private int maximumQty;
		private float unitPriceSold;


		public PriceRange() : this(int.MinValue) { }
		public PriceRange(int priceRangeID) : this(priceRangeID, int.MinValue) { }
		public PriceRange(int priceRangeID, int packageID) : this(priceRangeID, packageID, int.MinValue) { }
		public PriceRange(int priceRangeID, int packageID, int minimumQty) : this(priceRangeID, packageID, minimumQty, int.MinValue) { }
		public PriceRange(int priceRangeID, int packageID, int minimumQty, int maximumQty) : this(priceRangeID, packageID, minimumQty, maximumQty, float.MinValue) { }
		public PriceRange(int priceRangeID, int packageID, int minimumQty, int maximumQty, float unitPriceSold) {
			this.priceRangeID = priceRangeID;
			this.packageID = packageID;
			this.minimumQty = minimumQty;
			this.maximumQty = maximumQty;
			this.unitPriceSold = unitPriceSold;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PriceRange>\r\n" +
			"	<PriceRangeID>" + priceRangeID + "</PriceRangeID>\r\n" +
			"	<PackageID>" + packageID + "</PackageID>\r\n" +
			"	<MinimumQty>" + minimumQty + "</MinimumQty>\r\n" +
			"	<MaximumQty>" + maximumQty + "</MaximumQty>\r\n" +
			"	<UnitPriceSold>" + unitPriceSold + "</UnitPriceSold>\r\n" +
			"</PriceRange>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("priceRangeId")) {
					SetXmlValue(ref priceRangeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageId")) {
					SetXmlValue(ref packageID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("minimumQty")) {
					SetXmlValue(ref minimumQty, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("maximumQty")) {
					SetXmlValue(ref maximumQty, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unitPriceSold")) {
					SetXmlValue(ref unitPriceSold, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PriceRange[] GetPriceRanges() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPriceRanges();
		}

		public static PriceRange GetPriceRangeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPriceRangeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPriceRange(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePriceRange(this);
		}
		#endregion

		#region Properties
		public int PriceRangeID {
			set { priceRangeID = value; }
			get { return priceRangeID; }
		}

		public int PackageID {
			set { packageID = value; }
			get { return packageID; }
		}

		public int MinimumQty {
			set { minimumQty = value; }
			get { return minimumQty; }
		}

		public int MaximumQty {
			set { maximumQty = value; }
			get { return maximumQty; }
		}

		public float UnitPriceSold {
			set { unitPriceSold = value; }
			get { return unitPriceSold; }
		}

		#endregion
	}
}
