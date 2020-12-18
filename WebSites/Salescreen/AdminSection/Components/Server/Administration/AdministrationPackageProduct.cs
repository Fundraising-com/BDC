using System;
using System.Data;
using efundraising.eFundraisingStore;
using efundraising.Diagnostics;
using System.Collections;


namespace AdminSection.Components.Server.Administration
{
	/// <summary>
	/// Summary description for AdministrationPackageProduct.
	/// </summary>
	public class AdministrationPackageProduct
	{
		public AdministrationPackageProduct()
		{
			//
			// TODO: Add constructor logic hered
			//
		}


		//Buid a dataTable for displaying all the Package/Product Relations
		public static DataTable CreateDataTableRelation(ProductPackageCollection ppCol)
		{
			DataTable dt = new DataTable();
			try
			{
				// Build our data table for binding
				dt.Columns.Add("ProductId");
                dt.Columns.Add("PackageId");
				dt.Columns.Add("Product");
				dt.Columns.Add("Package");
				dt.Columns.Add("DisplayOrder");
				dt.Columns.Add("Display");
					
				//for every relation
				foreach (ProductPackage productPackage in ppCol)
				{
					DataRow dr = dt.NewRow();
					
					dr["ProductId"] = productPackage.ProductId;
                    dr["PackageId"] = productPackage.PackageId;
					//go get product_name w/ product_id
					Product p = Product.GetProductByID(productPackage.ProductId); 
					if (p != null)
					{
						dr["Product"] = p.Name;
					}
										
					//get package_name w/ id
					Package pk = Package.GetPackageByID(productPackage.PackageId); 
					if (pk != null)
					{
						dr["Package"] = pk.Name;
					}		
					
					if (productPackage.DisplayOrder == short.MinValue)
					{
						dr["DisplayOrder"] = "-";
					}
					else
					{
						dr["DisplayOrder"] = productPackage.DisplayOrder;
					}
					

					if (productPackage.Display == 0)
					{
						dr["Display"] = "No";
					}
					else
					{
						dr["Display"] = "Yes";
					}
				
					dt.Rows.Add(dr);
				}
				
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in CreateDataTableProductPackage", ex);
			}

			return dt;

		}

        //checks if the same page name exists with the same Root Package
		//parent permet de determiner si le package id a ete associe a un parent
		//Si le package est pas associe a un parent, on campoare la page name du package id avec les page name de la bd et ont exlus
		//le package id courant
		public static bool PageNameExists(string pageName, int packageID)
		{
			bool samePageFound = false;
			try
			{
				//get current  root id
				if (pageName != "" && packageID != 0)
				{
					int currentRootID = 0; 
					currentRootID = Package.GetPackageRootIDByID(packageID);
					if (currentRootID == 0)
					{
						currentRootID = packageID; //parent his root
					}
				
					//get all package ids for all every package w/ same page names
					PackageDescCollection packageDescs = PackageDesc.GetPackageDescsByPageName(pageName); 
							
					foreach (PackageDesc packageDesc in packageDescs)
					{
					   samePageFound = CompareRootID(currentRootID,packageDesc.PackageId);
					}

					if (!(samePageFound))
					{
						//get all products ids for all every product w/ same page names
						ProductDescCollection productDescs = ProductDesc.GetProductDescsByPageName(pageName);  
						//for each product found, we have to find the root id....by finding the package id
						foreach (ProductDesc productDesc in productDescs)
						{
							if (!(samePageFound))
							{
								//get product root
								int productRootID = Product.GetProductRootIDByID(productDesc.ProductId);

								//get packageID
								ProductPackageCollection productPackages = ProductPackage.GetProductPackageByProductID(productRootID);
								if (productPackages != null)
								{
									//compare root id
									foreach(ProductPackage productPackage in productPackages)
									{
										if (!(samePageFound))
										{
											samePageFound = CompareRootID(currentRootID, productPackage.PackageId);
										}
										else
										{
											break;
										}

									}
								}
							}
							else
							{
								break;
							}
						}
					}
				}
			}	
			catch(Exception ex)
	        {
		       Logger.LogError("Error in PageNameExists", ex);
	        }

			return samePageFound;
			
		}

		private static bool CompareRootID(int currentRootID, int packageID)
		{
            bool pageFound = false;
			if (!(pageFound))
			{
				//check if same root
				int rootID = Package.GetPackageRootIDByID(packageID);
				if (rootID == 0) //is root
				{
					rootID = packageID;
				}
										
				if (rootID == currentRootID)
				{
					pageFound = true;
				}
			}
			return pageFound;
		}
         
		//this method is for the user who dsoesnt know if parentPackageID is 0, if it is, 
		//the package id is supplied to PageNameExists
		public static bool PageNameExists(string pageName, int packageID, int parentPackageID)
		{
			if (parentPackageID > 0)
			{
                return PageNameExists(pageName, parentPackageID);
			}
			else
			{
				return PageNameExists(pageName, packageID);
			}
		}



		private static bool PageNameFoundInProducts(string pageName, int parentProductID, ArrayList rootsID)
		{
			bool samePageFound = false;
			try
			{
				//get all products ids for every product w/ same page names
				ProductDescCollection productDescs = ProductDesc.GetProductDescsByPageName(pageName);  
				//for each product found, we have to find the root id....by finding the package id
				foreach (ProductDesc productDesc in productDescs)
				{
					if (productDesc.ProductId != parentProductID)
					{
						if (!(samePageFound))
						{
							//get packageID
							ProductPackageCollection productPackages = ProductPackage.GetProductPackageByProductID(productDesc.ProductId);
							if (productPackages != null)
							{
								//compare root id
								foreach(ProductPackage productPackage in productPackages)
								{
									if (!(samePageFound))
									{
										foreach(int packageRootID in rootsID)
										{
											if (!(samePageFound))
											{
												if (!(samePageFound))
												{
													samePageFound = CompareRootID(packageRootID, productPackage.PackageId);
												}
											}
											else
											{
												break;
											}
										}
									}
									else
									{
										break;
									}
								}
							}
						}
						else
						{
							break;
						}
					}
				}
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in PageNameFoundInProducts", ex);
			}
			return samePageFound;
		
		}

		//checks if the same page name exists with the same Root Package
		public static bool PageNameExistsForProducts(string pageName, int parentProductID)
		{
			bool samePageFound = false;
			try
			{
				ArrayList rootsID = new ArrayList();
				//get current  root id
				if (pageName != "" && parentProductID != 0)
				{
										
					//get all the package roots that the product belongs to
					int productRootID = Product.GetProductRootIDByID(parentProductID);
					if (productRootID == 0)
					{
						productRootID = parentProductID ; //parent his root
					}

                    ProductPackageCollection productPackages = ProductPackage.GetProductPackageByProductID(productRootID);
					foreach(ProductPackage productPackage in productPackages)
					{
					   int currentRootID =  Package.GetPackageRootIDByID(productPackage.PackageId);
					   if (currentRootID == 0)
					   {
					     currentRootID = productPackage.PackageId; //parent his root
					   }
					   rootsID.Add(currentRootID); 
     				}
	
						
					//get all package ids for every package w/ same page names
				
					PackageDescCollection packageDescs = PackageDesc.GetPackageDescsByPageName(pageName);  
							
					foreach (PackageDesc packageDesc in packageDescs)
					{
						if (!(samePageFound))
						{
							foreach(int packageRootID in rootsID)
							{
								if (!(samePageFound))
								{
									if (!(samePageFound))
									{
										samePageFound = CompareRootID(packageRootID,packageDesc.PackageId);
									}
								}
								else
								{
									break;
								}
							}
						}
						else
						{
						   break;
						}
					}

					if (!(samePageFound))
					{
	                   samePageFound = PageNameFoundInProducts(pageName, parentProductID, rootsID);
					}
				
				}
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error in PageNameExists", ex);
			}

			return samePageFound;
			
			
		}

		//this method is for the user who dsoesnt know if parentProductID is 0, if it is, 
		//the product id is supplied to PageNameExists
		public static bool PageNameExistsForProducts(string pageName, int productID, int parentProductID)
		{
			if (parentProductID > 0)
			{
				return PageNameExistsForProducts(pageName, parentProductID);
			}
			else
			{
				return PageNameExistsForProducts(pageName, productID);
			}
			
		}


	}
}
