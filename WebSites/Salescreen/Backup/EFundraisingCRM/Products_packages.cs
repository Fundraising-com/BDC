using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ProductsPackages: EFundraisingCRMDataObject {

		private int productId;
		private short packageId;
		private short displayOrder;
		private short displayable;


		public ProductsPackages() : this(int.MinValue) { }
		public ProductsPackages(int productId) : this(productId, short.MinValue) { }
		public ProductsPackages(int productId, short packageId) : this(productId, packageId, short.MinValue) { }
		public ProductsPackages(int productId, short packageId, short displayOrder) : this(productId, packageId, displayOrder, short.MinValue) { }
		public ProductsPackages(int productId, short packageId, short displayOrder, short displayable) {
			this.productId = productId;
			this.packageId = packageId;
			this.displayOrder = displayOrder;
			this.displayable = displayable;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductsPackages>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"	<Displayable>" + displayable + "</Displayable>\r\n" +
			"</ProductsPackages>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("productId")) {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageId")) {
					SetXmlValue(ref packageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayOrder")) {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayable")) {
					SetXmlValue(ref displayable, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductsPackages[] GetProductsPackagess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductsPackagess();
		}

		public static ProductsPackages GetProductsPackagesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProductsPackagesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProductsPackages(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProductsPackages(this);
		}
		#endregion

		#region Properties
		public int ProductId {
			set { productId = value; }
			get { return productId; }
		}

		public short PackageId {
			set { packageId = value; }
			get { return packageId; }
		}

		public short DisplayOrder {
			set { displayOrder = value; }
			get { return displayOrder; }
		}

		public short Displayable {
			set { displayable = value; }
			get { return displayable; }
		}

		#endregion
	}
}
