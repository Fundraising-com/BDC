using System;
using System.Web.UI.WebControls;
using efundraising.eFundraisingStore;


namespace AdminSection
{
	/// <summary>
	/// Summary description for PackageProductBase.
	/// </summary>
	public class PackageProductBase : eFundWebPage
	{
		protected Package package = null;
		protected Package parentPackage = null;
		protected Product product = null;
		protected ProductCollection products = null;

		#region Constants
		protected const string CHILD_IMAGE_PATH = "~/Resources/Images/Products/";
		protected const string NO_IMAGE_PATH =  "~/Resources/Images/NoImage.jpg";
		protected const string PACKAGE_IMG_PATH = "~/Resources/Images/Packages/";
		protected const string PRODUCT_IMG_PATH = "~/Resources/Images/Products/";
		protected const string LARGE_IMG_FOLDER = "Large/";
		
		protected const string DEFAULT_CULTURE = "en-US";
        protected const string PROFITBURSTS_IMG_PATH = "~/resources/images/_efund_/_classic_/en-us/profit_bursts/";
		protected const string BROCHURE_PAGES_LINK = "javascript:popUp('Components/User/brochures/_PACKAGE_ID_/page.htm', 'Brochures_Display', 'width=610,height=455,left=300,top=200')";
		protected const string POPCORN_ENLARGE_LINK = "javascript:popUp('UserResources/Images/Products/large/_PRODUCT_ID_.jpg', 'Popcorn_Display', 'width=188,height=275,left=400,top=300')";
		protected const string SCRATCH_CANADA_ENLRG ="javascript:popUp('resources/images/_efund_/_classic_/en-us/Canada/Card_big.jpg', 'Scratchcard_Display', 'width=188,height=275,left=400,top=300')";
		protected const string COUPON_SCRATCH_CANADA_ENLRG = "javascript:popUp('resources/images/_efund_/_classic_/en-us/Canada/Coupon_Big.gif', 'Coupon_Display', 'width=188,height=275,left=400,top=300')";
		#endregion
		
		public PackageProductBase()
		{
            
		}
		
		// get a package and its description from the database.
		// if the package has a parent, it will also be fetched from the database.
		protected void InitializePackage(int packageId)
		{
			try
			{
				package = Package.GetPackageByID(packageId);
				package.PackageDescription = PackageDesc.GetPackageDescByID(package.PackageId);
				if (package.ParentPackageId != short.MinValue)
					parentPackage = Package.GetPackageByID(package.ParentPackageId);
					if (parentPackage != null)
						parentPackage.PackageDescription = PackageDesc.GetPackageDescByID(parentPackage.PackageId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		// get a product and its description from the database based on a packageId. 
		// This method should always get only 1 product.
		protected void InitializeProduct(int packageId)
		{
			ProductCollection products = Product.GetProductsByPackageID(packageId);
			
			if (products != null)
			{
				if (products.Count == 1)
				{
					product = (Product)products[0];
					
					product.ProductDescription = ProductDesc.GetProductDescByID(product.ProductId);
					if (product.ParentProductId == int.MinValue)
						product.ChildrenProducts = Product.GetProductsByParentId(product.ProductId);
				}
				else
				{
					throw new ArgumentException("This Package should only have one product under it!");
				}
			}
			else
			{
				throw new ArgumentException("PackageID provided does not exist.");
			}
		}

		// get a specific product with its provided productId.
		protected void InitializeSubProduct(int productId)
		{
			try
			{
				product = Product.GetProductByID(productId);
				product.ProductDescription = ProductDesc.GetProductDescByID(product.ProductId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
