using System;
using System.Text.RegularExpressions;
using GA.BDC.Core.eFundraisingStore;
using GA.BDC.Core.Configuration;


namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for PackageProductTemplateBasePage.
	/// </summary>
	public class PackageProductTemplateUserControl : System.Web.UI.UserControl
	{
		
		protected System.Web.UI.WebControls.Label PageDescriptionLabel;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageH1;
		protected System.Web.UI.WebControls.PlaceHolder ProfitChart = new System.Web.UI.WebControls.PlaceHolder();

		private int packageId;
		private int productId;

		public PackageProductTemplateUserControl()
		{
			//
			// TODO: Add constructor logic here
			//
		}



		#region Methods

		protected void SetGeneralValues()
		{
			if(packageId != 0)
			{
				Package package = Package.GetPackageByID(packageId);
				package.PackageDescription = PackageDesc.GetPackageDescByID(packageId);

                PageH1.InnerHtml = package.PackageDescription.PageTitle + "<br><img src=\"" + GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"] + "/Resources/Images/_fund_/_classic_/en-us/stroke.gif" + "\" border=0 alt='fundraising.com spacer'>";
				PageDescriptionLabel.Text = package.PackageDescription.LongDesc;
			}
			else
			{
				Product product = Product.GetProductByID(productId);
				product.ProductDescription = ProductDesc.GetProductDescByID(productId);

                PageH1.InnerHtml = product.ProductDescription.PageTitle + "<br><img src=\"" + GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"] + "/Resources/Images/_fund_/_classic_/en-us/stroke.gif" + "\" border=0 alt='fundraising.com spacer'>";
				PageDescriptionLabel.Text = product.ProductDescription.LongDesc;
			
				string profitChartPath;
				//TODO: dispatch the right path to the string
				
				if(product.ProductDescription.ExtraDesc != null)
				{
					if(product.ProductDescription.ExtraDesc != "")
					{
						Regex r = new Regex(@"<profitchart>([\w]+)</profitchart>"); 
						// Find a single match in the string.
						Match m = r.Match(product.ProductDescription.ExtraDesc); 
						if (m.Success) 
						{
				
							//profitChartPath="ProfitChart/"+ m.Groups[1].Value +"_profitchart.ascx";
							//ProfitChartBaseUserControl uc = (ProfitChartBaseUserControl)LoadControl(profitChartPath);
							//ProfitChart.Controls.Add(uc);
						}
					}
				}
			}
		}

		#endregion Methods

		#region Properties

		public int PackageId
		{
			get { return packageId; }
			set { packageId = value; }
		}

		public int ProductId
		{
			get { return productId; }
			set { productId = value; }
		}

		#endregion Properties
	}
}
