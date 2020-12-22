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

namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for PageTemplate.
	/// </summary>
	public class Test: System.Web.UI.Page// eFundraisingWeb.PackageProductBase
	{
		protected System.Web.UI.WebControls.PlaceHolder MainPlaceHolder = new PlaceHolder();
		protected GA.BDC.Core.Web.UI.MasterPages.Content Content1;
		protected GA.BDC.Core.Web.UI.MasterPages.MasterPage MasterPage1;
		public Test(){}

		private void Page_Load(object sender, System.EventArgs e)
		{
			ProductDescCollection productDescs = null;
			ProductPackageCollection productPackages  = ProductPackage.GetProductPackageByPackageID(18);
			foreach (ProductPackage productPackage in productPackages)
			{
				ProductDesc productDesc = ProductDesc.GetProductDescByID(productPackage.ProductId);
				productDescs.Add(productDesc);
			}
			/*ProductDescCollection productDescs = ProductDesc. .GetProductDescByID()  this.getTemplate(Request.Path.Substring(Request.Path.LastIndexOf(@"/") +1));
			efundraising.eFundraisingStore;.Culture. temp = Page.LoadControl(vdsfvsdn/cvsv);
			placeholder.add(temp)
				TEMP.datasource = productDsc;
			temp.Databind();*/
			
		}
	

		private void InitializeComponent()
		{
		
		}
	

		
	}
}