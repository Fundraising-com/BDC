namespace  efundraising.ScratchcardWeb.Components.User.Controls.Products
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using GA.BDC.Core.eFundraisingStore;

	/// <summary>
	///		Summary description for ProductCategorySummary.
	/// </summary>
	public class ProductCategorySummary : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label CategoryLabel;
		protected System.Web.UI.WebControls.HyperLink CategoryLink;
		protected System.Web.UI.WebControls.Image CategoryImage;
		protected System.Web.UI.WebControls.Image Image2;
		private int packageId;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			LoadPackageValue();
				
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
		
		private void LoadPackageValue()
		{
			Package package = Package.GetPackageByID(packageId);
			package.PackageDescription = PackageDesc.GetPackageDescByID(package.PackageId);

			CategoryImage.ImageUrl = GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"]+ GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingStore.ImageBasePath", "ImagePath"] +  package.PackageDescription.ImageName;
			CategoryLabel.Text=package.PackageDescription.ShortDesc;
			CategoryLink.NavigateUrl=GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"] + package.PackageDescription.PageName;
			CategoryLink.Text=package.PackageDescription.Name;
		}

		#endregion Methods

		#region Properties

		public int PackageId
		{
			get { return packageId; }
			set { packageId = value; }
		}
		#endregion Properties
	}
}
