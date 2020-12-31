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
	
	public delegate void SelectPremiumEventHandler(object sender, SelectPremiumClickedArgs e);
	/// <summary>
	///		Summary description for CatalogSearchControl.
	/// </summary>
	public class PremiumSearchControl : MarketingMgtControlDataGrid
	{
		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblProductCode;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblTerm;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblMagInstance;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divSearch;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.DropDownList ddlNewRenew;
		protected System.Web.UI.WebControls.Label lblProductType;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox tbxCatalogCode;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlSeason;
		protected System.Web.UI.WebControls.DropDownList ddlType;
		protected System.Web.UI.WebControls.DropDownList ddlLanguage;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlStatus;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox tbxCampaignID;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox tbxProductCode;
		protected DataGridObject dtgMain;
		
		public event SelectPremiumEventHandler SelectPremiumClick;
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
			if(e.CommandName == "Edit")
			{
				try 
				{
					SelectPremiumClickedArgs args;
				
					args = new SelectPremiumClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.Premium(GetPremiumID(e.Item), GetPremiumCode(e.Item), GetYear(e.Item), GetSeason(e.Item), GetEnglishDescription(e.Item), GetFrenchDescription(e.Item), GetIsValid(e.Item)));
				
					if(SelectPremiumClick != null)
						SelectPremiumClick(source,args);
				} 
				catch(Exception ex) 
				{
					this.Page.ManageError(ex);
				}
			} 
		}

		public override void DataBind()
		{
			LoadData();
			Bind();
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Premium");

			this.Page.BusCatalog.SelectAllPremiums(DataSource);
		}

		private int GetPremiumID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblPremiumID")).Text);
		}
		private string GetPremiumCode(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblPremiumCode")).Text;
		}
		private int GetYear(DataGridItem e) 
		{
			return Convert.ToInt32(((Label)e.FindControl("lblYear")).Text);
		}
		private string GetSeason(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblSeason")).Text;
		}
		private string GetEnglishDescription(DataGridItem e)
		{
			return ((Label)e.FindControl("lblEnglishDescription")).Text;
		}
		private string GetFrenchDescription(DataGridItem e)
		{
			return ((Label)e.FindControl("lblFrenchDescription")).Text;
		}
		private string GetIsValid(DataGridItem e) 
		{
			return ((Label)e.FindControl("lblIsValid")).Text;
		}
	}
}