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
using efundraising.Diagnostics;

namespace AdminSection
{
	/// <summary>
	/// Summary description for AdministrationProductImport.
	/// </summary>
	public class AdministrationProductImport : AdministrationBasePage
	{
		protected System.Web.UI.WebControls.DropDownList ProductClassDropDownList;
		protected System.Web.UI.WebControls.DropDownList ProductCodeDropDownList;
		protected System.Web.UI.WebControls.DropDownList ProductDropDownList;
		protected efundraising.Web.UI.UIControls.PagePanelControl PagePanelControl1;
		protected efundraising.Web.UI.MasterPages.Content Content1;
		protected efundraising.Web.UI.MasterPages.MasterPage MasterPage1;
		protected System.Web.UI.WebControls.Button OKButton;
		protected System.Web.UI.WebControls.Button CloseButton;
	
		///This Page lets the user Import a Product from the Sqleorse-Efundraising database,
		//to then create a new product
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (!(IsPostBack))
				{
					string productClassID = "";
				
					//fill product class dropdownlist
					efundraising.EFundraisingCRM.ProductClass[] productClasses = efundraising.EFundraisingCRM.ProductClass.GetProductClasss();
					foreach(efundraising.EFundraisingCRM.ProductClass productclass in productClasses) 
					{
						ProductClassDropDownList.Items.Add(new ListItem(productclass.Description, productclass.ProductClassId.ToString()));
					}
			        FillDropDownLists();

				
					//get product class from URL (for back button)
					if (Request["pcID"] != null)
					{
						productClassID = Request["pcID"].ToString();
						ProductClassDropDownList.SelectedValue = productClassID;
						FillDropDownLists();
					}
				}
			}catch(Exception ex)
			 {
				 Logger.LogError("Error on Page_Load in Admin ProductImport");
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			this.ProductClassDropDownList.SelectedIndexChanged += new System.EventHandler(this.ProductClassDropDownList_SelectedIndexChanged);
			this.ProductCodeDropDownList.SelectedIndexChanged += new System.EventHandler(this.ProductCodeDropDownList_SelectedIndexChanged);
			this.ProductDropDownList.SelectedIndexChanged += new System.EventHandler(this.ProductDropDownList_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Private Methods
		
		//fills all the the dropdowns when the product class is changed
		private void ProductClassDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillDropDownLists();
		}

		//fills both product and product code dropdowns
		private void FillDropDownLists()
		{
			//clear lists
			ProductCodeDropDownList.Items.Clear();
			ProductDropDownList.Items.Clear();
	
			//fill product code and product name dropdownlist based on selected product class
			efundraising.EFundraisingCRM.ScratchBook[] scratchbooks = efundraising.EFundraisingCRM.ScratchBook.GetScratchBooksByProductClassID(GetProductClass());
			foreach(efundraising.EFundraisingCRM.ScratchBook scratchbook in scratchbooks) 
			{
				ProductCodeDropDownList.Items.Add(new ListItem(scratchbook.ProductCode, scratchbook.ScratchBookId.ToString()));
				ProductDropDownList.Items.Add(new ListItem(scratchbook.Description, scratchbook.ScratchBookId.ToString()));
			}
		
		}


		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'> { window.close();}</script>");
		}
     
        //move to the Create Product Page, with the product id chosen
		private void OKButton_Click(object sender, System.EventArgs e)
		{
		    string scratchbookID = ProductDropDownList.SelectedValue;
			string productClassID = ProductClassDropDownList.SelectedValue;
			if (scratchbookID != "")
			{
				Response.Redirect("AdministrationProductNew.aspx?sbid=" + scratchbookID + "&pcid=" + productClassID);
	    	}
		}


        //when product is changed, product code must be changed
		private void ProductDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//synchrozine product code
			ProductCodeDropDownList.SelectedValue = ProductDropDownList.SelectedValue;
		}
	    
		
       //when product code is changed, product must be changed
		private void ProductCodeDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//synchrozine product name
			ProductDropDownList.SelectedValue = ProductCodeDropDownList.SelectedValue;
		
		}
			
		#endregion
	
		#region Product Class get set methods
		public int GetProductClass() 
		{
			return Convert.ToInt32(ProductClassDropDownList.SelectedValue);
		}
		
		/*public void SetProductClass(int val) 
		{
			ProductClass = val;
	
		}*/	
		

		#endregion
	}
}
