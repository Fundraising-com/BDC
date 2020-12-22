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
	using System.Diagnostics;


	/// <summary>
	////		Summary description for PackageInfo.
	///		This Object can be use for editing existing packages or creating new Packages
	/// </summary>
	public class PackageInfo : System.Web.UI.UserControl
	{
		
		private bool packageEnabledValue  = false;
		private short parentPackageValue = short.MinValue;
        private ProductCollection childProducts = null;


		protected System.Web.UI.WebControls.DropDownList ParentPackageDropDownList;
		protected efundraising.Web.UI.InputControls.StringTextBox PackageNameTextBox;
		protected efundraising.Web.UI.InputControls.IntegerTextBox ProfitPercentTextBox;
		protected System.Web.UI.WebControls.DropDownList PackageEnabledDropDownList;
		protected System.Web.UI.WebControls.Image ExistsImage;
	
		protected System.Web.UI.WebControls.Label PackageIDLabel;
	    		
	    //This control represent the Package Object
		private void Page_Load(object sender, System.EventArgs e)
		{
			try  
			{
		 		if (!(IsPostBack))
				{
        			// fill package drop down list
					Package[] packages = Package.GetPackages();
					foreach(Package package in packages) 
					{
						string name = package.Name + " (" + package.PackageId +")";
						ParentPackageDropDownList.Items.Add(new ListItem(name, package.PackageId.ToString()));
					}
					ParentPackageDropDownList.Items.Add(new ListItem("N/A", short.MinValue.ToString()));
			
					// fill child products drop down list if in Edit Mode
				/*	if (GetPackageIDLabel() != "")
					{
						ProductCollection products = Product.GetProductsByPackageID(Convert.ToInt16(GetPackageIDLabel()));
						if (products !=  null)
						{
							foreach(Product product in products) 
							{
								ChildProductsDropdownlist.Items.Add(new ListItem(product.Name, product.ProductId.ToString()));
							}
						}
					}*/
					
					//fill PackageEnabled drop down list
					PackageEnabledDropDownList.Items.Add(new ListItem("True","1"));
					PackageEnabledDropDownList.Items.Add(new ListItem("False","0"));
											
					PackageEnabledDropDownList.SelectedValue = Convert.ToInt32(packageEnabledValue).ToString();
					
					//set value of ParentPackage
				    ParentPackageDropDownList.SelectedValue = parentPackageValue.ToString();
					
				}
	

			}
			catch(Exception ex)
			{
                 Logger.LogError("Error in Page Load of PackageInfo",ex);
			}
			
		}

	    #region Private Methods

		//open page to edit package info
		private void EditButton_Click(object sender, System.EventArgs e)
		{

		/*	if (!(GetChildProduct() == 0))
			{
				//opens package info window
				Response.Redirect("AdministrationProductEdit.aspx?pid=" + GetChildProduct());
			}*/
		}

		//open page to edit the relations between package and products
		private void RelationsButton_Click(object sender, System.EventArgs e)
		{
			//get id from selected product
			if (!(GetPackageIDLabel().Equals("")))
			{
				Response.Redirect("AdministrationPackageProductLink.aspx?pkid=" + GetPackageIDLabel());
			}
		}

		//open page to create a new product
		private void AddButton_Click(object sender, System.EventArgs e)
		{
			//get id from selected product
			if (!(GetPackageIDLabel().Equals("")))
			{
				Response.Redirect("AdministrationProductNew.aspx?pid=" + GetPackageIDLabel());
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

		#region Set/Get Methods

		#region PackageIDLabel setter and getters 
		public string GetPackageIDLabel() 
		{
			return PackageIDLabel.Text;
		}	

		public void SetPackageIDLabel(string val) 
		{
			PackageIDLabel.Text = val;
		}
		#endregion

		#region Parent Package
		public short GetParentPackage() 
		{
			return Convert.ToInt16(ParentPackageDropDownList.SelectedValue);
		}

		public void SetParentPackage(short val) 
		{
	    	 parentPackageValue = val;
		}
		#endregion

		#region Package Enabled
		public bool GetPackageEnabled() 
		{
			return Convert.ToBoolean(Convert.ToInt32(PackageEnabledDropDownList.SelectedValue));
		}	

		public void SetPackageEnabled(bool val) 
		{
			packageEnabledValue = val;
		}
		#endregion

        #region ExistsImage
		public void SetExistsImage(bool val) 
		{
			ExistsImage.Visible = val;
		}
        #endregion


		#region PackageNameTextBox setter and getters 
		public string GetPackageNameTextBox() 
		{
			return PackageNameTextBox.Text;
		}	

		public void SetPackageNameTextBox(string val) 
		{
			PackageNameTextBox.Text = val;
		}
		#endregion
		
		#region ProfitPercentTextBox setter and getters 
		public string GetProfitPercentTextBox() 
		{
			if(ProfitPercentTextBox.Text == "")
			{
				return short.MinValue.ToString();
			}
			else
			{
				return ProfitPercentTextBox.Text;
			}
			
		}	

		public void SetProfitPercentTextBox(string val) 
		{
			if (val == short.MinValue.ToString())
			{
				ProfitPercentTextBox.Text = "";
			}
			else
			{
				ProfitPercentTextBox.Text = val;
			}
			
			
		}
		#endregion
			
	
		
        
#endregion

	


		

			

	}
}
