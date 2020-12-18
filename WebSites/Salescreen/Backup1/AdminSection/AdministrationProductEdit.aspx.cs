using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using efundraising.eFundraisingStore;
using efundraising.Diagnostics;
using System.IO;

namespace AdminSection
{
	/// <summary>
	/// Summary description for AdministrationProductEdit.
	/// </summary>
	/// 


	public class AdministrationProductEdit : AdministrationBasePage
	{
		protected System.Web.UI.WebControls.Button SaveButton;
		protected System.Web.UI.WebControls.Button CloseButton;
		protected efundraising.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected System.Web.UI.WebControls.DropDownList CultureDropDownList;
		protected System.Web.UI.WebControls.Label errorLabel;
	    protected Components.User.Administration.ProductInfo ProductInfo1;
		protected System.Web.UI.WebControls.Image ProductImage;
		protected efundraising.Web.UI.MasterPages.Content Content1;
		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
		protected Components.User.Administration.PackageDescInfo ProductDescInfo1;
		protected Components.User.Administration.RelationsDatagrid RelationsDatagrid1;


		///This Page lets the user edit an existing product information
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(IsPostBack))
			{
				short productID = 0;
				try
				{
					//reset image in memory
					Session["ImagePathToDisplay"] = null;
						
					//get ID from URL
					if (Request["pID"] != null)
					{
				    	//fill culture dropdown
						// set language dropdown values 
						Culture[] cultures = efundraising.eFundraisingStore.Culture.GetCultures();
						foreach(Culture culture in cultures) 
						{
							CultureDropDownList.Items.Add(new ListItem(culture.CultureName, culture.CultureCode));
						}

						//sets the product id from the url
						productID = Convert.ToInt16(Request["pID"]);
												
						//set the relation datagrid with the package
						RelationsDatagrid1.SetProductID(productID);

						//get product object from id
						Product product = Product.GetProductByID(productID);
															
						//set product info on control
						ProductInfo1.SetProductIDLabel(product.ProductId.ToString());
						ProductInfo1.SetProductNameTextBox(product.Name);
						ProductInfo1.SetProductCodeTextBox(product.ProductCode);
						ProductInfo1.SetParentProduct(product.ParentProductId);
						ProductInfo1.SetProductEnabled(Convert.ToBoolean(product.Enabled));
						ProductInfo1.SetProductIsInner(Convert.ToBoolean(product.IsInner));
								
						if (product.RaisingPotential != Decimal.MinValue)
						{
							ProductInfo1.SetRaisingPotentialTextBox(product.RaisingPotential.ToString());
						}
						else
						{
							ProductInfo1.SetRaisingPotentialTextBox("0");
						}

                        
						//GET EN-US DESC BY DEFAULT, if doesnt exists
						bool cultureFound = SetProductDescFromCultureCode(DEFAULT_CULTURE);
				
						if (!(cultureFound))
						{
							///get first product desc object from id
							ProductDescCollection productDescs = ProductDesc.GetProductDescsByProductID(productID);
							if (productDescs.Count >0)
							{
								ProductDesc productDesc = (ProductDesc) productDescs[0];
								SetProductDescFromCultureCode(productDesc.CultureCode);
								//set culture dropdown
								CultureDropDownList.SelectedValue = productDesc.CultureCode;
							}
							else
							{
								Logger.LogError("Error in Page Load of AdministrationProductEdit. Product has no DESC ProductID=" + productID);	
							}
						}
						else
						{
							//set culture dropdown
							CultureDropDownList.SelectedValue = DEFAULT_CULTURE;
						}

						//check if product name is duplicate, display warning
						ProductCollection products  = Product.GetProductsByName(product.Name);
										
						if (products.Count > 1)
						{
							ProductInfo1.SetExistsImage(true);
						}
					
					}  
					//check if image to display exists, if not a default image is set
		            RefreshImage(sender,e);
						
				}
				catch(Exception ex)
				{
	    			Logger.LogError("Error in Page Load of AdministrationProductEdit. ProductID=" + productID);	
				}
		   }
		
		 			
		 	 
		}
		

		#region Private Methods
		
		//this method is called whenever the culture dropdown is changed
		//the Desc control has to be refreshed
		//Returns if a Desc was found
		private bool SetProductDescFromCultureCode(string cultureCode)
		{
			bool cultureFound = false;
			try
			{		
				string noImgPath = NO_IMAGE_PATH; //"~/UserResources/Images/noimage.jpg";
			
				int productID = Convert.ToInt32(Request["pID"]);
				//Get specific Desc from the package ID and CUlture ID
				ProductDesc productDesc = ProductDesc.GetProductDescByProductIDAndCultureCode(productID, cultureCode);
					
				//sets image paths for when the user uploads
				ProductDescInfo1.SetSmallImagePath(PRODUCT_IMG_PATH + cultureCode + "/");
				ProductDescInfo1.SetLargeImagePath(PRODUCT_IMG_PATH + cultureCode + "/" + LARGE_IMG_FOLDER);
			
				//if the Desc was found
				if (productDesc != null)
				{
					cultureFound = true;
					//set producte desc info
					ProductDescInfo1.SetName(productDesc.Name);
					ProductDescInfo1.SetType("Product");
                    ProductDescInfo1.SetOriginalPageName(productDesc.PageName);
					ProductDescInfo1.SetPageName(productDesc.PageName);
					ProductDescInfo1.SetPageTitle(productDesc.PageTitle);
					ProductDescInfo1.SetTemplate(productDesc.TemplateId);
					ProductDescInfo1.SetShortDescription(productDesc.ShortDesc);
					ProductDescInfo1.SetLongDescription(productDesc.LongDesc);
					ProductDescInfo1.SetExtraDescription(productDesc.ExtraDesc);
					ProductDescInfo1.SetImageName(productDesc.ImageName);
					ProductDescInfo1.SetOriginalImageName(productDesc.ImageName);

					ProductDescInfo1.SetImageAltText(productDesc.ImageAltText);
                    ProductDescInfo1.SetIsEditMode(true);

					if (productDesc.DisplayOrder != short.MinValue)
					{
						ProductDescInfo1.SetDisplayOrderTextBox(productDesc.DisplayOrder.ToString());
					}
					else
					{
						ProductDescInfo1.SetDisplayOrderTextBox("0");
					}
					
					ProductDescInfo1.SetEnabled(Convert.ToBoolean(productDesc.Enabled));
					//get image
					string imgPath = PRODUCT_IMG_PATH + cultureCode + "/" + productDesc.ImageName;
					string largeImgPath = PRODUCT_IMG_PATH + cultureCode +  "/" + LARGE_IMG_FOLDER + productDesc.ImageName;
									
					//check if small image exists and sets image
					if (File.Exists(Server.MapPath(imgPath)))
					{
						ViewState["SmallImagePath"] = imgPath;
					}
					else
					{
						ViewState["SmallImagePath"] = noImgPath;  
					}
									
					//check if large image exists
					if (File.Exists(largeImgPath))
					{
						ViewState["LargeImagePath"] = largeImgPath;
					}
					else
					{
						ViewState["LargeImagePath"] = noImgPath;  
					}
											
					//sets image with small image unless we have a specific image to display
					if (Session["ImagePathToDisplay"] == null)
					{
						Session["ImagePathToDisplay"] = ViewState["SmallImagePath"].ToString();
					}
				}
				else
				{
					//resets tge Desc fields
					ProductDescInfo1.Clear();
					ProductDescInfo1.SetSmallImagePath(PRODUCT_IMG_PATH + cultureCode + "/");
					ProductDescInfo1.SetLargeImagePath(PRODUCT_IMG_PATH + cultureCode + "/" + LARGE_IMG_FOLDER);
			
					ViewState["SmallImagePath"] = noImgPath;  
					ViewState["LargeImagePath"] = noImgPath;  
				}
						
			}
			catch(Exception ex)
			{
					Logger.LogError("Error in  SetProductDescFromCultureCode");	
			}

			return cultureFound;
								
		}


		//this method will update the product information id the fields are valid
		//it will also update product description information
		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			try{
             
				bool error = false;
				bool IsUpdate = true; 
				errorLabel.Visible = false;
				ProductDescInfo1.ClearErrorLabel();
				
				//image must have extension
				string imageName = ProductDescInfo1.GetImageName();
				int pos = imageName.IndexOf(".");
				if (pos == -1 && imageName.Length > 0)
				{
					errorLabel.Text = "Error. The Image Name must contain an extension (.jpg)";
					error = true;
					errorLabel.Visible = true;
				}
				else if(ProductDescInfo1.GetTemplate() != int.MinValue) 	//If a template is selected, must have a  Page Title
				{
					if (ProductDescInfo1.GetPageTitle().Trim() == "")
					{
						errorLabel.Text = "Error. When selected a template, the Page Title can not be empty.";
						errorLabel.Visible = true;
						error = true;
					}
				}

				int productID = Convert.ToInt32(ProductInfo1.GetProductIDLabel());
                int parentProductID = ProductInfo1.GetParentProduct();
				//compare parent product id with current id, can not be the same
				if (productID == parentProductID)
				{
					errorLabel.Text = "Error. The Parent Product can not be the same as the current Product.";
					errorLabel.Visible = true;
					error = true;
				}
                
				
				///Image Name must be unique
				ProductDescCollection productDescs = null;
				if (!(ProductDescInfo1.GetImageName().Trim().Equals("")))
				{
					if (!(ProductDescInfo1.GetOriginalImageName().Equals(ProductDescInfo1.GetImageName())))
					{
						productDescs = ProductDesc.GetProductDescsByImageName(ProductDescInfo1.GetImageName());
					}

					if (productDescs != null)
					{
						errorLabel.Text = "Error. The Image Name already exists.";
						errorLabel.Visible = true;
						error = true;
					}
				}

				//Page name must be unique for a root package (Partner) 
				//get parentPackageForProductRoot
				//check first if page name was changed
				if (!(ProductDescInfo1.GetImageName().Trim().Equals("")))
				{
					//check if original page name is null
					if (ProductDescInfo1.GetOriginalPageName() != null)
					{
						if(!(ProductDescInfo1.GetOriginalPageName().Equals(ProductDescInfo1.GetPageName())))
						{
							string pageName = ProductDescInfo1.GetPageName();
							bool pageExists = Components.Server.Administration.AdministrationPackageProduct.PageNameExistsForProducts(pageName, productID, parentProductID);
							if (pageExists)
							{
								errorLabel.Text = "Error. The Page Name already exists.";
								errorLabel.Visible = true;
								error = true;
							}
						}
					}
					
				}                

				//if no errors
				if(Page.IsValid && !error) 
				{
					// get the product object and package Desc object
					efundraising.eFundraisingStore.Product product = efundraising.eFundraisingStore.Product.GetProductByID(productID);
					
					// get the package desc object and package Desc object
					string cultureCode = GetCultureCode();
					ProductDesc productDesc = ProductDesc.GetProductDescByProductIDAndCultureCode(productID, cultureCode);
					
					//check if desc exists, if not, its an insert
					if (productDesc == null)
					{
						IsUpdate = false;
                        productDesc = new ProductDesc();
						productDesc.ProductId = productID;
						productDesc.CultureCode = cultureCode;
					}

					// fill out new data from interface
					product.Name = ProductInfo1.GetProductNameTextBox();
                   	product.ParentProductId = ProductInfo1.GetParentProduct();
					product.ProductCode = ProductInfo1.GetProductCodeTextBox();
					product.IsInner = Convert.ToInt16(ProductInfo1.GetProductIsInner());
					product.RaisingPotential = Convert.ToDecimal(ProductInfo1.GetRaisingPotentialTextBox());
					product.Enabled = Convert.ToInt16(ProductInfo1.GetProductEnabled());

					//fill desc object			
					productDesc.Name = ProductDescInfo1.GetName();
                  	productDesc.PageName = ProductDescInfo1.GetPageName();
				    productDesc.PageTitle = ProductDescInfo1.GetPageTitle();
                    productDesc.TemplateId = ProductDescInfo1.GetTemplate();
					productDesc.ShortDesc = ProductDescInfo1.GetShortDescriptionTextBox();
					productDesc.LongDesc = ProductDescInfo1.GetLongDescriptionTextBox();
					productDesc.ExtraDesc = ProductDescInfo1.GetExtraDescriptionTextBox();
                    productDesc.ImageName = ProductDescInfo1.GetImageName();
				    productDesc.ImageAltText = ProductDescInfo1.GetImageAltText();
                    productDesc.DisplayOrder = Convert.ToInt32(ProductDescInfo1.GetDisplayOrderTextBox());
					productDesc.Enabled = Convert.ToInt16(ProductDescInfo1.GetEnabled());

							
					// update with package info
					ProductStatus productStatus = (ProductStatus) product.Update();

					switch(productStatus) 
					{
						case ProductStatus.Ok:

							//check if product name is duplicate, display Warning
							ProductCollection products  = Product.GetProductsByName(ProductInfo1.GetProductNameTextBox());
										
							if (products.Count > 1)
							{
								ProductInfo1.SetExistsImage(true);
							}
							else
							{
								ProductInfo1.SetExistsImage(false);
							}

						/*	tiboTalker = new Components.Server.Tibo.TiboTalker("Product Updated: " + product.ProductId,
								"A Product Description has been updated", product, "Product Object", null,
								1);*/
							break;
						default:
							/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Updated: " + product.ProductId,
								"A Product Description has been updated", product, "Product Object", null,
								3);*/
					
							throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
					}

	                // update with package desc info
					ProductDescStatus productDescStatus;
					if (IsUpdate)
					{
						productDescStatus = (ProductDescStatus) productDesc.Update();
					}
					else
					{
						// update with package desc info
						productDescStatus = (ProductDescStatus) productDesc.Insert();
					}
					
					switch(productDescStatus) 
					{
						case ProductDescStatus.Ok:
							if (IsUpdate)
							{
								//set originals to new names
								ProductDescInfo1.SetOriginalImageName(ProductDescInfo1.GetImageName()); 
								ProductDescInfo1.SetOriginalPageName(ProductDescInfo1.GetPageName());

							/*	tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Updated: " + product.ProductId + "-" + productDesc.CultureCode,
									"A Product Description has been updated", productDesc, "ProductDesc Object", null,
									1);*/
						
							}
							else
							{
								/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Inserted: " + product.ProductId + "-" + productDesc.CultureCode,
									"A Product Description has been inserted", productDesc, "ProductDesc Object", null,
									1);*/
							}
							break;
						default:
							if (IsUpdate)
							{
							/*	tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Updated: " + product.ProductId + "-" + productDesc.CultureCode,
									"A Product Description has been updated", productDesc, "ProductDesc Object", null,
									3);*/
						
							}
							else
							{
								/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Inserted: " + product.ProductId + "-" + productDesc.CultureCode,
									"A Product Description has been inserted", productDesc, "ProductDesc Object", null,
									3);*/
							}
							throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
					}
					// notify user of successful operation
					string message = "Saved";
					Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
	
				}
		
			}
			catch(Exception ex)
			{
			
				// notify user of an error
				string message = "An error occured while saving";
				Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
				Logger.LogError("Error in SaveButton of AdministrationProductEdit", ex);	
			
			}

		}

		
		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'> { window.close();}</script>");
		}

      

	   private void RefreshButton_Click(object sender, System.EventArgs e)
		{
			//postback
		}
        
		//called when the culture is changed, resets the data
		private void CultureDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			errorLabel.Visible = false;  
			Session["ImagePathToDisplay"] = null; //reset the image
			SetProductDescFromCultureCode(GetCultureCode());
			ProductDescInfo1.SetInfo();
			RefreshImage(sender, e);
		}

		private void StartButton_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("AdministrationProductPackage.aspx", false);
		}

		private void RefreshImage(object sender, System.EventArgs e)
		{
			try
			{
				// TODO:  Add AdministrationProductEdit.RefreshPicture implementation
				//check if image to display exists, if not a default image is set
				if (!(Session["ImagePathToDisplay"] == null))
				{
					if (File.Exists(Server.MapPath(Session["ImagePathToDisplay"].ToString())))
					{
						ProductImage.ImageUrl = Session["ImagePathToDisplay"].ToString() + "?" + DateTime.Now.Ticks;
					}
					else
					{
						ProductImage.ImageUrl = NO_IMAGE_PATH;
					}
				}
				else
				{
					ProductImage.ImageUrl = NO_IMAGE_PATH;
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Refresh ImageSaveButton of AdministrationProductEdit", ex);	
			}
		}


        #endregion
        
		#region GET culture code
		private string GetCultureCode()
		{
           return CultureDropDownList.SelectedValue;
		}
		#endregion

	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.ProductDescInfo1.eventRefresh += new System.EventHandler(this.RefreshImage);
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.CultureDropDownList.SelectedIndexChanged += new System.EventHandler(this.CultureDropDownList_SelectedIndexChanged);
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			try
			{
             
				bool error = false;
				bool IsUpdate = true; 
				errorLabel.Visible = false;
				ProductDescInfo1.ClearErrorLabel();
				
				//image must have extension
				string imageName = ProductDescInfo1.GetImageName();
				int pos = imageName.IndexOf(".");
				if (pos == -1 && imageName.Length > 0)
				{
					errorLabel.Text = "Error. The Image Name must contain an extension (.jpg)";
					error = true;
					errorLabel.Visible = true;
				}
				else if(ProductDescInfo1.GetTemplate() != int.MinValue) 	//If a template is selected, must have a  Page Title
				{
					if (ProductDescInfo1.GetPageTitle().Trim() == "")
					{
						errorLabel.Text = "Error. When selected a template, the Page Title can not be empty.";
						errorLabel.Visible = true;
						error = true;
					}
				}

				int productID = Convert.ToInt32(ProductInfo1.GetProductIDLabel());
				int parentProductID = ProductInfo1.GetParentProduct();
				//compare parent product id with current id, can not be the same
				if (productID == parentProductID)
				{
					errorLabel.Text = "Error. The Parent Product can not be the same as the current Product.";
					errorLabel.Visible = true;
					error = true;
				}
                
				
				///Image Name must be unique
				ProductDescCollection productDescs = null;
				if (!(ProductDescInfo1.GetImageName().Trim().Equals("")))
				{
					if (!(ProductDescInfo1.GetOriginalImageName().Equals(ProductDescInfo1.GetImageName())))
					{
						productDescs = ProductDesc.GetProductDescsByImageName(ProductDescInfo1.GetImageName());
					}

					if (productDescs != null)
					{
						errorLabel.Text = "Error. The Image Name already exists.";
						errorLabel.Visible = true;
						error = true;
					}
				}

				//Page name must be unique for a root package (Partner) 
				//get parentPackageForProductRoot
				//check first if page name was changed
				if (!(ProductDescInfo1.GetImageName().Trim().Equals("")))
				{
					if(!(ProductDescInfo1.GetOriginalPageName().Equals(ProductDescInfo1.GetPageName())))
					{
						string pageName = ProductDescInfo1.GetPageName();
						bool pageExists = Components.Server.Administration.AdministrationPackageProduct.PageNameExistsForProducts(pageName, productID, parentProductID);
						if (pageExists)
						{
							errorLabel.Text = "Error. The Page Name already exists.";
							errorLabel.Visible = true;
							error = true;
						}
					}
				}


				//if no errors
				if(Page.IsValid && !error) 
				{
					// get the product object and package Desc object
					efundraising.eFundraisingStore.Product product = efundraising.eFundraisingStore.Product.GetProductByID(productID);
					
					// get the package desc object and package Desc object
					string cultureCode = GetCultureCode();
					ProductDesc productDesc = ProductDesc.GetProductDescByProductIDAndCultureCode(productID, cultureCode);
					
					//check if desc exists, if not, its an insert
					if (productDesc == null)
					{
						IsUpdate = false;
						productDesc = new ProductDesc();
						productDesc.ProductId = productID;
						productDesc.CultureCode = cultureCode;
					}

					// fill out new data from interface
					product.Name = ProductInfo1.GetProductNameTextBox();
					product.ParentProductId = ProductInfo1.GetParentProduct();
					product.ProductCode = ProductInfo1.GetProductCodeTextBox();
					product.IsInner = Convert.ToInt16(ProductInfo1.GetProductIsInner());
					product.RaisingPotential = Convert.ToDecimal(ProductInfo1.GetRaisingPotentialTextBox());
					product.Enabled = Convert.ToInt16(ProductInfo1.GetProductEnabled());

					//fill desc object			
					productDesc.Name = ProductDescInfo1.GetName();
					productDesc.PageName = ProductDescInfo1.GetPageName();
					productDesc.PageTitle = ProductDescInfo1.GetPageTitle();
					productDesc.TemplateId = ProductDescInfo1.GetTemplate();
					productDesc.ShortDesc = ProductDescInfo1.GetShortDescriptionTextBox();
					productDesc.LongDesc = ProductDescInfo1.GetLongDescriptionTextBox();
					productDesc.ExtraDesc = ProductDescInfo1.GetExtraDescriptionTextBox();
					productDesc.ImageName = ProductDescInfo1.GetImageName();
					productDesc.ImageAltText = ProductDescInfo1.GetImageAltText();
					productDesc.DisplayOrder = Convert.ToInt32(ProductDescInfo1.GetDisplayOrderTextBox());
					productDesc.Enabled = Convert.ToInt16(ProductDescInfo1.GetEnabled());

							
					// update with package info
					ProductStatus productStatus = (ProductStatus) product.Update();

					switch(productStatus) 
					{
						case ProductStatus.Ok:

							//check if product name is duplicate, display Warning
							ProductCollection products  = Product.GetProductsByName(ProductInfo1.GetProductNameTextBox());
										
							if (products.Count > 1)
							{
								ProductInfo1.SetExistsImage(true);
							}
							else
							{
								ProductInfo1.SetExistsImage(false);
							}

						/*	tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Updated: " + product.ProductId,
								"A Product Description has been updated", product, "Product Object", null,
								1);*/
							break;
						default:
							/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Updated: " + product.ProductId,
								"A Product Description has been updated", product, "Product Object", null,
								3);*/
					
							throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
					}

					// update with package desc info
					ProductDescStatus productDescStatus;
					if (IsUpdate)
					{
						productDescStatus = (ProductDescStatus) productDesc.Update();
					}
					else
					{
						// update with package desc info
						productDescStatus = (ProductDescStatus) productDesc.Insert();
					}
					
					switch(productDescStatus) 
					{
						case ProductDescStatus.Ok:
							if (IsUpdate)
							{
								//set originals to new names
								ProductDescInfo1.SetOriginalImageName(ProductDescInfo1.GetImageName()); 
								ProductDescInfo1.SetOriginalPageName(ProductDescInfo1.GetPageName());

								/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Updated: " + product.ProductId + "-" + productDesc.CultureCode,
									"A Product Description has been updated", productDesc, "ProductDesc Object", null,
									1);*/
						
							}
							else
							{
								/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Inserted: " + product.ProductId + "-" + productDesc.CultureCode,
									"A Product Description has been inserted", productDesc, "ProductDesc Object", null,
									1);*/
							}
							break;
						default:
							if (IsUpdate)
							{
								/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Updated: " + product.ProductId + "-" + productDesc.CultureCode,
									"A Product Description has been updated", productDesc, "ProductDesc Object", null,
									3);*/
						
							}
							else
							{
								/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Inserted: " + product.ProductId + "-" + productDesc.CultureCode,
									"A Product Description has been inserted", productDesc, "ProductDesc Object", null,
									3);*/
							}
							throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
					}
					// notify user of successful operation
					string message = "Saved";
					Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
	
				}
		
			}
			catch(Exception ex)
			{
			
				// notify user of an error
				string message = "An error occured while saving";
				Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
				Logger.LogError("Error in SaveButton of AdministrationProductEdit", ex);	
			
			}
		
		}
	
	}
}
