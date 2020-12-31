using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.MarketingMgt.Control;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductMaintenance.
	/// </summary>
	public partial class CatalogSectionMaintenance : MarketingMgtPage
	{
		protected CatalogSectionMaintenanceControl ctrlCatalogSectionMaintenanceControl;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		private void CatalogSectionMaintenance_PreRender(object sender, EventArgs e)
		{
			if(!IsPostBack) 
			{
				try 
				{
					this.ctrlCatalogSectionMaintenanceControl.DataBind();
				} 
				catch(Exception ex) 
				{
					ManageError(ex);
				}
			}
		}
		
 		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlCatalogSectionMaintenanceControl.CatalogSectionSaved += new SelectCatalogSectionEventHandler(ctrlCatalogSectionMaintenanceControl_CatalogSectionSaved);
			this.ctrlCatalogSectionMaintenanceControl.CatalogSectionCancelled += new EventHandler(ctrlCatalogSectionMaintenanceControl_CatalogSectionCancelled);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new EventHandler(CatalogSectionMaintenance_PreRender);
		}
		#endregion

		private void ctrlCatalogSectionMaintenanceControl_CatalogSectionSaved(object sender, SelectCatalogSectionClickedArgs e)
		{
			this.CatalogSectionInfo = e.CatalogSectionInfo;

			this.Page.RegisterStartupScript("ConfirmCloseReload","<script language=\"javascript\"> window.opener.pleasewait(); window.opener.Refresh(); self.close(); window.parent.focus(); </script>");
		}

		private void ctrlCatalogSectionMaintenanceControl_CatalogSectionCancelled(object sender, EventArgs e)
		{
			this.Page.RegisterStartupScript("ConfirmCloseReload","<script language=\"javascript\"> window.opener.pleasewait(); window.opener.Refresh(); self.close(); window.parent.focus(); </script>");
		}
	}
}
