namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.CustomerService;
	
	public delegate void SelectProductCategoryEventHandler(object sender, SelectProductCategoryClickedArgs e);

	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class ProductCategorySearchControl : MarketingMgtControlDataGrid
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblProductCode;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblTerm;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblMagInstance;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSearch;
		protected DataGridObject dtgMain;

		public event SelectProductCategoryEventHandler SelectProductCategoryClick;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,dtgMain,lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
			this.dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);
		}
		#endregion

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			SelectProductCategoryClickedArgs args;

			if(e.CommandName == "Select")
			{
				try 
				{
					args = new SelectProductCategoryClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.ProductCategory(GetInstance(e.Item), GetDescription(e.Item)));
				
					if(SelectProductCategoryClick != null)
						SelectProductCategoryClick(source,args);
				} 
				catch(Exception ex) 
				{
					this.Page.ManageError(ex);
				}
			}
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("ProductCategory");

			this.Page.BusProduct.SelectAllProductCategories(DataSource);
		}

		private int GetInstance(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblInstance")).Text);
		}
		private string GetDescription(DataGridItem e)
		{
			return ((Label)e.FindControl("lblDescription")).Text;
		}
	}
}