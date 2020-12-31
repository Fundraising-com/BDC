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
	public partial class StepSelectCatalogControl : CatalogMaintenanceStepControl
	{

		protected CatalogSearchControl ctrlCatalogSearchControl;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				if(!IsPostBack) 
				{
					this.ctrlCatalogSearchControl.DataBind();
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
			this.StepControl = Step.SelectCatalog;
			this.ctrlCatalogSearchControl.SelectCatalogClick += new SelectCatalogEventHandler(ctrlCatalogSearchControl_SelectCatalogClick);
			this.ctrlCatalogSearchControl.SelectCatalogDelete += new SelectCatalogEventHandler(ctrlCatalogSearchControl_SelectCatalogDelete);
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

		private void ctrlCatalogSearchControl_SelectCatalogClick(object sender, SelectCatalogClickedArgs e)
		{
			this.Page.CatalogInfo = e.CatalogInfo;
			this.Page.CatalogSectionInfo = null;

			StepCompletedArgs args;
				
			args = new StepCompletedArgs(this.StepControl);
				
			OnStepCompleted(this, args);
		}

		/// <summary>
		/// CatalogSearchControl's "Catalog deleted" event handling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ctrlCatalogSearchControl_SelectCatalogDelete(object sender, SelectCatalogClickedArgs e)
		{
			this.Page.NewSearch = true;
			this.ctrlCatalogSearchControl.DataBind();
		}
	}
}
