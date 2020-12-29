using System;
using System.Xml;


namespace GA.BDC.Core.eFundraisingStore {

	public enum ProductStatus
	{
		Error,
		Ok
	}

	public class Product: eFundraisingStoreDataObject {

		private int productId;
		private int parentProductId;
		private int scratchBookId;
		private string name;
		private Decimal raisingPotential;
		private string productCode;
		private short enabled;
		private short isInner;
		private DateTime createDate;
		private ProductDesc productDescription = null;
		private ProductCollection childrenProducts = null;


		public Product() : this(int.MinValue) { }
		public Product(int productId) : this(productId, int.MinValue) { }
		public Product(int productId, int parentProductId) : this(productId, parentProductId, int.MinValue) { }
		public Product(int productId, int parentProductId, int scratchBookId) : this(productId, parentProductId, scratchBookId, null) { }
		public Product(int productId, int parentProductId, int scratchBookId, string name) : this(productId, parentProductId, scratchBookId, name, Decimal.MinValue) { }
		public Product(int productId, int parentProductId, int scratchBookId, string name, Decimal raisingPotential) : this(productId, parentProductId, scratchBookId, name, raisingPotential, null) { }
		public Product(int productId, int parentProductId, int scratchBookId, string name, Decimal raisingPotential, string productCode) : this(productId, parentProductId, scratchBookId, name, raisingPotential, productCode, short.MinValue) { }
		public Product(int productId, int parentProductId, int scratchBookId, string name, Decimal raisingPotential, string productCode, short enabled) : this(productId, parentProductId, scratchBookId, name, raisingPotential, productCode, enabled, short.MinValue) { }
		public Product(int productId, int parentProductId, int scratchBookId, string name, Decimal raisingPotential, string productCode, short enabled, short isInner) : this(productId, parentProductId, scratchBookId, name, raisingPotential, productCode, enabled, isInner, DateTime.MinValue) { }
		public Product(int productId, int parentProductId, int scratchBookId, string name, Decimal raisingPotential, string productCode, short enabled, short isInner, DateTime createDate) {
			this.productId = productId;
			this.parentProductId = parentProductId;
			this.scratchBookId = scratchBookId;
			this.name = name;
			this.raisingPotential = raisingPotential;
			this.productCode = productCode;
			this.enabled = enabled;
			this.isInner = isInner;
			this.createDate = createDate;
		}

		#region Method
		private void LoadChildrenProduct(Product product) 
		{
			try 
			{
				//product.productDescription = ProductDesc.GetProductDescByID(product.productId);
			} 
			catch {}

			product.childrenProducts = Product.GetProductsByParentId(product.productId);
			foreach(Product p in product.childrenProducts) 
			{
				LoadChildrenProduct(p);
			}
		}

		public void LoadChildrenProducts() 
		{
			try 
			{
				//productDescription = ProductDesc.GetProductDescByID(productId);
			} 
			catch {}
			childrenProducts = Product.GetProductsByParentId(productId);
			foreach(Product p in childrenProducts) 
			{
			   LoadChildrenProduct(p);
			}
			
		}

		//Gets page to display with parameters for sc.com
		public string ScratchCardPageUrl 
		{
			get 
			{ 
				
				string type = this.ProductDescription.ExtraDesc;
				string imageName = ProductDescription.ImageName;
				int pos = ProductDescription.ImageName.IndexOf(".");
				if (pos > 1)
				{
					imageName = ProductDescription.ImageName.Substring(0,pos);
				}				                   

				string url = "Scratchcard.aspx?type=" + imageName; 

				//check the extradesc to know if a second image is to be displayed
				//also checks if we must reverse the images
				if (type != null && type.Trim() != "" )
				{
					System.Data.DataSet ds = new System.Data.DataSet();
					ds.ReadXml(new System.IO.StringReader(type));
					type = ds.Tables[0].Rows[0][0].ToString();
					pos = type.IndexOf(".");
					if (pos > 1)
					{
						type = type.Substring(0,pos);
					}
					

					bool reverse = Convert.ToBoolean(ds.Tables[0].Rows[0][1]);
					
					if (reverse)
					{
						url = "Scratchcard.aspx?type=" + type + "&secondtype=" + imageName; 
					}
					else
					{
						url += "&secondtype=" + type;
					}
				}
			
				return url;	
			}
			
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Product>\r\n" +
			"	<ProductId>" + productId + "</ProductId>\r\n" +
			"	<ParentProductId>" + parentProductId + "</ParentProductId>\r\n" +
			"	<ScratchBookId>" + scratchBookId + "</ScratchBookId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<RaisingPotential>" + raisingPotential + "</RaisingPotential>\r\n" +
			"	<ProductCode>" + System.Web.HttpUtility.HtmlEncode(productCode) + "</ProductCode>\r\n" +
			"	<Enabled>" + enabled + "</Enabled>\r\n" +
			"	<IsInner>" + isInner + "</IsInner>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</Product>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "productId") {
					SetXmlValue(ref productId, node.InnerText);
				}
				if(node.Name.ToLower() == "parentProductId") {
					SetXmlValue(ref parentProductId, node.InnerText);
				}
				if(node.Name.ToLower() == "scratchBookId") {
					SetXmlValue(ref scratchBookId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "raisingPotential") {
					SetXmlValue(ref raisingPotential, node.InnerText);
				}
				if(node.Name.ToLower() == "productCode") {
					SetXmlValue(ref productCode, node.InnerText);
				}
				if(node.Name.ToLower() == "enabled") {
					SetXmlValue(ref enabled, node.InnerText);
				}
				if(node.Name.ToLower() == "isInner") {
					SetXmlValue(ref isInner, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Product[] GetProducts() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProducts();
		}

		public static Product GetProductByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductByID(id);
		}

		public static Product GetProductByTopParentPackageIDAndPageName(int topParentPackageId, string pageName) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductByTopParentPackageIDAndPageName(topParentPackageId, pageName);
		}

		public static ProductCollection GetProductsRoot() 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductsRoot();
		}
		
		public static ProductCollection GetProductsByScratchBookID(int scratchBookID, int packageRootID) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
           	return dbo.GetProductsByScratchBookID(scratchBookID,packageRootID);
		}
		
		public static ProductCollection GetProductsByName(string name) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductsByName(name);
		}
		
		public static ProductCollection GetProductsByNameSimilar(string name, int packageRootID) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductsByNameSimilar(name,packageRootID);
		}
		
		public static ProductCollection GetProductsByProductCode(string name, int packageRootID) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductsByProductCode(name, packageRootID);
		}
		
		public static int GetProductRootIDByID(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductRootIDByID(id);
		}

		public static ProductCollection GetProductsByParentId(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductsByParentId(id);
		}

		public static ProductCollection GetProductsByPackageID(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductsByPackageID(id);
		}

        public static ProductCollection GetProductsByPackageIDWebsite(int id)
        {
            DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
            return dbo.GetProductsByPackageIDWebsite(id);
        }

        
		/*public static Product[] GetProductsByPackageID(int id) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProductsByPackageID(id);
		}*/
		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProduct(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProduct(this);
		}

		#endregion

		#region Properties
		public int ProductId {
			set { productId = value; }
			get { return productId; }
		}

		public int ParentProductId {
			set { parentProductId = value; }
			get { return parentProductId; }
		}

		public int ScratchBookId {
			set { scratchBookId = value; }
			get { return scratchBookId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public Decimal RaisingPotential {
			set { raisingPotential = value; }
			get { return raisingPotential; }
		}

		public string ProductCode {
			set { productCode = value; }
			get { return productCode; }
		}

		public short Enabled {
			set { enabled = value; }
			get { return enabled; }
		}

		public short IsInner {
			set { isInner = value; }
			get { return isInner; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		public ProductDesc ProductDescription {
			set { productDescription = value; }
			get { return productDescription; }
		}

	
		public ProductCollection ChildrenProducts 
		{
			set { childrenProducts = value; }
			get { return childrenProducts; }
		}

		public string ImageUrl 
		{
			get { return "../../../UserResources/Images/Products/" + productId + ".jpg"; }
		}

		public string ScratchCardImageUrl 
		{
			get { return "../../../../Resources/Images/_ScratchcardWeb_/_classic_/en-US/aboutproduct/smallcards/" + ProductDescription.ImageName; }
		}

		public string ScratchCardAgentImageUrl 
		{
			get { return "../../../../Resources/Images/_AgentWeb_/_classic_/en-US/aboutproduct/smallcards/" + ProductDescription.ImageName; }
		}


       		

		public string EnlargeUrl 
		{
			get { return "javascript:NewWindow('UserResources/Images/Products/large/"+productId+".jpg', '355','680')" /*height="99" alt="General" src="resources/images/_efund_/_classic_/en-us/scratchcards/cards/generalsport.jpg"*/; }
		}
		#endregion
	}
}
