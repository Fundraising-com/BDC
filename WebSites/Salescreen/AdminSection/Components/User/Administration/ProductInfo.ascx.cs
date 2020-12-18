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
	////		Summary description for ProductInfo.
	/// </summary>
	public class ProductInfo : System.Web.UI.UserControl
	{
	
		private bool productEnabledValue  = false;
		private bool productIsInnerValue  = false;
		private int parentProductValue = int.MinValue;
  
		protected System.Web.UI.WebControls.DropDownList ParentProductDropDownList;
		protected System.Web.UI.WebControls.Label ProductIDLabel;
		protected efundraising.Web.UI.InputControls.StringTextBox ProductCodeTextBox;
		protected efundraising.Web.UI.InputControls.DecimalTextBox RaisingPotentialTextBox;
		protected System.Web.UI.WebControls.DropDownList AccountingClassDropDownList;
		protected System.Web.UI.WebControls.DropDownList PackageLanguageDropDownList;
		protected efundraising.Web.UI.InputControls.IntegerTextBox DisplayOrderTextBox;
		protected System.Web.UI.WebControls.DropDownList SupplierDropDownlist;
		protected efundraising.Web.UI.InputControls.StringTextBox CurrentDescriptionTextBox;
		protected efundraising.Web.UI.InputControls.StringTextBox ProductNameTextBox;
		protected System.Web.UI.WebControls.DropDownList ProductEnabledDropDownList;
		protected System.Web.UI.WebControls.DropDownList IsInnerDropDownList;
		protected System.Web.UI.WebControls.DropDownList SupplierDropDownList;
		protected System.Web.UI.WebControls.Image ExistsImage;

	    //this control represents the Product Object
		private void Page_Load(object sender, System.EventArgs e)
		{
			try{
				if (!(IsPostBack))
				{
					// fill product drop down list
					Product[] products = Product.GetProducts();
					foreach(Product product in products) 
					{
						ParentProductDropDownList.Items.Add(new ListItem(product.Name, product.ProductId.ToString()));
					}
					ParentProductDropDownList.Items.Add(new ListItem("N/A", int.MinValue.ToString()));

			
					//fill ProductEnabled drop down list
					ProductEnabledDropDownList.Items.Add(new ListItem("True","1"));
					ProductEnabledDropDownList.Items.Add(new ListItem("False","0"));
					
					//fill IsInner drop down list
					IsInnerDropDownList.Items.Add(new ListItem("True","1"));
					IsInnerDropDownList.Items.Add(new ListItem("False","0"));

					//set value of ParentProduct
					ParentProductDropDownList.SelectedValue = parentProductValue.ToString();
			
					//sets value of Enabled
					ProductEnabledDropDownList.SelectedValue = Convert.ToInt32(productEnabledValue).ToString();
					
					//sets value of IsInner
					IsInnerDropDownList.SelectedValue = Convert.ToInt32(productIsInnerValue).ToString();
				}
			}catch(Exception ex)
			{
                Logger.LogError("Error in Page Load of ProductInfo",ex);
			}
		}
        
		#region Private Methods

		//open page to edit product info
		private void EditButton_Click(object sender, System.EventArgs e)
		{
			if (!(GetProductIDLabel().Equals("")))
			{
				//opens package info window
				Response.Redirect("AdministrationPackageProductLink.aspx?pid=" + GetProductIDLabel());
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

		#region ProductIDLabel setter and getters 
		public string GetProductIDLabel() 
		{
			return ProductIDLabel.Text;
		}	

		public void SetProductIDLabel(string val) 
		{
			ProductIDLabel.Text = val;
		}
		#endregion
	
		#region ProductNameTextBox setter and getters 
		public string GetProductNameTextBox() 
		{
			return ProductNameTextBox.Text;
		}	

		public void SetProductNameTextBox(string val) 
		{
			ProductNameTextBox.Text = val;
			
		}
		#endregion

		#region ScratchBookID setter and getters 
		public int GetScratchBookID() 
		{
			if (ViewState["ScratchBookID"] != null)
			{
				return  Convert.ToInt32(ViewState["ScratchBookID"]);
			}
			else
			{
				return 0;
			}

			
		}	

		public void SetScratchBookID(int val) 
		{
			ViewState["ScratchBookID"] = val;
			
		}
		#endregion

		#region ProductCodeTextBox setter and getters 
		public string GetProductCodeTextBox() 
		{
			return ProductCodeTextBox.Text;
		}	

		public void SetProductCodeTextBox(string val) 
		{
			ProductCodeTextBox.Text = val;
		}
		#endregion

		#region RaisingPotentialTextBox setter and getters 
		public string GetRaisingPotentialTextBox() 
		{
			if(RaisingPotentialTextBox.Text == "")
			{
				return Decimal.MinValue.ToString();
			}
			else
			{
				return RaisingPotentialTextBox.Text;
			}
		}	

		public void SetRaisingPotentialTextBox(string val) 
		{
			RaisingPotentialTextBox.Text = val.ToString();
		}
		#endregion

		#region Parent Product
		public int GetParentProduct() 
		{
			return Convert.ToInt32(ParentProductDropDownList.SelectedValue);
		}

		public void SetParentProduct(int val) 
		{
			parentProductValue = val;
	
		}
		#endregion
		
		#region Product Enabled
		public bool GetProductEnabled() 
		{
			return Convert.ToBoolean(Convert.ToInt32(ProductEnabledDropDownList.SelectedValue));
		}	

		public void SetProductEnabled(bool val) 
		{
			productEnabledValue = val;
		}
		#endregion

		#region Product Is Inner
		public bool GetProductIsInner() 
		{
			return Convert.ToBoolean(Convert.ToInt32(IsInnerDropDownList.SelectedValue));
		}	

		public void SetProductIsInner(bool val) 
		{
			productIsInnerValue = val;
		}
		#endregion

		#region ExistsImage
		public void SetExistsImage(bool val) 
		{
			ExistsImage.Visible = val;
		}
		#endregion

				
	
		
	

#endregion
	

	


	}
}
