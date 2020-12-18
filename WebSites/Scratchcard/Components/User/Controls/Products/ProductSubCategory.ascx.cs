namespace GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Products
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using GA.BDC.Core.eFundraisingStore;

	/// <summary>
	///		Summary description for ProductCategorySummary.
	/// </summary>
	public class ProductSubCategory : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label CategoryLabel;
		protected System.Web.UI.WebControls.HyperLink CategoryLink;
		protected System.Web.UI.WebControls.Image CategoryImage;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Table ProductsTable;
		protected System.Web.UI.WebControls.Image Image1;
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

			CategoryLabel.Text=package.PackageDescription.PageTitle;

			LoadProducts();

		}

		private void LoadProducts()
		{
			ProductCollection products = Product.GetProductsByPackageID(packageId);

			int cellCount = 1;
			TableRow tr = new TableRow();
			TableRow tr2 = new TableRow();

			TableCell td = new TableCell();
			TableCell td2 = new TableCell();

			if (products != null) 
			{

				//products.Sort(new ProductDisplayOrderComparer());

				foreach(Product p in products)
				{
					p.ProductDescription = ProductDesc.GetProductDescByID(p.ProductId);

					if(cellCount == 1)
					{
						tr = new TableRow();
						tr2 = new TableRow();
					}

					System.Web.UI.WebControls.Unit unit = new System.Web.UI.WebControls.Unit("33%");
					td = new TableCell();
					td2 = new TableCell();

					td.HorizontalAlign=System.Web.UI.WebControls.HorizontalAlign.Center;
					td.VerticalAlign=System.Web.UI.WebControls.VerticalAlign.Bottom;
					td.Width=unit;
				
					//td.Text= "<br><a href=\""+ Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"] + p.ProductDescription.PageName +"\">" + "<img border=0 src=\""+GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"]+ GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingStore.ImageBasePath", "ImagePath"] + p.ProductDescription.ImageName+"\"></a>";
				
					td2.HorizontalAlign=System.Web.UI.WebControls.HorizontalAlign.Center;
					td2.Width=unit;
					td2.VerticalAlign=System.Web.UI.WebControls.VerticalAlign.Top;
					//td2.Text= "<a href=\""+ Configuration.ApplicationSettings.GetConfig()["Common.Web.WebServer", "host"] + p.ProductDescription.PageName +"\">" + p.ProductDescription.Name +"</a>";
				
					tr.Cells.Add(td);
					tr2.Cells.Add(td2);

					if(cellCount == 3)
					{
						ProductsTable.Rows.Add(tr);
						ProductsTable.Rows.Add(tr2);
						cellCount=0;
					}
					cellCount+=1;
				}

				if(cellCount ==2)
				{
					//add 2 cell + row
					tr.Cells.Add(new TableCell());
					tr.Cells.Add(new TableCell());
					ProductsTable.Rows.Add(tr);

					tr2.Cells.Add(new TableCell());
					tr2.Cells.Add(new TableCell());
					ProductsTable.Rows.Add(tr2);
				}
				else if (cellCount ==3)
				{
					//add 1 cell + row
					tr.Cells.Add(new TableCell());
					ProductsTable.Rows.Add(tr);

					tr2.Cells.Add(new TableCell());
					ProductsTable.Rows.Add(tr2);
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
		#endregion Properties
	}
}
