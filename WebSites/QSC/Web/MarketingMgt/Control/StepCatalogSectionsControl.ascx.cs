namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for StepCatalogInformationsControl.
	/// </summary>
	public partial class StepCatalogSectionsControl : CatalogMaintenanceStepControl
	{

		protected CatalogSectionSearchControl ctrlCatalogSectionSearchControl;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected void StepCatalogSectionsControl_PreRender(object sender, EventArgs e)
		{
			try 
			{
				DataBind();
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
			this.ctrlCatalogSectionSearchControl.SelectCatalogSectionClick += new SelectCatalogSectionEventHandler(ctrlCatalogSectionSearchControl_SelectCatalogSectionClick);
			this.ctrlCatalogSectionSearchControl.SelectCatalogSectionIncludeProducts += new SelectCatalogSectionEventHandler(ctrlCatalogSectionSearchControl_SelectCatalogSectionIncludeProducts);
			InitializeComponent();
			this.StepControl = Step.CatalogSections;
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new System.EventHandler(this.StepCatalogSectionsControl_PreRender);

		}
		#endregion

		protected void btnCreate_Click(object sender, System.EventArgs e)
		{
			string script;

			try 
			{
				this.Page.CatalogSectionInfo = null;

				script  = "<script language=\"javascript\">\n";
				script += "  OpenCustom('CatalogSectionMaintenance.aspx?IsNewWindow=true&CreateNew=true', 400, 300);\n";
				script += "</script>\n";

				this.Page.RegisterStartupScript("OpenPopUp", script);
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ctrlCatalogSectionSearchControl_SelectCatalogSectionClick(object sender, SelectCatalogSectionClickedArgs e)
		{
			string script;

			this.Page.CatalogSectionInfo = e.CatalogSectionInfo;

			script  = "<script language=\"javascript\">\n";
			script += "  OpenCustom('CatalogSectionMaintenance.aspx?IsNewWindow=true', 400, 300);\n";
			script += "</script>\n";

			this.Page.RegisterStartupScript("OpenPopUp", script);
		}

		private void ctrlCatalogSectionSearchControl_SelectCatalogSectionIncludeProducts(object sender, SelectCatalogSectionClickedArgs e)
		{
			this.Page.CatalogSectionInfo = e.CatalogSectionInfo;

			StepCompletedArgs args;
				
			args = new StepCompletedArgs(this.StepControl);
				
			OnStepCompleted(this, args);
		}

		public override void DataBind()
		{
			this.ctrlCatalogSectionSearchControl.DataBind();
		}
	}
}
