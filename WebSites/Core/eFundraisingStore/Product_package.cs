using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public enum ProductPackageStatus
	{
		Error,
		Ok
	}

	public class ProductPackage: eFundraisingStoreDataObject {

		private int productId;
		private short packageId;
		private short displayOrder;
		private short display;
		private DateTime createDate;


		public ProductPackage() : this(int.MinValue) { }
		public ProductPackage(int productId) : this(productId, short.MinValue) { }
		public ProductPackage(int productId, short packageId) : this(productId, packageId, short.MinValue) { }
		public ProductPackage(int productId, short packageId, short displayOrder) : this(productId, packageId, displayOrder, short.MinValue) { }
		public ProductPackage(int productId, short packageId, short displayOrder, short display) : this(productId, packageId, displayOrder, display, DateTime.MinValue) { }
		public ProductPackage(int productId, short packageId, short displayOrder, short display, DateTime createDate) {
			this.productId = productId;
			this.packageId = packageId;
			this.displayOrder = displayOrder;
			this.display = display;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProductPackage>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"	<Display>" + display + "</Display>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</ProductPackage>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "productId") {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(node.Name.ToLower() == "packageId") {
					SetXmlValue(ref packageId, node.InnerText);
				}
				if(node.Name.ToLower() == "displayOrder") {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
				if(node.Name.ToLower() == "display") {
					SetXmlValue(ref display, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProductPackage[] GetProductPackages() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductPackages();
		}

		public static ProductPackage GetProductPackageByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductPackageByID(id);
		}

		public static ProductPackageCollection GetProductPackageByPackageID(short id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductPackageByPackageID(id);
		}
		
		public static ProductPackageCollection GetProductPackageByProductID(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductPackageByProductID(id);
		}

		public static ProductPackage GetProductPackageByPackageIDAndProductID(int packageID, int productID) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductPackageByPackageIDAndProductID(packageID,productID);
		}

		public int Insert() 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProductPackage(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProductPackage(this);
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

		public short Display {
			set { display = value; }
			get { return display; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
