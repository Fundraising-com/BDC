using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ScratchBook: EFundraisingCRMDataObject
	{

		private int scratchBookId;
		private short productClassId;
		private short supplierId;
		private int packageId;
		private string description;
		private float raisingPotential;
		private string productCode;
		private string currentDescription;
		private int isActive;
		private int isDisplayable;
		private int totalQty;
		private decimal fixedProfit; 


		public ScratchBook() : this(int.MinValue) { }
		public ScratchBook(int scratchBookId) : this(scratchBookId, short.MinValue) { }
		public ScratchBook(int scratchBookId, short productClassId) : this(scratchBookId, productClassId, short.MinValue) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId) : this(scratchBookId, productClassId, supplierId, int.MinValue) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId, int packageId) : this(scratchBookId, productClassId, supplierId, packageId, null) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId, int packageId, string description) : this(scratchBookId, productClassId, supplierId, packageId, description, short.MinValue) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId, int packageId, string description, float raisingPotential) : this(scratchBookId, productClassId, supplierId, packageId, description, raisingPotential, null) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId, int packageId, string description, float raisingPotential, string productCode) : this(scratchBookId, productClassId, supplierId, packageId, description, raisingPotential, productCode, null) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId, int packageId, string description, float raisingPotential, string productCode, string currentDescription) : this(scratchBookId, productClassId, supplierId, packageId, description, raisingPotential, productCode, currentDescription, int.MinValue) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId, int packageId, string description, float raisingPotential, string productCode, string currentDescription, int isActive) : this(scratchBookId, productClassId, supplierId, packageId, description, raisingPotential, productCode, currentDescription, isActive, int.MinValue) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId, int packageId, string description, float raisingPotential, string productCode, string currentDescription, int isActive, int isDisplayable) : this(scratchBookId, productClassId, supplierId, packageId, description, raisingPotential, productCode, currentDescription, isActive, isDisplayable, int.MinValue) { }
		public ScratchBook(int scratchBookId, short productClassId, short supplierId, int packageId, string description, float raisingPotential, string productCode, string currentDescription, int isActive, int isDisplayable, int totalQty) {
			this.scratchBookId = scratchBookId;
			this.productClassId = productClassId;
			this.supplierId = supplierId;
			this.packageId = packageId;
			this.description = description;
			this.raisingPotential = raisingPotential;
			this.productCode = productCode;
			this.currentDescription = currentDescription;
			this.isActive = isActive;
			this.isDisplayable = isDisplayable;
			this.totalQty = totalQty;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ScratchBook>\r\n" +
			"	<ScratchBookId>" + scratchBookId + "</ScratchBookId>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<SupplierId>" + supplierId + "</SupplierId>\r\n" +
			"	<PackageId>" + packageId + "</PackageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<RaisingPotential>" + raisingPotential + "</RaisingPotential>\r\n" +
			"	<ProductCode>" + System.Web.HttpUtility.HtmlEncode(productCode) + "</ProductCode>\r\n" +
			"	<CurrentDescription>" + System.Web.HttpUtility.HtmlEncode(currentDescription) + "</CurrentDescription>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"	<IsDisplayable>" + isDisplayable + "</IsDisplayable>\r\n" +
			"	<TotalQty>" + totalQty + "</TotalQty>\r\n" +
			"</ScratchBook>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("scratchBookId")) {
					SetXmlValue(ref scratchBookId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("supplierId")) {
					SetXmlValue(ref supplierId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("packageId")) {
					SetXmlValue(ref packageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("raisingPotential")) {
					SetXmlValue(ref raisingPotential, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productCode")) {
					SetXmlValue(ref productCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("currentDescription")) {
					SetXmlValue(ref currentDescription, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDisplayable")) {
					SetXmlValue(ref isDisplayable, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("totalQty")) {
					SetXmlValue(ref totalQty, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		
		public static EFundraisingCRMCollectionBase GetCollectionScratchBooks()
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCollectionScratchBooks() ;
		}

		public static ScratchBook[] GetScratchBooks() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetScratchBooks();
		}


		public static ScratchBook[] GetScratchBooksByProductClassID(int productClassID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetScratchBooksByProductClassID(productClassID);
		}

		public static ScratchBook GetScratchBookByID(int id) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetScratchBookByID(id);
		}

		public static bool UpdateProductsOnEfundWeb(int id) 
		{

			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProductsOnEfundWeb(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertScratchBook(this);
		}

		/*public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			//return dbo.UpdateScratchBook(this);
		}*/
		#endregion

		#region Properties
		public int ScratchBookId {
			set { scratchBookId = value; }
			get { return scratchBookId; }
		}

		public short ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public short SupplierId {
			set { supplierId = value; }
			get { return supplierId; }
		}

		public int PackageId {
			set { packageId = value; }
			get { return packageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public float RaisingPotential {
			set { raisingPotential = value; }
			get { return raisingPotential; }
		}

		public string ProductCode {
			set { productCode = value; }
			get { return productCode; }
		}

		public string CurrentDescription {
			set { currentDescription = value; }
			get { return currentDescription; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		public int IsDisplayable {
			set { isDisplayable = value; }
			get { return isDisplayable; }
		}

		public int TotalQty {
			set { totalQty = value; }
			get { return totalQty; }
		}

		public decimal FixedProfit 
		{
			set {fixedProfit = value;}
			get {return fixedProfit;}
		}
		#endregion

		public static ScratchBook GetScratchBookByProductCode(string productCode)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetScratchBookByProductCode(productCode);
		}
		#region IComparable Members

		public override int CompareTo(object obj)
		{
			// TODO:  Add ScratchBook.CompareTo implementation
			ScratchBook theBook = obj as ScratchBook;
			if (theBook != null)
				return string.Compare(description, theBook.description);
			return 0;
		}

		#endregion
	}
}
