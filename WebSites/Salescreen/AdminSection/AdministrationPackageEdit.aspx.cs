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
	/// Summary description for AdministrationPackageEdit.
	/// </summary>
	public class AdministrationPackageEdit : AdministrationBasePage
	{
		protected System.Web.UI.WebControls.Button SaveButton;
		protected efundraising.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
		protected efundraising.Web.UI.UIControls.ContentPanelControl Contentpanelcontrol2;
		protected efundraising.Web.UI.UIControls.ContentPanelControl Contentpanelcontrol3;
		protected System.Web.UI.WebControls.DropDownList CultureDropDownList;
		protected Components.User.Administration.PackageInfo PackageInfo1;
		protected Components.User.Administration.RelationsDatagrid RelationsDatagrid1;
		protected Components.User.Administration.PackageDescInfo PackageDescInfo1;
		protected System.Web.UI.WebControls.Button CloseButton;
		protected System.Web.UI.WebControls.Label errorLabel;
		protected efundraising.Web.UI.MasterPages.Content Content1;
		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
		protected efundraising.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected System.Web.UI.WebControls.Image PackageImage;
		
		
		
		//This Page lets the user edit the information of a Package
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(IsPostBack))
			{
				short packageID = 0;
				try
				{
					//reset image in memory
					Session["ImagePathToDisplay"] = null;

					//get ID from URL
					if (Request["pID"] != null)
					{
                		//fill culture dropdown
						Culture[] cultures = efundraising.eFundraisingStore.Culture.GetCultures();
						foreach(Culture culture in cultures) 
						{
							CultureDropDownList.Items.Add(new ListItem(culture.CultureName, culture.CultureCode));
						}

				    	packageID = Convert.ToInt16(Request["pID"]);

                        //set the relation datagrid with the package
						RelationsDatagrid1.SetPackageID(packageID);
											
						//get package object from id
						Package package = Package.GetPackageByID(packageID);
										
						//set package info on control
						PackageInfo1.SetPackageIDLabel(package.PackageId.ToString());
						PackageInfo1.SetPackageNameTextBox(package.Name);
						PackageInfo1.SetParentPackage(package.ParentPackageId);
						PackageInfo1.SetPackageEnabled(Convert.ToBoolean(package.Enabled));
						PackageInfo1.SetProfitPercentTextBox(package.ProfitPercentage.ToString());
						
                        
						//GET EN-US DESC BY DEFAULT, if doesnt exists
						bool cultureFound = SetPackageDescFromCultureCode(DEFAULT_CULTURE);
				
						//if the default is not found
						if (!(cultureFound))
						{
							///get first package desc object from id
							PackageDescCollection packageDescs = PackageDesc.GetPackageDescsByPackageID(packageID);
							if (packageDescs.Count >0)
							{
								PackageDesc packageDesc = (PackageDesc) packageDescs[0];
								SetPackageDescFromCultureCode(packageDesc.CultureCode);
								//set culture dropdown value
								CultureDropDownList.SelectedValue = packageDesc.CultureCode;
							}
							else
							{
								Logger.LogError("Error in Page Load of AdministrationPackageEdit. Package has no DESC PackageID=" + packageID);	
							}

						}
						else
						{
							//set culture dropdown
							CultureDropDownList.SelectedValue = DEFAULT_CULTURE;
						}

						
						//check if package name is duplicate
						PackageCollection packagess  = Package.GetPackagesByName(package.Name);
										
						if (packagess.Count > 1)
						{
							PackageInfo1.SetExistsImage(true);
						}
		
					}
					RefreshImage(sender,e);
				}
				catch(Exception ex)
				{
					Logger.LogError("Error in Page Load of AdministrationPackageEdit. PackageID=" + packageID,ex);	
				}
			}	
			
  

	
			 			
		}

		#region Private Methods

		//this method is called whenever the culture dropdown is changed
		//the Desc control has to be refreshed
		//Returns if a Desc was found
		private bool SetPackageDescFromCultureCode(string cultureCode)
		{	
			bool cultureFound = false;
			try
			{			
				string noImgPath = NO_IMAGE_PATH; //"~/UserResources/Images/noimage.jpg";
			
				short packageID = Convert.ToInt16(Request["pID"]);
				//Get specific Desc from the package ID and CUlture ID
				PackageDesc packageDesc = PackageDesc.GetPackageDescByPackageIDAndCultureCode(packageID, cultureCode);

				//sets image paths for when the user uploads
				PackageDescInfo1.SetSmallImagePath(PACKAGE_IMG_PATH + cultureCode + "/");
				PackageDescInfo1.SetLargeImagePath(PACKAGE_IMG_PATH + cultureCode + "/" + LARGE_IMG_FOLDER);
			
				//if a Desc was found for the culture
				if (packageDesc != null)
				{
					cultureFound = true;
					//set package desc info
					PackageDescInfo1.SetName(packageDesc.Name);
					PackageDescInfo1.SetType("Package");
					PackageDescInfo1.SetPageName(packageDesc.PageName);
					PackageDescInfo1.SetOriginalPageName(packageDesc.PageName);
					PackageDescInfo1.SetPageTitle(packageDesc.PageTitle);
					PackageDescInfo1.SetTemplate(packageDesc.TemplateId);
					PackageDescInfo1.SetShortDescription(packageDesc.ShortDesc);
					PackageDescInfo1.SetLongDescription(packageDesc.LongDesc);
					PackageDescInfo1.SetExtraDescription(packageDesc.ExtraDesc);
					PackageDescInfo1.SetImageName(packageDesc.ImageName);
					PackageDescInfo1.SetOriginalImageName(packageDesc.ImageName);
					PackageDescInfo1.SetImageAltText(packageDesc.ImageAltText);
                    PackageDescInfo1.SetIsEditMode(true);
					PackageDescInfo1.SetDisplayOrderTextBox(packageDesc.DisplayOrder.ToString());
					PackageDescInfo1.SetEnabled(Convert.ToBoolean(packageDesc.Enabled));
			
					//get image
					string imgPath = PACKAGE_IMG_PATH + cultureCode + "/" + packageDesc.ImageName;
					string largeImgPath = PACKAGE_IMG_PATH + cultureCode +  "/" + LARGE_IMG_FOLDER + packageDesc.ImageName;
									
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
					//no Desc is found, the object is reset
					PackageDescInfo1.Clear();
					PackageDescInfo1.SetSmallImagePath(PACKAGE_IMG_PATH + cultureCode + "/");
					PackageDescInfo1.SetLargeImagePath(PACKAGE_IMG_PATH + cultureCode + "/" + LARGE_IMG_FOLDER);
			
					ViewState["SmallImagePath"] = noImgPath;  
					ViewState["LargeImagePath"] = noImgPath;  
				}
						
			}
			catch(Exception ex)
			{
				
				Logger.LogError("Error in  SetPackageDescFromCultureCode",ex);	
				
			}

			return cultureFound;
		}

		///update the information if the data is valid
		//updates Package info and Package Desc info
		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			

		}

		//Refreshes image
		private void RefreshImage(object sender, System.EventArgs e)
		{
			try
			{
				//check if image to display exists, if not a default image is set
				
				if (!(Session["ImagePathToDisplay"] == null))
				{
					if (File.Exists(Server.MapPath(Session["ImagePathToDisplay"].ToString())))
					{
						PackageImage.ImageUrl = Session["ImagePathToDisplay"].ToString() + "?" + DateTime.Now.Ticks;
					}
					else
					{
						PackageImage.ImageUrl = NO_IMAGE_PATH;;
					}
				}
				else
				{
					PackageImage.ImageUrl = NO_IMAGE_PATH;
				}
			}
			catch(Exception ex)
			{
				if (ex.Message != "Thread was being aborted.")
				{
					Logger.LogError("Error in Refresh ImageSaveButton of AdministrationPackageEdit", ex);	
				}
			}
		}

        //called when the culture is changed
	    private void CultureDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
	        errorLabel.Visible = false;  
			Session["ImagePathToDisplay"] = null; //reset the image
		    SetPackageDescFromCultureCode(GetCultureCode());
			PackageDescInfo1.SetInfo();
			RefreshImage(sender,e);
		}

		//invoke postback to refresh image
		private void RefreshButton_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
		{
			//do postback
		}

		//go the the main page
		private void StartButton_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("AdministrationProductPackage.aspx");
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'> { window.close();}</script>");
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
		    this.PackageDescInfo1.eventRefresh += new System.EventHandler(this.RefreshImage);
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

        protected void SaveButton_Click1(object sender, EventArgs e)
        {
            try
            {
                bool error = false;
                bool IsUpdate = true;
                errorLabel.Visible = false;
                PackageDescInfo1.ClearErrorLabel();


                //image must have extension
                int pos = PackageDescInfo1.GetImageName().IndexOf(".");
                if (pos == -1 && PackageDescInfo1.GetImageName().Trim().Length > 0)
                {
                    errorLabel.Text = "Error. The Image Name must contain the extension (.jpg)";
                    error = true;
                    errorLabel.Visible = true;
                }
                else if (PackageDescInfo1.GetTemplate() != int.MinValue) 	//If a template is selected, must have a  Page Title
                {
                    if (PackageDescInfo1.GetPageTitle().Trim() == "")
                    {
                        errorLabel.Text = "Error. When selected a template, the Page Title can not be empty.";
                        errorLabel.Visible = true;
                        error = true;
                    }
                }

                int packageID = Convert.ToInt32(PackageInfo1.GetPackageIDLabel());
                int parentPackageID = Convert.ToInt32(PackageInfo1.GetParentPackage());
                //compare parent package id with current id, can not be the same
                if (packageID == parentPackageID)
                {
                    errorLabel.Text = "Error. The Parent Package can not be the same as the current Package.";
                    errorLabel.Visible = true;
                    error = true;
                }


                //Image Name must be unique
                PackageDescCollection packageDescs = null;
                if (!(PackageDescInfo1.GetImageName().Trim().Equals("")))
                {
                    if (!(PackageDescInfo1.GetOriginalImageName().Equals(PackageDescInfo1.GetImageName())))
                    {
                        packageDescs = PackageDesc.GetPackageDescsByImageName(PackageDescInfo1.GetImageName());
                    }

                    if (packageDescs != null)
                    {
                        errorLabel.Text = "Error. The Image Name already exists.";
                        errorLabel.Visible = true;
                        error = true;
                    }
                }


                //Page name must be unique for a root package (Partner) 
                //check first if page name was changed
                if (!(PackageDescInfo1.GetPageName().Trim().Equals("")))
                {
                    //check if original page name is null
                    if (PackageDescInfo1.GetOriginalPageName() != null)
                    {

                        if (!(PackageDescInfo1.GetOriginalPageName().Equals(PackageDescInfo1.GetPageName())))
                        {
                            string pageName = PackageDescInfo1.GetPageName();
                            bool pageExists = Components.Server.Administration.AdministrationPackageProduct.PageNameExists(pageName, packageID, parentPackageID);
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
                if (Page.IsValid && !error)
                {
                    packageID = Convert.ToInt32(PackageInfo1.GetPackageIDLabel());
                    // get the package object and package Desc object
                    Package package = Package.GetPackageByID(packageID);

                    // get the package desc object and package Desc object
                    string cultureCode = GetCultureCode();
                    PackageDesc packageDesc = PackageDesc.GetPackageDescByPackageIDAndCultureCode(packageID, cultureCode);

                    //check if desc exists, if not, its an insert
                    if (packageDesc == null)
                    {
                        IsUpdate = false;
                        packageDesc = new PackageDesc();
                        packageDesc.PackageId = packageID;
                        packageDesc.CultureCode = cultureCode;
                    }

                    // fill out new data from interface
                    package.Name = PackageInfo1.GetPackageNameTextBox();

                    package.ParentPackageId = PackageInfo1.GetParentPackage();

                    package.ProfitPercentage = Convert.ToInt16(PackageInfo1.GetProfitPercentTextBox());
                    package.Enabled = Convert.ToInt16(PackageInfo1.GetPackageEnabled());

                    //fill desc object			
                    packageDesc.Name = PackageDescInfo1.GetName();
                    packageDesc.PageName = PackageDescInfo1.GetPageName();
                    packageDesc.PageTitle = PackageDescInfo1.GetPageTitle();
                    packageDesc.TemplateId = PackageDescInfo1.GetTemplate();
                    packageDesc.ShortDesc = PackageDescInfo1.GetShortDescriptionTextBox();
                    packageDesc.LongDesc = PackageDescInfo1.GetLongDescriptionTextBox();
                    packageDesc.ExtraDesc = PackageDescInfo1.GetExtraDescriptionTextBox();
                    packageDesc.ImageName = PackageDescInfo1.GetImageName();
                    packageDesc.ImageAltText = PackageDescInfo1.GetImageAltText();
                    packageDesc.DisplayOrder = Convert.ToInt32(PackageDescInfo1.GetDisplayOrderTextBox());
                    packageDesc.Enabled = Convert.ToInt16(PackageDescInfo1.GetEnabled());


                    // update with package info
                    PackageStatus packageStatus = (PackageStatus)package.Update();

                    switch (packageStatus)
                    {
                        case PackageStatus.Ok:

                            //check if package name is duplicate, display Warning
                            PackageCollection packagess = Package.GetPackagesByName(PackageInfo1.GetPackageNameTextBox());

                            if (packagess.Count > 1)
                            {
                                PackageInfo1.SetExistsImage(true);
                            }
                            else
                            {
                                PackageInfo1.SetExistsImage(false);
                            }


                            //tibo
                            /*tiboTalker = new Components.Server.Tibo.TiboTalker("Package Updated",
                                "A Package has been updated", package, "Package Object", null,
                                1);*/
                            break;
                        default:
                            /*	tiboTalker = new Components.Server.Tibo.TiboTalker("Package Updated: " + package.PackageId,
                                    "A Package has failed to be updated", package, "Package Object", null,
                                    3);*/
                            throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
                    }

                    PackageDescStatus packageDescStatus;
                    if (IsUpdate)
                    {
                        // update with package desc info
                        packageDescStatus = (PackageDescStatus)packageDesc.Update();
                    }
                    else
                    {
                        // update with package desc info
                        packageDescStatus = (PackageDescStatus)packageDesc.Insert();
                    }

                    switch (packageDescStatus)
                    {
                        case PackageDescStatus.Ok:

                            //set originals to new names
                            PackageDescInfo1.SetOriginalImageName(PackageDescInfo1.GetImageName());
                            PackageDescInfo1.SetOriginalPageName(PackageDescInfo1.GetPageName());

                            //tibo
                            if (IsUpdate)
                            {
                                /*	tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Package Desc Updated: " + package.PackageId + "-" + packageDesc.CultureCode,
                                        "A Package Description has been updated", packageDesc, "PackageDesc Object", null,
                                        1);*/
                            }
                            else
                            {
                                /*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Package Desc Inserted: " + package.PackageId + "-" + packageDesc.CultureCode,
                                    "A Package Description has been inserted", packageDesc, "PackageDesc Object", null,
                                    1);*/
                            }
                            break;
                        default:
                            if (IsUpdate)
                            {
                                /*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Package Desc Updated: " + package.PackageId + "-" + packageDesc.CultureCode ,
                                    "A Package Description has been updated", packageDesc, "PackageDesc Object", null,
                                    3);*/
                            }
                            else
                            {
                                /*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Package Desc Inserted: " + package.PackageId + "-" + packageDesc.CultureCode,
                                    "A Package Description has been inserted",packageDesc, "PackageDesc Object", null,
                                    3);*/
                            }
                            throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
                    }
                    // notify user of successful operation
                    string message = "Saved";
                    Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");

                }

            }
            catch (Exception ex)
            {
                // notify user of an error
                string message = "An error occured while saving";
                Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
                Logger.LogError("Error in SaveButton of AdministrationPackageEdit", ex);

            }
        }



	
		
		
	}
}

