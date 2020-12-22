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
using log4net;

//using EFundraisingCRMWeb.App_Data;

namespace EFundraisingCRMWeb.Components.User.Package
{
    /// <summary>
    /// Summary description for ProductSearch.
    /// </summary>
    public partial class ProductSearch : System.Web.UI.Page//EFundraisingCrmSalesBasePage, IPage
    {
        public ILog Logger { get; set; }

        public ProductSearch()
	    {
            Logger = LogManager.GetLogger(GetType());
	    }
        string allSelections = "";
        protected string TextFieldName = "";
        protected string script = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ProductGrid1.gridRowSelected += new EventHandler(ProductGrid1_gridRowSelected);

                 ErrorLabel.Visible = false;
                int scratchBookID = 0;
                // Put user code to initialize the page here
                if (!IsPostBack)
                {

                    MultiView1.SetActiveView(DetailView);

             
                    //Tab1.AddTab("Products", string.Empty, ProductPackageTreeView1);

                    if (Request["pID"] != "")
                    {
                        scratchBookID = Convert.ToInt32(Request["pID"]);
                        /////////////////////////////////////
                        if (scratchBookID > 0)
                        {
                            SetProductDesc(scratchBookID);

                        }
                        //////////////////////////////////////////////////
                        ///
                    }
                    TextFieldName = Request["name"];
                    ViewState["FieldName"] = TextFieldName;

                    script = @"<script language='javascript'>
				if (document.frames != null){
					try{						
						var oitem = window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "Calendarframe") + @"');
						var obody = window.frames.parent.document.body;
						oitem.style.width = window.document.getElementById('cadre').offsetWidth+2; 
						oitem.style.height = window.document.getElementById('cadre').offsetHeight+2;
						if((oitem.style.posHeight+oitem.style.posTop)>obody.offsetHeight)
							oitem.style.posTop=(obody.offsetHeight-oitem.style.posHeight);
						}
						catch(err)
						{
						}
				}
				</script>";
                    DataBind();




                }
                else
                    TextFieldName = (string)ViewState["FieldName"];

			
            

        }


        private void SetProductDesc(int scratchbookId)
        {

            //get product id from scratchbook 
            //may have multiple products for the same scratchbook id, 
            //we want the one under efundraisingCRM

            efundraising.eFundraisingStore.ProductDesc prdDesc = null;
            int rootID = Components.Server.ManageSaleScreen.GetRootIDFromWebConfig();

            efundraising.eFundraisingStore.ProductCollection products = efundraising.eFundraisingStore.Product.GetProductsByScratchBookID(scratchbookId, rootID);
            int i = 0;
            foreach (efundraising.eFundraisingStore.Product product in products)
            {
                i++;
                if (i == 1)
                {
                    //for now, get the first one
                    //int productRoot = eFundraisingStore.Product.GetProductRootIDByID(product.ProductId);
                    //if (productRoot == rootID)
                    //{
                    prdDesc = efundraising.eFundraisingStore.ProductDesc.GetProductDescByID(product.ProductId);

                    if (product != null && prdDesc != null)
                    {
                        ProductNameLabel.Text = prdDesc.Name;
                        if (prdDesc.LongDesc != "")
                        {
                            productDetailLabel.Text = prdDesc.LongDesc;
                        }
                        else
                        {
                            productDetailLabel.Text = prdDesc.ShortDesc;
                        }
                        //this.productDetailLabel.Text = string.Format("Product Code:{0}<BR>Description: {1}", prd.ProductCode , prd.Name);
                        hdProductName.Value = product.Name;
                        hdProductId.Value = product.ProductId.ToString();

                        SetProductClassTextBox(product);

                        MultiView1.SetActiveView(DetailView);
                    }
                    else
                    {
                        ErrorLabel.Visible = true;
                    }
                }
            }
        }


        private void SetProductClassTextBox(efundraising.eFundraisingStore.Product prd)
        {
            if (prd == null)
            {
                return;
            }
            efundraising.EFundraisingCRM.ScratchBook scrB = Components.Server.ManageProduct.GetScratchBookByID(prd.ScratchBookId, Session);

            if (scrB != null)
            {
                ProductCodeTextbox.Text = scrB.ProductCode;
                efundraising.EFundraisingCRM.ProductClass pc = efundraising.EFundraisingCRM.ProductClass.GetProductClassById(scrB.ProductClassId);
                if (pc != null)
                {
                    ProductClassTextbox.Text = pc.Description;
                    efundraising.EFundraisingCRM.Supplier supByObj = Global.GetSupplierByID(Application, System.Convert.ToInt16(scrB.SupplierId));

                    if (supByObj != null)
                    {
                        this.SupplierTextbox.Text = supByObj.SupplierName;
                    }
                }
                else
                {
                    ProductClassTextbox.Text = string.Empty;
                    ProductCodeTextbox.Text = string.Empty;
                    ErrorLabel.Visible = true;
                }
            }
            else
            {
                ProductClassTextbox.Text = string.Empty;
                ProductCodeTextbox.Text = string.Empty;
            }

        }

        protected void ProductsButton_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(TreeViewView);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(GridView);
        }
       

        private void FillSelection()
        {
            try
            {
                View view = MultiView1.GetActiveView();
                if (view.ID == "TreeViewView")
                {
                    TreeView treeView = ProductPackageTreeView1.GetTreeView();
                    foreach (TreeNode n in treeView.Nodes)
                    {
                        IsChecked(n);
                        CheckedChildren(n.ChildNodes);
                    }
                }
                else if (view.ID == "GridView")
                {
                    for (int i = 0; i < ProductGrid1.productDataGrid.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)ProductGrid1.productDataGrid.Items[i].FindControl("SelectCheckBox");

                        if (chk.Checked)
                        {
                            string scID = ProductGrid1.productDataGrid.Items[i].Cells[3].Text;
                            allSelections += "," + scID;

                        }
                    }
                }
                else if (view.ID == "DetailView")
                {


                        TreeView treeView = ProductPackageTreeView1.GetTreeView();
                        foreach (TreeNode n in treeView.Nodes)
                        {
                            IsChecked(n);
                            CheckedChildren(n.ChildNodes);
                        }

                    if (allSelections == ""){

                        if (hdProductId.Value.Trim() != string.Empty && hdProductId.Value.Trim() != "-1")
                        {

                            efundraising.eFundraisingStore.Product p = efundraising.eFundraisingStore.Product.GetProductByID(Convert.ToInt32(hdProductId.Value.Trim()));
                            allSelections = "," + p.ScratchBookId;
                        }
                    
                    }
                      


                    
                }

            }
            catch (Exception x)
            {
                Logger.Error("Sales Screen: FillSelection", x);
            }


        }

        private void IsChecked(TreeNode n)
        {
            if (n.Checked)
            {
                string[] split = n.Value.Split(';');
                if (split != null && split.Length > 1 && string.Compare(split[1], "Product", true) == 0)
                {
                    efundraising.eFundraisingStore.Product prd = Global.GetProductObject(int.Parse(split[0]));

                    int scID = prd.ScratchBookId;
                    if (!(scID > 0))
                    {
                        ErrorLabel.Visible = true;
                    }
                    else
                    {
                        allSelections = allSelections + "," + scID.ToString();
                    }
                }/*
				else //check if package
				{
					if (split != null && split.Length > 1 && string.Compare(split[1], "Package", true) == 0)
					{
						eFundraisingStore.Package .Product prd = Global.GetProductObject(int.Parse(split[0]));
				
						int scID = prd.ScratchBookId;
						if (!(scID > 0))
						{
							ErrorLabel.Visible = true;
						}
						else
						{
							allSelections = allSelections + "," + scID.ToString();
						}
					}
				
				}*/

            }

        }

        private void ProductGrid1_gridRowSelected(object sender, EventArgs e)
        {
            int productId = System.Convert.ToInt32(sender);

            efundraising.eFundraisingStore.Product prd = Global.GetProductObject(productId);
            if (prd.ScratchBookId > 0)
            {
                SetProductDesc(prd.ScratchBookId);

            }
            else
            {
                ErrorLabel.Visible = true;
            }
            
        }


        private void CheckedChildren(TreeNodeCollection parent)
        {
            foreach (TreeNode n in parent)
            {
                IsChecked(n);
                CheckedChildren(n.ChildNodes);
            }
        }

        protected void SelectButton_Click(object sender, ImageClickEventArgs e)
        {
            FillSelection();

            if (allSelections.Length > 1)
            {
                allSelections = allSelections.Substring(1, allSelections.Length - 1);
                System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
                strBuilder.Append("<script language='javascript'>window.frames.parent.document.getElementById('");
                strBuilder.Append(TextFieldName);
                //strBuilder.Append("productsearchframe");
                strBuilder.Append("').value = '");
                strBuilder.Append("1");
                strBuilder.Append("';window.frames.parent.document.getElementById('");
                strBuilder.Append(TextFieldName.Replace("TextBox1", "productidHidden"));
                strBuilder.Append("').value = '");
                strBuilder.Append(hdProductId.Value);
                strBuilder.Append("';window.frames.parent.document.getElementById('");
                strBuilder.Append(TextFieldName.Replace("TextBox1", "productsearchframe"));
                strBuilder.Append("').style.display = 'none';");
                strBuilder.Append(string.Format("window.frames.parent.__doPostBack('allproductselected','{0}')", allSelections + ";" + Request["rn"]) + ";</script>");
                
                script = strBuilder.ToString();
                hdProductId.Value.Trim();

                //DataBind();
                this.RegisterStartupScript("SelectLinkbutton_Click", script);
            }
        }

        protected void DetailButton_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(DetailView);
            FillSelection();
            
            //get the first id from the list
            if (allSelections != "")
            {
                string[] ids = allSelections.Remove(0, 1).Split(',');
                if (ids != null)
                {
                    int id = Convert.ToInt32(ids[0]);
                    SetProductDesc(id);
                }
            }
        }

        protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
        {
               
       script = @"<script language='javascript'>
					window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "productsearchframe") + @"').style.display = 'none'; 
		</script>";
            DataBind();
        }

  

   
        protected void ImageButton1_Click2(object sender, ImageClickEventArgs e)
        {
            
            script =  @"<script language='javascript'> window.frames.parent.document.getElementById('" + TextFieldName.Replace("TextBox1", "productsearchframe") + @"').style.display = 'none'; </script>";
            //DataBind();
            this.RegisterStartupScript("SelectLinkbutton_Click", script);
        }






        protected void SearchButton_Click(object sender, EventArgs e)
        {
            
            int rootID = Components.Server.ManageSaleScreen.GetRootIDFromWebConfig();
            efundraising.eFundraisingStore.ProductCollection prdCol = efundraising.eFundraisingStore.Product.GetProductsByNameSimilar(this.SearchTextBox.Text.Trim(), rootID);
            if (prdCol.Count == 0)
            {
                //try searching for product code
                prdCol = efundraising.eFundraisingStore.Product.GetProductsByProductCode(this.SearchTextBox.Text.Trim(), rootID);

            }
            //efundraising.eFundraisingStore.ProductCollection prdCol = SearchMatchProduct(this.SearchTextBox.Value.Trim());
            ProductGrid1.DoBinding(prdCol);
            MultiView1.SetActiveView(GridView);
       
        }
      
}
}