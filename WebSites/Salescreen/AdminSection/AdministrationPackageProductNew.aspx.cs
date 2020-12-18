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
using efundraising.Diagnostics;
using efundraising.eFundraisingStore;

namespace AdminSection
{
	/// <summary>
	/// Summary description for AdministrationPackageProductNew.
	/// </summary>
	public class AdministrationPackageProductNew : AdministrationBasePage
	{
		protected System.Web.UI.WebControls.Button SaveButton;
		protected System.Web.UI.WebControls.Button BackButton;
		protected System.Web.UI.WebControls.Button CloseButton;
		protected efundraising.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected efundraising.Web.UI.MasterPages.Content Content1;
		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
		protected System.Web.UI.WebControls.Label errorLabel;
		protected Components.User.Administration.PackageProductLinkInfo PackageProductLinkInfo1;
	
		//This Page lets the user create a new ProductPackage link
    	private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(IsPostBack))
			{
				try
				{
					int productID = 0;
					short packageID = 0;

					//set the package and product to enable
					PackageProductLinkInfo1.SetProductEnabled(true);
					PackageProductLinkInfo1.SetPackageEnabled(true);

					//get product ID or package ID from URL
					if (Request["pid"] != null)
					{
						productID = Convert.ToInt32(Request["pid"]);
						PackageProductLinkInfo1.SetProduct(productID);
						//for back button purposes
						ViewState["ID"] = productID;
						ViewState["Type"] = "Product";
					}
					else if (Request["pkid"] != null)
					{
						packageID = Convert.ToInt16(Request["pkid"]);
						PackageProductLinkInfo1.SetPackage(packageID);
						ViewState["ID"] = packageID;
						ViewState["Type"] = "Package";
					}
				}	
				catch(Exception ex)
				{
					if (ex.Message != "Thread was being aborted.")
					{
						Logger.LogError("Error in Page Load of AdministrationProductNew",ex);	
					}
				}
			}
	
		}

		#region Private Methods

		//this method will insert in the databse the new ProductPackage information
		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			
		}

		//redirects to main page
		private void StartButton_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("AdministrationProductPackage.aspx");
		}

		//redirects to previous page depending if it was a product or package
		private void BackButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				string id =  ViewState["ID"].ToString(); 
				string type = (string) ViewState["Type"]; 
			
				if (type.Equals("Product"))
				{
					Response.Redirect("AdministrationPackageProductLink.aspx?pid=" + id );
				}
				else
				{
					Response.Redirect("AdministrationPackageProductLink.aspx?pkid=" + id );
				}
			}
			catch(Exception ex)
			{
				if (ex.Message != "Thread was being aborted.")
				{
					Logger.LogError("Error on back button in Admin PackageProduct New",ex);
				}
			}
		
			Globalize(PagePanelControl1);
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'> { window.close();}</script>");
		}


#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        protected void SaveButton_Click1(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    // create the package product object
                    ProductPackage productPakage = new ProductPackage();

                    // fill out new data from interface
                    productPakage.ProductId = PackageProductLinkInfo1.GetProduct();
                    productPakage.PackageId = PackageProductLinkInfo1.GetPackage();
                    productPakage.Display = Convert.ToInt16(PackageProductLinkInfo1.GetDisplay());
                    productPakage.DisplayOrder = PackageProductLinkInfo1.GetDisplayOrderTextBox();

                    // update with package info
                    ProductPackageStatus productPackageStatus = (ProductPackageStatus)productPakage.Insert();

                    switch (productPackageStatus)
                    {
                        case ProductPackageStatus.Ok:
                            /*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Package Inserted: " + productPakage.ProductId + "-" + productPakage.PackageId ,
                                "A Product/Package has been inserted", productPakage, "ProductPackage Object", null,
                                1);*/
                            break;
                        default:
                            /*	tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Package Inserted: " + productPakage.ProductId + "-" + productPakage.PackageId ,
                                    "A Product/Package has been inserted", productPakage, "ProductPackage Object", null,
                                    3);*/
                            throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update product package object", null, null);
                    }
                    // notify user of successful operation
                    string message = "Saved";
                    Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");

                    if (ViewState["Type"] != null)
                    {
                        Response.Redirect("AdministrationPackageProductEdit.aspx?pid=" + PackageProductLinkInfo1.GetProduct() + "&pkid=" + PackageProductLinkInfo1.GetPackage() + "&type=" + ViewState["Type"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    // notify user of an error
                    string message = "An error ocurred while saving. This relation might already exists.";
                    Page.RegisterClientScriptBlock("messagebox", "<script language='javascript'>alert('" + message.Replace("'", "\'") + "');</script>");
                    Logger.LogError("Error in SaveButton of AdministrationPackageProductNew", ex);
                }
            }	
        }

	

	
	


	}
}
