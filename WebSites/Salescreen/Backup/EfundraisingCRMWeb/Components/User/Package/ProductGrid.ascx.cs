namespace EFundraisingCRMWeb.Components.User.Package
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ProductGrid.
	/// </summary>
	public partial class ProductGrid : System.Web.UI.UserControl
	{
       // public System.Web.UI.WebControls.DataGrid ProductDataGrid;
        protected System.Web.UI.WebControls.LinkButton ProductLinkButton;
		private string productInSession = "productInSession";
		
		public event EventHandler gridRowSelected;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
				messagelabel.Visible = false;
		}

		private efundraising.eFundraisingStore.ProductCollection GetProductCollection()
		{
			return (efundraising.eFundraisingStore.ProductCollection)Components.Server.CurrentWorkingObject.Get(this.Page.Session, productInSession);
		}

		
		private void SaveProductCollection(efundraising.eFundraisingStore.ProductCollection prdCol)
		{
			// save it for futher usage to the current working object
			Components.Server.CurrentWorkingObject.Save(prdCol, this.Page.Session , productInSession, null);
		}

		public void DoBinding(efundraising.eFundraisingStore.ProductCollection prdCol)
		{
            
			this.ProductDataGrid.CurrentPageIndex = 0;
			messagelabel.Visible = false;
			if (prdCol == null || prdCol.Count < 1)
			{
				messagelabel.Visible = true;
				messagelabel.Text = "No product found";
			}
			this.ProductDataGrid.DataSource = prdCol;
			this.ProductDataGrid.DataBind();
			SaveProductCollection(prdCol);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.ProductDataGrid.ItemCommand +=new DataGridCommandEventHandler(ProductDataGrid_ItemCommand);
			this.ProductDataGrid.PageIndexChanged +=new DataGridPageChangedEventHandler(ProductDataGrid_PageIndexChanged);
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

		private void ProductDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if (gridRowSelected != null)
				{
					//int iItemIndex = this.ProductDataGrid.CurrentPageIndex*this.ProductDataGrid.PageSize + e.Item.ItemIndex;
					int iItemIndex = e.Item.ItemIndex;
					gridRowSelected(this.ProductDataGrid.DataKeys[iItemIndex].ToString(), e);
				}
			}
		}

		
		private void ProductDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			efundraising.eFundraisingStore.ProductCollection prdCol = GetProductCollection();
			
			messagelabel.Visible = false;
			if (prdCol == null || prdCol.Count < 1)
			{
				messagelabel.Visible = true;
				messagelabel.Text = "No product found";
			}
			this.ProductDataGrid.CurrentPageIndex = e.NewPageIndex;
			this.ProductDataGrid.DataSource = prdCol;
			this.ProductDataGrid.DataBind();
		}

		public void ProductLink_Click(object sender, System.EventArgs e)
		{
			//Display Details for lead ID clicked
			LinkButton img = (LinkButton) sender;
			TableCell cell = (TableCell) img.Parent;
			DataGridItem item = (DataGridItem) cell.Parent;

            string a = this.ProductDataGrid.DataKeys[item.ItemIndex].ToString();
			gridRowSelected(a, e);
			
		}
        public DataGrid productDataGrid
        {
            get { return ProductDataGrid; }
        }

	}
}
