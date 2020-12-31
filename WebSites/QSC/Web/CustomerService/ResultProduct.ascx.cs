namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;

	/// <summary>
	///		Summary description for ResultSubscriber.
	/// </summary>
	public class ResultProduct : ControlerResult
	{
		protected DataGridObject dtgMain;
		
		//private ParameterValueList pvlList;

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
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
			this.DataBinding += new System.EventHandler(this.List_DataBinding);
			this.dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);
			this.dtgMain.PageIndexChanged +=new DataGridPageChangedEventHandler(dtgMain_PageIndexChanged);
		}
		#endregion
		public override void DataBind()
		{
			this.dtgMain.CurrentPageIndex = this.Page.PageIndexSubNested;
			base.DataBind ();
		}
		private void List_DataBinding(object sender, System.EventArgs e)
		{
			DataGridItem dgi = (DataGridItem) this.BindingContainer;
			DataSet ds = (DataSet) dgi.DataItem;
			dtgMain.DataSource = ds;
			dtgMain.DataMember = "Product";
			dtgMain.DataBind();
			

		
			
		}
		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			//this.dtgMain.CurrentPageIndex = e.NewPageIndex;
			this.Page.NewSearch = false;
			this.Page.PageIndexSubNested = e.NewPageIndex;
			DataBind();

		}
		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == DataGrid.SelectCommandName)
			{
				//this.Page.FireEventSelect(GetOrderID(e.Item),gets);	
			}
		}
		private int GetOrderID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblOrderID")).Text);
		}
		
	}
}
