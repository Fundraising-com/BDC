namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for CatalogMaintenanceStepsMenuControl.
	/// </summary>
	public partial class CatalogMaintenanceStepsMenuControl : MarketingMgtControl
	{
		protected CatalogMaintenanceOneStepMenuControl ctrlCatalogMaintenanceOneStepMenuControlSelectCatalog;
		protected CatalogMaintenanceOneStepMenuControl ctrlCatalogMaintenanceOneStepMenuControlCatalogInformations;
		protected CatalogMaintenanceOneStepMenuControl ctrlCatalogMaintenanceOneStepMenuControlCatalogSections;
		protected CatalogMaintenanceOneStepMenuControl ctrlCatalogMaintenanceOneStepMenuControlIncludeProducts;

		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		private void CatalogMaintenanceStepsMenuControl_PreRender(object sender, EventArgs e)
		{
			try 
			{
				SetActiveSteps();
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
			this.ctrlCatalogMaintenanceOneStepMenuControlSelectCatalog.StepControl = Step.SelectCatalog;
			this.ctrlCatalogMaintenanceOneStepMenuControlCatalogInformations.StepControl = Step.CatalogInformations;
			this.ctrlCatalogMaintenanceOneStepMenuControlCatalogSections.StepControl = Step.CatalogSections;
			this.ctrlCatalogMaintenanceOneStepMenuControlIncludeProducts.StepControl = Step.IncludeProducts;
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new EventHandler(CatalogMaintenanceStepsMenuControl_PreRender);
		}
		#endregion

		private void SetActiveSteps() 
		{
			SetActiveStepSelectCatalog();
			SetActiveStepCatalogInformations();
			SetActiveStepCatalogSections();
			SetActiveStepIncludeProducts();
		}

		private void SetActiveStepSelectCatalog() 
		{
			this.ctrlCatalogMaintenanceOneStepMenuControlSelectCatalog.Visible = !this.Page.CreateNew;
			this.ctrlCatalogMaintenanceOneStepMenuControlSelectCatalog.Enabled = true;
		}

		private void SetActiveStepCatalogInformations() 
		{
			this.ctrlCatalogMaintenanceOneStepMenuControlCatalogInformations.Enabled = (this.Page.CreateNew || (this.Page.CatalogInfo != null && this.Page.CatalogInfo.CatalogID != 0));
		}

		private void SetActiveStepCatalogSections() 
		{
			this.ctrlCatalogMaintenanceOneStepMenuControlCatalogSections.Enabled = (this.Page.CatalogInfo != null && this.Page.CatalogInfo.CatalogID != 0);
		}

		private void SetActiveStepIncludeProducts() 
		{
			this.ctrlCatalogMaintenanceOneStepMenuControlIncludeProducts.Enabled = (this.Page.CatalogSectionInfo != null && this.Page.CatalogSectionInfo.CatalogSectionID != 0);
		}
	}
}
