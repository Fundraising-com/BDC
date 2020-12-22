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
	///		Summary description for PackageProductLinkInfo.
	/// </summary>
	public class PackageProductLinkInfo : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList PackageDropDownList;
		protected System.Web.UI.WebControls.DropDownList DisplayDropdownlist;
		protected efundraising.Web.UI.InputControls.IntegerTextBox DisplayOrderTextBox;
		protected System.Web.UI.WebControls.DropDownList ProductDropdownlist;

	    private string displayValue  = null;
		private int packageValue = int.MinValue;
		private int productValue = int.MinValue;
		

        //This control represent PackageProduct relation Object
		//It lists all the products associated with a package and vice versa
		private void Page_Load(object sender, System.EventArgs e)
		{
			try  
			{
				if (!(IsPostBack))
				{
					// fill product drop down list
					Product[] products = Product.GetProducts();
					foreach(Product product in products) 
					{
						ProductDropdownlist.Items.Add(new ListItem(product.Name + "(" + product.ProductId.ToString() + ")", product.ProductId.ToString()));
					}
				
					// fill package drop down list
					Package[] packages = Package.GetPackages();
					foreach(Package package in packages) 
					{
						PackageDropDownList.Items.Add(new ListItem(package.Name + "(" + package.PackageId.ToString() + ")", package.PackageId.ToString()));
					}
				
					//fill Display drop down list
					DisplayDropdownlist.Items.Add(new ListItem("True","1"));
					DisplayDropdownlist.Items.Add(new ListItem("False","0"));

		
					//set value of Product
					if(productValue != int.MinValue) 
					{
						ProductDropdownlist.SelectedValue = productValue.ToString();
					}
				

					//set value of Package
					if(packageValue != int.MinValue) 
					{
						PackageDropDownList.SelectedValue = packageValue.ToString();
					}
				
					//sets display value
					if(displayValue != null) 
					{
						DisplayDropdownlist.SelectedValue = displayValue.ToString();
					}

				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Page Load of PackageProductLinkInfo",ex);
			}

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

		#region Set/Get Methods

		#region Package
		public short GetPackage() 
		{
			return Convert.ToInt16(PackageDropDownList.SelectedValue);
		}

		public void SetPackage(short val) 
		{
			packageValue = val;
	
		}
		#endregion

		#region Product
		public int GetProduct() 
		{
			return Convert.ToInt32(ProductDropdownlist.SelectedValue);
		}

		public void SetProduct(int val) 
		{
			productValue = val;
	
		}
		#endregion

		#region DisplayOrderTextBox setter and getters 
		public short GetDisplayOrderTextBox() 
		{
			return short.Parse(DisplayOrderTextBox.Text);
		}	

		public void SetDisplayOrderTextBox(short val) 
		{
			if (val != short.MinValue)
			{
				DisplayOrderTextBox.Text = val.ToString();
			}
			else
			{
				DisplayOrderTextBox.Text = "0";
			}
		}
		#endregion

		#region Display
		public string GetDisplay() 
		{
			return DisplayDropdownlist.SelectedValue;
		}	

		public void SetDisplay(string val) 
		{
			displayValue = val;
		}
		#endregion
		
		#region ProductEnabled
		//Has to be set to disabled for Editing
		public void SetProductEnabled(bool val) 
		{
			ProductDropdownlist.Enabled = val;
		}
		#endregion

		#region PackagetEnabled
		//Has to be set to disabled for Editing
		public void SetPackageEnabled(bool val) 
		{
			PackageDropDownList.Enabled = val;
		}
		#endregion

		#endregion


		


	}
}
