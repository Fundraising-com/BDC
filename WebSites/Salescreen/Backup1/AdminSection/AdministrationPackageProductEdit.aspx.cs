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

namespace AdminSection
{
	/// <summary>
	/// Summary description for AdministrationPackageProductEdit.
	/// </summary>
	public class AdministrationPackageProductEdit : AdministrationBasePage
	{
		protected System.Web.UI.WebControls.Button SaveButton;
		protected System.Web.UI.WebControls.Button BackButton;
		protected System.Web.UI.WebControls.Button CloseButton;
		protected efundraising.Web.UI.MasterPages.Content Content1;
		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
		protected efundraising.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected Components.User.Administration.PackageProductLinkInfo PackageProductLinkInfo1;
	
		//This Page lets the user update an existing ProductPackage link
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(IsPostBack))
			{
				int productID = 0;
				int packageID = 0;
				string type = "";
				try
				{
					//get ID from URL
					if (Request["pID"] != null)
					{
						productID = Convert.ToInt32(Request["pid"]);
                        packageID = Convert.ToInt32(Request["pkID"]);
						type = Request["type"].ToString();
						
						//viewstate values for 'Back' Button
						ViewState["pid"] = productID;
                        ViewState["pkid"] = packageID;
						ViewState["Type"] = type;

						//get product package link info
						ProductPackage productPackage = ProductPackage.GetProductPackageByPackageIDAndProductID(packageID, productID);
						
						//set package product info on control
						PackageProductLinkInfo1.SetProduct(productPackage.ProductId);
						PackageProductLinkInfo1.SetPackage(productPackage.PackageId);
						PackageProductLinkInfo1.SetDisplay(productPackage.Display.ToString());
						PackageProductLinkInfo1.SetDisplayOrderTextBox(productPackage.DisplayOrder);
				
						PackageProductLinkInfo1.SetProductEnabled(false);
						PackageProductLinkInfo1.SetPackageEnabled(false);
    				}
				}
				catch(Exception ex)
				{
					Logger.LogError("Error in Page Load of Admin PackageProduct Edit",ex);
				
				}
			}
	
		}

		#region Private Methods

		//Updates new information in database
		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Page.IsValid) 
				{
				

					// get the package product object
					int productID = Convert.ToInt32(PackageProductLinkInfo1.GetProduct());
					int packageID = Convert.ToInt32(PackageProductLinkInfo1.GetPackage());
					ProductPackage productPakage = ProductPackage.GetProductPackageByPackageIDAndProductID(packageID,productID);
				
					// fill out new data from interface
					productPakage.ProductId = PackageProductLinkInfo1.GetProduct();
					productPakage.PackageId = PackageProductLinkInfo1.GetPackage();
					productPakage.Display = Convert.ToInt16(PackageProductLinkInfo1.GetDisplay());
					productPakage.DisplayOrder = PackageProductLinkInfo1.GetDisplayOrderTextBox();
				
					// update with package info
					ProductPackageStatus productPackageStatus = (ProductPackageStatus) productPakage.Update();

					switch(productPackageStatus) 
					{
						case ProductPackageStatus.Ok:
							/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Package Updated: " + productPakage.ProductId + "-" + productPakage.PackageId ,
								"A Product/Package has been updated",productPakage, "ProductPackage Object", null,
								1);*/
							break;
						default:
							/*tiboTalker = new eFundraisingWeb.Components.Server.Tibo.TiboTalker("Product Package Updated: " + productPakage.ProductId + "-" + productPakage.PackageId ,
								"A Product/Package has been updated", productPakage, "ProductPackage Object", null,
								3);*/
							throw new efundraising.EFundraisingCRM.EFundraisingCRMException("Unable to update product package object", null, null);
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
				Logger.LogError("Error in SaveButton of AdministrationPackageProductEdit",ex);	
			
			}	
		}

		//***back button used for pop ups****
		private void BackButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				string type = (string) ViewState["Type"]; 
		
				if (type.Equals("Product"))
				{
					int id = (int) ViewState["pid"]; 
					Response.Redirect("AdministrationPackageProductLink.aspx?pid=" + id,false);
				}
				else
				{
					int id = (int) ViewState["pkid"]; 
					Response.Redirect("AdministrationPackageProductLink.aspx?pkid=" + id, false);
				}
			}
			catch(Exception ex)
			{
			
				Logger.LogError("Error on back button in Admin PackageProduct Edit",ex);
				
			}
		
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'> { window.close();}</script>");
		}

		//redirects to main page
		private void StartButton_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("AdministrationProductPackage.aspx", false);
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

	

		


	}
}
