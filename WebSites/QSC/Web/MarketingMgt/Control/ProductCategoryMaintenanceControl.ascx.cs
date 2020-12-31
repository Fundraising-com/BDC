namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepControl.
	/// </summary>
	public partial class ProductCategoryMaintenanceControl : MarketingMgtControl
	{

		public event SelectProductCategoryEventHandler SelectProductCategoryClick;
		public event System.EventHandler ProductCategoryCancelled;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
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

		}
		#endregion

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			SelectProductCategoryClickedArgs args;

			try 
			{
				SaveCategoryInformations();

				args = new SelectProductCategoryClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.ProductCategory(this.ProductCategoryInfo.Instance, GetDescription()));

				if(SelectProductCategoryClick != null)
					SelectProductCategoryClick(sender, args);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(ProductCategoryCancelled != null)
					ProductCategoryCancelled(sender, e);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public ProductCategory ProductCategoryInfo 
		{
			get 
			{
				return (ProductCategory) ViewState["ProductCategoryInfo"];
			}
			set 
			{
				ViewState["ProductCategoryInfo"] = value;
			}
		}

		public override void DataBind()
		{
			if(this.ProductCategoryInfo != null) 
			{
				SetValue();
			} 
			else 
			{
				SetValueEmpty();
			}
		}

		private void SetValue() 
		{
			this.tbxDescription.Text = this.ProductCategoryInfo.Description;
		}

		private void SetValueEmpty() 
		{
			this.tbxDescription.Text = "";
		}

		private void SaveCategoryInformations() 
		{
			if(this.ProductCategoryInfo != null) 
			{
				this.Page.BusCatalog.UpdateProductCategory(this.ProductCategoryInfo.Instance, GetDescription());
			}
			else 
			{
				this.ProductCategoryInfo = new ProductCategory();
				this.ProductCategoryInfo.Instance = this.Page.BusCatalog.InsertProductCategory(GetDescription());
			}
		}

		private string GetDescription() 
		{
			return this.tbxDescription.Text;
		}
	}
}
