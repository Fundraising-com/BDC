using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Packages: EFundraisingCRMDataObject {

		private short packageId;
		private short parentPackageId;
		private short packageTemplateId;
		private short accountingClassId;
		private string packageName;
		private short profitPercentage;
		private short displayOrder;
		private int packageEnabled;
		private int containsProducts;
		private short nbParticipantsPerCase;


		public Packages() : this(short.MinValue) { }
		public Packages(short packageId) : this(packageId, short.MinValue) { }
		public Packages(short packageId, short parentPackageId) : this(packageId, parentPackageId, short.MinValue) { }
		public Packages(short packageId, short parentPackageId, short packageTemplateId) : this(packageId, parentPackageId, packageTemplateId, short.MinValue) { }
		public Packages(short packageId, short parentPackageId, short packageTemplateId, short accountingClassId) : this(packageId, parentPackageId, packageTemplateId, accountingClassId, null) { }
		public Packages(short packageId, short parentPackageId, short packageTemplateId, short accountingClassId, string packageName) : this(packageId, parentPackageId, packageTemplateId, accountingClassId, packageName, short.MinValue) { }
		public Packages(short packageId, short parentPackageId, short packageTemplateId, short accountingClassId, string packageName, short profitPercentage) : this(packageId, parentPackageId, packageTemplateId, accountingClassId, packageName, profitPercentage, short.MinValue) { }
		public Packages(short packageId, short parentPackageId, short packageTemplateId, short accountingClassId, string packageName, short profitPercentage, short displayOrder) : this(packageId, parentPackageId, packageTemplateId, accountingClassId, packageName, profitPercentage, displayOrder, int.MinValue) { }
		public Packages(short packageId, short parentPackageId, short packageTemplateId, short accountingClassId, string packageName, short profitPercentage, short displayOrder, int packageEnabled) : this(packageId, parentPackageId, packageTemplateId, accountingClassId, packageName, profitPercentage, displayOrder, packageEnabled, int.MinValue) { }
		public Packages(short packageId, short parentPackageId, short packageTemplateId, short accountingClassId, string packageName, short profitPercentage, short displayOrder, int packageEnabled, int containsProducts) : this(packageId, parentPackageId, packageTemplateId, accountingClassId, packageName, profitPercentage, displayOrder, packageEnabled, containsProducts, short.MinValue) { }
		public Packages(short packageId, short parentPackageId, short packageTemplateId, short accountingClassId, string packageName, short profitPercentage, short displayOrder, int packageEnabled, int containsProducts, short nbParticipantsPerCase) {
			this.packageId = packageId;
			this.parentPackageId = parentPackageId;
			this.packageTemplateId = packageTemplateId;
			this.accountingClassId = accountingClassId;
			this.packageName = packageName;
			this.profitPercentage = profitPercentage;
			this.displayOrder = displayOrder;
			this.packageEnabled = packageEnabled;
			this.containsProducts = containsProducts;
			this.nbParticipantsPerCase = nbParticipantsPerCase;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Packages>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"	<ParentPackageId>" + parentPackageId + "</ParentPackageId>\r\n" +
			"	<PackageTemplateId>" + packageTemplateId + "</PackageTemplateId>\r\n" +
			"	<AccountingClassId>" + accountingClassId + "</AccountingClassId>\r\n" +
			"	<PackageName>" + System.Web.HttpUtility.HtmlEncode(packageName) + "</PackageName>\r\n" +
			"	<ProfitPercentage>" + profitPercentage + "</ProfitPercentage>\r\n" +
			"	<DisplayOrder>" + displayOrder + "</DisplayOrder>\r\n" +
			"	<PackageEnabled>" + packageEnabled + "</PackageEnabled>\r\n" +
			"	<ContainsProducts>" + containsProducts + "</ContainsProducts>\r\n" +
			"	<NbParticipantsPerCase>" + nbParticipantsPerCase + "</NbParticipantsPerCase>\r\n" +
			"</Packages>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("packageId")) {
					SetXmlValue(ref packageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("parentPackageId")) {
					SetXmlValue(ref parentPackageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageTemplateId")) {
					SetXmlValue(ref packageTemplateId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accountingClassId")) {
					SetXmlValue(ref accountingClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageName")) {
					SetXmlValue(ref packageName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("profitPercentage")) {
					SetXmlValue(ref profitPercentage, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayOrder")) {
					SetXmlValue(ref displayOrder, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageEnabled")) {
					SetXmlValue(ref packageEnabled, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("containsProducts")) {
					SetXmlValue(ref containsProducts, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nbParticipantsPerCase")) {
					SetXmlValue(ref nbParticipantsPerCase, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Packages[] GetPackagess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPackagess();
		}

		/*
		public static Packages GetPackagesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPackagesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPackages(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePackages(this);
		}*/
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

		public short PackageTemplateId {
			set { packageTemplateId = value; }
			get { return packageTemplateId; }
		}

		public short AccountingClassId {
			set { accountingClassId = value; }
			get { return accountingClassId; }
		}

		public string PackageName {
			set { packageName = value; }
			get { return packageName; }
		}

		public short ProfitPercentage {
			set { profitPercentage = value; }
			get { return profitPercentage; }
		}

		public short DisplayOrder {
			set { displayOrder = value; }
			get { return displayOrder; }
		}

		public int PackageEnabled {
			set { packageEnabled = value; }
			get { return packageEnabled; }
		}

		public int ContainsProducts {
			set { containsProducts = value; }
			get { return containsProducts; }
		}

		public short NbParticipantsPerCase {
			set { nbParticipantsPerCase = value; }
			get { return nbParticipantsPerCase; }
		}

		#endregion
	}
}
