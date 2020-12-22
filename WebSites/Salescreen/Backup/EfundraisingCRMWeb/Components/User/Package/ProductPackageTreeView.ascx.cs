//using EFundraisingCRMWeb.App_Data;

namespace EFundraisingCRMWeb.Components.User.Package
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using efundraising.eFundraisingStore;
    using efundraising.Diagnostics;
    using System.Web.Caching;


    /// <summary>
    ///		Summary description for ProductPackageTreeView.
    /// </summary>
    public partial class ProductPackageTreeView : System.Web.UI.UserControl
    {
        #region Constants
        protected string PACKAGE_IMG_PATH = "Ressources/Images/";
        #endregion




        public event EventHandler nodeSelelected;
        public event EventHandler selectProduct;
        public event EventHandler seeDetail;
        public event EventHandler cancel;



        private string appPath
        {
            get
            {
                string ApPath = Request.ApplicationPath;
                if (!ApPath.EndsWith("/"))
                    ApPath += "/";
                return ApPath;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            
            // Put user code to initialize the page here
            int rootPackageID = 0;
            PACKAGE_IMG_PATH = appPath + PACKAGE_IMG_PATH;
            if (!IsPostBack)
            {
                PackageCollection packageCol = null;
               
                rootPackageID = Components.Server.ManageSaleScreen.GetRootIDFromWebConfig();

                if (Cache["packageCol"] == null)
                {
                    //create tree
                    packageCol = efundraising.eFundraisingStore.Package.GetPackagesRoot(rootPackageID);
                }
                else
                {
                    packageCol = (PackageCollection) Cache["packageCol"];
                    
                    //call sp to update products, if an update, the regenerate the tree
                    bool update = efundraising.EFundraisingCRM.ScratchBook.UpdateProductsOnEfundWeb(rootPackageID);
                    if (update)
                    {
                        //create tree
                        packageCol = efundraising.eFundraisingStore.Package.GetPackagesRoot(rootPackageID);
                    }

                }

                LoadAllPackages(packageCol, rootPackageID);

            }
        }

        #region Properties
        public TreeView ProductPackageTree
        {
            get
            {
                return TreeView1;
            }
        }
        #endregion


        #region ToBe deleted
        private void LoadTreeView()
        {

            
            DataSet ds = new DataSet();
            DataTable dtPackages = efundraising.eFundraisingStore.Package.GetPackagesInDataTable().Copy();
            DataRow[] rowsRoot = dtPackages.Copy().Select("Isnull(Parent_package_id, 0) = 0");
            // Root Table
            DataTable roots = dtPackages.Clone();
            System.Text.StringBuilder strParentIds = new System.Text.StringBuilder();
            for (int i = 0; i < rowsRoot.Length; i++)
            {
                roots.ImportRow(rowsRoot[i]);
                strParentIds.Append("," + rowsRoot[i]["package_id"].ToString());
            }
            string parentTableName = "Root";
            roots.TableName = parentTableName;
            ds.Tables.Add(roots);
            string sTmp = strParentIds.ToString();
            TreeNode nodeParent;
            TreeNode nodeChild = null;
            TreeNodeCollection nodesCollection = TreeView1.Nodes;
            if (sTmp != null && sTmp.Length > 1)
            {
                sTmp = sTmp.Substring(1);
                DataRow[] rowsLevel = dtPackages.Select(string.Format("Parent_package_id in ({0})", sTmp));
                int iCount = 0;
                int iMaxLevel = 16;
                while (rowsLevel != null && rowsLevel.Length > 0 && iCount < iMaxLevel)
                {
                    DataTable dtTmp = dtPackages.Clone();
                    string childTableName = "Level" + iCount.ToString();
                    string firstLevelRelation = "Relationship" + iCount.ToString();

                    System.Text.StringBuilder strTmpIds = new System.Text.StringBuilder();
                    for (int i = 1; i < rowsLevel.Length; i++)
                    {
                        dtTmp.ImportRow(rowsLevel[i]);
                        strTmpIds.Append("," + rowsLevel[i]["package_id"].ToString());
                    }
                    dtTmp.TableName = childTableName;
                    ds.Tables.Add(dtTmp);
                    //Create a relation between the Parent and the Child tables.
                    ds.Relations.Add(firstLevelRelation,
                        ds.Tables[parentTableName].Columns["Package_id"],
                        ds.Tables[childTableName].Columns["Parent_package_id"]);
                    //Populate the TreeView from the DataSet.
                    foreach (DataRow rowParent in ds.Tables[parentTableName].Rows)
                    {
                        nodeParent = new TreeNode();
                        nodeParent.Text = rowParent["name"].ToString();
                        nodesCollection.Add(nodeParent);
                        foreach (DataRow rowChildren in rowParent.GetChildRows(firstLevelRelation))
                        {
                            nodeChild = new TreeNode();
                            nodeChild.Text = rowChildren["name"].ToString();
                            nodeParent.ChildNodes.Add(nodeChild);
                        }
                    }
                    rowsLevel = null;
                    parentTableName = childTableName;
                    nodesCollection = nodeChild.ChildNodes;
                    sTmp = strTmpIds.ToString();
                    if (sTmp != null && sTmp.Length > 1)
                    {
                        sTmp = sTmp.Substring(1);
                        rowsLevel = dtPackages.Select(string.Format("Parent_package_id in ({0})", sTmp));
                    }
                    iCount++;
                }
            }

        }


       /* void doRecursion(TreeNodeCollection t, string theText)
        {
            EFundraisingCRM.Wildcards wc = new efundraising.EFundraisingCRM.Wildcards(theText, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            foreach (TreeNode tn in t)
            {
                if (wc.IsMatch(tn.Text))
                {
                    doParentExpanding(tn.GetSiblingNodeCollection());
                    return;
                }
                doRecursion(tn.Nodes, theText);
            }
        }
        void doParentExpanding(TreeNodeCollection t)
        {
            if (t.Parent.ToString() == "TreeNode")
            {
                TreeNode tn = (TreeNode)t.Parent;
                tn.Expanded = true;
                doParentExpanding(tn.GetSiblingNodeCollection());
            }
        }
        */
      /*  bool IsMatch(TreeNodeCollection t, string theText)
        {
            EFundraisingCRM.Wildcards wc = new efundraising.EFundraisingCRM.Wildcards(theText, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            foreach (TreeNode tn in t)
            {
                string thetmp = tn.Text;
                if (wc.IsMatch(thetmp))
                    return true;
                if (tn.Nodes != null && tn.Nodes.Count > 0)
                    if (IsMatch(tn.Nodes, theText))
                        return true;
            }
            return false;
        }*/

        //		public void LoadAllPackages(string searchString)
        //		{
        //			//LoadAllPackages(TreeView1.Nodes, searchString);
        //		}

        //		private void LoadAllPackages(Microsoft.Web.UI.WebControls.TreeNodeCollection t, string searchString)
        //		{
        //			eFundraisingStore.PackageCollection packageCol = GetTreeViewPackage();
        //			if (packageCol == null)
        //			{
        //				packageCol= eFundraisingStore.Package.GetPackagesRoot();
        //				//Create a node for every package, plus additional node for their children
        //			}
        //			t.Clear();
        //			if (searchString == null || searchString.Trim() == string.Empty)
        //			{
        //				
        //				LoadAllPackages(packageCol);
        //				return;
        //			}
        //			System.Collections.ArrayList ar = new System.Collections.ArrayList();
        //			EFundraisingCRM.Wildcards wc = new efundraising.EFundraisingCRM.Wildcards(searchString, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //			for (int i =0; i< packageCol.Count ; i++)
        //			{
        //				eFundraisingStore.Package pkg = packageCol[i] as eFundraisingStore.Package;
        //				if (IsPackageContain(pkg, searchString))
        //				{
        //					ar.Add(pkg);
        //				}
        //			}
        //			eFundraisingStore.PackageCollection pCol = new PackageCollection();
        //			for (int i=0; i< ar.Count; i++)
        //			{
        //				pCol.Add(ar[i] as eFundraisingStore.Package);
        //			}
        //			
        //			LoadAllPackages(pCol);
        //		}

    /*    bool IsPackageContain(eFundraisingStore.Package p, string searchString)
        {
            if (p != null)
            {
                EFundraisingCRM.Wildcards wc = new efundraising.EFundraisingCRM.Wildcards(searchString, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (p.Products != null && p.Products.Count > 0)
                {
                    for (int j = 0; j < p.Products.Count; j++)
                    {
                        eFundraisingStore.Product prd = p.Products[j] as eFundraisingStore.Product;
                        if (wc.IsMatch(prd.Name))
                            return true;
                    }
                }
                if (p.ChildrenPackages != null && p.ChildrenPackages.Count > 0)
                {
                    for (int j = 0; j < p.ChildrenPackages.Count; j++)
                    {
                        if (IsPackageContain(p.ChildrenPackages[j] as eFundraisingStore.Package, searchString))
                            return true;
                    }
                }
            }
            return false;
        }
        */

        #endregion

        #region  Load Tree
        //for every children of a specific parent, will call AddChild
        private void AddPackageChildren(TreeNode parentNode, Package package)
        {
            try
            {
                if (package.ChildrenPackages != null)
                {
                    foreach (Package childPackage in package.ChildrenPackages)
                    {
                        if (childPackage.Enabled != 0)
                        {
                            AddPackageChild(parentNode.ChildNodes, childPackage);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AddChildren", ex);
            }

        }

        // method that will add a child node to the parent node
        private void AddPackageChild(TreeNodeCollection nodes, Package package)
        {
            try
            {
                //Create a grandchild node and add it to its parent.
                TreeNode node = new TreeNode();
                node.Text = package.Name;
                node.Value = package.PackageId.ToString() + ";Package";

                if (package.Name == "CRM")
                {
                   node.Expanded = true;

                }
                /*else if (package.Name == "Frozen Food US")
                {
                    node.CheckBox = true;
                }*/
                string path = PACKAGE_IMG_PATH + "package.gif";
                node.ImageUrl = path;
                nodes.Add(node);

                LoadProductsForPackage(node, package);
                AddPackageChildren(node, package);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AddChild", ex);
            }

        }

        private void AddProductChild(TreeNodeCollection nodes, Product product)
        {
            try
            {
                //Create a grandchild node and add it to its parent.
                TreeNode node = new TreeNode();
                node.Text = product.Name;
                node.ShowCheckBox = true;
                node.Value = product.ProductId.ToString() + ";Product";
                nodes.Add(node);

                AddProductChildren(node, product);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AddChild", ex);
            }

        }
        private int productClassId
        {
            get
            {
                if (ViewState["productClassId"] == null)
                {
                    if (Request["sid"] == null)
                        return int.MinValue;

                    int sid = int.MinValue;
                    try
                    {
                        sid = System.Convert.ToInt32(Request["sid"]);
                        efundraising.EFundraisingCRM.SalesItem[] sItems = efundraising.EFundraisingCRM.SalesItem.GetSalesItemsBySaleID(sid);
                        if (sItems != null || sItems.Length > 0)
                        {
                            efundraising.EFundraisingCRM.ProductClass prdClass = efundraising.EFundraisingCRM.ProductClass.GetProductClassById(sItems[0].ProductClassId);
                            if (prdClass != null)
                            {
                                ViewState["productClassId"] = System.Convert.ToInt32(prdClass.ProductClassId);
                            }
                            else
                            {
                                ViewState["productClassId"] = int.MinValue;
                            }
                        }
                        else
                        {
                            ViewState["productClassId"] = int.MinValue;
                        }
                    }
                    catch (Exception)
                    {
                        return int.MinValue;
                    }
                }

                return System.Convert.ToInt32(ViewState["productClassId"]);
            }
        }


        //gets every products from the database et displays it in a Treeview
        private void LoadProductsForPackage(TreeNode packageNode, Package package)
        {
            try
            {
                //pourrait faire ca mais on a deja Products dans la classe Package
                //ProductCollection productCol = Product.GetProductsByPackaheID();

                //Create a node for every product, plus additional node for their children
                if (package.Products != null)
                {
                    if (productClassId < 0)
                    {
                        foreach (Product product in package.Products)
                        {
                            AddProductChild(packageNode.ChildNodes, product);

                        }
                    }
                    else
                    {
                        foreach (Product product in package.Products)
                        {
                            efundraising.EFundraisingCRM.ScratchBook scrb = Components.Server.ManageProduct.GetScratchBookByID(product.ScratchBookId, Session);
                            if (scrb.ProductClassId == productClassId)
                                AddProductChild(packageNode.ChildNodes, product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in LoadProductsForPackages", ex);
            }
        }

        // method that will add a child node to the parent node

        private void AddProductChildren(TreeNode parentNode, Product product)
        {
            try
            {

                if (product.ChildrenProducts != null)
                {
                    foreach (Product childProduct in product.ChildrenProducts)
                    {
                        AddProductChild(parentNode.ChildNodes, childProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AddChildren", ex);
            }
        }


        private efundraising.eFundraisingStore.PackageCollection GetTreeViewPackage()
        {
            return (efundraising.eFundraisingStore.PackageCollection)Components.Server.CurrentWorkingObject.Get(HttpContext.Current.Application, Global.TreeViewPackage);
        }




        private void SaveTreeViewPackage(efundraising.eFundraisingStore.PackageCollection theObject)
        {
            // save it for futher usage to the current working object
            Components.Server.CurrentWorkingObject.Save(theObject, HttpContext.Current.Application, Global.TreeViewPackage, null);
        }
        private void SaveTreeViewPackage(efundraising.eFundraisingStore.Package theObject)
        {
            // save it for futher usage to the current working object
            Components.Server.CurrentWorkingObject.Save(theObject, HttpContext.Current.Application, Global.TreeViewPackage, null);
        }


        private void LoadAllPackages(efundraising.eFundraisingStore.PackageCollection packageCol, int rootPackageID)
        {
            try
            {
                // Create the root tree node.
                TreeNode root = new TreeNode();

                root.Text = "All Packages and Products";
                root.Value = "0;Root";

                TreeView1.Nodes.Add(root);
                foreach (efundraising.eFundraisingStore.Package package in packageCol)
                {
                     AddPackageChild(root.ChildNodes, package);
                }
                root.Expanded = true;

                // Create the Create New Package node
                /*TreeNode newPackage = new TreeNode();
                newPackage.Text = "CREATE NEW PACKAGE";
                newPackage.Value = "0;NewPackage";

                TreeView1.Nodes.Add(newPackage);
				
                // Create the Create New Product node
                TreeNode newProduct = new TreeNode();
                newProduct.Text = "CREATE NEW PRODUCT";
                newProduct.Value = "0;NewProduct";
                TreeView1.Nodes.Add(newProduct);*/
               
                //SaveTreeViewPackage(packageCol);
                Cache.Insert("packageCol", packageCol, null,DateTime.Now.AddHours(12),TimeSpan.Zero,CacheItemPriority.High,null);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in LoadAllPackages", ex);
            }

        }

        private void LoadPackage(efundraising.eFundraisingStore.Package package)
        {
            try
            {
                // Create the root tree node.
                TreeNode root = new TreeNode();
                root.Text = "All Packages and Products";
                root.Value = "0;Root";

                TreeView1.Nodes.Add(root);
                AddPackageChild(root.ChildNodes, package);

                root.Expanded = true;

                // Create the Create New Package node
                /*TreeNode newPackage = new TreeNode();
                newPackage.Text = "CREATE NEW PACKAGE";
                newPackage.Value = "0;NewPackage";

                TreeView1.Nodes.Add(newPackage);
				
                // Create the Create New Product node
                TreeNode newProduct = new TreeNode();
                newProduct.Text = "CREATE NEW PRODUCT";
                newProduct.Value = "0;NewProduct";
                TreeView1.Nodes.Add(newProduct);*/
                SaveTreeViewPackage(package);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in LoadAllPackages", ex);
            }

        }






        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            //this.TreeView1.SelectedIndexChange +=new SelectEventHandler(TreeView1_SelectedIndexChange);
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        public void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (nodeSelelected != null)
            {
               nodeSelelected(sender, e);
            }
        }

        public TreeView GetTreeView()
        {
            return TreeView1;
        }



        private void SelectImage_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (selectProduct != null)
            {
                selectProduct(sender, e);
            }
        }

        private void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (seeDetail != null)
            {
                seeDetail(sender, e);
            }
        }

        private void CancelImage_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (cancel != null)
            {
                cancel(sender, e);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }


        protected void PackageTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }
        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {

        }
}
}
