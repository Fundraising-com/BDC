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

using System.Web.Caching;


namespace AdminSection
{
	/// <summary>
	/// Summary description for AdministrationPackages.
	/// </summary>
//	public class AdministrationProductPackage : AdministrationBasePage
//	{

//        #region Constants
//        protected string PACKAGE_IMG_PATH = "/Resources/Images/";
//        #endregion


//		protected efundraising.Web.UI.MasterPages.Content Content1;
//		protected efundraising.Web.UI.UIControls.PagePanelControl PagePanelControl1;
//		protected efundraising.Web.UI.UIControls.ContentPanelControl Contentpanelcontrol24;
//		protected System.Web.UI.WebControls.Button RefreshButton;
//		protected efundraising.Web.UI.UIControls.ContentPanelControl ContentPanelControl2;
//		protected efundraising.Web.UI.UIControls.ContentPanelControl ContentPanelControl4;
//		protected efundraising.Web.UI.UIControls.ContentPanelControl ContentPanelControl3;
//		protected efundraising.Web.UI.UIControls.ContentPanelControl ContentPanelControl1;
//		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
//        protected System.Web.UI.WebControls.TreeView PackageTreeview;
        
        
//        private string appPath
//        {
//            get
//            {
//                string ApPath = Request.ApplicationPath;
//                if (!ApPath.EndsWith("/"))
//                    ApPath += "/";
//                return ApPath;
//            }
//        }
	
//		//This Page lets the user browse the packages and products through as TreeView
//		private void Page_Load(object sender, System.EventArgs e)
//		{
            
//            //DataAccess._efundraisingStoreInterface efundStore = new DataAccess._efundraisingStoreInterface();
             
//            // Put user code to initialize the page here
//            int rootPackageID = 0;
//            PACKAGE_IMG_PATH = appPath + PACKAGE_IMG_PATH;
//            if (!IsPostBack)
//            {
//                PackageCollection packageCol = null;

//               // rootPackageID = 375;//Convert.ToInt32(efundraising.Configuration.ApplicationSettings.GetConfig()["SaleScreen.RootPackageID", "prod"]);


//                if (Cache["packageCol"] == null)
//                //if (packageCol == null)
//                {
//                    //create tree
//                    //packageCol = efundraising.eFundraisingStore.Package.GetPackagesRoot((rootPackageID);
//                    packageCol = efundraising.eFundraisingStore.Package.GetPackagesRoot();
//                  }
//                else
//                {
//                    packageCol = (PackageCollection)Cache["packageCol"];
                          

//                }

//                LoadAllPackages(packageCol, rootPackageID);


                



//            }
			
		
//		}


//		#region Web Form Designer generated code
//		override protected void OnInit(EventArgs e)
//		{
//			//
//			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
//			//
//			InitializeComponent();
//			base.OnInit(e);
//		}
		
//		/// <summary>
//		/// Required method for Designer support - do not modify
//		/// the contents of this method with the code editor.
//		/// </summary>
//		private void InitializeComponent()
//		{    
//		//	this.PackageTreeview.SelectedIndexChange += new Microsoft.Web.UI.WebControls.SelectEventHandler(this.PackageTreeview_SelectedIndexChange);
//			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
//			this.Load += new System.EventHandler(this.Page_Load);
            

//		}
//		#endregion

//		#region Private Methods 
		
//        private void LoadAllPackages(efundraising.eFundraisingStore.PackageCollection packageCol, int rootPackageID)
//        {
//            try
//            {
//                // Create the root tree node.
//                TreeNode root = new TreeNode();

//                root.Text = "All Packages and Products";
//                root.Value = "0;Root";

//                PackageTreeview.Nodes.Add(root);
//                foreach (efundraising.eFundraisingStore.Package package in packageCol)
//                {
//                    AddPackageChild(root.ChildNodes, package);
//                }
//                root.Expanded = true;

//                // Create the Create New Package node
//                TreeNode newPackage = new TreeNode();
//                newPackage.Text = "CREATE NEW PACKAGE";
//                newPackage.Value = "0;NewPackage";

//                PackageTreeview.Nodes.Add(newPackage);
				
//                // Create the Create New Product node
//                TreeNode newProduct = new TreeNode();
//                newProduct.Text = "CREATE NEW PRODUCT";
//                newProduct.Value = "0;NewProduct";
//                PackageTreeview.Nodes.Add(newProduct);

//                //SaveTreeViewPackage(packageCol);
//                Cache.Insert("packageCol", packageCol, null, DateTime.Now.AddHours(12), TimeSpan.Zero, CacheItemPriority.High, null);
//            }
//            catch (Exception ex)
//            {
//                Logger.LogError("Error in LoadAllPackages", ex);
//            }

//        }




		

        
//		// method that will add a child node to the parent node
//		private void AddPackageChild(TreeNodeCollection nodes, Package package)
//		{
//			try
//			{
//				//Create a grandchild node and add it to its parent.
//				TreeNode node = new TreeNode();
//				node.Text = package.Name;
//				node.Value = package.PackageId.ToString() + ";Package";
                
//                node.Expanded = false;
//                if (package.Name == "CRM")
//                {
//                    node.Expanded = true;

//                }
				
//				string path = PACKAGE_IMG_PATH + "package.gif";
//				node.ImageUrl =path.Substring(2, path.Length -2);
//				nodes.Add(node);

//				LoadProductsForPackage(node, package);
//				AddPackageChildren(node, package);
			    
//			}
//			catch(Exception ex)
//			{
//				Logger.LogError("Error in AddChild", ex);
//			}

//		}

//		//for every children of a specific parent, will call AddChild
//		private void AddPackageChildren(TreeNode parentNode, Package package)
//		{
//			try
//			{
//				if (package.ChildrenPackages != null)
//				{
//					foreach (Package childPackage in package.ChildrenPackages)
//					{
//						AddPackageChild(parentNode.ChildNodes,childPackage);
//					}
//				}
                
//				/*TreeNode node = new TreeNode();
//				node = new TreeNode();
//				node.Text = "ADD NEW PACKAGE";
//				node.Value = package.PackageId.ToString() + ";Package";
//				parentNode.Nodes.Add(node);*/

//			}
//			catch(Exception ex)
//			{
//				Logger.LogError("Error in AddChildren", ex);
//			}
			
//		}
	


	

//		//gets every products from the database et displays it in a Treeview
//		private void LoadProductsForPackage(TreeNode packageNode, Package package)
//		{
//			try
//			{
//				//pourrait faire ca mais on a deja Products dans la classe Package
//				//ProductCollection productCol = Product.GetProductsByPackaheID();
			
//				//Create a node for every product, plus additional node for their children
//				if (package.Products != null)
//				{
//					foreach (Product product in package.Products)
//					{
//						AddProductChild(packageNode.ChildNodes, product);
//					}
//				}
//     		}
//			catch(Exception ex)
//			{
//			    Logger.LogError("Error in LoadProductsForPackages", ex);
//			}
//    	}

//		// method that will add a child node to the parent node
//		private void AddProductChild(TreeNodeCollection nodes, Product product)
//		{
//			try
//			{
//				//Create a grandchild node and add it to its parent.
//				TreeNode node = new TreeNode();
//				node.Text = product.Name;
//				node.Value = product.ProductId.ToString() + ";Product";
//				nodes.Add(node);

//				AddProductChildren(node, product);
				
//			}
//			catch(Exception ex)
//			{
//				Logger.LogError("Error in AddChild", ex);
//			}

//		}
//		private void AddProductChildren(TreeNode parentNode, Product product)
//		{
//			try
//			{

//				if (product.ChildrenProducts != null)
//				{
//					foreach (Product childProduct in product.ChildrenProducts)
//					{
//						AddProductChild(parentNode.ChildNodes,childProduct);
//					}
//				}
//			}
//			catch(Exception ex)
//			{
//				Logger.LogError("Error in AddChildren", ex);
//			}
//		}

//		private void RefreshButton_Click(object sender, System.EventArgs e)
//		{
		  

//		}
//#endregion

//        protected void PackageTreeview_SelectedNodeChanged(object sender, EventArgs e)
//        {
//            string script = "";
//            string[] ids = PackageTreeview.SelectedNode.Value.Split(';');
//            int id = Convert.ToInt32(ids[0]);
//            string type = ids[1];

//            //Check what package or product was selected, or if New was selected
//            switch (type)
//            {
//                case "Package":
//                    script = "AdministrationPackageEdit.aspx?pID=" + id;
//                    break;
//                case "Product":
//                    script = "AdministrationProductEdit.aspx?pID=" + id;
//                    break;
//                case "NewPackage":
//                    script = "AdministrationPackageNew.aspx";
//                    break;
//                case "NewProduct":
//                    script = "AdministrationProductImport.aspx";
//                    break;
//                case "Root":
//                    break;
//            }

//            //opens package info window
//            if (script != "")
//            {
//                //Page.RegisterClientScriptBlock("Open", script);  "AdministrationProductImport.aspx','Streaming', 'width=630, height=750, left = 400,top = 100,location=no, menubar=no, status=no, toolbar=no, scrollbars=no, resizable=yes')</script>"; 
//                Response.Redirect(script, false);
//            }
//        }

//        protected void RefreshButton_Click1(object sender, EventArgs e)
//        {
//            PackageTreeview.Nodes.Clear();

//            int rootPackageID = 0;//Convert.ToInt32(efundraising.Configuration.ApplicationSettings.GetConfig()["SaleScreen.RootPackageID", "prod"]);
//            PackageCollection packageCol = efundraising.eFundraisingStore.Package.GetPackagesRoot();
//            LoadAllPackages(packageCol, rootPackageID);
//        }

//        protected void UpdateButton_Click(object sender, EventArgs e)
//        {
//            efundraising.EFundraisingCRM.ScratchBook sb = efundraising.EFundraisingCRM.ScratchBook.GetScratchBookByID(1);
//            sb.Replicated = 0;
//            int result = sb.Update();

            
//        }

      

      
//	}
}
