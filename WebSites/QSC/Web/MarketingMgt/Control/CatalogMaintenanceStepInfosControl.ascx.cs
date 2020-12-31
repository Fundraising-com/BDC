namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for CatalogMaintenanceStepInfos.
	/// </summary>
	public partial class CatalogMaintenanceStepInfosControl : MarketingMgtControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		private void CatalogMaintenanceStepInfosControl_PreRender(object sender, EventArgs e)
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
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new EventHandler(CatalogMaintenanceStepInfosControl_PreRender);
		}
		#endregion

		public override void DataBind()
		{
			SetValueCatalog();
			SetValueSection();
		}

		private void SetValueCatalog() 
		{
			if(this.Page.CatalogInfo != null && this.Page.CatalogInfo.Name != "") 
			{
				this.divInfos.Visible = true;
				this.lblCatalogName.Text = this.Page.CatalogInfo.Name;
			} 
			else 
			{
				this.divInfos.Visible = false;
			}
		}

		private void SetValueSection() 
		{
			if(this.Page.CatalogSectionInfo != null && this.Page.CatalogSectionInfo.Name != "") 
			{
				this.divSection.Visible = true;
				this.lblSectionName.Text = this.Page.CatalogSectionInfo.Name;
			} 
			else 
			{
				this.divSection.Visible = false;
			}
		}
	}
}
