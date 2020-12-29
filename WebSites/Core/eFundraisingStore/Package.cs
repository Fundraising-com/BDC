using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public enum PackageStatus
	{
		Error,
		Ok
	}

	public class Package: eFundraisingStoreDataObject {

		private short packageId;
		private short parentPackageId;
		private string name;
		private short profitPercentage;
		private short enabled;
		private DateTime createDate;
		private PackageDesc packageDescription;
		private PackageCollection childrenPackages;
		private ProductCollection products;
        

		public Package() : this(short.MinValue) { }
		public Package(short packageId) : this(packageId, short.MinValue) { }
		public Package(short packageId, short parentPackageId) : this(packageId, parentPackageId, null) { }
		public Package(short packageId, short parentPackageId, string name) : this(packageId, parentPackageId, name, short.MinValue) { }
		public Package(short packageId, short parentPackageId, string name, short profitPercentage) : this(packageId, parentPackageId, name, profitPercentage, short.MinValue) { }
		public Package(short packageId, short parentPackageId, string name, short profitPercentage, short enabled) : this(packageId, parentPackageId, name, profitPercentage, enabled, DateTime.MinValue) { }
		public Package(short packageId, short parentPackageId, string name, short profitPercentage, short enabled, DateTime createDate) {
			this.packageId = packageId;
			this.parentPackageId = parentPackageId;
			this.name = name;
			this.profitPercentage = profitPercentage;
			this.enabled = enabled;
			this.createDate = createDate;
			packageDescription = null;
			childrenPackages = null;
			products = null;
		}

		#region Method
		private void LoadChildrenPackage(Package package) {
			try {
				//package.packageDescription = PackageDesc.GetPackageDescByID(package.packageId);
			} catch {}

			package.childrenPackages = Package.GetPackagesByParentPackageID(package.packageId);
			foreach(Package p in package.childrenPackages) {
				LoadChildrenPackage(p);
			}
		}

		public void LoadChildrenPackages() {
			try {
				//packageDescription = PackageDesc.GetPackageDescByID(packageId);
			} catch {}
			childrenPackages = Package.GetPackagesByParentPackageID(packageId);
			foreach(Package p in childrenPackages) {
				System.Diagnostics.Debug.Write(p.name);
				LoadChildrenPackage(p);
			}
		}

	
		public void LoadProducts() 
		{
			products = Product.GetProductsByPackageID(this.packageId);
		}
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Package>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"	<ParentPackageId>" + parentPackageId + "</ParentPackageId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<ProfitPercentage>" + profitPercentage + "</ProfitPercentage>\r\n" +
			"	<Enabled>" + enabled + "</Enabled>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</Package>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "packageId") {
					SetXmlValue(ref packageId, node.InnerText);
				}
				if(node.Name.ToLower() == "parentPackageId") {
					SetXmlValue(ref parentPackageId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "profitPercentage") {
					SetXmlValue(ref profitPercentage, node.InnerText);
				}
				if(node.Name.ToLower() == "enabled") {
					SetXmlValue(ref enabled, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Package[] GetPackages() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackages();
		}

		public static Package GetPackageByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageByID(id);
		}

		public static Package GetPackageByTopParentPackageIDAndPageName(int topParentPackageId, string pageName) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageByTopParentPackageIDAndPageName(topParentPackageId, pageName);
		}

		public static int GetPackageRootIDByID(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageRootIDByID(id);
		}

		public static Package GetPackageRootByID(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageRootByID(id);
		}

		public static Package GetPackageByProductID(int id)
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackageByProductID(id);
		}

       
        public static PackageCollection GetPackagesByParentPackageID(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackagesByParentPackageID(id);
		}
		
		public static PackageCollection GetPackagesByName(string name) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackagesByName(name);
		}

		public static PackageCollection GetPackagesRoot() 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackagesRoot(int.MinValue);
		}
	
		public static PackageCollection GetPackagesRoot(int packageRootID) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackagesRoot(packageRootID);
		}
	
			
		public static System.Data.DataTable GetPackagesInDataTable() 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetPackagesInDataTable();
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertPackage(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdatePackage(this);
		}
		#endregion

		#region Properties
		public short PackageId {
			set { packageId = value; }
			get { return packageId; }
		}

		public short ParentPackageId {
			set { parentPackageId = value; }
			get { return parentPackageId; }
		}

        //only use for display purpose
        public string Index { get; set; }

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public short ProfitPercentage {
			set { profitPercentage = value; }
			get { return profitPercentage; }
		}

		public short Enabled {
			set { enabled = value; }
			get { return enabled; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}
		public PackageDesc PackageDescription
		{
			set { packageDescription = value; }
			get { return packageDescription; }
		}

		public PackageCollection ChildrenPackages
		{
			set { childrenPackages = value; }
			get { return childrenPackages; }
		}

		public ProductCollection Products
		{
			set { products = value; }
			get { return products; }
		}

		public string ImageUrl 
		{
			get { return "UserResources/Images/Packages/" + packageId + ".jpg"; }
		}

	
		#endregion
	}
}
