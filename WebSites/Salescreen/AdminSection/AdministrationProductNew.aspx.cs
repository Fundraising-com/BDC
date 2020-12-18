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
using efundraising.EFundraisingCRM;
using efundraising.Diagnostics;
using System.IO;

namespace AdminSection
{
	/// <summary>
	/// Summary description for AdministrationProductNew.
	/// </summary>
	public class AdministrationProductNew : AdministrationBasePage
	{
		protected System.Web.UI.WebControls.Button SaveButton;
		protected System.Web.UI.WebControls.Button CloseButton;
		protected efundraising.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected efundraising.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
		protected efundraising.Web.UI.UIControls.ContentPanelControl Contentpanelcontrol1;
		protected System.Web.UI.WebControls.Label errorLabel;
		protected efundraising.Web.UI.MasterPages.Content Content1;
		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
		protected System.Web.UI.WebControls.Image ProductImage;
		protected System.Web.UI.WebControls.DropDownList CultureDropDownList;
		protected Components.User.Administration.PackageDescInfo ProductDescInfo1; 
		protected Components.User.Administration.ProductInfo ProductInfo1; 
	
		///This Page lets the user create a new product
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(IsPostBack))
			{
				int scrathcbookID = 0;
				try
				{
					//reset image in memory
					Session["ImagePathToDisplay"] = null;

					//get scratchbook ID from URL
					if (Request["sbid"] != null)
					{
						scrathcbookID = Convert.ToInt32(Request["sbid"]);
						//get scratchbook object from id
						efundraising.EFundraisingCRM.ScratchBook scratchbook = efundraising.EFundraisingCRM.ScratchBook.GetScratchBookByID(scrathcbookID);
					
						//fill scratchbook info
						FillProductInformation(scratchbook);
					}

					//fill culture dropdown
					Culture[] cultures = efundraising.eFundraisingStore.Culture.GetCultures();
					foreach(Culture culture in cultures) 
					{
						CultureDropDownList.Items.Add(new ListItem(culture.CultureName, culture.CultureCode));
					}

					CultureDropDownList.SelectedValue = DEFAULT_CULTURE;

							
					//sets default culture
					SetProductDescFromCultureCode(DEFAULT_CULTURE);

					//tells the control that the user is creating a product
					ProductDescInfo1.SetIsEditMode(false);
					ProductDescInfo1.SetType("Product");
				
				
					//check if current image to dislay exists, if not a default image is set
				    RefreshImage(sender,e);
				}

				catch(Exception ex)
				{
					if (ex.Message != "Thread was being aborted.")
					{
						Logger.LogError("Error in Page Load of AdministrationProductNew");	
					}
				}
			}

	
		}

		#region Private Methods

		//set the image paths (for uploading) depending on the culture selected
		private void SetProductDescFromCultureCode(string cultureCode)
		{	
			try
			{
			    string noImgPath = NO_IMAGE_PATH; //"~/UserResources/Images/noimage.jpg";
			
				ProductDescInfo1.SetSmallImagePath(PRODUCT_IMG_PATH + cultureCode + "/");
				ProductDescInfo1.SetLargeImagePath(PRODUCT_IMG_PATH + cultureCode + "/" + LARGE_IMG_FOLDER);
			}
			catch(Exception ex)
			{
				if (ex.Message != "Thread was being aborted.")
				{
					Logger.LogError("Error in SetProductDescFromCultureCode in Create Package");	
				}
			}
		}

		//insert new product into database, if data is valid
		//and insert new product description info
		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				bool error = false;
				errorLabel.Visible = false;
				ProductDescInfo1.ClearErrorLabel();
		
				//create product and desc objects
				Product product = new Product();
				efundraising.eFundraisingStore.ProductDesc productDesc = new efundraising.eFundraisingStore.ProductDesc();

				//image must have extension
				int pos = ProductDescInfo1.GetImageName().IndexOf(".");
				if (pos == -1 && ProductDescInfo1.GetImageName().Trim().Length > 0)
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

				
				//Image Name must be unique
				ProductDescCollection productDescs = null;
				if (!(ProductDescInfo1.GetImageName().Trim().Equals("")))
				{
					productDescs = efundraising.eFundraisingStore.ProductDesc.GetProductDescsByImageName(ProductDescInfo1.GetImageName());
				
					if (productDescs != null)
					{
						//errorLabel.Text = "Error. The Image Name already exists.";
						//errorLabel.Visible = true;
						//error = true;
					}
				}


				//check if page name exists (only if a parent is chosen, 
				string pageName = ProductDescInfo1.GetPageName();
				int parentProductID = ProductInfo1.GetParentProduct();
				if (parentProductID != int.MinValue)
				{
					if (!(ProductDescInfo1.GetPageName().Trim().Equals("")))
					{
						bool pageExists = Components.Server.Administration.AdministrationPackageProduct.PageNameExists(pageName, parentProductID);
						if (pageExists)
						{
							errorLabel.Text = "Error. The Page Name already exists.";
							errorLabel.Visible = true;
							error = true;
						}
					}
				}


				//if no error
				if(Page.IsValid && !error) 
				{
					// get the product object and product Desc object
					string cultureCode = GetCultureCode();
					
					// fill out new data from interface
                    product.ScratchBookId = ProductInfo1.GetScratchBookID();
					product.Name = ProductInfo1.GetProductNameTextBox();
					product.ParentProductId = ProductInfo1.GetParentProduct();
					product.ProductCode = ProductInfo1.GetProductCodeTextBox();
					product.IsInner = Convert.ToInt16(ProductInfo1.GetProductIsInner());
					product.RaisingPotential = Convert.ToDecimal(ProductInfo1.GetRaisingPotentialTextBox());
					product.Enabled = Convert.ToInt16(ProductInfo1.GetProductEnabled());

					//fill desc object			
					productDesc.CultureCode = cultureCode;
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
							
					//inserts with product info
					ProductStatus productStatus = (ProductStatus) product.Insert();

					switch(productStatus) 
					{
						case ProductStatus.Ok:
							productDesc.ProductId = product.ProductId;
							/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Inserted: " + product.ProductId,
								"A Product has been inserted", product, "Product Object", null,
								1);*/
				    		break;
						default:
						/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Inserted: " + product.ProductId,
								"A Product has been inserted", product, "Product Object", null,
								3);*/
				    	
							throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
					}

					//inserts desc with productDesc info
					ProductDescStatus productDescStatus = (ProductDescStatus) productDesc.Insert();
										
					switch(productDescStatus) 
					{
						case ProductDescStatus.Ok:
						/*	tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Inserted: " + product.ProductId + "-" + productDesc.CultureCode,
								"A Product Description has been inserted", productDesc, "ProductDesc Object", null,
								1);*/
				    	
							break;
						default:
						/*	tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Desc Inserted: " + product.ProductId + "-" + productDesc.CultureCode,
								"A Product Description has been inserted", productDesc, "ProductDesc Object", null,
								3);*/
				    	
							throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
					}
					// notify user of successful operation
					string message = "Saved";
					Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
	                Response.Redirect("AdministrationProductEdit.aspx?pid=" + product.ProductId,false);
				}
		
			}
			catch(Exception ex)
			{
				if (ex.Message != "Thread was being aborted.")
				{
					// notify user of an error
					string message = "An error occured while saving";
					Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
					Logger.LogError("Error in SaveButton of AdministrationProductNew");	
				}
			}
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'> { window.close();}</script>");
		}
	

		//This method gets a ScratchBook Obect and display its data in the new product form
		private void FillProductInformation(efundraising.EFundraisingCRM.ScratchBook scratchbook)
		{
			int productID = 0;
			try
			{
				//set product info on control
                ProductInfo1.SetScratchBookID(scratchbook.ScratchBookId);
				ProductInfo1.SetProductNameTextBox(scratchbook.Description);
				ProductInfo1.SetProductCodeTextBox(scratchbook.ProductCode);
				ProductInfo1.SetProductEnabled(Convert.ToBoolean(scratchbook.IsActive));
				
				//set raising potential value
				if (scratchbook.RaisingPotential != short.MinValue)
				{
					ProductInfo1.SetRaisingPotentialTextBox(scratchbook.RaisingPotential.ToString());
				}
				else
				{
					ProductInfo1.SetRaisingPotentialTextBox("0");
				}

				//sers description for name and current desc for short desc
				ProductDescInfo1.SetName(scratchbook.Description);
				ProductDescInfo1.SetShortDescription(scratchbook.CurrentDescription);
 				
			}
			catch(Exception ex)
			{
				
				Logger.LogError("Error in Page Load of AdministrationProductNew. ProductID=" + productID);	
				
			}
		}

		//***used for pop ups	 
	    private void BackButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				string productClassID = ViewState["ProductClassID"].ToString();
				Response.Redirect("AdministrationProductImport.aspx?pcid=" + productClassID,false);
			}	
			catch(Exception ex)
			{
				Logger.LogError("Error on back button in Admin Product New");
			}
		}

		private void RefreshImage(object sender, System.EventArgs e)
		{
			try
			{
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
				
				Logger.LogError("Error in Refresh ImageSaveButton of AdministrationProductNew", ex);	
				
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

		private void CultureDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		


	}
}
