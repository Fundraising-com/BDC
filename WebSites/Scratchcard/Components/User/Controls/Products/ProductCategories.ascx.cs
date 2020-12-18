namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Products
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;

    using GA.BDC.Core.eFundraisingStore;

	/// <summary>
	///		Summary description for ProductCategories.
	/// </summary>
	public class ProductCategories : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label PageDescriptionLabel;
		protected System.Web.UI.HtmlControls.HtmlGenericControl PageH1;
		protected System.Web.UI.WebControls.PlaceHolder ProductCategoriesPlaceHolder;

		private int packageId;
		private int productId;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			SetGeneralValues();
			DisplayPackages();
		}



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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Methods


		private void DisplayPackages()
		{
			ProductSubCategory productSubCategory;
			
			PackageCollection packages = Package.GetPackagesByParentPackageID(PackageId);
			
			packages.Sort(new PackageDisplayOrderComparer());

			foreach(Package p in packages)
			{
				
				productSubCategory = (ProductSubCategory) LoadControl("ProductSubCategory.ascx"); 
				productSubCategory.PackageId=p.PackageId;
				ProductCategoriesPlaceHolder.Controls.Add(productSubCategory); 

			}
		}

		#endregion Methods


		#region Methods

		protected void SetGeneralValues()
		{
			if(packageId != 0)
			{
				Package package = Package.GetPackageByID(packageId);
				package.PackageDescription = PackageDesc.GetPackageDescByID(packageId);

				PageH1.InnerHtml = package.PackageDescription.PageTitle + "<br><img src=\"" + GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"] + "/Resources/Images/_fund_/_classic_/en-us/stroke.gif" +"\" border=0 alt='fundraising.com spacer'>";
				PageDescriptionLabel.Text = package.PackageDescription.LongDesc;
			}
			else
			{
				Product product = Product.GetProductByID(productId);
				product.ProductDescription = ProductDesc.GetProductDescByID(productId);

                PageH1.InnerHtml = product.ProductDescription.PageTitle + "<br><img src=\"" + GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"] + "/Resources/Images/_fund_/_classic_/en-us/stroke.gif" + "\" border=0 alt='fundraising.com spacer'>";
				PageDescriptionLabel.Text = product.ProductDescription.LongDesc;
			
				
				
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
