namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for CatalogMaintenanceOneStepMenuControl.
	/// </summary>
	public partial class CompletedCatalog : MarketingMgtControl
	{

		private const string COMPLETED_CATALOG_MESSAGE = "The catalog [code] has been successfully modified.";


		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected void CompletedCatalog_PreRender(object sender, EventArgs e)
		{
			try 
			{
				if(!IsPostBack && this.Page.CompletedCatalog != "") 
				{
					this.lblCompletedCatalog.Text = COMPLETED_CATALOG_MESSAGE.Replace("[code]", this.Page.CompletedCatalog);
					this.lblCompletedCatalog.Attributes.CssStyle["margin"] = "10px";
				} 
				else 
				{
					this.lblCompletedCatalog.Text = "";
					this.lblCompletedCatalog.Attributes.CssStyle["margin"] = "";
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
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
			this.PreRender += new System.EventHandler(this.CompletedCatalog_PreRender);

		}
		#endregion
	}
}
