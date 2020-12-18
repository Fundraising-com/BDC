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
using GA.BDC.Core.eFundraisingStore;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.Diagnostics;

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for PageTemplate.
	/// </summary>
	public class Products: ScratchcardWebBase
	{
		protected System.Web.UI.WebControls.PlaceHolder MainPlaceHolder = new PlaceHolder();
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected System.Web.UI.WebControls.PlaceHolder TopPlaceHolder;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
		public Products(){}

		private void Page_Load(object sender, System.EventArgs e)
		{
			try{
	        	//Get the root package id from the web config (Scratchcards)
				// List section

				GA.BDC.Core.efundraisingCore.eFundEnv oEnv = GA.BDC.Core.efundraisingCore.eFundEnv.Create();
				string culture = oEnv.CultureName;
										
				int packageID = int.Parse(ApplicationSettings.GetConfig()["Scratchcard.General", "PackageID"]);
				//System.Diagnostics.Debug.Assert(packageID==250, "Error");


				PackageCollection xx = Package.GetPackagesByParentPackageID(packageID);
					
				//initialize top control
				Components.User.Controls.Common.ScratchcardsTop top = (Components.User.Controls.Common.ScratchcardsTop) Page.LoadControl("Components/User/Controls/Common/scratchcardsTop.ascx");
				TopPlaceHolder.Controls.Add(top);


				//get package image
				PackageDesc packageDesc = PackageDesc.GetPackageDescByPackageIDAndCultureCode(packageID, culture);
								
				top.ImageUrl = packageDesc.ScratchCardImageUrl;
				top.ExtraDescXML = packageDesc.ExtraDesc;
				top.CultureName = culture;

				foreach(Package x in xx) 
				{	
				    //create instance of grid control for each child package	
					Components.User.Controls.Common.Scratchcards_Grid  tmp = (Components.User.Controls.Common.Scratchcards_Grid) Page.LoadControl("Components/User/Controls/Common/scratchcards_grid.ascx");
					//Components.User.Controls.Products.ProductCategories tmp = (Components.User.Controls.Products.ProductCategories) Page.LoadControl("Components/User/Controls/Products/productcategories.ascx");
					ProductCollection col = Product.GetProductsByPackageID(x.PackageId);
	
					//set data to control
					tmp.Category = x.PackageDescription.Name + ":";
					tmp.ShortDesc = x.PackageDescription.ShortDesc;
			   		tmp.SCDatalist.DataSource = col;
					tmp.SCDatalist.DataBind();

					MainPlaceHolder.Controls.Add(tmp);
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Page Load of Products on AgentWeb",ex);
				
			}
			
		}
	

		private void InitializeComponent()
		{

		}
	

		
	}
}