namespace AdminSection.Components.User.Administration
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using efundraising.eFundraisingStore;
	using efundraising.Diagnostics;

	/// <summary>
	///		Summary description for RelationsDatagrid.
	/// </summary>
	public class RelationsDatagrid : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataGrid PackageProductDataGrid;
        private int productID = 0;
		protected System.Web.UI.WebControls.Button CreateNewButton;
        private int packageID = 0;
//
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				//reset values from viewstate when postback
				if (ViewState["ProductID"] != null)
				{
					productID = Convert.ToInt32(ViewState["ProductID"]);
				}
				else if (ViewState["PackageID"] != null)
				{
					packageID = Convert.ToInt32(ViewState["PackageID"]);
				}

				//get product ID or package ID 
				if (productID != 0)
				{
					// bind collection object to data grid
					DataBind(productID, "Product");
					PackageProductDataGrid.Columns[2].Visible = false;
				}
				else if (packageID != 0) 
				{
					// bind collection object to data grid
					DataBind(packageID, "Package");
					PackageProductDataGrid.Columns[3].Visible = false;
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Page Load of rELATION dATAgRID. ProductID=" + productID,ex);	
				}
			
		}

		//this method get the PackageProduct information from its collection class
		//then creates a datatable to display it in a datagrid
		//It can either display information based on a Product or a Package
		private new void DataBind(int id, string type) 
		{
			string errorMsg = "";
			try
			{
				ProductPackageCollection productPackageCol;
				if (type.Equals("Product"))
				{
					// get the all the info related to the product id
					productPackageCol = ProductPackage.GetProductPackageByProductID(id);
					errorMsg = "ProductID = " + ID;
				}
				else
				{
					// get the all the info related to the package id
					productPackageCol = ProductPackage.GetProductPackageByPackageID((short)id);
					errorMsg = "PackageID = " + ID;
				}
				
				// get Datagrid information of the new requests and Binds it
				PackageProductDataGrid.DataSource = Components.Server.Administration.AdministrationPackageProduct.CreateDataTableRelation(productPackageCol);
				PackageProductDataGrid.DataBind();
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in DataBind of AdministrationPackageProduct." + errorMsg,ex);	
			}

		}

		//This method is called when the page of the datagrid changes
		private void PackageProductDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			PackageProductDataGrid.CurrentPageIndex = e.NewPageIndex;
			int id = (int) ViewState["ID"]; 
			string type = (string) ViewState["Type"]; 
			DataBind(id,type);
		}

		//this method is called when of Product link is clicked, gets the product id and redirects
		protected void ProductLink_Click(object sender, System.EventArgs e)
		{
			try
			{
				LinkButton lnk = (LinkButton) sender;
				TableCell cell = (TableCell) lnk.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;
				int i = item.ItemIndex;
		    
				int productID = Convert.ToInt32(PackageProductDataGrid.Items[i].Cells[0].Text);
				Response.Redirect("AdministrationProductEdit.aspx?pid=" + productID);
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in ProductLink Click of Relation DataGrid.",ex);	
			}
		}


		//this method is called when of Package link is clicked, gets the package id and redirects
		protected void PackageLink_Click(object sender, System.EventArgs e)
		{
			try
			{
				LinkButton lnk = (LinkButton) sender;
				TableCell cell = (TableCell) lnk.Parent;
				DataGridItem item = (DataGridItem) cell.Parent;
				int i = item.ItemIndex;
		    
				int packageID = Convert.ToInt32(PackageProductDataGrid.Items[i].Cells[1].Text);
				Response.Redirect("AdministrationPackageEdit.aspx?pid=" + packageID);
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in PackageLink Click of Relation DataGrid.",ex);	
			}
		}


		//this method will open a new page containing the information of the ProductPackage Link selected for editing
		private void PackageProductDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//check if an item is clicked
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				int productID = Convert.ToInt32(e.Item.Cells[0].Text);
				int packageID = Convert.ToInt32(e.Item.Cells[1].Text);
				string type = (string) ViewState["Type"]; 
				
				Response.Redirect("AdministrationPackageProductEdit.aspx?pid=" + productID + "&pkid=" + packageID + "&type=" + type);
			}
		}

		//this method will open a new Page to create a New ProductPackage Link
		private void CreateNewButton_Click(object sender, System.EventArgs e)
		{
			if (ViewState["ProductID"] != null)
			{
				Response.Redirect("AdministrationPackageProductNew.aspx?pid=" + Convert.ToInt32(ViewState["ProductID"]));
			}
			else if(ViewState["PackageID"] != null)
			{
				Response.Redirect("AdministrationPackageProductNew.aspx?pkid=" + Convert.ToInt32(ViewState["PackageID"]));
			}
			
			
		}

		#region Set/Get Methods

		#region PackageID setter 
		
		public void SetPackageID(int val) 
		{
			packageID =  val;
			ViewState["PackageID"] = val;
		}


		#endregion

		#region ProductID setter 
		
		public void SetProductID(int val) 
		{
			productID =  val;
			ViewState["ProductID"] = val;
		}


		#endregion

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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PackageProductDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.PackageProductDataGrid_ItemCommand);
			this.PackageProductDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.PackageProductDataGrid_PageIndexChanged_1);
			this.PackageProductDataGrid.SelectedIndexChanged += new System.EventHandler(this.PackageProductDataGrid_SelectedIndexChanged);
			this.CreateNewButton.Click += new System.EventHandler(this.CreateNewButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void PackageProductDataGrid_PageIndexChanged_1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{

			PackageProductDataGrid.CurrentPageIndex = e.NewPageIndex;
            int productID = 0;
			int packageID = 0;

			if (ViewState["ProductID"] != null)
			{
				productID = Convert.ToInt32(ViewState["ProductID"]);
			}
			else if (ViewState["PackageID"] != null)
			{
				packageID = Convert.ToInt32(ViewState["PackageID"]);
			}

			//get product ID or package ID 
			if (productID != 0)
			{
				// bind collection object to data grid
				DataBind(productID, "Product");
				//PackageProductDataGrid.Columns[2].Visible = false;
			}
			else if (packageID != 0) 
			{
				// bind collection object to data grid
				DataBind(packageID, "Package");
				//PackageProductDataGrid.Columns[3].Visible = false;
			}



		}

		private void PackageProductDataGrid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
		
		}

		
	}
}
