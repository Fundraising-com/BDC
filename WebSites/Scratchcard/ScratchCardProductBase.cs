using System;
using System.Web.UI.WebControls;
using GA.BDC.Core.eFundraisingStore;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.BusinessBase;



namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for ScratchCardProductBase.
	/// </summary>
	public class ScratchCardProductBase : ScratchcardWebBase
	{

		protected Package package = null;
		protected Package parentPackage = null;
		protected Product product = null;
		protected ProductCollection products = null;

		protected System.Web.UI.WebControls.PlaceHolder PagePlaceHolder = new PlaceHolder();

		protected bool IsPackage = true;

		protected int TOP_PARENT_PACKAGE_ID; 


		public ScratchCardProductBase()
		{
			//
			// TODO: Add constructor logic here
			//
		}

				
		protected override void OnInit(EventArgs e)
		{
            if (GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Scratchcard.Production", "isProduction"] == "true")
            { TOP_PARENT_PACKAGE_ID = Convert.ToInt32(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingStore.TopParentPackageID.Release", "PackageID"]); }
			else
            { TOP_PARENT_PACKAGE_ID = Convert.ToInt32(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingStore.TopParentPackageID.Debug", "PackageID"]); }
			IsPackageOrProduct();
		}

		protected override void OnLoad(EventArgs e)
		{
			FillPagePlaceHolder();
		}



		private void FillPagePlaceHolder()
		{
			if(!Page.IsPostBack) 
			{ 

				//if(IsPackage)
				//{
					Template template = Template.GetTemplateByID(package.PackageDescription.TemplateId);
					PackageProductTemplateUserControl uc = (PackageProductTemplateUserControl) LoadControl(template.Path); 
					uc.PackageId = package.PackageId;
					PagePlaceHolder.Controls.Add(uc); 
			/*	}
				else
				{
					Template template = Template.GetTemplateByID(product.ProductDescription.TemplateId);
					PackageProductTemplateUserControl uc = (PackageProductTemplateUserControl) LoadControl(template.Path); 		
					uc.ProductId = product.ProductId;
					PagePlaceHolder.Controls.Add(uc); 
				}*/
				
			}
		}

		//Find if the current page is a package page or a product page
		private void IsPackageOrProduct()
		{
			try
			{
				string pageName = this.GetPageName();
				package = Package.GetPackageByTopParentPackageIDAndPageName(TOP_PARENT_PACKAGE_ID, pageName);
				if(package == null)
				{
					IsPackage=false;
					InitializeProduct(this.GetPageName());
				}
				else
				{
					IsPackage=true;
					InitializePackage(this.GetPageName());

					//if the package has no child package and contains only one product, then redirect to product page
					PackageCollection childPackage = Package.GetPackagesByParentPackageID(package.PackageId);
					if(childPackage.Count==0)
					{
						ProductCollection childProduct = Product.GetProductsByPackageID(package.PackageId);
						if(childProduct != null)
						{
							if(childProduct.Count == 1)
							{
								IsPackage=false;
								foreach(Product p in childProduct)
								{
									InitializeProduct(p.ProductId);
								}
							}
						}
					}

				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

	

		public string GetPageName()
		{
            return Request.Url.AbsoluteUri.Substring(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"].Length, Request.Url.AbsoluteUri.Length - GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"].Length);
		}


		// get a package and its description from the database.
		// if the package has a parent, it will also be fetched from the database.
		protected void InitializePackage(string pageName)
		{
			try
			{
				package = Package.GetPackageByTopParentPackageIDAndPageName(TOP_PARENT_PACKAGE_ID, pageName);
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


		// get a product and its description from the database based on a page name. 
		// This method should always get only 1 product.
		protected void InitializeProduct(string pageName)
		{

			try
			{
				product = Product.GetProductByTopParentPackageIDAndPageName(TOP_PARENT_PACKAGE_ID, pageName);
				product.ProductDescription = ProductDesc.GetProductDescByID(product.ProductId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// get a product and its description from the database based on a packageId. 
		// This method should always get only 1 product.
		protected void InitializeProduct(int productId)
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

	

