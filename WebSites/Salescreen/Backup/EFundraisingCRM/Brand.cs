using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Brand: EFundraisingCRMDataObject {

		private int brandID;
		private string name;
		private string promotion;


		public Brand() : this(int.MinValue) { }
		public Brand(int brandID) : this(brandID, null) { }
		public Brand(int brandID, string name) : this(brandID, name, null) { }
		public Brand(int brandID, string name, string promotion) {
			this.brandID = brandID;
			this.name = name;
			this.promotion = promotion;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Brand>\r\n" +
			"	<BrandID>" + brandID + "</BrandID>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<Promotion>" + System.Web.HttpUtility.HtmlEncode(promotion) + "</Promotion>\r\n" +
			"</Brand>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("brandId")) {
					SetXmlValue(ref brandID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("name")) {
					SetXmlValue(ref name, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotion")) {
					SetXmlValue(ref promotion, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Brand[] GetBrands() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBrands();
		}

		public static Brand GetBrandByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBrandByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBrand(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBrand(this);
		}
		#endregion

		#region Properties
		public int BrandID {
			set { brandID = value; }
			get { return brandID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Promotion {
			set { promotion = value; }
			get { return promotion; }
		}

		#endregion
	}
}
