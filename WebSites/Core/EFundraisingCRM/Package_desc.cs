using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PackageDesc: EFundraisingCRMDataObject {

		private short packageId;
		private short languageId;
		private string packageName;
		private string packageShortDesc;
		private string packageLongDesc;
		private string packageExtraDesc;
		private string packageSmallImg;
		private string packageLargeImg;
		private string pageUrl;


		public PackageDesc() : this(short.MinValue) { }
		public PackageDesc(short packageId) : this(packageId, short.MinValue) { }
		public PackageDesc(short packageId, short languageId) : this(packageId, languageId, null) { }
		public PackageDesc(short packageId, short languageId, string packageName) : this(packageId, languageId, packageName, null) { }
		public PackageDesc(short packageId, short languageId, string packageName, string packageShortDesc) : this(packageId, languageId, packageName, packageShortDesc, null) { }
		public PackageDesc(short packageId, short languageId, string packageName, string packageShortDesc, string packageLongDesc) : this(packageId, languageId, packageName, packageShortDesc, packageLongDesc, null) { }
		public PackageDesc(short packageId, short languageId, string packageName, string packageShortDesc, string packageLongDesc, string packageExtraDesc) : this(packageId, languageId, packageName, packageShortDesc, packageLongDesc, packageExtraDesc, null) { }
		public PackageDesc(short packageId, short languageId, string packageName, string packageShortDesc, string packageLongDesc, string packageExtraDesc, string packageSmallImg) : this(packageId, languageId, packageName, packageShortDesc, packageLongDesc, packageExtraDesc, packageSmallImg, null) { }
		public PackageDesc(short packageId, short languageId, string packageName, string packageShortDesc, string packageLongDesc, string packageExtraDesc, string packageSmallImg, string packageLargeImg) : this(packageId, languageId, packageName, packageShortDesc, packageLongDesc, packageExtraDesc, packageSmallImg, packageLargeImg, null) { }
		public PackageDesc(short packageId, short languageId, string packageName, string packageShortDesc, string packageLongDesc, string packageExtraDesc, string packageSmallImg, string packageLargeImg, string pageUrl) {
			this.packageId = packageId;
			this.languageId = languageId;
			this.packageName = packageName;
			this.packageShortDesc = packageShortDesc;
			this.packageLongDesc = packageLongDesc;
			this.packageExtraDesc = packageExtraDesc;
			this.packageSmallImg = packageSmallImg;
			this.packageLargeImg = packageLargeImg;
			this.pageUrl = pageUrl;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PackageDesc>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<PackageName>" + System.Web.HttpUtility.HtmlEncode(packageName) + "</PackageName>\r\n" +
			"	<PackageShortDesc>" + System.Web.HttpUtility.HtmlEncode(packageShortDesc) + "</PackageShortDesc>\r\n" +
			"	<PackageLongDesc>" + System.Web.HttpUtility.HtmlEncode(packageLongDesc) + "</PackageLongDesc>\r\n" +
			"	<PackageExtraDesc>" + System.Web.HttpUtility.HtmlEncode(packageExtraDesc) + "</PackageExtraDesc>\r\n" +
			"	<PackageSmallImg>" + System.Web.HttpUtility.HtmlEncode(packageSmallImg) + "</PackageSmallImg>\r\n" +
			"	<PackageLargeImg>" + System.Web.HttpUtility.HtmlEncode(packageLargeImg) + "</PackageLargeImg>\r\n" +
			"	<PageUrl>" + System.Web.HttpUtility.HtmlEncode(pageUrl) + "</PageUrl>\r\n" +
			"</PackageDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("packageId")) {
					SetXmlValue(ref packageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageName")) {
					SetXmlValue(ref packageName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageShortDesc")) {
					SetXmlValue(ref packageShortDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageLongDesc")) {
					SetXmlValue(ref packageLongDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageExtraDesc")) {
					SetXmlValue(ref packageExtraDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageSmallImg")) {
					SetXmlValue(ref packageSmallImg, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageLargeImg")) {
					SetXmlValue(ref packageLargeImg, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("pageUrl")) {
					SetXmlValue(ref pageUrl, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PackageDesc[] GetPackageDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPackageDescs();
		}

		/*
		public static PackageDesc GetPackageDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPackageDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPackageDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePackageDesc(this);
		}*/
		#endregion

		#region Properties
		public short PackageId {
			set { packageId = value; }
			get { return packageId; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string PackageName {
			set { packageName = value; }
			get { return packageName; }
		}

		public string PackageShortDesc {
			set { packageShortDesc = value; }
			get { return packageShortDesc; }
		}

		public string PackageLongDesc {
			set { packageLongDesc = value; }
			get { return packageLongDesc; }
		}

		public string PackageExtraDesc {
			set { packageExtraDesc = value; }
			get { return packageExtraDesc; }
		}

		public string PackageSmallImg {
			set { packageSmallImg = value; }
			get { return packageSmallImg; }
		}

		public string PackageLargeImg {
			set { packageLargeImg = value; }
			get { return packageLargeImg; }
		}

		public string PageUrl {
			set { pageUrl = value; }
			get { return pageUrl; }
		}

		#endregion
	}
}
