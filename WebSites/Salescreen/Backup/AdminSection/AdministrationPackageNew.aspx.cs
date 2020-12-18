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
using efundraising.eFundraisingStore;
using efundraising.Diagnostics;
using System.IO;

namespace AdminSection
{
	/// <summary>
	/// Summary description for AdministrationPackageNew.
	/// </summary>
	public class AdministrationPackageNew : AdministrationBasePage
	{
		protected System.Web.UI.WebControls.Button SaveButton;
		protected System.Web.UI.WebControls.Button CloseButton;
		protected System.Web.UI.WebControls.DropDownList CultureDropDownList;
		protected Components.User.Administration.PackageInfo PackageInfo1;
		protected System.Web.UI.WebControls.Image PackageImage;
		protected System.Web.UI.WebControls.Label errorLabel;
		protected efundraising.Web.UI.MasterPages.Content Content1;
		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
		protected efundraising.Web.UI.UIControls.PagePanelControl PagePanelControl1;
	 	protected Components.User.Administration.PackageDescInfo PackageDescInfo1; 
		

		///This Page lets the user create a New Package
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(IsPostBack))
			{
				try
				{
					//reset image in memory
					Session["ImagePathToDisplay"] = null;

					//fill culture dropdown
					// set language dropdown values 
					Culture[] cultures = efundraising.eFundraisingStore.Culture.GetCultures();
					foreach(Culture culture in cultures) 
					{
						CultureDropDownList.Items.Add(new ListItem(culture.CultureName, culture.CultureCode));
					}

					//sets default culture
	                CultureDropDownList.SelectedValue = DEFAULT_CULTURE;

							
					//inistialize the desc section
					SetPackageDescFromCultureCode(DEFAULT_CULTURE);

					//Tell the control the package is being create
					PackageDescInfo1.SetIsEditMode(false);
					PackageDescInfo1.SetType("Package");

     			}
				catch(Exception ex)
				{
					if (ex.Message != "Thread was being aborted.")
					{
						Logger.LogError("Error in Page Load of AdministrationPackageNew",ex);	
					}
				}
			}

			
			//checks to see if image to display exists, if not a default image is set
			if (!(Session["ImagePathToDisplay"] == null))
			{
				if (File.Exists(Server.MapPath(Session["ImagePathToDisplay"].ToString())))
				{
					PackageImage.ImageUrl = Session["ImagePathToDisplay"].ToString() + "?" + DateTime.Now.Ticks;
				}
				else
				{
					PackageImage.ImageUrl = NO_IMAGE_PATH;
				}
			}
			else
			{
				PackageImage.ImageUrl = NO_IMAGE_PATH;
			}
			
		
		}


		#region Private Methods

		//sets the image paths (for upload) depending on the culture selected
		private void SetPackageDescFromCultureCode(string cultureCode)
		{	
			try
			{
				string noImgPath = NO_IMAGE_PATH; //"~/UserResources/Images/noimage.jpg";
			
				PackageDescInfo1.SetSmallImagePath(PACKAGE_IMG_PATH + cultureCode + "/");
				PackageDescInfo1.SetLargeImagePath(PACKAGE_IMG_PATH + cultureCode + "/" + LARGE_IMG_FOLDER);
			}
			catch(Exception ex)
			{
				if (ex.Message != "Thread was being aborted.")
				{
					Logger.LogError("Error in SetPackageDescFromCultureCode in Create Package",ex);	
				}
			}
		}


		//create the package if information is valid
		//create the package desc info
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
						PackageImage.ImageUrl = NO_IMAGE_PATH;
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
					Logger.LogError("Error in Refresh ImageSaveButton of AdministrationPackageNew", ex);	
				}

			}
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'> { window.close();}</script>");
		}


		private void CultureDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			errorLabel.Visible = false;  
			Session["ImagePathToDisplay"] = null; //reset the image
			SetPackageDescFromCultureCode(GetCultureCode());
		}

		
		#region GET culture code
		private string GetCultureCode()
		{
			return CultureDropDownList.SelectedValue;
		}
		#endregion
	

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
                errorLabel.Visible = false;
                PackageDescInfo1.ClearErrorLabel();

                //create package and desc objects
                Package package = new Package();
                PackageDesc packageDesc = new PackageDesc();

                //image must have extension
                int pos = PackageDescInfo1.GetImageName().IndexOf(".");
                if (pos == -1 && PackageDescInfo1.GetImageName().Trim().Length > 0)
                {
                    errorLabel.Text = "Error. The Image Name must contain the extension (.jpg)";
                    error = true;
                    errorLabel.Visible = true;
                }
                else if (PackageDescInfo1.GetTemplate() != int.MinValue) 	//If a templete is selected, must have a  Page Title
                {
                    if (PackageDescInfo1.GetPageTitle().Trim() == "")
                    {
                        errorLabel.Text = "Error. When selected a template, the Page Title can not be empty.";
                        errorLabel.Visible = true;
                        error = true;
                    }
                }


                //Image Name must be unique
                PackageDescCollection packageDescs = null;
                if (!(PackageDescInfo1.GetImageName().Trim().Equals("")))
                {
                    packageDescs = PackageDesc.GetPackageDescsByImageName(PackageDescInfo1.GetImageName());

                    if (packageDescs != null)
                    {
                        errorLabel.Text = "Error. The Image Name already exists.";
                        errorLabel.Visible = true;
                        error = true;
                    }
                }


                //check if page name exists (only if a parent is chosen, 
                string pageName = PackageDescInfo1.GetPageName();
                int parentPackageID = PackageInfo1.GetParentPackage();
                if (parentPackageID != short.MinValue)
                {
                    if (!(PackageDescInfo1.GetPageName().Trim().Equals("")))
                    {
                        bool pageExists = Components.Server.Administration.AdministrationPackageProduct.PageNameExists(pageName, parentPackageID);
                        if (pageExists)
                        {
                            errorLabel.Text = "Error. The Page Name already exists.";
                            errorLabel.Visible = true;
                            error = true;
                        }
                    }
                }


                //if no errors
                if (Page.IsValid && !error)
                {
                    // get the package desc object and package Desc object
                    string cultureCode = GetCultureCode();

                    // fill out new data from interface
                    package.Name = PackageInfo1.GetPackageNameTextBox();
                    package.ParentPackageId = PackageInfo1.GetParentPackage();

                    package.ProfitPercentage = Convert.ToInt16(PackageInfo1.GetProfitPercentTextBox());
                    package.Enabled = Convert.ToInt16(PackageInfo1.GetPackageEnabled());

                    //fill desc object		
                    packageDesc.CultureCode = cultureCode;
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
                    PackageStatus packageStatus = (PackageStatus)package.Insert();

                    switch (packageStatus)
                    {
                        case PackageStatus.Ok:
                            packageDesc.PackageId = package.PackageId;
                            //tibo
                            /*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Package Inserted: " + package.PackageId,
                                "A Package has been inserted", package, "Package Object", null,
                                1);*/
                            break;
                        default:
                            //tibo
                            /*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Package Inserted: " + package.PackageId,
                                "A Package has been inserted", package, "Package Object", null,
                                3);*/
                            throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
                    }

                    PackageDescStatus packageDescStatus = (PackageDescStatus)packageDesc.Insert();

                    switch (packageDescStatus)
                    {
                        case PackageDescStatus.Ok:
                            //tibo
                            /*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Package Desc Inserted: " + package.PackageId + "-" + packageDesc.CultureCode,
                                "A Package Description has been inserted", packageDesc, "PackageDesc Object", null,
                                1);*/
                            break;
                        default:
                            /*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Package Desc Inserted: " + package.PackageId + "-" + packageDesc.CultureCode,
                                "A Package Description has been inserted", packageDesc, "PackageDesc Object", null,
                                3);*/
                            throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update package object", null, null);
                    }
                    // notify user of successful operation
                    string message = "Saved";
                    Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
                    Response.Redirect("AdministrationPackageEdit.aspx?pid=" + package.PackageId);
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
